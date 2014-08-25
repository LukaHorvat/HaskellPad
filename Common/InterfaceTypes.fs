namespace Common

open System.Collections.Generic

[<AllowNullLiteral>]
type ITagStats =
    abstract member ValueEqual : ITagStats -> bool

type Line = Raw of string | Tagged of ITagStats * List<string * string> * ITagStats 

/// <summary>
/// Properties and field with this attribute will have their value set before the OnLoad.
/// If a matching dependency isn't provided, an exception will be thown by the resolver (MainWindow.cs).
/// </summary>
type Dependency(name : string) =
    inherit System.Attribute()
    member x.matchName : string = name

type DependencyProvider(name : string) =
    inherit System.Attribute()
    member x.matchName : string = name