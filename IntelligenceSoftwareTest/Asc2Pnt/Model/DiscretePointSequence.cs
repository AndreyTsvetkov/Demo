using System;
using System.Collections.Generic;
using System.Linq;

//using System.Diagnostics.Contracts;

namespace Asc2Pnt.Model
{
//	[Pure]
	public class DiscretePointSequence
	{
		/// <summary>
		/// Создаем участок ломаной изначально из одной точки: начало и конец совпадают, поворотов нет. 
		/// </summary>
		/// <param name="start"></param>
		public DiscretePointSequence(DiscretePoint start)
		{
			var singleEnd = new DiscretePointSequenceEnd(start);
			_ends = new[] {singleEnd, singleEnd};
		}

		/// <summary>
		/// Можно ли пристроить данную точку к ломаной (примыкает ли она по одному из 4-х направлений к одному из концов ломаной)
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public bool CanBeExtendedBy(DiscretePoint point)
		{
			return !_isClosed && _ends.Any(s => s.CanAttach(point));
		}

		/// <summary>
		/// Расширить ломаную переданной точкой.
		/// </summary>
		/// <param name="newPoint"></param>
		/// <param name="onTurnDetected">необязательный обработчик события нахождения нового поворота в ломаной</param>
		/// <exception cref="InvalidOperationException">Фигура уже замкнута - нельзя расширить, или не нашлось конца для приставки этой точки. </exception>
		/// <returns>Ломаная с включенной в нее новой точкой</returns>
		public DiscretePointSequence ExtendBy(DiscretePoint newPoint, Action<DiscretePoint> onTurnDetected = null)
		{
			if (onTurnDetected == null) onTurnDetected = _ => { };

			if (_isClosed)
				throw new InvalidOperationException("Figure is closed");

			if (_ends.All(s => s.CanAttach(newPoint))) // оба конца рядом с точкой → значит, либо фигура пока всего из одной точки, либо фигуре осталась 1 точка до закрытия
				if (_ends.First() == _ends.Last()) // пока только одна точка в последовательности, добавляем новыю точку как вторую
					return new DiscretePointSequence(
						new[]
							{
								_ends.First().ShiftTo(newPoint),
								_ends.Last().MakePointingFrom(newPoint)
							},
						_turns);
				else // новая точка закрыла фигуру; соединяем концы, добавляем возможные 2, 1 или 0 углов
					return new DiscretePointSequence(
						new[]
							{
								_ends.First(), _ends.First()
							},
						_turns.Union(
							(from end in _ends
							 where !end.InSameLineAs(newPoint)
							 select end.CurrentPoint)
								.ForeachDeferred(onTurnDetected)));
			else if (_ends.Any(s => s.CanAttach(newPoint)))
			{
				var movingEnd = _ends.First(e => e.CanAttach(newPoint));
				var steadyEnd = _ends.First(e => e != movingEnd);
				var additionalTurns = movingEnd.InSameLineAs(newPoint) ? Enumerable.Empty<DiscretePoint>() : Enumerable.Repeat(movingEnd.CurrentPoint, 1);
				return new DiscretePointSequence(new[] { movingEnd.ShiftTo(newPoint), steadyEnd }, _turns.Union(additionalTurns.ForeachDeferred(onTurnDetected)));
			}
			else
				throw new InvalidOperationException("newPoint is no neighbour for neither of my ends");
		}
		/// <summary>
		/// Присоединить другую ломаную к исходной. Соприкасающиеся концы «срастутся». Концы должны соприкасаться «внахлест».
		/// </summary>
		/// <param name="other"></param>
		/// <param name="onTurnDetected">необязательный обработчик события нахождения нового поворота в ломаной</param>
		/// <returns></returns>
		public DiscretePointSequence ExtendBy(DiscretePointSequence other, Action<DiscretePoint> onTurnDetected = null)
		{
			if (onTurnDetected == null) onTurnDetected = _ => { };

			if (_isClosed)
				throw new InvalidOperationException("Figure is closed");

			// повороты на "швах", где стыкуются под углом две последовательности
			var turnsOnSeams = (from myEnd in _ends
			                    join otherEnd in other._ends on myEnd equals otherEnd
			                    where myEnd.MakesTurnWith(otherEnd)
			                    select myEnd.CurrentPoint)
				.ForeachDeferred(onTurnDetected)
				.ToArray();

			if (_ends.All(myEnd => other._ends.Any(otherEnd => otherEnd == myEnd)))
				return new DiscretePointSequence(
					Enumerable.Repeat(_ends.First(), 2),
					_turns.Union(turnsOnSeams).Union(other._turns)
					);
			else if (_ends.Any(myEnd => other._ends.Any(otherEnd => otherEnd == myEnd)))
				return new DiscretePointSequence(
					_ends.UnionAll(other._ends).RemoveSamePairs(),
					_turns.Union(turnsOnSeams).Union(other._turns)
					);
			else
				throw new InvalidOperationException("sequences have no common ends");
		}

		public DiscretePoint[] Turns { get { return _turns; } }

		/// <summary>
		/// Создаем ломаную с указанием концов и поворотов
		/// </summary>
		/// <param name="ends"></param>
		/// <param name="turns"></param>
		private DiscretePointSequence(IEnumerable<DiscretePointSequenceEnd> ends, IEnumerable<DiscretePoint> turns)
		{
			_ends = ends.ToArray();
			if (_ends.Count() != 2)
				throw new InvalidOperationException("Ends can be only a pair");

			_turns = turns.ToArray();
			_isClosed = _ends.First() == _ends.Last();
		}

		private readonly DiscretePointSequenceEnd[] _ends;
		private readonly bool _isClosed = false;
		private readonly DiscretePoint[] _turns = new DiscretePoint[0];
	}
}