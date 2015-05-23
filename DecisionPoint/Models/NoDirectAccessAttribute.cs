using System;
using System.Configuration;
using System.Web.Mvc;

namespace DecisionPoint.Models
{
    /// <summary>
    /// filter to prevent direct URL hitting in browser
    /// <createdby>Sumit Saurav</createdby>
    /// <createddate>13/05/2014</createddate>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (!controllerName.Contains("login") && !actionName.Contains("uploadscorm") && !actionName.Contains("CheckPasswordLogin") && !actionName.Contains("PayPalResponse") && !actionName.Contains("Welcome") && !actionName.Contains("UpgradePackage") && !actionName.Contains("MakeIcPayment"))
            {
                if (filterContext.HttpContext.Request.UrlReferrer == null ||
                            filterContext.HttpContext.Request.Url.Host != filterContext.HttpContext.Request.UrlReferrer.Host)
                {
                    //send them off to the login page
                    var url = new UrlHelper(filterContext.RequestContext);
                    var loginUrl = url.Content("~/Login/Login");
                    
filterContext.HttpContext.Response.Redirect(loginUrl, true);
                }
            }
        }
    }
}