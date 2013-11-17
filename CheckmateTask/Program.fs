module CheckmateTask.Main

open System
open System.Text.RegularExpressions
open System.IO
open System.Linq

type BoardSize = { Width: int; Height: int}
type Position = { X: int; Y: int } with 
    member x.IsNeighbourTo (other:Position) = match x.GetAbsoluteDeltaTo other with | (dX, dY) -> dX <= 1 && dY <= 1
    member x.IsSameStraightLineWith (other:Position) = x.X = other.X || x.Y = other.Y 
    member x.IsSameDiagonalLineWith (other:Position) = match x.GetAbsoluteDeltaTo other with | (dX, dY) -> dX = dY
    member x.GetAbsoluteDeltaTo (other:Position) = (Math.Abs (x.X - other.X), Math.Abs (x.Y - other.Y))
    override x.ToString() = String.Format("{0},{1}", x.X, x.Y)

type Figure = 
    | Empty
    | King
    | Queen
    | Bishop
    | Rook
    | Knight
    member inline x.CanAttackFrom (my:Position) (target:Position) = 
        if my = target then false 
        else
            match x with 
            | Empty -> false
            | King -> my.IsNeighbourTo target
            | Queen -> my.IsSameStraightLineWith target || my.IsSameDiagonalLineWith target
            | Bishop -> my.IsSameDiagonalLineWith target
            | Rook -> my.IsSameStraightLineWith target
            | Knight ->
                let absoluteDelta = my.GetAbsoluteDeltaTo target
                absoluteDelta = (2, 1) || absoluteDelta = (1, 2)
    override x.ToString() = SerializationHelpers.toString x

type Population = Map<Figure, int>

type Placement = { Figure: Figure; Position: Position } with 
    override x.ToString() = String.Format("{0} at {1}", x.Figure, x.Position)
    member x.CanAttack (placement:Placement) =
        x.Figure.CanAttackFrom x.Position placement.Position

type Layout = Placement list

let readBoard fileName = 
    let fileText = File.ReadAllText(fileName)
    
    let oneFigureRegexExpression = @"(?<Name>King|Queen|Bishop|Rook|Knight)\:(?<Qty>\d+)"
    let globalMatch = (new Regex(@"(?<X>\d+)x(?<Y>\d+)\s+(?<Figures>(?<Figure>" +  oneFigureRegexExpression + "\s*)+)")).Match(fileText)

    let boardSize = 
        let read (name:string) = Int32.Parse(globalMatch.Groups.[name].Value) 
        { Width = read "X"; Height = read "Y" }
    let figures = 
        (new Regex(oneFigureRegexExpression)).Matches(fileText).Cast<Match>()
        |> Seq.map (fun m -> (SerializationHelpers.fromString<Figure> m.Groups.["Name"].Value, Int32.Parse(m.Groups.["Qty"].Value)))
        |> Seq.filter (fun (maybeFigure, _) -> match maybeFigure with | Some f -> true | None -> false)
        |> Seq.map (fun (maybeFigure, qty) -> (maybeFigure.Value, qty))
        |> Map.ofSeq
    
    (boardSize, figures)

exception TooManyFigures

let walkAllPairsFromStart (array:'a array) startOneSeq startOtherSeq endSeq = seq {
    for i = startOneSeq to startOtherSeq - 1 do 
        yield (array.[i], array.[startOtherSeq], startOtherSeq)
    for t = startOtherSeq+1 to endSeq do 
        for i = startOneSeq to t-1 do 
            yield (array.[i], array.[t], t)
}

