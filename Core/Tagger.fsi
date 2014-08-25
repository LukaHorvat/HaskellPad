module Tagger

open Common
open System.Collections.Generic

val tag : string -> ITagStats -> Line
val tagLines : List<Line> -> List<Line>