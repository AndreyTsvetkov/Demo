using System.Web.Mvc;

// ReSharper disable CheckNamespace
namespace BookKeeping
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
// ReSharper restore CheckNamespace