let countPeacefulLayouts boardSize (population:Population) =
    let cellsCount = boardSize.Height * boardSize.Width
    let figuresCount = population |> (0 |> Map.fold (fun acc key value -> acc + value))
    let freeCellsCount = cellsCount - figuresCount
    
    if freeCellsCount < 0 then raise TooManyFigures

    let field = [|for x in [1..boardSize.Width] do for y in [1..boardSize.Height] -> { X = x; Y = y }|]

    let isPeaceful (layout:(Placement*int) array) previousIsPeaceful changedFromIndex =
        let findCollisions startOneSeq startOtherSeq endSeq = 
            walkAllPairsFromStart layout startOneSeq startOtherSeq endSeq
            |> Seq.collect (fun ((first, _), (second, _), secondIndex) -> [(first.CanAttack second, secondIndex); (second.CanAttack first, secondIndex)])
            
        let changedPairs = findCollisions 0 changedFromIndex (layout.Length - 1) 
        let unchangedPairs = findCollisions 0 0 (changedFromIndex - 1) 

        let changedPairsResult = changedPairs |> Seq.tryFind (fun (canAttack, secondIndex) -> canAttack)
        let changedPairsArePeacful = changedPairsResult.IsNone
        let unchangedPairsResult = lazy (unchangedPairs |> Seq.tryFind (fun (canAttack, secondIndex) -> canAttack))
        let unchangedPairsArePeacful = lazy unchangedPairsResult.Value.IsNone

        if previousIsPeaceful then 
            if changedPairsArePeacful then 
                (true, None)
            else
                (false, Some (snd changedPairsResult.Value))
        else
            if changedPairsArePeacful then 
                if unchangedPairsArePeacful.Value then 
                    (true, None)
                else 
                    (false, Some (snd unchangedPairsResult.Value.Value))
            else
                if unchangedPairsArePeacful.Value then 
                    (false, Some (snd changedPairsResult.Value))
                else 
                    (false, Some (min (snd changedPairsResult.Value) (snd unchangedPairsResult.Value.Value)))
        
    let layouts =
        let codeForFreeCell _ = 0
        let codeForFigure figure _ = match figure with | King -> 1 | Queen -> 2 | Bishop -> 3 | Rook -> 4 | Knight -> 5 | Empty -> 0
        let initialPermutation = 
            [| 
                yield! List.init freeCellsCount codeForFreeCell 
                yield! [for pair in population do yield! List.init pair.Value (codeForFigure pair.Key)]
            |] |> Array.sort
        let indices = [|0..initialPermutation.Length-1|]

        let figureForCode code = match code with | 1 -> Some King | 2 -> Some Queen | 3 -> Some Bishop | 4 -> Some Rook | 5 -> Some Knight | _ -> None
        let filterAndWrap wrap (pos, index, optionFig) = match optionFig with | Some fig -> [| wrap fig pos index |] | None -> [||]

        let step permutation changedFromCellIndex lastResult =
            let layout = permutation |> Array.map figureForCode 
                                     |> Array.zip3 field indices 
                                     |> Array.collect (filterAndWrap (fun f p i -> ({ Figure = f; Position = p }, i)))
            let orElse fallback opt = match opt with | Some v -> v | None -> fallback
            let changedFigureIndex = (layout |> Seq.tryFindIndex (fun (placement, cellIndex) -> cellIndex = changedFromCellIndex)) |> orElse 0
            let (peace, brokeOnFigureIndex) = isPeaceful layout lastResult changedFigureIndex
            let brokeOnCellIndex = match brokeOnFigureIndex with | Some figureIndex -> Some (snd layout.[figureIndex]) | None -> None

            let (maybeNextPermutation, nextChangedFromCellIndex) = Permutations.getNext permutation brokeOnCellIndex
            (if peace then Some layout else None), peace, maybeNextPermutation, nextChangedFromCellIndex

        let rec loop permutation changedCellIndex lastPeace = seq {
                let maybeLayout, peace, maybeNextPermutation, nextChangedFromCellIndex = step permutation changedCellIndex lastPeace
                if maybeLayout.IsSome then yield maybeLayout.Value
                match maybeNextPermutation with 
                | None -> yield! Seq.empty
                | Some next -> yield! loop next nextChangedFromCellIndex peace
            }
        loop initialPermutation 0 false

    layouts |> Seq.length        

[<EntryPoint>]
let main argv = 
    let timer = new System.Diagnostics.Stopwatch()
    try
        timer.Start()
        try 
            if argv.Length = 1 then 
                let (boardSize, population) = readBoard argv.[0]
                let layoutsNumber = countPeacefulLayouts boardSize population
                Console.WriteLine("The answer is {0}", layoutsNumber)
                0
            else
                Console.WriteLine("Usage: <exe-file-name> <input-file-name>")
                -1
        with
            | ex -> Console.WriteLine(ex); -2
    finally
        timer.Stop()
        Console.WriteLine("Timing: {0} MTicks, {1}", float timer.ElapsedTicks/1000.0, timer.Elapsed)
        ignore(Console.ReadLine())