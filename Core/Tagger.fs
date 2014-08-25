module Tagger

open Common
open System.Collections.Generic
open FSharpExt
open CodeParser
open CommentCounter

let addToLastTag acc chars = 
    match acc with
    | [] -> failwith "Invalid operation"
    | (current, tagName) :: rest -> ((List.rev chars) @ current, tagName) :: rest

let lastTagIs acc name = 
    match acc with
    | [] -> false
    | (current, tagName) :: rest -> tagName = name

let parseSubstring (stats, (acc : (char list * string) list)) str = 
    let parsers = 
        [ CodeParser.multilineComment
          CodeParser.number
          CodeParser.keywordOrIdentifier
          CodeParser.operator
          CodeParser.any ]
    
    let rec firstMatch parsers = 
        match parsers with
        | parser :: rest -> 
            let res = parser stats str
            if res <> Mismatch then res
            else firstMatch rest
        | [] -> failwith "Invalid operation"
    
    let (Tag(take, drop, stats, tagName)) = firstMatch parsers
    if lastTagIs acc tagName then (stats, addToLastTag acc take), drop
    else (stats, (List.rev take, tagName) :: acc), drop

[<DependencyProvider("tag")>]
let tag (str : string) (stats : ITagStats) = 
    let listStr = 
        [ for c in str -> c ]
    
    let braces = 
        if stats = null then OpenBraces 0
        else stats :?> CommentCounter
    
    let (newStats, tagList) = List.splitFold parseSubstring (braces, []) listStr
    let listForInterop = 
        List.rev tagList |> List.map (fun (charList, name) -> System.String(List.rev charList |> List.toArray), name)
    Tagged(stats, List<string * string>(listForInterop), newStats)

let rec stateMap f state list = 
    match list with
    | [] -> []
    | x :: xs -> 
        let newState, y = f state x
        y :: stateMap f newState xs

let lineText line = 
    match line with
    | Raw str -> str
    | Tagged(_, tags, _) -> Seq.fold (fun str (token, _) -> str + token) "" tags

let rec tagLine (stats : ITagStats) line = 
    match line with
    | Raw str -> 
        let Tagged(_, _, newStats) as tagged = tag str stats
        (newStats, tagged)
    | Tagged(expectedState, tags, newState) as tagged -> 
        if safeEquals expectedState stats then (newState, tagged)
        else tagLine stats (Raw <| lineText tagged) //Just tag the same line but in it's raw form

[<DependencyProvider("tagLines")>]
let tagLines (lines : List<Line>) = 
    let list = List.ofSeq lines
    List<Line>(stateMap tagLine null list)
