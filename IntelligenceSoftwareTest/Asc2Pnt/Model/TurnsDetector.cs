using System;
using System.Collections.Generic;
using System.Linq;

namespace Asc2Pnt.Model
{
	public class TurnsDetector
	{
		private const int SleepForPointInDemoMode = 15;
		/// <summary>
		/// Содержит основной алгоритм
		/// </summary>
		/// <param name="figure"></param>
		/// <returns></returns>
		public DiscretePoint[] FindTurnPointsIn(IEnumerable<DiscretePoint> figure)
		{
			// ReSharper disable AccessToForEachVariableInClosure: ок, так как никаких отложенных вызовов не делается

			/* Бросаем точки «на поле». каждая точка либо «приклеивается» к своему фрагменту ломаной,
			 * если она оказалась примыкающей к нему, 
			 * либо начинает собой новый фрагмент ломаной. 
			 * 
			 * По мере увеличения числа точек ломаные срастаются и объединяются, пока не остается одна 
			 * единственная замкнутся фигура.
			 * 
			 * Учет углов прозводится в моменты прироста ломаных точками и другими ломаными: 
			 * фиксируются моменты смены направления концов.
			 */

			// текущий перечень ломаных
			var fragments = new List<DiscretePointSequence>();

			// бросаем точки по одной, всё за один проход
			foreach (var point in figure)
			{
				OnPointHandled(point);
				if (DemoMode) System.Threading.Thread.Sleep(SleepForPointInDemoMode);

				// находим, к каким фрагментам она может приклеиться
				var fragmentsForPoint = fragments.Where(f => f.CanBeExtendedBy(point)).ToArray();
				// если нет таких, она начинает новый фрагмент
				if (!fragmentsForPoint.Any())
					fragments.Add(new DiscretePointSequence(point));
				else
				{
					// если есть, все (тут может быть 1 или 2 фрагмента) расширяем этой точкой; 
					// p.s. for с индексом и Remove/Add взялись тут из-за функционального стиля объектов модели (например DiscretePointSequence), 
					// они все неизменяемые, а этот общий алгоритм удобнее императивно сделать, из-за этого расхождения такой "мостик" 
					for (int i = 0; i < fragmentsForPoint.Length; i++)
					{
						// заменяем фрагменты на расширенные
						fragments.Remove(fragmentsForPoint[i]);
						fragments.Add(fragmentsForPoint[i] = fragmentsForPoint[i].ExtendBy(point, OnTurnDetected));
					}
					// если все же два нашлось фрагмента, значит они склеются теперь
					if (fragmentsForPoint.Length == 2)
					{
						// останется в перечне первый, он вберет в себя  второй, который уйдет
						var winnerFragment = fragmentsForPoint.First();
						var looserFragment = fragmentsForPoint.Last();
						fragments.Remove(winnerFragment);
						fragments.Remove(looserFragment);
						fragments.Add(winnerFragment.ExtendBy(looserFragment, OnTurnDetected));
					}
				}
			}

			// если на вход дали точки замкнутого контура, то останется только один фрагмент. его и берем
			if (fragments.Count() == 1)
				return fragments.Single().Turns;
			else
				throw new ArgumentException("points do not form a closed fugure");

			// p.s. для незамкнутого тоже работало бы:
			//return fragments.SelectMany(_ => _.Turns).ToArray();
		}

		public bool DemoMode { get; set; }
		public event EventHandler<DiscretePointEventArgs> PointHandled;
		public event EventHandler<DiscretePointEventArgs> TurnDetected;

		protected void OnPointHandled(DiscretePoint point)
		{
			var temp = PointHandled;
			if (DemoMode && temp != null)
				temp(this, new DiscretePointEventArgs(point));
		}
		protected void OnTurnDetected(DiscretePoint point)
		{
			var temp = TurnDetected;
			if (DemoMode && temp != null)
				temp(this, new DiscretePointEventArgs(point));
		}
	}

	public class DiscretePointEventArgs : EventArgs
	{
		public DiscretePoint Point { get; private set; }

		public DiscretePointEventArgs(DiscretePoint point)
		{
			Point = point;
		}
	}
}