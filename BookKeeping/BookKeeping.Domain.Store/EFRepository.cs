using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;

namespace BookKeeping.Domain.Store
{
	public class EFRepository<T> : IRepository<T>
		where T: class
    {
		public EFRepository(IDbContextFor<T> dbContext)
		{
			_dbContext = dbContext;
		} 

		public IReadOnlyCollection<T> Query(Expression<Func<T, bool>> filter)
		{
			return _dbContext.Set().Where(filter).ToArray();
		}

		public void Insert(T newExpense)
		{
			_dbContext.Set().Add(newExpense);
			_dbContext.SaveChanges();
		}

		private readonly IDbContextFor<T> _dbContext;
	}

	public interface IDbContextFor<T> where T : class
	{
		IDbSet<T> Set();
		void SaveChanges();
	}
}
