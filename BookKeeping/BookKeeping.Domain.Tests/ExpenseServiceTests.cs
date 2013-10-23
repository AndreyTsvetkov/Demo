using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookKeeping.Domain.Tests
{
	[TestClass]
	public class ExpenseServiceTests
	{
		[TestMethod]
		public void WhenListsInPeriod_ThenAllItemsReturnedBelongToThatPeriod()
		{
			// given
			var baseDate = new DateTime(2011, 1, 15);
			var testData = (from counter in Enumerable.Range(0, 5) 
							let date = baseDate.AddMonths(counter)
							select new Expense(date, 1m, "Category1")).ToArray();

			var repoMock = new Mock<IRepository<Expense>>();
			repoMock.Setup(_ => _.Query(It.IsAny<Expression<Func<Expense, bool>>>()))
			        .Returns<Expression<Func<Expense, bool>>>(filter => testData.Where(filter.Compile()).ToArray());

			var expenseService = new ExpenseService(repoMock.Object);

			// when
			var result = expenseService.List(from: baseDate.AddMonths(1), to: baseDate.AddMonths(3));

			// then 
			Assert.AreEqual(1, result.Count());
			Assert.IsTrue(result.All(_ => _.WhenApplied > baseDate.AddMonths(1) && _.WhenApplied < baseDate.AddMonths(3)));
		}
	}
}