module Core

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
        else match list with
            | []        -> false
            | x :: rest -> x = str.[i] && startsWithRec rest (i + 1)
    startsWithRec list 0

type TagKind = Mismatch | TagContinuation of char list * char list * CommentCounter | TagEnd of char list * char list * CommentCounter

let consumeMultilineComment stats str = 
    match stats with
    | OpenBraces n ->
        if startsWith "-}" str then
            let ctor = if n = 1 then TagEnd else TagContinuation 
            ctor(List.take 2 str, List.drop 2 str, OpenBraces 0)
        else if startsWith "{-" str then TagContinuation(List.take 2 str, List.drop 2 str, OpenBraces (n + 1))
        else if n = 0 then Mismatch //We're not starting a comment and there isn't an ongoing one
        else TagContinuation(List.take 1 str, List.tail str, stats)


let addToLastTag acc char =
    match acc with
    | []                         -> failwith "Invalid operation"
    | (current, tagName) :: rest -> (char :: current, tagName) :: rest

let parseSubstring (stats, acc) str =
    match stats with
    | OpenBraces n when n > 0 ->

let tag line (stats : ITagStats) =
    let rec tagAcc line stats acc : Line =
        match stats with
        | OpenBraces n when n > 0 ->
            let comm, rest, level = eatMultilineComment n line
            if rest = "" then Tagged(List<string * string>((comm, "comment") :: acc), OpenBraces level)
            else tagAcc rest (OpenBraces level) ((comm, "comment") :: acc)
        | _                       ->
            

    tagAcc line (stats :?> CommentCounter) []