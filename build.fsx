#r "paket:
nuget Fake.Core.Target
nuget Fake.DotNet.Cli
"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet

// properties
let sln = "./src/Monies.sln"

module BuildConfiguration =
    let get () =
        DotNet.BuildConfiguration.fromEnvironVarOrDefault
            "BuildConfiguration"
            DotNet.BuildConfiguration.Debug

    let toString (config: DotNet.BuildConfiguration) =
        match config with
            | DotNet.BuildConfiguration.Debug -> "Debug"
            | DotNet.BuildConfiguration.Release -> "Release"
            | DotNet.BuildConfiguration.Custom x -> x

// targets

Target.create "Clean" (fun _ ->
    Trace.log " --- Cleaning --- "

    let config = BuildConfiguration.get () |> BuildConfiguration.toString

    let setOptions (options: DotNet.Options) =
        { options with
            CustomParams = Some (sprintf "--configuration %s" config) }

    DotNet.exec setOptions "clean" sln |> ignore
)

Target.create "Build" (fun _ ->
    Trace.log " --- Building --- "

    let setOptions (options: DotNet.BuildOptions) =
        { options with 
            Configuration = BuildConfiguration.get () }

    DotNet.build setOptions sln
)

Target.create "Test" (fun _ ->
    Trace.log " --- Running Tests --- "
    
    let setOptions (options: DotNet.TestOptions) =
        { options with
            Configuration = BuildConfiguration.get ()
            NoBuild = true }

    DotNet.test setOptions sln
)

// dependency graph

open Fake.Core.TargetOperators

"Clean"
    ==> "Build"
    ==> "Test"

// run
Target.runOrDefault "Build"