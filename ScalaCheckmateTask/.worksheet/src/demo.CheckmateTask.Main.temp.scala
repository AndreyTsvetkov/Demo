package demo.CheckmateTask.Main
import demo.CheckmateTask.Figures._
import demo.CheckmateTask.DownWalker

object temp {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(144); 
  val b = new Board(3,3);System.out.println("""b  : demo.CheckmateTask.Figures.Board = """ + $show(b ));$skip(42); val res$0 = 
  King() canReachFrom new Position(0,0,b);System.out.println("""res0: scala.collection.immutable.Set[demo.CheckmateTask.Figures.Position] = """ + $show(res$0));$skip(90); val res$1 = 
  //Brain.factorialMemoized(54)/Brain.factorialMemoized(48)/2
  BigInt(54l)*51*39*33*39*7;System.out.println("""res1: scala.math.BigInt = """ + $show(res$1));$skip(31); val res$2 = 
  BigInt(54l)*34*21*16*18*15/2;System.out.println("""res2: scala.math.BigInt = """ + $show(res$2));$skip(86); val res$3 = 
      
  DownWalker.goodLayoutQuantity(b, Map[Figure, Int](King() -> 2, Rook() -> 1));System.out.println("""res3: scala.math.BigInt = """ + $show(res$3));$skip(94); val res$4 = 
  DownWalker.goodLayoutQuantity(new Board(4,4), Map[Figure, Int](Knight() -> 4, Rook() -> 2));System.out.println("""res4: scala.math.BigInt = """ + $show(res$4))}

}
