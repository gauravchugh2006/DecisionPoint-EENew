using DecisionPointCAL;
using System;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;

// ******************************************************************************************************************************
//                                                 class:DecisionPointHandler.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Feb. 24, 2014     |Bobi           | This class holds the interaction logic for DecsisonPointHandlerModule.cs
// ******************************************************************************************************************************

namespace DecisionPointHandler
{
    public class DecsisonPointHandlerModule : IHttpModule
    {

        #region Global Variables
        private static HttpRequest objHttpRequest = null;
        private static HttpResponse objHttpResponse = null;
        #endregion

        /// <summary>
        /// Init the module for authenticate the request
        /// </summary>
        /// <param name="context"></param>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>
        public void Init(HttpApplication context)
        {
            //call OnApplicationAuthenticateRequest : for get the passed authorization in request and check that the requested use is authorized for access the URL or not.
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            //call OnApplicationAuthenticateRequest : logic inetraction at the end the reuest.
            context.EndRequest += OnApplicationEndRequest;
        }

        /// <summary>
        /// Used for get the passed authorization in request and check that the requested use is authorized for access the URL or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>
        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                //get the HttpContext current request
                objHttpRequest = HttpContext.Current.Request;
                //get the HttpContext current response
                objHttpResponse = HttpContext.Current.Response;
                //get Authorization header of current request
                var authHeader = objHttpRequest.Headers["Authorization"];
                //check if there is no any authorization header
                if (authHeader != null)
                {
                    //check the passed credentials is authorized or not
                    var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
                    if (authHeaderVal.Scheme.Equals("basic",
                            StringComparison.OrdinalIgnoreCase) &&
                        authHeaderVal.Parameter != null)
                    {
                        AuthenticateUser(authHeaderVal.Parameter);
                    }
                }
                else
                {
                    objHttpResponse.Clear();
                    objHttpResponse.Write(System.DateTime.Now + DecisionPointR.BrowserRequestMessage);
                    objHttpResponse.End();
                   
                   
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                //To Handle HTTP Exception "Cannot redirect after HTTP headers have been sent".
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used for processed the logic at the end of application request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            try
            {
                objHttpRequest = HttpContext.Current.Request;
                objHttpResponse = HttpContext.Current.Response;
                var authHeader = objHttpRequest.Headers["Authorization"];
                //check if there is no any authorization header
                if (authHeader != null)
                {
                    //check the user is authorized or not
                    if (objHttpResponse.StatusCode == 401)
                    {
                        objHttpResponse.Headers.Add("WWW-Authenticate",
                            string.Format("Basic realm=\"{0}\"", objHttpRequest.Url.Host));
                    }
                   
                }
                //return objHttpResponse;
            }
            catch (Exception)
            { 
                throw;
            }
        }

        /// <summary>
        /// Implement logic for AuthenticateUser
        /// </summary>
        /// <param name="credentials"></param>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>
        private static void AuthenticateUser(string credentials)
        {
            try
            {
                //Encode the passed credentials with request
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                //get the passed user name and password 
                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);
                //check that username and password which we get from current request is correct or not
                if (CheckPassword(name, password))
                {
                    var identity = new System.Security.Principal.GenericIdentity(name);
                    SetPrincipal(new System.Security.Principal.GenericPrincipal(identity, null));
                }
                else
                {
                    //Invalid username or password.
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                HttpContext.Current.Response.StatusCode = 401;
            }
        }

        /// <summary>
        /// Set the Thread.CurrentPrincipal with current credentials
        /// </summary>
        /// <param name="principal"></param>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>
        private static void SetPrincipal(System.Security.Principal.IPrincipal principal)
        {
            try
            {
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Here is where you would validate the username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>28 Feb 2015</createddate>/// 
        private static bool CheckPassword(string username, string password)
        {
            try
            {
                return username == DecisionPointR.SterlingUserName && password == DecisionPointR.SterlingPassword;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        /// <summary>
        /// Dispose current object
        /// </summary>
        public void Dispose(){}
    }
}