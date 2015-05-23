// ********************************************************************************************************************************
//                                                  class:LoginController
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 17, 2013    |Arun Kumar     | This class is controller for login of individual, company and vender user
// *********************************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DecisionPoint.Models;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;

namespace DecisionPoint.Controllers
{
    /// <summary>
    /// This controller class used for login of individual, company and vender user all activity for login handled by this controller class.
    /// </summary>
    public class LoginController : Controller
    {
        #region Global Valriables
        //log declatration
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine decisionPointEngine = null;
        LoginDetailResponse loginDetailResponse = null;
        ActionResult objActionResult = null;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        #endregion

        #region User Login Section
        // GET: /Login/
        /// <summary>
        /// get:Login
        /// </summary>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>13 oct 2013</createdDate>
        ///  <modifiedby>Bobis</modifiedby>
        /// <returns>view login</returns>
        [HttpGet]
        public ActionResult Login()
        {
            //SterlingController obj = new SterlingController();
            //SterlingModel objsterlingModel = new SterlingModel();
            //obj.GoToSterlingForScreening(objsterlingModel);
            Session["InviteeCompany"] = Request.QueryString["id"];
            ActionResult result = null;
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
            {
                UserLogin userLogin = new UserLogin();
                if (Request.Cookies["DecisionPoint"] != null && Request.Cookies["DecisionPoint"].HasKeys)
                {
                    userLogin.Emailid = Request.Cookies["DecisionPoint"].Values["UserId"];
                    userLogin.Password = Request.Cookies["DecisionPoint"].Values["Password"];
                    userLogin.RememberMe = true;
                }
                decisionPointEngine = new DecisionPointEngine();
                userLogin.Announcement = decisionPointEngine.GetAnnouncement().Where(x => x.IsClose == false).ToList();
                ViewData.Model = userLogin;
                result = View();
            }
            else
            {
                Session.Abandon();
                UserLogin userLogin = new UserLogin();
                if (Request.Cookies["DecisionPoint"] != null && Request.Cookies["DecisionPoint"].HasKeys)
                {
                    userLogin.Emailid = Request.Cookies["DecisionPoint"].Values["UserId"];
                    userLogin.Password = Request.Cookies["DecisionPoint"].Values["Password"];
                    userLogin.RememberMe = true;
                }
                decisionPointEngine = new DecisionPointEngine();
                userLogin.Announcement = decisionPointEngine.GetAnnouncement().Where(x => x.IsClose == false).ToList();
                ViewData.Model = userLogin;
                ViewBag.ErrorMessage = TempData["ErrorMsg"];
                TempData["ErrorMsg"] = string.Empty;
                return View();
            }
            return result;
        }

