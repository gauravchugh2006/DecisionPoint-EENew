using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DecisionPoint.Models;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPointBAL.Implementation.Request;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using DecisionPointBAL.Core;
using System.Web.Script.Serialization;
using CsvHelper;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;

namespace DecisionPoint.Controllers
{
    public class InvitationController : Controller
    {
        #region GlobalVariable & Objecjs
        #region Variables
        int userId = 0;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        string companyId = string.Empty;
        List<string> serach1 = null;
        List<string> serach2 = null;
        List<string> serachFinal = null;
        #endregion
        #region Objects
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        CompanyDashBoard objComapnyDashBoard = null;
        ActionResult objactionresult = null;
        ActionResult objActionResult = null;
        FilterRequest filterRequest = null;
        ViewModel objViewModel = null;
        InviteVendorBulk inviteVendorModel = null;
        #endregion
        #endregion

        #region IC Sent Invitation

        /// <summary>
        /// ICManualInvitation
        /// </summary>
        /// <createdby>mamta gupta</createdby>
        ///  <createdDate>13 jan 2014</createdDate>
        /// <returns>return view with data</returns>
        public ActionResult ICManualInvitation()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                InviteVendorManual objInviteVendorManual = new InviteVendorManual();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    
                    // if (!object.Equals(Session["UserType"], null))
                    //  {
                    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("SuperAdmin"))
                    {
                        objInviteVendorManual.CompanyVendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, companyId).Select(x => new VendorTypeResponse { VendorTypeName = x.categoryName, VendorTypeId = x.id });
                    }
                    else
                    {
                        objInviteVendorManual.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false);
                    }
                    // }
                    // else { objInviteVendorManual.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC); }
                    objInviteVendorManual.GetICPaymentMode = objDecisionPointEngine.GetICPaymentMode().OrderByDescending(x => x.PaymentId);
                    ConfiguratonSettingRequest objConfiguratonSettingRequest=objDecisionPointEngine.GetConfigSetting(companyId);
                    if (!object.Equals(objConfiguratonSettingRequest,null))
                    {
                        if (!objConfiguratonSettingRequest.IsICFreeBasicMembership)
                        {
                            objInviteVendorManual.GetICPaymentMode = objInviteVendorManual.GetICPaymentMode.Where(x => x.PaymentId != 3);
                        }
                    }
                    //assign the view model
                    ViewData.Model = objInviteVendorManual;
                    objactionresult = View("ICManualInvitation");
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
        /// InviteICBulk
        /// </summary>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>16 jan 2014</createdDate>
        /// <returns>InviteICBulk page</returns>
        [HttpGet]
        public ActionResult InviteICBulk()
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
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    inviteVendorModel = new Models.InviteVendorBulk();
                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    ViewData.Model = inviteVendorModel;
                    objactionresult = View("ICBulkInvitation");
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
        /// post:ICBulkCSvUpload
        /// </summary>
        /// <param name="invitevendor">InviteVendorBulk class</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view ICBulkCSvUpload</returns>
        [HttpPost]
        public ActionResult ICBulkCSvUpload(InviteVendorBulk invitevendor)
        {
            #region Global
            string flowName = Shared.SendTo;
            string paymentType = string.Empty;
            string VType = string.Empty;
            string BGC = string.Empty;
            bool col1 = false;
            bool col2 = false;
            bool col3 = false;
            bool col4 = false;
            bool col5 = false;
            bool col6 = false;
            bool col7 = false;
            string exactfilename = string.Empty;
            string filePath = string.Empty;
            string fnext = string.Empty;
            var file = Request.Files[0];
            string serverpathname = string.Empty;
            #endregion
            objDecisionPointEngine = new DecisionPointEngine();

            logMessage = new StringBuilder();
            string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        objViewModel = new ViewModel();
                        companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                        userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                        exactfilename = Path.GetFileName(file.FileName);
                        fnext = Path.GetExtension(exactfilename);
                        if (!string.IsNullOrEmpty(exactfilename))
                        {
                            exactfilename = userId + Shared.UnderScore + BusinessCore.GenrateRandomnumber() + Shared.CsvFileExt;
                        }
                        if (file != null && file.ContentLength > 0 && string.Equals(fnext, Shared.CsvFileExt, StringComparison.OrdinalIgnoreCase))
                        {
                            if (string.IsNullOrEmpty(invitevendor.CsvFileName))
                            {
                                filePath = Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]),
                                                              Path.GetFileName(exactfilename));
                                file.SaveAs(filePath);
                                if (!object.Equals(Session["UserType"], null))
                                {
                                    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("SuperAdmin"))
                                    {
                                        invitevendor.CompanyVendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, companyId).Select(x => new VendorTypeResponse { VendorTypeName = x.categoryName, VendorTypeId = x.id });
                                    }
                                    else { invitevendor.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }
                                }
                                else { invitevendor.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }
                                invitevendor.GetICPaymentMode = objDecisionPointEngine.GetICPaymentMode();
                                // Called method for comparing CSV values to the List
                                serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + exactfilename;
                            }
                            else
                            {
                                exactfilename = invitevendor.CsvFileName;
                                serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + exactfilename;
                            }
                            CsvReader objCSVReader = CSVFileReader.ReadCSvFile(serverpathname);
                            string[] headerCol = { };
                            int countIndex = 0;
                            while (objCSVReader.Read())
                            {
                                headerCol = objCSVReader.FieldHeaders;
                                if (headerCol.Count() != 7)
                                {
                                    ViewBag.Error = DecisionPointR.CsvNotMatch; // "CSV Column  not match ";
                                    objactionresult = View("ICBulkInvitation", invitevendor);

                                }
                                else
                                {


                                    if (headerCol.Count() == 7)
                                    {
                                        for (int i = 0; i < 7; i++)
                                        {
                                            if (headerCol[i] == "Company Name")
                                            {
                                                col1 = true;
                                            }
                                            if (headerCol[i] == "Background Check")
                                            {
                                                col2 = true;
                                            }
                                            if (headerCol[i] == "First Name")
                                            {
                                                col3 = true;
                                            }
                                            if (headerCol[i] == "Last Name")
                                            {
                                                col4 = true;
                                            }
                                            if (headerCol[i] == "Email")
                                            {
                                                col5 = true;
                                            }
                                            if (headerCol[i] == "Payment")
                                            {
                                                col6 = true;
                                            }
                                            if (headerCol[i] == "Profession Type")
                                            {
                                                col7 = true;
                                            }

                                        }

                                        if (!col1 || !col2 || !col3 || !col4 || !col5 || !col6 || !col7)
                                        {
                                            ViewBag.Error = DecisionPointR.CsvHeaderNotMatch; // "CSV Column header not match ";
                                            objactionresult = View("ICBulkInvitation", invitevendor);
                                        }
                                        else
                                        {
                                            countIndex++;
                                            invitevendor.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForIC(objCSVReader, invitevendor, countIndex));


                                        }
                                    }


                                }
                            }
                            objCSVReader.Dispose();
                        }
                        invitevendor.pagesize = invitevendor.LstVendorCsv.Count();
                        invitevendor.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                        invitevendor.CsvFileName = exactfilename;
                        objactionresult = View("ICBulkInvitation", invitevendor);
                    }
                    else
                    {
                        if (object.Equals(file, null))
                        {
                            ViewBag.Error = DecisionPointR.UploadCsv;// "Please Upload CSV File";
                        }
                        else
                        {
                            ViewBag.Error = DecisionPointR.InvalidCsvFormat;// "Please Upload CSV Format File";
                        }

                        invitevendor.LstVendorCsv = null;
                        objactionresult = View("ICBulkInvitation", invitevendor);
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
        public ActionResult ICDeleteItem(int? id, string csvFileName)
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
                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + csvFileName;
                    inviteVendorModel = new InviteVendorBulk();

                    UpdateVendorModel objUpdateVendorModel = new UpdateVendorModel();
                    objViewModel = new ViewModel();
                    objUpdateVendorModel.No = id;
                    objViewModel.ReadAndWriteContentInCSVForIC(serverpathname, inviteVendorModel, objUpdateVendorModel, 1);
                    int countIndex = 0;
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    //Read the CSV file after update the record
                    CsvReader objCsvReader1 = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCsvReader1.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForIC(objCsvReader1, inviteVendorModel, countIndex));
                    }
                    objCsvReader1.Dispose();
                    objDecisionPointEngine = new DecisionPointEngine();

                    inviteVendorModel.flowDetails = objDecisionPointEngine.GetFlow();
                    inviteVendorModel.DocflowDetails = objDecisionPointEngine.GetDocFlow();

                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objactionresult = View("ICBulkInvitation", inviteVendorModel);
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
        /// DownloadICInviteSample
        /// </summary>
        /// <param name="Format">string Format</param>
        /// <createdby>rohit koshik</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>file</returns>
        public FileResult DownloadICInviteSample(string Format)
        {
            logMessage = new StringBuilder();
            FileResult objFileResult = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (Format == DecisionPointR.ICListWithID.Split('.')[0])
                {
                    var fileName = "ICListWithID.csv";
                    var filePath = Server.MapPath(ConfigurationManager.AppSettings["downloadcsvfile"] + DecisionPointR.ICListWithID);
                    objFileResult = File(filePath, "application/octet-stream", fileName);
                }
                else
                {
                    var fileName = DecisionPointR.ICListWithoutID;
                    var filePath = Server.MapPath(ConfigurationManager.AppSettings["downloadcsvfile"] + DecisionPointR.ICListWithoutID);
                    objFileResult = File(filePath, "application/octet-stream", fileName);
                }
                return objFileResult;

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                return objFileResult;

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        /// <summary>
        /// VendorUpdateItem
        /// </summary>
        /// <param name="No">int no</param>
        /// <param name="FName">string First Name</param>
        /// <param name="LName">string Last Name</param>
        /// <param name="CName">string Company Name</param>
        /// <param name="Email">string Email</param>
        /// <param name="FlowID">int FlowID</param>
        /// <param name="FlowText">string FlowText</param>
        /// <param name="PaymentType">int PaymentType</param>
        /// <param name="PaymentMode">string PaymentMode</param>
        ///  <createdby>Mamta gupta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        ///  <modifiedby>Bobi</modifiedby>
        ///  <modifieddate>16 july 2014</modifieddate>
        /// <returns>view vendor invite bulk</returns>
        [HttpPost]
        public ActionResult VendorUpdateItem(UpdateVendorModel updateVendorModel)
        {
            logMessage = new StringBuilder();
            int countIndex = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objActionResult = RedirectToAction("Login", "Login");
                }
                else
                {
                    inviteVendorModel = new InviteVendorBulk();
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();
                    objViewModel = new ViewModel();
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    inviteVendorModel.flowDetails = objDecisionPointEngine.GetFlow();
                    if (!object.Equals(Session["UserType"], null))
                    {
                        if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals("SuperAdmin"))
                        {
                            inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, companyId).Select(x => new VendorTypeResponse { VendorTypeName = x.categoryName, VendorTypeId = x.id });
                        }
                        else { inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }
                    }
                    else { inviteVendorModel.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false); }

                    string serverpathname = Server.MapPath(ConfigurationManager.AppSettings["uploadcsvinvitefile"]) + updateVendorModel.CsvFileName;
                    //called method for update the record in CSV file : 0 used for update record
                    objViewModel.ReadAndWriteContentInCSVForIC(serverpathname, inviteVendorModel, updateVendorModel, 0);
                    countIndex = 0;
                    inviteVendorModel.LstVendorCsv = new List<VendorCSV>();
                    //Read the CSV file after update the record
                    CsvReader objCsvReader1 = CSVFileReader.ReadCSvFile(serverpathname);
                    while (objCsvReader1.Read())
                    {
                        countIndex++;
                        inviteVendorModel.LstVendorCsv.Add(objViewModel.BindColFromCSVFileForIC(objCsvReader1, inviteVendorModel, countIndex));
                    }
                    objCsvReader1.Dispose();
                    inviteVendorModel.pagesize = inviteVendorModel.LstVendorCsv.Count();
                    inviteVendorModel.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objActionResult = View("ICBulkInvitation", inviteVendorModel);
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
            return objActionResult;
        }

        #endregion

        #region Manage IC
        /// <summary>
        /// ManageIC
        /// </summary>
        /// <param name="id">int id</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>16 jan 2014</createdDate>
        /// <returns>view manage ic</returns>
        [HttpGet]
        public ActionResult ManageIC(int id, string type)
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
                        type = id == 0 ? 1 : 0,

                    };
                    if (id.Equals(0))
                    {
                        //Called method for get internal staff details of particular company
                        objComapnyDashBoard.activeICdetails = objDecisionPointEngine.GetICdetail(filterRequest).OrderBy(x => x.InvitationStatus);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (!type.Equals(Shared.All))
                            {
                                objComapnyDashBoard.activeICdetails = objComapnyDashBoard.activeICdetails.Where(x => x.VType.StartsWith(type) || x.businessName.StartsWith(type)).ToList();
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.activeICdetails.Count();
                        objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }
                    else if (id.Equals(1))
                    {
                        //Called method for get internal staff details of particular company
                        objComapnyDashBoard.inactiveICdetails = objDecisionPointEngine.GetICdetail(filterRequest).OrderBy(x => x.InvitationStatus);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (!string.IsNullOrEmpty(type))
                            {
                                objComapnyDashBoard.inactiveICdetails = objComapnyDashBoard.inactiveICdetails.Where(x => x.VType.StartsWith(type) || x.businessName.StartsWith(type)).ToList();
                            }
                        }
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.inactiveICdetails.Count();
                        objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }
                    if (!object.Equals(Session["UserType"], null))
                    {
                        //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.SuperAdmin))
                        //{
                        //    objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetVendorType(string.Empty, companyId).ToList();
                        //}
                        //else { 
                        objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false).Select(x => new CompanyDashBoardResponse { categoryName = x.VendorTypeName, id = x.VendorTypeId }).ToList();
                        // }
                    }
                    #region Groupby Status
                    int count = 0;
                    IList<ICResponse> icDetails = new List<ICResponse>().ToList();
                    ICResponse objICResponse = new ICResponse()
                    {
                        fname = string.Empty,
                        lname = string.Empty,
                        emailId = string.Empty,
                        phone = string.Empty,
                        IsActive = false,
                        service = string.Empty,
                        zipcode = string.Empty,
                        businessName = string.Empty,
                        Id = 0,
                        companyid = string.Empty,
                        InvitationStatus = false,
                        VTId = 0,
                        VType = string.Empty,
                        LastInviteMailDate = null
                    };
                    if (!object.Equals(objComapnyDashBoard.activeICdetails, null))
                    {
                        var col = objComapnyDashBoard.activeICdetails;
                        icDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.activeICdetails.ToList())
                        {
                            if (list.InvitationStatus)
                            {
                                icDetails.Insert(count, objICResponse);
                                count++;
                                break;
                            }
                            count++;
                        }
                        objComapnyDashBoard.activeICdetails = icDetails.ToList();
                    }
                    else if (!object.Equals(objComapnyDashBoard.inactiveICdetails, null))
                    {
                        var col = objComapnyDashBoard.inactiveICdetails;
                        icDetails = col.ToList();
                        foreach (var list in objComapnyDashBoard.inactiveICdetails.ToList())
                        {
                            if (list.InvitationStatus)
                            {
                                icDetails.Insert(count, objICResponse);
                                count++;
                                break;
                            }
                            count++;
                        }
                        objComapnyDashBoard.inactiveICdetails = icDetails.ToList();
                    }


                    #endregion
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = PartialView("ManageIC", objComapnyDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
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
        /// search in IC
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>23 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInICs(string term, int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objComapnyDashBoard.SearchICdetails = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    filterRequest = new FilterRequest()
                    {
                        CompanyId = companyId,
                        type = type == 0 ? 1 : 0,
                    };
                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard.SearchICdetails = objDecisionPointEngine.GetICdetail(filterRequest);
                    if (!string.IsNullOrEmpty(term))
                    {
                        serach1 = objComapnyDashBoard.SearchICdetails.Where(x => (x.businessName.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(y => y.businessName).Distinct().ToList();
                        serach2 = objComapnyDashBoard.SearchICdetails.Where(x => (x.VType.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(y => y.VType).Distinct().ToList();
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
        #endregion

        #region Invite
        /// <summary>
        /// Invite
        /// </summary>
        /// <param name="id">string id</param> 
        /// <param name="">string type</param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view</returns>
        public ActionResult Invite(string id, int type)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard = new CompanyDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    IEnumerable<InviteResponse> finalinvitedetails = null;

                    if (type.Equals(1))
                    {
                        objComapnyDashBoard.currenrinviteDetails = objDecisionPointEngine.GetInviteDetails(userId, companyId).Where(x => x.Isdeleted != 2 && ((x.Type == Shared.AccecptedO || x.Type == Shared.DenyI || x.Type == Shared.DenyO || x.Type == Shared.PendingO || x.Type == Shared.ReceivedI) && (x.Isdeleted == 1 || x.Isdeleted == 0)));
                        finalinvitedetails = objComapnyDashBoard.currenrinviteDetails;
                        TempData["isCurrent"] = "1";
                    }
                    else if (type.Equals(0))
                    {
                        objComapnyDashBoard.removedinviteDetails = objDecisionPointEngine.GetInviteDetails(userId, companyId).Where(x => (x.Isdeleted == 2) || (x.Type == Shared.AccecptedI && x.Isdeleted == 1));
                        finalinvitedetails = objComapnyDashBoard.removedinviteDetails;
                        TempData["isCurrent"] = "0";
                    }
                    if (id.Equals(Shared.IReceived))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.UserId == userId && x.Status != 2 && x.Status != 1);
                    }
                    else if (id.Equals(Shared.OPending))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.CompanyId == companyId && x.Status != 2 && x.Status != 1);
                    }
                    else if (id.Equals(Shared.IOAccepted))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Status == 1 && x.Isdeleted != 2 && (x.Type == Shared.AccecptedO && x.Isdeleted == 1));
                    }
                    else if (id.Equals(Shared.IDenied))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Status == 2);
                    }
                    else if (!id.Equals(Shared.All))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Contact == id || x.CompanyName == id);
                    }
                    if (finalinvitedetails != null && finalinvitedetails.Count() > 0)
                    {
                        #region groupby invites
                        string invitetype = string.Empty;
                        int count = 0;

                        IList<InviteResponse> invitedetails = new List<InviteResponse>().ToList();
                        InviteResponse objInviteResponse = new InviteResponse()
                        {
                            CompanyId = string.Empty,
                            CompanyName = string.Empty,
                            Contact = string.Empty,
                            Date = System.DateTime.Now,
                            DocFlow = string.Empty,
                            EmailId = string.Empty,
                            FlowTableId = 0,
                            Phone = string.Empty,
                            RelationShip = string.Empty,
                            Status = 0,
                            TableId = 0,
                            Type = string.Empty,
                            UserId = 0,
                            UserType = string.Empty,
                        };
                        var col = finalinvitedetails;
                        invitedetails = col.ToList();
                        foreach (var list in finalinvitedetails.ToList())
                        {
                            if (!string.IsNullOrEmpty(invitetype))
                            {
                                if (list.Type != invitetype)
                                {
                                    invitedetails.Insert(count, objInviteResponse);
                                    count++;
                                }

                            }
                            invitetype = list.Type;
                            count++;
                        }
                        finalinvitedetails = invitedetails;
                        #endregion
                    }
                    if (type.Equals(1))
                    {
                        objComapnyDashBoard.currenrinviteDetails = finalinvitedetails;
                        if (!object.Equals(objComapnyDashBoard.currenrinviteDetails, null))
                            objComapnyDashBoard.pagesize = objComapnyDashBoard.currenrinviteDetails.Count();
                    }
                    else if (type.Equals(0))
                    {
                        objComapnyDashBoard.removedinviteDetails = finalinvitedetails;
                        if (!object.Equals(objComapnyDashBoard.removedinviteDetails, null))
                            objComapnyDashBoard.pagesize = objComapnyDashBoard.removedinviteDetails.Count();
                    }
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard.flowDetails = objDecisionPointEngine.GetDocFlow();
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
        /// InvitaionOperation
        /// </summary>
        /// <param name="id">int id</param>
        /// <param name="type">string type</param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view InvitaionOperation</returns>
        [HttpGet]
        public ActionResult InvitaionOperation(int id, int type)
        {

            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard = new CompanyDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();

                    objDecisionPointEngine.InvitaionOperation(id, type);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objactionresult = RedirectToAction("Invite", "Invitation", new { id = Shared.All, type = 1 });
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
        /// UpdateFlow
        /// </summary>
        /// <param name="UniqueId">int UniqueId</param>
        /// <param name="flowId">int flowId</param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>returns int type update</returns>
        public int UpdateFlow(int UniqueId, int flowId)
        {
            int IsUpdated = 0;
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    IsUpdated = objDecisionPointEngine.UpdateFlow(UniqueId, flowId);
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
            return IsUpdated;
        }

        /// <summary>
        /// ICInvites
        /// </summary>
        /// <param name="id">string id</param>
        /// <param name="type">int type</param>
        ///  <createdby>Mamta Gupta</createdby>
        ///  <createdDate>17 mar 2014</createdDate>
        /// <returns>view ICInvites</returns>
        public ActionResult ICInvites(string id, int type)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard = new CompanyDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    IEnumerable<InviteResponse> finalinvitedetails = null;

                    if (type.Equals(1))
                    {
                        objComapnyDashBoard.currenrinviteDetails = objDecisionPointEngine.GetICInvite(userId).Where(x => x.Isdeleted != 2 && x.Type != Shared.AccecptedI);
                        TempData["isCurrent"] = Shared.One;
                        finalinvitedetails = objComapnyDashBoard.currenrinviteDetails;
                    }
                    else if (type.Equals(0))
                    {
                        objComapnyDashBoard.removedinviteDetails = objDecisionPointEngine.GetICInvite(userId).Where(x => (x.Isdeleted == 2 || (x.Type == Shared.AccecptedI && x.Isdeleted == 1)));
                        TempData["isCurrent"] = Shared.Zero;
                        finalinvitedetails = objComapnyDashBoard.removedinviteDetails;
                    }

                    if (id.Equals(DecisionPointR.IReceived))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Status != 2 && x.Status != 1);
                    }

                    else if (id.Equals("IAccepted"))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Status == 1 || x.Type == Shared.AccecptedO);
                    }
                    else if (id.Equals("IDenied"))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Status == 2);
                    }
                    else if (id.Equals(Shared.PendingO))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Type == Shared.PendingO);
                    }
                    else if (!id.Equals(Shared.All))
                    {
                        finalinvitedetails = finalinvitedetails.Where(x => x.Contact == id || x.CompanyName == id);
                    }
                    if (finalinvitedetails != null && finalinvitedetails.Count() > 0)
                    {
                        #region groupby invites
                        string invitetype = string.Empty;
                        int count = 0;

                        IList<InviteResponse> invitedetails = new List<InviteResponse>().ToList();
                        InviteResponse objInviteResponse = new InviteResponse()
                        {
                            CompanyId = string.Empty,
                            CompanyName = string.Empty,
                            Contact = string.Empty,
                            Date = System.DateTime.Now,
                            DocFlow = string.Empty,
                            EmailId = string.Empty,
                            FlowTableId = 0,
                            Phone = string.Empty,
                            RelationShip = string.Empty,
                            Status = 0,
                            TableId = 0,
                            Type = string.Empty,
                            UserId = 0,
                            UserType = string.Empty,
                        };
                        // historydetails.Add(objUserDashBoardResponse);
                        var col = finalinvitedetails;
                        invitedetails = col.ToList();
                        foreach (var list in finalinvitedetails.ToList())
                        {
                            if (!string.IsNullOrEmpty(invitetype))
                            {
                                if (list.Type != invitetype)
                                {
                                    invitedetails.Insert(count, objInviteResponse);
                                    count++;
                                }

                            }
                            invitetype = list.Type;
                            count++;
                        }
                        finalinvitedetails = invitedetails;
                        #endregion
                    }
                    if (type.Equals(1))
                    {
                        objComapnyDashBoard.currenrinviteDetails = finalinvitedetails;
                        if (!object.Equals(objComapnyDashBoard.currenrinviteDetails, null))
                            objComapnyDashBoard.pagesize = objComapnyDashBoard.currenrinviteDetails.Count();
                    }
                    else if (type.Equals(0))
                    {
                        objComapnyDashBoard.removedinviteDetails = finalinvitedetails;
                        if (!object.Equals(objComapnyDashBoard.removedinviteDetails, null))
                            objComapnyDashBoard.pagesize = objComapnyDashBoard.removedinviteDetails.Count();
                    }
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard.flowDetails = objDecisionPointEngine.GetDocFlow();
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
        /// ICInvitaionOperation
        /// </summary>
        /// <param name="id">int id</param>
        /// <param name="type">string type </param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>18 mar 2014</createdDate>
        /// <returns>view ICInvitaionOperation</returns>
        public ActionResult ICInvitaionOperation(int id, string type)
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objComapnyDashBoard = new CompanyDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    objDecisionPointEngine.InvitaionOperation(id, Convert.ToInt32(type));

                    objactionresult = RedirectToAction("ICInvites", "Invitation", new { id = "All", type = 1 });

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
        /// search in company
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>12 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInInvite(string term, int type, int inviteType)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objComapnyDashBoard.searchInviteDetails = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();

                    //0 for current list page
                    if (type.Equals(0))
                    {
                        if (inviteType.Equals(0))
                        {
                            objComapnyDashBoard.searchInviteDetails = objDecisionPointEngine.GetInviteDetails(userId, companyId).Where(x => x.Isdeleted != 2 && ((x.Type == DecisionPointR.accecptedO || x.Type == DecisionPointR.denyI || x.Type == DecisionPointR.denyO || x.Type == DecisionPointR.pendingO || x.Type == DecisionPointR.receivedI) && (x.Isdeleted == 1 || x.Isdeleted == 0)));
                        }
                        else
                        {
                            objComapnyDashBoard.searchInviteDetails = objDecisionPointEngine.GetICInvite(userId).Where(x => x.Isdeleted != 2 && x.Type != Shared.AccecptedI);
                        }
                        //objComapnyDashBoard.searchInviteDetails = objComapnyDashBoard.searchInviteDetails.Where(x => x.CompanyName.StartsWith(term, StringComparison.OrdinalIgnoreCase) || x.Contact.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    //1 for past list
                    else if (type.Equals(1))
                    {
                        if (inviteType.Equals(0))
                        {
                            objComapnyDashBoard.searchInviteDetails = objDecisionPointEngine.GetInviteDetails(userId, companyId).Where(x => (x.Isdeleted == 2) || (x.Type == DecisionPointR.accecptedI && x.Isdeleted == 1));
                        }
                        else
                        {
                            objComapnyDashBoard.searchInviteDetails = objDecisionPointEngine.GetICInvite(userId).Where(x => (x.Isdeleted == 2 || (x.Type == Shared.AccecptedI && x.Isdeleted == 1)));
                        }
                        // objComapnyDashBoard.searchInviteDetails = objComapnyDashBoard.searchInviteDetails.Where(x => x.CompanyName.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    if (!object.Equals(objComapnyDashBoard.searchInviteDetails, null))
                    {
                        if (objComapnyDashBoard.searchInviteDetails.Count() > 0)
                        {
                            serach1 = objComapnyDashBoard.searchInviteDetails.Where(x => x.CompanyName.StartsWith(term, StringComparison.OrdinalIgnoreCase)).Select(x => x.CompanyName).ToList();
                            serach2 = objComapnyDashBoard.searchInviteDetails.Where(x => x.Contact.StartsWith(term, StringComparison.OrdinalIgnoreCase)).Select(x => x.Contact).ToList();
                            serachFinal = serach1.Union(serach2).ToList();
                        }
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

        [HttpGet]
        public ActionResult AddInvite()
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
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

        /// <summary>
        /// Validate Ic with email id and Ic type
        /// </summary>
        /// <param name="email">email</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>19 Jan 2015</CreatedDate>
        /// <returns>int</returns>
        [HttpGet]
        public int ValidateIcWithIcType(string email)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                return objDecisionPointEngine.ValidateIcWithIcType(email);
            }
            catch
            {

                throw;
            }
        }


        [HttpGet]
        public ActionResult ICApproval(string type)
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
                    objComapnyDashBoard.ICBGReviewDetail = objDecisionPointEngine.GetICBackgroundReviewDetails(companyId, 0, string.Empty).OrderBy(x => x.BGStatus);

                    if (!type.Equals(Shared.All))
                    {
                        string filterType = string.Empty;
                        if (!string.IsNullOrEmpty(type))
                        {
                            filterType = type.Split(char.Parse(Shared.Colon))[1];
                            if (filterType.Equals(Shared.One))
                            {
                                objComapnyDashBoard.ICBGReviewDetail = objComapnyDashBoard.ICBGReviewDetail.Where(x => x.fname == type.Split(char.Parse(Shared.Colon))[0]).OrderBy(x => x.BGStatus);
                            }
                            else 
                            {
                                if (!string.IsNullOrEmpty(type.Split(char.Parse(Shared.Colon))[0]) && !string.Equals(type.Split(char.Parse(Shared.Colon))[0], "IC Type"))
                                {
                                    objComapnyDashBoard.ICBGReviewDetail = objComapnyDashBoard.ICBGReviewDetail.Where(x => x.VType == type.Split(char.Parse(Shared.Colon))[0]).OrderBy(x => x.BGStatus);
                                }
                                if (!string.IsNullOrEmpty(type.Split(char.Parse(Shared.Colon))[1]) && !string.Equals(type.Split(char.Parse(Shared.Colon))[1], "Status"))
                                {
                                    objComapnyDashBoard.ICBGReviewDetail = objComapnyDashBoard.ICBGReviewDetail.Where(x => x.BGStatus == type.Split(char.Parse(Shared.Colon))[1]).OrderBy(x => x.BGStatus);
                                }
                            }
                        }
                    }

                    if (!object.Equals(objComapnyDashBoard.ICBGReviewDetail, null))
                    {
                        objComapnyDashBoard.pagesize = objComapnyDashBoard.ICBGReviewDetail.Select(x => new { x.fname, x.lname, x.businessName, x.emailId, x.mname, x.VTId, x.VType, x.ICUserId, x.ICCompanyId, x.LastInviteMailDate, x.IsRegistred }).Distinct().ToList().Count();
                    }
                    //#region Groupby Status
                    //int count = 0;
                    //string status1 = string.Empty;
                    //string status2= string.Empty;
                    //IList<ICResponse> icDetails = new List<ICResponse>().ToList();
                    //ICResponse objICResponse = new ICResponse()
                    //{
                    //    fname = string.Empty,
                    //    mname = string.Empty,
                    //    lname = string.Empty,
                    //    businessName = string.Empty,
                    //    emailId = string.Empty,
                    //    VTId = 0,
                    //    VType = string.Empty,
                    //    BGStatus = string.Empty,
                    //    Source = string.Empty,
                    //    ICUserId = 0,
                    //    ICCompanyId = string.Empty,
                    //    SterlingOrderId = string.Empty,
                    //    IsRegistred = false,
                    //    LastInviteMailDate = null
                    //};
                    //if (!object.Equals(objComapnyDashBoard.ICBGReviewDetail, null))
                    // {
                    //     var col = objComapnyDashBoard.ICBGReviewDetail;
                    //    icDetails = col.ToList();
                    //    foreach (var list in objComapnyDashBoard.ICBGReviewDetail.Select(x => new { x.fname, x.lname, x.businessName, x.emailId, x.mname, x.VTId, x.VType, x.ICUserId, x.ICCompanyId, x.LastInviteMailDate, x.IsRegistred }).Distinct().ToList())
                    //    {
                    //        status2 = status1;
                    //        if (objComapnyDashBoard.ICBGReviewDetail.Where(x => x.ICUserId == list.ICUserId && x.BGStatus == Shared.Pending).Count() > 0)
                    //        {
                    //            status1 = Shared.Pending;
                    //        }
                    //        else if (objComapnyDashBoard.ICBGReviewDetail.Where(x => x.ICUserId == list.ICUserId && x.BGStatus == Shared.InProgress).Count() > 0)
                    //        {
                    //            status1 = Shared.InProgress;
                    //        }
                    //        else if (objComapnyDashBoard.ICBGReviewDetail.Where(x => x.ICUserId == list.ICUserId && x.BGStatus == Shared.Review).Count() > 0)
                    //        {
                    //            status1 = Shared.Review;
                    //        }
                    //        else
                    //        {
                    //            status1 = Shared.Accepted;
                    //        }
                    //        if (!string.IsNullOrEmpty(status1))
                    //        {
                    //            if (!status1.Equals(status2))
                    //            {
                    //                icDetails.Insert(count, objICResponse);
                    //                count++;
                    //                //break;
                    //            }
                    //        }
                    //        count++;
                    //    }
                    //    objComapnyDashBoard.ICBGReviewDetail = icDetails.ToList();
                    //}

                    
                    //#endregion
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);

                    objComapnyDashBoard.VendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == false).Select(x => new CompanyDashBoardResponse { categoryName = x.VendorTypeName, id = x.VendorTypeId }).ToList();
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View();
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
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
        /// search in IC
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>23 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInICApproval(string term)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objComapnyDashBoard = new CompanyDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objComapnyDashBoard.ICBGReviewDetail = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);

                    objDecisionPointEngine = new DecisionPointEngine();
                    objComapnyDashBoard.ICBGReviewDetail = objDecisionPointEngine.GetICBackgroundReviewDetails(companyId, 1, term);
                    if (!string.IsNullOrEmpty(term))
                    {
                        serach1 = objComapnyDashBoard.ICBGReviewDetail.Select(x => x.fname).Distinct().ToList();
                        serachFinal = serach1.ToList();
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

    }
}
