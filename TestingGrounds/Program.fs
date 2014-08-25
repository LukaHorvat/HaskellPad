// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

open FSharpExt
open CodeParser

[<EntryPoint>]
let main argv = 
    printfn "%A" <| CodeParser.keywordOrIdentifier CommentCounter.noComment (List.ofSeq "where")
    ignore <| System.Console.ReadLine()
    0 // return an integer exit code
