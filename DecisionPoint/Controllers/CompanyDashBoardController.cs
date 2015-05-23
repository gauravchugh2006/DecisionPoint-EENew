using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DecisionPoint.Controllers.WebApi;
using DecisionPoint.Models;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using RusticiSoftware.HostedEngine.Client;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;
using DecisionPoint.Models.DashBoardViewModel.CompanyDashBoardViewModel;
using CsvHelper;

namespace DecisionPoint.Controllers
{
    public class CompanyDashBoardController : Controller
    {
        #region GlobalVariable
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        CompanyDashBoard objComapnyDashBoard = null;
        int userId = 0;
        int modifiedBy = 0;
        string companyId = string.Empty;
        UserDashBoard objUserDashBoard = null;
        ActionResult objactionresult = null;
        UserDashBoardResponse objUserDashBoardResponse = null;
        JavaScriptSerializer javaScriptSerializer = null;
        InviteStaffModel objInviteStaffModel = null;
        UserDashBoardRequest objUserDashBoardRequest = null;
        ServiceTranslationTableRequest objServiceTranslationTableRequest = null;
        EmpReq objEmpReq = null;
        SendInvitation objSendInvitation = null;
        FilterRequest filterRequest = null;
        SubmitReqDocRequest objSubmitReqDocRequest = null;
        SuperAdminDashBoard objSuperAdminDashBoard = null;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        MyDashBoard objMyDashBoard = null;
        LicenseModel objLicenseModel = null;
        LicenseInsuranceRequest objLicenseInsuranceRequest = null;
        ViewModel objViewModel = null;
        SuperAdminViewModel objSuperAdminViewModel = null;
        IList<string> sessionList = null;
        InviteVendorBulk inviteVendorModel = null;
        CsvReader objCSVReader = null;
        PermissionTable objPermissionTable = null;
        PermissionTableRequest objPermissionTableRequest = null;
        JobComplianceRequirementModel objJobComplianceRequirementModel = null;
        bool col1 = false;
        bool col2 = false;
        bool col3 = false;
        bool col4 = false;
        List<string> serach1 = null;
        List<string> serach2 = null;
        List<string> serachFinal = null;
        #endregion

        #region MyMessages/Document/Courses/internalStaff


