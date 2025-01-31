using System.Web;
using System.Web.Optimization;

namespace DemoProject.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jslib").Include(
                        //"~/Scripts/bootstrap.js",
                        //"~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery-idleTimeout.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/myjquery.js",
                        "~/Scripts/progress.js",
                        "~/Scripts/sidebar.js",
                        "~/Scripts/Custom/dateLock.js"
                      ));

            bundles.Add(new StyleBundle("~/bundles/css/custom").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Custom.css",
                      "~/Content/css/select2.min.css",
                      "~/Content/sweetalert.css"
                      ));
        }
    }
}
