open System
open System.Collections.Generic

/// <summary>
/// Класс листа дерева.
/// </summary>
type InTree<'a> = 
    | Node of 'a * InTree<'a> * InTree<'a> 
    | Empty

/// <summary>
/// Класс дерева.
/// </summary>
type Tree<'a when 'a : comparison>() =    
    let mutable head = InTree.Empty
    
    let rec getListOfElements node =
        match node with
        | InTree.Empty -> []
        | InTree.Node(a, l, r) -> (getListOfElements l) @ [a] @ (getListOfElements r)

    let rec addRec node add =
        match node with
        | InTree.Node(a, l, r) when a < add -> InTree.Node(a, l, addRec r add)
        | InTree.Node(a, l, r) when a >= add -> InTree.Node(a, addRec l add, r)
        | InTree.Empty -> InTree.Node(add, InTree.Empty, InTree.Empty)
    
    let rec overbalance left node =
        match left with
        | InTree.Empty -> node
        | InTree.Node(a, l, r) -> InTree.Node(a, overbalance l node, r)

    let rec deleteRec node del =
        match node with
        | InTree.Node(a, l, r) when a < del -> InTree.Node(a, l, deleteRec r del)
        | InTree.Node(a, l, r) when a > del -> InTree.Node(a, deleteRec l del, r)
        | InTree.Node(a, InTree.Empty, InTree.Empty) when a = del -> InTree.Empty
        | InTree.Node(a, l, InTree.Empty) when a = del -> l
        | InTree.Node(a, l, r) when a = del -> overbalance r l
        | InTree.Empty -> InTree.Empty
    
    interface IEnumerable<'a> with
        member this.GetEnumerator() = 
            ((new List<'a>(this.GetList())).GetEnumerator() :> Collections.IEnumerator)
        member this.GetEnumerator() =
            ((new List<'a>(this.GetList())).GetEnumerator() :> IEnumerator<'a>)
    
    /// <summary>
    /// Добавление элемента в дерево.
    /// </summary>
    /// <param name="add">Добавляемый элемент</param>
    member x.Add(add : 'a) =
        head <- addRec head add

    /// <summary>
    /// Удаление элемента из дерева.
    /// </summary>
    /// <param name="del">Удаляемый элемент</param>
    member x.Delete(del : 'a) =
        head <- deleteRec head del

    /// <summary>
    /// Преобразование дерева в list.
    /// </summary>
    member x.GetList() =
        getListOfElements head


[<EntryPoint>]
let main argv =
    0 
   