using System.Web;
using System.Web.Optimization;

namespace FileControl
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                      "~/Scripts/DataTables-1.10.20/js/jquery.dataTables.js",
                      "~/Scripts/DataTables-1.10.20/js/dataTables.bootstrap4.js"));

            bundles.Add(new StyleBundle("~/bundles/dataTables/css").Include(
                     "~/Scripts/DataTables-1.10.20/css/jquery.dataTables.css",
                     "~/Scripts/DataTables-1.10.20/css/dataTables.bootstrap4.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include(
                      "~/Scripts/DropZone/dropzone.js",
                      "~/Scripts/DropZone/dropzone-custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                      "~/Scripts/jquery.signalR-2.4.1.js",
                      "~/signalr/hubs"));
        }
    }
}
