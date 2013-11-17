module CheckmateTask.Permutations

open System

// gererates next lexicographical permutation on array; if needed, skips all subtrees from breakOn index
let getNext (array: int array) (breakOn: int option) = 
    // just utils for backwise seach in an array
    let findBackwise cond (array:'a array) =
        let rec findBackwise' cond array i =
            if cond array i then Some i
            elif i = 0 then None
            else findBackwise' cond array (i - 1)
        findBackwise' cond array (array.Length - 1)

    // for a single item
    let findLastIndex (cond: 'a -> bool) array = 
        findBackwise (fun array index -> cond array.[index]) array
    // for a pair of consequent items
    let findLastIndex2 (cond: 'a -> 'a -> bool) array = 
        findBackwise (fun array index -> index < (array.Length - 1) && cond array.[index] array.[index + 1]) array

    // standart lexicographical step; refactored as imperative for performance
    let getNextSequential array = 
        match findLastIndex2 (fun item nextItem -> item < nextItem) array with
        | None -> (None, 0)
        | Some k -> 
            match findLastIndex (fun item -> item > array.[k]) array with 
            | None -> (None, 0)
            | Some i -> 
                // i can be k + 1 or more
                let swap (array: 'a array) i j = 
                    let temp = array.[i]
                    array.[i] <- array.[j]; array.[j] <- temp;
                swap array i k
                let (restStart, restEnd) = (k + 1, array.Length - 1)
                for j = 0 to (restEnd - restStart + 1) / 2 - 1 do 
                    swap array (restStart + j) (restEnd - j)
                (Some array, k)

    let write array = Console.WriteLine((array |> Array.fold (fun acc i -> acc + i.ToString()) ""))
    match breakOn with // if break requested, use another path:
    | Some breakIndex -> 
        let breakItem = array.[breakIndex] 
        let rest = array |> Seq.skip breakIndex 
        match rest |> Seq.filter (fun i -> i > breakItem) |> Seq.toArray with  // if the break item is the largest of the rest, faildown to standart step
        | [| |] -> getNextSequential array
        | rest -> // else if the break item is not the largest of the rest
            let replacement = rest |> Array.min // find the replacement to the break item
            let res = (
                Some [| 
                    for i = 0 to breakIndex - 1 do  // take the starting of the sequence
                        yield array.[i] 
                    yield replacement // then replacement instead of the break item
                    yield! (seq { // and then the other items sorted lexicographycally
                        let once = ref true
                        for i = breakIndex + 1 to array.Length - 1 do 
                            if !once && array.[i] = replacement then 
                                once := false
                                yield breakItem
                            else 
                                yield array.[i]
                    }) |> Seq.sort
                |], 
                breakIndex
            )
            res
    | None -> // else just the standart step
        write array
        getNextSequential array
