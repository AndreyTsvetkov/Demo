package demo.CheckmateTask.Main

import java.security.InvalidParameterException
import java.util.regex.Matcher
import java.util.regex.Pattern
import demo.CheckmateTask.CallableImplicits._
import demo.CheckmateTask.Using._
import demo.CheckmateTask.Figures._
import demo.CheckmateTask.DownWalker

object Main extends App {
  val (boardSize, figures) = readFile("input.txt") |> load
  
  println("The answer is: " + DownWalker.goodLayoutQuantity(boardSize, figures))
  
  def load(fileText: String) = {
    val oneFigureRegexExpression = """(?<Name>King|Queen|Bishop|Rook|Knight)\:(?<Qty>\d+)"""
    val globalMatch = (Pattern.compile("""(?<X>\d+)x(?<Y>\d+)\s+(?<Figures>(?<Figure>""" + oneFigureRegexExpression + """\s*)+)""")).matcher(fileText)
    if (!globalMatch.find()) throw new InvalidParameterException("input.txt contains bad content")

    val boardSize = {
      def read(name: String) = Integer.parseInt(globalMatch.group(name))
      new Board(read("X"), read("Y"))
    }

    val figures = {
      def allMatches(m: Matcher)(keys: Set[String]): List[Map[String, String]] = {
        if (m.find()) {
          val values = (keys map (x => (x, m group x))).toMap
          values :: allMatches(m)(keys)
        } else Nil
      }
      def toFigure(s: String): Option[Figure] = s match {
        case "King" => Some(King())
        case "Queen" => Some(Queen())
        case "Bishop" => Some(Bishop())
        case "Rook" => Some(Rook())
        case "Knight" => Some(Knight())
        case _ => None
      }

      ((Pattern.compile(oneFigureRegexExpression).matcher(fileText) |> allMatches)(Set("Name", "Qty"))
        map (m => { m("Name") |> toFigure match { case Some(f) => Some(f, Integer.parseInt(m("Qty"))) case None => None } })
        flatMap (opt => opt match { case Some(pair) => List(pair) case None => Nil })).toMap
    }

    (boardSize, figures)
  }

  def readFile(name: String) = {
    using(io.Source.fromFile(name)) { 
      src => src.getLines() mkString "\n"
    }
  }
}