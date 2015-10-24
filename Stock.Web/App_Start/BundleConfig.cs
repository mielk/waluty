using System.Web.Optimization;

namespace Stock.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                        "~/Scripts/common/notify.js",
                        "~/Scripts/common/mielk.js",
                        "~/Scripts/jquery.sizes.js",
                        "~/Scripts/common/util.js",
                        "~/Scripts/common/tree.js",
                        "~/Scripts/common/spin.js",
                        "~/Scripts/common/select2.js",
                        "~/Scripts/common/dropdown.js",
                        //"~/Scripts/css-element-queries/ElementQueries.js",
                        //"~/Scripts/css-element-queries/ResizeSensor.js",
                        "~/Scripts/app/global.js"));

            bundles.Add(new ScriptBundle("~/bundles/stock").Include(
                        "~/Scripts/STOCK/StockCommon.js",
                        "~/Scripts/STOCK/Quotation.js",
                        "~/Scripts/STOCK/Market.js",
                        "~/Scripts/STOCK/Company.js",
                        "~/Scripts/STOCK/QuotationsAnalyzer.js",
                        "~/Scripts/STOCK/SvgPathFactory.js",
                        "~/Scripts/STOCK/SvgRenderer.js",
                        "~/Scripts/STOCK/LabelFactory.js",
                        "~/Scripts/STOCK/ProcessButtons.js"));

            bundles.Add(new ScriptBundle("~/bundles/charts").Include(
                        "~/Scripts/common/Raphael.js",
                        "~/Scripts/charts/Chart.js",
                        "~/Scripts/charts/ChartsContainer.js",
                        "~/Scripts/charts/OptionPanel.js",
                        "~/Scripts/charts/ChartsController.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/normalize.css",
                        "~/Content/select2.css",
                        "~/Content/tree.css",
                        "~/Content/dropdown.css",
                        "~/Content/mielk.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/charts").Include(
                        "~/Content/charts.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/normalize.css",
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/bootstrap/bootstrap-theme.css",
                        "~/Content/bootstrap/bootstrap.css"));
        }
    }
}