using System;
using System.Collections.Generic;

namespace Asc2Pnt.Model
{
	internal static class LinqEx
	{
		public static IEnumerable<T> UnionAll<T>(this IEnumerable<T> src, IEnumerable<T> other)
		{
			foreach (var item in src)
				yield return item;
			foreach (var item in other)
				yield return item;
		}
		public static T[] RemoveSamePairs<T>(this IEnumerable<T> src)
		{
			var output = new List<T>();
			foreach (var item in src)
			{
				if (output.Contains(item))
					output.Remove(item);
				else
					output.Add(item);
			}
			return output.ToArray();
		} 
		public static IEnumerable<T> ForeachDeferred<T>(this IEnumerable<T> src, Action<T> action)
		{
			foreach (var item in src)
			{
				action(item);
				yield return item;
			}
		} 
	}
}