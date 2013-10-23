using System;
using System.Linq;
using System.Web.Http;
using BookKeeping.Domain;

namespace BookKeeping.Controllers.API
{
    public class ExpenseController : ApiController
    {
	    public static readonly TimeSpan TimeWindow = TimeSpan.FromDays(100);

	    public ExpenseController(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		[HttpGet]
		[HttpPost]
		public void Add(NewExpenseDTO newDto)
		{
			_expenseService.Add(decimal.Parse(newDto.Amount), newDto.Category);
		}

		[HttpGet]
		[HttpPost]
		public ExpenseDTO[] List()
		{
			return _expenseService.List(DateTime.Now - TimeWindow, DateTime.Now)
			               .Select(ExpenseDTO.FromDomain)
			               .ToArray();
		}

		private readonly IExpenseService _expenseService;
	}

	public class ExpenseDTO
	{
		public static ExpenseDTO FromDomain(Expense arg)
		{
			return new ExpenseDTO { Amount = arg.Amount, Category = arg.Category, Date = arg.WhenApplied.Date, Id = arg.Id };
		}

		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Category { get; set; }
		public decimal Amount { get; set; }
	}

	public class NewExpenseDTO
	{
		public string Category { get; set; }
		public string Amount { get; set; }
	}
}
