using System;
using System.Collections.Generic;
using System.Linq;

namespace Util
{
	public struct Maybe<T>
	{
		public static Maybe<T> Nothing;

		public Maybe(T value)
		{
			_value = value;
			// ReSharper disable CompareNonConstrainedGenericWithNull by design: если будет value type, то всегда true. это нас устраивает
			HasValue = value != null;
			// ReSharper restore CompareNonConstrainedGenericWithNull
		}

		public T Value
		{
			get
			{
				if (!HasValue)
					throw new InvalidOperationException("Maybe Nothing has no Value");
				return _value;
			}
		}
		public readonly bool HasValue;

		public override string ToString()
		{
			return HasValue ? _value.ToString() : "Nothing of " + typeof(T).Name;
		}
		private readonly T _value;
	}

	public static class Maybe
	{
		public static void Do<T>(this Maybe<T> maybe, Action<T> action)
		{
			if (maybe.HasValue)
				action(maybe.Value);
		}

		public static Maybe<T> ToMaybe<T>(this T value)
		{
			return new Maybe<T>(value);
		}

		public static Maybe<T> FirstMaybe<T>(this IEnumerable<T> values, Func<T, bool> predicate)
		{
			return values.FirstOrDefault(predicate).ToMaybe();
		}

		public static Maybe<TR> Select<T, TR>(this Maybe<T> val, Func<T, TR> projector)
		{
			return val.HasValue ? projector(val.Value).ToMaybe() : Maybe<TR>.Nothing;
		}

		public static T OrElse<T>(this Maybe<T> val, T @default)
		{
			return val.HasValue ? val.Value : @default;
		}
		public static T OrElse<T>(this Maybe<T> val, Func<T> @default)
		{
			return val.HasValue ? val.Value : @default();
		}
		public static T OrElse<T>(this Maybe<T> val, Func<Exception> exception)
		{
			if (!val.HasValue) throw exception();

			return val.Value;
		}
	}
}