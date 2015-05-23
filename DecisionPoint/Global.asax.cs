using log4net;
using log4net.Appender;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RusticiSoftware.HostedEngine.Client;
using System.Web.Configuration;
using System.Web;

namespace DecisionPoint
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// regsiters global filters bundles, routes, scorm api
        /// </summary>
        ///  <createdby>Sumit Saurav</createdby>
        /// <createddate>20 oct 2014</createddate>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ScormCloud.Configuration =
                new RusticiSoftware.HostedEngine.Client.Configuration(
                        WebConfigurationManager.AppSettings["HostedEngineWebServicesUrl"],
                        WebConfigurationManager.AppSettings["HostedEngineAppId"],
                        WebConfigurationManager.AppSettings["HostedEngineSecurityKey"],
                        WebConfigurationManager.AppSettings["Origin"]);
        }
        /// <summary>       
        /// don't store cache prevent on browser back button.
        /// </summary>
        ///  /// <createdby>Sumit Saurav</createdby>
        /// <createddate>21 may 2014</createddate>
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}