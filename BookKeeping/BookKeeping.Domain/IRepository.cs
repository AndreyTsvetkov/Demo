using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookKeeping.Domain
{
	public interface IRepository<T>
	{
		IReadOnlyCollection<T> Query(Expression<Func<T, bool>> filter);
		void Insert(T newItem);
	}
}