type BoardSize = { Width: int; Height: int}
type Figure = 
    | King
    | Queen
    | Bishop
    | Rook
    | Knight
    override x.ToString() = 
        match x with | King -> "King" | Queen -> "Queen" | Bishop -> "Bishop" | Rook -> "Rook" | Knight -> "Knight"

type Population = Map<Figure, int>

let readBoard fileName = ({Width=2; Height=2}, Map.ofSeq [ (King, 1); (Queen, 2) ]) : (BoardSize * Population)

let countPeacefulLayouts boardSize population =
    let produceDistinctLayouts boardSize (population:Population) = 
        let field = [for x in [1..boardSize.Width] do for y in [1..boardSize.Height] -> (x, y)] |> Seq.toArray
        seq {
            for item in population do 
                for point in field do 
                    let _ = (System.Console.WriteLine("{0} {1}", item.Key.ToString(), point.ToString()))
                    yield (item.Key, point)
        }
    let isPeaceful l = true

    produceDistinctLayouts boardSize population
        |> Seq.where isPeaceful
        |> Seq.length        

[<EntryPoint>]
let main argv = 
    let (boardSize, population) = readBoard "input.txt"
    let layoutsNumber = countPeacefulLayouts boardSize population
    printfn "The answer is %i" layoutsNumber
    ignore(System.Console.ReadLine())
    0


