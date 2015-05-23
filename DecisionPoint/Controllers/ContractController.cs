using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DecisionPoint.Controllers.WebApi;
using DecisionPoint.Models;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;

namespace DecisionPoint.Controllers
{
    public class ContractController : Controller
    {
        #region GlobalVariable
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        ContractModel objContractModel = null;
        CompanyDashBoard objComapnyDashBoard = null;
        int userId = 0;
        string companyId = string.Empty;
        ActionResult objactionresult = null;
        FilterRequest filterRequest = null;
        CreateContractRequest objCreateContractRequest = null;
        CreateContractResponse objCreateContractResponse = null;
        #endregion

        #region Contract Master

        /// <summary>
        /// show page of contract master
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 nov 2014</CreatedDate>
        /// <returns>view</returns>
        public ActionResult ViewContractType()
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
                    objComapnyDashBoard.ContractTypeDetails = objDecisionPointEngine.GetContractType("admin", Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    IList<CompanyDashBoardResponse> GetList = objComapnyDashBoard.ContractTypeDetails.OrderBy(q => q.ContractTypeName).ToList();
                    #region Divide in Four Columns

                    #region list
                    List<LstProp> lstTest = new List<LstProp>();

                    int count = GetList.Count();

                    for (int i = 0; i < count; i++)
                    {
                        LstProp lstprop = new LstProp();
                        lstprop.Col1 = GetList[i].ContractTypeName;
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
                    objComapnyDashBoard.pagesize = objComapnyDashBoard.ContractTypeDetails.Count();
                    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    //assign the view model
                    ViewData.Model = objComapnyDashBoard;
                    objactionresult = View("ViewContractType", objComapnyDashBoard);
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
        /// Add and update contract type
        /// </summary>
        /// <param name="contractType">contractType</param>
        /// <param name="status">status</param>
        /// <param name="contractId">contractId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 nov 2014</CreatedDate>
        /// <returns>int</returns>
        [HttpPost, ValidateInput(false)]
        public int AddContractType(string contractType, string status, int contractId)
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
                if (!string.IsNullOrEmpty(contractType))
                {
                    if (status.Equals("Save"))
                        Isinserted = objDecisionPointEngine.AddContractType(contractType, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                    else if (status.Equals("Edit"))
                        Isinserted = objDecisionPointEngine.UpdateContractType(contractId, contractType, Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
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
        [HttpPost, ValidateInput(false)]
        public int DisaEnaContractType(int contractId, string isActive)
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
                Isupdated = objDecisionPointEngine.DisaEnaContractType(contractId, isActivestatus);

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

        #region Create Contract
        /// <summary>
        /// Create  Contract action
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>19 Nov 2014</CreatedDate>
        /// <returns>view </returns>
        [HttpGet]
        public ActionResult Create(int id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objContractModel = new ContractModel();
                objDecisionPointEngine = new DecisionPointEngine();
                objCreateContractResponse = new CreateContractResponse();
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                objContractModel.StateList = objDecisionPointEngine.GetStateList();
                if (!id.Equals(0))
                {
                    objCreateContractResponse = objDecisionPointEngine.GetContractListAsPerId(id);
                    objContractModel.TblId = objCreateContractResponse.Id;
                    objContractModel.BusinessName = objCreateContractResponse.BusinessName;
                    objContractModel.StreetName = objCreateContractResponse.StreetName;
                    objContractModel.StreetNumber = objCreateContractResponse.StreetNumber;
                    objContractModel.Direction = objCreateContractResponse.Direction;
                    objContractModel.CityName = objCreateContractResponse.CityName;
                    objContractModel.StateId = objCreateContractResponse.StateId;
                    objContractModel.ZipCode = objCreateContractResponse.ZipCode;
                    objContractModel.FName = objCreateContractResponse.FName;
                    objContractModel.MName = objCreateContractResponse.MName;
                    objContractModel.LName = objCreateContractResponse.LName;
                    objContractModel.OfficePhone = objCreateContractResponse.OfficePhone;
                    objContractModel.DirectPhone = objCreateContractResponse.DirectPhone;
                    objContractModel.EmailId = objCreateContractResponse.EmailId;
                    objContractModel.Role = objCreateContractResponse.Role;
                    objContractModel.ExecutedDate = objCreateContractResponse.ExecutedDate;
                    objContractModel.ContractDate = objCreateContractResponse.ContractDate;
                    objContractModel.ExpirationDate = objCreateContractResponse.ExpirationDate;
                    objContractModel.ExpirationDateReminder = objCreateContractResponse.ExpirationDateReminder;
                    //objContractModel.Paragraph = objCreateContractResponse.Paragraph;
                    //objContractModel.Section = objCreateContractResponse.Section;
                    //objContractModel.SubSection = objCreateContractResponse.SubSection;
                    //objContractModel.EventDate = objCreateContractResponse.EventDate;
                    //objContractModel.EventDateReminder = objCreateContractResponse.EventDateReminder;
                    //objContractModel.Notes = objCreateContractResponse.Notes;
                    objContractModel.GeneralComments = objCreateContractResponse.GeneralComments;
                    objContractModel.ManagerName = objCreateContractResponse.ManagerName;
                    objContractModel.IsActive = objCreateContractResponse.IsActive;
                    objContractModel.Title = objCreateContractResponse.Title;
                    objContractModel.OwnerName = objCreateContractResponse.OwnerName;
                    objContractModel.ExecutedByName = objCreateContractResponse.ExecutedByName;
                    objContractModel.ContractWithName = objCreateContractResponse.ContractWithName;
                    objContractModel.OwnerId = objCreateContractResponse.OwnerId;
                     objContractModel.ContractWithId = objCreateContractResponse.ContractWithId;
                     objContractModel.ContractorCompanyId = objCreateContractResponse.ContractorCompanyId;
                     objContractModel.ExecutedById = objCreateContractResponse.ExecutedById;
                     objContractModel.ServiceName = objCreateContractResponse.ServiceName;
                     objContractModel.ServiceId = objCreateContractResponse.ServiceId;
                   
                }
                objContractModel.titleDetails = objDecisionPointEngine.GetTitle(Shared.Company.ToLower(), companyId);
                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ContractUploadedDocPath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objContractModel.uploadedcontentpath = folderDirectory;

                objContractModel.hostURL = ViewModel.GetSiteRoot();
                ViewData.Model = objContractModel;
                objactionresult = View();
                // return View();

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
        /// Get Internal Staff details
        /// </summary>
        /// <param name="term">serach keyword</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>20 Nov 2014</CreatedDate>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult GetInternalStaff(string term)
        {
            logMessage = new StringBuilder();
            IEnumerable<InternalstaffResponse> list = null;
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
                        type = 0,
                        servicefilter = string.Empty,
                        titlefilter = string.Empty,
                        locationfilter = string.Empty,

                    };
                    //Called method for get internal staff details of particular company
                    objComapnyDashBoard.activeStaffdetails = objDecisionPointEngine.GetInternalstaffdetail(filterRequest).Where(x => x.invitationStatus == true).ToList();
                    list = objComapnyDashBoard.activeStaffdetails.Where(m => m.fname.ToLower().StartsWith(term.ToLower()) || m.title.ToLower().StartsWith(term.ToLower()));
                    

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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetVendorClient(string term)
        {
            logMessage = new StringBuilder();
            var list = new List<UserDashBoard>();
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
                        isActive = true,
                        filtertype = Shared.None,
                        uType = Shared.Vendor,
                    };
                    //Called method for get internal staff details of particular company
                    objComapnyDashBoard.vendordetails = objDecisionPointEngine.GetVendorList(filterRequest);
                    filterRequest.uType = Shared.Client;
                    objComapnyDashBoard.vendordetails = objComapnyDashBoard.vendordetails.Union(objDecisionPointEngine.GetVendorList(filterRequest));

                    // list1.Add(new Tuple<string,int>(objComapnyDashBoard.vendordetails.Where(m => m.contact.ToLower().StartsWith(term.ToLower())).Select(x=>new {x.contact,x.Id})));
                    var list1 = objComapnyDashBoard.vendordetails.Where(m => m.contact.ToLower().StartsWith(term.ToLower()) && m.UserType == Shared.Vendor).Select(x => new UserDashBoard { FName = x.contact, Id = (int)x.Id, UserType = Shared.Vendor, CompanyId = x.CompanyId });
                    var list2 = objComapnyDashBoard.vendordetails.Where(m => m.contact.ToLower().StartsWith(term.ToLower()) && m.UserType == Shared.Client).Select(x => new UserDashBoard { FName = x.contact, Id = (int)x.Id, UserType = Shared.Client, CompanyId = x.CompanyId });
                    list = list1.Union(list2).ToList();
                    ViewData.Model = objComapnyDashBoard;

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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// upload
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>21 nov 2014</CreatedDate>
        /// </summary>
        public void upload()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ContractUploadedDocPath"], CultureInfo.InvariantCulture) + userId;
                    UploadController.UploadDocByFolderPath(folderDirectory);
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
        /// Delete Doc 
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="id">id</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>21 Nov 2014</CreatedDate>
        [HttpPost]
        public void DeleteDocVideo(string name, int id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                //Remove video or doc umnet form folder
                string filepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ContractUploadedDocPath"], CultureInfo.InvariantCulture) + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)));
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
                    objDecisionPointEngine.DeleteDocVideo(id, 4);// type 4 for contrcat doc
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

        [HttpPost]
        public ActionResult SetCreateContract(ContractModel objContractModel)
        {
            logMessage = new StringBuilder();
            try
            {
               objDecisionPointEngine = new DecisionPointEngine();
               logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["CompanyId"])))
                {
                    string companyId = Convert.ToString(Session["CompanyId"]);
                    userId = Convert.ToInt32(Session["UserId"]);
                    objCreateContractRequest = new CreateContractRequest()
                    {
                        #region Parameter Assignment

                        ManagerName = objContractModel.ManagerName,
                        OwnerId = objContractModel.OwnerId,
                        ContractWithId = objContractModel.ContractWithId,
                        ExecutedById = objContractModel.ExecutedById,
                        ExecutedDate = objContractModel.ExecutedDate,
                        ExpirationDate = objContractModel.ExpirationDate,
                        ContractDate = objContractModel.ContractDate,
                        ExpirationDateReminder = objContractModel.ExpirationDateReminder,
                        GeneralComments = objContractModel.GeneralComments,
                        Paragraph = objContractModel.Paragraph,
                        Section = objContractModel.Section,
                        SubSection = objContractModel.SubSection,
                        Notes = objContractModel.Notes,
                        EventDate = objContractModel.EventDate,
                        EventDateReminder = objContractModel.EventDateReminder,
                        CreatorCompanyId = companyId,
                        ContractorCompanyId = objContractModel.ContractorCompanyId,
                        IsActive = true,
                        IsDeleted = false,
                        IsCompleted = objContractModel.IsCompleted,
                        CreatedBy = userId,
                        ServiceList = objContractModel.SelectedServiceList,
                        FilePathStr = objContractModel.FilePathStr,
                        Role = objContractModel.Role,
                        Title = objContractModel.Title,
                        EventList=objContractModel.EventList,
                        Id=objContractModel.TblId
                        #endregion
                    };
                    int result=0;
                    if (!objContractModel.TblId.Equals(0)) { result = objDecisionPointEngine.UpdateCreateContract(objCreateContractRequest); }
                    else { result = objDecisionPointEngine.SetCreateContract(objCreateContractRequest); }
                    
                    if (result > 0)
                    {
                        TempData["SuccessMsg"] = "Saved Sucess..!!";
                    }
                    else
                    {
                        TempData["SuccessMsg"] = "Something went wrong, please try after some time.";
                    }
                    objactionresult = RedirectToAction("Create", "Contract", new { id = 0 });
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }

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
            return objactionresult;
        }

        [HttpPost]
        public int SaveNonMembers(ContractModel objContractModel)
        {
            logMessage = new StringBuilder();
            int IsInserted = 0;
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objCreateContractRequest = new CreateContractRequest()
                    {
                        EmailId = objContractModel.EmailId,
                        FName = objContractModel.FName,
                        MName = objContractModel.MName,
                        LName = objContractModel.LName,
                        BusinessName = objContractModel.CompanyName,
                        StreetNumber = objContractModel.StreetNumber,
                        Direction = objContractModel.Direction,
                        StreetName = objContractModel.StreetName,
                        CityName = objContractModel.CityName,
                        StateId = objContractModel.StateId,
                        ZipCode = objContractModel.ZipCode,
                        OfficePhone = objContractModel.OfficePhone,
                        DirectPhone = objContractModel.DirectPhone,
                        CreatedBy = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                        CreatorCompanyId = companyId,
                        FlowId = objContractModel.FlowId,
                    };
                    if (string.IsNullOrEmpty(objCreateContractRequest.EmailId))
                    {
                        objCreateContractRequest.EmailId = string.Empty;
                    }
                    IsInserted = objDecisionPointEngine.SaveNonMember(objCreateContractRequest);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

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
            return IsInserted;
        }

        /// <summary>
        /// search in company
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>12 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInContract(string term,int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objContractModel.ContractSearchResult = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    //Create Object for Get User DashBoard Details
                    objDecisionPointEngine = new DecisionPointEngine();
                    objContractModel = new ContractModel();
                    //0 for contract list page
                    if (type.Equals(0))
                    {
                        objContractModel.ContractSearchResult = objDecisionPointEngine.SerachInContract(term).Where(x => x.CreatorCompanyId == companyId).ToList();
                        objContractModel.ContractSearchResult = objContractModel.ContractSearchResult.Where(x => x.BusinessName.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    //1 for my contract page
                    else if (type.Equals(1))
                    {
                        objContractModel.ContractSearchResult = objDecisionPointEngine.SerachInContract(term).Where(x => x.ContractorCompanyId == companyId).ToList();
                        objContractModel.ContractSearchResult = objContractModel.ContractSearchResult.Where(x => x.CompanyName.StartsWith(term, StringComparison.OrdinalIgnoreCase)).ToList();
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
            return this.Json(objContractModel.ContractSearchResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetNonMemberDetails
        /// </summary>
        /// <param name="userId">userId</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 17 2014</CreatedDate>
        /// <returns>JsonResult</returns>
        [HttpGet]
        public JsonResult GetNonMemberDetails(int userId)
        {
            logMessage = new StringBuilder();
            var list = new List<NonMemberResponse>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();                
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {                   
                    list = objDecisionPointEngine.GetNonMemberDetails(userId);
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
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int InviteNonMember(InviteNonMember objInviteNonMember)
        {
            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                
                NonMemberResponse objNonMemberResponse = new NonMemberResponse()
                {
                    DocFlowId = objInviteNonMember.DocFlowId,
                    UserId = Convert.ToInt32(objInviteNonMember.UserId, CultureInfo.InvariantCulture),
                    CreatedBy = objInviteNonMember.CreatedBy,
                    UserCompanyId = objInviteNonMember.UserCompanyId,
                    CreatorCompanyId = objInviteNonMember.CreatorCompanyId,
                    CreatorCompanyName=objInviteNonMember.CreatorCompanyName,
                    fName=objInviteNonMember.fName,
                    lName=objInviteNonMember.lName,
                    emailId=objInviteNonMember.emailId,
                    Password=objInviteNonMember.Credentails,
                    SiteRoot=ViewModel.GetSiteRoot(),
                };
                result = objDecisionPointEngine.InviteNonMember(objNonMemberResponse);
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
            return result;
        }

        [HttpGet]
        public JsonResult GetContrcatDoc(int contrcatId)
        {
            logMessage = new StringBuilder();
            var list = new List<CommContentRequest>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    list = objDecisionPointEngine.GetContrcatDoc(contrcatId).ToList();
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
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Contract List
        /// <summary>
        /// get contract list
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContractList(string id,int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objContractModel = new ContractModel();
                objDecisionPointEngine = new DecisionPointEngine();
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (id.Equals(Shared.All))
                {
                    objContractModel.ContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == true).ToList();
                    objContractModel.PastContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == false).ToList();
                }
                else
                {
                    //0 used for filter by types
                    if (type.Equals(0))
                    {
                        objContractModel.ContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == true && x.Status == id).ToList();
                        objContractModel.PastContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == false && x.Status == id).ToList();
                    }
                        //1 used for serach funtionality
                    else if (type.Equals(1))
                    {
                        objContractModel.ContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == true && x.BusinessName == id).ToList();
                        objContractModel.PastContractList = objDecisionPointEngine.GetContractList(userId).Where(x => x.IsActive == false && x.BusinessName == id).ToList();
                    }
                }
                objContractModel.StateList = objDecisionPointEngine.GetStateList();
                if (!object.Equals(objContractModel, null))
                {
                    objContractModel.PageSize = objContractModel.ContractList.Count;
                }
                objContractModel.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                ViewData.Model = objContractModel;
                objactionresult = View();
                // return View();

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
        /// delete contrcat
        /// </summary>
        /// <param name="contractId">contractId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 05 2014</CreatedDate>
        /// <returns>json result</returns>
        public JsonResult DeleteContract(int contractId)
        {
            int result = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                result = objDecisionPointEngine.DeleteContract(contractId);

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
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// delete contrcat
        /// </summary>
        /// <param name="contractId">contractId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 05 2014</CreatedDate>
        /// <returns>json result</returns>
        public JsonResult ReActiveContract(int contractId)
        {
            int result = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                result = objDecisionPointEngine.ReActiveContract(contractId);

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
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get contract list
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyContract(string id,int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objContractModel = new ContractModel();
                objDecisionPointEngine = new DecisionPointEngine();
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (id.Equals(Shared.All))
                {
                    objContractModel.ContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == true).ToList();
                    objContractModel.PastContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == false).ToList();
                }
                else
                {
                    if (type.Equals(0))
                    {
                        objContractModel.ContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == true && x.Status == id).ToList();
                        objContractModel.PastContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == false && x.Status == id).ToList();
                    }
                    else
                    {
                        objContractModel.ContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == true && x.BusinessName == id).ToList();
                        objContractModel.PastContractList = objDecisionPointEngine.GetMyContract(userId).Where(x => x.IsActive == false && x.BusinessName == id).ToList();
                    }
                }

                objContractModel.StateList = objDecisionPointEngine.GetStateList();
                if (!object.Equals(objContractModel, null))
                {
                    objContractModel.PageSize = objContractModel.ContractList.Count;
                }
                objContractModel.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                ViewData.Model = objContractModel;
                objactionresult = View();
                // return View();

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

        #region View Contrcat

        /// <summary>
        /// Create  Contract action
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>19 Nov 2014</CreatedDate>
        /// <returns>view </returns>
        [HttpGet]
        public ActionResult View(int id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objContractModel = new ContractModel();
                objDecisionPointEngine = new DecisionPointEngine();
                objCreateContractResponse = new CreateContractResponse();
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                objContractModel.StateList = objDecisionPointEngine.GetStateList();
                if (!id.Equals(0))
                {                    
                    objCreateContractResponse = objDecisionPointEngine.GetContractListAsPerId(id);
                    objContractModel.TblId = objCreateContractResponse.Id;
                    objContractModel.BusinessName = objCreateContractResponse.BusinessName;
                    objContractModel.StreetName = objCreateContractResponse.StreetName;
                    objContractModel.StreetNumber = objCreateContractResponse.StreetNumber;
                    objContractModel.Direction = objCreateContractResponse.Direction;
                    objContractModel.CityName = objCreateContractResponse.CityName;
                    objContractModel.StateId = objCreateContractResponse.StateId;
                    objContractModel.ZipCode = objCreateContractResponse.ZipCode;
                    objContractModel.FName = objCreateContractResponse.FName;
                    objContractModel.MName = objCreateContractResponse.MName;
                    objContractModel.LName = objCreateContractResponse.LName;
                    objContractModel.OfficePhone = objCreateContractResponse.OfficePhone;
                    objContractModel.DirectPhone = objCreateContractResponse.DirectPhone;
                    objContractModel.EmailId = objCreateContractResponse.EmailId;
                    objContractModel.Role = objCreateContractResponse.Role;
                    objContractModel.ExecutedDate = objCreateContractResponse.ExecutedDate;
                    objContractModel.ContractDate = objCreateContractResponse.ContractDate;
                    objContractModel.EventDate = objCreateContractResponse.EventDate;
                    objContractModel.ExpirationDate = objCreateContractResponse.ExpirationDate;
                    objContractModel.ExpirationDateReminder = objCreateContractResponse.ExpirationDateReminder;
                    objContractModel.EventDateReminder = objCreateContractResponse.EventDateReminder;
                    objContractModel.Paragraph = objCreateContractResponse.Paragraph;
                    objContractModel.Section = objCreateContractResponse.Section;
                    objContractModel.SubSection = objCreateContractResponse.SubSection;
                    objContractModel.Notes = objCreateContractResponse.Notes;
                    objContractModel.GeneralComments = objCreateContractResponse.GeneralComments;
                    objContractModel.ManagerName = objCreateContractResponse.ManagerName;
                    objContractModel.IsActive = objCreateContractResponse.IsActive;
                    objContractModel.Title = objCreateContractResponse.Title;
                    objContractModel.OwnerName = objCreateContractResponse.OwnerName;
                    objContractModel.ExecutedByName = objCreateContractResponse.ExecutedByName;
                    objContractModel.ContractWithName = objCreateContractResponse.ContractWithName;
                    objContractModel.ServiceName = objCreateContractResponse.ServiceName;
                    string stateName = objContractModel.StateList.Where(x => x.StateId == objCreateContractResponse.StateId).Select(x => x.SateName).FirstOrDefault();
                    objContractModel.AddressLine1 = objCreateContractResponse.StreetNumber + Shared.Comma + objCreateContractResponse.Direction + Shared.Comma + objCreateContractResponse.StreetName + Shared.Comma + objCreateContractResponse.CityName + Shared.Comma + stateName;
                }
                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ContractUploadedDocPath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objContractModel.uploadedcontentpath = folderDirectory;

                objContractModel.hostURL = ViewModel.GetSiteRoot();
                ViewData.Model = objContractModel;
                objactionresult = View();
                // return View();

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
        /// get contract events list
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <createdby>Bobi</createdby>
        /// <createddate>Dec 25 2014</createddate>
        /// <returns>JsonResult</returns>
        [HttpGet]
        public JsonResult GetContractEventsListAsPerId(int id)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            IList<ContractEvent> GetContractEvents = null;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
           try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    GetContractEvents=  objDecisionPointEngine.GetContractEventsListAsPerId(id);
                }
                else
                {
                    RedirectToAction("Login", "Login");
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
            return Json(GetContractEvents, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
