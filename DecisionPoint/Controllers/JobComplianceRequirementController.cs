using DecisionPoint.Controllers.WebApi;
using DecisionPoint.Models;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DecisionPoint.Controllers
{
    public class JobComplianceRequirementController : Controller
    {
        #region Global Variables
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        JobComplianceRequirementModel objJobComplianceRequirementModel = null;
        LicenseInsuranceRequest objLicenseInsuranceRequest = null;
        BackGroundCheckMasterRequest objBackGroundCheckMasterRequest = null;
        JavaScriptSerializer javaScriptSerializer = null;
        int userId = 0;
        int modifiedBy = 0;
        string companyId = string.Empty;
        ActionResult objactionresult = null;
        FilterRequest objFilterRequest = null;
        #endregion

        #region JCR
        [HttpGet]
        public ActionResult JobComplianceReqiurements()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objJobComplianceRequirementModel = new JobComplianceRequirementModel();
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objJobComplianceRequirementModel.ICTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false).Select(x => new CompanyDashBoardResponse { categoryName = x.VendorTypeName, id = x.VendorTypeId }).ToList();
                    objFilterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        isActive = true,
                        filtertype = Shared.None,
                        uType = Shared.Client,
                    };
                    objJobComplianceRequirementModel.ClientsList = objDecisionPointEngine.GetVendorList(objFilterRequest).OrderBy(x => x.vendor);
                    objJobComplianceRequirementModel.StateDetails = objDecisionPointEngine.GetStateList().ToList();
                    objJobComplianceRequirementModel.BackgroundPkgList = objDecisionPointEngine.GetBackgroundCheckPackages();
                    objJobComplianceRequirementModel.BackgroundMasterList = objDecisionPointEngine.GetBackgroundCheckMaster(companyId).ToList();
                    objJobComplianceRequirementModel.ProfLicMasterList = objDecisionPointEngine.GetProfessionalLicenseMaster(companyId).ToList();
                    objJobComplianceRequirementModel.InsuranceMasterList = objDecisionPointEngine.GetInsuranceMaster(companyId).ToList();
                    objJobComplianceRequirementModel.AddReqMasterList = objDecisionPointEngine.GetAdditionareqMaster(companyId).Where(x => x.IsStaffTitle == false).ToList();

                    objJobComplianceRequirementModel.HostURL = ViewModel.GetSiteRoot();
                    objJobComplianceRequirementModel.AdditioalReqDoc = Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture).Split('~')[1] + companyId;
                    objJobComplianceRequirementModel.StateList = objDecisionPointEngine.GetStateList();
                    //Particular user
                    objJobComplianceRequirementModel.TitleList = objDecisionPointEngine.GetTitle(Shared.Company.ToLower(), companyId);
                    ViewData.Model = objJobComplianceRequirementModel;
                    objactionresult = View();
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
        /// used for Save JobCompliance Reqiurements
        /// </summary>
        /// <param name="objJobComplianceRequirementModel"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>Feb 6 2015</createddate>
        [HttpPost]
        public ActionResult SaveJobComplianceReqiurements(JobComplianceRequirementModel objJobComplianceRequirementModel)
        {
            int isInserted = 0;
            logMessage = new StringBuilder();
            string partialViewName = string.Empty;

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    //objLicenseInsuranceRequest used for save professional license, additional req and insurance
                    objLicenseInsuranceRequest = new LicenseInsuranceRequest()
                    {
                        //set the property in BAL layer class
                        UserId = userId,
                        CompanyId = companyId,
                        ICTypeStaffTitleIds = objJobComplianceRequirementModel.ICTypeStaffTitleIds,
                        ClientIds = objJobComplianceRequirementModel.ClientsIds,
                        OperationType = objJobComplianceRequirementModel.OperationType,
                        ModifiedById = modifiedBy
                    };

                    partialViewName = Shared.GetEnumDescription((ComplianceSection)objJobComplianceRequirementModel.Type);

                    //0 for save background details
                    if (objJobComplianceRequirementModel.Type.Equals((int)ComplianceSection.Background))
                    {
                        objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest()
                        {
                            UserId = userId,
                            CompanyId = companyId,
                            ICTypeStaffTitleIds = objJobComplianceRequirementModel.ICTypeStaffTitleIds,
                            ClientIds = objJobComplianceRequirementModel.ClientsIds,
                            BGCheckId = objJobComplianceRequirementModel.BgCheckPkgId,
                            ModifiedBy = modifiedBy,
                            OperationType = objJobComplianceRequirementModel.OperationType,
                            Source = objJobComplianceRequirementModel.Source,
                            RequirmentType = 1
                        };
                        if (!object.Equals(objBackGroundCheckMasterRequest.ClientIds, null))
                        {
                            if (objBackGroundCheckMasterRequest.ClientIds.Count == 1)
                            {
                                if (Convert.ToInt32(objBackGroundCheckMasterRequest.ClientIds[0].Split(char.Parse(Shared.Colon))[0], CultureInfo.InvariantCulture) == userId)
                                {
                                    //if RequirmentType 0 than Requirment for Internal
                                    objBackGroundCheckMasterRequest.RequirmentType = 0;
                                }
                            }

                        }
                        objDecisionPointEngine.SetBackgroundMapping(objBackGroundCheckMasterRequest);
                    }
                    //1 for save professional license details
                    else if (objJobComplianceRequirementModel.Type.Equals((int)ComplianceSection.ProfessionalLicense))
                    {
                        objLicenseInsuranceRequest.Title = objJobComplianceRequirementModel.Title;
                        objLicenseInsuranceRequest.Acknowleagement = objJobComplianceRequirementModel.Acknowleagement;
                        objLicenseInsuranceRequest.LicenseType = objJobComplianceRequirementModel.LicenseType;
                        objLicenseInsuranceRequest.Source = objJobComplianceRequirementModel.Source;
                        objLicenseInsuranceRequest.ReqDocId = objJobComplianceRequirementModel.ProfLicTblId;
                        objLicenseInsuranceRequest.IsStaffTitle = objJobComplianceRequirementModel.IsStaffTitle;
                        objLicenseInsuranceRequest.ICTypeStaffTitleIds = objJobComplianceRequirementModel.ICTypeStaffTitleIds;

                        if (objJobComplianceRequirementModel.OperationType.Equals(2))
                        {
                            objLicenseInsuranceRequest.ICTypeId = objJobComplianceRequirementModel.ICTypeId;
                            objLicenseInsuranceRequest.LicenseNumber = objJobComplianceRequirementModel.LicenseNumber;
                            objLicenseInsuranceRequest.StateId = objJobComplianceRequirementModel.StateId;
                            objLicenseInsuranceRequest.ExpirationDate = objJobComplianceRequirementModel.ExpirationDate;
                            objLicenseInsuranceRequest.Source = "Other";
                        }
                        objDecisionPointEngine.SaveProfessionalLicense(objLicenseInsuranceRequest);
                        objJobComplianceRequirementModel.StateDetails = objDecisionPointEngine.GetStateList().ToList();
                    }
                    //2 for save insurance details
                    else if (objJobComplianceRequirementModel.Type.Equals((int)ComplianceSection.Insurance))
                    {
                        objLicenseInsuranceRequest.Title = objJobComplianceRequirementModel.Title;
                        objLicenseInsuranceRequest.Acknowleagement = objJobComplianceRequirementModel.Acknowleagement;
                        objLicenseInsuranceRequest.InsuranceType = objJobComplianceRequirementModel.InsuranceType;
                        objLicenseInsuranceRequest.ReqDocId = objJobComplianceRequirementModel.InsTblId;
                        objLicenseInsuranceRequest.Source = objJobComplianceRequirementModel.Source;
                        objLicenseInsuranceRequest.InsuranceVerType = objJobComplianceRequirementModel.InsuranceVerType;
                        if (objJobComplianceRequirementModel.OperationType.Equals(2))
                        {
                            objLicenseInsuranceRequest.ICTypeId = objJobComplianceRequirementModel.ICTypeId;
                            objLicenseInsuranceRequest.CompanyName = objJobComplianceRequirementModel.CompanyName;
                            objLicenseInsuranceRequest.PolicyNumber = objJobComplianceRequirementModel.PolicyNumber;
                            objLicenseInsuranceRequest.Aggregate = objJobComplianceRequirementModel.Aggregate;
                            objLicenseInsuranceRequest.SingleOccurance = objJobComplianceRequirementModel.SingleOccurance;
                        }
                        objDecisionPointEngine.SaveInsurance(objLicenseInsuranceRequest);
                    }
                    //3 for save additional req details
                    else if (objJobComplianceRequirementModel.Type.Equals((int)ComplianceSection.AdditionalCredentials))
                    {
                        objLicenseInsuranceRequest.Title = objJobComplianceRequirementModel.Title;
                        objLicenseInsuranceRequest.Acknowleagement = objJobComplianceRequirementModel.Acknowleagement;
                        objLicenseInsuranceRequest.Review = objJobComplianceRequirementModel.Review;
                        objLicenseInsuranceRequest.UploadedBy = objJobComplianceRequirementModel.UploadedBy;
                        objLicenseInsuranceRequest.ReqDocId = objJobComplianceRequirementModel.AdditionalReqTblId;
                        objLicenseInsuranceRequest.UploadedDoc = objJobComplianceRequirementModel.UploadedDocs;
                        objLicenseInsuranceRequest.IsStaffTitle = objJobComplianceRequirementModel.IsStaffTitle;
                        objLicenseInsuranceRequest.ExpirationDate = objJobComplianceRequirementModel.ExpirationDate;
                        objDecisionPointEngine.SaveAdditionalReq(objLicenseInsuranceRequest);
                    }
                    objJobComplianceRequirementModel.AddReqMasterList = objDecisionPointEngine.GetAdditionareqMaster(companyId).Where(x => x.IsStaffTitle == objJobComplianceRequirementModel.IsStaffTitle).ToList();
                    objJobComplianceRequirementModel.ProfLicMasterList = objDecisionPointEngine.GetProfessionalLicenseMaster(companyId).Where(x => x.IsStaffTitle == objJobComplianceRequirementModel.IsStaffTitle).ToList();
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
            return PartialView(partialViewName, objJobComplianceRequirementModel);
            //return  Json(objJobComplianceRequirementModel,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetJCRComplianceSection(int jsComplianceSection, Boolean jsIsStaffTitle)
        {
            string partialViewName = string.Empty;
            JobComplianceRequirementModel jcrModel = new JobComplianceRequirementModel();
            ViewBag.IsJCRForStaff = jsIsStaffTitle;
            try
            {
                if (Session["CompanyId"] != null)
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    partialViewName = Shared.GetEnumDescription((ComplianceSection)jsComplianceSection);
                    switch (jsComplianceSection)
                    {
                        case (int)ComplianceSection.AdditionalCredentials:
                            using (var dpEngine = new DecisionPointEngine())
                            {
                                var addCredList = dpEngine.GetAdditionareqMaster(companyId).Where(x => x.IsStaffTitle == jsIsStaffTitle).ToList();
                                jcrModel.AddReqMasterList = addCredList;
                            }
                            break;
                        case (int)ComplianceSection.Background:
                            using (var dpEngine = new DecisionPointEngine())
                            {
                                var bgCheckMasterList = dpEngine.GetBackgroundCheckMaster(companyId)
                                    //.Where(x => x.IsStaffTitle == jsIsStaffTitle)
                                    .ToList();
                                //jcrModel.BackgroundMasterList = bgCheckMasterList;
                                jcrModel.BackgroundPkgList = dpEngine.GetBackgroundCheckPackages();

                            }
                            break;
                        case (int)ComplianceSection.ProfessionalLicense:
                            using (var dpEngine = new DecisionPointEngine())
                            {   
                                var profLicMasterList = dpEngine.GetProfessionalLicenseMaster(companyId).Where(x => x.IsStaffTitle == jsIsStaffTitle).ToList();
                                jcrModel.ProfLicMasterList = profLicMasterList;
                                jcrModel.StateDetails = dpEngine.GetStateList().ToList(); 
                            }
                            break;
                        case (int)ComplianceSection.Insurance:
                            using (var dpEngine = new DecisionPointEngine())
                            {
                                var insMasterList = dpEngine.GetInsuranceMaster(companyId).Where(x => x.IsStaffTitle == jsIsStaffTitle).ToList();
                                jcrModel.InsuranceMasterList = insMasterList;
                            }
                            break;
                        default:
                            break;
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
                //logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                //log.Info(logMessage.ToString());
            }
            return PartialView(partialViewName, jcrModel);

        }

        /// <summary>
        /// Used to get ack details for professional license, insurance and additional req detials
        /// </summary>
        /// <param name="licInsId">Id</param>
        /// <returns>IList<string></returns>
        /// <createdby>Bobi</createdby>
        /// <creayeddate>Feb 10 2015</creayeddate>
        [HttpGet]
        public string GetAckByLicInsId(int tblMapId, int type)
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
                    acklist = objDecisionPointEngine.GetAckByLicInsId(tblMapId, type);
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

        [HttpPost]
        public string GetRespouseFromAPI(string reqXML)
        {
            return "Completed";
        }

        #region Edit Professional License, Insurance, Communication And Additional Reqiurements on IC Profile
        [HttpPost]
        public int EditJobComplianceReqiurementsOnProfile(JobComplianceRequirementModel objJobComplianceRequirementModel)
        {
            int isInserted = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    //objLicenseInsuranceRequest used for save professional license, additional req and insurance
                    objLicenseInsuranceRequest = new LicenseInsuranceRequest()
                    {
                        //set the property in BAL layer class
                        UserId = userId,
                        CompanyId = companyId,
                        LicenseNumber = objJobComplianceRequirementModel.LicenseNumber,
                        ExpirationDate = objJobComplianceRequirementModel.ExpirationDate,
                        StateId = objJobComplianceRequirementModel.StateId,
                        StateAbbre = objJobComplianceRequirementModel.StateAbbre,
                        ModifiedById = modifiedBy,
                        PolicyNumber = objJobComplianceRequirementModel.PolicyNumber,
                        CompanyName = objJobComplianceRequirementModel.CompanyName,
                        SingleOccurance = objJobComplianceRequirementModel.SingleOccurance,
                        Aggregate = objJobComplianceRequirementModel.Aggregate,
                        LicInsMapId = objJobComplianceRequirementModel.LicInsMapId,
                        UploadedDoc = objJobComplianceRequirementModel.UploadedDocs,
                        LicInsMasterId = objJobComplianceRequirementModel.ProfLicTblId
                    };

                    //0 for edit professional license details
                    if (objJobComplianceRequirementModel.Type.Equals(0))
                    {
                        objDecisionPointEngine.EditProfessionalLic(objLicenseInsuranceRequest);
                    }
                    //1 for edit insurance
                    else if (objJobComplianceRequirementModel.Type.Equals(1))
                    {
                        objDecisionPointEngine.EditInsurance(objLicenseInsuranceRequest);
                    }
                    //2 for edit additional requirement
                    else if (objJobComplianceRequirementModel.Type.Equals(2))
                    {
                        objDecisionPointEngine.EditAdditionalReq(objLicenseInsuranceRequest);
                    }
                    //3 for edit driver License
                    else if (objJobComplianceRequirementModel.Type.Equals(3))
                    {
                        objDecisionPointEngine.EditDriverLicense(objLicenseInsuranceRequest);
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
            return isInserted;
        }
        /// <summary>
        /// Used to get ack details for professional license, insurance and additional req detials
        /// </summary>
        /// <param name="licInsId">Id</param>
        /// <returns>IList<string></returns>
        /// <createdby>Bobi</createdby>
        /// <creayeddate>Feb 10 2015</creayeddate>
        [HttpGet]
        public string GetUploadedDocs(int tblMapId, int type)
        {
            IList<UploadDocDetailsRequest> uploadlist = null;
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
                    uploadlist = objDecisionPointEngine.GetUploadDocByLicInsId(tblMapId, type);
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
        /// used for active or deactivate the JCR
        /// </summary>
        /// <param name="tblId"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>16 Feb 2015</createddate>
        public int ActiveOrDeactivateJCR(int tblId, bool status, int type)
        {
            int isUpdated = 0;
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
                    isUpdated = objDecisionPointEngine.ActiveOrDeactivateJCR(tblId, status, type);
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
                    HttpCookie mytitleCookie = new HttpCookie("jcrdoctitlename");
                    mytitleCookie.Value = name;
                    HttpContext.Response.Cookies.Add(mytitleCookie);
                }
                else if (type.Equals(1))
                {
                    if (Request.Cookies["jcrdoctitlename"] != null)
                    {
                        HttpCookie myCookie = new HttpCookie("jcrdoctitlename");
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
        /// <summary>
        /// upload JCR Doc
        /// </summary>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>20 Feb 2015</createdDate>
        public void UploadEmpReqDoc()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string title = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (Request.Cookies["jcrdoctitlename"] != null)
                    {
                        title = Request.Cookies["jcrdoctitlename"].Value;
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

        [HttpPost]
        public int ApprovedStatus(JobComplianceRequirementModel objJobComplianceRequirementModel)
        {
            int isInserted = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
                    //objLicenseInsuranceRequest used for save professional license, additional req and insurance
                    objLicenseInsuranceRequest = new LicenseInsuranceRequest()
                    {
                        //set the property in BAL layer class
                        UserId = userId,
                        CompanyId = companyId,
                        ModifiedById = modifiedBy,
                        LicInsMapId = objJobComplianceRequirementModel.LicInsMapId,
                        Status = objJobComplianceRequirementModel.Status,
                        LicInsMasterId = objJobComplianceRequirementModel.ProfLicTblId,
                        OperationType = objJobComplianceRequirementModel.OperationType

                    };

                    //0 for edit professional license, insurance and additional requirements details
                    objDecisionPointEngine.ApprovedStatus(objLicenseInsuranceRequest);

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
            return isInserted;
        }

        //public bool CheckJobComplianceVerification(JobComplianceRequirementModel objJobComplianceRequirementModel)
        //{
        //    try
        //    {
        //        bool isExist = false;
        //        logMessage = new StringBuilder();
        //        try
        //        {
        //            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //            objDecisionPointEngine = new DecisionPointEngine();
        //            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //            {
        //                RedirectToAction("Login", "Login");
        //            }
        //            else
        //            {
        //                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
        //                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);


        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
        //            objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
        //        }
        //        finally
        //        {
        //            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //            log.Info(logMessage.ToString());
        //        }
        //        return isExist;
        //    }
        //    catch 
        //    {
        //        throw;
        //    }
        //}

        #endregion
        #endregion
    }
}
