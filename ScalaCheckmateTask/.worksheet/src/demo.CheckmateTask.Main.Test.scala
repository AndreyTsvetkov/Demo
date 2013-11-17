package demo.CheckmateTask.Main
import demo.CheckmateTask.Figures._

object Test {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(110); val res$0 = 
  9297779400l/1000/60/60/24;System.out.println("""res0: Long = """ + $show(res$0));$skip(137); val res$1 = 
     
  Brain.goodLayoutQuantity(new Board(6,9), Map[Figure, Int](King() -> 2, Queen() -> 1, Bishop() -> 1, Rook() -> 1, Knight() -> 1));System.out.println("""res1: scala.math.BigInt = """ + $show(res$1));$skip(144); val res$2 = 
 
   
  Brain.countTotalDistinctLayouts(new Board(6,9), Map[Figure, Int](King() -> 2, Queen() -> 1, Bishop() -> 1, Rook() -> 1, Knight() -> 1));System.out.println("""res2: scala.math.BigInt = """ + $show(res$2));$skip(90); val res$3 = 
  
  Brain.goodLayoutQuantity(new Board(3,3), Map[Figure, Int](King() -> 2, Rook() -> 1));System.out.println("""res3: scala.math.BigInt = """ + $show(res$3));$skip(84); val res$4 = 
  Brain.countBadLayouts(new Board(3,3), Map[Figure, Int](King() -> 2, Rook() -> 1));System.out.println("""res4: scala.math.BigInt = """ + $show(res$4));$skip(101); val res$5 = 
  
   
  Brain.countTotalDistinctLayouts(new Board(3,3), Map[Figure, Int](King() -> 2, Rook() -> 1));System.out.println("""res5: scala.math.BigInt = """ + $show(res$5));$skip(125); val res$6 = 
                                                  
  Brain.goodLayoutQuantity(new Board(2,2), Map[Figure, Int](Rook() -> 2));System.out.println("""res6: scala.math.BigInt = """ + $show(res$6))}
     
                                                  
}
