using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace BookKeeping.App_Start
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/libs").Include(
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/jquery.validate.js",
				"~/Scripts/jquery.cookie.js",
				"~/Scripts/jquery-ui-{version}.js",
				"~/Scripts/xdr.js",
				"~/Scripts/modernizr-{version}.js",
				"~/Scripts/knockout-{version}.js",
				"~/Scripts/knockout.mapping-latest.js",
				"~/Scripts/jquery.scroll.js"
				));


			var bundle = new StyleBundle("~/bundles/app/style")
				.IncludeDirectory("~/Scripts/App/Framework/", "*.css", true)
				.IncludeDirectory("~/Scripts/App/", "*.css", true);
			bundle.Transforms.Add(new CssMinify());
			bundles.Add(bundle);

			bundles.Add(new ScriptBundle("~/bundles/app/scripts")
							.IncludeDirectory("~/Scripts/App/framework/", "*.js", true)
							.Include("~/Scripts/App/ExpenseService.js")
							.Include("~/Scripts/App/ExpenseApp.js")
			);
		}
	}
}