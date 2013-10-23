using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EspleyTest.Grabber;
using EspleyTest.Viewer.App_Start;
using Microsoft.Practices.Unity;

namespace EspleyTest.Viewer
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var container = Bootstrapper.Initialise();
			var importer = container.Resolve<Importer>();

			var cts = new CancellationTokenSource();
			Task.Factory.StartNew(() => importer.Start(cts.Token), cts.Token);
		}
	}
}