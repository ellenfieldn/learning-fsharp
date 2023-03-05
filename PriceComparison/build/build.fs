open Fake.Core
open Fake.DotNet

open Fake.Core.TargetOperators

let DefineTargets () = 
  Target.create "Init" (fun _ -> Trace.log "---Initializing Build---" )
  Target.create "Build" (fun _ -> DotNet.build id "")

  "Init"
    ==> "Build"

[<EntryPoint>]
let main args = 
  args
  |> Array.toList
  |> Context.FakeExecutionContext.Create false "build.fsx"
  |> Context.RuntimeContext.Fake 
  |> Context.setExecutionContext
  DefineTargets ()
  Target.runOrDefaultWithArguments "Build"
  0