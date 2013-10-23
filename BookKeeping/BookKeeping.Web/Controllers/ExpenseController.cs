using System.Web.Mvc;

namespace BookKeeping.Controllers
{
	public class ExpenseController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}