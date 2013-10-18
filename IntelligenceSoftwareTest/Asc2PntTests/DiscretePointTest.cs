using Asc2Pnt;
using Asc2Pnt.Model;
using NUnit.Framework;

namespace Asc2PntTests
{
	[TestFixture]
	public class DiscretePointTest
	{
		[Test]
		[TestCase(10, 10, 11, 10, Description = "Left", Result = true)]
		[TestCase(10, 10, 10, 11, Description = "Top", Result = true)]
		[TestCase(10, 10, 10, 09, Description = "Bottom", Result = true)]
		[TestCase(10, 10, 09, 10, Description = "Right", Result = true)] 

		[TestCase(10, 10, 10, 10, Description = "Same", Result = false)] 
		[TestCase(10, 10, 11, 11, Description = "Diagonal", Result = false)] 
		[TestCase(10, 10, 12, 11, Description = "Far", Result = false)]
		[TestCase(10, 10, 12, 13, Description = "Very far", Result = false)]
		
		public bool IsNeighbourWith_Test(int x1, int y1, int x2, int y2)
		{
			return new DiscretePoint(x1, y1).IsNeighbourWith(new DiscretePoint(x2, y2));
		}

		[Test]
		[TestCase(10, 11, 10, 13, 10, 15, Description = "All in line", Result = true)]
		[TestCase(11, 10, 13, 10, 15, 10, Description = "All in line", Result = true)]
		[TestCase(11, 10, 13, 10, 13, 13, Description = "Two in line", Result = false)]
		[TestCase(11, 15, 13, 10, 14, 13, Description = "Chaotic", Result = false)]
		public bool InSameLineAs_Test(int x1, int y1, int x2, int y2, int x3, int y3)
		{
			return new DiscretePoint(x1, y1).InSameLineAs(new DiscretePoint(x2, y2), new DiscretePoint(x3, y3));
		}
	}
}
