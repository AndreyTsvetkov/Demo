package demo.CheckmateTask.Main
import demo.CheckmateTask.Figures._
import demo.CheckmateTask.DownWalker

object temp {
  val b = new Board(3,3)                          //> b  : demo.CheckmateTask.Figures.Board = demo.CheckmateTask.Figures.Board@3c1
                                                  //| 96fa2
  King() canReachFrom new Position(0,0,b)         //> res0: scala.collection.immutable.Set[demo.CheckmateTask.Figures.Position] = 
                                                  //| Set((0,1), (1,0), (1,1))
  //Brain.factorialMemoized(54)/Brain.factorialMemoized(48)/2
  BigInt(54l)*51*39*33*39*7                       //> res1: scala.math.BigInt = 967620654
  BigInt(54l)*34*21*16*18*15/2                    //> res2: scala.math.BigInt = 83280960
      
  DownWalker.goodLayoutQuantity(b, Map[Figure, Int](King() -> 2, Rook() -> 1))
                                                  //> More top level done: 0
                                                  //| More top level done: 0
                                                  //| More top level done: 2
                                                  //| More top level done: 2
                                                  //| More top level done: 2
                                                  //| More top level done: 2
                                                  //| More top level done: 0
                                                  //| More top level done: 0
                                                  //| More top level done: 0
                                                  //| res3: scala.math.BigInt = 4
  DownWalker.goodLayoutQuantity(new Board(4,4), Map[Figure, Int](Knight() -> 4, Rook() -> 2))
                                                  //> More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| More top level done: 24
                                                  //| res4: scala.math.BigInt = 8

}