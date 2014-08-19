module Diff

open System.Collections.Generic
open Common

[<DependencyProvider("diff")>]
val diff : string [] -> string [] -> List<int>
