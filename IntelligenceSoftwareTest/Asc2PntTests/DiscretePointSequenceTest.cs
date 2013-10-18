// ReSharper disable RedundantAssignment

using System;
using System.Linq;
using Asc2Pnt.Model;
using NUnit.Framework;
namespace Asc2PntTests
{
	namespace DiscretePointSequenceTest
	{
		[TestFixture]
		public class CanBeExtendedByTest
		{
			public readonly static DiscretePoint Start = new DiscretePoint(10, 10);

			[Test]
			[TestCase(0, 1, Result = true)]
			[TestCase(1, 0, Result = true)]
			[TestCase(0, -1, Result = true)]
			[TestCase(-1, 0, Result = true)]
			[TestCase(0, 0, Result = false)]
			[TestCase(1, 1, Result = false)]
			[TestCase(-2, -3, Result = false)]
			public bool Given_SinglePointSequence_Then_CanBeExtendedBy4Directions(int newDeltaX, int newDeltaY)
			{
				var sut = new DiscretePointSequence(Start);

				return sut.CanBeExtendedBy(Start.Move(newDeltaX, newDeltaY));
			}

			[Test]
			[TestCase(-1, 0, Result = true)]
			[TestCase(0, -1, Result = true)]
			[TestCase(1, 0, Result = true)]
			[TestCase(-1, 1, Result = true)]
			[TestCase(0, 2, Result = true)]
			[TestCase(1, 1, Result = true)]
			[TestCase(0, 0, Result = false)]
			[TestCase(0, 1, Result = false)]
			[TestCase(1, 2, Result = false)]
			public bool Given_TwoPointSequence_Then_CanBeExtendedBy6Directions(int newDeltaX, int newDeltaY)
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveY(+1));

				return sut.CanBeExtendedBy(Start.Move(newDeltaX, newDeltaY));
			}

