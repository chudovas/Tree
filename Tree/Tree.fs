open System
open System.Collections.Generic


type InTree<'a> = 
    | Node of 'a * InTree<'a> * InTree<'a> 
    | Empty

type Tree<'a when 'a : comparison>() =    
    let mutable head = InTree.Empty
    
    let rec GetListOfElements node =
        match node with
        | InTree.Empty -> []
        | InTree.Node(a, l, r) -> (GetListOfElements l)@[a]@(GetListOfElements r)

    let rec AddRec node add =
        match node with
        | InTree.Node(a, l, r) when a < add -> InTree.Node(a, l, AddRec r add)
        | InTree.Node(a, l, r) when a >= add -> InTree.Node(a, AddRec l add, r)
        | InTree.Empty -> InTree.Node(add, InTree.Empty, InTree.Empty)
    
    let rec Overbalance left node =
        match left with
        | InTree.Empty -> node
        | InTree.Node(a, l, r) -> InTree.Node(a, Overbalance l node, r)

    let rec DeleteRec node del =
        match node with
        | InTree.Node(a, l, r) when a < del -> InTree.Node(a, l, DeleteRec r del)
        | InTree.Node(a, l, r) when a > del -> InTree.Node(a, DeleteRec l del, r)
        | InTree.Node(a, InTree.Empty, InTree.Empty) when a = del -> InTree.Empty
        | InTree.Node(a, l, InTree.Empty) when a = del -> l
        | InTree.Node(a, l, r) when a = del -> Overbalance r l
        | InTree.Empty -> InTree.Empty

    
    interface IEnumerable<'a> with
        member this.GetEnumerator() = 
            ((new List<'a>(this.GetList())).GetEnumerator() :> Collections.IEnumerator)
        member this.GetEnumerator() =
            ((new List<'a>(this.GetList())).GetEnumerator() :> IEnumerator<'a>)
    
    member x.Add(add : 'a) =
        head <- AddRec head add

    member x.Delete(del : 'a) =
        head <- DeleteRec head del

    member x.GetList() =
        GetListOfElements head


[<EntryPoint>]
let main argv =
    0 
   