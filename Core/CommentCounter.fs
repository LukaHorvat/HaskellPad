module CommentCounter

open Common

type CommentCounter = 
    | OpenBraces of int
    interface ITagStats with
        member x.ValueEqual(y : ITagStats) = 
            match x with
            | OpenBraces n -> 
                if y = null && n = 0 then true
                else if y :? CommentCounter then 
                    match y :?> CommentCounter with
                    | OpenBraces m when n = m -> true
                    | _ -> false
                else false

let noComment = OpenBraces 0

let safeEquals (x : ITagStats) (y : ITagStats) = 
    if x = null && y = null then true
    else if x <> null then x.ValueEqual y
    else if y <> null then y.ValueEqual x
    else false