			[Test]
			[TestCase(-1, 0, Result = false)]
			[TestCase(0, -1, Result = false)]
			[TestCase(1, 0, Result = false)]
			[TestCase(-1, 1, Result = false)]
			[TestCase(0, 2, Result = false)]
			[TestCase(1, 2, Result = false)]
			[TestCase(2, 1, Result = false)]
			[TestCase(2, 0, Result = false)]
			[TestCase(1, -1, Result = false)]
			public bool Given_ClosedSequence_Then_CanNotBeExtended(int newDeltaX, int newDeltaY)
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveY(+1));
				sut = sut.ExtendBy(Start.Move(+1, +1));
				sut = sut.ExtendBy(Start.MoveX(+1));

				return sut.CanBeExtendedBy(Start.Move(newDeltaX, newDeltaY));
			}
		}

		[TestFixture]
		public class ExtendByTest
		{
			public readonly static DiscretePoint Start = new DiscretePoint(10, 10);

			[Test]
			[ExpectedException(typeof (InvalidOperationException))]
			public void Given_ClosedSequnce_When_Extending_Then_Fail()
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveY(+1));
				sut = sut.ExtendBy(Start.Move(+1, +1));
				sut = sut.ExtendBy(Start.MoveX(+1));

				sut = sut.ExtendBy(Start.MoveY(-1));
			}

			[Test]
			[ExpectedException(typeof(InvalidOperationException))]
			public void Given_TwoNonNeighbourSequences_When_Extending_Then_Fail()
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveX(+1));
				sut = sut.ExtendBy(Start.MoveX(+2));

				var other = new DiscretePointSequence(Start.MoveX(+10));
				other = other.ExtendBy(Start.MoveX(+9));
				other = other.ExtendBy(Start.MoveX(+8));

				sut = sut.ExtendBy(other);
			}

			[Test]
			public void Given_TwoLongNeighbourSequences_When_Extending_Then_Ok()
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveX(+1));
				sut = sut.ExtendBy(Start.MoveX(+2));

				var other = new DiscretePointSequence(Start.MoveX(+4));
				other = other.ExtendBy(Start.MoveX(+3));
				other = other.ExtendBy(Start.MoveX(+2));

				sut = sut.ExtendBy(other);

				Assert.IsTrue(sut.CanBeExtendedBy(Start.MoveX(-1)));
				Assert.IsTrue(sut.CanBeExtendedBy(Start.MoveX(+5)));
			}

			[Test]
			public void Given_LongAndPointNeighbourSequences_When_Extending_Then_Ok()
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveX(+1));
				sut = sut.ExtendBy(Start.MoveX(+2));

				var other = new DiscretePointSequence(Start.MoveX(+2));

				sut = sut.ExtendBy(other);

				Assert.IsTrue(sut.CanBeExtendedBy(Start.MoveX(-1)));
				Assert.IsTrue(sut.CanBeExtendedBy(Start.MoveX(+3)));
			}

			[Test]
			public void Given_TwoHalvesOfClosedFigure_When_Extending_Then_Ok()
			{
				var sut = new DiscretePointSequence(Start);
				sut = sut.ExtendBy(Start.MoveX(+1));
				sut = sut.ExtendBy(Start.Move(+1, +1));

				var other = new DiscretePointSequence(Start);
				other = other.ExtendBy(Start.MoveY(+1));
				other = other.ExtendBy(Start.Move(+1, +1));

				sut = sut.ExtendBy(other);

				Assert.IsFalse(sut.CanBeExtendedBy(Start.MoveX(-1)));
				Assert.IsFalse(sut.CanBeExtendedBy(Start.MoveX(+2)));
			}
		}

		[TestFixture]
		public class TurnsTest
		{
			public readonly static DiscretePoint Start = new DiscretePoint(10, 10);

			[Test]
			public void GivenSequence_WhenExtendingByPoints_DetectsTurns()
			{
				var cursor = Start;
				var sut = new DiscretePointSequence(cursor);
				Assert.AreEqual(0, sut.Turns.Count());

				sut = sut.ExtendBy(cursor = cursor.MoveX(+1));
				Assert.AreEqual(0, sut.Turns.Count());

				sut = sut.ExtendBy(cursor = cursor.MoveX(+1));
				Assert.AreEqual(0, sut.Turns.Count());

				sut = sut.ExtendBy(cursor = cursor.MoveY(+1));
				Assert.AreEqual(1, sut.Turns.Count());
				Assert.AreEqual("X=12,Y=10", sut.Turns.Last().ToString());

				sut = sut.ExtendBy(cursor = cursor.MoveY(+1));
				Assert.AreEqual(1, sut.Turns.Count());

				sut = sut.ExtendBy(cursor = cursor.MoveX(-1));
				Assert.AreEqual(2, sut.Turns.Count());
				Assert.AreEqual("X=12,Y=12", sut.Turns.Last().ToString());

				sut = sut.ExtendBy(cursor = cursor.MoveX(-1));
				Assert.AreEqual(2, sut.Turns.Count());

				sut = sut.ExtendBy(cursor = cursor.MoveY(-1));
				Assert.AreEqual(4, sut.Turns.Count());
				Assert.AreEqual("X=10,Y=12", sut.Turns.Take(3).Last().ToString());
				Assert.AreEqual("X=10,Y=10", sut.Turns.Last().ToString());
			}

			[Test]
			public void GivenSequence_WhenExtendingBySequences_DetectsTurns()
			{
				/*
				 *  11122
				 *  4   2
				 *  4   222
				 *	444   3
				 *    4  33
				 *    4444     
				 */
				var cursor = Start;
				var s1 = new DiscretePointSequence(cursor);
				s1 = s1.ExtendBy(cursor = cursor.MoveX(+1));
				s1 = s1.ExtendBy(cursor = cursor.MoveX(+1));
				var s2 = new DiscretePointSequence(cursor);
				s2 = s2.ExtendBy(cursor = cursor.MoveX(+1));
				s2 = s2.ExtendBy(cursor = cursor.MoveX(+1));
				s2 = s2.ExtendBy(cursor = cursor.MoveY(+1));
				s2 = s2.ExtendBy(cursor = cursor.MoveY(+1));
				s2 = s2.ExtendBy(cursor = cursor.MoveX(+1));
				s2 = s2.ExtendBy(cursor = cursor.MoveX(+1));
				var s3 = new DiscretePointSequence(cursor);
				s3 = s3.ExtendBy(cursor = cursor.MoveY(+1));
				s3 = s3.ExtendBy(cursor = cursor.MoveY(+1));
				s3 = s3.ExtendBy(cursor = cursor.MoveX(-1));
				var s4 = new DiscretePointSequence(cursor);
				s4 = s4.ExtendBy(cursor = cursor.MoveY(+1));
				s4 = s4.ExtendBy(cursor = cursor.MoveX(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveX(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveX(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveY(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveY(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveX(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveX(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveY(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveY(-1));
				s4 = s4.ExtendBy(cursor = cursor.MoveY(-1));

				Assert.AreEqual(0, s1.Turns.Count());
				s1 = s1.ExtendBy(s2);
				Assert.AreEqual(2, s1.Turns.Count());
				s1 = s1.ExtendBy(s3);
				Assert.AreEqual(4, s1.Turns.Count());
				Assert.AreEqual("X=16,Y=12", s1.Turns.Take(3).Last().ToString());
				s1 = s1.ExtendBy(s4);
				Assert.AreEqual(10, s1.Turns.Count());
			}
		}
	}
}
