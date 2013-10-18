using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asc2Pnt.Model;

namespace Asc2Pnt
{
	public class DiscretePointSerializer : ITextSerializer<DiscretePoint>
	{
		public string Serialize(IEnumerable<DiscretePoint> points)
		{
			return points
				.Aggregate(new StringBuilder(), (builder, point) => builder.AppendFormat("{0}{2}{1}{3}", point.X, point.Y, CoordinateSeparators.First(), PointSeparator))
				.ToString();
		}

		public DiscretePoint[] Deserialize(string input)
		{
			const int xIndex = 0, yIndex = 1;
			return (from line in input.Split(new[] {PointSeparator}, StringSplitOptions.RemoveEmptyEntries)
			        let lineParts = line.Split(CoordinateSeparators, StringSplitOptions.RemoveEmptyEntries)
			        where lineParts.Count() > yIndex
			        select new DiscretePoint(int.Parse(lineParts[xIndex]), int.Parse(lineParts[yIndex])))
				.ToArray();
		}

		private readonly static string PointSeparator = Environment.NewLine;
		private readonly static string[] CoordinateSeparators = new[] { " ", "\t" };
	}
}