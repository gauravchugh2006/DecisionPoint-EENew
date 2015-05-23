using System;
using System.Web;
using System.Web.Mvc;

namespace DecisionPoint.Models
{
    /// <summary>
    /// filter to check session on each action
    /// <createdby>Sumit Saurav</createdby>
    /// <createddate>13/5/2014</createddate>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            if (!controllerName.Contains("login"))
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                var user = session["UserId"]; //Key 2 should be User or UserName
                if (((user == null) && (!session.IsNewSession)) || (session.IsNewSession))
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