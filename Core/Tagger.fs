module Tagger

open Common
open System.Collections.Generic
open FSharpExt

type CommentCounter = 
    | OpenBraces of int
    interface ITagStats with
        member x.ValueEqual (y : ITagStats) =
            if y :? CommentCounter then 
                match x, y :?> CommentCounter with
                | OpenBraces n, OpenBraces m when n = m -> true
                | _                                     -> false
            else false

//splitFold takes three parameters. A function f, the starting accumulator and a list to fold.
//The function f takes two paramters: the current accumulator and the rest of the list.
//It produces the updated accumulator and the remainder of the list that it didn't process.
//splitFold repeatedly applies the function f to the remainders of the list until the function
//returns an empty list as a remainder.
let rec splitFold (f : 'a -> 'b list -> ('a * 'b list)) (acc : 'a) (list : 'b list) : 'a =
    let newAcc, rest = f acc list
    if List.isEmpty rest then newAcc
    else splitFold f newAcc rest

let startsWith (str : string) list =
    let len = str.Length
    let rec startsWithRec list i =
        if i = len then true
        else
            match list with
            | []        -> false
            | x :: rest -> x = str.[i] && startsWithRec rest (i + 1)
    startsWithRec list 0

type TagKind = Mismatch | Tag of char list * char list * CommentCounter

let consumeMultilineComment (OpenBraces n as stats) str =
    if startsWith "-}" str then
        Tag(List.take 2 str, List.drop 2 str, OpenBraces (n - 1))
    else if startsWith "{-" str then Tag(List.take 2 str, List.drop 2 str, OpenBraces (n + 1))
    else if n = 0 then Mismatch //We're not starting a comment and there isn't an ongoing one
    else Tag(List.take 1 str, List.tail str, stats)

let consumeText stats str = Tag(List.take 1 str, List.drop 1 str, stats)

let addToLastTag acc chars =
    match acc with
    | []                         -> failwith "Invalid operation"
    | (current, tagName) :: rest -> ((List.rev chars) @ current, tagName) :: rest

let lastTagIs acc name =
    match acc with
    | []                         -> false
    | (current, tagName) :: rest -> tagName = name

let parseSubstring (stats, (acc : (char list * string) list)) str =
    let parsers = [(consumeMultilineComment, "comment"); (consumeText, "text")]
    let rec firstMatch parsers =
        match parsers with
        | (parser, name) :: rest ->
            let res = parser stats str
            if res <> Mismatch then parser, name, res
            else firstMatch rest
        | []                     -> failwith "Invalid operation"
    let parser, name, (Tag(take, drop, stats)) = firstMatch parsers
    if lastTagIs acc name then (stats, addToLastTag acc take), drop
    else (stats, (List.rev take, name) :: acc), drop

[<DependencyProvider("tag")>]
let tag (str : string) (stats : ITagStats) =
    let listStr = [for c in str -> c]
    let braces = if stats = null then OpenBraces 0 else stats :?> CommentCounter
    let (newStats, tagList) = splitFold parseSubstring (braces, []) listStr
    let listForInterop = List.rev tagList |> List.map (fun (charList, name) -> System.String(List.rev charList |> List.toArray), name)
    Tagged(List<string * string>(listForInterop), newStats)