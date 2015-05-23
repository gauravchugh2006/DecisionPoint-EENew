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
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointBAL.Implementation.Request;
using DecisionPointCAL.Common;

namespace DecisionPoint.Controllers
{
    public class CommunicationController : Controller
    {
        #region GlobalVariables & Objects
        #region Variables
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int userId = 0;
        string companyId = string.Empty;
        #endregion

        #region Objects
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        CompanyDashBoard objComapnyDashBoard = null;
        ActionResult objactionresult = null;
        ConfigurationSettingDetail objConfigurationSettingDetail = null;
        UserDashBoard objUserDashBoard = null;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        #endregion
        #endregion

        #region Public Methods
        #region Performances

        /// <summary>
        /// satff performance
        /// </summary>
        /// <createdby>Rohit k</createdby>
        ///  <createdDate>12 nov 2013</createdDate>
        /// <returns>view StaffPerformance with model data</returns>
        public ActionResult StaffPerformance()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    CommunicationModel communicationModel = new CommunicationModel();
                    communicationModel.staffPerformanceLst = objDecisionPointEngine.GetStaffPerformanceList(userId, companyId).ToList();
                    communicationModel.pagesize = communicationModel.staffPerformanceLst.Count();
                    communicationModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    return View("StaffPerformance", communicationModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }

        /// <summary>
        /// ICCompliance
        /// </summary>
        /// <createdby>Mamta gupta</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view of actionresult</returns>
        public ActionResult ICCompliance()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    CommunicationModel communicationModel = new CommunicationModel();
                    communicationModel.ICPerformanceLst = objDecisionPointEngine.GetICPerformanceList(userId, companyId).ToList();
                    if (!object.Equals(communicationModel.ICPerformanceLst, null))
                    {
                        communicationModel.pagesize = communicationModel.ICPerformanceLst.Count(); ;
                    }
                    communicationModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    return View("ICCompliance", communicationModel);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        /// <summary>
        /// vendor performance
        /// </summary>
        /// <createdby>Nilesh Dubey</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view vendor performance with model data</returns>
        [HttpGet]
        public ActionResult VendorPerformance()
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

                    objComapnyDashBoard.vendorPerformance = objDecisionPointEngine.VendorPerformance(companyId, true, "Vendor", userId);
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.vendorPerformance.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("VendorPerformance", objComapnyDashBoard);
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
        /// get clientperformance
        /// </summary>
        /// <returns>ActionResuslt</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>March 13 2014</createdDate>
        [HttpGet]
        public ActionResult ClientPerfromance()
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

                    objComapnyDashBoard.vendorPerformance = objDecisionPointEngine.VendorPerformance(companyId, true, DecisionPointR.client, userId);
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.vendorPerformance.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("ClientPerfromance", objComapnyDashBoard);
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

