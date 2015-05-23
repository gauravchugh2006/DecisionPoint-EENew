using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DecisionPoint.Models;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPoint.Models.Webinar;

namespace DecisionPoint.Controllers
{
    public class WebinarController : Controller
    {
        //
        // GET: /Webinar/
        #region Global Variables

        DecisionPointEngine objDecisionPointEngine = null;
        WebinarDashboardModel objWebinarDashboardModel = null;
        WebinarUsersResponse objWebinarUsersResponse = null;
        WebinarModel objWebinarModel = null;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        #endregion
        #region WebinarLogin

        [HttpGet]
        public ActionResult WebinarLogin()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                // IList<WebinarUsersResponse> userList = objDecisionPointEngine.GetWebinarOrganiser(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                IList<WebinarUsersResponse> userList = objDecisionPointEngine.GetWebinarOrganiser(1);// for super admin credentails fixed temprorary
                if (userList != null && userList.Count > 0)
                {
                    objWebinarModel = new WebinarModel()
                    {
                        Id = userList[0].Id,
                        UserId = userList[0].UserId,
                        emailId = userList[0].UserName,
                        apiKey = userList[0].AppKey,
                        password = userList[0].Password,
                        OrganiserId = userList[0].OrganiserId,

                    };
                    return WebinarLogin(objWebinarModel);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                return RedirectToAction("Error", "Login", new { errorMsg = ex.ToString() });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        [HttpPost]
        public ActionResult WebinarLogin(WebinarModel webinarModel)
        {
            // first we need to create the uri for the web request
            string uri = String.Format("https://api.citrixonline.com/oauth/access_token?grant_type=password&user_id={0}&password={1}&client_id={2}",
                             webinarModel.emailId, webinarModel.password, webinarModel.apiKey);

            // then the request to login is created and sent. From the response
            // we need to store at least the access token and the organizer key
            // to use for further calls

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            string Result = string.Empty;
            try
            {
                var response = request.GetResponse();

                //the following lines duplicate the response stream so we can read it for
                //deserialization and also re-read it and write it out.

                using (MemoryStream ms = new MemoryStream())
                {
                    var stream = response.GetResponseStream();
                    stream.CopyTo(ms);
                    ms.Position = 0;
                    stream.Close();

                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ResponseDirectLogin));
                    var deserialized = (ResponseDirectLogin)ser.ReadObject(ms);
                    OauthToken = deserialized.AccessToken;
                    OrganizerKey = deserialized.OrganizerKey;

                    ms.Position = 0;
                    using (var sr = new StreamReader(ms))
                        Result = sr.ReadToEnd();
                }
                if (string.IsNullOrEmpty(webinarModel.OrganiserId))
                {
                    objWebinarUsersResponse = new WebinarUsersResponse()
                   {
                       Id = webinarModel.Id,
                       UserName = webinarModel.emailId,
                       Password = webinarModel.password,
                       AppKey = webinarModel.apiKey,
                       OrganiserId = OrganizerKey,
                       UserId = webinarModel.UserId,
                       IsActive = true,
                   };
                    objDecisionPointEngine = new DecisionPointEngine();
                    objDecisionPointEngine.setWebinarUserDetails(objWebinarUsersResponse);
                }

            }
            catch (WebException e)
            {
                using (var sr = new StreamReader(e.Response.GetResponseStream()))
                    Result = sr.ReadToEnd();
                return RedirectToAction("Error", "Login", new { errorMsg = Result });
            }
            return RedirectToAction("WebinarDashboard");
        }

        #endregion

