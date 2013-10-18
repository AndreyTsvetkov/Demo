using System;
using Util;

namespace Asc2Pnt.Model
{
	//[Pure]
	/// <summary>
	/// ������������ "�����" ������� (�.�. �������, � ����� ������� �� ���� ������ �������). 
	/// ������������ ����� ���� �����: ������� ����� ������� � �������� ��� ����� ������� (������������). 
	/// � ����������� ������ ������� �� ����� �����, ������������ ����� ����� � ����� ������. 
	/// � ������ ������� �� ���� �����, �������� ����� ������ ����� ��������� � ����������� ������ �������, � ��������
	/// </summary>
	public class DiscretePointSequenceEnd : IEquatable<DiscretePointSequenceEnd>
	{
		/// <summary>
		/// ������� ����������� ����� �� ����� �����
		/// </summary>
		/// <param name="currentPoint"></param>
		public DiscretePointSequenceEnd(DiscretePoint currentPoint)
		{
			CurrentPoint = currentPoint;
		}
		/// <summary>
		/// ������� ����������� ������������ ����� �� �������� � ���������� �����
		/// </summary>
		/// <param name="currentPoint"></param>
		/// <param name="previousPoint"></param>
		public DiscretePointSequenceEnd(DiscretePoint currentPoint, DiscretePoint previousPoint)
		{
			CurrentPoint = currentPoint;
			_previousPoint = previousPoint.ToMaybe();
		}

		/// <summary>
		/// ����� �� ������ ����� ��������� � ���� ��� �����
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool CanAttach(DiscretePoint point)
		{
			// ���� ��� �������� � �������
			return CurrentPoint.IsNeighbourWith(point) &&  // � �� ��������� � ����������� (���� ��� ����)
				_previousPoint.Select(pp => pp != point).OrElse(true);
		}

		/// <summary>
		/// ���������� �� ��� ����� ����������� �����
		/// </summary>
		/// <param name="newPoint"></param>
		/// <returns></returns>
		public bool InSameLineAs(DiscretePoint newPoint)
		{
			return _previousPoint.Select(pp => pp.InSameLineAs(CurrentPoint, newPoint))
			                     .OrElse(CurrentPoint.InSameLineAs(newPoint, newPoint));
		}

		/// <summary>
		/// �������� ����� ���� ������. ��� ���������� ��������, ������ �������� ���������� ����������
		/// </summary>
		/// <param name="newPoint"></param>
		/// <returns></returns>
		public DiscretePointSequenceEnd ShiftTo(DiscretePoint newPoint)
		{
			return new DiscretePointSequenceEnd(newPoint, CurrentPoint);
		}
		/// <summary>
		/// �������������� ����� �� ���� ����� (������ �� ����������, �� ����� �������� �����)
		/// </summary>
		/// <param name="newPoint"></param>
		/// <returns></returns>
		public DiscretePointSequenceEnd MakePointingFrom(DiscretePoint newPoint)
		{
			return new DiscretePointSequenceEnd(CurrentPoint, newPoint);
		}

		/// <summary>
		/// �������� �� ��� ����� ����
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool MakesTurnWith(DiscretePointSequenceEnd other)
		{
			return other._previousPoint.Select(opp => !InSameLineAs(opp)).OrElse(false);
		}

		public readonly DiscretePoint CurrentPoint;

		private readonly Maybe<DiscretePoint> _previousPoint;

		#region Equals
		public override string ToString()
		{
			return string.Format("Now {0}, was {1}", CurrentPoint, _previousPoint);
		}

		public bool Equals(DiscretePointSequenceEnd other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return CurrentPoint.Equals(other.CurrentPoint);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((DiscretePointSequenceEnd)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (CurrentPoint.GetHashCode() * 397) ^ _previousPoint.GetHashCode();
			}
		}

		public static bool operator ==(DiscretePointSequenceEnd left, DiscretePointSequenceEnd right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(DiscretePointSequenceEnd left, DiscretePointSequenceEnd right)
		{
			return !Equals(left, right);
		}
		#endregion
	}
}