        #region Withdrwan Items
        /// <summary>
        /// GET: UserDashBorad/HistoryAction/
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view with data</returns>
        [HttpGet]
        public ActionResult WithdrawnHistory(string id)
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
                    objUserDashBoard.HistoryDetails = objDecisionPointEngine.GetWithdrawnHistoryDetails(userId, DecisionPointR.mycommunication, companyId, id);
                    if (objUserDashBoard.HistoryDetails != null)
                    {
                        #region groupby category
                        string doctype = string.Empty;
                        int count = 0;
                        IList<UserDashBoardResponse> historydetails = new List<UserDashBoardResponse>().ToList();
                        UserDashBoardResponse objUserDashBoardResponse = new UserDashBoardResponse()
                        {
                            reqDocType = string.Empty,
                            title = string.Empty,
                            commFromPersonName = string.Empty,
                            commFromComapnyName = string.Empty,
                            receviedDate = System.DateTime.Now.Date,
                            accecpted = false,
                            assesmentStatus = string.Empty,
                            docTypeId = 0,
                            tableId = 0,
                            reference = string.Empty,
                            timeSpend = string.Empty,
                            deliveredUserId = 0,
                            CompanyId = string.Empty,
                            docSeqno = 0,
                            policyNo = string.Empty,
                            completeDate = System.DateTime.Now.Date,
                            versionno = 0,
                            effectiveDate = System.DateTime.Now.Date
                        };
                        var col = objUserDashBoard.HistoryDetails;
                        historydetails = col.ToList();

                        foreach (var list in objUserDashBoard.HistoryDetails.ToList())
                        {
                            if (!string.IsNullOrEmpty(doctype))
                            {
                                if (list.reqDocType != doctype)
                                {
                                    historydetails.Insert(count, objUserDashBoardResponse);
                                }

                            }
                            doctype = list.reqDocType;
                            count++;
                        }
                        objUserDashBoard.HistoryDetails = historydetails;
                        #endregion


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
                    objactionresult = View("WithdrawnHistory", objUserDashBoard);
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
        /// Withdrawn Inbox History
        /// </summary>
        /// <param name="id">string type Id for filter</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view with result </returns>
        [HttpGet]
        public ActionResult WithdrawnInboxHistory(string id)
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
                    objUserDashBoard.HistoryDetails = objDecisionPointEngine.GetWithdrawnHistoryDetails(userId, DecisionPointR.incomming, companyId, id);

                    if (objUserDashBoard.HistoryDetails != null)
                    {
                        #region groupby category
                        string doctype = string.Empty;
                        int count = 0;

                        IList<UserDashBoardResponse> historydetails = new List<UserDashBoardResponse>().ToList();
                        UserDashBoardResponse objUserDashBoardResponse = new UserDashBoardResponse()
                        {
                            reqDocType = string.Empty,
                            title = string.Empty,
                            commFromPersonName = string.Empty,
                            commFromComapnyName = string.Empty,
                            receviedDate = System.DateTime.Now.Date,
                            accecpted = false,
                            assesmentStatus = string.Empty,
                            docTypeId = 0,
                            tableId = 0,
                            reference = string.Empty,
                            timeSpend = string.Empty,
                            deliveredUserId = 0,
                            CompanyId = string.Empty,
                            docSeqno = 0,
                            policyNo = string.Empty,
                            completeDate = System.DateTime.Now.Date,
                            versionno = 0,
                            effectiveDate = System.DateTime.Now.Date
                        };
                        var col = objUserDashBoard.HistoryDetails;
                        historydetails = col.ToList();

                        foreach (var list in objUserDashBoard.HistoryDetails.ToList())
                        {
                            if (!string.IsNullOrEmpty(doctype))
                            {
                                if (list.reqDocType != doctype)
                                {
                                    historydetails.Insert(count, objUserDashBoardResponse);
                                }

                            }
                            doctype = list.reqDocType;
                            count++;
                        }
                        objUserDashBoard.HistoryDetails = historydetails;
                        #endregion


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

        #region Configuration Setting
        /// <summary>
        /// get all  services list as per client 
        /// </summary>  
        /// <createdBy>Bobi</createdBy>
        /// <param name="companyId">companyId</param>
        /// <createdDate>30 june 2014</createdDate>
        /// <returns>list service list</returns>
        [HttpGet]
        public JsonResult GetCompanyVendorType(string companyId, string type)
        {

            logMessage = new StringBuilder();
            objComapnyDashBoard = new Models.CompanyDashBoard();
            try
            {

                objDecisionPointEngine = new DecisionPointEngine();
                objConfigurationSettingDetail = new ConfigurationSettingDetail();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    objConfigurationSettingDetail.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, type).Where(x => x.IsUserBased == false);
                }
                else
                {
                    objConfigurationSettingDetail.CompanyVendorTypeDetails = null;
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
            return Json(objConfigurationSettingDetail.CompanyVendorTypeDetails, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Used for Configuration Setting for companyies
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>22 july 2014</createdDate>
        [HttpGet]
        public ActionResult ConfigurationSetting(int userId, string companyId)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objConfigurationSettingDetail = new ConfigurationSettingDetail();
                objConfigurationSettingDetail.UserId = userId;
                objConfigurationSettingDetail.CompanyId = companyId;
                //get config setting details
                objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                if (!object.Equals(objConfiguratonSettingRequest, null))
                {
                    #region Config Properties
                    objConfigurationSettingDetail.IsCoveragearea = objConfiguratonSettingRequest.IsCoveragearea;
                    objConfigurationSettingDetail.IsClient = objConfiguratonSettingRequest.IsClient;
                    objConfigurationSettingDetail.IsIc = objConfiguratonSettingRequest.IsIc;
                    objConfigurationSettingDetail.IsServices = objConfiguratonSettingRequest.IsServices;
                    objConfigurationSettingDetail.IsClientOnMyProfile = objConfiguratonSettingRequest.IsClientOnMyProfile;
                    objConfigurationSettingDetail.IsVendor = objConfiguratonSettingRequest.IsVendor;
                    objConfigurationSettingDetail.IsWebinarApply = objConfiguratonSettingRequest.IsWebinarApply;
                    objConfigurationSettingDetail.IsScormApply = objConfiguratonSettingRequest.IsScormApply;
                    objConfigurationSettingDetail.IsICFreeBasicMembership = objConfiguratonSettingRequest.IsICFreeBasicMembership;
                    objConfigurationSettingDetail.IsICUsePackages = objConfiguratonSettingRequest.IsICUsePackages;
                    objConfigurationSettingDetail.IsICInsApply = objConfiguratonSettingRequest.IsICInsApply;
                    objConfigurationSettingDetail.IsStaffInsApply = objConfiguratonSettingRequest.IsStaffInsApply;
                    objConfigurationSettingDetail.IsStaffCommApply = objConfiguratonSettingRequest.IsStaffCommApply;
                    objConfigurationSettingDetail.IsICCommApply = objConfiguratonSettingRequest.IsICCommApply;

                    objConfigurationSettingDetail.IsBgCheckForIC = objConfiguratonSettingRequest.IsBgCheckForIC;
                    objConfigurationSettingDetail.IsICLicenseApply = objConfiguratonSettingRequest.IsLiceInsForIC;
                    objConfigurationSettingDetail.IsAddCreForIC = objConfiguratonSettingRequest.IsAddCreForIC;
                    objConfigurationSettingDetail.IsCoverageAreaForIC = objConfiguratonSettingRequest.IsCoverageAreaForIC;
                    objConfigurationSettingDetail.IsServicesForIC = objConfiguratonSettingRequest.IsAddCreForIC;
                    objConfigurationSettingDetail.IsICClientOnMyProfile = objConfiguratonSettingRequest.IsICClientOnMyProfile;

                    objConfigurationSettingDetail.IsAddCreForStaff = objConfiguratonSettingRequest.IsAddCreForStaff;
                    objConfigurationSettingDetail.IsBgCheckForStaff = objConfiguratonSettingRequest.IsBgCheckForStaff;
                    objConfigurationSettingDetail.IsCoverageAreaForStaff = objConfiguratonSettingRequest.IsCoverageAreaForStaff;
                    objConfigurationSettingDetail.IsServicesForStaff = objConfiguratonSettingRequest.IsServicesForStaff;
                    objConfigurationSettingDetail.IsStaffClientOnMyProfile = objConfiguratonSettingRequest.IsStaffClientOnMyProfile;
                    objConfigurationSettingDetail.IsLicenseForStaff = objConfiguratonSettingRequest.IsLicenseForStaff;
                    objConfigurationSettingDetail.IsClientNameApplyForIC = objConfiguratonSettingRequest.IsClientNameApplyForIC;
                    objConfigurationSettingDetail.IsContractApply = objConfiguratonSettingRequest.IsContractApply;
                    #endregion
                }
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                objConfigurationSettingDetail.VendorTypeDetails = objDecisionPointEngine.GetVendorType(Shared.None, companyId).ToList();
                //objConfigurationSettingDetail.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, string.Empty);
                ViewData.Model = objConfigurationSettingDetail;
                objactionresult = View();
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
            return objactionresult;
        }

        /// <summary>
        /// Used for save the configuration setting
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="companyID">companyID</param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>22 july 2014</createdDate>
        [HttpPost]
        public int SaveConfigSettings(ConfigurationSettingDetail objConfigurationSettingDetail)
        {
            int isInserted = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest()
                    {
                        #region Config Properties
                        IsCoveragearea = objConfigurationSettingDetail.IsCoveragearea,
                        IsClient = objConfigurationSettingDetail.IsClient,
                        IsIc = objConfigurationSettingDetail.IsIc,
                        IsServices = objConfigurationSettingDetail.IsServices,
                        IsClientOnMyProfile = objConfigurationSettingDetail.IsClientOnMyProfile,
                        IsVendor = objConfigurationSettingDetail.IsVendor,
                        IsICFreeBasicMembership = objConfigurationSettingDetail.IsICFreeBasicMembership,
                        IsWebinarApply = objConfigurationSettingDetail.IsWebinarApply,
                        IsScormApply = objConfigurationSettingDetail.IsScormApply,
                        IsICUsePackages = objConfigurationSettingDetail.IsICUsePackages,
                        IsICInsApply = objConfigurationSettingDetail.IsICInsApply,
                        IsICCommApply = objConfigurationSettingDetail.IsICCommApply,
                        IsStaffCommApply = objConfigurationSettingDetail.IsStaffCommApply,
                        IsStaffInsApply = objConfigurationSettingDetail.IsStaffInsApply,

                        IsBgCheckForIC = objConfigurationSettingDetail.IsBgCheckForIC,
                        IsLiceInsForIC = objConfigurationSettingDetail.IsICLicenseApply,
                        IsAddCreForIC = objConfigurationSettingDetail.IsAddCreForIC,
                        IsCoverageAreaForIC = objConfigurationSettingDetail.IsCoverageAreaForIC,
                        IsServicesForIC = objConfigurationSettingDetail.IsAddCreForIC,
                        IsICClientOnMyProfile = objConfigurationSettingDetail.IsICClientOnMyProfile,

                        IsAddCreForStaff = objConfigurationSettingDetail.IsAddCreForStaff,
                        IsBgCheckForStaff = objConfigurationSettingDetail.IsBgCheckForStaff,
                        IsCoverageAreaForStaff = objConfigurationSettingDetail.IsCoverageAreaForStaff,
                        IsServicesForStaff = objConfigurationSettingDetail.IsServicesForStaff,
                        IsStaffClientOnMyProfile = objConfigurationSettingDetail.IsStaffClientOnMyProfile,
                        IsLicenseForStaff = objConfigurationSettingDetail.IsLicenseForStaff,
                        IsClientNameApplyForIC = objConfigurationSettingDetail.IsClientNameApplyForIC,
                        IsContractApply = objConfigurationSettingDetail.IsContractApply,
                        #endregion
                        #region Other
                        UserId = objConfigurationSettingDetail.UserId,
                        CompanyId = objConfigurationSettingDetail.CompanyId,
                        CreatedBy = userId
                        #endregion
                    };
                    isInserted = objDecisionPointEngine.SaveConfigSetting(objConfiguratonSettingRequest);
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
            return isInserted;
        }
        #endregion
        #endregion
    }
}
