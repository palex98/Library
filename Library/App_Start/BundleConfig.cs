using System.Web;
using System.Web.Optimization;

namespace Library
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/Script.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap.css").Include(
                      "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/Style.css"));

        }
    }
}
