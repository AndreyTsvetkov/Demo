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
    override x.ToString() = SerializationHelpers.toString x
    static member fromString s = SerializationHelpers.fromString<Figure> s
    member x.CanAttackFrom (my:Position) (target:Position) = 
        match x with 
        | King -> my.IsNeighbourTo target
        | Queen -> my.IsSameStraightLineWith target || my.IsSameDiagonalLineWith target
        | Bishop -> my.IsSameDiagonalLineWith target
        | Rook -> my.IsSameStraightLineWith target
        | Knight ->
            let absoluteDelta = my.GetAbsoluteDeltaTo target
            absoluteDelta = (2, 1) || absoluteDelta = (1, 2)

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

let countPeacefulLayouts boardSize (population:Population) =
    let field = [for x in [1..boardSize.Width] do for y in [1..boardSize.Height] -> { X = x; Y = y }]
    let flattenedPopulation = [for pair in population do yield! Seq.init pair.Value (fun _ -> pair.Key)]
    let placements = [for figure in flattenedPopulation do yield (figure, [for position in field -> { Figure = figure; Position = position }])]
    
    let layouts =
        let branchLayoutsForFigure (initialLayouts: Layout list) (oneMoreFigurePlacements: Figure * Placement list) = [
            for layout in initialLayouts do
                for placement in snd oneMoreFigurePlacements do
                    if isFree placement.Position layout && placement.IsSafeWith layout then 
                        yield placement :: layout
        ]

        let initialLayouts = [for placement in snd (placements |> Seq.nth 0) -> [placement]]
        Seq.fold branchLayoutsForFigure initialLayouts (placements |> Seq.skip 1)
            |> List.map (fun layout -> 
                layout |> List.sortBy (fun placement -> placement.Figure, placement.Position.X, placement.Position.Y))
                               

    let equal l1 l2 = Seq.compareWith (fun p1 p2 -> if p1 = p2 then 0 else -1) l1 l2 = 0
    Seq.fold (fun agg item -> if not (Seq.exists (equal item) agg) then item :: agg else agg) [] layouts
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