        #region Webinar
        [HttpGet]
        public ActionResult WebinarDashboard()
        {
            objWebinarDashboardModel = new WebinarDashboardModel();
            objDecisionPointEngine = new DecisionPointEngine();
            objWebinarDashboardModel.WebinarUsersList = objDecisionPointEngine.GetAllCompanyAdmin();
            objWebinarDashboardModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
            objWebinarDashboardModel.pagesize = objWebinarDashboardModel.WebinarUsersList.Count();
            ViewData.Model = objWebinarDashboardModel;
            return View();
        }
        [HttpPost]
        public ActionResult WebinarDashboard(WebinarModel webinarModel)
        {

            return View();
        }
        [HttpPost]
        public JsonResult getUpcomingWebinar()
        {
            // first we need to create the uri for the web request
            string uri = String.Format("https://api.citrixonline.com/G2W/rest/organizers/{0}/upcomingWebinars",
                             OrganizerKey);

            // then the request to get upcoming webinars is created and sent
            // N.B. we need to include the access token to the headers to access
            // the user's account and data

            string output = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers = new WebHeaderCollection();
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Content-type", "application/json");
                client.Headers.Add("Authorization", string.Format("OAuth oauth_token={0}", OauthToken));

                try
                {
                    string str = client.DownloadString(uri);

                    // JavaScriptSerializer ser = new JavaScriptSerializer();
                    // var webinars = ser.Deserialize<List<ResponseGetUpcomingWebinar>>(str);

                    // the deserialized response is a list of webinars, through which we can iterate
                    // and pick and display the fields deemed as interesting

                    //var sb = new StringBuilder("Webinar Key\tSubject\tIn Session\tOrganizer Key\tStart Time\tEnd Time\n");
                    //foreach (var wb in webinars)
                    //    sb.AppendFormat(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\n",
                    //            wb.WebinarKey, wb.Subject, wb.InSession, wb.OrganizerKey, wb.Times[0].StartTime, wb.Times[0].EndTime));

                    //output = sb.ToString();
                    return Json(str, JsonRequestBehavior.AllowGet);
                }
                catch (WebException e)
                {
                    using (var sr = new StreamReader(e.Response.GetResponseStream()))
                        output = sr.ReadToEnd();
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
            };
            // return output;
        }

