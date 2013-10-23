using System;

namespace BookKeeping.Domain
{
    public class Expense
    {
	    public Expense(DateTime whenApplied, decimal amount, string category)
	    {
		    WhenApplied = whenApplied;
		    Amount = amount;
		    Category = category;
	    }

		// for ORM only; Domain.Store projects sees this constructor thanks to friendship between these assemplies
		internal Expense(){}

		public int Id { get; set; }
	    public string Category { get; set; }
		public decimal Amount { get; set; }
		public DateTime WhenApplied { get; set; }
    }
}
