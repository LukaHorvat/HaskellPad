module Diff

open System.Collections.Generic
open FSharpExt

//Converts the second array to a list of indices to the matching strings in the first array or -1 if there's no match
let diff (oldLines : string []) (newLines : string []) =
    let oldMap = Seq.mapi (Func.flip Func.tuple) oldLines |> Map.ofSeq
    List<int>(Seq.map (Func.reorderBca Map.findOrDefault -1 oldMap) newLines)