        [HttpGet]
        public ActionResult Invite()
        {
            objWebinarDashboardModel = new WebinarDashboardModel();
            objDecisionPointEngine = new DecisionPointEngine();
            objWebinarDashboardModel.WebinarUsersList = objDecisionPointEngine.getAllWebinarUsers(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
            objWebinarDashboardModel.UpcomingWebinarList = GetUpcomingWebinarList();
            ViewData.Model = objWebinarDashboardModel;
            return View();
        }

        [HttpPost]
        public ActionResult Invite(InvitePost str)
        {
            if (str != null)
            {
                for (int i = 0; i < str.Email.Count; i++)
                {
                    RegisterUsers(str.Email[i], str.WebinarId[i], str.Fname[i], str.Lname[i]);
                }
            }
            objWebinarDashboardModel = new WebinarDashboardModel();
            objDecisionPointEngine = new DecisionPointEngine();
            objWebinarDashboardModel.WebinarUsersList = objDecisionPointEngine.getAllWebinarUsers(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
            ViewData.Model = objWebinarDashboardModel;
            return View();
        }

        public JsonResult getRegistrants(string webinarId)
        {
            string output = string.Empty;
            // first we need to create the uri for the web request

            string uri = String.Format(@"https://api.citrixonline.com/G2W/rest/organizers/{0}/webinars/{1}/registrants",
                             OrganizerKey, webinarId);

            // then the request to get the registrants is created and sent
            // N.B. we need to include the access token to the headers to access
            // the user's account and data

            using (WebClient client = new WebClient())
            {
                client.Headers = new WebHeaderCollection();
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Content-type", "application/json");
                client.Headers.Add("Authorization", string.Format("OAuth oauth_token={0}", OauthToken));

                try
                {
                    string str = client.DownloadString(uri);
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    var registrants = ser.Deserialize<List<ResponseGetRegistrant>>(str);

                    // the deserialized response is a list of registrants, through which we can iterate
                    // and pick and display the fields deemed as interesting

                    var sb = new StringBuilder("First Name\tLast Name\tEmail\tRegistration Date\tStatus\n");
                    foreach (var rg in registrants)
                        sb.AppendFormat(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\n",
                                rg.FirstName, rg.LastName, rg.Email,
                                rg.RegistrationDate, rg.Status));

                    output = sb.ToString();
                    return Json(str, JsonRequestBehavior.AllowGet);

                }
                catch (WebException e)
                {
                    using (var sr = new StreamReader(e.Response.GetResponseStream()))
                        output = sr.ReadToEnd();
                    return Json(output, JsonRequestBehavior.AllowGet);
                }
            };
        }
        /// <summary>
        /// get upcoming webinar list
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 07 2014</CreatedDate>
        /// <returns>ResponseGetUpcomingWebinar</returns>
        public IList<ResponseGetUpcomingWebinar> GetUpcomingWebinarList()
        {
            // first we need to create the uri for the web request
            string uri = String.Format("https://api.citrixonline.com/G2W/rest/organizers/{0}/upcomingWebinars",
                             OrganizerKey);

            // then the request to get upcoming webinars is created and sent
            // N.B. we need to include the access token to the headers to access
            // the user's account and data

            //string output = string.Empty;
            using (WebClient client = new WebClient())
            {
                client.Headers = new WebHeaderCollection();
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Content-type", "application/json");
                client.Headers.Add("Authorization", string.Format("OAuth oauth_token={0}", OauthToken));
                var webinars = new List<ResponseGetUpcomingWebinar>();
                try
                {
                    string str = client.DownloadString(uri);
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    webinars = ser.Deserialize<List<ResponseGetUpcomingWebinar>>(str);

                    return webinars;
                }
                catch (WebException e)
                {
                    using (var sr = new StreamReader(e.Response.GetResponseStream()))
                       // output = sr.ReadToEnd();
                    return webinars;
                }
            };
        }
        #endregion

        #region Api Operations

        private string RegisterUsers(string email, string webinarid, string fname, string lname)
        {
            string result = string.Empty;
            // first we need to create the uri for the web request
            string uri = String.Format("https://api.citrixonline.com/G2W/rest/organizers/{0}/webinars/{1}/registrants",
                             OrganizerKey, Convert.ToString(webinarid.Trim(), CultureInfo.InvariantCulture));

            //then create and serialize the registrant object
            var registrant = new Registrant
            {
                firstName = "vikash",// Convert.ToString(fname, CultureInfo.InvariantCulture),
                lastName = "Gaurav", //Convert.ToString(lname, CultureInfo.InvariantCulture),
                email = "vikashg@yopmail.com"//Convert.ToString(email, CultureInfo.InvariantCulture)
            };

            JavaScriptSerializer ser = new JavaScriptSerializer();
            string json = ser.Serialize(registrant);

            // then the request to create a registrant is created and sent
            // N.B. we need to include the access token to the headers to access
            // the user's account and data

            using (WebClient client = new WebClient())
            {
                client.Headers = new WebHeaderCollection();
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("Content-type", "application/json");
                client.Headers.Add("Authorization", string.Format("OAuth oauth_token={0}", OauthToken));

                try
                {
                    string resp = client.UploadString(uri, "POST", json);
                    var ok = ser.Deserialize<ResponseCreateRegistrantOk>(resp);

                    // from the deserialized response we can display the fields deemed as interesting

                    var sb = new StringBuilder("Registration Success!\n");
                    sb.AppendFormat(String.Format("Registrant key: {0}\n", ok.RegistrantKey));
                    sb.AppendFormat(String.Format("Join Url: {0}\n", ok.JoinUrl));

                    result = sb.ToString();
                }
                catch (WebException e)
                {
                    //if there is an error, e.g. the registrant exists already
                    // we need an alternative deserialization

                    using (var sr = new StreamReader(e.Response.GetResponseStream()))
                    {
                        var dupe = ser.Deserialize<ResponseCreateRegistrantDuplicate>(sr.ReadToEnd());
                        var sb = new StringBuilder("Registration Error\n");
                        sb.AppendFormat(String.Format("Description: {0}\n", dupe.Description));
                        sb.AppendFormat(String.Format("Incident: {0}\n", dupe.Incident));
                        sb.AppendFormat(String.Format("Registrant key: {0}\n", dupe.RegistrantKey));
                        sb.AppendFormat(String.Format("Join Url: {0}\n", dupe.JoinUrl));

                        result = sb.ToString();
                    }
                }
            };
            return result;
        }

        private string OauthToken
        {
            get
            {
                return Session["OauthToken"] == null
                    ? string.Empty
                        : (string)Session["OauthToken"];
            }
            set
            {
                this.Session["OauthToken"] = value;
            }
        }

        private string OrganizerKey
        {
            get
            {
                return Session["OrganizerKey"] == null
                    ? string.Empty
                        : (string)Session["OrganizerKey"];
            }
            set
            {
                this.Session["OrganizerKey"] = value;
            }
        }
        #endregion

        #region DataBase Opertaions
        /// <summary>
        /// save update & delete methods of webinar user details
        /// </summary>
        /// <param name="objWebinarDashboardModel">WebinarDashboardModel</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>sep 10 2014</CreatedDate>
        /// <returns>int</returns>
        [HttpPost]
        public int SetWebinarUserDetails(WebinarDashboardModel objWebinarDashboardModel)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objWebinarUsersResponse = new WebinarUsersResponse()
                {
                    Id = objWebinarDashboardModel.Id,
                    UserName = objWebinarDashboardModel.UserName,
                    Password = objWebinarDashboardModel.Password,
                    AppKey = objWebinarDashboardModel.AppKey,
                    OrganiserId = objWebinarDashboardModel.OrganiserId,
                    UserId = objWebinarDashboardModel.UserId,
                    IsActive = objWebinarDashboardModel.IsActive,
                    CreatedBy = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                };
                return objDecisionPointEngine.setWebinarUserDetails(objWebinarUsersResponse);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                return 0;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        #endregion
    }

}
