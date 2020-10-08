#r "paket:
nuget Fake.Core.Target
"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core

// targets

Target.create "Clean" (fun _ ->
    Trace.log " --- Cleaning --- "
)

Target.create "Build" (fun _ ->
    Trace.log " --- Building --- "
)

Target.create "Test" (fun _ ->
    Trace.log " --- Running Tests --- "
)

// dependency graph

open Fake.Core.TargetOperators

"Clean"
    ==> "Build"
    ==> "Test"

// run
Target.runOrDefault "Build"