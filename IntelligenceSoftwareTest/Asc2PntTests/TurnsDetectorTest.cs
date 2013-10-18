using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Asc2Pnt;
using Asc2Pnt.Model;
using NUnit.Framework;
namespace Asc2PntTests
{
	[TestFixture]
	public class TurnsDetectorTest
	{
		[Test]
		public void FindTurnPointsInTest()
		{
			var folder = Path.Combine(Environment.CurrentDirectory, @"..\..\TestData");
			var inFile = Path.Combine(folder, "sample.asc");
			var answerFile = Path.Combine(folder, "sample.answer.asc");

			var storage = new FileStorage<DiscretePoint>(new DiscretePointSerializer());
			var inPoints = storage.LoadFromStorage(inFile);
			var answerPoints = storage.LoadFromStorage(answerFile);

			var sut = new TurnsDetector();

			var result = sut.FindTurnPointsIn(inPoints).OrderBy(_ => _.X).ThenBy(_ => _.Y);

			Trace.WriteLine(new DiscretePointSerializer().Serialize(answerPoints));
			Trace.WriteLine(new DiscretePointSerializer().Serialize(result));

			foreach (var pair in result.OrderBy(_ => _.X).ThenBy(_ => _.Y).Zip(answerPoints.OrderBy(_ => _.X).ThenBy(_ => _.Y), (r, a) => new { Got = r, Expected = a }))
				Assert.AreEqual(pair.Expected, pair.Got);
		}
	}
}
