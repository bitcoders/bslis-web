using System.Web.Optimization;

namespace LitmusWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Initial script for each page
            bundles.Add(new ScriptBundle("~/Bundles/scriptBundle_initialScripts")
                .Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                         "~/Content/mainContent/vendors/base/vendor.bundle.base.js",
                         //"~/Content/mainContent/vendors/chart.js/Chart.min.js",
                         "~/Content/mainContent/vendors/datatables.net/jquery.dataTables.min.js",
                         "~/Content/mainContent/vendors/datatables.net-bs4/dataTables.bootstrap4.js",
                         "~/Scripts/js/off-canvas.js",
                         "~/Scripts/js/hoverable-collapse.js",
                         "~/Scripts/js/template.min.js",
                         "~/Scripts/js/dashboard.min.js",
                         "~/Scripts/js/data-table.min.js",
                         "~/Scripts/jquery.validate.min.js",
                         "~/Scripts/jquery.validate.unobtrusive.min.js",
                         //"~/Scripts/file-upload.js",
                         "~/Scripts/umd/popper.min.js"
                          , "~/Content/mainContent/vendors/jquery-bar-rating/jquery.barrating.min.js"
                          //"~/Content/mainContent/vendors/jquery-asColor/jquery-asColor.min.js",
                          //"~/Content/mainContent/vendors/jquery-asGradient/jquery-asGradient.min.js"
                          //,"~/Content/mainContent/vendors/jquery-asColorPicker/jquery-asColorPicker.min.js"
                          //,"~/Content/mainContent/vendors/x-editable/bootstrap-editable.min.js"
                          , "~/Content/mainContent/vendors/moment/moment.min.js"
                          , "~/Content/mainContent/vendors/tempusdominus-bootstrap-4/tempusdominus-bootstrap-4.min.js"
                          , "~/Content/mainContent/vendors/bootstrap-datepicker/bootstrap-datepicker.min.js"
                          //, "~/Content/mainContent/vendors/dropify/dropify.min.js"
                          //, "~/Content/mainContent/vendors/jquery-file-upload/jquery.uploadfile.min.js"
                          , "~/Content/mainContent/vendors/jquery-tags-input/jquery.tagsinput.min.js"
                          //, "~/Content/mainContent/vendors/dropzone/dropzone.js"
                          , "~/Content/mainContent/vendors/jquery.repeater/jquery.repeater.min.js"
                          , "~/Content/mainContent/vendors/inputmask/jquery.inputmask.bundle.js"
                          , "~/Scripts/js/hoverable-collapse.js"
                          //, "~/Scripts/js/template.js"
                          , "~/Scripts/js/settings.js"
                          , "~/Scripts/js/todolist.js"
                          , "~/Scripts/js/formpickers.js"
                          , "~/Scripts/js/form-addons.js"
                          , "~/Scripts/js/x-editable.js"
                          //, "~/Scripts/js/dropify.js"
                          //, "~/Scripts/js/dropzone.js"
                          //, "~/Scripts/js/jquery-file-upload.js"
                          , "~/Scripts/js/formpickers.js"
                          , "~/Scripts/js/form-repeater.js"
                          , "~/Scripts/js/inputmask.js"
                          , "~/Scripts/jquery.datetimepicker.js"
                          , "~/Scripts/CustomScripts/dateTimePicker.js"

                ));
            #endregion

            // js bundle for datatable on web pages
            #region Scripts and CSS for data-table pages
            #region javascript
            bundles.Add(new ScriptBundle("~/Bundles/scriptbundle_datatable")
                .Include("~/Scripts/js/tabs.js",
                         "~/Scripts/dataTables.bootstrap4.js",
                         "~/Scripts/hoverable-collapse.js",
                         "~/Content/mainContent/vendors/mdi/css/materialdesignicons.min.css",
                         "~/Content/mainContent/vendors/base/vendor.bundle.base.css",
                         "~/Content/mainContent/css/style.css"


            ));
            bundles.Add(new StyleBundle("~/bundles/styleBundle_dataTable")
                .Include(
                "~/Content/mainContent/css/data-table/bootstrap-table.css",
                "~/Content/ainContent/css/data-table/bootstrap-editable.css",
                  "~/Content/MainContent/vendors/datatables.net-bs4/dataTables.bootstrap4.css",
                  "~/Content/jquery.datetimepicker.css"
                ));

            #endregion
            #region css/Style sheets

            #endregion
            #endregion

            ///set EnableOptimization to true for minification and optimization, otherwise all
            ///scripts will be loaded as usual
            BundleTable.EnableOptimizations = false;
        }



    }
}