using System;
using System.Collections.Generic;
using System.Linq;

namespace Asc2Pnt.Model
{
	/// <summary>
	/// Выявитель поворотов («углов»)
	/// </summary>
	public class TurnsDetector
	{
		private const int SleepPerPointInDemoMode = 15;
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
			var allFragments = new List<DiscretePointSequence>();

			// бросаем точки по одной, всё за один проход
			foreach (var point in figure)
			{
				OnPointHandled(point);
				if (DemoMode) 
					System.Threading.Thread.Sleep(SleepPerPointInDemoMode);

				// находим, к каким фрагментам она может приклеиться
				var fragmentsNearPoint = allFragments.Where(f => f.CanBeExtendedBy(point)).ToArray();
				// если нет таких, она начинает новый фрагмент
				if (!fragmentsNearPoint.Any())
					allFragments.Add(new DiscretePointSequence(point));
				else
				{
					// если смежные фрагменты есть (а их тут может быть или 1, или 2), 
					fragmentsNearPoint = (from fragment in fragmentsNearPoint
										  let extended = fragment.ExtendBy(point, OnTurnDetected) // то расширяем каждый фрагмент этой точкой; 
										  let _ = fragment.Do(it => allFragments.Replace(it, extended)) // заменяем изменившиеся фрагменты в общем перечне; allFragments — единственная изменяемая коллекция, поэтому и побочный эффект возникает
										  select extended)
						.ToArray();

					// если же два нашлось фрагмента, значит они склеются теперь
					if (fragmentsNearPoint.Length == 2)
					{
						// останется в перечне первый, он вберет в себя  второй, который уйдет
						var winnerFragment = fragmentsNearPoint.First();
						var looserFragment = fragmentsNearPoint.Last();
						allFragments.Replace(winnerFragment, winnerFragment.ExtendBy(looserFragment, OnTurnDetected));
						allFragments.Remove(looserFragment);
					}
				}
			}

			// если на вход дали точки замкнутого контура, то останется только один фрагмент. его и берем
			if (allFragments.Count() == 1)
				return allFragments.Single().Turns;
			else
				throw new ArgumentException("points do not form a closed fugure");

			// p.s. для незамкнутого тоже работало бы:
			//return allFragments.SelectMany(_ => _.Turns).ToArray();
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

	internal static class ListEx
	{
		public static void Replace<T>(this List<T> list, T itemToRemove, T itemToAdd)
		{
			list.Remove(itemToRemove);
			list.Add(itemToAdd);
		}
	}
	internal static class FluentEx
	{
		/// <summary>
		/// Выполняет переданный функтор, передавая туда целевой объект; 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="action"></param>
		/// <remarks>полезен для формирования fluent-цепочек, в частности, вставки побочных эффектов в код linq</remarks>
		/// <returns>возвращает исходный объект</returns>
		public static T Do<T>(this T obj, Action<T> action)
		{
			if (!ReferenceEquals(obj, null))
				action(obj);
			return obj;
		}
	}
}