        /// <summary>
        /// Method to handle login page [http post] this method handle [http Post] propery of Login View
        /// </summary>
        /// <param name="userLogin">UserLogin model class</param>
        /// <param name="command">string command</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>13 oct 2013</createdDate>
        ///  <modifiedby>Bobis</modifiedby>
        /// <returns>view</returns>   
        [HttpPost, ValidateInput(false)]
        public ActionResult Login(Models.UserLogin userLogin, string command)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                // entity query for fetch user's records such as User Id, user Type, User Name from database through user's userid and password 
                if (command == "Login")
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            decisionPointEngine = new DecisionPointEngine();
                            loginDetailResponse = new LoginDetailResponse();
                            // get User Details from database using login id and password
                            loginDetailResponse = decisionPointEngine.Login(userLogin.Emailid, userLogin.Password);
                            if (loginDetailResponse != null && loginDetailResponse.UserId > 0)
                            {
                                //check user is active or not??
                                if (loginDetailResponse.IsActive)
                                {
                                    //set form authontication of user
                                    if (userLogin.RememberMe)
                                    {
                                        createCookies(userLogin); //create cookies
                                    }
                                    else
                                    {
                                        removeCookies();   //remove cookies
                                    }

                                    createSession(loginDetailResponse);  //create session

                                    if (!loginDetailResponse.IsPayment)
                                    {
                                        return loginOnPaymentNotDone();
                                    }
                                    else if (loginDetailResponse.IsPayment)
                                    {
                                        //Need to confirm that code for individual and company
                                        if (loginDetailResponse.UserType == Shared.SuperAdmin)
                                        {
                                            Session["logopath"] = GetCompanylogo("none");
                                            Session["IsSuperAdmin"] = "Yes";
                                            //return RedirectToAction("GetCompanies", "CompanyDashBoard", new { id = "1", search = string.Empty });
                                            return RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date });
                                        }
                                        else if (loginDetailResponse.UserType == Shared.NonClient)
                                        {
                                            return loginOnPaymentDone(loginDetailResponse);
                                        }
                                        else
                                        {
                                            var isInvoice = decisionPointEngine.getPaymentAmount(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)).Select(x => x.IsInvoice).FirstOrDefault(); //get is invoice is true or false

                                           // if (!Convert.ToBoolean(isInvoice))
                                           // {
                                             //   return checkRecurring(loginDetailResponse);
                                           // }
                                           // else
                                            //{
                                                return loginOnPaymentDone(loginDetailResponse);
                                           // }
                                        }
                                    }
                                    else
                                    {
                                        //message will show in login screen while user name or password will incorrect
                                        ViewBag.ErrorMessage = DecisionPointR.IncorrectPswd; // "The user name or password provided is incorrect";
                                    }
                                }
                                else
                                {
                                    ViewBag.ErrorMessage = Shared.SystemDown;// show system down message, if user is inactive 
                                }
                            }
                            else
                            {
                                //message will show in login screen while user name or password will incorrect
                                ViewBag.ErrorMessage = DecisionPointR.InvalidUserNamePassword;  // "The user name or password provided is incorrect";
                            }
                            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.ToString(), ex);
                            throw;
                        }

                    }
                    decisionPointEngine = new DecisionPointEngine();
                    userLogin.Announcement = decisionPointEngine.GetAnnouncement().ToList();
                    return View(userLogin);
                }
                else
                {
                    return View(userLogin);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }

        #endregion

        #region Change & forgot password

        /// <summary>
        /// get: Loads change password page
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>13 oct 2013</createdDate>
        /// <returns>view change password page</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            Models.UserLogin userLogin = new UserLogin();
            BusinessCryptography businessCryptography = new BusinessCryptography();
            decisionPointEngine = new DecisionPointEngine();
            if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
            {
                IList<AdminProfileResponse> admin = decisionPointEngine.GetAdminProfile(Convert.ToInt32(Session["UserId"])).ToList();
                if (admin != null)
                {
                    if (!Convert.ToBoolean(Session["IsTemp"]))
                    {
                        userLogin.NewPassword = businessCryptography.base64Decode2(admin[0].Password);
                        userLogin.ConfirmPassword = businessCryptography.base64Decode2(admin[0].Password);
                    }
                    else
                    {
                        userLogin.NewPassword = string.Empty;
                        userLogin.ConfirmPassword = string.Empty;
                    }
                }
                userLogin.Email = Convert.ToString(Session["Emailid"]);
                userLogin.PaymentType = Request["Res"];
                userLogin.RedirectType = Request["Type"];
                ViewData.Model = userLogin;
                return View();
            }
            else
            {
                userLogin.Email = "";
                ViewData.Model = userLogin;
                return View();
                // return RedirectToAction("Login");
            }
        }
        /// <summary>
        /// post:change password
        /// </summary>
        /// <param name="userLogin">UserLogin mnodel class</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>15 oct 2013</createdDate>
        /// <returns>view of change password</returns>
        [HttpPost]
        public ActionResult ChangePassword(Models.UserLogin userLogin)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            int UserId = 0;

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    UserId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                ChangePasswordRequest changePasswordRequest = new ChangePasswordRequest();
                changePasswordRequest.UserId = UserId;
                changePasswordRequest.email = userLogin.Email;
                changePasswordRequest.OldPassword = userLogin.OldPassword;
                changePasswordRequest.Password = userLogin.NewPassword;
                //Called method to change password
                var payType = userLogin.PaymentType;
                int result = decisionPointEngine.ChangePassword(changePasswordRequest);
                var returnType = Session["UserType"];
                //get config setting of particular company
                // objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                //  objConfiguratonSettingRequest = decisionPointEngine.GetConfigSetting(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                if (result > 0)
                {
                    Session["IsTemp"] = "false";

                    if (Convert.ToString(Session["UserType"]) == Shared.Individual)
                    {
                        returnType = "SIC";
                        objActionResult = RedirectToAction("ViewService", "CompanyDashBoard", new { @type = returnType });
                    }
                    //if (Convert.ToString(Session["UserType"]) == Shared.IC)
                    //{
                    //    returnType = Shared.IC;
                    //    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                    //    result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.IC, Convert.ToString(Session["CompanyId"]));
                    //    return RedirectToAction("Welcome", "Company");
                    //}
                    //if (Convert.ToString(Session["UserType"]) == Shared.NonClient)
                    //{
                    //    returnType = Shared.NonClient;
                    //    return RedirectToAction("LisenceAggrement", "Company", new { @type = returnType });
                    //}
                    else
                    {
                        objActionResult = RedirectToAction("ChangePassword", "Login", new { @type = returnType, @Res = payType });
                    }
                }
                else
                {
                    if (!Convert.ToBoolean(Session["IsTemp"]))
                    {
                        if (Convert.ToString(Session["UserType"]) == Shared.Individual)
                        {
                            returnType = "SIC";
                            objActionResult = RedirectToAction("ViewService", "CompanyDashBoard", new { @type = returnType });

                        }
                        //if (Convert.ToString(Session["UserType"]) == Shared.IC)
                        //{
                        //    returnType = Shared.IC;
                        //    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                        //    result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.NonClient, Convert.ToString(Session["CompanyId"]));
                        //    return RedirectToAction("Welcome", "Company");

                        //}
                        //if (Convert.ToString(Session["UserType"]) == Shared.NonClient)
                        //{
                        //    returnType = Shared.NonClient;
                        //    return RedirectToAction("LisenceAggrement", "Company", new { @type = returnType });
                        //}

                    }
                    else
                    {
                        if (Convert.ToString(Session["UserType"]) == "Individual")
                        {
                            returnType = "SIC";
                        }
                        //if (Convert.ToString(Session["UserType"]) == "IC")
                        //{
                        //    returnType = "IC";
                        //}
                        //if (Convert.ToString(Session["UserType"]) == Shared.NonClient)
                        //{
                        //    returnType = Shared.NonClient;
                        //}
                        TempData["ErrorMessage"] = "New Password is same as old password";
                        objActionResult = RedirectToAction("ChangePassword", "Login", new { @type = returnType, @Res = payType });
                    }


                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objActionResult;
        }


        /// <summary>
        /// get:forget password
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>16 oct 2013</createdDate>
        /// <returns>view forget password</returns>
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// post:forget password
        /// </summary>
        /// <param name="objForgetPassword">ForgetPassword class</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>17 oct 2013</createdDate>
        /// <returns>view of ForgetPassword</returns>
        [HttpPost]
        public ActionResult ForgetPassword(Models.ForgetPassword objForgetPassword)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                //Called method to change password
                string result = decisionPointEngine.CheckEmail(objForgetPassword.Emailid);

                if (!string.IsNullOrEmpty(result))
                {
                    // code to send email
                    decisionPointEngine.Getsmtpdetails(objForgetPassword.Emailid, result);
                    ViewBag.SucessMessage = DecisionPointR.PasswordSent; // "Password Sent To Your Email.";
                }
                else
                {
                    ViewBag.ErrorMessage = DecisionPointR.UserExists;  // "User with this Email not exists";
                }
                return View();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }
        #endregion

        #region Logout
        /// <summary>
        /// logut
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>13 oct 2013</createdDate>
        /// <returns>logout action</returns>
        public ActionResult LogOut()
        {
            if (Request.Cookies["documentid"] != null)
            {
                Response.Cookies["documentid"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Abandon();
            return RedirectToAction("Login");
        }
        #endregion

        #region Miscellenious

        /// <summary>
        /// CheckPasswordLogin
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <modifiedby>bobis</modifiedby>
        ///  <createdDate>13 mar 2013</createdDate>
        /// <returns>action</returns>
        [HttpGet]
        public ActionResult CheckPasswordLogin()
        {
            decisionPointEngine = new DecisionPointEngine();
            Session["InviteeCompany"] = Request.QueryString["id"];
            ActionResult result = null;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    UserLogin userLogin = new UserLogin();
                    if (Request.Cookies["DecisionPoint"] != null && Request.Cookies["DecisionPoint"].HasKeys)
                    {
                        userLogin.Emailid = Request.Cookies["DecisionPoint"].Values["UserId"];
                        userLogin.Password = Request.Cookies["DecisionPoint"].Values["Password"];
                        userLogin.RememberMe = true;
                    }

                    userLogin.Announcement = decisionPointEngine.GetAnnouncement().ToList();
                    ViewData.Model = userLogin;
                    result = View("Login");
                }
                else
                {
                    //if (Convert.ToString(Session["IsTemp"]) == "True")
                    //{
                    //    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == "IC")
                    //    {
                    //        result = RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC"});
                    //    }
                    //    //else
                    //    //{
                    //    //    return RedirectToAction("ChangePassword", "Login");
                    //    //}
                    //}
                    //else 
                    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == "Individual")
                    {
                        result = RedirectToAction("DocumentAction", "UserDashBoard", new { id = "All" });
                    }
                    //else if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == "IC")
                    //{
                    //    result = RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC"});//RedirectToAction("ChangePassword", "Login", new { Type = "IC" });
                    //}
                    // If user type is idividual then page will redirect to individual dashboard
                    else if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == "Company")
                    {
                        // objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                        // objConfiguratonSettingRequest = decisionPointEngine.GetConfigSetting(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                        // if (!object.Equals(objConfiguratonSettingRequest, null))
                        // {
                        //  if (objConfiguratonSettingRequest.IsCoveragearea)
                        //   {
                        //      result = RedirectToAction("CreateServices", "Company", new { Type = "V" });
                        //   }
                        //  else
                        //  {
                        result = RedirectToAction("ViewService", "CompanyDashBoard", new { Type = "V" });
                        //  }
                        //}
                        // else
                        //{
                        // result = RedirectToAction("ViewService", "CompanyDashBoard", new { Type = "V" });
                        //    result = RedirectToAction("CreateServices", "Company", new { Type = "V" }); 
                        //}
                        // result = RedirectToAction("CreateServices", "Company", new { Type = "V" });
                    }
                    else if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == "SuperAdmin")
                    {
                        result = RedirectToAction("DocumentAction", "CompanyDashBoard", new { id = "All" });
                    }
                }
                
               // SetTempPay();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return result;
        }

        ///// <summary>
        ///// temp pay for testing purpose before implementation of payment gateway to complete regsitration steps.
        ///// </summary>
        ///// <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 nov 2013</createdDate>
        ///// <returns>payment sucess or not?</returns>
        //[HttpGet]
        //public JsonResult SetTempPay()
        //{

        //    int result = 0;
        //    logMessage = new StringBuilder();
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        decisionPointEngine = new DecisionPointEngine();
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            int ParentUserId = 0;
        //            if (object.Equals(Session["InviteeCompany"], null))
        //            {
        //                ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
        //                result = decisionPointEngine.SetTempPay(Convert.ToInt32(Session["UserId"]), ParentUserId);
        //            }
        //            else
        //            {
        //                result = decisionPointEngine.SetTempPay(Convert.ToInt32(Session["UserId"]), Convert.ToInt32(Session["InviteeCompany"]));
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// get company logo
        /// </summary>
        /// <param name="type">string type</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>path of company logo</returns>
        public string GetCompanylogo(string type)
        {
            StringBuilder logMessage = new StringBuilder();
            string Companylogo = string.Empty;
            int UserId = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                decisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (type.Equals("staff"))
                    {
                        UserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        UserId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    }

                    //Called method for get My profile details of Individual and send it to view property for UI
                    UserDashBoardResponse objUserDashBoardResponse = new UserDashBoardResponse();
                    objUserDashBoardResponse = decisionPointEngine.GetAccountDetails(UserId);
                    if (!objUserDashBoardResponse.Equals(null))
                    {
                        if (!string.IsNullOrEmpty(objUserDashBoardResponse.companylogo))
                        {
                            Companylogo = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + objUserDashBoardResponse.companylogo;
                        }
                        else { Companylogo = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + "no-img-icon.jpg"; }
                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Companylogo;
        }
        #endregion

        #region Payment Section

        /// <summary>
        /// to load Company payemnt page
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>17 july 2014</createdDate>
        /// <returns>return to payment page</returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CompanyPayment()
        {
            logMessage = new StringBuilder();
            Payment payment = new Payment();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                payment.StateList = decisionPointEngine.GetStateList().ToList();
                payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                payment.BusinessName = Convert.ToString(Session["BusinessName"], CultureInfo.InvariantCulture);
                payment.NoOfIc = decisionPointEngine.GetAllWePayIc(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)).Count();
                payment.NoOfFieldStaff = decisionPointEngine.GetAllBusinessPartners(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)).Count();
                payment.NoOfOfficeStaff = decisionPointEngine.GetAllStaff(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)).Count();
                if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.Company))
                {
                    payment.ParentUserId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    payment.ParentUserType = Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture);
                }
                else
                {
                    payment.ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                    payment.ParentUserType = decisionPointEngine.GetParentUserType(payment.ParentUserId);
                }

                payment.RegPaymentId = decisionPointEngine.IsRegistrationPaymentDone(Convert.ToString(payment.ParentUserId, CultureInfo.InvariantCulture), Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                payment.isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(payment.ParentUserId, "Monthly Plan");
                payment.isAnnualPaymentDone = decisionPointEngine.IsRecurringPaymentDone(payment.ParentUserId, "Annual Plan");
                payment.RootUrl = ViewModel.GetSiteRoot();
                payment.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];

            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View(payment);

        }

        /// <summary>
        /// make payment
        /// </summary>
        /// <param name="payment">paramters for payment gateway</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>17 July 2014</createdDate>
        /// <returns>return with payemnt success or fail message</returns>
        [HttpPost]
        public ActionResult CompanyPayment(Payment payment)
        {
            // write code for payment gateway and change status of database
            decisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (ModelState.IsValid)
                {

                    #region Parameters
                    PaymentResponse paymentResponse = new PaymentResponse
                    {
                        Amount = payment.Amount * 100,
                        TransactionType = payment.TransactionType,
                        ExpiryMonth = payment.ExpiryMonth,
                        ExpiryYear = payment.ExpiryYear,
                        CardNumber = payment.CardNumber,
                        CreditCardType = payment.CreditCardType,
                        NameOnCard = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        Street = payment.Street,
                        StreetNumber = payment.StreetNumber,
                        Directions = payment.Directions,
                        Zip = payment.Zip,
                        CVC = payment.CVC,
                        CompanyFee = payment.CompanyFee * 100,
                        CustomerEmail = Convert.ToString(Session["Emailid"]),
                        UserId = Convert.ToInt32(Session["UserId"]),
                        isMonthlyPaymentDone = payment.isMonthlyPaymentDone,
                        isAnnualPaymentDone = payment.isAnnualPaymentDone,
                    };
                    PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                    {
                        CompanyCode = payment.CompanyCode,
                        CompanyFee = payment.CompanyFee,
                        PerFieldStaffFee = payment.PerFieldStaffFee,
                        PerOfficeStaffFee = payment.PerOfficeStaffFee,
                        PerIcFee = payment.IcFee,
                        IsInvoice = payment.IsInvoice,
                        NoOfPartners = payment.NoOfFieldStaff,
                        NoOfStaff = payment.NoOfOfficeStaff,
                        NoOfIc = payment.NoOfIc,
                        BusinessName = payment.BusinessName,
                        TransactionType = payment.BusinessName,
                        userId = Convert.ToInt32(Session["UserId"]),
                        InviteeCompanyId = payment.ParentUserId
                    };
                    #endregion
                    int result = 0;
                    if (payment.RegPaymentId > 0)
                    {
                        result = decisionPointEngine.UpdateRecurringPayment(paymentResponse, paymentAmountResponse);
                    }
                    else
                    {
                        result = decisionPointEngine.companyPayment(paymentResponse, paymentAmountResponse);
                    }
                    if (result > 0)
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        #region Send Mail

                        string email = Convert.ToString(Session["Emailid"]);
                        string text = "<div style='line-height:25px'>Dear " + payment.BusinessName + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + payment.Amount + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";
                        string subject = "Compliance Tracker Payment Success";
                        decisionPointEngine.GetSmtpDetail(email, text, subject);

                        #endregion
                        return RedirectToAction("Accountprofile", "CompanyDashBoard");
                    }
                    else
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                    }
                }
                else
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    return View(payment);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                TempData["ErrorMessage"] = ex.Message;

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            payment.StateList = decisionPointEngine.GetStateList().ToList();
            payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
            return View(payment);


        }

        /// <summary>
        /// IC payment page for failure case
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>21 mjuly 2014</createdDate>
        /// <returns>returns view with model data</returns>
        [HttpGet]
        //  [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult IcPayment()
        {
            logMessage = new StringBuilder();

            Payment payment = new Payment();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();


                payment.StateList = decisionPointEngine.GetStateList().ToList();
                payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                payment.BusinessName = Convert.ToString(Session["BusinessName"]);
                int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                payment.ParentUserType = decisionPointEngine.GetParentUserType(ParentUserId);

                payment.RegPaymentId = decisionPointEngine.IsRegistrationPaymentDone(Convert.ToString(ParentUserId, CultureInfo.InvariantCulture), Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                payment.isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(ParentUserId, "Monthly Plan");
                payment.RootUrl = ViewModel.GetSiteRoot();
                payment.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View(payment);
        }

        /// <summary>
        /// make payment
        /// </summary>
        /// <param name="payment">paramters for payment gateway</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>return with payemnt success or fail message</returns>
        [HttpPost]
        public ActionResult MakeIcPayment(Payment payment)
        {

            // var tbl = ConfigurationManager.AppSettings["StripeKey"].ToString();  
            // write code for payment gateway and change status of database
            decisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (ModelState.IsValid)
                {
                    #region Parameters
                    PaymentResponse paymentResponse = new PaymentResponse
                    {
                        Amount = payment.Amount * 100,
                        TransactionType = payment.TransactionType,
                        ExpiryMonth = payment.ExpiryMonth,
                        ExpiryYear = payment.ExpiryYear,
                        CardNumber = payment.CardNumber,
                        CreditCardType = payment.CreditCardType,
                        NameOnCard = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        Street = payment.Street,
                        StreetNumber = payment.StreetNumber,
                        Directions = payment.Directions,
                        Zip = payment.Zip,
                        CVC = payment.CVC,
                        CompanyFee = payment.CompanyFee * 100,
                        CustomerEmail = Convert.ToString(Session["Emailid"]),
                        UserId = Convert.ToInt32(Session["UserId"]),
                        isMonthlyPaymentDone = payment.isMonthlyPaymentDone,
                        isAnnualPaymentDone = payment.isAnnualPaymentDone,
                    };
                    PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                    {
                        CompanyCode = payment.CompanyCode,
                        CompanyFee = payment.CompanyFee,
                        PerFieldStaffFee = payment.PerFieldStaffFee,
                        PerOfficeStaffFee = payment.PerOfficeStaffFee,
                        PerIcFee = payment.IcFee,
                        IsInvoice = payment.IsInvoice,
                        NoOfPartners = payment.NoOfFieldStaff,
                        NoOfStaff = payment.NoOfOfficeStaff,
                        NoOfIc = payment.NoOfIc,
                        CardType = payment.CreditCardType,
                        CardHolderName = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        TransactionType = payment.BusinessName,
                        userId = Convert.ToInt32(Session["UserId"]),
                        InviteeCompanyId = payment.ParentUserId,
                    };
                    #endregion
                    int result = 0;
                    if (payment.RegPaymentId > 0)
                    {
                        result = decisionPointEngine.UpdateRecurringPayment(paymentResponse, paymentAmountResponse);
                    }
                    else
                    {
                        result = decisionPointEngine.companyPayment(paymentResponse, paymentAmountResponse);
                    }
                    if (result > 0)
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        #region Send Mail

                        string email = Convert.ToString(Session["Emailid"]);
                        string text = "<div style='line-height:25px'>Dear " + payment.BusinessName + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + payment.Amount + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";

                        string subject = "Compliance Tracker Payment Success";
                        decisionPointEngine.GetSmtpDetail(email, text, subject);

                        #endregion
                        return RedirectToAction("ICProfile", "UserDashBoard");
                    }
                    else
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = "Payment Not Done";
                    }
                }
                else
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    return View(payment);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                TempData["ErrorMessage"] = ex.Message;

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            payment.StateList = decisionPointEngine.GetStateList().ToList();
            payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
            return View(payment);


        }
        #endregion

        #region functions

        /// <summary>
        /// create cookies while login
        /// </summary>
        /// <param name="userLogin">UserLogin</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate>
        [NonAction]
        private void createCookies(Models.UserLogin userLogin)
        {
            #region Create Cookies
            HttpCookie myCookie = new HttpCookie("DecisionPoint");
            myCookie["UserId"] = userLogin.Emailid;
            myCookie["Password"] = userLogin.Password;
            myCookie.Expires = DateTime.Now.AddDays(365);
            HttpContext.Response.Cookies.Add(myCookie);
            #endregion
        }

        /// <summary>
        /// remove cookies
        /// </summary>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate>
        [NonAction]
        private void removeCookies()
        {
            #region Remove Cookies
            if (Request.Cookies["DecisionPoint"] != null)
            {
                HttpCookie myCookie = new HttpCookie("DecisionPoint");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            #endregion
        }

        /// <summary>
        /// create and set session on login
        /// </summary>
        /// <param name="loginDetailResponse">LoginDetailResponse</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate>
        [NonAction]
        private void createSession(LoginDetailResponse loginDetailResponse)
        {
            #region Create Session
            //unique id of user for whole application
            HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
            Session["UserId"] = loginDetailResponse.UserId;
            Session["BusinessName"] = loginDetailResponse.BusinessName;
            Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
            Session["UserType"] = loginDetailResponse.UserType;
            Session["CompanyId"] = loginDetailResponse.CompanyId;
            Session["IsTemp"] = loginDetailResponse.IsTemp;
            Session["superAdmin"] = loginDetailResponse.UserId;
            Session["superAdminCompanyId"] = loginDetailResponse.CompanyId;
            Session["IsRegistered"] = loginDetailResponse.IsRegistered;


            #endregion
        }

        /// <summary>
        /// login cases On Payment Not Done
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate> 
        [NonAction]
        private ActionResult loginOnPaymentNotDone()
        {
            #region Payment Not Done

            Session["logopath"] = GetCompanylogo("none");
            Session["IsSuperAdmin"] = "No";
            if (loginDetailResponse.UserType == Shared.IC)
            {

                decisionPointEngine = new DecisionPointEngine();
                int result = 0;
                if (object.Equals(Session["InviteeCompany"], null))
                {
                    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                    Session["InviteeCompany"] = ParentUserId;
                    result = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(ParentUserId));
                }
                else
                {
                    result = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(Session["InviteeCompany"]));
                }
                if (result == 1)
                {
                    objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = Shared.IC, Res = "1" });//RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC", Res = "1" });
                }
                else if (result == 2)
                {
                    objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = Shared.IC, Res = "2" });//RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC", Res = "2" });
                }
                else if (result == 3)
                {
                    objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = Shared.IC, Res = "3" });
                }

            }
            else if (loginDetailResponse.UserType == Shared.NonClient)
            {
                objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type =Shared.NonClient });//RedirectToAction("SetupCompnayProfile", "Company", new { Type = Shared.NonClient });
            }
            else
            {
                int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                Session["InviteeCompany"] = ParentUserId;
                objActionResult = RedirectToAction("WelcomeInDP", "Company");//RedirectToAction("SetupCompnayProfile", "Company");
            }
            return objActionResult;
            #endregion
        }

        /// <summary>
        /// login cases On Payment  Done
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate>  
        [NonAction]
        private ActionResult loginOnPaymentDone(LoginDetailResponse loginDoetailResponse)
        {
            decisionPointEngine = new DecisionPointEngine();
            #region Payment Done

            if (loginDetailResponse.UserType == "Individual")
            {
                #region Individual

                Session["logopath"] = GetCompanylogo("staff");
                Session["IsSuperAdmin"] = "No";
                if (Convert.ToBoolean(Session["IsRegistered"]) == false)
                {

                    objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = "SIC" });//RedirectToAction("ChangePassword", "Login", new { Type = "SIC" });
                }
                else
                {
                    //if (loginDetailResponse.permission == "Compliance Manager" || loginDetailResponse.permission == "Business Manager" || loginDetailResponse.permission == "Admin")
                    //{
                    objActionResult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date });
                    //objActionResult = RedirectToAction("DocumentAction", "CompanyDashBoard", new { id = "All" });
                    //}
                    //else if (loginDetailResponse.permission == "User")
                    //{
                    //    objActionResult = RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date });
                    //   // objActionResult = RedirectToAction("DocumentAction", "UserDashBoard", new { id = "All" });
                    //}
                }
                #endregion
            }
            else if (loginDetailResponse.UserType == "IC")
            {
                #region IC

                Session["logopath"] = GetCompanylogo("none");
                Session["IsSuperAdmin"] = "No";
                if (Convert.ToBoolean(Session["IsRegistered"]) == false)
                {

                    if (Convert.ToBoolean(Session["IsRegistered"]) == false)
                    {

                        int result = 0;
                        if (object.Equals(Session["InviteeCompany"], null))
                        {
                            int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                            result = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(ParentUserId));
                        }
                        else
                        {
                            result = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(Session["InviteeCompany"]));
                        }
                        objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = "IC", Res = Convert.ToString(result, CultureInfo.InvariantCulture) });//RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC", Res =Convert.ToString(result,CultureInfo.InvariantCulture) });
                        //if (result == 1)
                        //{
                        //    objActionResult = RedirectToAction("SetupCompnayProfile", "Company", new { Type = "IC", Res = "1" });
                        //}
                        //else if (result == 2)
                        //{
                        //    objActionResult = RedirectToAction("ChangePassword", "Login", new { Type = "IC", Res = "2" });
                        //}
                    }
                    else
                    {
                        // objActionResult = RedirectToAction("DocumentAction", "UserDashBoard", new { id = "All" });
                        objActionResult = RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date });
                    }

                }
                else
                {
                    objActionResult = RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date });
                    // objActionResult = RedirectToAction("DocumentAction", "UserDashBoard", new { id = "All" });
                }
                #endregion
            }
            // If user type is idividual then page will redirect to individual dashboard
            else if (loginDetailResponse.UserType == "Company")
            {
                #region Company

                Session["logopath"] = GetCompanylogo("none");
                if (Convert.ToBoolean(Session["IsRegistered"]) == true)
                {
                    Session["IsSuperAdmin"] = "No";
                    Session["IsAdmin"] = "Yes";
                    objActionResult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date });
                }
                else
                {
                    objActionResult = RedirectToAction("ViewService", "CompanyDashBoard", new { Type = "V" });

                }
                #endregion
            }
            else if (loginDetailResponse.UserType == Shared.NonClient)
            {
                #region Non Client

                Session["logopath"] = GetCompanylogo("none");
                if (Convert.ToBoolean(Session["IsRegistered"]) == true)
                {
                    Session["IsSuperAdmin"] = "No";
                    objActionResult = RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date });
                }
                else
                {
                    objActionResult = RedirectToAction("WelcomeInDP", "Company", new { Type = Shared.NonClient });//RedirectToAction("SetupCompnayProfile", "Company", new { Type = Shared.NonClient});

                }
                #endregion
            }
            return objActionResult;
            #endregion
        }

        /// <summary>
        /// check recurring payments
        /// </summary>
        /// <param name="loginDetailResponse">LoginDetailResponse</param>
        /// <returns>ActionResult</returns>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 22 2014</createddate> 
        [NonAction]
        private ActionResult checkRecurring(LoginDetailResponse loginDetailResponse)
        {
            #region GetRecurringDetails
            int parentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture) + Shared.Comma + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture), Shared.Staff);
            //int parentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), loginDetailResponse.UserType); //get parent company User Id
            //get parent user type
            string ParentUserType = decisionPointEngine.GetParentUserType(parentUserId);
            bool isMonthlyPaymentDone = false;
            bool isAnnualPaymentDone = false;
            if (loginDetailResponse.UserType == "Individual")
            {
                isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(parentUserId, "Monthly Plan");
                isAnnualPaymentDone = decisionPointEngine.IsRecurringPaymentDone(parentUserId, "Annual Plan");
            }
            if (loginDetailResponse.UserType == "IC")
            {
                int PayType = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(parentUserId));
                if (PayType == 1)
                {
                    isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(parentUserId, "Monthly Plan");
                    isAnnualPaymentDone = decisionPointEngine.IsRecurringPaymentDone(parentUserId, "Annual Plan");
                }
                if (PayType == 2)
                {
                    isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), "Monthly Plan");
                    isAnnualPaymentDone = decisionPointEngine.IsRecurringPaymentDone(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), "Annual Plan");
                }
            }
            if (loginDetailResponse.UserType == "Company")
            {
                isMonthlyPaymentDone = decisionPointEngine.IsRecurringPaymentDone(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), "Monthly Plan");
                isAnnualPaymentDone = decisionPointEngine.IsRecurringPaymentDone(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), "Annual Plan");
            }
            #endregion
            if ((ParentUserType == "SuperAdmin" && loginDetailResponse.UserType == "Individual") || (isMonthlyPaymentDone && isAnnualPaymentDone) || (ParentUserType == "SuperAdmin" && loginDetailResponse.UserType == "IC"))
            {
                objActionResult = loginOnPaymentDone(loginDetailResponse);
                //if (loginDetailResponse.UserType == "IC")
                //{
                //    objActionResult = loginOnPaymentDone(loginDetailResponse);
                //    //int PayType = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(parentUserId));
                //    //if (PayType == 1)
                //    //{
                //    //    objActionResult = loginOnPaymentDone(loginDetailResponse);
                //    //}
                //    //if (PayType == 2)
                //    //{
                //    //    objActionResult = RedirectToAction("IcPayment", "Login");
                //    //}
                //}
                //else
                //{
                //    objActionResult = loginOnPaymentDone(loginDetailResponse);
                //}
            }
            else
            {
                #region Redirect If cases for Recurring Not Done
                //redirect to payment page with condition to check user type.
                if (loginDetailResponse.UserType == "Company")
                {
                    objActionResult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date });
                    //objActionResult = RedirectToAction("CompanyPayment", "Login");
                }
                if (loginDetailResponse.UserType == "Individual")
                {
                    TempData["ErrorMsg"] = Shared.SystemDown;
                    objActionResult = RedirectToAction("login", "login");
                }
                if (loginDetailResponse.UserType == "IC")
                {
                    int PayType = decisionPointEngine.GetIcPaymentType(Convert.ToInt32(Session["UserId"]), Convert.ToString(parentUserId));
                    if (PayType == 1)
                    {
                        TempData["ErrorMsg"] = Shared.SystemDown;
                        objActionResult = RedirectToAction("login", "login");
                    }
                    if (PayType == 2)
                    {
                      objActionResult=  RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date });
                        //objActionResult = RedirectToAction("IcPayment", "Login");
                    }
                }
                #endregion
            }
            return objActionResult;
        }
        #endregion

        #region Error
        /// <summary>
        /// Used for rendered the error page for User 
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>30 Aug 2014</createddate>
        [HttpGet]
        public ActionResult Error(string errorMsg)
        {
            Error objError = null;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objError = new Error();
                //set the error message
                objError.ErrorMsg = errorMsg;
                ViewData.Model = objError;
                return View();
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objError = new Error();
                objError.ErrorMsg = DecisionPointR.Errorlbl;
                ViewData.Model = objError;
                return View();
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }
        #endregion
    }
}





