using System.Web;
using System.Web.Optimization;

namespace VLCitas
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/calendarcss").Include(
                      "~/Content/fullcalendar.min.css",
                      "~/Content/theme/fullcalendar.min.css"));

            bundles.Add(new StyleBundle("~/Content/quill-editor").Include(
                      "~/Content/robust/app-assets/vendors/css/editors/quill/katex.min.css",
                      "~/Content/robust/app-assets/vendors/css/editors/quill/quill.bubble.css",
                      "~/Content/robust/app-assets/vendors/css/editors/quill/quill.snow.css"));
                      //"~/Content/robust/app-assets/vendors/css/editors/quill/quill.table.css"));

            bundles.Add(new StyleBundle("~/Content/datetimepicker").Include(
                    "~/Content/datetimepicker/extension.css"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                     "~/Scripts/dropzone/theme/vendor_dropzone.min.js",
                     "~/Scripts/dropzone/theme/extension_dropzone.min.js"));
                     //"~/Scripts/dropzone/dropzone.js"));

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/theme/vendor_dropzone.min",
                     "~/Scripts/dropzone/theme/dropzone.min.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/vendorDataTables").Include(
                   "~/Content/robust/app-assets/vendors/js/tables/jquery.dataTables.min.js",
                   "~/Content/robust/app-assets/vendors/js/tables/dataTables.bootstrap4.min.js",
                   "~/Content/robust/app-assets/vendors/js/tables/dataTables.responsive.min.js",
                   "~/Content/robust/app-assets/vendors/js/tables/dataTables.rowReorder.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                   "~/Content/datetimepicker/extension.js"));

            bundles.Add(new ScriptBundle("~/bundles/quill-editor").Include(
                 "~/Content/robust/app-assets/vendors/js/editors/quill/highlight.min.js",
                 "~/Content/robust/app-assets/vendors/js/editors/quill/katex.min.js",
                 "~/Content/robust/app-assets/vendors/js/editors/quill/quill.min.js"));
                 //"~/Content/robust/app-assets/vendors/js/editors/quill/quill-table.js"));
        }
    }
}
