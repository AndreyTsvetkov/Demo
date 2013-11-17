package demo.CheckmateTask

// add F#-style 'apply argument first' operator
class Callable[T](private val o: T) { def |> [TR](f: Function[T, TR]) = f(o) }

object CallableImplicits {
  implicit def objToCallable[T](o: T): Callable[T] = new Callable(o)
}

// standart idiom for closeable resources
object Using {
  def using[Closeable <: { def close(): Unit }, B](closeable: Closeable)(getB: Closeable => B): B =
    try {
      getB(closeable)
    } finally {
      closeable.close()
    }
}
 
// memoization rocks
class Memoize1[-T, +R](f: T => R) extends (T => R) {
  import scala.collection.mutable
  private[this] val vals = mutable.Map.empty[T, R]
 
  def apply(x: T): R = {
    if (vals.contains(x)) {
      vals(x)
    }
    else {
      val y = f(x)
      vals += ((x, y))
      y
    }
  }
} 
object Memoize1 {
  def apply[T, R](f: T => R) = new Memoize1(f)
}

class Memoize2[-T1, -T2, +R](f: (T1, T2) => R) extends ((T1, T2) => R) {
  import scala.collection.mutable
  private[this] val vals = mutable.Map.empty[(T1, T2), R]
 
  def apply(x1: T1, x2: T2): R = {
    if (vals.contains((x1, x2))) {
      vals((x1, x2))
    }
    else {
      val y = f(x1, x2)
      vals += (((x1, x2), y))
      y
    }
  }
}
object Memoize2 {
  def apply[T1, T2, R](f: (T1, T2) => R) = new Memoize2(f)
}