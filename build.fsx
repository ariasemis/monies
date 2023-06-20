#r "paket:
nuget FSharp.Core 6.0.0.0
nuget Fake.Core.Target
nuget Fake.DotNet.Cli
nuget Fake.DotNet.Testing.Coverlet
nuget Fake.Tools.GitVersion
nuget GitVersion.CommandLine storage:packages
nuget Microsoft.Build 17.3.2
nuget Microsoft.Build.Framework 17.3.2
nuget Microsoft.Build.Tasks.Core 17.3.2
nuget Microsoft.Build.Utilities.Core 17.3.2
//"

// I'm pinning some dependencies due to these issues with FAKE:
// * https://github.com/fsprojects/FAKE/issues/2001
// * https://github.com/fsprojects/FAKE/issues/2719
// * https://github.com/fsprojects/FAKE/issues/2722
// I totally need to move away from FAKE, it is causing a lot of headaches...

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet
open Fake.DotNet.Testing
open Fake.Tools

// *********************************
// properties

let sln = "./src/Monies.sln"

module BuildConfiguration =
    let get () =
        DotNet.BuildConfiguration.fromEnvironVarOrDefault
            "BUILD_CONFIGURATION"
            DotNet.BuildConfiguration.Debug

    let toString (config: DotNet.BuildConfiguration) =
        match config with
            | DotNet.BuildConfiguration.Debug -> "Debug"
            | DotNet.BuildConfiguration.Release -> "Release"
            | DotNet.BuildConfiguration.Custom x -> x

let getVersion() =
    Trace.log " --- Computing version --- "
    let setParams (p: GitVersion.GitversionParams) =
        { p with ToolPath = ".fake/build.fsx/packages/GitVersion.CommandLine/tools/GitVersion.exe" }
    let version = GitVersion.generateProperties setParams
    Trace.logf "Version is : %s " version.SemVer
    Trace.setBuildNumber version.SemVer
    version

let version = lazy ( getVersion() )

// *********************************
// targets

Target.create "Clean" (fun _ ->
    Trace.log " --- Cleaning --- "

    let config = BuildConfiguration.get () |> BuildConfiguration.toString

    let setOptions (options: DotNet.Options) =
        { options with
            CustomParams = Some (sprintf "--configuration %s" config) }

    DotNet.exec setOptions "clean" sln |> ignore
)

Target.create "Restore" (fun _ ->
    Trace.log " --- Restoring Dependencies --- "

    DotNet.restore id sln
)

Target.create "Build" (fun _ ->
    Trace.log " --- Building --- "

    let setOptions (options: DotNet.BuildOptions) =
        { options with 
            Configuration = BuildConfiguration.get ()
            NoRestore = true }

    DotNet.build setOptions sln
)

Target.create "Test" (fun _ ->
    Trace.log " --- Running Tests --- "
    
    let setOptions (options: DotNet.TestOptions) =
        { options with
            Configuration = BuildConfiguration.get ()
            NoBuild = true
        }
        |> Coverlet.withDotNetTestOptions (fun p ->
            { p with
                OutputFormat = [Coverlet.OutputFormat.Lcov]
                Output = "TestResults/"
            })

    DotNet.test setOptions sln
)

Target.create "Pack" (fun _ ->
    Trace.log " --- Packaging nuget --- "

    let packageVersion = (version.Force()).NuGetVersion

    let setOptions (options: DotNet.PackOptions) =
        { options with
            Configuration = BuildConfiguration.get ()
            NoBuild = true
            Common = { options.Common with CustomParams = Some (sprintf "/p:PackageVersion=\"%s\"" packageVersion) } }
    
    DotNet.pack setOptions sln
)

Target.create "All" (fun _ -> Trace.log " --- END --- " )

// *********************************
// dependency graph

open Fake.Core.TargetOperators

"Clean"
    ==> "Restore"
    ==> "Build"
    ==> "Test"
    ==> "Pack"
    ==> "All"

// run
Target.runOrDefault "Build"