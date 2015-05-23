using System.Web.Optimization;

namespace DecisionPoint
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            //Added CSS Files for bootstrap
            bundles.Add(new StyleBundle("~/Content/css/bootcss").Include(
                 "~/Content/css/Dashboard/bootstrap.css",
                 "~/Content/css/Dashboard/bootstrap-datetimepicker.css"
                ));
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //           "~/Scripts/modernizr-*"));
            //Added js files for validations
            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                         "~/Scripts/jquery.validate.js*",
                         "~/Scripts/jquery.validate.unobtrusive.js*"));

            // Added scripts for DashBoard UI
            bundles.Add(new Bundle("~/bundles/dashboard").Include(
                                   "~/Scripts/jquery-1.9.1.js*",
                                   "~/Scripts/jquery-ui-1.10.3.js*",
                                  "~/Scripts/chosen.jquery.min.js*",
                                  "~/Scripts/fullcalendar.min.js*",
                                  "~/Scripts/jquery.media.js*",
                                  "~/Scripts/new.js*",
                                   "~/Scripts/bootstrap-datetimepicker.min.js*",
                                    "~/Scripts/bootstrap.min.js*"

                                  ));
            //Added CSS Files for Dashboard
            bundles.Add(new StyleBundle("~/Content/css/Dashboardcss").Include(
                 "~/Content/css/Dashboard/chosen.css",
                 "~/Content/css/Dashboard/fullcalendar.css",
                 "~/Content/css/Dashboard/screen.css",
                 "~/Content/css/Dashboard/screen-mix.css",
                  "~/Content/css/Dashboard/style.css",
                  "~/Content/css/Dashboard/megamenu.css",
                  "~/Content/css/Dashboard/jquery-ui.css",
                  "~/Content/css/Dashboard/bootstrap-datetimepicker.min.css",
                   "~/Content/css/Dashboard/bootstrap.min.css",
                  "~/Content/themes/base/jquery-ui*",
               "~/Content/themes/base/jquery.ui*",
               "~/Content/css/Dashboard/common.css"

                 ));

            //Added CSS Files for History section
            bundles.Add(new StyleBundle("~/Content/css/historycss").Include(
                 "~/Content/css/Dashboard/Innerstyles.css"
                ));
            // Added scripts for DashBoard UI
            bundles.Add(new Bundle("~/bundles/historyscript").Include(
                                   "~/Scripts/jquery-1.9.1.js*",
                                   "~/Scripts/jquery-ui-1.10.3.js*"
                                  ));
            //Added CSS Files for Login
            bundles.Add(new StyleBundle("~/Content/css/Logincss").Include(
                 "~/Content/css/Login/fullcalendar.css",
                 "~/Content/css/Login/chosen.css",
                 "~/Content/css/Login/screen.css"
                 ));

            // Added scripts for Login UI
            bundles.Add(new Bundle("~/bundles/Login").Include(
                                    "~/Scripts/jquery-1.7.1.js*",
                                  "~/Scripts/jquery-1.7.1.min.js*",
                                  "~/Scripts/bootstrap.min.js*",
                                  "~/Scripts/fullcalendar.min.js*",
                                  "~/Scripts/bootstrap-datetimepicker.min.js*",
                                  "~/Scripts/chosen.jquery.min.js*",
                                  "~/Scripts/application.js*"
                                  ));
            bundles.Add(new Bundle("~/bundles/register").Include(
                                  "~/Scripts/jquery-1.9.1.js*",
                                  "~/Scripts/jquery-ui-1.10.3.js",
                                 "~/Scripts/bootstrap.min.js*",
                                 "~/Scripts/bootstrap-datetimepicker.min.js*",
                                 "~/Scripts/chosen.jquery.min.js*",
                                 "~/Scripts/fullcalendar.min.js*",
                                 "~/Scripts/jquery.media.js*"
                                 ));

            bundles.Add(new Bundle("~/bundles/RegisterCss").Include(
                                 "~/Content/css/Register/screen.css",
                                 "~/Content/css/Register/chosen.css",
                                "~/Content/css/Register/fullcalendar.css",
                                "~/Content/css/Register/bootstrap.min.css",
                                "~/Content/css/Login/bootstrap-datetimepicker.min.css",
                                "~/Content/css/Dashboard/jquery-ui.css"
                                ));

            //CSS AND JS FOR FULL CALENDER
            bundles.Add(new StyleBundle("~/bundles/fullcalendercss").Include("~/Content/css/FullCalender/fullcalendar.css"));
            bundles.Add(new StyleBundle("~/bundles/fullcalenderjs").Include( "~/Scripts/moment.min.js*", "~/Scripts/fullcalendar.min.js*"));
            bundles.Add(new StyleBundle("~/bundles/fullcalenderjs").Include("~/Scripts/moment.min.js*", "~/Scripts/fullcalendar.min.js*"));
            // script for CompanyDashboardLayout
            bundles.Add(new StyleBundle("~/bundles/CompanyDashBoardLayoutjs").Include("~/Scripts/DashBoard/_CompanyDashBoardLayout.js*", "~/Scripts/DashBoard/_CompanyDashBoardLayout.js"));
            //Script for Permission Table
            bundles.Add(new StyleBundle("~/bundles/permissionjs").Include("~/Scripts/PermissionTable.js*"));
            BundleTable.EnableOptimizations = true;

        }
    }
}