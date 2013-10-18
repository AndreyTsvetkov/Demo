using System;

//using System.Diagnostics.Contracts;

namespace Asc2Pnt.Model
{
//	[Pure]
	public class DiscretePoint : IEquatable<DiscretePoint>
	{
		public DiscretePoint(int x, int y) { X = x; Y = y; }
		public readonly int X; 
		public readonly int Y;

		/// <summary>
		/// Являются ли соседними точками по одному из четырех направлений (верх, низ, право, лево)
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool IsNeighbourWith(DiscretePoint point)
		{
			var sameX = point.X == X;
			var sameY = point.Y == Y;

			if (!sameX && !sameY)
				return false;

			var mustBeNear = sameX ? new { One = Y, Another = point.Y } : new { One = X, Another = point.X };
			return Math.Abs(mustBeNear.One - mustBeNear.Another) == 1;
		}
		/// <summary>
		/// Находятся ли все три точки на одной вертикальной или горизонтальной прямой
		/// </summary>
		/// <param name="one"></param>
		/// <param name="two"></param>
		/// <returns></returns>
		public bool InSameLineAs(DiscretePoint one, DiscretePoint two)
		{
			return X == one.X && X == two.X ||
				   Y == one.Y && Y == two.Y;
		}

		public DiscretePoint MoveY(int dy)
		{
			return Move(0, dy);
		}
		public DiscretePoint MoveX(int dx)
		{
			return Move(dx, 0);
		}
		public DiscretePoint Move(int dx, int dy)
		{
			return new DiscretePoint(X + dx, Y + dy);
		}

		#region Equals & ToString
		public bool Equals(DiscretePoint other)
		{
			return X == other.X && Y == other.Y;
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is DiscretePoint && Equals((DiscretePoint)obj);
		}
		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}
		public static bool operator ==(DiscretePoint left, DiscretePoint right)
		{
			return !ReferenceEquals(left, null) && left.Equals(right);
		}
		public static bool operator !=(DiscretePoint left, DiscretePoint right)
		{
			return !ReferenceEquals(left, null) && !left.Equals(right);
		}

		public override string ToString()
		{
			return string.Format("X={0},Y={1}", X, Y);
		}
		#endregion
	}
}