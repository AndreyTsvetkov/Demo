using System;
using System.Collections.Generic;

namespace BookKeeping.Domain
{
	public interface IExpenseService
	{
		IReadOnlyCollection<Expense> List(DateTime from, DateTime to);
		void Add(decimal amount, string category);
	}

	public class ExpenseService : IExpenseService
	{
		public ExpenseService(IRepository<Expense> repository)
		{
			_repository = repository;
		}

		public IReadOnlyCollection<Expense> List(DateTime from, DateTime to)
		{
										// the single little bit of some 'domain logic'
			return _repository.Query(expense => expense.WhenApplied > from && expense.WhenApplied < to);
		}

		public void Add(decimal amount, string category)
		{
			var newExpense = new Expense(DateTime.Now, amount, category);
			
			// simple variant of repos - no UoW here, as not needed
			_repository.Insert(newExpense);
		}

		private readonly IRepository<Expense> _repository;
	}
}