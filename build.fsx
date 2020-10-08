#r "paket:
nuget Fake.Core.Target
nuget Fake.IO.FileSystem
nuget Fake.DotNet.Cli
"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.DotNet

// properties
let sln = "./src/Monies.sln"

// targets

Target.create "Clean" (fun _ ->
    Trace.log " --- Cleaning --- "
    DotNet.exec id "clean" sln |> ignore
)

Target.create "Build" (fun _ ->
    Trace.log " --- Building --- "
    // TODO: build release
    DotNet.build id sln
)

Target.create "Test" (fun _ ->
    Trace.log " --- Running Tests --- "
    DotNet.test id sln
)

// dependency graph

open Fake.Core.TargetOperators

"Clean"
    ==> "Build"
    ==> "Test"

// run
Target.runOrDefault "Build"