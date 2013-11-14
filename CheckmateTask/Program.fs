module CheckmateTask.Main

open System
open System.Text.RegularExpressions
open System.IO
open System.Linq

type BoardSize = { Width: int; Height: int}
type Position = { X: int; Y: int } with 
    override x.ToString() = String.Format("{0},{1}", x.X, x.Y)
    member x.IsNeighbourTo (other:Position) = 
        let (deltaX, deltaY) = x.GetAbsoluteDeltaTo other
        deltaX <= 1 && deltaY <= 1
    member x.IsSameStraightLineWith (other:Position) = x.X = other.X || x.Y = other.Y 
    member x.IsSameDiagonalLineWith (other:Position) =
        let (deltaX, deltaY) = x.GetAbsoluteDeltaTo other
        deltaX = deltaY
    member internal x.GetAbsoluteDeltaTo (other:Position) = (Math.Abs (x.X - other.X), Math.Abs (x.Y - other.Y))

type Figure = 
    | King
    | Queen
    | Bishop
    | Rook
    | Knight
    member x.CanAttackFrom (my:Position) (target:Position) = 
        if my = target then false 
        else
            match x with 
            | King -> my.IsNeighbourTo target
            | Queen -> my.IsSameStraightLineWith target || my.IsSameDiagonalLineWith target
            | Bishop -> my.IsSameDiagonalLineWith target
            | Rook -> my.IsSameStraightLineWith target
            | Knight ->
                let absoluteDelta = my.GetAbsoluteDeltaTo target
                absoluteDelta = (2, 1) || absoluteDelta = (1, 2)
    override x.ToString() = SerializationHelpers.toString x
    static member fromString s = SerializationHelpers.fromString<Figure> s

and Population = Map<Figure, int>

and Placement = { Figure: Figure; Position: Position } with 
    override x.ToString() = String.Format("{0} at {1}", x.Figure, x.Position)
    member x.CanAttack (placement:Placement) =
        x.Figure.CanAttackFrom x.Position placement.Position
    member this.IsSafeWith (others:Layout) = 
        Seq.forall (fun other -> not (this.CanAttack other) && not (other.CanAttack this)) others

and Layout = Placement list

let isFree position layout = Seq.forall (fun layoutPlacement -> layoutPlacement.Position <> position) layout

let readBoard fileName = 
    let fileText = File.ReadAllText(fileName)
    
    let oneFigureRegexExpression = @"(?<Name>King|Queen|Bishop|Rook|Knight)\:(?<Qty>\d+)"
    let globalMatch = (new Regex(@"(?<X>\d+)x(?<Y>\d+)\s+(?<Figures>(?<Figure>" +  oneFigureRegexExpression + "\s*)+)")).Match(fileText)

    let boardSize = 
        let read (name:string) = Int32.Parse(globalMatch.Groups.[name].Value) 
        { Width = read "X"; Height = read "Y" }
    let figures = 
        (new Regex(oneFigureRegexExpression)).Matches(fileText).Cast<Match>()
        |> Seq.map (fun m -> (Figure.fromString m.Groups.["Name"].Value, Int32.Parse(m.Groups.["Qty"].Value)))
        |> Seq.filter (fun (maybeFigure, _) -> match maybeFigure with | Some f -> true | None -> false)
        |> Seq.map (fun (maybeFigure, qty) -> (maybeFigure.Value, qty))
        |> Map.ofSeq
    
    (boardSize, figures)

exception TooManyFigures

let countPeacefulLayouts boardSize (population:Population) =
    let cellsCount = boardSize.Height * boardSize.Width
    let figuresCount = population |> (0 |> Map.fold (fun acc key value -> acc + value))
    let freeCellsCount = cellsCount - figuresCount
    
    if freeCellsCount < 0 then raise TooManyFigures

    let field = [for x in [1..boardSize.Width] do for y in [1..boardSize.Height] -> { X = x; Y = y }]

    let isPeaceful (layout:Layout) = 
        seq { for placement1 in layout do for placement2 in layout -> placement1.CanAttack placement2}
        |> Seq.forall (fun canAttack -> canAttack = false)
        
    let layouts =
        let codeForFreeCell _ = 0
        let codeForFigure figure _ = match figure with | King -> 1 | Queen -> 2 | Bishop -> 3 | Rook -> 4 | Knight -> 5
        let permutationList = [ 
            yield! List.init freeCellsCount codeForFreeCell 
            yield! [for pair in population do yield! List.init pair.Value (codeForFigure pair.Key)]
        ]

        let figureForCode code : Figure option = match code with | 1 -> Some King | 2 -> Some Queen | 3 -> Some Bishop | 4 -> Some Rook | 5 -> Some Knight | _ -> None
        seq {
            for layout in Permutations.generatePermutations permutationList 
                |> Seq.map (List.map figureForCode >> List.zip field >> List.collect (fun (p, f) -> if f.IsSome then [{ Figure = f.Value; Position = p }] else [])) do 
                if isPeaceful layout then 
                    yield layout
        }

    layouts
        |> Seq.map (fun layout -> ignore(Console.WriteLine(layout)); layout)
        |> Seq.length        

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