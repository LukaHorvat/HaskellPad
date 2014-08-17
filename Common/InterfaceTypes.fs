namespace Common

open System.Collections.Generic

type ITagStats =
    abstract member ValueEqual : ITagStats -> bool

type Line = Raw of string | Tagged of List<string * string> * ITagStats 
