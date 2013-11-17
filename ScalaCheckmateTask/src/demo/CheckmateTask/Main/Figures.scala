package demo.CheckmateTask.Figures

class Board(val width: Int, val height: Int) {
  val size = width * height
  lazy val items = (for (x <- 0 to width - 1; y <- 0 to height - 1) yield new Position(x, y, this)) toSet
  def contains(coords: (Int, Int)) = coords match { case (x, y) => 0 <= x && x < width && 0 <= y && y < height }
}

class Position(val x: Int, val y: Int, val board: Board) extends Equals {
  require(board contains (x, y), "Bad coordinates")

  def move(delta: (Int, Int)) = delta match {
    case (dx, dy) if board.contains(x + dx, y + dy) => Some(new Position(x + dx, y + dy, board))
    case _ => None
  }

  lazy val horizontalLine = (for (newX <- 0 to board.width - 1 if newX != x) yield new Position(newX, y, board)) toSet
  lazy val verticalLine = (for (newY <- 0 to board.height - 1 if newY != y) yield new Position(x, newY, board)) toSet
  lazy val diagonalLineOne = {
    val offset = x - y
    val xstart = Math.max(0, offset)
    val xend = Math.min(offset + board.height - 1, board.width - 1)
    (for (newX <- xstart to xend if newX != x) yield new Position(newX, newX - offset, board)) toSet
  }
  lazy val diagonalLineTwo = {
    val offset = x + y
    val xstart = Math.max(0, offset - board.height + 1)
    val xend = Math.min(offset, board.width - 1)
    (for (newX <- xstart to xend if newX != x) yield new Position(newX, offset - newX, board)) toSet
  }

  lazy val neighbours = {
    val (up, down) = (if (y == 0) 0 else -1, if (y == board.height - 1) 0 else 1)
    val (left, right) = (if (x == 0) 0 else -1, if (x == board.width - 1) 0 else 1)

    (for (dx <- left to right; dy <- up to down; val p = new Position(x + dx, y + dy, board) if p != this) yield p) toSet
  }

  override def toString = "(" + x + "," + y + ")"

  def canEqual(other: Any) = other.isInstanceOf[Position]
  override def equals(other: Any) = other match {
    case that: Position => that.canEqual(Position.this) && x == that.x && y == that.y && board == that.board
    case _ => false
  }
  override def hashCode() = { val prime = 41; prime * (prime * (prime + x.hashCode) + y.hashCode) + board.hashCode }
}

// pretty nice chess domain explained 
trait Figure {
  def canReachFrom(point: Position): Set[Position]
  override def equals(that: Any) = this.getClass == that.getClass
  override def hashCode = this.getClass.hashCode
}
case class King extends Figure { // 'king can reach its neighbours', well still too many words and signs all around, but Scala is quite expressive (and impressive :) )  
  override def canReachFrom(point: Position) = point neighbours
}
case class Queen extends Figure {
  override def canReachFrom(point: Position) = point.verticalLine ++ point.horizontalLine ++ point.diagonalLineOne ++ point.diagonalLineTwo
}
case class Bishop extends Figure {
  override def canReachFrom(point: Position) = point.diagonalLineOne ++ point.diagonalLineTwo
}
case class Rook extends Figure {
  override def canReachFrom(point: Position) = point.horizontalLine ++ point.verticalLine
}
case class Knight extends Figure {
  override def canReachFrom(point: Position) = {
    val knightsMagic = List(-2, -1, 1, 2) 
    (for {
      dx <- knightsMagic 
      dy <- knightsMagic
      if Math.abs(dx) != Math.abs(dy)
      val maybePoint = point move (dx, dy)
      if maybePoint.nonEmpty
    } yield maybePoint.get) toSet
  }
}
