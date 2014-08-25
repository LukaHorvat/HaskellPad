module CodeParser

open CommentCounter
open FSharpExt
open FSharpExt.Parse

//Helpers
let startsWith (str : string) list = 
    let len = str.Length
    
    let rec startsWithRec list i = 
        if i = len then true
        else 
            match list with
            | [] -> false
            | x :: rest -> x = str.[i] && startsWithRec rest (i + 1)
    startsWithRec list 0

type TagKind = 
    | Mismatch
    | Tag of char list * char list * CommentCounter * string

let multilineComment (OpenBraces n as stats) str = 
    if startsWith "{-" str then Tag(List.take 2 str, List.drop 2 str, OpenBraces(n + 1), "comment")
    else if n = 0 then Mismatch //We're not starting a comment and there isn't an ongoing one
    else if startsWith "-}" str then Tag(List.take 2 str, List.drop 2 str, OpenBraces(n - 1), "comment")
    else Tag(List.take 1 str, List.tail str, stats, "comment")

let keywordsList = 
    "as case of class data data family data instance default deriving instance do forall foreign hiding if then else import infix infixl, infixr instance let in mdo module newtype proc qualified rec type type family type instance where"
        .Split(' ') |> List.ofSeq |> List.map List.ofSeq

let keywordOrIdentifier stats str = 
    let maybeWord = (Parse.letter |>| Parse.noneOrMore (Parse.word ||| Parse.digit ||| Parse.anyOf "'_")) str
    match maybeWord with
    | None -> Mismatch
    | Some(wrd, rest) -> 
        let len = List.length wrd
        let tag = Func.curry4 Tag (List.take len str) (List.drop len str) stats
        if List.contains wrd keywordsList then tag "keyword"
        else tag "identifier"

let number stats str =
    let exponent = Parse.anyOf "eE" |>| Parse.oneOrNone (Parse.anyOf "+-") |>| Parse.number
    match 
        (Parse.number 
            |>| ((Parse.char '.' |>| Parse.number |>| Parse.oneOrNone exponent) 
                ||| exponent)) 
        ||| (Parse.char '0' 
            |>| ((Parse.anyOf "oO" |>| Parse.oneOrMore (Parse.anyOf "01234567")) 
                ||| (Parse.anyOf "xX" |>| Parse.oneOrMore (Parse.anyOf "0123456789abcdefABCDEF"))))
        ||| Parse.number 
        <| str with
    | None -> Mismatch
    | Some(wrd, rest) -> Tag(wrd, rest, stats, "number")

let symbols = ['\\'; '-'; ':'; '.'; '_'; '!'; '#'; '$'; '%'; '&'; '*'; '+'; '/'; '<'; '='; '>'; '?'; '@'; '^'; '|'; '~']

let isSymbol c = List.contains c symbols || System.Char.IsSymbol c

let operator stats str =
    match Parse.oneOrMore (Parse.charCondition isSymbol) str with
    | None -> Mismatch
    | Some(wrd, rest) -> Tag(wrd, rest, stats, "operator")

let any stats str = Tag(List.take 1 str, List.drop 1 str, stats, "text")
