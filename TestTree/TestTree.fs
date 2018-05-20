// Learn more about F# at http://fsharp.org

open System
open FsUnit
open NUnit.Framework
open Tree

[<Test>]
let ``test of tree 1``() =
    let tr = new Tree<int>()
    tr.Add(5)
    tr.Delete(10)
    tr.GetList() |> should equal [5]

[<Test>]
let ``test of tree 2``() =
    let tr = new Tree<int>()
    tr.Delete(10)
    tr.GetList() |> should equal []

[<Test>]
let ``test of tree 3``() =
    let tr = new Tree<char>()
    let mutable res = []
    tr.Add('d')
    tr.Add('c')
    tr.Add('a')
    tr.Add('b')
    tr.Add('e')
    tr.Add('f')
    
    for c in tr do
        res <- c::res

    res |> should equal ['f'; 'e'; 'd'; 'c'; 'b'; 'a']

[<Test>]
let ``test of tree 4``() =
    let tr = new Tree<char>()
    let mutable res = []
    tr.Add('d')
    tr.Add('c')
    tr.Add('a')
    tr.Add('b')
    tr.Add('e')
    tr.Add('f')
    
    tr.Delete('a')
    tr.Delete('b')
    for c in tr do
        res <- c::res
    
    res |> should equal ['f'; 'e'; 'd'; 'c']

[<EntryPoint>]
let main argv =
    ``test of tree 1``()
    ``test of tree 2``()
    ``test of tree 3``()
    ``test of tree 4``()
    0 // return an integer exit code
