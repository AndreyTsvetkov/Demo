module CheckmateTask.Permutations

open System

let generatePermutations (set:int list) : seq<int list> = 
    let minimalPermutation = List.sort set

    let findLastIndex (cond: 'a -> bool) list = 
        let rec findLastIndex' condition list currentIndex earlierFoundIndexAndValue = 
            match list with
            | [] -> earlierFoundIndexAndValue
            | item :: otherItems -> 
                let newlyFoundIndexAndValue = if cond item then Some (currentIndex, item) else earlierFoundIndexAndValue
                findLastIndex' condition otherItems (currentIndex + 1) newlyFoundIndexAndValue

        findLastIndex' cond list 0 None

    let findLastIndex2 (cond: 'a -> 'a -> bool) list = 
        let rec findLastIndex2' condition list currentIndex earlierFoundIndexAndValues = 
            match list with
            | [] -> earlierFoundIndexAndValues
            | [_] -> earlierFoundIndexAndValues
            | item :: (nextItem :: otherItems) -> 
                let newlyFoundIndex = if cond item nextItem then Some (currentIndex, item, nextItem) else earlierFoundIndexAndValues
                findLastIndex2' condition (nextItem :: otherItems) (currentIndex + 1) newlyFoundIndex

        findLastIndex2' cond list 0 None

    let next (somePermutation: int list) : int list option = 
        match findLastIndex2 (fun item nextItem -> item < nextItem) somePermutation with
        | None -> None
        | Some (k, kItem, kNextItem) -> 
            match findLastIndex (fun item -> item > kItem) somePermutation with 
            | None -> None
            | Some (i, iItem) -> 
                // i can be k + 1 or more
                Some [
                    yield! somePermutation |> Seq.take k
                    yield iItem
                    yield! [
                        yield! somePermutation |> Seq.skip (k + 1) |> Seq.take (i - k - 1)
                        yield kItem
                        yield! somePermutation |> Seq.skip (i + 1)
                    ] |> List.rev
                ]

    let rec generate somePermutation = seq {
        yield somePermutation
        match next somePermutation with
        | Some p -> 
            yield! generate p
        | _ -> yield! []
    }

    generate minimalPermutation

// 
let generatePermutationsImperative (set: int list) : seq<int array*int> = 
    let workingArray = set |> List.sort |> Seq.toArray

    let findBackwise cond (array:'a array) =
        let rec findBackwise' cond array i =
            if cond array i then Some i
            elif i = 0 then None
            else findBackwise' cond array (i - 1)
        findBackwise' cond array (array.Length - 1)

    let findLastIndex (cond: 'a -> bool) array = 
        findBackwise (fun array index -> cond array.[index]) array
    let findLastIndex2 (cond: 'a -> 'a -> bool) array = 
        findBackwise (fun array index -> index < (array.Length - 1) && cond array.[index] array.[index + 1]) array

    seq {
        let stop = ref false
        let lastK = ref 0
        while not !stop do 
            yield (workingArray, !lastK)
            match findLastIndex2 (fun item nextItem -> item < nextItem) workingArray with
            | None -> stop := true
            | Some k -> 
                lastK := k
                match findLastIndex (fun item -> item > workingArray.[k]) workingArray with 
                | None -> stop := true
                | Some i -> 
                    // i can be k + 1 or more
                    let swap (array: 'a array) i j = 
                        let temp = array.[i]
                        array.[i] <- array.[j]; array.[j] <- temp;
                    swap workingArray i k
                    let (restStart, restEnd) = (k + 1, workingArray.Length - 1)
                    for j = 0 to (restEnd - restStart + 1) / 2 - 1 do 
                        swap workingArray (restStart + j) (restEnd - j)
    }
