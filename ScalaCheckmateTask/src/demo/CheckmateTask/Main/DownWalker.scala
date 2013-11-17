package demo.CheckmateTask

import demo.CheckmateTask.Figures._
import demo.CheckmateTask.CallableImplicits._

object DownWalker {
  def goodLayoutQuantity(board: Board, figures: Map[Figure, Int]) = {
    val flatFigures = figures |> flatten
    traverse(board.items, flatFigures, Set()) |> makeDistinct(flatFigures size, (figures |> toCounts))
  }
  private def flatten[T](m: Map[T, Int]) = m flatMap {case (f, qty) => 1 to qty map (_ => f)} toList
  private def toCounts[T](m: Map[T, Int]) = m map {case (_, qty) => qty} toList

  // counts the good layouts; DOES make difference for pieces of SAME kind, e.g. King1, King2 != King2, King1 for this method 
  private def traverse(free: Set[Position], figuresToPlace: List[Figure], occupied: Set[Position]): BigInt = figuresToPlace match {
    case Nil => 1
    case lastFigure :: Nil => goodPlaces(free, occupied, lastFigure) size 
    case figure :: restOfFigures =>
      val accessibleForFigure = goodPlaces(free, occupied, figure) toList
      val counts = (for (point <- accessibleForFigure) 
        yield traverse(free - point -- figureAttackZone(figure, point), restOfFigures, occupied + point))
      (counts sum) |> dumpIf("More top level done: ", _ => occupied.size == 1)
  }
  // those free & not attacked places, from which out piece cannot attack anyone
  private def goodPlaces(free: Set[Position], occupied: Set[Position], figure: Figure) = free filter {figureAttackZone(figure, _) & occupied isEmpty}
  // we've got plenty of repeating calls to know 'where does the piece reach from this point'. it deserves memoization 
  private val figureAttackZone = Memoize2((f: Figure, p: Position) => f canReachFrom p)

  // applies fix to make the count NOT to make difference for all pieces of the same king; after its application: King1, King2 == King2, King1
  private def makeDistinct(freeSize: Int, figureCounts: Seq[Int])(count: BigInt) = {
    val remainingFreeCells = freeSize - (figureCounts sum)
    val figureCountsWithEmptyCellFigure = remainingFreeCells :: figureCounts.toList
    count / (figureCountsWithEmptyCellFigure map { factorial(_) } reduceLeft { _ * _ })
  }
  
  private def factorial_(n: Int) = (1 to n map { BigInt(_) }).foldLeft(BigInt(1)) { _ * _ }
  private val factorial = Memoize1(factorial_)

  private def dumpIf[T](msg: String, pred: (T => Boolean))(arg: T) = { if (pred(arg)) println(msg  + arg); arg }
  
  def countDistinctLayouts(freeSize: Int, figureCounts: Seq[Int]) = {
    factorial(freeSize) |> makeDistinct(freeSize, figureCounts)
  }
}