        /// <summary>
        /// GET: UserDashBorad/HistoryAction/
        /// </summary>
        /// <param name="id">string id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 3 2013</createddate>
        /// <returns> HistoryAction view</returns>
        [HttpGet]
        public ActionResult HistoryAction(string id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    //get communication Details of particular login user
                    objUserDashBoard = objViewModel.GetHistoryCommunicationDetails(id, Shared.Company);
                    //assign the view model
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("_History", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }

            }

            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }

            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }
        /// <summary>
        /// GET: UserDashBorad/GlobalLibrary/
        /// </summary>
        /// <param name="">string id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>may 10 2014</createddate>
        /// <returns> GlobalLibrary view</returns>
        [HttpGet]
        public ActionResult GlobalLibrary(string id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);

                    objUserDashBoard.HistoryDetails = objDecisionPointEngine.GetGlobalLibrary(companyId, id);

                    if (objUserDashBoard.HistoryDetails != null)
                    {
                        objUserDashBoard.PageSize = objUserDashBoard.HistoryDetails.Count();
                    }

                    objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objUserDashBoard.GroupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), companyId);
                    //set confgi setting
                    ConfiguratonSettingRequest objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        objUserDashBoard.IsICApply = objConfiguratonSettingRequest.IsIc;
                        objUserDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                    }
                    //assign the view model
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("GlobalLibrary", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }

        /// <summary>
        /// Used for Set the sesstion in case of impersination 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <createdby>Bobi</createdby>
        /// <createDate>30 Aug 2014</createDate>
        [NonAction]
        public ActionResult BackToDashboard(string date, int type)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //set session values when admin come from staff view

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture)))
                {
                    if (!type.Equals(1))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture)))
                        {
                            string parentuserid = string.Empty;
                            LoginDetailResponse loginDetailResponse = new LoginDetailResponse();

                            sessionList = new List<string>();
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                            {
                                sessionList = Convert.ToString(Session["parentUserid"]).Split(',').ToList();
                            }
                            if (!object.Equals(sessionList, null))
                            {
                                if (sessionList.Count <= 1)
                                {
                                    parentuserid = sessionList[sessionList.Count - 1];
                                    loginDetailResponse = SetSession(parentuserid);
                                    Session["parentUserid"] = null;
                                }
                                else
                                {
                                    parentuserid = sessionList[sessionList.Count - 2];
                                    sessionList.RemoveAt(sessionList.Count - 1);
                                    if (sessionList.Count <= 1)
                                    {
                                        Session["parentUserid"] = null;
                                    }
                                    loginDetailResponse = SetSession(parentuserid);
                                }
                            }
                            else
                            {
                                loginDetailResponse = SetSession(Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture));
                            }

                            if (loginDetailResponse != null)
                            {

                                //  Session["parentUserid"] = sessionlist;
                                HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                                Session["UserId"] = loginDetailResponse.UserId;
                                Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
                                Session["BusinessName"] = loginDetailResponse.BusinessName;
                                Session["UserType"] = loginDetailResponse.UserType;
                                Session["CompanyId"] = loginDetailResponse.CompanyId;
                                Session["IsTemp"] = loginDetailResponse.IsTemp;
                            }
                            Session["logopath"] = GetCompanylogo("none");
                            Expiredocsession();
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                        {
                            Session["parentUserid"] = null;
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["superAdmin"])))
                            {
                                LoginDetailResponse loginDetailResponse = new LoginDetailResponse();

                                loginDetailResponse = SetSession(Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture));

                                if (loginDetailResponse != null)
                                {
                                    HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                                    Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
                                    Session["UserId"] = loginDetailResponse.UserId;
                                    Session["BusinessName"] = loginDetailResponse.BusinessName;
                                    Session["UserType"] = loginDetailResponse.UserType;
                                    Session["CompanyId"] = loginDetailResponse.CompanyId;
                                    Session["IsTemp"] = loginDetailResponse.IsTemp;
                                }
                                Session["logopath"] = GetCompanylogo("none");
                                Expiredocsession();
                            }

                        }
                    }
                }
                if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("DocumentAction", "CompanyDashBoard", new { id = "All" });
                }
                else
                {
                    objactionresult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date });
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// GET: /UserDashBoard/DocumentAction
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>dec 5 2013</createddate>
        /// <returns>view of DocumentAction</returns>
        [HttpGet]
        public ActionResult DocumentAction(string id)
        {
            //Create Object for Get User DashBoard Details
            objDecisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    //get communication Details of particular login user
                    objUserDashBoard = objViewModel.GetMyCommunicationDetails(id, Shared.MyCommunication);
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("CompanyDashBoard", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// get internal staff details
        /// </summary>
        /// <param name="id">id for filter</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 8 2013</createddate>
        /// <returns>renders internal staff page with details </returns>
        [HttpGet]
        public ActionResult InternalStaff(int id, string type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        type = id
                    };
                    if (id == 0)
                    {
                        //Called method for get internal staff details of particular company
                        objComapnyDashBoard.activeStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (!string.Equals(type, Shared.All))
                            {
                                objComapnyDashBoard.activeStaffdetails = objComapnyDashBoard.activeStaffdetails.Where(x => x.fname.StartsWith(type) || x.title.StartsWith(type)).ToList();
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.activeStaffdetails.Count();
                        objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }
                    else if (id == 1)
                    {
                        //Called method for get internal staff details of particular company
                        objComapnyDashBoard.inactiveStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest);
                        if (!string.Equals(type, Shared.All))
                        {
                            objComapnyDashBoard.inactiveStaffdetails = objComapnyDashBoard.inactiveStaffdetails.Where(x => x.fname.StartsWith(type) || x.title.StartsWith(type)).ToList();
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.inactiveStaffdetails.Count();
                        objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }
                    //Particular user
                    objComapnyDashBoard.titleDetails = objDecisionPointEngine.GetTitle(Shared.Company.ToLower(), companyId);
                    #region Groupby Status
                    int count = 0;
                    IList<InternalstaffResponse> staffDetails = new List<InternalstaffResponse>().ToList();
                    InternalstaffResponse objInternalstaffResponse = new InternalstaffResponse()
                    {
                        fname = string.Empty,
                        lname = string.Empty,
                        emailId = string.Empty,
                        phone = string.Empty,
                        title = string.Empty,
                        Id = 0,
                        IsActive = false,
                        service = string.Empty,
                        zipcode = string.Empty,
                        businessName = string.Empty,
                        invitationStatus = false,
                        companyId = string.Empty,
                        UserType = string.Empty,
                        LastInviteMailDate = null
                    };
                    if (!object.Equals(objComapnyDashBoard.activeStaffdetails, null))
                    {
                        var col = objComapnyDashBoard.activeStaffdetails;
                        staffDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.activeStaffdetails.ToList())
                        {
                            if (list.invitationStatus)
                            {
                                staffDetails.Insert(count, objInternalstaffResponse);
                                count++;
                                break;
                            }
                            count++;
                        }
                        objComapnyDashBoard.activeStaffdetails = staffDetails.ToList();
                    }
                    else if (!object.Equals(objComapnyDashBoard.inactiveStaffdetails, null))
                    {
                        var col = objComapnyDashBoard.inactiveStaffdetails;
                        staffDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.inactiveStaffdetails.ToList())
                        {
                            if (list.invitationStatus)
                            {
                                staffDetails.Insert(count, objInternalstaffResponse);
                                count++;
                                break;
                            }
                            count++;
                        }
                        objComapnyDashBoard.inactiveStaffdetails = staffDetails.ToList();
                    }


                    #endregion
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("InternalStaff", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// search in staffs
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>12 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInStaffs(string term, int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objComapnyDashBoard.SearchStaffdetails = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        type = type
                    };
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard.SearchStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest);
                    bool isActive = true;
                    //0 for current staff
                    if (type.Equals(1))
                    {
                        isActive = false;
                    }
                    serach1 = objComapnyDashBoard.SearchStaffdetails.Where(x => x.IsActive == isActive && (x.fname.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(x => x.fname).Distinct().ToList();
                    serach2 = objComapnyDashBoard.SearchStaffdetails.Where(x => x.IsActive == isActive && (x.title.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(x => x.title).Distinct().ToList();
                    serachFinal = serach1.Union(serach2).ToList();

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return this.Json(serachFinal, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// undate internal staff details
        /// </summary>
        /// <param name="id">unique id of user</param>
        /// <param name="roleid">role id </param>
        /// <param name="">title id</param>
        /// <param name="permissionid">permission id</param>
        /// <param name="fName">first Name</param>
        /// <param name="">last Name</param>
        /// <param name="phone">phone</param>
        /// <param name="emailId">email Id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 4 2014</createddate>
        /// <returns>integer type result sucess or not?</returns>
        [HttpPost]
        public int UpdateInternalStaff(InternalStaffModel internalStaffModel)
        {
            int isUpdated = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (internalStaffModel.UserType.Trim().ToLower().Equals(Shared.IC.Trim().ToLower()))
                    {
                        isUpdated = objDecisionPointEngine.UpdateVendorType(internalStaffModel.id, internalStaffModel.CompanyId, companyId, internalStaffModel.titleid);
                    }
                    //Update Satff profile
                    objUserDashBoardRequest = new UserDashBoardRequest();
                    objUserDashBoardRequest.FName = (string.IsNullOrEmpty(internalStaffModel.fName)) ? string.Empty : internalStaffModel.fName.Trim();
                    objUserDashBoardRequest.LName = (string.IsNullOrEmpty(internalStaffModel.lName)) ? string.Empty : internalStaffModel.lName.Trim();
                    objUserDashBoardRequest.EmailId = (string.IsNullOrEmpty(internalStaffModel.emailId)) ? string.Empty : internalStaffModel.emailId.Trim();
                    if (!string.IsNullOrEmpty(internalStaffModel.phone))
                    {
                        if (internalStaffModel.UserType.Trim().ToLower().Equals(Shared.IC.Trim().ToLower()))
                        {
                            objUserDashBoardRequest.OfficePhone = internalStaffModel.phone.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                        }
                        else
                        {
                            objUserDashBoardRequest.DirectPhone = internalStaffModel.phone.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                        }
                    }
                    objUserDashBoardRequest.UserId = internalStaffModel.id;
                    objUserDashBoardRequest.ModifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    isUpdated = objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, Shared.StaffEdit);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isUpdated;
        }

        /// <summary>
        /// reactive staff 
        /// </summary>
        /// <param name="id">int type staff id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>jan 2 2014</createddate>
        /// <returns>returns result with 1 as success and 0 as fails</returns>
        [HttpPost]
        public int Reactivetaff(int id)
        {
            int isReactive = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    isReactive = objDecisionPointEngine.Reactivestaff(id, companyId);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isReactive;
        }
        /// <summary>
        /// remove staff from a company
        /// </summary>
        /// <param name="id">staff id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 13 2013</createddate>
        /// <returns>integer type result whether removed or not with 1 as sucess and 0 as fails</returns>
        [HttpPost]
        public int Removetaff(int id)
        {
            int isReactive = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    isReactive = objDecisionPointEngine.Removetaff(id, companyId);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isReactive;
        }
        /// <summary>
        /// Used for delete any paricular document
        /// </summary>
        /// <param name="docId">documnet id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 14 2013</createddate>
        /// <returns>int type result</returns>
        public int ReactiveDocument(int docId, int type)
        {
            logMessage = new StringBuilder();
            int isUpdated = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    isUpdated = objDecisionPointEngine.ReactiveDocument(docId, type, companyId);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isUpdated;
        }

        /// <summary>
        /// manage vendor page
        /// </summary>
        /// <param name="id">id of current vendor or past vendor</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>feb 3 2014</createddate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Vendors(int id, string type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        isActive = false,
                        filtertype = Shared.None,
                        uType = Shared.Vendor,
                    };
                    if (id == 0)
                    {
                        objComapnyDashBoard.PastvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (!string.Equals(type, Shared.All))
                            {
                                if (type.Equals(Shared.Pending))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.Pending).ToList();
                                }
                                else if (type.Equals(Shared.NonMember))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.NonMember).ToList();
                                }
                                else if (type.Equals(Shared.Member))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.Member).ToList();
                                }
                                else
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.vendor.StartsWith(type) || x.contact.StartsWith(type)).ToList();
                                }
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.PastvendorsList.Count();
                        TempData["isCurrent"] = Shared.Zero;
                    }
                    else
                    {
                        filterRequest.isActive = true;
                        objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                        if (!string.Equals(type, Shared.All))
                        {
                            if (type.Equals(Shared.Pending))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.Pending).ToList();
                            }
                            else if (type.Equals(Shared.NonMember))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.NonMember).ToList();
                            }
                            else if (type.Equals(Shared.Member))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.Member).ToList();
                            }
                            else
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.vendor.StartsWith(type) || x.contact.StartsWith(type)).ToList();
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.CurrentvendorsList.Count();
                        TempData["isCurrent"] = Shared.One;
                    }
                    #region Groupby Status
                    int count = 0;
                    string status = string.Empty;
                    IList<VendorsList> companyDetails = new List<VendorsList>().ToList();
                    VendorsList objVendorsList = new VendorsList()
                    {
                        Id = 0,
                        contact = string.Empty,
                        vendor = string.Empty,
                        emailId = string.Empty,
                        phone = string.Empty,
                        service = string.Empty,
                        title = string.Empty,
                        stateAbbre = string.Empty,
                        zipCode = string.Empty,
                        CompanyId = string.Empty,
                        DocFlow = string.Empty,
                        DocFTblId = 0,
                        invitationStatus = false,
                        LastInviteMailDate = System.DateTime.Now,
                        UserType = string.Empty,
                        IsNonMember = false,
                        Status = string.Empty
                    };
                    if (!object.Equals(objComapnyDashBoard.CurrentvendorsList, null))
                    {
                        var col = objComapnyDashBoard.CurrentvendorsList;
                        companyDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.CurrentvendorsList.ToList())
                        {
                            if (!string.IsNullOrEmpty(status))
                            {
                                if (!status.Equals(list.Status))
                                {
                                    companyDetails.Insert(count, objVendorsList);
                                    count++;
                                    //break;
                                }
                            }
                            status = list.Status;
                            count++;
                        }
                        objComapnyDashBoard.CurrentvendorsList = companyDetails.ToList();
                    }
                    else if (!object.Equals(objComapnyDashBoard.PastvendorsList, null))
                    {
                        var col = objComapnyDashBoard.PastvendorsList;
                        companyDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.PastvendorsList.ToList())
                        {
                            if (!string.IsNullOrEmpty(status))
                            {
                                if (!status.Equals(list.Status))
                                {
                                    companyDetails.Insert(count, objVendorsList);
                                    count++;
                                    //break;
                                }
                            }
                            status = list.Status;
                            count++;
                        }
                        objComapnyDashBoard.PastvendorsList = companyDetails.ToList();
                    }


                    #endregion
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);


                    objComapnyDashBoard.flowDetails = objDecisionPointEngine.GetDocFlow();
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("Vendors", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// search in Vendors
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>12 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInVendors(string term, int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objComapnyDashBoard.SearchStaffdetails = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        isActive = true,
                        filtertype = Shared.None,
                        uType = Shared.Vendor,
                    };
                    if (!string.IsNullOrEmpty(term))
                    {
                        if (Convert.ToInt32(term.Split(char.Parse(Shared.Colon))[1], CultureInfo.InvariantCulture).Equals(2))
                        {
                            filterRequest.uType = Shared.Client;
                        }
                    }
                    //0 for past vendors
                    if (type.Equals(0))
                    {
                        filterRequest.isActive = false;
                    }
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard.SerachInVendors = objDecisionPointEngine.GetVendorList(filterRequest);
                    if (!string.IsNullOrEmpty(term))
                    {
                        serach1 = objComapnyDashBoard.SerachInVendors.Where(x => (x.vendor.StartsWith(term.Split(char.Parse(Shared.Colon))[0], StringComparison.OrdinalIgnoreCase))).Select(y => y.vendor).Distinct().ToList();
                        serach2 = objComapnyDashBoard.SerachInVendors.Where(x => (x.contact.StartsWith(term.Split(char.Parse(Shared.Colon))[0], StringComparison.OrdinalIgnoreCase))).Select(y => y.contact).Distinct().ToList();
                        serachFinal = serach1.Union(serach2).ToList();
                    }


                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return this.Json(serachFinal, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// update document flow
        /// </summary>
        /// <param name="UniqueId">Unique Id</param>
        /// <param name="flowId">flow Id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 14 2014</createddate>
        /// <returns>int type rsult doc updated or not?</returns>
        [HttpPost]
        public int UpdateDocFlow(InternalStaffModel internalStaffModel)
        {
            int IsUpdated = 0;
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();

                    objUserDashBoardRequest = new UserDashBoardRequest()
                    {
                        CompanyName = internalStaffModel.CompanyName,
                        EmailId = internalStaffModel.emailId,
                        DirectPhone = internalStaffModel.phone,
                        UserId = internalStaffModel.UserId,
                        FlowId = internalStaffModel.FlowId,
                        FlowTblId = internalStaffModel.FlowTblId,
                        FirstName = internalStaffModel.fName,
                        LastName = internalStaffModel.lName
                    };
                    if (!string.IsNullOrEmpty(internalStaffModel.phone))
                    {
                        objUserDashBoardRequest.DirectPhone = internalStaffModel.phone.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim();
                    }
                    if (string.IsNullOrEmpty(internalStaffModel.emailId))
                    {
                        objUserDashBoardRequest.EmailId = string.Empty;
                    }
                    IsUpdated = objDecisionPointEngine.UpdateDocFlow(objUserDashBoardRequest);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return IsUpdated;
        }
        /// <summary>
        /// reactive vendors
        /// </summary>
        /// <param name="id">unique id of vendor</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 25 2013</createddate>
        /// <returns>result updated or not in int type</returns>
        [HttpPost]
        public int ReactiveVendor(int id)
        {
            int response = 0;
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture)))
                    {
                        if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("IC"))
                        {
                            companyId = Shared.None;
                        }
                        else
                        {
                            companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                        }
                    }
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.ReactiveVendor(id, companyId);
                    // response = objDecisionPointEngine.ReactiveVendor(id,CompanyId);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        [HttpPost]
        public int ReactiveBusinessPartners(int id)
        {
            int response = 0;
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);

                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.ReactiveBusinessPartners(id);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// remove vendor
        /// </summary>
        /// <param name="id">unique id of vendor</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>feb 20 2014</createddate>
        /// <returns>result in inetger type</returns>
        [HttpPost]
        public int RemoveVendor(int id)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture)))
                    {
                        if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("IC"))
                        {
                            companyId = Shared.None;
                        }
                        else
                        {
                            companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                        }
                    }

                    objDecisionPointEngine = new DecisionPointEngine();
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    response = objDecisionPointEngine.RemoveVendor(id, companyId);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        [HttpPost]
        public int RemoveBusinessPartners(int id)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    response = objDecisionPointEngine.RemoveBusinessPartners(id);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// client
        /// </summary>
        /// <param name="id">int type id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 25 2013</createddate>
        /// <returns>client page</returns>
        [HttpGet]
        public ActionResult Client(int id, string type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        isActive = false,
                        filtertype = Shared.None,
                        uType = Shared.Client,
                    };
                    if (id == 0)
                    {
                        objComapnyDashBoard.PastvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (!string.Equals(type, Shared.All))
                            {
                                if (type.Equals(Shared.Pending))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.Pending).ToList();
                                }
                                else if (type.Equals(Shared.NonMember))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.NonMember).ToList();
                                }
                                else if (type.Equals(Shared.Member))
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.Status == Shared.Member).ToList();
                                }
                                else
                                {
                                    objComapnyDashBoard.PastvendorsList = objComapnyDashBoard.PastvendorsList.Where(x => x.vendor == type || x.contact == type).ToList();
                                }
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.PastvendorsList.Count();
                        TempData["isCurrent"] = Shared.Zero;
                    }
                    else
                    {
                        filterRequest.isActive = true;
                        objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                        if (!string.Equals(type, Shared.All))
                        {
                            if (type.Equals(Shared.Pending))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.Pending).ToList();
                            }
                            else if (type.Equals(Shared.NonMember))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.NonMember).ToList();
                            }
                            else if (type.Equals(Shared.Member))
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.Status == Shared.Member).ToList();
                            }
                            else
                            {
                                objComapnyDashBoard.CurrentvendorsList = objComapnyDashBoard.CurrentvendorsList.Where(x => x.vendor == type || x.contact == type).ToList();
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.CurrentvendorsList.Count();
                        TempData["isCurrent"] = Shared.One;
                    }
                    #region Groupby Status
                    int count = 0;
                    string status = string.Empty;
                    IList<VendorsList> companyDetails = new List<VendorsList>().ToList();
                    VendorsList objVendorsList = new VendorsList()
                    {
                        Id = 0,
                        contact = string.Empty,
                        vendor = string.Empty,
                        emailId = string.Empty,
                        phone = string.Empty,
                        service = string.Empty,
                        title = string.Empty,
                        stateAbbre = string.Empty,
                        zipCode = string.Empty,
                        CompanyId = string.Empty,
                        DocFlow = string.Empty,
                        DocFTblId = 0,
                        invitationStatus = false,
                        LastInviteMailDate = System.DateTime.Now,
                        UserType = string.Empty,
                        IsNonMember = false,
                        Status = string.Empty
                    };
                    if (!object.Equals(objComapnyDashBoard.CurrentvendorsList, null))
                    {
                        var col = objComapnyDashBoard.CurrentvendorsList;
                        companyDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.CurrentvendorsList.ToList())
                        {
                            if (!string.IsNullOrEmpty(status))
                            {
                                if (!status.Equals(list.Status))
                                {
                                    companyDetails.Insert(count, objVendorsList);
                                    count++;
                                    //break;
                                }
                            }
                            status = list.Status;
                            count++;
                        }
                        objComapnyDashBoard.CurrentvendorsList = companyDetails.ToList();
                    }
                    else if (!object.Equals(objComapnyDashBoard.PastvendorsList, null))
                    {
                        var col = objComapnyDashBoard.PastvendorsList;
                        companyDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.PastvendorsList.ToList())
                        {
                            if (!string.IsNullOrEmpty(status))
                            {
                                if (!status.Equals(list.Status))
                                {
                                    companyDetails.Insert(count, objVendorsList);
                                    count++;
                                    // break;
                                }
                            }
                            status = list.Status;
                            count++;
                        }
                        objComapnyDashBoard.PastvendorsList = companyDetails.ToList();
                    }


                    #endregion
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard.flowDetails = objDecisionPointEngine.GetDocFlow();
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("Client", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// GET: /UserDashBoard/CompanyProfile
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 20 2013</createddate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CompanyProfile()
        {
            logMessage = new StringBuilder();
            try
            {
                int cAreaType = 0;
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    //Called method for get My profile details of Individual and send it to view property for UI
                    objUserDashBoardResponse = new UserDashBoardResponse();
                    objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(userId);
                    if (!objUserDashBoardResponse.Equals(null))
                    {
                        objUserDashBoard.FName = objUserDashBoardResponse.fName;
                        objUserDashBoard.MName = objUserDashBoardResponse.mName;
                        objUserDashBoard.LName = objUserDashBoardResponse.lName;
                        objUserDashBoard.EmailId = objUserDashBoardResponse.emailId;
                        objUserDashBoard.OfficePhone = objUserDashBoardResponse.officePhone;
                        objUserDashBoard.CompanyId = objUserDashBoardResponse.CompanyId;
                        objUserDashBoard.CompanyName = objUserDashBoardResponse.companyName;
                        objUserDashBoard.CoverageAreaStatus = objUserDashBoardResponse.CoverageAreaStatus;
                        objUserDashBoard.CertificationNumber = objUserDashBoardResponse.CertificationNumber;
                        objUserDashBoard.CertificateExpDate = objUserDashBoardResponse.CertificateExpDate;
                        objUserDashBoard.CertifyingAgency = objUserDashBoardResponse.CertifyingAgency;
                        objUserDashBoard.BusinessClass = objUserDashBoardResponse.BusinessClass;
                        if (!string.IsNullOrEmpty(objUserDashBoardResponse.companylogo))
                        {
                            objUserDashBoard.File = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + objUserDashBoardResponse.companylogo;
                        }
                        else
                        {
                            objUserDashBoard.File = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + Convert.ToString(ConfigurationManager.AppSettings["DefaultCompanyProflePicName"]);
                        }
                    }
                    //Particular user
                    objUserDashBoard.ServiceDetails = objDecisionPointEngine.GetServices("Company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)).Select(x => x.serviceName).ToList();
                    if (!string.IsNullOrEmpty(objUserDashBoard.CoverageAreaStatus))
                    {
                        if (objUserDashBoard.CoverageAreaStatus.Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Zip.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            cAreaType = 3;
                        }
                    }
                    objUserDashBoard.StateDetails = objDecisionPointEngine.GetStateList(userId, companyId, cAreaType);
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    {
                        //All data
                        objUserDashBoard.ServiceList = objDecisionPointEngine.GetServices(string.Empty, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        objactionresult = RedirectToAction("Login", "Login");
                    }
                    //Called method for get My Profile details of Individual and send it to view property for UI
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {

                        objUserDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objUserDashBoard.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoveragearea;

                    }
                    objUserDashBoard.BusinessClassDetails = objDecisionPointEngine.GetBusinessClass();
                    //assign the view model
                    ViewData.Model = objUserDashBoard;

                    objactionresult = View("CompanyProfile", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// GET: /UserDashBoard/Accountprofile
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>dec 18 2013</createddate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Accountprofile()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    objUserDashBoard = objViewModel.GetMyProfileDetails(Shared.Company);
                    ViewData.Model = objUserDashBoard;

                    objactionresult = View("Accountprofile", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }


        /// <summary>
        /// update prfile
        /// </summary>
        /// <param name="objUserDashBoard"> model class of user dashboard</param>
        /// <param name="file">uploaded profile pic</param>
        /// <createdby>Bobi s</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 24 2013</createddate>
        /// <modifieddate>jan 23 2014</modifieddate>
        /// <returns>my profile page</returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Updatemyprofile(UserDashBoard objUserDashBoard, HttpPostedFileBase file)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    objViewModel = new ViewModel();
                    objViewModel.UpdateMyProfile(objUserDashBoard, file);
                    objactionresult = RedirectToAction("Accountprofile");

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary> GET: /UserDashBoard/Myprofile
        /// </summary>
        /// <param name="objUserDashBoard">userdashboard model class</param>
        /// <param name="file">uploaded file</param>
        /// <returns>returns view after saving details</returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Updatecompanyprofile(UserDashBoard objUserDashBoard, HttpPostedFileBase file)
        {
            logMessage = new StringBuilder();
            string NewPath = string.Empty;
            var fileName = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    string Companyid = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    //Update My profile
                    objUserDashBoardRequest = new UserDashBoardRequest();
                    objUserDashBoardRequest.FName = (string.IsNullOrEmpty(objUserDashBoard.FName)) ? string.Empty : objUserDashBoard.FName.Trim();
                    objUserDashBoardRequest.MName = (string.IsNullOrEmpty(objUserDashBoard.MName)) ? string.Empty : objUserDashBoard.MName.Trim();
                    objUserDashBoardRequest.LName = (string.IsNullOrEmpty(objUserDashBoard.LName)) ? string.Empty : objUserDashBoard.LName.Trim();
                    objUserDashBoardRequest.EmailId = (string.IsNullOrEmpty(objUserDashBoard.EmailId)) ? string.Empty : objUserDashBoard.EmailId.Trim();
                    if (!string.IsNullOrEmpty(objUserDashBoard.OfficePhone))
                    {
                        objUserDashBoardRequest.OfficePhone = objUserDashBoard.OfficePhone.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim(); ;
                    }
                    objUserDashBoardRequest.CompanyName = (string.IsNullOrEmpty(objUserDashBoard.CompanyName)) ? string.Empty : objUserDashBoard.CompanyName.Trim();
                    objUserDashBoardRequest.UserId = userId;
                    objUserDashBoardRequest.CerificationNumber = objUserDashBoard.CertificationNumber;
                    objUserDashBoardRequest.CertificateExpDate = objUserDashBoard.CertificateExpDate;
                    objUserDashBoardRequest.CertifyingAgency = objUserDashBoard.CertifyingAgency;
                    objUserDashBoardRequest.BusinessClass = objUserDashBoard.BusinessClass;
                    if (file != null && file.ContentLength > 0)
                    {

                        fileName = Path.GetFileName(file.FileName);
                        string FileWithId = Companyid + "_CmpLogo" + fileName;
                        NewPath = Path.Combine(Server.MapPath("~" + ConfigurationManager.AppSettings["companylogo"]), FileWithId);
                        string folderPath = Server.MapPath("~" + ConfigurationManager.AppSettings["companylogo"]);
                        string[] files = Directory.GetFiles(folderPath);
                        foreach (string filename in files)
                        {
                            if (Path.GetFileName(filename).Split('_')[0] == Companyid)
                            {
                                System.IO.File.Delete(filename);
                            }
                        }
                        file.SaveAs(NewPath);
                        //Resize the image
                        Image imgOriginal = Image.FromFile(NewPath);
                        BusinessCore.GetThumbnailImage(imgOriginal, NewPath);
                        Session["logopath"] = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + FileWithId;
                        objUserDashBoardRequest.ProfilePhoto = FileWithId;
                    }
                    //Update my profile details  
                    objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, "companyprofile");
                    //Called method for get My profile details of Individual and send it to view property for UI
                    Session["BusinessName"] = objUserDashBoardRequest.CompanyName;
                    objactionresult = RedirectToAction("CompanyProfile");
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Used for delete any paricular document
        /// </summary>
        /// <param name="docId">int type document id</param>
        /// <param name="type">int type </param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>apr 1 2013</createddate>
        /// <returns>int</returns>
        public int RemoveDocument(int docId, int type)
        {
            logMessage = new StringBuilder();
            int removed = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    removed = objViewModel.RemovedCommunication(docId, type);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return removed;
        }

        /// <summary>
        /// used for view any company detail
        /// </summary>
        /// <param name="UserId">integer type user id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 1 2013</createddate>
        /// <returns>view of company profile</returns>
        [HttpGet]
        public string ViewCompanyprofile(int userId)
        {
            //Used for display log text
            logMessage = new StringBuilder();

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objUserDashBoard = new UserDashBoard();
                objViewModel = new ViewModel();
                objUserDashBoard = objViewModel.ViewCompanyProfile(userId);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard);
        }

        /// <summary>
        /// Used for Search in history section
        /// </summary>
        /// <param name="term">search</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 22 2013</createddate>
        /// <returns>search result</returns>
        [HttpPost]
        public JsonResult SerachInhistory(string term)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            List<string> serachByBoth = new List<string>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    serachByBoth = objViewModel.SerachInHistoryCommunications(term);

                }
                else
                {
                    serachByBoth = null;
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                serachByBoth = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return this.Json(serachByBoth, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// user dashboard
        /// </summary>
        /// <param name="userID">unique id of user</param>
        /// <param name="parentform">string type parent form</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>dec 20 2013</createddate>
        /// <returns>user dashboard</returns>
        public ActionResult UserDashboard(string userID, string parentform)
        {

            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
                loginDetailResponse = SetSession(userID);
                IList<string> sessionlist = new List<string>();
                string OldCompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                {
                    sessionlist = Convert.ToString(Session["parentUserid"]).Split(char.Parse(Shared.Comma)).ToList();
                }
                if (loginDetailResponse != null)
                {
                    int index = 0;
                    if (sessionlist != null)
                    {
                        if (sessionlist.Count > 0)
                        {
                            index = sessionlist.Count - 1;
                            if (Convert.ToInt32(sessionlist[index], CultureInfo.InvariantCulture) != Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture))
                            {
                                Session["parentUserid"] = Session["parentUserid"] + Shared.Comma + Session["UserId"];
                            }
                        }
                        else
                        {
                            Session["parentUserid"] = Session["UserId"];
                        }
                    }
                    else
                    {
                        Session["parentUserid"] = Session["UserId"];
                    }
                    HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                    Session["UserId"] = loginDetailResponse.UserId;
                    Session["UserName"] = loginDetailResponse.Firstname + Shared.SingleSpace + loginDetailResponse.LastName;
                    Session["BusinessName"] = loginDetailResponse.BusinessName;
                    Session["UserType"] = loginDetailResponse.UserType;
                    Session["CompanyId"] = loginDetailResponse.CompanyId;
                    Session["IsTemp"] = loginDetailResponse.IsTemp;
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                    {
                        sessionlist = Convert.ToString(Session["parentUserid"]).Split(char.Parse(Shared.Comma)).ToList();
                    }
                    if (sessionlist != null)
                    {
                        if (sessionlist.Count > 0)
                        {

                            if (Convert.ToInt32(sessionlist[index], CultureInfo.InvariantCulture) != Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture))
                            {
                                Session["parentUserid"] = Session["parentUserid"] + Shared.Comma + Session["UserId"];
                            }

                        }
                    }
                    else
                    {
                        Session["parentUserid"] = Session["UserId"];

                    }
                    if (!string.IsNullOrEmpty(parentform))
                    {
                        if (parentform.Trim().Equals("'sp'") || parentform.Trim().Equals("'ms'"))
                        {
                            Session["logopath"] = GetCompanylogo("staff");
                        }
                        else if (parentform.Trim().Equals("'mic'"))
                        {
                            Session["logopath"] = GetCompanylogo("none");
                        }
                    }
                    if (!string.IsNullOrEmpty(parentform))
                    {
                        if (parentform.Contains(char.Parse(Shared.Colon)))
                        {
                            Session["CompanyId"] = OldCompanyId;
                        }
                    }
                    Session["parentform"] = parentform;


                    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == Shared.IC)
                    {
                        if (parentform.Equals("sp")) { objactionresult = RedirectToAction("DocumentAction", "UserDashBoard", new { id = Shared.All }); }
                        else
                        {
                            objactionresult = RedirectToAction("ICProfile", "UserDashBoard");
                        }
                    }
                    else
                    {
                        if (parentform.Equals("sp")) { objactionresult = RedirectToAction("DocumentAction", "CompanyDashBoard", new { id = Shared.All }); }
                        else
                        {
                            objactionresult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date, type = 1 });
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// set session of user
        /// </summary>
        /// <param name="userID">string type user id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>feb 14 2014</createddate>
        /// <returns> LoginDetailResponse class type</returns>
        public LoginDetailResponse SetSession(string userID)
        {
            // Set Session for particular visit user
            objDecisionPointEngine = new DecisionPointEngine();
            LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
            SuperAdminResponse superAdminResponse = new SuperAdminResponse();
            superAdminResponse = objDecisionPointEngine.LoginDetail(userID, "userview");
            loginDetailResponse = objDecisionPointEngine.Login(superAdminResponse.UserID, superAdminResponse.Password);
            return loginDetailResponse;
        }
        /// <summary>
        /// return on my view
        /// </summary>
        /// <param name="id">string type id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>dec 17 2013</createddate>
        /// <returns>view</returns>
        public ActionResult Returntomyview(string id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    Expiredocsession();
                    IList<string> sessionlist = new List<string>();
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                    {
                        sessionlist = Convert.ToString(Session["parentUserid"]).Split(',').ToList();
                    }

                    string parentuserId = Shared.Zero;
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["superAdmin"])))
                    {
                        LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
                        if (sessionlist != null)
                        {
                            if (sessionlist.Count > 0)
                            {
                                if (sessionlist.Count >= 2)
                                {
                                    parentuserId = sessionlist[sessionlist.Count - 2];
                                    sessionlist.RemoveAt((sessionlist.Count - 1));
                                    Session["parentUserid"] = null;
                                    foreach (var ids in sessionlist)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                                        {
                                            Session["parentUserid"] = Session["parentUserid"] + "," + ids;
                                        }
                                        else
                                        {
                                            Session["parentUserid"] = ids;
                                        }
                                    }

                                }
                                else if (sessionlist.Count <= 1)
                                {
                                    Session["parentUserid"] = null;
                                }

                            }
                        }
                        if (parentuserId == Shared.Zero)
                        {
                            parentuserId = Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture);
                        }
                        loginDetailResponse = SetSession(parentuserId);

                        if (loginDetailResponse != null)
                        {
                            HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                            Session["UserId"] = loginDetailResponse.UserId;
                            Session["BusinessName"] = loginDetailResponse.BusinessName;
                            Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
                            Session["UserType"] = loginDetailResponse.UserType;
                            Session["CompanyId"] = loginDetailResponse.CompanyId;
                            Session["IsTemp"] = loginDetailResponse.IsTemp;
                            Session["logopath"] = GetCompanylogo("none");
                        }
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                    {
                        sessionlist = Convert.ToString(Session["parentUserid"]).Split(',').ToList();
                        if (sessionlist.Count <= 1)
                        {
                            Session["parentUserid"] = null;
                        }
                    }
                    if (Session["UserType"].Equals("SuperAdmin"))
                    {
                        objactionresult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date, type = 1 });
                    }
                    else if (Session["UserType"].Equals("Guest"))
                    {
                        objactionresult = RedirectToAction("MyDashBoard", "UserDashBoard", new { date = DateTime.Now.Date, type = 1 });
                    }
                    else
                    {
                        objactionresult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date, type = 1 });
                    }
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion

        #region Title
        /// <summary>
        /// add title
        /// </summary>
        /// <param name="title">string title name</param>
        /// <param name="status">string status</param>
        /// <param name="titleId">int tile id</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>dec 16 2013</createddate>
        /// <returns>return title page</returns>
        public int AddTitle(string title, string status, int titleId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(title))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddTitle(title, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateTitle(titleId, title, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// view title
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 1 2013</createddate>
        /// <returns>title view</returns>
        public ActionResult ViewTitle()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    //Called method for get Messages details of Individual and send it to view property for UI
                    objComapnyDashBoard.titleDetails = objDecisionPointEngine.GetTitle(Shared.Admin.ToLower(CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.titleDetails.OrderBy(q => q.titleName).ToList();
                    #region Divide in Foru Columns
                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();

                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].titleName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstTest.Add(lstprop);
                    }


                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;

                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            LstProp lstprop = new LstProp();

                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;


                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }


                    #endregion
                    #endregion
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.titleDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewTitle", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Disable Enable Title
        /// </summary>
        /// <param name="titleId">int title Id</param>
        /// <param name="isActive">string type isActive</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 18 2013</createddate>
        /// <returns>return result with int type </returns>
        public int DisaEnaTitle(int titleId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals(Shared.Enable))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaTitle(titleId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region Reference
        /// <summary>
        /// AddReference
        /// </summary>
        /// <param name="reference">string type reference</param>
        /// <param name="status">string type status</param>
        /// <param name="referenceId">int type referenceId</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 5 2014</createddate>
        /// <returns>return refrence added or not? with</returns>
        public int AddReference(string reference, string status, int referenceId, int groupId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(reference))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddReference(reference, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), groupId);
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateReference(referenceId, reference, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), groupId);
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// view refrence
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 12 2013</createddate>
        /// <returns>page of view refrence</returns>
        public ActionResult ViewReference(string id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        //Called method for get Messages details of Individual and send it to view property for UI
                        if (id.Equals("All"))
                        {
                            objComapnyDashBoard.referenceDetails = objDecisionPointEngine.GetReference(Shared.Admin.ToLower(CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), id);
                        }
                        else
                        {
                            objComapnyDashBoard.referenceDetails = objDecisionPointEngine.GetReference(Shared.Communication, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), id);
                        }
                    }
                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.referenceDetails.OrderBy(q => q.referenceName).ToList();
                    #region
                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();

                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].referenceName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstprop.Col1GroupName = GetList[i].groupName;
                        lstTest.Add(lstprop);
                    }


                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;

                    //  List<Test> lstTest1 = new List<Test>();
                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            //Test test = new Test();
                            LstProp lstprop = new LstProp();

                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;
                            lstprop.Col1GroupName = lstTest[innerColumnIterator].Col1GroupName;
                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;
                            lstprop.Col2GroupName = lstTest[innerColumnIterator].Col1GroupName;
                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;
                            lstprop.Col3GroupName = lstTest[innerColumnIterator].Col3GroupName;

                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }






                    #endregion
                    #endregion
                    objComapnyDashBoard.groupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.referenceDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewReference", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// disable enable refrence
        /// </summary>
        /// <param name="referenceId">int reference Id</param>
        /// <param name="">string isActive</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 25 2013</createddate>
        /// <returns>view return type in int</returns>
        public int DisaEnaReference(int referenceId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals(Shared.Enable))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaReference(referenceId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region Client
        /// <summary>
        /// view client
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 11 2014</createddate>
        /// <returns>client view</returns>
        public ActionResult ViewClient()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {

                    //Called method for get Messages details of Individual and send it to view property for UI
                    objComapnyDashBoard.clientDetails = objDecisionPointEngine.GetClient(Shared.Admin.ToLower(CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.clientDetails.OrderBy(q => q.clientName).ToList();
                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();
                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].clientName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstTest.Add(lstprop);
                    }


                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;

                    //  List<Test> lstTest1 = new List<Test>();
                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            //Test test = new Test();
                            LstProp lstprop = new LstProp();

                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;


                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }

                    #endregion
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.clientDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewClient", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// add client
        /// </summary>
        /// <param name="client">string type client</param>
        /// <param name="status">string type status</param>
        /// <param name="">int type clientId</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>nov 11 2013</createddate>
        /// <returns>int type client added or not?</returns>
        public int AddClient(string client, string status, int clientId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(client))
                {
                    if (status.Equals(Shared.Save))
                        Isinserted = objDecisionPointEngine.AddClient(client, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    else if (status.Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Edit.Trim().ToLower(CultureInfo.InvariantCulture)))
                        Isinserted = objDecisionPointEngine.UpdateClient(clientId, client);
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// enable or disable client
        /// </summary>
        /// <param name="">int clientid</param>
        /// <param name="">string isActive</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 1 2014</createddate>
        /// <returns></returns>
        public int DisaEnaClient(int clientid, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals("enable"))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaClient(clientid, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region Service

        /// <summary>
        /// view services
        /// </summary>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 1 2014</createddate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewService()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    //Called method for get Messages details of Individual and send it to view property for UI
                    objComapnyDashBoard.serviceDetails = objDecisionPointEngine.GetServices("admin", companyId);
                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.serviceDetails.OrderBy(q => q.serviceName).ToList();
                    #region Divide In Four Columns
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();
                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].serviceName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstTest.Add(lstprop);
                    }


                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;

                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            LstProp lstprop = new LstProp();
                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;

                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;


                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }
                    #endregion
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.serviceDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    if (!string.IsNullOrEmpty(companyId) && (Request["type"] == "SIC" || Request["type"] == "SPS"))
                    {

                        objComapnyDashBoard.serviceList = objDecisionPointEngine.GetServices("user", companyId);
                        objComapnyDashBoard.userserviceDetails = objDecisionPointEngine.GetUserServices(userId, companyId);
                        objComapnyDashBoard.ServiceStatus = Convert.ToInt32(objDecisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 2));
                    }
                    if (Request["type"] == Shared.IC || Request["type"] == "ICD")
                    {
                        if (Convert.ToString(HttpContext.Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.IC))
                        {
                            objViewModel = new ViewModel();
                            string parenttUserId = objViewModel.GetVisitorUserId(companyId);
                            //Check the impersination case and get the visitor userId
                            if (!string.IsNullOrEmpty(parenttUserId) && !Convert.ToString(Session["IsSuperAdmin"], CultureInfo.InvariantCulture).Equals(Shared.Yes))
                            {
                                string parentCompanyId = Convert.ToString(objDecisionPointEngine.GetParentUserId(parenttUserId, Shared.GetCompanyid), CultureInfo.InvariantCulture);
                                objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetICClientList(userId, true).Where(x => x.CompanyId == parentCompanyId);
                            }
                            else
                            {
                                objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetICClientList(userId, true);
                            }
                        }

                    }

                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewService", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// add service
        /// </summary>
        /// <param name="service">string service</param>
        /// <param name="">string status</param>
        /// <param name="">int serviceId</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 8 2014</createddate>
        /// <returns>result saved or not in int type</returns>
        public int AddService(string service, string status, int serviceId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (!string.IsNullOrEmpty(service))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddService(service, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateService(serviceId, service, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        public int DisaEnaService(int serviceId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals("enable"))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaService(serviceId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }

        /// <summary>
        /// get all  services list as per client 
        /// </summary>  
        /// <createdBy>Bobi</createdBy>
        /// <param name="companyId">companyId</param>
        /// <createdDate>30 june 2014</createdDate>
        /// <returns>list service list</returns>
        [HttpGet]
        public JsonResult GetICClientService(string companyId, int type)
        {

            logMessage = new StringBuilder();
            objComapnyDashBoard = new Models.CompanyDashBoard();
            try
            {

                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    if (type.Equals(0))
                    {
                        objComapnyDashBoard.serviceList = objDecisionPointEngine.GetServices("user", companyId);
                        objComapnyDashBoard.ServiceStatus = Convert.ToInt32(objDecisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 3));
                        if(!(objComapnyDashBoard.serviceList.ToList().Count > 0)){
                            objComapnyDashBoard.serviceList = objDecisionPointEngine.GetServicesOfNewHired(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                        }
                    }
                    else
                    {
                        objComapnyDashBoard.serviceList = objDecisionPointEngine.GetServicesOfNewHired(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    objComapnyDashBoard.serviceList = null;
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(objComapnyDashBoard, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used for saved the service of IC
        /// </summary>
        /// <param name="serviceids"></param>
        /// <returns>int</returns>
        /// <createdby>bobi</createdby>
        /// <createddate>july 1 2014</createddate>
        [HttpPost]
        public int SaveICServices(string serviceids)
        {
            int isInserted = 0;
            logMessage = new StringBuilder();
            try
            {

                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    isInserted = objDecisionPointEngine.SaveICServices(serviceids, userId, companyId);

                }
                else
                {
                    objComapnyDashBoard.serviceList = null;
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isInserted;
        }
        #endregion

        #region Invitation

        /// <summary>
        /// send invitation
        /// </summary>
        /// <param name="objSendInvitation">SendInvitation class</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>mar 4 2014</createddate>
        /// <modifiedby>bobi s, Nilesh Dubey</modifiedby>
        /// <returns>int type result saved or not?</returns>
        [HttpPost]
        public int SendInvitation(List<SendInvitation> objSendInvitation)
        {

            int result = 0;
            logMessage = new StringBuilder();
            try
            {

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    objViewModel = new ViewModel();
                    result = objViewModel.SendInviteToUsers(objSendInvitation);
                    TempData["InvitaionMessage"] = DecisionPointR.inviteSentSucessfullMsg;

                }
                else
                {
                    result = -1;
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {

                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return result;

        }

        /// <summary>
        /// get company ID
        /// </summary>
        /// <param name="term">serach term string type</param>
        /// <createdby>sumit saurav, bobis</createdby>
        /// <createddate>dec 8 2013</createddate>
        /// <returns>json result company id</returns>
        [HttpGet]
        public JsonResult GetCompanyId(string term)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                var compId = objDecisionPointEngine.getCompanyId();
                if (term != null)
                {
                    compId = compId.Where(m => (m.CompanyId.ToLower().StartsWith(term.ToLower()) || m.UserId.ToLower().StartsWith(term.ToLower())));
                }
                return Json(compId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }

        /// <summary>
        /// Check Existing EmailId
        /// </summary>
        /// <param name="">string type emailId</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>mar 4 2014</createddate>
        /// <returns>exists or not? json type</returns>
        [HttpGet]
        public JsonResult CheckExistingEmailId(string emailId)
        {
            bool IsExist = false;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();

                if (!string.IsNullOrEmpty(emailId))
                {
                    IsExist = objDecisionPointEngine.CheckExistingEmailId(emailId);
                }
                return Json(IsExist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }



        /// <summary>
        /// get company name as per company Id
        /// </summary>
        /// <param name="CompanyId">company name</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>mar 14 2014</createddate>
        /// <returns>json type company name</returns>
        [HttpGet]
        public JsonResult GetCompanyNameByUserType(string CompanyId, string Usertype)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                var compId = objDecisionPointEngine.GetCompanyNameByUserType(CompanyId, Usertype);


                return Json(compId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }

        /// <summary>
        /// add staff for manual and bulk upload
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <modifiedby>bobis</modifiedby>
        /// <createddate>dec 4 2013</createddate>
        /// <returns>invite staff</returns>
        [HttpGet]
        public ActionResult InviteStaff()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objInviteStaffModel = new InviteStaffModel();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    //Called method for get Messages details of Individual and send it to view property for UI
                    objInviteStaffModel.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    //objInviteStaffModel.roleDetails = objDecisionPointEngine.GetRoles();
                    objInviteStaffModel.flowDetails = objDecisionPointEngine.GetFlow();


                    objInviteStaffModel.pagesize = objInviteStaffModel.titleDetails.Count();
                    objInviteStaffModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = objInviteStaffModel;
                    objactionresult = View("InviteStaff", objInviteStaffModel);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Download Staff Invite Sample
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>dec 5 2014</createddate>
        /// <returns>file on the view</returns>
        public FileResult DownloadStaffIviteSample()
        {
            var fileName = "StaffList.csv";
            var filePath = Server.MapPath("~/Content/documents/Sample/stafflist.csv");
            return File(filePath, "application/octet-stream", fileName);
        }

        /// <summary>
        /// invite vendor manual
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>dec 6 2014</createddate>
        /// <returns>vendor manual invitation page with data</returns>
        [HttpGet]
        public ActionResult InviteVendorManual()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                InviteVendorManual objComapnyDashBoard = new InviteVendorManual();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {

                    //Called method for get Messages details of Individual and send it to view property for UI
                    objComapnyDashBoard.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    //objComapnyDashBoard.roleDetails = objDecisionPointEngine.GetRoles();

                    objComapnyDashBoard.flowDetails = getflowasperconfig();

                    objComapnyDashBoard.DocflowDetails = objDecisionPointEngine.GetDocFlow();


                    objComapnyDashBoard.pagesize = objComapnyDashBoard.titleDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    //assign the view model


                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        objComapnyDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                        objComapnyDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                    }
                    if (!object.Equals(Session["UserType"], null))
                    {
                        if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("SuperAdmin"))
                        {
                            objComapnyDashBoard.CompanyVendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, string.Empty).Select(x => new VendorTypeResponse { VendorTypeName = x.categoryName, VendorTypeId = x.id });
                        }
                        else { objComapnyDashBoard.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.Company).Where(x => x.IsUserBased == false); }
                    }
                    else { objComapnyDashBoard.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.Company).Where(x => x.IsUserBased == false); }

                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// Used for set the flow as per config
        /// </summary>
        /// <returns>IEnumerable<RoleResponse></returns>
        [NonAction]
        private IEnumerable<RoleResponse> getflowasperconfig()
        {
            IEnumerable<RoleResponse> finalrole = null;
            try
            {
                userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                if (!object.Equals(objConfiguratonSettingRequest, null))
                {
                    if (objConfiguratonSettingRequest.IsClient && objConfiguratonSettingRequest.IsVendor)
                    {
                        finalrole = objDecisionPointEngine.GetFlow();
                    }
                    else if (objConfiguratonSettingRequest.IsVendor)
                    {
                        finalrole = objDecisionPointEngine.GetFlow().Where(x => x.flowId == 2);
                    }
                    else if (objConfiguratonSettingRequest.IsClient)
                    {
                        finalrole = objDecisionPointEngine.GetFlow().Where(x => x.flowId == 1);
                    }
                }
                else
                {
                    finalrole = objDecisionPointEngine.GetFlow();
                }
                return finalrole;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Invite Vendor Bulk
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>dec 6 2014</createddate>
        /// <returns>bulk invite vendor page with data</returns>
        [HttpGet]
        public ActionResult InviteVendorBulk()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                inviteVendorModel = new InviteVendorBulk();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    inviteVendorModel = new Models.InviteVendorBulk();
                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = inviteVendorModel;
                    objactionresult = View();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// Invite Staff Bulk
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>dec 19 2014</createddate>
        /// <returns>invite staff bulk page</returns>
        [HttpGet]
        public ActionResult InviteStaffBulk()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objInviteStaffModel = new InviteStaffModel();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {


                    objInviteStaffModel.pagesize = objInviteStaffModel.lst_Csv.Count();
                    objInviteStaffModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    ViewData.Model = objInviteStaffModel;
                    objactionresult = View();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        #region Staff Bulk


        /// <summary>
        /// post:Invite Staff Bulk CSv Upload
        /// </summary>
        /// <param name="inviteStaffModel">inviteStaffModel class</param>
        ///  <createdby>Rohit Kaushik</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>view invite staff with uploaded or not message</returns>
        [HttpPost]
        public ActionResult InviteStaffBulkCSvUpload(InviteStaffModel inviteStaffModel)
        {
            objDecisionPointEngine = new DecisionPointEngine();
            var filePath = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (ModelState.IsValid)
                    {

                        var file = Request.Files[0];
                        var exactfilename = Path.GetFileName(file.FileName);
                        string fnext = Path.GetExtension(exactfilename);
                        if (!string.IsNullOrEmpty(exactfilename))
                        {
                            exactfilename = Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture) + "_" + BusinessCore.GenrateRandomnumber() + Shared.CsvFileExt;
                        }
                        if (file != null && file.ContentLength > 0 && string.Equals(fnext, Shared.CsvFileExt, StringComparison.OrdinalIgnoreCase))
                        {
                            filePath = Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]),
                                                          Path.GetFileName(exactfilename));
                            file.SaveAs(filePath);


                            //Called method for comparing CSV values to the List
                            inviteStaffModel.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));

                            string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + exactfilename;
                            objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                            string[] headerCol = { };
                            int CountIndex = 0;
                            while (objCSVReader.Read())
                            {
                                headerCol = objCSVReader.FieldHeaders;
                                if (headerCol.Count() != 4)
                                {
                                    ViewBag.Error = DecisionPointR.CsvNotMatch;// "CSV Column not match ";

                                    objactionresult = View("InviteStaffBulk", inviteStaffModel);
                                }
                                else
                                {
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (headerCol[i] == "First Name")
                                        {
                                            col1 = true;
                                        }
                                        if (headerCol[i] == "Last Name")
                                        {
                                            col2 = true;
                                        }
                                        if (headerCol[i] == "Email")
                                        {
                                            col3 = true;
                                        }
                                        if (headerCol[i] == "Title")
                                        {
                                            col4 = true;
                                        }
                                    }
                                    if (!col1 || !col2 || !col3 || !col4)
                                    {
                                        ViewBag.Error = DecisionPointR.CsvHeaderNotMatch;// "CSV Column header not match ";

                                        objactionresult = View("InviteStaffBulk", inviteStaffModel);
                                    }
                                    else
                                    {

                                        CountIndex++;
                                        objViewModel = new ViewModel();
                                        inviteStaffModel.lst_Csv.Add(objViewModel.BindColFromCSVFileForStaff(objCSVReader, inviteStaffModel, CountIndex));

                                    }
                                }
                            }

                            objCSVReader.Dispose();

                        }
                        inviteStaffModel.pagesize = inviteStaffModel.lst_Csv.Count();
                        inviteStaffModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                        inviteStaffModel.CsvFileName = exactfilename;


                        objactionresult = View("InviteStaffBulk", inviteStaffModel);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(filePath))
                        {

                            ViewBag.Error = DecisionPointR.UploadCsv; // "Please Upload CSV File";
                            inviteStaffModel.lst_Csv = null;
                            objactionresult = View("InviteStaffBulk", inviteStaffModel);
                        }
                        else
                        {
                            ViewBag.Error = DecisionPointR.InvalidCsvFormat;
                            inviteStaffModel.lst_Csv = null;
                            objactionresult = View("InviteStaffBulk", inviteStaffModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// DeleteItem
        /// </summary>
        /// <param name="ID">int id of row</param>
        /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 25 2013</createddate>
        /// <returns>invite staff with removed row from list</returns>
        [HttpPost]
        public ActionResult DeleteItem(int id, string csvFileName)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objInviteStaffModel = new InviteStaffModel();
                    objInviteStaffModel.titleDetails = objDecisionPointEngine.GetTitle(Shared.Company.ToLower(CultureInfo.InvariantCulture), companyId);

                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + csvFileName;

                    CSV objCSV = new CSV();
                    objCSV.No = id;
                    objViewModel = new ViewModel();
                    objViewModel.ReadAndWriteContentInCSVForStaff(serverpathname, objInviteStaffModel, objCSV, 1);
                    int countIndex = 0;
                    objInviteStaffModel.lst_Csv = new List<CSV>();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        objInviteStaffModel.lst_Csv.Add(objViewModel.BindColFromCSVFileForStaff(objCSVReader, objInviteStaffModel, countIndex));
                    }
                    objCSVReader.Dispose();



                    objInviteStaffModel.pagesize = objInviteStaffModel.lst_Csv.Count();
                    objInviteStaffModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objactionresult = View("InviteStaffBulk", objInviteStaffModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }

        /// <summary>
        /// post:update list
        /// </summary>       
        /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 26 2013</createddate>
        /// <returns>invite staff with removed row from list</returns>
        [HttpPost]
        public ActionResult UpdateItem(CSV newCSV)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    objInviteStaffModel = new InviteStaffModel();
                    objDecisionPointEngine = new DecisionPointEngine();
                    objInviteStaffModel.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));

                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + newCSV.CsvFileName;
                    objViewModel = new ViewModel();
                    //called method for update the record in CSV file : 0 used for update record
                    objViewModel.ReadAndWriteContentInCSVForStaff(serverpathname, objInviteStaffModel, newCSV, 0);
                    int countIndex = 0;
                    objInviteStaffModel.lst_Csv = new List<CSV>();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        objInviteStaffModel.lst_Csv.Add(objViewModel.BindColFromCSVFileForStaff(objCSVReader, objInviteStaffModel, countIndex));
                    }
                    objCSVReader.Dispose();


                    objInviteStaffModel.pagesize = objInviteStaffModel.lst_Csv.Count();
                    objInviteStaffModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objactionresult = View("InviteStaffBulk", objInviteStaffModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// save details
        /// </summary>
        ///  /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 27 2013</createddate>
        /// <returns>saved or not message on invite page</returns>
        [HttpPost]
        public ActionResult SaveItem(string CsvFileName)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    List<SendInvitation> listSendInvitation = new List<Models.SendInvitation>();
                    objInviteStaffModel = new InviteStaffModel();
                    DecisionPointEngine objDecisionPointEngine = new DecisionPointEngine();
                    objInviteStaffModel.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    int countIndex = 0;
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + CsvFileName;
                    objInviteStaffModel.lst_Csv = new List<CSV>();
                    objViewModel = new ViewModel();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        objInviteStaffModel.lst_Csv.Add(objViewModel.BindColFromCSVFileForStaff(objCSVReader, objInviteStaffModel, countIndex));
                    }
                    objCSVReader.Dispose();
                    objSendInvitation = new Models.SendInvitation();
                    for (int i = 0; i < objInviteStaffModel.lst_Csv.Count(); i++)
                    {
                        try
                        {

                            objSendInvitation = new Models.SendInvitation();
                            objSendInvitation.firstName = objInviteStaffModel.lst_Csv[i].StaffFName;
                            objSendInvitation.lastName = objInviteStaffModel.lst_Csv[i].StaffLName;
                            objSendInvitation.titleId = objInviteStaffModel.lst_Csv[i].StaffTitleID;
                            objSendInvitation.permissionId = objInviteStaffModel.lst_Csv[i].StaffPermissionID;
                            objSendInvitation.emailId = objInviteStaffModel.lst_Csv[i].staffEmail;
                            objSendInvitation.companyId = null;
                            objSendInvitation.companyName = Convert.ToString(Session["BusinessName"], CultureInfo.InvariantCulture);
                            objSendInvitation.flowId = 0;
                            objSendInvitation.PaymentType = Shared.One;
                            objSendInvitation.type = Shared.Staff;
                            if (i > 4)
                            {
                                objSendInvitation.IsMailSent = false;
                            }
                            else
                            {
                                objSendInvitation.IsMailSent = true;
                            }

                            listSendInvitation.Add(objSendInvitation);


                        }
                        catch (Exception ex)
                        {
                            log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                            objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
                        }
                        finally
                        {
                            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                            log.Info(logMessage.ToString());
                        }
                    }
                    //call method used for invite the staff(s)
                    SendInvitation(listSendInvitation);
                    objInviteStaffModel.lst_Csv = null;
                    objInviteStaffModel.permissionDetails = null;
                    objInviteStaffModel.titleDetails = null;
                    objInviteStaffModel.CsvFileName = string.Empty;

                    objactionresult = View("InviteStaffBulk", objInviteStaffModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        #endregion


        #region Vendor Bulk


        /// <summary>
        /// post:Vendor Bulk CSv Upload
        /// </summary>
        /// <param name="invitevendor">invitevendor class type</param>
        ///  /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 31 2013</createddate>
        /// <returns>VendorBulkCSvUpload page</returns>
        [HttpPost]
        public ActionResult VendorBulkCSvUpload(InviteVendorBulk invitevendor)
        {
            #region Global
            bool col1 = false;
            bool col2 = false;
            bool col3 = false;
            bool col4 = false;
            bool col5 = false;
            bool col6 = false;
            var filePath = string.Empty; ;
            #endregion
            objDecisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    if (ModelState.IsValid)
                    {

                        var file = Request.Files[0];
                        var exactfilename = Path.GetFileName(file.FileName);

                        string fnext = Path.GetExtension(exactfilename);
                        if (!string.IsNullOrEmpty(exactfilename))
                        {
                            exactfilename = userId + Shared.UnderScore + BusinessCore.GenrateRandomnumber() + Shared.CsvFileExt;
                        }
                        if (file != null && file.ContentLength > 0 && string.Equals(fnext, Shared.CsvFileExt, StringComparison.OrdinalIgnoreCase))
                        {
                            filePath = Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]),
                                                          Path.GetFileName(exactfilename));
                            file.SaveAs(filePath);



                            invitevendor.flowDetails = getflowasperconfig();
                            invitevendor.DocflowDetails = objDecisionPointEngine.GetDocFlow();


                            string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + exactfilename;
                            objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                            string[] headerCol = { };
                            int countIndex = 0;
                            while (objCSVReader.Read())
                            {
                                headerCol = objCSVReader.FieldHeaders;
                                if (headerCol.Count() != 6 && headerCol.Count() != 1)
                                {
                                    ViewBag.Error = DecisionPointR.CsvNotMatch; // "CSV Column  not match ";
                                    objactionresult = View("InviteVendorBulk", invitevendor);

                                }
                                else
                                {
                                    if (headerCol.Count() == 6)
                                    {
                                        if (headerCol.Count() != 6)
                                        {
                                            ViewBag.Error = DecisionPointR.CsvNotMatch;// "CSV Column not match ";
                                            objactionresult = View("InviteVendorBulk", invitevendor);
                                        }
                                        for (int i = 0; i < 6; i++)
                                        {
                                            if (headerCol[i] == "Company Name")
                                            {
                                                col1 = true;
                                            }
                                            if (headerCol[i] == "Relationship")
                                            {
                                                col2 = true;
                                            }
                                            if (headerCol[i] == "Flow")
                                            {
                                                col3 = true;
                                            }
                                            if (headerCol[i] == "First Name")
                                            {
                                                col4 = true;
                                            }
                                            if (headerCol[i] == "Last Name")
                                            {
                                                col5 = true;
                                            }
                                            if (headerCol[i] == "Email")
                                            {
                                                col6 = true;
                                            }

                                        }

                                        if (!col1 || !col2 || !col3 || !col4 || !col5 || !col6)
                                        {
                                            ViewBag.Error = DecisionPointR.CsvHeaderNotMatch; // "CSV Column header not match ";
                                            objactionresult = View("InviteVendorBulk", invitevendor);
                                        }
                                        else
                                        {
                                            countIndex++;
                                            objViewModel = new ViewModel();
                                            invitevendor.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForVendor(objCSVReader, invitevendor, countIndex));
                                        }
                                    }
                                }
                            }
                            objCSVReader.Dispose();

                        }
                        invitevendor.pagesize = invitevendor.LstVendorCsv.Count();
                        invitevendor.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                        invitevendor.CsvFileName = exactfilename;
                        objactionresult = View("InviteVendorBulk", invitevendor);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(filePath))
                        {

                            ViewBag.Error = DecisionPointR.UploadCsv; // "Please Upload CSV File";

                            invitevendor.LstVendorCsv = null;
                            objactionresult = View("InviteVendorBulk", invitevendor);
                        }
                        else
                        {
                            ViewBag.Error = DecisionPointR.InvalidCsvFormat; // "Please Upload CSV Format File";

                            invitevendor.LstVendorCsv = null;
                            objactionresult = View("InviteVendorBulk", invitevendor);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// post:vendor delete from list
        /// </summary>
        /// <param name="id">int type id</param>
        ///  /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>dec 31 2013</createddate>
        /// <returns>deleted vendor list</returns>
        [HttpPost]
        public ActionResult VendorDeleteItem(int? id, string csvFileName)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    inviteVendorModel = new InviteVendorBulk();
                    inviteVendorModel.flowDetails = getflowasperconfig();
                    inviteVendorModel.DocflowDetails = objDecisionPointEngine.GetDocFlow();
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + csvFileName;

                    UpdateVendorModel objUpdateVendorModel = new UpdateVendorModel();
                    objUpdateVendorModel.No = id;
                    objViewModel = new ViewModel();
                    objViewModel.ReadAndWriteContentInCSVForvendor(serverpathname, inviteVendorModel, objUpdateVendorModel, 1);
                    int countIndex = 0;
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForVendor(objCSVReader, inviteVendorModel, countIndex));
                    }
                    objCSVReader.Dispose();


                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objactionresult = View("InviteVendorBulk", inviteVendorModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }


        /// <summary>
        /// post:update vendor item
        /// </summary>
        /// <param name="No">int type number</param>
        /// <param name="FName">string First Name</param>
        /// <param name="LName">string Last Name</param>
        /// <param name="">string Company Name</param>
        /// <param name="Email">string email </param>
        /// <param name="FlowID">int flow id</param>
        /// <param name="FlowText">string flow text</param>
        /// <param name="DocFlowID">int doc flow id</param>
        /// <param name="DocFlowText">string Doc Flow Text</param>
        ///  <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>jan 2 2014</createddate>
        /// <returns>updated vendorvendor </returns>
        [HttpPost]
        public ActionResult VendorUpdateItem(UpdateVendorModel updateVendorModel)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    inviteVendorModel = new InviteVendorBulk();
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();
                    inviteVendorModel.flowDetails = getflowasperconfig();
                    inviteVendorModel.DocflowDetails = objDecisionPointEngine.GetDocFlow();
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + updateVendorModel.CsvFileName;
                    objViewModel = new ViewModel();
                    //called method for update the record in CSV file : 0 used for update record
                    objViewModel.ReadAndWriteContentInCSVForvendor(serverpathname, inviteVendorModel, updateVendorModel, 0);
                    int countIndex = 0;
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForVendor(objCSVReader, inviteVendorModel, countIndex));
                    }
                    objCSVReader.Dispose();
                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    objactionresult = View("InviteVendorBulk", inviteVendorModel);
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// VendorBulkSaveItem
        /// </summary>
        ///  /// <createdby>rohit kaushik</createdby>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createddate>jan 2 2014</createddate>
        /// <returns>saved bulk uploaded vendor</returns>
        [HttpPost]
        public ActionResult VendorBulkSaveItem(string CsvFileName)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    List<SendInvitation> listSendInvitation = new List<Models.SendInvitation>();
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    inviteVendorModel = new InviteVendorBulk();
                    int countIndex = 0;
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + CsvFileName;
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    objViewModel = new ViewModel();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForIC(objCSVReader, inviteVendorModel, countIndex));
                    }
                    objCSVReader.Dispose();
                    //send the invitation to uploaded users throw CSV file
                    for (int i = 0; i < inviteVendorModel.LstVendorCsv.Count(); i++)
                    {
                        objSendInvitation = new Models.SendInvitation();
                        objSendInvitation.firstName = inviteVendorModel.LstVendorCsv[i].FName;
                        objSendInvitation.lastName = inviteVendorModel.LstVendorCsv[i].LName;
                        objSendInvitation.titleId = 0;
                        objSendInvitation.permissionId = 0;
                        objSendInvitation.emailId = inviteVendorModel.LstVendorCsv[i].EmailId;
                        objSendInvitation.companyId = inviteVendorModel.LstVendorCsv[i].CompanyId;
                        objSendInvitation.companyName = inviteVendorModel.LstVendorCsv[i].CompanyName;
                        objSendInvitation.flowId = inviteVendorModel.LstVendorCsv[i].FlowId;
                        objSendInvitation.docflowId = inviteVendorModel.LstVendorCsv[i].DocFlowId;
                        objSendInvitation.PaymentType = Shared.Two;
                        objSendInvitation.vendorTypeId = inviteVendorModel.LstVendorCsv[i].VTID;
                        objSendInvitation.type = Shared.VendorWithoutId;
                        if (i > 4)
                        {
                            objSendInvitation.IsMailSent = false;
                        }
                        else
                        {
                            objSendInvitation.IsMailSent = true;
                        }

                        listSendInvitation.Add(objSendInvitation);

                    }
                    //call method for invite Vendor(s)/Client(s)
                    SendInvitation(listSendInvitation);
                    inviteVendorModel.LstVendorCsv = null;
                    inviteVendorModel.flowDetails = null;
                    inviteVendorModel.CsvFileName = string.Empty;
                    objDecisionPointEngine = new DecisionPointEngine();
                    //get the permission details of particular login users
                    objactionresult = View("InviteVendorBulk", inviteVendorModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Download Vendor Invite Sample
        /// </summary>
        /// <param name="Format">format with id or without id</param>
        /// <createdby>sumit saurav</createdby>       
        /// <createddate>jan 3 2014</createddate>
        /// <returns>invite format file </returns>
        public FileResult DownloadVendorInviteSample(string Format)
        {
            if (Format == "vendorwithid")
            {
                var fileName = "VendorListWithID.csv";
                var filePath = Server.MapPath("~/Content/documents/Sample/VendorListWithID.csv");
                return File(filePath, "application/octet-stream", fileName);
            }
            else
            {
                var fileName = "VendorListWithoutID.csv";
                var filePath = Server.MapPath("~/Content/documents/Sample/VendorListWithoutID.csv");
                return File(filePath, "application/octet-stream", fileName);
            }


        }


        #endregion

        #region IC
        /// <summary>
        /// ICBulkSaveItem
        /// </summary>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view of ic bulk upload</returns>
        [HttpPost]
        public ActionResult ICBulkSaveItem(string CsvFileName)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["ComoanyId"], CultureInfo.InvariantCulture);
                    List<SendInvitation> listSendInvitation = new List<Models.SendInvitation>();
                    int countIndex = 0;
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + CsvFileName;
                    inviteVendorModel = new InviteVendorBulk();
                    if (!object.Equals(Session["UserType"], null))
                    {
                        if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("SuperAdmin"))
                        {
                            inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, companyId).Select(x => new VendorTypeResponse { VendorTypeName = x.categoryName, VendorTypeId = x.id });
                        }
                        else { inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }
                    }
                    else { inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    objViewModel = new ViewModel();
                    //Read the CSV file after update the record
                    objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCSVReader.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForIC(objCSVReader, inviteVendorModel, countIndex));
                    }
                    objCSVReader.Dispose();


                    string vendorType = string.Empty;

                    if (inviteVendorModel.LstVendorCsv[0].CheckType == Shared.IcWithId)
                    {
                        vendorType = Shared.IcWithId;
                    }
                    else
                    {
                        vendorType = Shared.IcWithoutId;
                    }

                    for (int i = 0; i < inviteVendorModel.LstVendorCsv.Count(); i++)
                    {
                        try
                        {
                            if (inviteVendorModel.LstVendorCsv[i].PaymentID != 0 && inviteVendorModel.LstVendorCsv[i].FlowId != 0)
                            {

                                objSendInvitation = new Models.SendInvitation();
                                objSendInvitation.firstName = inviteVendorModel.LstVendorCsv[i].FName;
                                objSendInvitation.lastName = inviteVendorModel.LstVendorCsv[i].LName;
                                objSendInvitation.titleId = 0;
                                objSendInvitation.permissionId = 0;
                                objSendInvitation.emailId = inviteVendorModel.LstVendorCsv[i].EmailId;
                                objSendInvitation.companyId = inviteVendorModel.LstVendorCsv[i].CompanyId;
                                objSendInvitation.companyName = inviteVendorModel.LstVendorCsv[i].CompanyName;
                                objSendInvitation.flowId = inviteVendorModel.LstVendorCsv[i].FlowId;
                                objSendInvitation.docflowId = inviteVendorModel.LstVendorCsv[i].DocFlowId;
                                objSendInvitation.PaymentType = Convert.ToString(inviteVendorModel.LstVendorCsv[i].PaymentID, CultureInfo.InvariantCulture);
                                objSendInvitation.type = vendorType;
                                objSendInvitation.vendorTypeId = inviteVendorModel.LstVendorCsv[i].VTID;
                                objSendInvitation.IsBackgroundCheck = true;
                                if (inviteVendorModel.LstVendorCsv[i].BGCheck.Equals(Shared.No, StringComparison.OrdinalIgnoreCase))
                                {
                                    objSendInvitation.IsBackgroundCheck = true;
                                }
                                if (i > 4)
                                {
                                    objSendInvitation.IsMailSent = false;
                                }
                                else
                                {
                                    objSendInvitation.IsMailSent = true;
                                }
                                listSendInvitation.Add(objSendInvitation);


                            }
                        }
                        catch (Exception ex)
                        {
                            log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                            objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
                        }
                    }
                    SendInvitation(listSendInvitation);
                    inviteVendorModel.LstVendorCsv = null;
                    inviteVendorModel.flowDetails = null;
                    inviteVendorModel.CsvFileName = string.Empty;
                    ViewData.Model = inviteVendorModel;
                    objactionresult = RedirectToAction("InviteICBulk", "Invitation");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion

        #endregion

        #region Incomming
        /// <summary>
        /// Get the incomming details
        /// </summary>
        /// <param name="">string id </param>
        ///  <createdby>bobis</createdby>       
        /// <createddate>dec 26 2014</createddate>
        /// <returns>incoming page with details</returns>
        [HttpGet]
        public ActionResult Incomming(string id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);


                    //Called method for get documents details of Individual and send it to view property for UI
                    objUserDashBoard.DocumentsDetails = objDecisionPointEngine.GetDocumentsDetails(userId, Shared.Incomming, companyId, id);
                    if (objUserDashBoard.DocumentsDetails != null)
                    {
                        objUserDashBoard.PageSize = objUserDashBoard.DocumentsDetails.Count();
                    }
                    objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    objUserDashBoard.ReferenceDetails = objDecisionPointEngine.GetUserReference(companyId);
                    objUserDashBoard.CategoryDetails = objDecisionPointEngine.GetUserCategory(companyId);
                    objUserDashBoard.GroupDetails = objDecisionPointEngine.GetGroup("user", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("Incomming", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion

        #region SetUP training
        /// <summary>
        /// Save basic details fo setup training
        /// </summary>
        /// <param name="objCommunicationBasicDetails">CommunicationBasicDetails class</param>
        ///<createdby>bobis</createdby>
        ///<createddate>feb 14 2014</createddate>
        /// <returns>save communication success or not?</returns>
        [HttpPost, ValidateInput(false)]
        public string SaveCommunication(CommunicationBasicDetails objCommunicationBasicDetails)
        {
            logMessage = new StringBuilder();
            string response = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    CommunicationBasicDetailsRequest objCommunicationBasicDetailsRequest = new CommunicationBasicDetailsRequest()
                    {
                        DocTitle = objCommunicationBasicDetails.DocTitle,
                        DocType = objCommunicationBasicDetails.DocType,
                        DueDate = Convert.ToDateTime(objCommunicationBasicDetails.DueDate),
                        Introduction = objCommunicationBasicDetails.Instruction,
                        Reference = objCommunicationBasicDetails.Reference,
                        Group = objCommunicationBasicDetails.Group,
                        RequHirestaff = objCommunicationBasicDetails.RequHirestaff,
                        RequHireic = objCommunicationBasicDetails.RequHireic,
                        RequHirevendor = objCommunicationBasicDetails.RequHirevendor,
                        retake = objCommunicationBasicDetails.retake,
                        EffectiveDate = Convert.ToDateTime(objCommunicationBasicDetails.EffectiveDate),
                        HOC = objCommunicationBasicDetails.HOC,
                        DaysToComplete = objCommunicationBasicDetails.DaysToComplete,
                        CompanyId = companyId,
                        DocTitles = objCommunicationBasicDetails.DocTitles,
                        VideoTitles = objCommunicationBasicDetails.VideoTitles,
                        ScormTitles = objCommunicationBasicDetails.ScormTitles,
                        Onstaging = objCommunicationBasicDetails.Onstaging,
                        OnLib = objCommunicationBasicDetails.Onlib,
                        IsEmpReq = objCommunicationBasicDetails.IsEmpReq
                    };
                    objCommunicationBasicDetailsRequest.UserId = userId;
                    response = objDecisionPointEngine.SaveCommunication(objCommunicationBasicDetailsRequest, objCommunicationBasicDetails.type, objCommunicationBasicDetails.Docid);
                    //  if (!objCommunicationBasicDetails.type.Equals("incomingupdate"))
                    // {
                    Session["docid"] = Convert.ToString(response.Split(':')[0], CultureInfo.InvariantCulture);
                    Session["doctype"] = objCommunicationBasicDetails.DocType;
                    Session["DueDate"] = objCommunicationBasicDetails.DueDate;
                    Session["versionno"] = Convert.ToString(response.Split(':')[1], CultureInfo.InvariantCulture);
                    //}

                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;

        }
        /// <summary>
        /// Save the links incuded in setup training
        /// </summary>
        /// <param name="docId"> int doc id</param>
        /// <param name="linkValue">string link value</param>
        /// <param name="type">string type</param>
        /// <param name="linkid">int linkid</param>
        /// <createdby>bobis</createdby>
        ///<createddate>feb 14 2014</createddate>
        /// <returns>int type saved or not?</returns>
        [HttpPost]
        public int SaveCommLinks(int docId, string linkValue, string type, int linkid)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    if (!linkValue.StartsWith("http://"))
                    {
                        linkValue = "http://" + linkValue;
                    }
                    response = objDecisionPointEngine.SaveCommLinks(docId, linkValue, type, linkid);
                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// Save the links incuded in setup training
        /// </summary>
        /// <param name="docId">int doc id</param>
        /// <param name="linkValue">string link values</param>
        /// <param name="reqActionval">string reqActionval</param>
        /// <param name="type">string type</param>
        /// <param name="reqactionid">int reqactionid</param>
        /// <createdby>bobis</createdby>
        ///<createddate>feb 14 2014</createddate>
        /// <returns>int type saved or not</returns>
        [HttpPost]
        public int SaveCommReqAction(int docId, string reqActionval, string type, int reqactionid)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.SaveCommReqActions(docId, reqActionval, type, reqactionid);
                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// Save the links incuded in setup training
        /// </summary>
        /// <param name="objCommunicationAssessment">CommunicationAssessment class</param>
        /// <createdby>bobis</createdby>
        ///<createddate>feb 17 2014</createddate>
        /// <returns>saved or not int type</returns>
        [HttpPost]
        public int SaveCommQuesAns(CommunicationAssessment objCommunicationAssessment)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    CommunicationAssessmentRequest objCommunicationAssessmentRequest = new CommunicationAssessmentRequest();
                    objCommunicationAssessmentRequest.docId = objCommunicationAssessment.docId;
                    objCommunicationAssessmentRequest.type = objCommunicationAssessment.type;
                    objCommunicationAssessmentRequest.question = objCommunicationAssessment.question;
                    objCommunicationAssessmentRequest.savestatus = objCommunicationAssessment.savestatus;
                    objCommunicationAssessmentRequest.assessmentid = objCommunicationAssessment.assessmentid;
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.SaveCommQuesAns(objCommunicationAssessmentRequest);
                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// Save the links incuded in setup training
        /// </summary>
        /// <param name="objCommunicationContent">CommunicationContent class</param>
        /// <createdby>bobis</createdby>
        ///<createddate>feb 17 2014</createddate>
        /// <returns>int type save communication content</returns>
        [HttpPost]
        public int SaveCommContents(CommunicationContent objCommunicationContent)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();

                    CommContentRequest commContentRequest = new CommContentRequest()
                    {
                        docId = objCommunicationContent.docId,
                        files = objCommunicationContent.docfiles,
                        filetype = "document",
                        userId = userId,
                        type = objCommunicationContent.type,
                        title = objCommunicationContent.doctitle,
                        scormname = objCommunicationContent.scormname,

                    };

                    //documents
                    response = objDecisionPointEngine.SaveCommContents(commContentRequest);

                    //video
                    commContentRequest.filetype = "video";
                    commContentRequest.files = objCommunicationContent.videofiles;
                    response = objDecisionPointEngine.SaveCommContents(commContentRequest);
                    commContentRequest.filetype = "scorm";
                    commContentRequest.files = objCommunicationContent.scormfiles;
                    response = objDecisionPointEngine.SaveCommContents(commContentRequest);

                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// Save the links incuded in setup training
        /// </summary>
        /// <param name="objCommunicationTestRules">CommunicationTestRules class</param>        
        /// <createdby>bobis</createdby>
        ///<createddate>feb 18 2014</createddate>
        /// <returns>int type saved or not</returns>
        [HttpPost]
        public int SaveCommTestRules(CommunicationTestRules objCommunicationTestRules)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    CommTestRulesRequest objCommTestRulesRequest = new CommTestRulesRequest();
                    objCommTestRulesRequest.RandQues = objCommunicationTestRules.RandQues;
                    objCommTestRulesRequest.RandAns = objCommunicationTestRules.RandAns;
                    objCommTestRulesRequest.ReqReTest = objCommunicationTestRules.ReqReTest;
                    objCommTestRulesRequest.PassingScore = objCommunicationTestRules.PassingScore;
                    objCommTestRulesRequest.ShowWrongeAns = objCommunicationTestRules.ShowWrongeAns;
                    objCommTestRulesRequest.Attempts = objCommunicationTestRules.Attempts;
                    objCommTestRulesRequest.Instruction = objCommunicationTestRules.Instruction;
                    objCommTestRulesRequest.docId = objCommunicationTestRules.docId;
                    objCommTestRulesRequest.Testruleid = objCommunicationTestRules.Testruleid;
                    response = objDecisionPointEngine.SaveCommTestRules(objCommTestRulesRequest, objCommunicationTestRules.type);
                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }
        /// <summary>
        /// communication receipent 
        /// </summary>
        /// <param name="id">string id</param>
        /// <createdby>bobis</createdby>
        ///<createddate>feb 26 2014</createddate>
        /// <returns>view CommunicationRecipient with data</returns>
        [HttpGet]
        public ActionResult CommunicationRecipient(string id)
        {
            logMessage = new StringBuilder();
            // string rolefilter = string.Empty;
            string titlefilter = string.Empty;
            string servicefilter = string.Empty;
            string locationfilter = string.Empty;
            List<string> typefilter = new List<string>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);

                    if (id.Equals(Shared.Zero))
                    {
                        TempData["Type"] = Shared.One;
                    }
                    else if (!string.IsNullOrEmpty(id) && id != Shared.Zero)
                    {

                        objComapnyDashBoard.CommFilterValue = objDecisionPointEngine.GetCommFilterDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), companyId);
                        #region Set Filters
                        //if (objComapnyDashBoard.CommFilterValue != null && objComapnyDashBoard.CommFilterValue.Count > 0)
                        //{
                        //    //set type filter
                        //    foreach (var types in objComapnyDashBoard.CommFilterValue.Where(x => x.FilterType == Shared.Type).Select(x => x.FilterValue))
                        //    {
                        //        if (string.IsNullOrEmpty(typefilter))
                        //        {
                        //            typefilter = types;
                        //        }
                        //        else
                        //        {
                        //            typefilter = typefilter + Shared.Comma + types;
                        //        }
                        //    }
                        //    //set title filter
                        //    foreach (var title in objComapnyDashBoard.CommFilterValue.Where(x => x.FilterType == Shared.Title).Select(x => x.FilterValue))
                        //    {
                        //        if (string.IsNullOrEmpty(titlefilter))
                        //        {
                        //            titlefilter = title;
                        //        }
                        //        else
                        //        {
                        //            titlefilter = titlefilter + Shared.Comma + title;
                        //        }
                        //    }
                        //    //set service filter
                        //    foreach (var service in objComapnyDashBoard.CommFilterValue.Where(x => x.FilterType == Shared.Service).Select(x => x.FilterValue))
                        //    {
                        //        if (string.IsNullOrEmpty(servicefilter))
                        //        {
                        //            servicefilter = service;
                        //        }
                        //        else
                        //        {
                        //            servicefilter = servicefilter + Shared.Comma+ service;
                        //        }
                        //    }
                        //    //set location filter
                        //    foreach (var location in objComapnyDashBoard.CommFilterValue.Where(x => x.FilterType == "location").Select(x => x.FilterValue))
                        //    {
                        //        if (string.IsNullOrEmpty(locationfilter))
                        //        {
                        //            locationfilter = location;
                        //        }
                        //        else
                        //        {
                        //            locationfilter = locationfilter + "," + location;
                        //        }
                        //    }
                        //}
                        #endregion
                        TempData["Type"] = Shared.Zero;
                        filterRequest = new FilterRequest()
                        {
                            CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture),
                            isActive = true,
                            filtertype = Shared.Comm,
                            uType = Shared.Vendor,
                            type = 2,
                            DocId = Convert.ToInt32(id, CultureInfo.InvariantCulture)
                        };
                    }
                    typefilter = objComapnyDashBoard.CommFilterValue.Where(x => x.FilterType == Shared.Type).Select(x => x.FilterValue).ToList();

                    if (typefilter.Count() >= 4)
                    {
                        if (typefilter[0].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Staff.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            objComapnyDashBoard.activeStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest);
                        }
                        if (typefilter[1].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.IC.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            objComapnyDashBoard.activeICdetails = objDecisionPointEngine.GetICdetail(filterRequest);
                        }
                        if (typefilter[2].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Vendor.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            objComapnyDashBoard.vendordetails = objDecisionPointEngine.GetVendorList(filterRequest);
                            //Called method for get Decendants details of Vendor for particular company
                            objComapnyDashBoard.DDvendordetails = objDecisionPointEngine.GetDecentantVendorList(filterRequest);
                        }
                        if (typefilter[2].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.DdVendor.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            //Called method for get Decendants details of Vendor for particular company
                            objComapnyDashBoard.DDvendordetails = objDecisionPointEngine.GetDecentantVendorList(filterRequest);
                        }
                        if (typefilter[3].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Client.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Client;
                            objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                        }
                        if (typefilter[3].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.DdClient.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Client;
                            //Called method for get Decendants details of Vendor for particular company
                            objComapnyDashBoard.DDvendordetails = objDecisionPointEngine.GetDecentantVendorList(filterRequest);
                        }
                    }

                    //get user service
                    objComapnyDashBoard.serviceDetails = objDecisionPointEngine.GetServices("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    //get user titles
                    objComapnyDashBoard.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));

                    if (Session["docid"] != null)
                    {
                        ViewData["DocId"] = id;
                    }
                    //if (Request["type"] == "IN")
                    //{
                    //    ViewData["DocId"] = id;
                    //}
                    //set configuration setting
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {

                        objComapnyDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objComapnyDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                        objComapnyDashBoard.IsICApply = objConfiguratonSettingRequest.IsIc;
                        objComapnyDashBoard.IsCoverageareaApply = objConfiguratonSettingRequest.IsCoveragearea;
                        objComapnyDashBoard.IsServicesApply = objConfiguratonSettingRequest.IsServices;
                    }
                    //check is coverage area DNA or not in this company
                    string coverageAreaStatus = objDecisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                    if (!string.IsNullOrEmpty(coverageAreaStatus))
                    {
                        if (coverageAreaStatus.Trim().ToLower().Equals(Shared.DNA.Trim().ToLower()))
                        {
                            objComapnyDashBoard.IsCoverageareaApply = false;
                        }
                    }
                    objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false).Select(x => new CompanyDashBoardResponse { categoryName = x.VendorTypeName, id = x.VendorTypeId }).ToList();
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView(objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        [HttpPost]
        public ActionResult ViewRecipientList(string docid, string doctype, string versionno, string duedate)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                Session["docid"] = Convert.ToString(docid, CultureInfo.InvariantCulture);
                Session["doctype"] = doctype;
                Session["DueDate"] = duedate;
                Session["versionno"] = Convert.ToString(versionno, CultureInfo.InvariantCulture);
                objactionresult = RedirectToAction("CommunicationRecipient", "CompanyDashBoard", new { id = docid });
                // RedirectToAction("CommunicationRecipient", new { id = docid });

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// FilterCommunicationRecipient
        /// </summary>
        /// <param name="rolefilter">string rolefilter</param>
        /// <param name="servicefilter">string servicefilter</param>
        /// <param name="titlefilter">string titlefilter</param>
        /// <param name="locationfilter">string locationfilter</param>
        /// <param name="typefilter">string typefilter</param>
        /// <createdby>bobis</createdby>
        ///<createddate>apr 4 2014</createddate>
        /// <returns>view communication receipient with data filter as per parameters</returns>
        [HttpPost]
        public ActionResult FilterCommunicationRecipient(FilterModel filterModel)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(filterModel.typefilter))
                    {
                        string[] typefillter = filterModel.typefilter.Split(char.Parse(Shared.Comma));
                        filterRequest = new FilterRequest()
                        {
                            UserId = userId,
                            CompanyId = companyId,
                            type = 2,
                            isActive = true,
                            filtertype = Shared.Comm,
                            DocId = Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture)
                        };
                        //save communication filter
                        if (!string.IsNullOrEmpty(Convert.ToString(Session["docid"], CultureInfo.InvariantCulture)))
                        {
                            if (!string.IsNullOrEmpty(typefillter[0]) || !string.IsNullOrEmpty(typefillter[1]) || !typefillter[2].Equals("notvendor") || !typefillter[3].Equals("notclient"))
                            {
                                filterRequest.typefilter = filterModel.typefilter;
                                filterRequest.filtertype = Shared.Type;
                                objDecisionPointEngine.SaveCommFilterValue(filterRequest, Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                            }
                            if (!string.IsNullOrEmpty(filterModel.titlefilter))
                            {
                                filterRequest.typefilter = filterModel.titlefilter;
                                filterRequest.filtertype = Shared.Title;
                                objDecisionPointEngine.SaveCommFilterValue(filterRequest, Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                            }
                            if (!string.IsNullOrEmpty(filterModel.servicefilter))
                            {
                                filterRequest.typefilter = filterModel.servicefilter;
                                filterRequest.filtertype = Shared.Service;
                                objDecisionPointEngine.SaveCommFilterValue(filterRequest, Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                            }
                            if (!string.IsNullOrEmpty(filterModel.locationfilter))
                            {
                                filterRequest.typefilter = filterModel.locationfilter;
                                filterRequest.filtertype = Shared.Location;
                                objDecisionPointEngine.SaveCommFilterValue(filterRequest, Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                            }
                            if (!string.IsNullOrEmpty(filterModel.professionalfilter) && !string.IsNullOrEmpty(typefillter[1]))
                            {
                                filterRequest.typefilter = filterModel.professionalfilter;
                                filterRequest.filtertype = Shared.ProfessionalType;
                                objDecisionPointEngine.SaveCommFilterValue(filterRequest, Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                            }
                        }

                        #region staff
                        if (typefillter[0].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Staff.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            //Called method for get internal staff details of particular company
                            objComapnyDashBoard.activeStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }
                        #endregion
                        #region vendor

                        if (typefillter[2].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Vendor.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Vendor;
                            //Called method for get internal staff details of particular company
                            objComapnyDashBoard.vendordetails = objDecisionPointEngine.GetVendorList(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }

                        else if (typefillter[2].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.DdVendor.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Vendor;
                            //Called method for get Decendants details of Vendor for particular company
                            objComapnyDashBoard.DDvendordetails = objDecisionPointEngine.GetDecentantVendorList(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }

                        #endregion
                        #region IC
                        if (typefillter[1].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.IC.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            //Called method for get internal staff details of particular company
                            objComapnyDashBoard.activeICdetails = objDecisionPointEngine.GetICdetail(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }
                        #endregion
                        #region Client
                        if (typefillter[3].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.Client.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Client;
                            //Called method for get internal staff details of particular company
                            objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }
                        else if (typefillter[3].Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.DdClient.Trim().ToLower(CultureInfo.InvariantCulture)))
                        {
                            filterRequest.uType = Shared.Client;
                            //Called method for get Decendants details of Vendor for particular company
                            objComapnyDashBoard.DDClientList = objDecisionPointEngine.GetDecentantVendorList(filterRequest);
                            TempData["Type"] = Shared.Zero;
                        }
                        #endregion
                    }
                    //get user service
                    objComapnyDashBoard.serviceDetails = objDecisionPointEngine.GetServices("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    //get user titles
                    objComapnyDashBoard.titleDetails = objDecisionPointEngine.GetTitle("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["docid"], CultureInfo.InvariantCulture)))
                    {
                        objComapnyDashBoard.CommFilterValue = objDecisionPointEngine.GetCommFilterDetails(Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture), companyId);
                    }
                    objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false).Select(x => new CompanyDashBoardResponse { categoryName = x.VendorTypeName, id = x.VendorTypeId }).ToList();
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("CommunicationRecipient", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// post:publish communication
        /// </summary>
        /// <param name="totalStaff">string totalStaff</param>
        /// <param name="totalIC">string totalIC</param>
        /// <param name="totalClient">string totalClient</param>
        /// <param name="totalVendor">string totalVendor</param>
        /// <createdby>bobis</createdby>
        ///<createddate>apr 4 2014</createddate>
        /// <returns>int type communication published or not?</returns>
        [HttpPost]
        public int PublishComm(string totalStaff, string totalIC, string totalClient, string totalVendor)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(Convert.ToString(Session["docid"], CultureInfo.InvariantCulture)))
                    {
                        int docId = Convert.ToInt32(Session["docid"]);
                        string doctype = Convert.ToString(Session["doctype"]);
                        int versoinno = Convert.ToInt32(Session["versionno"]);
                        DateTime duedate = Convert.ToDateTime(Session["DueDate"]);
                        objDecisionPointEngine = new DecisionPointEngine();
                        publishCommRequest objPublishComm = new publishCommRequest();
                        objPublishComm.totalStaff = totalStaff;
                        objPublishComm.totalClient = totalClient;
                        objPublishComm.totalVendor = totalVendor;
                        objPublishComm.totalIC = totalIC;
                        objPublishComm.docId = docId;
                        objPublishComm.dueDate = duedate;
                        objPublishComm.Doctype = doctype;
                        objPublishComm.versionno = versoinno;
                        objPublishComm.Companyid = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                        response = objDecisionPointEngine.PublishComm(objPublishComm, userId);
                    }

                }
                else
                {
                    RedirectToAction("login", "login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return response;
        }

        /// <summary>
        /// Expiredocsession
        /// </summary>
        /// <createdby>bobis</createdby>
        ///<createddate>apr 4 2014</createddate>
        [HttpPost]
        public void Expiredocsession()
        {
            if (Session["docid"] != null)
            {
                Session["docid"] = null;
                Session["doctype"] = null;
                Session["versionno"] = null;
            }
        }


        /// <summary>
        /// delete doc video
        /// </summary>
        /// <param name="name">string name</param>
        /// <param name="id">int id</param>
        /// <createdby>bobis</createdby>
        /// <createddate>apr 4 2014</createddate>       
        [HttpPost]
        public void DeleteDocVideo(string name, int id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Remove video or doc umnet form folder
                string filepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["uploadedcontentpath"], CultureInfo.InvariantCulture) + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)));
                string[] files = Directory.GetFiles(filepath);
                foreach (string filename in files)
                {
                    if (Path.GetFileName(filename) == name)
                    {
                        System.IO.File.Delete(filename);
                    }
                }
                if (!id.Equals(0))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    objDecisionPointEngine.DeleteDocVideo(id, 0);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            // return Json();
        }
        /// <summary>
        /// delete assessment
        /// </summary>
        /// <param name="id">int id</param>
        /// <createdby>bobis</createdby>
        /// <createddate>apr 7 2014</createddate>    
        [HttpPost]
        public void DeleteAssesment(int id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (id != 0)
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    objDecisionPointEngine.DeleteAssesment(id);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            // return Json();
        }

        /// <summary>
        /// GET: /UserDashBoard/Communication
        /// </summary>
        /// <createdby>bobis</createdby>
        /// <createddate>dec 8 2013</createddate>    
        /// <returns>view of communication with data</returns>
        [HttpGet]
        public ActionResult Communication(string id)
        {
            logMessage = new StringBuilder();
            //if (Session["docid"] != null)
            //{
            //    id = Convert.ToString(Session["docid"]);
            //}

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard = new CompanyDashBoard();
                    if (id.Equals("0"))
                    {
                        ViewBag.IsReipientExist = false;
                        objComapnyDashBoard.referenceDetails = objDecisionPointEngine.GetUserReference(companyId);
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetUserCategory(companyId);
                        objComapnyDashBoard.groupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), companyId);
                        string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["uploadedcontentpath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                        objComapnyDashBoard.uploadedcontentpath = folderDirectory;

                        objComapnyDashBoard.hostURL = ViewModel.GetSiteRoot();
                    }
                    else
                    {
                        ViewBag.IsReipientExist = objDecisionPointEngine.GetIsAnyRecipient(Convert.ToInt32(id, CultureInfo.InvariantCulture));
                        IList<CommDetailsResponse> objBasicDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "documentdetail");
                        if (objBasicDetails != null && objBasicDetails.Count > 0)
                        {
                            objComapnyDashBoard.DocTitle = objBasicDetails.ToList()[0].DocTitle;
                            objComapnyDashBoard.DocType = objBasicDetails.ToList()[0].DocType;
                            objComapnyDashBoard.DaysOfCompletion = objBasicDetails.ToList()[0].DaysOfCompletion;
                            objComapnyDashBoard.DueDate = objBasicDetails.ToList()[0].DueDate;
                            objComapnyDashBoard.Reference = objBasicDetails.ToList()[0].Reference;
                            objComapnyDashBoard.Group = objBasicDetails.ToList()[0].Group;
                            objComapnyDashBoard.Introduction = objBasicDetails.ToList()[0].Introduction;
                            objComapnyDashBoard.Reqnewhirestaff = objBasicDetails.ToList()[0].Reqnewhirestaff;
                            objComapnyDashBoard.Reqnewhireic = objBasicDetails.ToList()[0].Reqnewhireic;
                            objComapnyDashBoard.Reqnewhirevendor = objBasicDetails.ToList()[0].Reqnewhirevendor;
                            objComapnyDashBoard.Effectivedate = objBasicDetails.ToList()[0].Effectivedate;
                            objComapnyDashBoard.Retake = objBasicDetails.ToList()[0].Retake;
                            objComapnyDashBoard.Policyno = objBasicDetails.ToList()[0].policyno;
                            objComapnyDashBoard.HourOfCredit = objBasicDetails.ToList()[0].HOC;
                            objComapnyDashBoard.DocTitles = objBasicDetails.ToList()[0].DocTitles;
                            objComapnyDashBoard.VideoTitles = objBasicDetails.ToList()[0].VideoTitles;
                            objComapnyDashBoard.ScormTitles = objBasicDetails.ToList()[0].ScormTitles;
                        }
                        objComapnyDashBoard.linksDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "links");
                        objComapnyDashBoard.contentsDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "content");
                        objComapnyDashBoard.reqactionDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "reqaction");

                        objComapnyDashBoard.assesmentDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "ques");
                        objComapnyDashBoard.ansDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "ans");

                        IList<CommDetailsResponse> objtestruledetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "testrule");
                        if (objtestruledetails != null && objtestruledetails.Count > 0)
                        {
                            objComapnyDashBoard.RandQues = objtestruledetails.ToList()[0].RandQues;
                            objComapnyDashBoard.RandAns = objtestruledetails.ToList()[0].RandAns;
                            objComapnyDashBoard.ShowWrongeAns = objtestruledetails.ToList()[0].ShowWrongeAns;
                            objComapnyDashBoard.ReqReTest = objtestruledetails.ToList()[0].ReqReTest;
                            objComapnyDashBoard.PassingScore = objtestruledetails.ToList()[0].PassingScore;
                            objComapnyDashBoard.Attempts = objtestruledetails.ToList()[0].Attempts;
                            objComapnyDashBoard.Instruction = objtestruledetails.ToList()[0].Instruction;
                            objComapnyDashBoard.Testruleid = objtestruledetails.ToList()[0].Testruleid;
                        }
                        objComapnyDashBoard.referenceDetails = objDecisionPointEngine.GetUserReference(companyId);
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetCategory("Communication", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), objComapnyDashBoard.Reference, objComapnyDashBoard.Group == null ? string.Empty : objComapnyDashBoard.Group);
                        objComapnyDashBoard.groupDetails = objDecisionPointEngine.GetGroup("user", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                        //objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetUserCategory(CompanyId);
                        string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["uploadedcontentpath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                        objComapnyDashBoard.uploadedcontentpath = folderDirectory;
                        objComapnyDashBoard.hostURL = ViewModel.GetSiteRoot();

                        //Session["docid"] = Convert.ToString(id, CultureInfo.InvariantCulture);
                        //Session["doctype"] = objComapnyDashBoard.DocType;
                        //Session["versionno"] = Convert.ToString(objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "documentdetail").Select(x => x.versionno).FirstOrDefault(), CultureInfo.InvariantCulture);


                    }

                    ViewData["DocId"] = id;

                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        objComapnyDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objComapnyDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                        objComapnyDashBoard.IsICApply = objConfiguratonSettingRequest.IsIc;
                        objComapnyDashBoard.IsCoverageareaApply = objConfiguratonSettingRequest.IsCoveragearea;
                        objComapnyDashBoard.IsScormApply = objConfiguratonSettingRequest.IsScormApply;

                    }

                    ViewData.Model = objComapnyDashBoard;
                    String courseId = Guid.NewGuid().ToString();
                    String redirectUrl = ViewModel.GetSiteRoot() + "/CompanyDashBoard/Communication?id=" + id + "";
                    ViewData["ScormAction"] = ScormCloud.CourseService.GetImportCourseUrl(courseId, redirectUrl);
                    objactionresult = View(objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// Used to remove the rqiured actions from communication
        /// </summary>
        /// <param name="reqactions"></param>
        [HttpGet]
        public int RemoveReqActions(int reqactions, string type)
        {
            int IsDeleted = 0;
            logMessage = new StringBuilder();
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    IsDeleted = objDecisionPointEngine.RemoveReqActions(reqactions, type);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return IsDeleted;
        }
        /// <summary>
        /// get all  state 
        /// </summary>  
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>5 may 2014</createdDate>
        /// <returns>list sub categories </returns>
        [HttpGet]
        public JsonResult getSubCategoryOnbasisOfCategory(string id)
        {

            logMessage = new StringBuilder();
            string source = string.Empty;
            string group = string.Empty;
            try
            {
                objComapnyDashBoard = new Models.CompanyDashBoard();
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    if (id.Equals("All"))
                    {
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetUserCategory(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            string[] sourceandgroup = id.Split('$');

                            if (sourceandgroup.Count() > 0)
                            {
                                source = sourceandgroup[0];
                            }
                            if (sourceandgroup.Count() > 1)
                            {
                                group = sourceandgroup[1];
                            }
                            objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetCategory("Communication", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), source, group);
                        }

                    }
                }
                else
                {
                    objComapnyDashBoard.categoryDetails = null;
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(objComapnyDashBoard.categoryDetails, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get all  state 
        /// </summary>  
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>5 may 2014</createdDate>
        /// <returns>list sub categories </returns>
        [HttpGet]
        public JsonResult getCategoryOnbasisOfGroup(string group)
        {

            logMessage = new StringBuilder();
            objComapnyDashBoard = new Models.CompanyDashBoard();
            try
            {

                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    if (group.Equals("All"))
                    {
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetUserReference(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetReference("Communication", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), group);
                    }
                }
                else
                {
                    objComapnyDashBoard.categoryDetails = null;
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(objComapnyDashBoard.categoryDetails, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// load Uploadscrom page
        /// </summary>
        /// <returns>Uploadscorm</returns>
        ///  /// <createdBy>Sumit saurav</createdBy>
        /// <createdDate>13 Jan 2014</createdDate>
        [HttpGet]
        public ActionResult UploadScorm()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                String courseId = Guid.NewGuid().ToString();
                String redirectUrl = ConfigurationManager.AppSettings["SiteName"] + "/CompanyDashBoard/UploadScorm";
                ViewData["ScormAction"] = ScormCloud.CourseService.GetImportCourseUrl(courseId, redirectUrl);
                ViewData["courseId"] = Request["courseid"];

                //Use closer page as default redirect
                ViewData["redirect"] = Request["redirect"];

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View();

        }
        /// <summary>
        /// preview scrom document
        /// </summary>
        /// <returns>scrom document</returns>
        ///  /// <createdBy>Sumit saurav</createdBy>
        /// <createdDate>13 Jan 2014</createdDate>
        [HttpGet]
        public ActionResult PreviewScorm()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                String courseId = Request["courseid"];

                //Use closer page as default redirect

                String redirectOnExitUrl = ConfigurationManager.AppSettings["SiteName"] + "/CompanyDashBoard/UploadScorm";

                //Get preview url from ScormCloud client lib
                String previewUrl = ScormCloud.CourseService.GetPreviewUrl(courseId, redirectOnExitUrl);

                //Send user on their way
                return Redirect(previewUrl);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            //return View();
        }
        /// <summary>
        /// delete scrom document
        /// </summary>
        /// <returns>json type result</returns>
        ///  <createdBy>Sumit saurav</createdBy>
        /// <createdDate>14 Jan 2014</createdDate>
        [HttpPost]
        public JsonResult DeleteScorm(string courseId, int Id)
        {
            var aa = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                ScormCloud.CourseService.DeleteCourse(courseId, false);
                aa = "sucess";
                if (!Id.Equals(0))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    objDecisionPointEngine.DeleteDocVideo(Id, 0);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(aa, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// check scrom document
        /// </summary>
        /// <returns>json type result</returns>
        ///   <createdBy>Sumit saurav</createdBy>
        /// <createdDate>14 Jan 2014</createdDate>
        [HttpPost]
        public JsonResult CheckUploadedScorm(string courseId)
        {
            var aa = "Fails";
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(courseId))
                {
                    var courseList = ScormCloud.CourseService.GetCourseList();
                    var check = courseList.Select(x => x.CourseId);
                    if (check != null && check.Contains(courseId))
                    {
                        //create registration
                        ScormCloud.RegistrationService.CreateRegistration("Reg-" + courseId, courseId, "Learner_Compliance", "Compliance", "Tracker");
                        //List<RegistrationData> regs = ScormCloud.RegistrationService.GetRegistrationListForCourse(courseId); // get registration list

                        Dictionary<string, string> scoLaunchType = new Dictionary<string, string>();
                        scoLaunchType.Add("scoLaunchType", "1");
                        scoLaunchType.Add("playerLaunchType", "1");
                        ScormCloud.CourseService.UpdateAttributes(courseId, scoLaunchType);

                        ScormCloud.RegistrationService.GetRegistrationSummary("Reg-" + courseId);
                        aa = "Sucess";
                    }
                    else
                    {
                        aa = "Fail";
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(aa, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// add assessment
        /// </summary>
        /// <param name="id">string id</param>
        ///  <createdBy>bobis</createdBy>
        /// <createdDate>16 Jan 2014</createdDate>
        /// <returns>add assessment view</returns>
        [HttpGet]
        public ActionResult AddAssessment(string id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (!id.Equals("0"))
                    {
                        userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                        objDecisionPointEngine = new DecisionPointEngine();
                        objComapnyDashBoard = new CompanyDashBoard();
                        objComapnyDashBoard.assesmentDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "ques");
                        objComapnyDashBoard.ansDetails = objDecisionPointEngine.GetCommDetails(Convert.ToInt32(id, CultureInfo.InvariantCulture), "ans");

                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View("CommAssesment");
        }
        /// <summary>
        /// get:addmessage
        /// </summary>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>16 Jan 2014</createdDate>
        /// <returns>view add message</returns>
        [HttpGet]
        public ActionResult AddMessage()
        {
            return View();
        }
        /// <summary>
        /// get:addcourse
        /// </summary>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>16 Jan 2014</createdDate>
        /// <returns>view add course</returns>
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        /// <summary>
        /// upload
        /// </summary>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>24 dec 2013</createdDate>
        public void upload()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    UploadController.Upload(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture));
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }

        /// <summary>
        /// communication staging
        /// </summary>
        /// <param name="id">string id</param>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>10 apr 2014</createdDate>
        /// <returns>view of communication staging</returns>
        public ActionResult CommunicationStaging(string id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                //if (!string.IsNullOrEmpty(Convert.ToString(Session["docid"], CultureInfo.InvariantCulture)))
                //{
                //    objDecisionPointEngine.UpdateStagingstatus(Convert.ToInt32(Session["docid"], CultureInfo.InvariantCulture));
                //    Expiredocsession();
                //}

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    //Called method for get documents details of Individual and send it to view property for UI
                    objUserDashBoard.DocumentsDetails = objDecisionPointEngine.GetUnSentDocumentsDetails(userId, companyId);
                    if (Session["docid"] != null)
                    {
                        ViewData["DocId"] = id;
                    }
                    objUserDashBoard.PageSize = objUserDashBoard.DocumentsDetails.Count();
                    objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {

                        objUserDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objUserDashBoard.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoveragearea;
                        objUserDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                        objUserDashBoard.IsICApply = objConfiguratonSettingRequest.IsIc;

                    }
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View(objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion

        #region Mail Footer
        /// <summary>
        /// get:mail footer
        /// </summary>
        /// <createdBy>Nilesh dubey</createdBy>
        /// <createdDate>10 feb 2014</createdDate>
        /// <returns>view of mail footer</returns>
        [HttpGet]
        public ActionResult MailFooter()
        {
            logMessage = new StringBuilder();

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (HttpContext.Session["UserId"] != null)
                {
                    MailFooter objMailFooter = new Models.MailFooter();
                    objDecisionPointEngine = new DecisionPointEngine();
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (objDecisionPointEngine.GetMailFooter(userId) != null)
                    {
                        objMailFooter.Signauture = objDecisionPointEngine.GetMailFooter(userId).Signauture;
                        objMailFooter.SignatureName = objDecisionPointEngine.GetMailFooter(userId).SignatureName;
                        objactionresult = View(objMailFooter);
                    }
                    else
                    {
                        objactionresult = View();
                    }

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// post:mail footer
        /// </summary>
        /// <param name="objMail">MailFooter class</param>
        /// <createdBy>nilesh dubey</createdBy>
        /// <createdDate>10 feb 2014</createdDate>
        /// <returns>view mail footer</returns>
        [HttpPost]
        public ActionResult MailFooter(MailFooter objMail)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();

                if (HttpContext.Session["UserId"] != null)
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (ModelState.IsValid)
                    {
                        MailFooterRequest objMailFooterRequest = new MailFooterRequest();
                        objMailFooterRequest.UserID = userId;
                        objMailFooterRequest.SignatureName = objMail.SignatureName;
                        objMailFooterRequest.Signauture = objMail.Signauture;
                        int a = objDecisionPointEngine.MailFooter(objMailFooterRequest);
                        if (a == 1)
                        {
                            ViewBag.Msg = DecisionPointR.MailfooterInserted; // "Inserted Successfully..";
                        }
                        else
                        {
                            ViewBag.Msg = DecisionPointR.MailfooterUpdated;  //"Updated Successfully..";
                        }
                    }
                    objactionresult = PartialView("MailFooter");
                }

                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        #endregion

        #region Service Translation Table
        /// <summary>
        /// Service Translation Table
        /// </summary>
        /// <createdBy>Sumit saurav</createdBy>
        /// <createdDate>10 feb 2014</createdDate>
        /// <returns>view of service translation table with data</returns>
        [HttpGet]
        public ActionResult ServiceTranslationTable()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (Request["Type"] == "IC")
                    {
                        objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetICClientList(userId, true);
                    }
                    else
                    {
                        filterRequest = new FilterRequest()
                        {
                            CompanyId = companyId,
                            // rolefilter = string.Empty,
                            servicefilter = string.Empty,
                            titlefilter = string.Empty,
                            locationfilter = string.Empty,
                            typefilter = string.Empty,
                            isActive = true,
                            filtertype = "none",
                            uType = "Client",
                        };
                        objComapnyDashBoard.CurrentvendorsList = objDecisionPointEngine.GetVendorList(filterRequest);
                    }
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.CurrentvendorsList.Count();
                    TempData["isCurrent"] = "1";

                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ServiceTranslationTable", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// service translation table
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 feb 2014</createdDate>
        /// <returns>view of service translation table</returns>
        [HttpGet]
        public ActionResult ServiceTranslation(string CompanyId, string CompanyName)
        {
            if (object.Equals(Session["UserId"], null))
            {
                return RedirectToAction("login", "login");
            }
            else
            {
                logMessage = new StringBuilder();
                try
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    ServiceTranslationModel serviceTranslationModel = new ServiceTranslationModel();
                    serviceTranslationModel.ChildServiceList = objDecisionPointEngine.GetServices("company", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    serviceTranslationModel.ParentServiceList = objDecisionPointEngine.GetServices("company", Convert.ToString(CompanyId, CultureInfo.InvariantCulture));
                    serviceTranslationModel.MappdedServiceList = objDecisionPointEngine.GetMappdedServices(Convert.ToString(CompanyId, CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    serviceTranslationModel.companyName = CompanyName;
                    serviceTranslationModel.pCompanyId = CompanyId;
                    ViewData.Model = serviceTranslationModel;
                }
                catch (Exception ex)
                {
                    log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

                }
                finally
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                    log.Info(logMessage.ToString());
                }
                return View();
            }
        }
        /// <summary>
        /// Action Used for sdave service translation table
        /// </summary>
        /// <param name="parentCompanyId">string parentCompanyId</param>
        /// <param name="">string mappedServices</param>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>20 mar 2014</createdDate>
        /// <returns>saved or not in int type</returns>
        [HttpPost]
        public int SaveServiceTranslation(string parentCompanyId, string mappedServices)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objServiceTranslationTableRequest = new ServiceTranslationTableRequest();
                    objServiceTranslationTableRequest.ChildCompanyId = companyId;
                    objServiceTranslationTableRequest.ParentCompanyId = parentCompanyId;
                    objServiceTranslationTableRequest.MappedServices = mappedServices;
                    objServiceTranslationTableRequest.CreatedByid = userId;
                    Isinserted = objDecisionPointEngine.SaveServiceTranslation(objServiceTranslationTableRequest);

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        #endregion

        #region Super Admin
        /// <summary>
        /// Get Companies list
        /// </summary>
        /// <returns></returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Feb 6 2013</createdDate>
        [HttpGet]
        public ActionResult GetCompanies(int id, string search, int type)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["superAdmin"])))
                {

                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (id.Equals(1))
                    {
                        TempData["isCurrent"] = "1";
                    }
                    else { TempData["isCurrent"] = "0"; }
                    if (!object.Equals(Session["SearchType"], null))
                    {
                        if (type.Equals(Convert.ToInt32(Session["SearchType"], CultureInfo.InvariantCulture)) || type.Equals(3))
                        {
                            type = Convert.ToInt32(Session["SearchType"], CultureInfo.InvariantCulture);
                        }
                    }
                    objSuperAdminViewModel = new SuperAdminViewModel();
                    objViewModel = new ViewModel();
                    objSuperAdminDashBoard = new SuperAdminDashBoard();
                    objSuperAdminDashBoard = objSuperAdminViewModel.GetMyPartnersDetails(id, search, type);
                    Session["SearchType"] = type;
                    objactionresult = PartialView("GetCompanies", objSuperAdminDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        public string GetCompanylogo(string type)
        {
            logMessage = new StringBuilder();
            string Companylogo = string.Empty;
            int UserId = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (type.Equals("staff"))
                    {
                        UserId = objDecisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        UserId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    }

                    //Called method for get My profile details of Individual and send it to view property for UI
                    UserDashBoardResponse objUserDashBoardResponse = new UserDashBoardResponse();
                    objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(UserId);
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
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Companylogo;
        }
        /// <summary>
        /// method to get list of companies
        /// </summary>
        /// <param name="companyID">string</param>
        /// <param name="businessName">string</param>
        /// <returns>View</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Feb 13 2014</createdDate>
        public ActionResult CompanyLogin(string companyID, string businessName)
        {
            logMessage = new StringBuilder();
            try
            {
                Expiredocsession();
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
                SuperAdminResponse superAdminResponse = new SuperAdminResponse();
                superAdminResponse = objDecisionPointEngine.LoginDetail(companyID, businessName);
                if (superAdminResponse != null)
                {
                    loginDetailResponse = objDecisionPointEngine.Login(superAdminResponse.UserID, superAdminResponse.Password);
                    if (loginDetailResponse != null)
                    {
                        if (loginDetailResponse.UserType == Shared.Company || loginDetailResponse.UserType == Shared.IC)
                        {
                            IList<string> sessionlist = new List<string>();
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                            {
                                sessionlist = Convert.ToString(Session["parentUserid"]).Split(char.Parse(Shared.Comma)).ToList();
                            }
                            int index = 0;
                            if (sessionlist != null)
                            {
                                if (sessionlist.Count > 0)
                                {
                                    index = sessionlist.Count - 1;
                                    if (Convert.ToInt32(sessionlist[index], CultureInfo.InvariantCulture) != Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture))
                                    {
                                        Session["parentUserid"] = Session["parentUserid"] + Shared.Comma + Session["UserId"];
                                    }
                                }
                                else
                                {
                                    Session["parentUserid"] = Session["UserId"];
                                }
                            }
                            else
                            {
                                Session["parentUserid"] = Session["UserId"];
                            }
                            HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                            Session["UserId"] = loginDetailResponse.UserId;
                            Session["BusinessName"] = loginDetailResponse.BusinessName;
                            Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
                            Session["UserType"] = loginDetailResponse.UserType;
                            Session["CompanyId"] = loginDetailResponse.CompanyId;
                            Session["IsTemp"] = loginDetailResponse.IsTemp;
                            Session["logopath"] = GetCompanylogo(Shared.None);
                            Session["parentform"] = "partners";
                            if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                            {
                                sessionlist = Convert.ToString(Session["parentUserid"]).Split(',').ToList();
                            }
                            if (sessionlist.Count > 0)
                            {

                                if (Convert.ToInt32(sessionlist[index], CultureInfo.InvariantCulture) != Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture))
                                {
                                    Session["parentUserid"] = Session["parentUserid"] + "," + Session["UserId"];
                                }
                            }
                            else
                            {
                                Session["parentUserid"] = Session["UserId"];
                            }
                            userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                            objactionresult = RedirectToAction("MyDashBoard", "CompanyDashBoard", new { date = DateTime.Now.Date, type = 1 });
                            //objactionresult = RedirectToAction("GetCompanies", new { id = 1, type = 1 });
                        }
                        else
                        {
                            objactionresult = RedirectToAction("GetCompanies", new { id = 1, type = 1 });
                        }
                    }
                }
                else
                {
                    objactionresult = RedirectToAction("GetCompanies", new { id = 1, type = 1 });
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Get company Profile and fee structure
        /// </summary>
        /// <param name="userID">int</param>
        /// <param name="companyID">string</param>
        /// <param name="busniessName">string</param>
        /// <returns>ActionResult</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>12 jan 2014</createdDate>
        /// <modifyby>bobi</modifyby>
        /// <modefieddate>16 may 2014</modefieddate>
        [HttpGet]
        public ActionResult FeeCreation(int userID, string companyID)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objUserDashBoardResponse = new UserDashBoardResponse();
                    UserDashBoard objUserDashBoard = new UserDashBoard();
                    //get fee details
                    objUserDashBoardResponse = objDecisionPointEngine.GetFeeDetail(userID, companyID);
                    if (objUserDashBoardResponse != null)
                    {
                        objUserDashBoard.CompanyName = objUserDashBoardResponse.companyName;
                        objUserDashBoard.FName = objUserDashBoardResponse.fName;
                        objUserDashBoard.LName = objUserDashBoardResponse.lName;
                        objUserDashBoard.MName = objUserDashBoardResponse.mName;
                        objUserDashBoard.CompanyFee = objUserDashBoardResponse.CompanyFee;
                        objUserDashBoard.PerOfficeStaffFee = objUserDashBoardResponse.PerOfficeStaffFee;
                        objUserDashBoard.PerFieldStaffFee = objUserDashBoardResponse.PerFieldStaffFee;
                        objUserDashBoard.PerICFee = objUserDashBoardResponse.PerICFee;
                        objUserDashBoard.EmailId = objUserDashBoardResponse.emailId;
                        objUserDashBoard.OfficePhone = objUserDashBoardResponse.officePhone;
                        objUserDashBoard.DirectPhone = objUserDashBoardResponse.directPhone;
                        objUserDashBoard.IsInvoice = objUserDashBoardResponse.IsInvoice;
                        objUserDashBoard.UserType = objUserDashBoardResponse.UserType;
                        objUserDashBoard.IsFreeBasicMembership = objUserDashBoardResponse.IsFreeBasicMembership;
                    }
                    objUserDashBoard.CompanyId = companyID;
                    objUserDashBoard.Id = userID;
                    objactionresult = View("FeeCreation", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return objactionresult;
        }
        /// <summary>
        /// edit fee
        /// </summary>
        /// <param name="objUserDashBoard">UserDashBoard class</param>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <modifiedby>sumit saurav</modifiedby>
        /// <createdDate>12 jan 2014</createdDate>
        /// <returns>view fee creation</returns>
        [HttpPost]
        public ActionResult EditFee(UserDashBoard objUserDashBoard)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    int userID = objUserDashBoard.Id;
                    objDecisionPointEngine = new DecisionPointEngine();
                    UserDashBoardRequest objUserDashBoardRequest = new UserDashBoardRequest();
                    objUserDashBoardRequest.CompanyCode = objUserDashBoard.CompanyId;
                    objUserDashBoardRequest.PerFieldStaffFee = objUserDashBoard.PerFieldStaffFee;
                    objUserDashBoardRequest.PerOfficeStaffFee = objUserDashBoard.PerOfficeStaffFee;
                    objUserDashBoardRequest.PerICFee = objUserDashBoard.PerICFee;
                    objUserDashBoardRequest.CompanyFee = objUserDashBoard.CompanyFee;
                    objUserDashBoardRequest.IsInvoice = objUserDashBoard.IsInvoice;

                    byte result = objDecisionPointEngine.SetFeeDetail(objUserDashBoardRequest);
                    if (result == 1)
                    {
                        TempData["SucessMessage"] = "Sucessfully saved";// javaScriptSerializer.Serialize(html);
                    }
                    else if (result == 2)
                    {
                        TempData["SucessMessage"] = "Sucessfully saved";// javaScriptSerializer.Serialize(html);
                    }
                    if (!objUserDashBoard.SaveClose)
                    {
                        objactionresult = RedirectToAction("FeeCreation", new { userID = userID, companyID = objUserDashBoard.CompanyId, busniessName = objUserDashBoard.CompanyName });
                    }
                    else
                    {
                        objactionresult = RedirectToAction("GetCompanies", new { id = 1, type = 1 });
                    }

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// default fee creation
        /// </summary>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>16 jan 2014</createdDate>
        /// <modifyby>bobi</modifyby>
        /// <modifieddate>16 may 2014</modifieddate>
        /// <returns>view of default fee creation</returns>
        [HttpGet]
        public ActionResult DefaultFeeCreation()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objUserDashBoardResponse = new UserDashBoardResponse();
                    UserDashBoard objUserDashBoard = new UserDashBoard();
                    objUserDashBoardResponse = objDecisionPointEngine.GetDefaultFeeDetail();
                    if (objUserDashBoardResponse != null)
                    {
                        objUserDashBoard.CompanyFee = objUserDashBoardResponse.CompanyFee;
                        objUserDashBoard.PerOfficeStaffFee = objUserDashBoardResponse.PerOfficeStaffFee;
                        objUserDashBoard.PerFieldStaffFee = objUserDashBoardResponse.PerFieldStaffFee;
                        objUserDashBoard.PerICFee = objUserDashBoardResponse.PerICFee;
                        objUserDashBoard.LastUpdatedDate = objUserDashBoardResponse.LastUpdatedDate;
                        objUserDashBoard.IsInvoice = objUserDashBoardResponse.IsInvoice;
                    }

                    objactionresult = PartialView("DefaultFeeCreation", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// post:set deafult fee for users
        /// </summary>
        /// <param name="objUserDashBoard">UserDashBoard class</param>
        /// <createdBy>sumit saurav </createdBy>
        /// <createdDate>16 jan 2014</createdDate>
        /// <returns>view of default fee master with result saved or not?</returns>
        [HttpPost]
        public ActionResult SetDefaultFee(UserDashBoard objUserDashBoard)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    UserDashBoardRequest objUserDashBoardRequest = new UserDashBoardRequest();
                    objUserDashBoardRequest.PerFieldStaffFee = objUserDashBoard.PerFieldStaffFee;
                    objUserDashBoardRequest.PerOfficeStaffFee = objUserDashBoard.PerOfficeStaffFee;
                    objUserDashBoardRequest.PerICFee = objUserDashBoard.PerICFee;
                    objUserDashBoardRequest.CompanyFee = objUserDashBoard.CompanyFee;
                    objUserDashBoardRequest.IsInvoice = objUserDashBoard.IsInvoice;
                    //javaScriptSerializer = new JavaScriptSerializer();
                    byte result = objDecisionPointEngine.SetDefaultFeeDetail(objUserDashBoardRequest);
                    if (result == 1)
                    {
                        TempData["SucessMessage"] = "Sucessfully saved";//javaScriptSerializer.Serialize(html);
                    }
                    else if (result == 2)
                    {
                        TempData["SucessMessage"] = "Sucessfully saved";//javaScriptSerializer.Serialize(html);
                    }
                    ViewData.Model = objUserDashBoard;
                    objactionresult = RedirectToAction("DefaultFeeCreation");

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #region Annoucement

        /// <summary>
        /// get:announcement
        /// </summary>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>20 jan 2014</createdDate>
        /// <returns>view announcement with details</returns>
        [HttpGet]
        public ActionResult Annoucement()
        {
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard = new CompanyDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard.Announcement = objDecisionPointEngine.GetAnnouncement().Where(x => x.IsClose == false).ToList();
                    objComapnyDashBoard.CloseAnnounce = objDecisionPointEngine.GetAnnouncement().Where(x => x.IsClose == true).Select(x => x.Announcement).FirstOrDefault();
                    objComapnyDashBoard.CloseAnnounceId = objDecisionPointEngine.GetAnnouncement().Where(x => x.IsClose == true).Select(x => x.Id).FirstOrDefault();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    if (object.Equals(objComapnyDashBoard.Announcement, null))
                    {
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.Announcement.Count();
                    }
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("Annoucement");
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// post:announcement
        /// </summary>
        /// <param name="objAddAnnouncement">AddAnnouncement class</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>21 jan 2014</createdDate>
        /// <returns>int type saved or not?</returns>
        public int AddAnnoucement(AddAnnouncement objAddAnnouncement)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(objAddAnnouncement.announcement))
                {
                    if (objAddAnnouncement.status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddAnnoucement(objAddAnnouncement.announcement, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                    else if (objAddAnnouncement.status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateAnnoucement(objAddAnnouncement.announcementId, objAddAnnouncement.announcement);
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }

        /// <summary>
        /// enable or disable ammouncement
        /// </summary>
        /// <param name="announcementId">int type announcement</param>
        /// <param name="isActive">string isactive</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>21 jan 2014</createdDate>
        /// <returns>int type saved or not?</returns>
        public int DisaEnaAnnoucement(int announcementId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (isActive.Equals("enable"))
                {
                    isActivestatus = true;
                }
                Isupdated = objDecisionPointEngine.DisaEnaAnnoucement(announcementId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }

        /// <summary>
        /// remove announcement
        /// </summary>
        /// <param name="announcementId">int type announcement id</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>21 jan 2014</createdDate>
        /// <returns>removed or not announcement?</returns>
        public int RemoveAnnoucement(int announcementId)
        {
            objDecisionPointEngine = new DecisionPointEngine();
            if (objDecisionPointEngine.RemoveAnnoucement(announcementId) > 0)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// add close announcement
        /// </summary>
        /// <param name="objAddAnnouncement">AddAnnouncement class</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>22 jan 2014</createdDate>
        /// <returns>int type added or not?</returns>
        public int AddCloseAnnoucement(AddAnnouncement objAddAnnouncement)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(objAddAnnouncement.announcement))
                {

                    Isinserted = objDecisionPointEngine.AddCloseAnnoucement(objAddAnnouncement.announcement, objAddAnnouncement.status, objAddAnnouncement.announcementId, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        #endregion

        /// <summary>
        /// search in company
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>21 jan 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpGet]
        public JsonResult SerachInCompany(string term)
        {
            //Used for display log text
            logMessage = new StringBuilder();

            // string[] serachByVType = null;
            int type = Convert.ToInt32(Session["SearchType"], CultureInfo.InvariantCulture);
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                }

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                objUserDashBoard.Search = objDecisionPointEngine.Search(term, userId, "company", type);

                serach1 = objUserDashBoard.Search.Select(t => string.IsNullOrEmpty(t.serachByTitle) ? string.Empty : t.serachByTitle.ToLowerInvariant()).ToList();

                serach2 = objUserDashBoard.Search.Select(t => string.IsNullOrEmpty(t.serachByFrom) ? string.Empty : t.serachByFrom.ToLowerInvariant()).ToList();

                //serachByVType = objUserDashBoard.Search.Select(t => string.IsNullOrEmpty(t.serachByVType) ? string.Empty : t.serachByVType.ToLowerInvariant()).ToArray();

                serachFinal = serach1.Union(serach2).ToList();

                return this.Json(serachFinal.Where(t => t.StartsWith(term)), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }


        /// <summary>
        /// withdrwan inbox
        /// </summary>
        /// <createdBy>Bobis</createdBy>
        /// <createdDate>22 jan 2014</createdDate>
        /// <returns>view withdrawninbox</returns>
        [HttpGet]
        public ActionResult WithDrawnInbox()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View();
        }

        /// <summary>
        /// save guide instruction
        /// </summary>
        /// <param name="instructions">Instruction class</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>saved or not?</returns>
        [HttpPost]
        public int SaveGuidInstrictions(Instruction instructions)
        {
            int res = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    res = objDecisionPointEngine.SaveInstructions(instructions.ID, instructions.GuidInstruction);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return res;


        }

        /// <summary>
        /// GetGuideInstruction
        /// </summary>
        /// <param name="IsActive">Isactive true or false</param>
        /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>returns instruction on the basis of IsActive</returns>
        [HttpGet]
        public string GetGuideInstruction(bool isActive)
        {
            logMessage = new StringBuilder();
            string instruction = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    instruction = objViewModel.GetGuidInstruction(isActive);
                    if (isActive.Equals(true))
                    {
                        TempData["GuideInstruction"] = instruction;
                        TempData["GuideStatus"] = true;
                    }

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return instruction;
        }

        /// <summary>
        /// SetTempData
        /// </summary>
        /// <param name="instructions">Instruction class</param>
        /// /// <createdBy>mamta gupta</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>int type saved o not?</returns>
        [HttpPost]
        public int SetTempData(Instruction instructions)
        {
            logMessage = new StringBuilder();
            int setTemp = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(instructions.GuidInstruction))
                {
                    TempData.Keep("GuideInstruction");
                    TempData.Keep("GuideStatus");
                    TempData["GuideInstruction"] = instructions.GuidInstruction;
                    TempData["GuideStatus"] = true;
                }
                else
                {
                    TempData.Remove("GuideInstruction");
                    TempData.Remove("GuideStatus");
                }
                setTemp = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return setTemp;

        }
        #endregion

        #region Category
        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="category">string category</param>
        /// <param name="status">string status</param>
        /// <param name="categoryId">int categoryId</param>
        /// <createdBy>bobi s</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>saved or not?</returns>
        [HttpPost]
        public int AddCategory(AddCategory objAddCategory)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(objAddCategory.category))
                {
                    CategoryRequest categoryRequest = new CategoryRequest()
                    {
                        categoryId = objAddCategory.categoryId,
                        categoryName = objAddCategory.category,
                        UserId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                        CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture),
                        sourceId = objAddCategory.sourceId,
                        groupId = objAddCategory.groupId,
                    };
                    if (objAddCategory.status.Equals("Save"))
                    {
                        Isinserted = objDecisionPointEngine.AddCategory(categoryRequest);
                    }
                    else if (objAddCategory.status.Equals("Edit"))
                    {
                        Isinserted = objDecisionPointEngine.UpdateCategory(categoryRequest);
                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// view category
        /// </summary>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>view category</returns>
        public ActionResult ViewCategory(string id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            string source = string.Empty;
            string group = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    //Called method for get category details of particular company
                    if (id.Equals("All"))
                    {
                        objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetCategory("admin", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), string.Empty, string.Empty);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            string[] sourceandgroup = id.Split('$');

                            if (sourceandgroup.Count() > 0)
                            {
                                source = sourceandgroup[0];
                            }
                            if (sourceandgroup.Count() > 1)
                            {
                                group = sourceandgroup[1];
                            }
                            objComapnyDashBoard.categoryDetails = objDecisionPointEngine.GetCategory("Communication", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), source, group);
                        }
                    }
                    if (objComapnyDashBoard.categoryDetails != null)
                    {
                        IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.categoryDetails.OrderBy(q => q.categoryName).ToList();
                        #region

                        #region list
                        List<LstProp> lstTest = new List<LstProp>();

                        int count = GetList.Count();

                        for (int i = 0; i < count; i++)
                        {
                            LstProp lstprop = new LstProp();
                            lstprop.Col1 = GetList[i].categoryName;
                            lstprop.Col1ID = GetList[i].id;
                            lstprop.Col1Status = GetList[i].isActive.Value;
                            lstprop.Col1Source = GetList[i].referenceName;
                            lstprop.Col1GroupName = GetList[i].groupName;
                            lstTest.Add(lstprop);
                        }
                        int row = 0;
                        int column = 0;
                        int fixedColumn = 3;
                        int innerColumnIterator = 0;
                        int actualCount = lstTest.Count() / fixedColumn;
                        int mod = lstTest.Count() % fixedColumn;
                        int rowCount = 0;

                        if (mod == 0)
                            rowCount = actualCount;
                        else
                            rowCount = actualCount + 1;
                        objComapnyDashBoard.lstBind = new List<LstProp>();
                        while (row < rowCount)
                        {
                            while (true)
                            {
                                LstProp lstprop = new LstProp();

                                innerColumnIterator = row;
                                lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                                lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                                lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;
                                lstprop.Col1Source = lstTest[innerColumnIterator].Col1Source;
                                lstprop.Col1GroupName = lstTest[innerColumnIterator].Col1GroupName;
                                if (row == actualCount && mod == 1)
                                {
                                    objComapnyDashBoard.lstBind.Add(lstprop);
                                    break;
                                }

                                if (mod == 0)
                                    innerColumnIterator = innerColumnIterator + actualCount;
                                else
                                    innerColumnIterator = innerColumnIterator + actualCount + 1;

                                lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                                lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                                lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;
                                lstprop.Col2Source = lstTest[innerColumnIterator].Col1Source;
                                lstprop.Col2GroupName = lstTest[innerColumnIterator].Col1GroupName;
                                if (row == actualCount && mod == 2)
                                {
                                    objComapnyDashBoard.lstBind.Add(lstprop);
                                    break;
                                }

                                if (mod < 2)
                                    innerColumnIterator = innerColumnIterator + actualCount;
                                else
                                    innerColumnIterator = innerColumnIterator + actualCount + 1;

                                lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                                lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                                lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;
                                lstprop.Col3Source = lstTest[innerColumnIterator].Col1Source;
                                lstprop.Col3GroupName = lstTest[innerColumnIterator].Col1GroupName;
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                column++;
                                break;
                            }
                            row++;
                        }

                        #endregion
                        #endregion
                    }
                    objComapnyDashBoard.referenceDetails = objDecisionPointEngine.GetUserReference(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    objComapnyDashBoard.groupDetails = objDecisionPointEngine.GetGroup("user", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.lstBind.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewCategory", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// disable enable category
        /// </summary>
        /// <param name="categoryId">int categoryId</param>
        /// <param name="isActive">string isActive</param>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>13 feb 2014</createdDate>
        /// <returns>int type disabled or not return type</returns>
        public int DisaEnaCategory(int categoryId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals("enable"))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaCategory(categoryId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region Group
        /// <summary>
        /// Addgroup
        /// </summary>
        /// <param name="group">string group</param>
        /// <param name="status">string status</param>
        /// <param name="groupId">int groupId</param>
        /// <createdBy>bobi s</createdBy>
        /// <createdDate>19 may 2014</createdDate>
        /// <returns>saved or not?</returns>
        public int AddGroup(string group, string status, int groupId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(group))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddGroup(group, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateGroup(groupId, group, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// view group
        /// </summary>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>19 may 2014</createdDate>
        /// <returns>view category</returns>
        public ActionResult Viewgroup()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    //Called method for get category details of particular company

                    objComapnyDashBoard.groupDetails = objDecisionPointEngine.GetGroup("admin", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));

                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.groupDetails.OrderBy(q => q.categoryName).ToList();
                    #region

                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();

                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].categoryName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstTest.Add(lstprop);
                    }
                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;
                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            LstProp lstprop = new LstProp();

                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;
                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;
                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;

                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }

                    #endregion
                    #endregion
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.lstBind.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ManageGroup", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// disable enable group
        /// </summary>
        /// <param name="groupId">int groupId</param>
        /// <param name="isActive">string isActive</param>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>19 may 2014</createdDate>
        /// <returns>int type disabled or not return type</returns>
        public int DisaEnaGroup(int groupId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals("enable"))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaGroup(groupId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region Vendor Type
        /// <summary>
        /// Add Vendor Type
        /// </summary>
        /// <param name="group">string group</param>
        /// <param name="status">string status</param>
        /// <param name="groupId">int groupId</param>
        /// <createdBy>bobi s</createdBy>
        /// <createdDate>7 july 2014</createdDate>
        /// <returns>saved or not?</returns>
        public int AddVendorType(string vType, string status, int vTypeId)
        {
            int Isinserted = 0;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                if (!string.IsNullOrEmpty(vType))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddVendorType(vType, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateVendorType(vTypeId, vType, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isinserted;
        }
        /// <summary>
        /// view group
        /// </summary>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>7 july 2014</createdDate>
        /// <returns>view vendor Type</returns>
        public ActionResult ViewVendorType()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetVendorType("admin", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    //Called method for get category details of particular company

                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.VendorTypeDetails.OrderBy(q => q.categoryName).ToList();
                    #region

                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();

                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].categoryName;
                        lstprop.Col1ID = GetList[i].id;
                        lstprop.Col1Status = GetList[i].isActive.Value;
                        lstTest.Add(lstprop);
                    }
                    int row = 0;
                    int column = 0;
                    int fixedColumn = 3;
                    int innerColumnIterator = 0;
                    int actualCount = lstTest.Count() / fixedColumn;
                    int mod = lstTest.Count() % fixedColumn;
                    int rowCount = 0;

                    if (mod == 0)
                        rowCount = actualCount;
                    else
                        rowCount = actualCount + 1;
                    objComapnyDashBoard.lstBind = new List<LstProp>();
                    while (row < rowCount)
                    {
                        while (true)
                        {
                            LstProp lstprop = new LstProp();

                            innerColumnIterator = row;
                            lstprop.Col1 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col1ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col1Status = lstTest[innerColumnIterator].Col1Status;
                            if (row == actualCount && mod == 1)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod == 0)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col2 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col2ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col2Status = lstTest[innerColumnIterator].Col1Status;
                            if (row == actualCount && mod == 2)
                            {
                                objComapnyDashBoard.lstBind.Add(lstprop);
                                break;
                            }

                            if (mod < 2)
                                innerColumnIterator = innerColumnIterator + actualCount;
                            else
                                innerColumnIterator = innerColumnIterator + actualCount + 1;

                            lstprop.Col3 = lstTest[innerColumnIterator].Col1;
                            lstprop.Col3ID = lstTest[innerColumnIterator].Col1ID;
                            lstprop.Col3Status = lstTest[innerColumnIterator].Col1Status;

                            objComapnyDashBoard.lstBind.Add(lstprop);
                            column++;
                            break;
                        }
                        row++;
                    }

                    #endregion
                    #endregion
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.lstBind.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ManageVendorType", objComapnyDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        [HttpPost]
        public int AddVendorTypeForCompany(ConfigurationSettingDetail objConfigurationSettingDetail)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            int isInserted = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    VendorTypeRequest objVendorTypeRequest = new VendorTypeRequest()
                    {
                        UserCompanyId = objConfigurationSettingDetail.CompanyId,
                        UserId = objConfigurationSettingDetail.UserId,
                        CreatedBy = userId,
                        CreatorCompanyID = companyId,
                        VendroTypeIds = objConfigurationSettingDetail.VendorTypeId,
                        TYPE = objConfigurationSettingDetail.Type

                    };
                    isInserted = objDecisionPointEngine.AddVendorTypeForCompany(objVendorTypeRequest);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isInserted;
        }

        /// <summary>
        /// disable enable group
        /// </summary>
        /// <param name="groupId">int groupId</param>
        /// <param name="isActive">string isActive</param>
        /// <createdBy>Bobi s</createdBy>
        /// <createdDate>7 july 2014</createdDate>
        /// <returns>int type disabled or not return type</returns>
        public int DisaEnaVendorType(int vTypeId, string isActive)
        {
            int Isupdated = 0;
            bool isActivestatus = false;
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    RedirectToAction("Login", "Login");
                if (isActive.Equals("enable"))
                    isActivestatus = true;
                Isupdated = objDecisionPointEngine.DisaEnaVendorType(vTypeId, isActivestatus);

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Isupdated;
        }
        #endregion

        #region DeleteFileFromDirectory
        /// <summary>
        /// Used for delete the file from directory
        /// </summary>
        /// <param name="fileLocation"></param>
        [HttpPost]
        public void DeleteFileFromDir(DeleteFileFromDirectory objDeleteFileFromDirectory)
        {
            int filetypeid = 1;
            string laststr = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (objDeleteFileFromDirectory.Type.Equals(1))
                    {
                        laststr = Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture);
                    }
                    string filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr);
                    if (objDeleteFileFromDirectory.FileLocType.Equals(1))
                    {
                        filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr);
                    }
                    if (objDeleteFileFromDirectory.FileLocType.Equals(2))
                    {
                        filetypeid = 2;
                        filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture) + laststr);
                    }
                    if (objDeleteFileFromDirectory.FileLocType.Equals(3))
                    {
                        filetypeid = 3;
                        filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture) + laststr);
                    }
                    if (objDeleteFileFromDirectory.FileLocType.Equals(4))
                    {
                        filetypeid = 5;
                        //objViewModel = new ViewModel();
                        //string parentUserId = objViewModel.GetVisitorUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                        filepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture) + laststr);
                    }
                    string[] files = Directory.GetFiles(filepath);
                    foreach (string filename in files)
                    {
                        if (Path.GetFileName(filename).Trim().ToUpper() == Path.GetFileName(objDeleteFileFromDirectory.FileLocation).Trim().ToUpper())
                        {
                            System.IO.File.Delete(filename);
                        }
                    }
                    //delete file from database
                    if (!objDeleteFileFromDirectory.FileId.Equals(0))
                    {
                        objDecisionPointEngine = new DecisionPointEngine();
                        objDecisionPointEngine.DeleteDocVideo(objDeleteFileFromDirectory.FileId, filetypeid);
                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Employement Reqiurement

        /// <summary>
        /// Used for get the Ic employement reqiuredment details
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>bobi</createdby>
        /// <createddate>23 may 2014</createddate>
        [HttpGet]
        public ActionResult ICEmployementReqiuredment(int id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    //create the object to call the method from BAl Layer
                    objDecisionPointEngine = new DecisionPointEngine();
                    //create the model object
                    objEmpReq = new EmpReq();
                    //get the state list
                    objEmpReq.StateList = objDecisionPointEngine.GetStateList();
                    objEmpReq.serviceDetails = objDecisionPointEngine.GetServices("company", companyId);

                    objEmpReq.globauploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture).Split('~')[1] + companyId; ;
                    objEmpReq.specificuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture).Split('~')[1] + companyId; ;

                    objEmpReq.hostURL = ViewModel.GetSiteRoot();
                    objSubmitReqDocRequest = new SubmitReqDocRequest()
                    {
                        UserId = userId,
                        CompanyId = companyId,
                        IsActive = id
                    };
                    if (id == 1)
                    {
                        objSubmitReqDocRequest.JCRType = 1;
                        //get the already saved global req doc details
                        objEmpReq.CurrentGlobalReqDocList = objDecisionPointEngine.GetReqDocBySender(objSubmitReqDocRequest).ToList();

                        //get the already saved specific req doc details
                        objSubmitReqDocRequest.JCRType = 2;
                        objEmpReq.CurrentSpecificReqDocList = objDecisionPointEngine.GetReqDocBySender(objSubmitReqDocRequest).ToList();
                    }
                    else
                    {
                        //get the already saved global req doc details
                        objSubmitReqDocRequest.JCRType = 1;
                        objEmpReq.PastGlobalReqDocList = objDecisionPointEngine.GetReqDocBySender(objSubmitReqDocRequest).ToList();
                        //get the already saved specific req doc details
                        objSubmitReqDocRequest.JCRType = 2;
                        objEmpReq.PastSpecificReqDocList = objDecisionPointEngine.GetReqDocBySender(objSubmitReqDocRequest).ToList();
                    }
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        objEmpReq.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objEmpReq.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                        objEmpReq.IsICApply = objConfiguratonSettingRequest.IsIc;
                        objEmpReq.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoveragearea;
                        objEmpReq.IsServiceApply = objConfiguratonSettingRequest.IsServices;
                    }
                    objEmpReq.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false);
                    ViewData.Model = objEmpReq;
                    objactionresult = View("ICEmployementReqiuredment", objEmpReq);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return objactionresult;
        }

        /// <summary>
        /// upload Emp req Doc
        /// </summary>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>28 may 2014</createdDate>
        public void UploadEmpReqDoc()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string title = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (Request.Cookies["titlename"] != null)
                    {
                        title = Request.Cookies["titlename"].Value;
                        UploadController.Uploadempdocdoc(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), title);
                    }


                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }

        /// <summary>
        /// Used to set the titlename for set the uploaded document
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>28 may 2014</createdDate>
        public string SettheEmpDocName(string title, int type)
        {
            string newFileName = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                int count = 1;
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                string laststr = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (type.Equals(1))
                {
                    laststr = Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture);
                }
                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr;
                if (!string.IsNullOrEmpty(title))
                {
                    if (title.Length > 2)
                    {
                        if (title.Substring(0, 1) == Shared.One && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == Shared.Two && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == Shared.Three && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == "4" && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture) + laststr;
                        }
                        //else if (title.Contains(char.Parse(Shared.DollarSign)))
                        //{
                        //    objViewModel = new ViewModel();
                        //    string parentUserId = objViewModel.GetVisitorUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                        //    if (!string.IsNullOrEmpty(parentUserId))
                        //    {
                        //        folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["BackgroundCheckuploadeddocpath"], CultureInfo.InvariantCulture) + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture) + Shared.UnderScore + parentUserId;
                        //    }
                        //    else
                        //    {
                        //        folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["BackgroundCheckuploadeddocpath"], CultureInfo.InvariantCulture) + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture);
                        //    }
                        //}
                    }

                    //set the new file name
                    if (Directory.Exists(HttpContext.Server.MapPath(folderDirectory)))
                    {

                        foreach (var fn in Directory.GetFiles(HttpContext.Server.MapPath(folderDirectory)))
                        {
                            if (!string.IsNullOrEmpty(Path.GetFileName(fn)))
                            {
                                if (title.Contains(char.Parse(Shared.DollarSign)))
                                {
                                    if (Path.GetFileName(fn).Split(char.Parse(Shared.ExclamationMark))[1].Split(char.Parse(Shared.UnderScore))[0].Equals(Regex.Replace(title.Split(char.Parse(Shared.DollarSign))[1].Substring(1, (title.Split(char.Parse(Shared.DollarSign))[1].Length - 1)), @"\s+", " ")))
                                    {
                                        count += 1;
                                    }
                                }
                                else
                                {
                                    if (Path.GetFileName(fn).Split('_')[0].Equals(Regex.Replace(title.Substring(1, (title.Length - 1)), @"\s+", " ")))
                                    {
                                        count += 1;
                                    }
                                }
                            }
                        }
                        //set the file name as per title
                        newFileName = title + Shared.UnderScore + count;
                    }
                    else
                    {
                        //set the file name as per title
                        newFileName = title + Shared.UnderScore + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return newFileName;
        }

        /// <summary>
        /// Used for set reqiure document for staff and ic
        /// </summary>
        /// <param name="objSubmitReqDoc">objSubmitReqDoc</param>
        ///  <createdby>bobi</createdby>
        ///  <createdDate>28 may 2014</createdDate>
        /// <returns>SubmitReqDocuments</returns>
        [HttpPost]
        public int SubmitReqDocuments(SubmitReqDoc objSubmitReqDoc)
        {
            int res = 0;
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    int ModifiedById = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    //set the property in BAl layer class
                    objSubmitReqDocRequest = new SubmitReqDocRequest()
                    {
                        title = objSubmitReqDoc.title,
                        UploadedDoc = objSubmitReqDoc.UploadedDoc,
                        IsExpDateReq = objSubmitReqDoc.IsExpDateReq,
                        IsCompanyReq = objSubmitReqDoc.IsCompanyReq,
                        IsLicenseReq = objSubmitReqDoc.IsLicenseReq,
                        Ack = objSubmitReqDoc.Ack,
                        IsStateReq = objSubmitReqDoc.IsStateReq,
                        UserPer = objSubmitReqDoc.UserPer,
                        DoNotShow = objSubmitReqDoc.DoNotShow,
                        IsPolicyReq = objSubmitReqDoc.IsPolicyReq,
                        ReqDocFor = objSubmitReqDoc.ReqDocFor,
                        ReqDocType = objSubmitReqDoc.ReqDocType,
                        Type = objSubmitReqDoc.Type,
                        ReqDocId = objSubmitReqDoc.ReqDocId,
                        UserId = userId,
                        ModifiedById = ModifiedById,
                        CompanyId = companyId,
                        ServicesId = objSubmitReqDoc.ServicesId,
                        VendorTypeId = objSubmitReqDoc.VendorTypeId,
                        Retake = objSubmitReqDoc.Retake
                    };
                    //Called method for submit expiration date
                    if (objDecisionPointEngine.SubmitRequireDocument(objSubmitReqDocRequest) > 0)
                        res = 1;
                    else
                        res = 0;
                }
            }
            catch (Exception ex)
            {

                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return res;
        }

        /// <summary>
        /// Used to get ack details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        [HttpGet]
        public string GetAckByreqDocId(int reqDocId)
        {
            IList<string> acklist = null;
            logMessage = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    acklist = objDecisionPointEngine.GetAckByreqDocId(reqDocId);
                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(acklist);

        }
        /// <summary>
        /// Used to get vendor type details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        [HttpGet]
        public string GetReqDocVendorTypeByreqDocId(int reqDocId)
        {
            IList<int> reqvendortypelist = null;
            logMessage = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    reqvendortypelist = objDecisionPointEngine.GetReqDocVendorTypeByreqDocId(reqDocId);
                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(reqvendortypelist);

        }
        /// <summary>
        /// Used to get upload doc details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        public string GetUploadDocByreqDocId(int reqDocId)
        {
            IList<UploadDocDetailsRequest> uploadlist = null;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    uploadlist = objDecisionPointEngine.GetUploadDocByreqDocId(reqDocId, companyId);
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(uploadlist);

        }

        /// <summary>
        /// Used for perform the JCR operations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationtype"></param>
        /// <returns>int</returns>
        /// <createdby>bobi</createdby>
        /// <createddate>5 july 2014</createddate>
        [HttpPost]
        public int JCROperation(int id, int operationtype)
        {
            logMessage = new StringBuilder();
            int isUpdated = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!object.Equals(Session["UserType"], null))
                {
                    //call method for JCR Operations
                    objDecisionPointEngine = new DecisionPointEngine();
                    isUpdated = objDecisionPointEngine.JCROperation(id, operationtype);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return isUpdated;
        }
        /// <summary>
        /// Used to create & erase the cookie for title name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        [HttpPost]
        public void CreateEraseTitleNameCookie(string name, int type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (type.Equals(0))
                {
                    HttpCookie mytitleCookie = new HttpCookie("titlename");
                    mytitleCookie.Value = name;
                    HttpContext.Response.Cookies.Add(mytitleCookie);
                }
                else if (type.Equals(1))
                {
                    if (Request.Cookies["titlename"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("titlename");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }
        #endregion


        #region MyDashBoard
        /// <summary>
        /// Used for show MY dashboard page
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>5 aug 2014</createddate>
        [HttpGet]
        public ActionResult MyDashBoard(string date)
        {
            logMessage = new StringBuilder();

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                //{
                //    BackToDashboard(date,Convert.ToInt32(Request["type"],CultureInfo.InvariantCulture));

                //}
                //set back parent user type to USERTYPE Session
                if (!string.IsNullOrEmpty(Convert.ToString(Session["parentUserid"], CultureInfo.InvariantCulture)))
                {
                    if (Convert.ToInt32(Request["type"], CultureInfo.InvariantCulture).Equals(0))
                    {
                        Session["parentUserid"] = null;
                        if (!string.IsNullOrEmpty(Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture)))
                        {
                            LoginDetailResponse loginDetailResponse = new LoginDetailResponse();
                            loginDetailResponse = SetSession(Convert.ToString(Session["superAdmin"], CultureInfo.InvariantCulture));
                            if (loginDetailResponse != null)
                            {
                                HttpContext.Session["Emailid"] = loginDetailResponse.Emailid;
                                Session["UserName"] = loginDetailResponse.Firstname + " " + loginDetailResponse.LastName;
                                Session["UserId"] = loginDetailResponse.UserId;
                                Session["BusinessName"] = loginDetailResponse.BusinessName;
                                Session["UserType"] = loginDetailResponse.UserType;
                                Session["CompanyId"] = loginDetailResponse.CompanyId;
                                Session["IsTemp"] = loginDetailResponse.IsTemp;
                            }
                            Session["logopath"] = GetCompanylogo("none");
                            Expiredocsession();
                        }

                    }

                }
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objMyDashBoard = new Models.MyDashBoard();
                    objViewModel = new ViewModel();
                    objMyDashBoard = objViewModel.GetMyDashBoardAlerts(date);
                    ViewData.Model = objMyDashBoard;
                    objactionresult = View();
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion


        #region BackGroundCheckMaster

        ///// <summary>
        ///// render view BackgroundCheckMaster
        ///// </summary>
        ///// <createdby>Sumit Saurav</createdby>
        ///// <createddate>aug 14 2014</createddate>
        ///// <returns> view BackgroundCheckMaster</returns>
        //[HttpGet]
        //public ActionResult BackgroundCheckMaster()
        //{
        //    logMessage = new StringBuilder();
        //    BackGroundCheckMasterModel objModel = null;
        //    try
        //    {

        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        objDecisionPointEngine = new DecisionPointEngine();
        //        userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
        //        companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
        //        objModel = new BackGroundCheckMasterModel()
        //        {
        //            Id = 0,
        //            Status = true
        //        };
        //        objModel.BackgroundList = objDecisionPointEngine.GetBackgroundMapping(companyId, userId).ToList();
        //        ViewData.Model = objModel;
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
        //    return View();
        //}

        ///// <summary>
        ///// save details of background master
        ///// </summary>
        ///// <param name="objMasterModel">BackGrounndCheckMasterModel</param>
        ///// <createdby>Sumit Saurav</createdby>
        ///// <createddate>Aug. 14 2014</createddate>
        ///// <returns>view with saved or not message</returns>
        //[HttpPost]
        //public ActionResult BackgroundCheckMaster(BackGroundCheckMasterModel objMasterModel)
        //{
        //    logMessage = new StringBuilder();
        //    objDecisionPointEngine = new DecisionPointEngine();
        //    BackGroundCheckMasterRequest objRequest = null;
        //    try
        //    {

        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (ModelState.IsValid)
        //        {
        //            objRequest = new BackGroundCheckMasterRequest()
        //            {
        //                BackgroundTitle = objMasterModel.BackgroundTitle,
        //                BackgroundSource = objMasterModel.BackgroundSource,
        //                Id = objMasterModel.Id,
        //                CreatedBy = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
        //                Status = objMasterModel.Status,
        //            };
        //            int result = objDecisionPointEngine.SetBackgroundCheckMaster(objRequest);
        //            if (result > 0)
        //            {
        //                ViewData["SucessMsg"] = "Saved Sucess..!!";
        //            }
        //            else
        //            {
        //                ViewData["ErrorMsg"] = "Not Saved..!!";
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
        //    return View();

        //}
        #endregion

        #region License & Insurance



        /// <summary>
        /// upload License Insurance Doc
        /// </summary>
        /// <createdBy>Sumit Saurav</createdBy>
        /// <createdDate>20 Aug 2014</createdDate>

        public void UploadLicInsDoc()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string title = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (Request.Cookies["LicInstitlename"] != null)
                    {
                        title = Request.Cookies["LicInstitlename"].Value;
                        UploadController.Uploadempdocdoc(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), title);
                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        /// <summary>
        /// Used to create & erase the cookie for title name
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="type">type</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createdate>20 Aug 2014</createdate>
        [HttpPost]
        public void CreateEraseLicInsTitleNameCookie(string name, int type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (type.Equals(0))
                {
                    HttpCookie mytitleCookie = new HttpCookie("LicInstitlename");
                    mytitleCookie.Value = name;
                    HttpContext.Response.Cookies.Add(mytitleCookie);
                }
                else if (type.Equals(1))
                {
                    if (Request.Cookies["LicInstitlename"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("LicInstitlename");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);


            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }



        ///// <summary>
        ///// Used for set reqiure document for staff and ic
        ///// </summary>
        ///// <param name="objSubmitReqDoc">objSubmitReqDoc</param>
        /////  <createdby>Sumit Saurav</createdby>
        /////  <createdDate>25 Aug 2014</createdDate>
        ///// <returns>SubmitReqDocuments</returns>
        //[HttpPost]
        //public int SubmitLicInsDocuments(SubmitReqDoc objSubmitReqDoc)
        //{
        //    int res = 0;
        //    logMessage = new StringBuilder();
        //    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        else
        //        {
        //            userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
        //            int ModifiedById = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
        //            companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
        //            objDecisionPointEngine = new DecisionPointEngine();
        //            //set the property in BAl layer class
        //            objLicenseInsuranceRequest = new LicenseInsuranceRequest()
        //            {
        //                Title = objSubmitReqDoc.title,
        //                UploadedDoc = objSubmitReqDoc.UploadedDoc,
        //                IsExpDateReq = objSubmitReqDoc.IsExpDateReq,
        //                IsCompanyReq = objSubmitReqDoc.IsCompanyReq,
        //                IsLicenseReq = objSubmitReqDoc.IsLicenseReq,
        //                Ack = objSubmitReqDoc.Ack,
        //                IsStateReq = objSubmitReqDoc.IsStateReq,
        //                UserPer = objSubmitReqDoc.UserPer,
        //                DoNotShow = objSubmitReqDoc.DoNotShow,
        //                IsPolicyReq = objSubmitReqDoc.IsPolicyReq,
        //                ReqDocFor = objSubmitReqDoc.ReqDocFor,
        //                ReqDocType = objSubmitReqDoc.ReqDocType,
        //                Type = objSubmitReqDoc.Type,
        //                ReqDocId = objSubmitReqDoc.ReqDocId,
        //                UserId = userId,
        //                ModifiedById = ModifiedById,
        //                CompanyId = companyId,
        //                ServicesId = objSubmitReqDoc.ServicesId,
        //                VendorTypeId = objSubmitReqDoc.VendorTypeId
        //            };
        //            //Called method for submit expiration date
        //            //if (objDecisionPointEngine.SubmitLicenseInsuranceDocument(objLicenseInsuranceRequest) > 0)
        //            //{
        //            //    res = 1;
        //            //}
        //            //else
        //            //{
        //            //    res = 0;
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return res;
        //}

        ///// <summary>
        ///// Used to get ack details as per Lic Ins Id
        ///// </summary>
        ///// <param name="licInsId">licInsId</param>
        ///// <returns>IList<string></returns>
        ///// <createdby>Sumit Saurav</createdby>
        ///// <creayeddate>26 August 2014</creayeddate>
        //[HttpGet]
        //public string GetAckByLicInsId(int licInsId)
        //{
        //    IList<string> acklist = null;
        //    logMessage = new StringBuilder();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        else
        //        {
        //            objDecisionPointEngine = new DecisionPointEngine();
        //            acklist = objDecisionPointEngine.GetAckByLicInsId(licInsId);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    javaScriptSerializer = new JavaScriptSerializer();
        //    return javaScriptSerializer.Serialize(acklist);

        //}

        ///// <summary>
        ///// Used to get upload doc details as per Lic Ins id
        ///// </summary>
        ///// <param name="licInsId">licInsId</param>
        ///// <returns>IList<string></returns>
        ///// <createdby>Sumit Saurav</createdby>
        ///// <creayeddate>26 August 2014</creayeddate>
        //public string GetUploadDocByLicInsId(int licInsId)
        //{
        //    IList<UploadDocDetailsRequest> uploadlist = null;
        //    logMessage = new StringBuilder();
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        else
        //        {
        //            companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
        //            objDecisionPointEngine = new DecisionPointEngine();
        //            uploadlist = objDecisionPointEngine.GetUploadDocByLicInsId(licInsId, companyId);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    javaScriptSerializer = new JavaScriptSerializer();
        //    return javaScriptSerializer.Serialize(uploadlist);

        //}


        #endregion

        #region Resend Invitation Mail

        /// <summary>
        /// resend invite to user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="type">type</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 10 2014</CreatedDate>
        /// <returns>int</returns>
        [HttpPost]
        public int ResendMailToUser(int userId, string type)
        {
            int result = 0;
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objViewModel = new ViewModel();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    result = objViewModel.ResendInviteToUser(userId, type);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return result;
        }
        #endregion

        #region Permission
        /// <summary>
        /// Used for Render the view of permission table
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        [HttpGet]
        public ActionResult PermissionTable()
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objPermissionTable = new PermissionTable();
                    objPermissionTable.TitleDetails = objDecisionPointEngine.GetTitle(Shared.Company.ToLower(CultureInfo.InvariantCulture), companyId).OrderBy(x => x.titleName);
                    objPermissionTable.GroupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), companyId);
                    objPermissionTableRequest = new PermissionTableRequest()
                    {
                        Type = 0,
                        CreatedCompanyId = companyId
                    };
                    objPermissionTable.FuntionalPermissions = objDecisionPointEngine.GetFuntionalPermission(objPermissionTableRequest);
                    objPermissionTableRequest.Type = 2;
                    objPermissionTable.CompanyBasedPermissions = objDecisionPointEngine.GetFuntionalPermission(objPermissionTableRequest);
                    ViewData.Model = objPermissionTable;
                    objactionresult = View();
                }
                else
                {
                    objactionresult = View("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Used for Save the funtional permission table mapping in database
        /// </summary>
        /// <param name="titleId">titleId</param>
        /// <param name="funPermissionIds">funPermissionIds</param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>5 Dec 2014</createdDate>
        [HttpPost]
        public int SaveFuntionalPermissionTableMapping(int titleId, string funPermissionIds)
        {
            logMessage = new StringBuilder();
            int inserted = 0;
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objPermissionTableRequest = new PermissionTableRequest()
                    {
                        CreatedCompanyId = companyId,
                        ModifiedBy = modifiedBy,
                        CreatedBy = userId,
                        TitleId = titleId,
                        FunPermissionIds = funPermissionIds
                    };
                    inserted = objDecisionPointEngine.SaveFuntionalPermissionTableMapping(objPermissionTableRequest);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return inserted;
        }

        /// <summary>
        /// Used for get the permission table mapping details as per title
        /// </summary>
        /// <param name="titleId">titleId</param>
        /// <returns>JsonResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>5 Dec 2014</createdDate>
        [HttpGet]
        public JsonResult GetPermissionTableMapAsPerTitle(int titleId)
        {
            logMessage = new StringBuilder();
            IList<PermissionTableResponse> funtionalPermissions = null;
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objPermissionTableRequest = new PermissionTableRequest()
                    {
                        CreatedCompanyId = companyId,
                        Type = 1,
                        TitleId = titleId

                    };
                    funtionalPermissions = objDecisionPointEngine.GetFuntionalPermission(objPermissionTableRequest);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(funtionalPermissions, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Check Email Id Exist or Not
        /// <summary>
        /// Check Existense of users
        /// </summary>
        /// <param name="emailId">string type emailId</param>
        /// <createdby>Sumit saurav</createdby>
        /// <createddate>july 29 2014</createddate>
        /// <returns>user type </returns>
        [HttpPost]
        public string CheckUserExistence(string emailId, string type)
        {
            string result = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();

                if (!string.IsNullOrEmpty(emailId))
                {
                    result = objDecisionPointEngine.CheckUserExistence(emailId.Trim(), type);
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "Not Found";
                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return result;
        }
        /// <summary>
        /// Check Existense of users
        /// </summary>
        /// <param name="emailId">string type emailId</param>
        /// <createdby>Sumit saurav</createdby>
        /// <createddate>july 29 2014</createddate>
        /// <returns>user type </returns>
        [HttpPost]
        public string CheckUserEmailExistence(string emailId, int userId, string userType)
        {
            string result = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(emailId))
                    {
                        result = objDecisionPointEngine.CheckUserEmailExistence(emailId, userId, userType, companyId);
                        if (string.IsNullOrEmpty(result))
                        {
                            result = "Not Found";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return result;
        }
        #endregion
    }
}
