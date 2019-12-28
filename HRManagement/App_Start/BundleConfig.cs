using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HRManagement.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            //Css StyleBundle
            bundle.Add(new StyleBundle("~/css/all").Include(
                "~/Content/Site.css",
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-theme.css"));

            //Js- ScriptBundle
            bundle.Add(new ScriptBundle("~/js/all").Include(
                "~/Scripts/jquery-3.3.1.min.js",
                 "~/Scripts/jquery-3.3.1.slim.min.js",
                  "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/modernizr-2.8.3.js"));

            //Css Home Index Page
            bundle.Add(new StyleBundle("~/css/Home/Index").Include(
                "~/Content/all.min.css",
                "~/Content/sb-admin-2.min.css"));

            //JS Home Index Page
            bundle.Add(new ScriptBundle("~/script/Home/Index").Include(
                "~/Scripts/bootstrap.bundle.min.js",
                "~/Scripts/jquery.easing.min.js",
                "~/Scripts/sb-admin-2.min.js",
                "~/Scripts/Chart.min.js",
                "~/Scripts/chart-area-demo.js",
                "~/Scripts/chart-pie-demo.js"));

            //Css Home Login Page
            bundle.Add(new StyleBundle("~/css/Login").Include(
              "~/Content/Login.css",
               "~/Content/bootstrap.min.css"));

            //JS Home Login Page
            bundle.Add(new ScriptBundle("~/script/Login").Include(
                "~/Scripts/jquery-3.3.1.min.js" ));

            BundleTable.EnableOptimizations = true;
        }
    }
}