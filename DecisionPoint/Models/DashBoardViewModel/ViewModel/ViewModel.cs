using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using DecisionPoint.Models;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using CsvHelper;
using System.Net;
using System.Xml;


// ******************************************************************************************************************************
//                                                 class:ViewModel.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date        |  Created By       | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Aug 30, 2014     |Bobi              | This class holds the logic for rendered the data on view
// ******************************************************************************************************************************
namespace DecisionPoint.Models.DashBoardViewModel.ViewModel
{
    public class ViewModel
    {
        #region GlobalVariable

        #region Data Members
        int modifiedBy = 0;
        List<string> clientList = null;
        int userId = 0;
        string companyId = string.Empty;
        string userType = string.Empty;
        string businessName = string.Empty;
        string parentUserId = string.Empty;
        string parentCompanyId = string.Empty;
        string parentUserType = string.Empty;
        List<string> icCompamiesList = null;
        IList<string> sessionList = null;
        string Url = string.Empty;
        string xmlTextFormat = string.Empty;
        string sterlingOrderInitiateUrl = string.Empty;
        string sterlingReviewResultUrl = string.Empty;
        string icClientsIds = string.Empty;
        #endregion

        #region Classs Objects
        SterlingModel objSterlingModel = null;
        HttpWebRequest objHttpWebRequest = null;
        HttpWebResponse objHttpWebResponse = null;
        Stream objRequestStream = null;
        DecisionPointEngine objDecisionPointEngine = null;
        SterlingResponse objSterlingResponse = null;
        UserDashBoard objUserDashBoard = null;
        UserDashBoardResponse objUserDashBoardResponse = null;
        MyDashBoardResponse objMyDashBoardResponse = null;
        MyDashBoard objMyDashBoard = null;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        MyDashBoardRequest objMyDashBoardRequest = null;
        UserDashBoardRequest objUserDashBoardRequest = null;
        CompanyDashBoardRequest objCompanyDashBoardRequest = null;
        BusinessCryptography objbusinessCryptography = null;
        BackGroundCheckMasterRequest objBackGroundCheckMasterRequest = null;
        MailInviteFormat objMailInviteFormat = null;
        IList<ICClientPermissionRequest> icPerClientList = null;
        VendorsList objVendorsList = null;
        #endregion
        #endregion

        #region Public Region

        #region Communications Pages
        /// <summary>
        /// Used for get the communication details dispaly on My history View
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">type</param>
        /// <returns>UserDashBoard</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public UserDashBoard GetHistoryCommunicationDetails(string id, string type)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                //called method from bsuiness layer for get the communication details
                objUserDashBoard.HistoryDetails = objDecisionPointEngine.GetHistoryDetails(userId, id, companyId, type);
                if (objUserDashBoard.HistoryDetails != null && objUserDashBoard.HistoryDetails.Count() > 0)
                {
                    #region groupby category
                    string doctype = string.Empty;
                    int count = 0;

                    IList<UserDashBoardResponse> historyDetails = new List<UserDashBoardResponse>().ToList();
                    objUserDashBoardResponse = new UserDashBoardResponse()
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
                    historyDetails = col.ToList();
                    foreach (var list in objUserDashBoard.HistoryDetails.ToList())
                    {
                        if (!string.IsNullOrEmpty(doctype))
                        {
                            if (list.reqDocType != doctype)
                            {
                                historyDetails.Insert(count, objUserDashBoardResponse);
                                count++;
                            }

                        }
                        doctype = list.reqDocType;
                        count++;
                    }
                    objUserDashBoard.HistoryDetails = historyDetails;
                    #endregion
                    objUserDashBoard.PageSize = objUserDashBoard.HistoryDetails.Count();
                }
                objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                objUserDashBoard.GroupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture));
                if (type.Equals(Shared.Staff))
                {
                    //Check the impersination case and get the visitor userId
                    parentUserId = GetVisitorUserId(companyId);
                }
                if (!string.IsNullOrEmpty(parentUserId) && type.Equals(Shared.Staff))
                {
                    //get the company id of visitor using the user Id
                    parentCompanyId = Convert.ToString(objDecisionPointEngine.GetParentUserId(parentUserId, DecisionPointR.getcompanyid), CultureInfo.InvariantCulture);
                    if (Convert.ToString(HttpContext.Current.Session["IsSuperAdmin"], CultureInfo.InvariantCulture).Equals(Shared.Yes))
                    {
                        // called method from business layer for get only the visitor sent communication for display in visiting user profile
                        objUserDashBoard.HistoryDetails = objUserDashBoard.HistoryDetails.ToList();
                    }
                    else
                    {
                        // called method from business layer for get only the visitor sent communication for display in visiting user profile
                        objUserDashBoard.HistoryDetails = objUserDashBoard.HistoryDetails.ToList().Where(x => x.CreatorCompanyid == parentCompanyId);
                    }

                }
                return objUserDashBoard;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for get the communication details display on my communication View
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">type</param>
        /// <returns>UserDashBoard</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public UserDashBoard GetMyCommunicationDetails(string id, string type)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                //called method from business layer for get communication details of particular user
                objUserDashBoard.DocumentsDetails = objDecisionPointEngine.GetDocumentsDetails(userId, type, companyId, id);
                if (type.Equals(Shared.StaffCommunication))
                {
                    //Check the impersination case and get the visitor userId
                    parentUserId = GetVisitorUserId(companyId);
                }
                if (!string.IsNullOrEmpty(parentUserId) && type.Equals(Shared.StaffCommunication))
                {
                    //get the company id of visitor using the user Id
                    parentCompanyId = Convert.ToString(objDecisionPointEngine.GetParentUserId(parentUserId, DecisionPointR.getcompanyid), CultureInfo.InvariantCulture);

                    if (Convert.ToString(HttpContext.Current.Session["IsSuperAdmin"], CultureInfo.InvariantCulture).Equals(Shared.Yes))
                    {
                        // called method from business layer for get only the visitor sent communication for display in visiting user profile
                        objUserDashBoard.DocumentsDetails = objUserDashBoard.DocumentsDetails.ToList();
                    }
                    else
                    {
                        // called method from business layer for get only the visitor sent communication for display in visiting user profile
                        objUserDashBoard.DocumentsDetails = objUserDashBoard.DocumentsDetails.ToList().Where(x => x.CreatorCompanyid == parentCompanyId);
                    }

                }

                //called method from business layer for get the actives groups
                objUserDashBoard.GroupDetails = objDecisionPointEngine.GetGroup(Shared.User.ToLower(CultureInfo.InvariantCulture), Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture));
                objUserDashBoard.PageSize = objUserDashBoard.DocumentsDetails.Count();
                objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                return objUserDashBoard;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for Remove the Communication from Grid which showing on in Grid On view section
        /// </summary>
        /// <param name="docId">docId</param>
        /// <param name="type"><type/param>
        /// <returns>int</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public int RemovedCommunication(int docId, int type)
        {
            try
            {
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                objDecisionPointEngine = new DecisionPointEngine();
                //call methdor for remove the particular communcation
                if (objDecisionPointEngine.RemoveDocument(docId, type, companyId) > 0)
                    return 1;
                else
                    return 0;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for serach the communication from My library View
        /// </summary>
        /// <param name="term">term</param>
        /// <returns>List<string></returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public List<string> SerachInHistoryCommunications(string term)
        {
            List<string> serachByTitle = new List<string>();
            List<string> serachByBoth = new List<string>();
            try
            {
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                //call methdo for serach in communication on history view
                objUserDashBoard.Search = objDecisionPointEngine.Search(term, userId, Shared.history, 0).Distinct().ToList();
                serachByTitle = objUserDashBoard.Search.Select(t => t.serachByTitle.ToLowerInvariant()).ToList();
                //serachByFrom = objUserDashBoard.Search.Select(t => t.serachByFrom.ToLowerInvariant()).ToList();
                //merage the record
                serachByBoth = serachByTitle;//.Union(serachByFrom).ToList();
                return serachByBoth;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Profile Pages
        /// <summary>
        /// Used for Get the My profile details of particular login user dispaly on My profile View
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>UserDashBoard</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public UserDashBoard GetMyProfileDetails(string type)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                objUserDashBoard = new UserDashBoard();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                userType = Convert.ToString(HttpContext.Current.Session["UserType"], CultureInfo.InvariantCulture);
                //Called method for get My profile details of Individual and send it to view property for UI
                objUserDashBoardResponse = new UserDashBoardResponse();
                objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(userId);
                #region My Profile Details


                if (!objUserDashBoardResponse.Equals(null))
                {
                    objUserDashBoard.FName = objUserDashBoardResponse.fName;
                    objUserDashBoard.NickName = objUserDashBoardResponse.NickName;
                    objUserDashBoard.MName = objUserDashBoardResponse.mName;
                    objUserDashBoard.LName = objUserDashBoardResponse.lName;
                    objUserDashBoard.EmailId = objUserDashBoardResponse.emailId;
                    objUserDashBoard.OfficePhone = objUserDashBoardResponse.officePhone;
                    objUserDashBoard.DirectPhone = objUserDashBoardResponse.directPhone;
                    objUserDashBoard.UserId = objUserDashBoardResponse.UserId;
                    objUserDashBoard.CompanyId = objUserDashBoardResponse.CompanyId;
                    objUserDashBoard.CompanyName = objUserDashBoardResponse.companyName;
                    objUserDashBoard.RegisteredDate = objUserDashBoardResponse.RegisteredDate;
                    objUserDashBoard.StreetNumber = objUserDashBoardResponse.StreetNumber;
                    objUserDashBoard.StreetName = objUserDashBoardResponse.StreetName;
                    objUserDashBoard.Direction = objUserDashBoardResponse.Direction;
                    objUserDashBoard.CityName = objUserDashBoardResponse.CityName;
                    objUserDashBoard.StateName = objUserDashBoardResponse.StateName;
                    objUserDashBoard.StateId = objUserDashBoardResponse.StateId;
                    objUserDashBoard.ZipCode = objUserDashBoardResponse.ZipCode;
                    objUserDashBoard.BioInfo = objUserDashBoardResponse.BioInfo;
                    objUserDashBoard.ServicesStatus = objUserDashBoardResponse.ServicesStatus;
                    objUserDashBoard.TitleName = objUserDashBoardResponse.title;
                    objUserDashBoard.CertificationNumber = objUserDashBoardResponse.CertificationNumber;
                    objUserDashBoard.CertificateExpDate = objUserDashBoardResponse.CertificateExpDate;
                    objUserDashBoard.CertifyingAgency = objUserDashBoardResponse.CertifyingAgency;
                    objUserDashBoard.BusinessClass = objUserDashBoardResponse.BusinessClass;
                    if (!string.IsNullOrEmpty(objUserDashBoardResponse.profilephoto))
                    {
                        objUserDashBoard.File = GetSiteRoot() + ConfigurationManager.AppSettings["profilepic"] + objUserDashBoardResponse.profilephoto;
                    }
                    else
                    {
                        objUserDashBoard.File = GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["profilepic"], CultureInfo.InvariantCulture) + Convert.ToString(ConfigurationManager.AppSettings["DefaultMyProflePicName"]);
                    }
                }
                #endregion
                //Particular user

                #region Title, State, Client And Service Details

                objUserDashBoard.TitleDetails = objDecisionPointEngine.GetUserTitle(userId, companyId);
                if (type.Equals(Shared.Staff))
                {
                    objUserDashBoard.ServiceDetails = objDecisionPointEngine.GetUserServices(userId, companyId);
                    objUserDashBoard.ClientDetails = objDecisionPointEngine.GetUserClients(userId, companyId);
                    //All data
                    objUserDashBoard.ServiceList = objDecisionPointEngine.GetServices(Shared.User.ToLower(), companyId);
                    objUserDashBoard.Clientlist = objDecisionPointEngine.GetClient(Shared.User.ToLower(), companyId);
                }
                else
                {
                    //get service data of company
                    if (userType.Equals(Shared.Company) || userType.Equals(Shared.IC) || userType.Equals(Shared.SuperAdmin))
                    //if (Convert.ToString(HttpContext.Current.Session["UserType"], CultureInfo.InvariantCulture).Equals("IC"))
                    {
                        objUserDashBoard.ClientDetails = objDecisionPointEngine.GetClient(Shared.Company.ToLower(), companyId).Select(x => x.clientName).ToList();
                        objUserDashBoard.ServiceDetails = objDecisionPointEngine.GetServices(Shared.Company.ToLower(), companyId).Select(x => x.serviceName).ToList();
                    }
                    else
                    {
                        objUserDashBoard.ClientDetails = objDecisionPointEngine.GetUserClients(userId, companyId);
                        objUserDashBoard.ServiceDetails = objDecisionPointEngine.GetUserServices(userId, companyId);
                    }
                    //All data
                    objUserDashBoard.ServiceList = objDecisionPointEngine.GetServices(Shared.Company.ToLower(), companyId);
                    objUserDashBoard.Clientlist = objDecisionPointEngine.GetClient(Shared.Company.ToLower(), companyId);
                }
                objUserDashBoard.StateList = objDecisionPointEngine.GetStateList();
                #endregion
                #region IC EditVisibility Client list
                //get IC client permission : Is IC allow to his/her for view backgroundcheck/LicenseAndInsuranceDetails
                ICClientPermissionRequest objICClientPermissionRequest = new ICClientPermissionRequest()
                {
                    ICCompanyId = companyId,
                    ICUserId = userId
                };
                icPerClientList = objDecisionPointEngine.GetICClientPermissions(objICClientPermissionRequest);
                if (!object.Equals(icPerClientList, null))
                {
                    if (icPerClientList.Count > 0)
                    {
                        icClientsIds = icPerClientList.Select(x => x.VisibleTo).Distinct().FirstOrDefault();
                        if (string.IsNullOrEmpty(icClientsIds))
                        {
                            objUserDashBoard.IsEditVisibilityDone = false;
                        }
                        else
                        {
                            objUserDashBoard.IsEditVisibilityDone = true;
                        }
                    }
                    else { objUserDashBoard.IsEditVisibilityDone = false; }
                }
                else { objUserDashBoard.IsEditVisibilityDone = false; }
                #endregion

                #region If for Impersonation And Else for Direct Login
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["parentUserid"], CultureInfo.InvariantCulture)))
                {
                    if (type.Equals(Shared.Staff) || type.Equals(Shared.IC) || type.Equals(Shared.Company))
                    {
                        //Check the impersination case and get the visitor userId
                        parentUserId = GetVisitorUserId(companyId);
                        //get the company id of visitor using the user Id
                        parentCompanyId = Convert.ToString(objDecisionPointEngine.GetParentUserId(parentUserId, Shared.GetCompanyid), CultureInfo.InvariantCulture);
                        objUserDashBoard.ParentCompanyName = objDecisionPointEngine.GetUserNameFromUserId(parentUserId);
                        objUserDashBoard.ParentUserId = parentUserId;
                        parentUserType = objDecisionPointEngine.GetParentUserType(Convert.ToInt32(parentUserId));
                    }
                }

                if (!string.IsNullOrEmpty(parentUserId) && (type.Equals(Shared.Staff) || type.Equals(Shared.IC)) && (!parentUserType.Equals(Shared.NonClient)) && !Convert.ToString(HttpContext.Current.Session["IsSuperAdmin"]).Equals(Shared.Yes))//case of impersonation
                {


                    objUserDashBoard.ReqiuredDocumentsDetails = objDecisionPointEngine.GetReqiuredDocuments(userId, companyId).Where(x => x.CreatorCompanyid == parentCompanyId);
                    //check user type and get config details as per user type
                    #region Ic Type Details


                    if (type.Equals(Shared.IC))
                    {
                        objUserDashBoard.CurrentClientList = objDecisionPointEngine.GetICClientList(userId, true).ToList();
                        //get Ic Client List
                        icCompamiesList = objUserDashBoard.CurrentClientList.Where(a => a.CompanyId == parentCompanyId).Select(x => x.CompanyId).ToList();
                        //get confis=g setting as per client list of that IC
                        objConfiguratonSettingRequest.ConfigSettingCollection = objDecisionPointEngine.GetConfigSettingForIC(icCompamiesList);
                        //get vendor type of IC as per impersination of entity
                        objUserDashBoard.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => x.IsUserBased == true).Where(x => x.CreatorCompanyId == parentCompanyId);
                        if (!object.Equals(objConfiguratonSettingRequest.ConfigSettingCollection, null))
                        {
                            if (objConfiguratonSettingRequest.ConfigSettingCollection.Count > 0)
                            {
                                objUserDashBoard.IsBgCheckApply = objConfiguratonSettingRequest.ConfigSettingCollection[0].IsBgCheckForIC;
                                objUserDashBoard.IsCoverageAreaApply = objConfiguratonSettingRequest.ConfigSettingCollection[0].IsCoverageAreaForIC;
                                objUserDashBoard.IsAddCreApply = objConfiguratonSettingRequest.ConfigSettingCollection[0].IsAddCreForIC;
                                objUserDashBoard.IsLicInspply = objConfiguratonSettingRequest.ConfigSettingCollection[0].IsLiceInsForIC;
                                objUserDashBoard.IsServicesApply = objConfiguratonSettingRequest.ConfigSettingCollection[0].IsServicesForIC;
                            }
                        }

                        if (!object.Equals(icPerClientList, null))
                        {
                            if (icPerClientList.Count > 0)
                            {
                                icClientsIds = icPerClientList.Select(x => x.VisibleTo).Distinct().FirstOrDefault();
                                if ((string.IsNullOrEmpty(icClientsIds)) || (!string.Equals(icClientsIds, Shared.All) && !icClientsIds.Split(char.Parse(Shared.Comma)).Contains(parentCompanyId)))
                                {
                                    objUserDashBoard.IsBgCheckApply = false;
                                    objUserDashBoard.IsLicInspply = false;
                                }
                            }
                        }


                    }

                    #endregion

                    #region Background Package Details
                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = parentCompanyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                    if (Convert.ToString(HttpContext.Current.Session["IsSuperAdmin"], CultureInfo.InvariantCulture) == Shared.Yes)
                    {
                        objBackGroundCheckMasterRequest.OperationType = 1;
                        objUserDashBoard.ProfessionalLicense = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).ToList();//.Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalInsurance = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).ToList();//.Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.AdditionalRequirements = objDecisionPointEngine.GetAdditionalRequirement(userId, companyId).ToList();//.Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalCommunications = objDecisionPointEngine.GetProfessionalCommunication(userId, companyId).ToList();
                    }
                    else
                    {
                        objUserDashBoard.ProfessionalLicense = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalInsurance = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.AdditionalRequirements = objDecisionPointEngine.GetAdditionalRequirement(userId, companyId).Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).Contains((int)(x.VendorTypeId == null ? 0 : x.VendorTypeId)) && x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalCommunications = objDecisionPointEngine.GetProfessionalCommunication(userId, companyId).Where(x => x.CreatorCompanyid == parentCompanyId).ToList();
                    }
                    objUserDashBoard.BackgroundList = objDecisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();

                    #endregion
                }

                else
                {
                    //check user type and get config details as per user type
                    if (type.Equals(Shared.IC))
                    {
                        List<int> vendorTypeCol = null;
                        objUserDashBoard.CurrentClientList = objDecisionPointEngine.GetICClientList(userId, true).ToList();
                        //get Ic Client List
                        icCompamiesList = objUserDashBoard.CurrentClientList.Select(x => x.CompanyId).ToList();

                        //get confis=g setting as per client list of that IC
                        objConfiguratonSettingRequest.ConfigSettingCollection = objDecisionPointEngine.GetConfigSettingForIC(icCompamiesList);
                        //get vendor type of IC as per client list of that IC
                        objUserDashBoard.CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => icCompamiesList.Contains(x.CreatorCompanyId) && x.IsUserBased == true);
                        if (!object.Equals(objConfiguratonSettingRequest.ConfigSettingCollection, null))
                        {
                            if (objConfiguratonSettingRequest.ConfigSettingCollection.Count > 0)
                            {
                                //get vendor type details as per ic company list which already set the config setting
                                vendorTypeCol = objUserDashBoard.CompanyVendorTypeDetails.Where(a => objConfiguratonSettingRequest.ConfigSettingCollection.Where(b => b.IsLiceInsForIC == true).Select(c => c.CompanyId).Contains(a.CreatorCompanyId)).Select(d => d.VendorTypeId).ToList();
                                if (objConfiguratonSettingRequest.ConfigSettingCollection.Where(x => x.IsServicesForIC == true).Count() <= 0)
                                {
                                    objUserDashBoard.IsServicesApply = false;
                                }
                                if (objConfiguratonSettingRequest.ConfigSettingCollection.Where(x => x.IsLiceInsForIC == true).Count() <= 0)
                                {
                                    objUserDashBoard.IsLicInspply = false;
                                }
                                if (objConfiguratonSettingRequest.ConfigSettingCollection.Where(x => x.IsCoverageAreaForIC == true).Count() <= 0)
                                {
                                    objUserDashBoard.IsCoverageAreaApply = false;
                                }
                                if (objConfiguratonSettingRequest.ConfigSettingCollection.Where(x => x.IsBgCheckForIC == true).Count() <= 0)
                                {
                                    objUserDashBoard.IsBgCheckApply = false;
                                }
                                if (objConfiguratonSettingRequest.ConfigSettingCollection.Where(x => x.IsAddCreForIC == true).Count() <= 0)
                                {
                                    objUserDashBoard.IsAddCreApply = false;
                                }
                            }
                            else
                            {
                                //get vendor type of ic if there no any config setting for ic
                                vendorTypeCol = objUserDashBoard.CompanyVendorTypeDetails.Select(x => x.VendorTypeId).ToList();
                            }
                        }
                        else
                        {
                            //get vendor type of ic if there no any config setting for ic
                            vendorTypeCol = objUserDashBoard.CompanyVendorTypeDetails.Select(x => x.VendorTypeId).ToList();
                        }
                        // objUserDashBoard.LicAndInsAsDetails = objDecisionPointEngine.GetLicAndInsAsPerVendorType(userId, companyId, vendorTypeCol);
                        objUserDashBoard.ProfessionalLicense = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).Where(x => x.CompanyId == companyId).ToList();
                        objUserDashBoard.ProfessionalInsurance = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).Where(x => x.CompanyId == companyId).ToList();
                        //objUserDashBoard.AdditionalRequirements = objDecisionPointEngine.GetAdditionalRequirement(userId, companyId).ToList();
                        //objUserDashBoard.ProfessionalCommunications = objDecisionPointEngine.GetProfessionalCommunication(userId, companyId).ToList();
                        objUserDashBoard.ReqiuredDocumentsDetails = objDecisionPointEngine.GetReqiuredDocuments(userId, companyId).Where(x => icCompamiesList.Contains(x.CreatorCompanyid));
                        #region Background Package Details
                        objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = companyId, UserId = userId, CompanyId = companyId, OperationType = 0 };

                        objUserDashBoard.BackgroundList = objDecisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();

                        #endregion
                    }
                    else
                    {
                        objUserDashBoard.ReqiuredDocumentsDetails = objDecisionPointEngine.GetReqiuredDocuments(userId, companyId);
                    }

                }
                #endregion
                //Called method for get My Profile details of Individual and send it to view property for UI                    
                #region Uploaded Doc Paths


                objUserDashBoard.Globauploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objUserDashBoard.Specificuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objUserDashBoard.ProfLicuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objUserDashBoard.Insuranceuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                //objUserDashBoard.ElecLicAndnsuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                //objUserDashBoard.NonElecLicAndInsuploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                //objUserDashBoard.BackgroundCheckDocPath = Convert.ToString(ConfigurationManager.AppSettings["BackgroundCheckuploadeddocpath"], CultureInfo.InvariantCulture).Split('~')[1] + userId + Shared.UnderScore + parentUserId;
                objUserDashBoard.AdditionalRequploadedcontentpath = Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture).Split('~')[1] + userId;
                objUserDashBoard.HostURL = GetSiteRoot();
                //set req type for return IC p[rofile or user profile from update profile action as per condition
                objUserDashBoard.ReqType = 0;
                #endregion


                //check user type and get config details as per user type
                if (!type.Equals(Shared.IC))
                {
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        objUserDashBoard.IsClientApply = objConfiguratonSettingRequest.IsClient;
                        objUserDashBoard.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoverageAreaForStaff;
                        objUserDashBoard.IsBgCheckApply = objConfiguratonSettingRequest.IsBgCheckForStaff;
                        objUserDashBoard.IsAddCreApply = objConfiguratonSettingRequest.IsAddCreForStaff;
                        objUserDashBoard.IsServicesApply = objConfiguratonSettingRequest.IsServicesForStaff;
                    }
                }


                //Get the services and coverage area alert
                #region Service and Coverage Area Alerts


                objMyDashBoardRequest = new MyDashBoardRequest()
                    {
                        CompanyId = companyId,
                        UserId = userId,
                        UserType = userType,
                        SelectedDate = Convert.ToDateTime(DateTime.Now.Date)
                    };
                objMyDashBoardResponse = new MyDashBoardResponse();
                objMyDashBoardResponse = objDecisionPointEngine.GetLoginUserAlerts(objMyDashBoardRequest);
                objUserDashBoard.CoverageAreaCount = objMyDashBoardResponse.ProfileDetailsAlerts.Select(x => x.CoverageAreadetail).FirstOrDefault();
                objUserDashBoard.ServiceCount = objMyDashBoardResponse.ProfileDetailsAlerts.Select(x => x.Serviecdetail).FirstOrDefault();

                //get coverage condition for staff
                if (userType.Equals(Shared.Individual) || userType.Equals(Shared.Company) || userType.Equals(Shared.IC))
                {
                    string coverageAreaStatus = objDecisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                    if (!string.IsNullOrEmpty(coverageAreaStatus))
                    {
                        if (coverageAreaStatus.Trim().ToLower().Equals(Shared.DNA.Trim().ToLower()))
                        {

                            if (userType.Equals(Shared.IC))
                            {
                                objUserDashBoard.CoverageAreaCount = 1;
                            }
                            else { objUserDashBoard.IsCoverageAreaApply = false; }
                        }
                        else if (coverageAreaStatus.Trim().ToLower().Equals(Shared.All.Trim().ToLower()))
                        {
                            if (userType.Equals(Shared.IC))
                            {
                                objUserDashBoard.CoverageAreaCount = 1;
                            }
                        }
                    }
                }
                #endregion

                #region Get Client And Prospective Client List
                if (type.Equals(Shared.IC))
                {
                    objUserDashBoard.ICProspectiveClientList = objDecisionPointEngine.ICProspectiveClients().Where(x => objUserDashBoard.CompanyVendorTypeDetails.Select(y => y.VendorTypeId).ToList().Contains(x.VendorTypeId)).ToList();
                    objUserDashBoard.ICPerProspectiveClientList = objUserDashBoard.ICProspectiveClientList.Where(x => icClientsIds.Contains(x.CompanyId)).ToList();
                    objVendorsList = new VendorsList();
                    IList<VendorsList> objVendorList = new List<VendorsList>();
                    //get the background check status for prspective clients
                    foreach (var item in objUserDashBoard.ICPerProspectiveClientList)
                    {
                        objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = item.CompanyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                        int ProfessionalLicenseCount = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).Where(x => x.CompanyId == item.CompanyId && (x.Status == "Fail" || x.Status == "Pending")).Count();
                        int ProfessionalInsuranceCount = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).Where(x => x.CompanyId == item.CompanyId && (x.Status == "Fail" || x.Status == "Pending")).Count();
                        int BackgroundListCount = objDecisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).Where(x => (x.Status == "Pending" || x.Status == "In Progress" || x.Status == "Review")).Count();

                        if (ProfessionalLicenseCount == 0 && ProfessionalInsuranceCount == 0 && BackgroundListCount == 0)
                        {
                            objVendorList.Add(AddCurrentClientListWithStatus(item, Shared.Fail));
                        }
                        else if (ProfessionalLicenseCount > 0 || ProfessionalInsuranceCount > 0 || BackgroundListCount > 0)
                        {
                            objVendorList.Add(AddCurrentClientListWithStatus(item, Shared.Fail));
                        }
                        else
                        {
                            objVendorList.Add(AddCurrentClientListWithStatus(item, Shared.Pass));
                        }
                    }
                    objUserDashBoard.ICPerProspectiveClientList = objVendorList.ToList();
                }
                #endregion
                return objUserDashBoard;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for Add current Client list with status
        /// </summary>
        /// <param name="objFinalVendorsList"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private VendorsList AddCurrentClientListWithStatus(VendorsList objFinalVendorsList, string status)
        {
            try
            {
                objFinalVendorsList = new VendorsList()
                {
                    vendor = objFinalVendorsList.vendor,
                    contact = objFinalVendorsList.contact,
                    emailId = objFinalVendorsList.emailId,
                    phone = objFinalVendorsList.phone,
                    Id = objFinalVendorsList.Id,
                    CompanyId = objFinalVendorsList.CompanyId,
                    Status = status
                };
                return objFinalVendorsList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Used for dispaly the detail of company profile on popup section which dispaly when user view the communication sender details
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>UserDashBoard</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public UserDashBoard ViewCompanyProfile(int UserId)
        {
            try
            {
                objUserDashBoard = new UserDashBoard();
                objUserDashBoardResponse = new UserDashBoardResponse();
                objDecisionPointEngine = new DecisionPointEngine();
                //used for get the particular user details
                objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(UserId);
                if (!objUserDashBoardResponse.Equals(null))
                {
                    objUserDashBoard.EmailId = objUserDashBoardResponse.emailId;
                    objUserDashBoard.CompanyName = objUserDashBoardResponse.companyName;
                    objUserDashBoard.OfficePhone = objUserDashBoardResponse.officePhone;
                    objUserDashBoard.DirectPhone = objUserDashBoardResponse.directPhone;
                    objUserDashBoard.FName = objUserDashBoardResponse.fName;
                    objUserDashBoard.MName = objUserDashBoardResponse.mName;
                    objUserDashBoard.LName = objUserDashBoardResponse.lName;
                }
                return objUserDashBoard;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for update the my profile details
        /// </summary>
        /// <param name="objUserDashBoard">objUserDashBoard</param>
        /// <param name="file">file</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public void UpdateMyProfile(UserDashBoard objUserDashBoard, HttpPostedFileBase file)
        {
            string newPath = string.Empty;
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                //Update My profile
                objUserDashBoardRequest = new UserDashBoardRequest()
                {
                    FName = (string.IsNullOrEmpty(objUserDashBoard.FName)) ? string.Empty : objUserDashBoard.FName.Trim(),
                    MName = (string.IsNullOrEmpty(objUserDashBoard.MName)) ? string.Empty : objUserDashBoard.MName.Trim(),
                    LName = (string.IsNullOrEmpty(objUserDashBoard.LName)) ? string.Empty : objUserDashBoard.LName.Trim(),
                    NickName = (string.IsNullOrEmpty(objUserDashBoard.NickName)) ? string.Empty : objUserDashBoard.NickName.Trim(),
                    EmailId = objUserDashBoard.EmailId.Trim(),
                    UserId = userId,
                    ModifiedBy = Convert.ToInt32(HttpContext.Current.Session["superAdmin"], CultureInfo.InvariantCulture),
                    OfficePhone = string.IsNullOrEmpty(objUserDashBoard.OfficePhone) ? string.Empty : objUserDashBoard.OfficePhone.Trim().Replace(Shared.RightParanThesis, "").Replace(Shared.LeftParanThesis, "").Replace(Shared.SingleDash, "").Replace(Shared.SingleSpace, "").Trim(),
                    DirectPhone = (string.IsNullOrEmpty(objUserDashBoard.DirectPhone)) ? string.Empty : objUserDashBoard.DirectPhone.Trim().Replace(Shared.RightParanThesis, "").Replace(Shared.LeftParanThesis, "").Replace(Shared.SingleDash, "").Replace(Shared.SingleSpace, "").Trim(),
                    StreetName = objUserDashBoard.StreetName,
                    StreetNumber = objUserDashBoard.StreetNumber,
                    Direction = objUserDashBoard.Direction,
                    CityName = objUserDashBoard.CityName,
                    ZipCode = objUserDashBoard.ZipCode,
                    CerificationNumber = objUserDashBoard.CertificationNumber,
                    CertificateExpDate = objUserDashBoard.CertificateExpDate,
                    CertifyingAgency = objUserDashBoard.CertifyingAgency,
                    BusinessClass = objUserDashBoard.BusinessClass,

                };
                if (!objUserDashBoard.StateId.Equals(0))
                {
                    objUserDashBoardRequest.StateId = objUserDashBoard.StateId;
                }
                //save person photo after rename
                if (file != null && file.ContentLength > 0)
                {
                    // fileName = Path.GetFileName(file.FileName);
                    string fileExt = Path.GetExtension(file.FileName);
                    string fileWithId = userId + Shared.UnderScore + BusinessCore.GenrateRandomnumber() + fileExt;
                    // string FileWithId = UserId + "_" + fileName;
                    newPath = Path.Combine(HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["uploadprofilepic"], CultureInfo.InvariantCulture)), fileWithId);
                    string folderPath = HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["uploadprofilepic"], CultureInfo.InvariantCulture));
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string filename in files)
                    {
                        if (Path.GetFileName(filename).Split(char.Parse(Shared.UnderScore))[0].Equals(Convert.ToString(userId, CultureInfo.InvariantCulture)))
                        {
                            System.IO.File.Delete(filename);
                        }
                    }
                    file.SaveAs(newPath);
                    objUserDashBoardRequest.ProfilePhoto = fileWithId;
                    //Resize the image
                    Image imgOriginal = Image.FromFile(newPath);
                    BusinessCore.GetThumbnailImage(imgOriginal, newPath);

                }

                //Update my profile details  
                objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, Shared.Profile);
                if (!objUserDashBoard.EmailId.Equals(HttpContext.Current.Session["EmailId"]))
                {
                    objDecisionPointEngine.UpdateEmailId(objUserDashBoard.EmailId, userId);
                }
                HttpContext.Current.Session["Emailid"] = objUserDashBoardRequest.EmailId;
                HttpContext.Current.Session["UserName"] = objUserDashBoardRequest.FName + Shared.SingleSpace + objUserDashBoardRequest.LName;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region My DashBoard ALerts
        /// <summary>
        /// Used for Get the alerts for display on My dashboaed View
        /// </summary>
        /// <param name="date">date</param>
        /// <returns>MyDashBoard</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public MyDashBoard GetMyDashBoardAlerts(string date)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                userType = Convert.ToString(HttpContext.Current.Session["UserType"], CultureInfo.InvariantCulture);
                objMyDashBoardResponse = new MyDashBoardResponse();
                objMyDashBoard = new MyDashBoard();
                if (userType.Equals(Shared.IC))
                {
                    List<int> vendorTypeCol = null;
                    //get Ic Client List
                    icCompamiesList = objDecisionPointEngine.GetICClientList(userId, true).Select(x => x.CompanyId).ToList();
                    //get confis=g setting as per client list of that IC
                    objConfiguratonSettingRequest.ConfigSettingCollection = objDecisionPointEngine.GetConfigSettingForIC(icCompamiesList);
                    //get vendor type of IC as per client list of that IC
                    IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails = objDecisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).Where(x => icCompamiesList.Contains(x.CreatorCompanyId) && x.IsUserBased == true);
                    if (!object.Equals(objConfiguratonSettingRequest.ConfigSettingCollection, null))
                    {
                        vendorTypeCol = CompanyVendorTypeDetails.Where(a => objConfiguratonSettingRequest.ConfigSettingCollection.Where(b => b.IsLiceInsForIC == true).Select(c => c.CompanyId).Contains(a.CreatorCompanyId)).Select(d => d.VendorTypeId).ToList();
                    }
                    else
                    {
                        vendorTypeCol = CompanyVendorTypeDetails.Select(x => x.VendorTypeId).ToList();
                    }
                    objMyDashBoard.LicAndInsAsDetails = objDecisionPointEngine.GetLicAndInsAsPerVendorType(userId, companyId, vendorTypeCol).ToList();
                }
                objMyDashBoardRequest = new MyDashBoardRequest()
                {
                    CompanyId = companyId,
                    UserId = userId,
                    UserType = userType,
                    SelectedDate = Convert.ToDateTime(date)
                };
                //get the alerts details for particular login users
                objMyDashBoardResponse = objDecisionPointEngine.GetLoginUserAlerts(objMyDashBoardRequest);
                if (!object.Equals(objMyDashBoardResponse, null))
                {
                    objMyDashBoard.IncomFromCompCommAlerts = objMyDashBoardResponse.IncomFromCompCommAlerts;
                    objMyDashBoard.IncomFromOutCompCommAlerts = objMyDashBoardResponse.IncomFromOutCompCommAlerts;
                    objMyDashBoard.CompanyBasedCommAlerts = objMyDashBoardResponse.CompanyBasedCommAlerts;
                    objMyDashBoard.ProfileDetailsAlerts = objMyDashBoardResponse.ProfileDetailsAlerts;
                    objMyDashBoard.JCRDetailsAlerts = objMyDashBoardResponse.JCRDetailsAlerts;
                    objMyDashBoard.ContractsAlerts = objMyDashBoardResponse.ContractsAlerts;
                    objMyDashBoard.IncomInviteAlerts = objMyDashBoardResponse.IncomInviteAlerts;
                    objMyDashBoard.OutgoICVendorInviteAlerts = objMyDashBoardResponse.OutgoICVendorInviteAlerts;
                    objMyDashBoard.OutgoStaffInviteAlerts = objMyDashBoardResponse.OutgoStaffInviteAlerts;
                }
                //get the config setting as per company id of particular login user
                //objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);

                //if (!object.Equals(objConfiguratonSettingRequest, null))
                //{
                //    objMyDashBoard.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoveragearea;
                //    objMyDashBoard.IsICApply = objConfiguratonSettingRequest.IsIc;
                //    objMyDashBoard.IsVendorApply = objConfiguratonSettingRequest.IsVendor;
                //    objMyDashBoard.IsServiceApply = objConfiguratonSettingRequest.IsServices;
                //}
                objMyDashBoard.SelectedDate = Convert.ToDateTime(date);
                return objMyDashBoard;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Guide
        /// <summary>
        /// Used for get the Guide instruction details publish by super admin
        /// </summary>
        /// <param name="isActive">isActive</param>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public string GetGuidInstruction(bool isActive)
        {
            string objinst = null;
            IEnumerable<GuideInstructionResponse> objGuideInst = null;
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                //get the publish guide details
                objGuideInst = objDecisionPointEngine.GetGuideInstruction().ToList();
                if (objGuideInst != null)
                {
                    if (objGuideInst.Count().Equals(1))
                    {
                        objinst = objDecisionPointEngine.GetGuideInstruction().ToList()[0].Instruction;
                    }
                    else
                    {
                        objinst = objDecisionPointEngine.GetGuideInstruction().Where(x => x.IsActive == isActive).ToList()[0].Instruction;
                    }

                }
                else
                {
                    objinst = string.Empty;
                }
                return objinst;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region VisitorUserId
        /// <summary>
        /// Used for User Id of user which impersinate the other users
        /// </summary>
        /// <param name="companyId">companyId</param>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Aug 30, 2014</CreatedDate>
        public string GetVisitorUserId(string companyId)
        {

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["parentUserid"], CultureInfo.InvariantCulture)))
                {
                    sessionList = new List<string>();
                    //check impersination case
                    if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["parentUserid"], CultureInfo.InvariantCulture)))
                    {
                        sessionList = Convert.ToString(HttpContext.Current.Session["parentUserid"]).Split(',').ToList();
                    }
                    if (sessionList != null)
                    {
                        if (sessionList.Count > 0)
                        {
                            if (sessionList.Count >= 2)
                            {
                                //set the parent user Id
                                parentUserId = sessionList[sessionList.Count - 2];
                            }
                        }
                    }

                }
                return parentUserId;
            }
            catch
            {
                throw;
            }

        }
        #endregion

        #region Root Url
        /// <summary>
        /// get root path of site
        /// </summary>
        /// <createdby>bobis</createdby>
        ///<createddate>apr 4 2014</createddate>
        /// <returns>string type site root</returns>
        public static string GetSiteRoot()
        {
            try
            {

                //get the root path of requested URL
                string port = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                if (port == null || port == "80" || port == "443")
                    port = "";
                else
                    port = ":" + port;

                string protocol = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (protocol == null || protocol == "0")
                    protocol = "http://";
                else
                    protocol = "https://";

                string sOut = protocol + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + port + System.Web.HttpContext.Current.Request.ApplicationPath;

                if (sOut.EndsWith("/"))
                {
                    sOut = sOut.Substring(0, sOut.Length - 1);
                }
                //return the ROOT path of reuested URL
                return sOut;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Invitation

        /// <summary>
        /// Used for Sent the invitation to users for join compliance tracker
        /// </summary>
        /// <param name="objSendInvitation">objSendInvitation</param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <CreatedDate>24 Sep 2014</CreatedDate>
        public int SendInviteToUsers(List<SendInvitation> listSendInvitation)
        {


            try
            {
                //SendInvitation objSendInvitation = new SendInvitation();
                string id = string.Empty;
                userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                businessName = Convert.ToString(HttpContext.Current.Session["BusinessName"], CultureInfo.InvariantCulture);

                objUserDashBoardResponse = new UserDashBoardResponse();
                objDecisionPointEngine = new DecisionPointEngine();
                objbusinessCryptography = new BusinessCryptography();

                foreach (SendInvitation objSendInvitation in listSendInvitation)
                {
                    objCompanyDashBoardRequest = new CompanyDashBoardRequest()
                    {
                        fName = objSendInvitation.firstName,
                        lName = objSendInvitation.lastName,
                        titleId = objSendInvitation.titleId,
                        permissionId = objSendInvitation.permissionId,
                        emailId = objSendInvitation.emailId,
                        UserId = userId,
                        companyId = objSendInvitation.companyId,
                        CompId = companyId,
                        companyName = objSendInvitation.companyName,
                        flowId = objSendInvitation.flowId,
                        docflowId = objSendInvitation.docflowId,
                        IsBackgroundCheck = objSendInvitation.IsBackgroundCheck,
                        vendorTypeId = objSendInvitation.vendorTypeId,
                        PaymentType = Convert.ToInt32(objSendInvitation.PaymentType),
                        password = BusinessCore.CreatePassword(),
                        IsMailSent = objSendInvitation.IsMailSent,
                        AllowToView = objSendInvitation.AllowToView,
                    };


                    //call method for save the record in database of invited users
                    id = objDecisionPointEngine.SendInvitation(objCompanyDashBoardRequest, objSendInvitation.type);
                    //check mail is to be sent or will be send by service.
                    if (objSendInvitation.IsMailSent)
                    {
                        #region Mail Sending Code


                        string[] split = id.Split(char.Parse(Shared.Comma));
                        id = split[0];
                        string password = string.Empty;
                        string Isexist = string.Empty;
                        if (split.Count() > 1)
                        {
                            password = split[1];
                            if (split.Count() > 2)
                            {
                                Isexist = split[2];
                                if (split.Count() > 3)
                                {
                                    objCompanyDashBoardRequest.emailId = split[3];
                                }
                            }
                        }
                        else
                        {
                            password = objCompanyDashBoardRequest.password;
                        }


                        objCompanyDashBoardRequest.password = password;
                        //decode password
                        password = objbusinessCryptography.base64Decode2(password);
                        string email = objCompanyDashBoardRequest.emailId;
                        string signature = string.Empty;
                        string subject = string.Empty;

                        //get the signature of invitee company
                        signature = objDecisionPointEngine.GetSignature(userId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }

                        string text = string.Empty;
                        string userType = string.Empty;
                        if (string.IsNullOrEmpty(Isexist) || (objSendInvitation.type.Trim().ToLower().Equals(Shared.NonClient.Trim().ToLower())))
                        {
                            //get companyId for set the last invitation date if user does not exist in current company with active state
                            int receiverUserId = 0;
                            if (!string.IsNullOrEmpty(id))
                            {
                                if (id.Contains(Shared.Astrik))
                                {
                                    receiverUserId = Convert.ToInt32(id.Split(char.Parse(Shared.Astrik))[1]);
                                }
                            }
                            objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(userId);

                            if (objSendInvitation.type.Trim().ToLower().Equals(Shared.IcWithId.Trim().ToLower()) || objSendInvitation.type.Trim().ToLower().Equals(Shared.IcWithoutId.Trim().ToLower()))
                            {
                                subject = "Compliance Tracker Account";
                                userType = Shared.IC;
                                //text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + " " + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + businessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + userId + "'>click here</a> to get to log in page</br><br/>User Name: " + objCompanyDashBoardRequest.emailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                            }
                            else
                            {
                                if (objSendInvitation.type.Trim().ToLower().Equals(Shared.Staff.Trim().ToLower()))
                                {
                                    subject = "Compliance Tracker Account";
                                }
                                else
                                {
                                    subject = "Compliance Tracker";
                                }
                                userType = Shared.Staff;
                                //text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + " " + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker<br/><br/>" + businessName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + businessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + userId + "'>click here</a> to get to log in page</br><br/>User Name: " + objCompanyDashBoardRequest.emailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                            }
                            //else if (objSendInvitation.type.Trim().ToLower().Equals(Shared.NonClient.Trim().ToLower()))
                            //{
                            //    subject = "Compliance Tracker";
                            //    text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker<br/><br/>Dear " + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + "<br/>" + objSendInvitation.companyName + " from " + businessName + " has allowed you access to their profile to consider assigning them work.  Your access is included at the bottom of the e-mail along with the link to access the log in.<br/><br/>Thank you for using Compliance Tracker.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + userId + "'>click here</a> to get to log in page</br><br/>User Name: " + objCompanyDashBoardRequest.emailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                            //}
                            objMailInviteFormat = new MailInviteFormat()
                            {
                                PersonName = objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName,
                                PersonCompanyName = objSendInvitation.companyName,
                                PersonUserType = userType,
                                Passwrod = password,
                                Signature = signature,
                                DomainUrl = ViewModel.GetSiteRoot(),
                                EmailId = objCompanyDashBoardRequest.emailId,
                                InviteeUserId = Convert.ToString(userId, CultureInfo.InvariantCulture),
                                FilePath = HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture)),
                                InviteePersonName = objUserDashBoardResponse.fName + Shared.SingleSpace + objUserDashBoardResponse.lName,
                                InviteeCompanyName = objUserDashBoardResponse.companyName
                            };

                            text = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                            objDecisionPointEngine.GetSmtpDetail(email, text, subject);
                            //set the last invite date in mapping table
                            objDecisionPointEngine.UpdateLastInvite(companyId, receiverUserId, objSendInvitation.type);
                        }
                        #endregion
                    }
                    else
                    {
                        return 1;
                    }
                    #region Commented Code

                    //check user if already exist or not
                    //if (string.IsNullOrEmpty(Isexist))
                    //{
                    //    if (objSendInvitation.type.Trim().ToLower().Equals(Shared.Staff.Trim().ToLower()) || objSendInvitation.type.Trim().ToLower().Equals(Shared.IcWithId.Trim().ToLower()) || objSendInvitation.type.Trim().ToLower().Equals(Shared.IcWithoutId.Trim().ToLower()))
                    //    {
                    //        subject = DecisionPointR.InviteMailSubjectForStaffAndIC;
                    //        // text = DecisionPointR.InviteMailBodyMsg1 + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + DecisionPointR.InviteMailBodyMsg2 + businessName + DecisionPointR.InviteMailBodyForSAndICMsg1 + businessName + DecisionPointR.InviteMailBodyForSAndICMsg2 + ViewModel.GetSiteRoot() + DecisionPointR.InviteMailBodyMsg6 + userId + objCompanyDashBoardRequest.emailId
                    //        //+ DecisionPointR.InviteMailBodyMsg3 + password + DecisionPointR.InviteMailBodyMsg4 + signature + DecisionPointR.InviteMailBodyMsg5;
                    //        text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + businessName +
                    //            " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures," +
                    //            "directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile." +
                    //            "If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href=" + ViewModel.GetSiteRoot() + "?id=" + userId + "'>click here</a> to get to log in page</br><br/>User Name: " + objCompanyDashBoardRequest.emailId +
                    //            "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    //    }
                    //    else if (objSendInvitation.type.Trim().ToLower().Equals(Shared.VendorWithId.Trim().ToLower()))
                    //    {

                    //        IList<AdminProfileResponse> profileDetails = objDecisionPointEngine.GetAdminProfile(id).ToList();
                    //        if (profileDetails != null && profileDetails.Count > 0)
                    //        {
                    //            email = profileDetails[0].Email;
                    //        }
                    //        subject = DecisionPointR.InviteMailSubjectForVendorAndClient;
                    //        // text = DecisionPointR.InviteMailBodyMsg1 + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + DecisionPointR.InviteMailBodyMsg2 + businessName + DecisionPointR.InviteMailBodyForVendorAndClientMsg1 + businessName +
                    //        //DecisionPointR.InviteMailBodyForVendorAndClientMsg2 + businessName + DecisionPointR.InviteMailBodyForVendorAndClientMsg3 + ViewModel.GetSiteRoot() + DecisionPointR.InviteMailBodyMsg6 + userId + 
                    //        //DecisionPointR.InviteMailBodyForVendorAndClientMsg4 + objCompanyDashBoardRequest.emailId + DecisionPointR.InviteMailBodyMsg3 + password + DecisionPointR.InviteMailBodyMsg4 + signature + DecisionPointR.InviteMailBodyMsg5;
                    //        text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker<br/><br/>" + businessName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " +
                    //            businessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href=" + ViewModel.GetSiteRoot() + "?id=" + userId +
                    //            "'>click here</a> to get to log in page</br><br/>User Name: " + objCompanyDashBoardRequest.emailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    //    }
                    //    else if (objSendInvitation.type.Trim().ToLower().Equals(Shared.VendorWithoutId.Trim().ToLower()))
                    //    {
                    //        subject = DecisionPointR.InviteMailSubjectForVendorAndClient;
                    //        // text = DecisionPointR.InviteMailBodyMsg1 + objSendInvitation.firstName + " " + objSendInvitation.lastName + DecisionPointR.InviteMailBodyMsg2 + businessName + DecisionPointR.InviteMailBodyForVendorAndClientMsg1 + businessName + DecisionPointR.InviteMailBodyForVendorAndClientMsg2 + 
                    //        //businessName + DecisionPointR.InviteMailBodyForVendorAndClientMsg3 + ViewModel.GetSiteRoot() + DecisionPointR.InviteMailBodyMsg6 + userId + DecisionPointR.InviteMailBodyForVendorAndClientMsg4 +
                    //        //objCompanyDashBoardRequest.emailId + DecisionPointR.InviteMailBodyMsg3 + password + DecisionPointR.InviteMailBodyMsg4 + signature + DecisionPointR.InviteMailBodyMsg5;
                    //        text = "<div style='line-height:25px'>To: " + objSendInvitation.firstName + Shared.SingleSpace + objSendInvitation.lastName + "<br/>From: " + businessName + "<br/>Subject: Compliance Tracker<br/><br/>" + businessName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " +
                    //           businessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href=" + ViewModel.GetSiteRoot() + "?id=" + userId + "'>click here</a> to get to log in page</br><br/>User Name: " +
                    //         objCompanyDashBoardRequest.emailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    //    }
                    //    //sent mail for invitation
                    //    objDecisionPointEngine.GetSmtpDetail(email, text, subject);
                    //}
                    #endregion

                }
                return 1;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// resend mail to user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="type">type specifies mail format for staff ic or vendor/client</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 10 2014</CreatedDate>
        /// <returns>int</returns>
        public int ResendInviteToUser(int userId, string type)
        {
            try
            {
                companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                objDecisionPointEngine = new DecisionPointEngine();
                objbusinessCryptography = new BusinessCryptography();
                string signature = string.Empty;
                string subject = string.Empty;
                string text = string.Empty;
                string password = string.Empty;
                string email = string.Empty;
                IList<InvitationMailSendResponse> objInvitationMailSendResponse = objDecisionPointEngine.ResendInvitationMail(userId);
                if (objInvitationMailSendResponse != null)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(objInvitationMailSendResponse[0].EmailId)))
                    {
                        email = objInvitationMailSendResponse[0].EmailId;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(objInvitationMailSendResponse[0].Password)))
                    {
                        password = objbusinessCryptography.base64Decode2(objInvitationMailSendResponse[0].Password);
                    }
                    signature = objDecisionPointEngine.GetSignature(objInvitationMailSendResponse[0].ParentUserId);

                    if (!string.IsNullOrEmpty(signature))
                    {
                        string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                        signature = sign[1].Trim();
                    }
                    objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(objInvitationMailSendResponse[0].ParentUserId);


                    if (type.Equals(Shared.Staff) || type.Equals(Shared.IC.ToLower(CultureInfo.InvariantCulture)))
                    {
                        subject = "Compliance Tracker Account";
                        if (type.Equals(Shared.IC.ToLower(CultureInfo.InvariantCulture)))
                        {
                            userType = Shared.IC;
                        }
                        else { userType = Shared.Staff; }
                        //text = "<div style='line-height:25px'>To: " + objInvitationMailSendResponse[0].FirstName + " " + objInvitationMailSendResponse[0].LastName + "<br/>From: " + objInvitationMailSendResponse[0].ParentCompanyId + "<br/>Subject: Compliance Tracker Account<br/><br/>" + objInvitationMailSendResponse[0].BusinessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + objInvitationMailSendResponse[0].ParentUserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objInvitationMailSendResponse[0].EmailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    }
                    if (type.Equals(Shared.Vendor.ToLower(CultureInfo.InvariantCulture)) || type.Equals(Shared.Company.ToLower(CultureInfo.InvariantCulture)))
                    {
                        subject = "Compliance Tracker";
                        userType = Shared.Staff;
                        //text = "<div style='line-height:25px'>To: " + objInvitationMailSendResponse[0].FirstName + " " + objInvitationMailSendResponse[0].LastName + "<br/>From: " + objInvitationMailSendResponse[0].ParentCompanyId + "<br/>Subject: Compliance Tracker<br/><br/>" + objInvitationMailSendResponse[0].ParentCompanyId + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + objInvitationMailSendResponse[0].BusinessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + objInvitationMailSendResponse[0].ParentUserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objInvitationMailSendResponse[0].EmailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    }
                    if (type.Equals(Shared.NonClient))
                    {
                        #region IC Non Client Mail format

                        subject = "Compliance Tracker";
                        userType = Shared.Staff;
                        //text = "<div style='line-height:25px'>To: " + objInvitationMailSendResponse[0].FirstName + Shared.SingleSpace + objInvitationMailSendResponse[0].LastName + "<br/>From: " + objInvitationMailSendResponse[0].ParentCompanyId + "<br/>Subject: Compliance Tracker<br/><br/>Dear " + objInvitationMailSendResponse[0].FirstName + Shared.SingleSpace + objInvitationMailSendResponse[0].LastName + "<br/>" + objInvitationMailSendResponse[0].ParentCompanyId + " from " + objInvitationMailSendResponse[0].BusinessName + " has allowed you access to their profile to consider assigning them work.  Your access is included at the bottom of the e-mail along with the link to access the log in.<br/><br/>Thank you for using Compliance Tracker.<br/><br/> Please <a href='" + ViewModel.GetSiteRoot() + "?id=" + objInvitationMailSendResponse[0].ParentUserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objInvitationMailSendResponse[0].EmailId + "<br/> Password: " + objInvitationMailSendResponse[0].Password + "<br/><br/></br></br>" + signature + "</div>";
                        #endregion
                    }
                    objMailInviteFormat = new MailInviteFormat()
                    {
                        PersonName = objInvitationMailSendResponse[0].FirstName + Shared.SingleSpace + objInvitationMailSendResponse[0].LastName,
                        PersonCompanyName = objInvitationMailSendResponse[0].BusinessName,
                        PersonUserType = userType,
                        Passwrod = password,
                        Signature = signature,
                        DomainUrl = ViewModel.GetSiteRoot(),
                        EmailId = objInvitationMailSendResponse[0].EmailId,
                        InviteeUserId = Convert.ToString(objInvitationMailSendResponse[0].ParentUserId, CultureInfo.InvariantCulture),
                        FilePath = HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture)),
                        InviteePersonName = objUserDashBoardResponse.fName + Shared.SingleSpace + objUserDashBoardResponse.lName,
                        InviteeCompanyName = objUserDashBoardResponse.companyName
                    };

                    text = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                    objDecisionPointEngine.GetSmtpDetail(email, text, subject);
                    //set the last invite date in mapping table
                    objDecisionPointEngine.UpdateLastInvite(companyId, userId, type);
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Save Coverage Area
        public int SaveCoverageArea(CoverageAreaModel objSaveCoverageArea)
        {
            int Inserted = 0;
            try
            {
                objSaveCoverageArea.ModifiedBy = Convert.ToInt32(HttpContext.Current.Session["superAdmin"], CultureInfo.InvariantCulture);
                objDecisionPointEngine = new DecisionPointEngine();
                if (objSaveCoverageArea.CoverageAreaType.Equals(Shared.State))
                {
                    #region Save State
                    if (!string.IsNullOrEmpty(objSaveCoverageArea.StateCol))
                    {
                        StateRequest stateRequest = new StateRequest
                        {
                            StateName = objSaveCoverageArea.StateCol,
                            UserId = objSaveCoverageArea.UserId,
                            CompanyId = objSaveCoverageArea.CompanyId,
                            ModifiedBy = objSaveCoverageArea.ModifiedBy,
                            CoverageAreaFor = objSaveCoverageArea.CoverageAreaFor
                        };
                        Inserted = objDecisionPointEngine.SaveStateMapping(stateRequest, objSaveCoverageArea.CoverageAreaMode);
                    }
                    #endregion
                    #region Save County
                    if (!string.IsNullOrEmpty(objSaveCoverageArea.CountyCol))
                    {
                        CountyRequest countyRequest = new CountyRequest
                        {
                            CountyName = objSaveCoverageArea.CountyCol,
                            UserId = objSaveCoverageArea.UserId,
                            CompanyId = objSaveCoverageArea.CompanyId,
                            ModifiedBy = objSaveCoverageArea.ModifiedBy,
                            CoverageAreaFor = objSaveCoverageArea.CoverageAreaFor
                        };
                        Inserted = objDecisionPointEngine.SaveCountyMapping(countyRequest, objSaveCoverageArea.CoverageAreaMode);
                    }
                    #endregion
                    #region Save City
                    if (!string.IsNullOrEmpty(objSaveCoverageArea.CityCol))
                    {
                        CityRequest cityRequest = new CityRequest
                        {
                            CityName = objSaveCoverageArea.CityCol,
                            UserId = objSaveCoverageArea.UserId,
                            CompanyId = objSaveCoverageArea.CompanyId,
                            ModifiedBy = objSaveCoverageArea.ModifiedBy,
                            CoverageAreaFor = objSaveCoverageArea.CoverageAreaFor
                        };
                        Inserted = objDecisionPointEngine.SaveCityMapping(cityRequest, objSaveCoverageArea.CoverageAreaMode);
                    }
                    #endregion
                }
                else if (objSaveCoverageArea.CoverageAreaType.Equals(Shared.Zip))
                {
                    #region Save Zip
                    if (!string.IsNullOrEmpty(objSaveCoverageArea.ZipCol))
                    {
                        ZipRequest zipRequest = new ZipRequest
                        {
                            ZipCode = objSaveCoverageArea.ZipCol,
                            UserId = objSaveCoverageArea.UserId,
                            CompanyId = objSaveCoverageArea.CompanyId,
                            CoverageAreaFor = objSaveCoverageArea.CoverageAreaFor,
                            ModifiedBy = objSaveCoverageArea.ModifiedBy
                        };
                        Inserted = objDecisionPointEngine.SaveZipMapping(zipRequest, objSaveCoverageArea.CoverageAreaMode);
                    }
                    #endregion
                }
                return Inserted;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Sterling
        public string InitiateOnSterling(SterlingModel objSterlingModel)
        {
            string sterlingResponseStatus = string.Empty;
            try
            {
                if (object.Equals(objSterlingModel.UserId, 0))
                {
                    userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                    objSterlingModel.UserId = userId;
                }
                else
                {
                    userId = objSterlingModel.UserId;
                }
                if (string.IsNullOrEmpty(objSterlingModel.CompanyId))
                {
                    companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objSterlingModel.CompanyId = companyId;
                }
                else
                {
                    companyId = objSterlingModel.CompanyId;
                }



                objSterlingModel.UniqueRequestId = GenerateUniqueRequestId(userId);
                #region Sterling Credentials
                objSterlingModel.SterlingUserName = Convert.ToString(ConfigurationManager.AppSettings["SterlingUserName"], CultureInfo.InvariantCulture);
                objSterlingModel.SterlingPassword = Convert.ToString(ConfigurationManager.AppSettings["SterlingPassword"], CultureInfo.InvariantCulture);
                objSterlingModel.SterlingAccount = Convert.ToString(ConfigurationManager.AppSettings["SterlingGroup"], CultureInfo.InvariantCulture);
                sterlingOrderInitiateUrl = Convert.ToString(ConfigurationManager.AppSettings["SterlingOrderInitiateUrl"], CultureInfo.InvariantCulture);
                #endregion

                //call method for create the request xml format for initiate/registred the candidate on sterling
                if (!objSterlingModel.PackageName.Contains("Basic"))
                {
                    #region Sterling Packages
                    //XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc.Load("D://BackgroundCheck.xml");
                    XmlDocument xmlDoc = CreateHRXMLForInitiateBGCheck(objSterlingModel);
                    //get text from xml file

                    xmlTextFormat = GetTextFromXMlFile(xmlDoc);
                    Url = sterlingOrderInitiateUrl;
                    objHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);

                    byte[] bytes;
                    bytes = System.Text.Encoding.ASCII.GetBytes(xmlTextFormat);
                    objHttpWebRequest.Method = "POST";
                    objHttpWebRequest.ContentLength = bytes.Length;
                    objHttpWebRequest.ContentType = "text/xml; charset=utf-8";
                    objHttpWebRequest.Credentials = new NetworkCredential(objSterlingModel.SterlingUserName, objSterlingModel.SterlingPassword);
                    objRequestStream = objHttpWebRequest.GetRequestStream();
                    objRequestStream.Write(bytes, 0, bytes.Length);

                    //Close stream
                    objRequestStream.Close();

                    objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();

                    //Start HttpResponse
                    if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader sr = new StreamReader(objHttpWebResponse.GetResponseStream(), System.Text.Encoding.Default);
                        string backstr = sr.ReadToEnd();

                        //SaveRequestXMLFile(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SterlingRequestDocPath"], CultureInfo.InvariantCulture) + objSterlingModel.UniqueRequestId), objSterlingModel.UserId + Shared.XmlFileExt,xmlDoc);
                        xmlDoc.Save(Path.Combine(HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SterlingRequestDocPath"], CultureInfo.InvariantCulture)), objSterlingModel.UniqueRequestId + Shared.XmlFileExt));

                        if (!string.IsNullOrEmpty(backstr))
                        {
                            if (backstr.Equals(Shared.Error))
                            {
                                sterlingResponseStatus = Shared.Fail;
                            }
                            else
                            {
                                XmlDocument sterlingOrderDoc = new XmlDocument();
                                sterlingOrderDoc.LoadXml(backstr);
                                //XmlReader sterlingOrderreader;
                                //sterlingOrderDoc.Load(sterlingOrderreader);
                                //StreamReader sterlingOrderSReader;
                                //sterlingOrderDoc.Load(sterlingOrderSReader);
                                sterlingResponseStatus = Shared.OK;
                                //XML parsing for order confirmation
                                objSterlingModel.BackgroundCheckOrderNumber = sterlingOrderDoc.GetElementsByTagName("IdValue").Item(0).InnerText;
                               
                                SaveAndUpdatePackages(objSterlingModel);
                                SaveSterlingLog(objSterlingModel);
                            }
                        }


                    }
                    else
                    {
                        sterlingResponseStatus = Shared.Fail;
                    }
                    //Close HttpWebResponse
                    objHttpWebResponse.Close();
                    #endregion
                }
                else
                {
                    #region Basic
                    SaveAndUpdatePackages(objSterlingModel);
                    #endregion
                }
            }
            catch (Exception)
            {

                throw;
            }
            return sterlingResponseStatus;
        }
        /// <summary>
        /// Create the request xml format for initiate/registred the candidate on sterling
        /// </summary>
        /// <param name="objSterlingModel"></param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 Feb 2015</CreatedDate>
        private XmlDocument CreateHRXMLForInitiateBGCheck(SterlingModel objSterlingModel)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                #region Create Nodes
                //create root node

                XmlNode rootNode = xmlDoc.CreateElement("BackgroundCheck");

                xmlDoc.AppendChild(rootNode);

                xmlDoc.DocumentElement.SetAttribute("account", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributeGroup"], CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("password", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributePassword"], CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("userId", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributeUserId"], CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("dpuserId", Convert.ToString(objSterlingModel.UserId, CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("xmlns", "http://ns.hr-xml.org/2006-02-28");
                xmlDoc.DocumentElement.SetAttribute("xmlns:xs", "http://www.w3.org/2001/XMLSchema");

                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.InsertBefore(xmldecl, rootNode);

                //BackgroundCheck-BackgroundSearchPackage
                XmlNode backgroundSearchPackageNode = xmlDoc.CreateElement("BackgroundSearchPackage");
                XmlAttribute backgroundSearchPackageattribute = xmlDoc.CreateAttribute("type");
                backgroundSearchPackageattribute.Value = objSterlingModel.PackageName;
                backgroundSearchPackageNode.Attributes.Append(backgroundSearchPackageattribute);

                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation
                XmlNode processingInformationNode = xmlDoc.CreateElement("ProcessingInformation");
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=UserName]
                XmlNode accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessUserNameCredentialattribute = xmlDoc.CreateAttribute("type");
                accessUserNameCredentialattribute.Value = "UserName";
                accessCredentialNode.Attributes.Append(accessUserNameCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingUserName;
                processingInformationNode.AppendChild(accessCredentialNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=Password]
                accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessPasswordCredentialattribute = xmlDoc.CreateAttribute("type");
                accessPasswordCredentialattribute.Value = "Password";
                accessCredentialNode.Attributes.Append(accessPasswordCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingPassword;
                processingInformationNode.AppendChild(accessCredentialNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=Account]
                accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessAccountCredentialattribute = xmlDoc.CreateAttribute("type");
                accessAccountCredentialattribute.Value = "Account";
                accessCredentialNode.Attributes.Append(accessAccountCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingAccount;
                processingInformationNode.AppendChild(accessCredentialNode);

                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-ClientReferenceId
                XmlNode clientReferenceIdNode = xmlDoc.CreateElement("ReferenceId");
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-ClientReferenceId-IdValue
                XmlNode clientReferenceIdValueNode = xmlDoc.CreateElement("IdValue");
                clientReferenceIdValueNode.InnerText = objSterlingModel.UniqueRequestId;
                clientReferenceIdNode.AppendChild(clientReferenceIdValueNode);

                //BackgroundCheck-BackgroundSearchPackage-PersonalData
                XmlNode personalDataNode = xmlDoc.CreateElement("PersonalData");
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PersonName
                XmlNode personNameNode = xmlDoc.CreateElement("PersonName");
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PersonName-GivenName[First Name]
                XmlNode givenNameNode = xmlDoc.CreateElement("GivenName");
                givenNameNode.InnerText = objSterlingModel.FirstName;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PersonName-MiddleName
                XmlNode middleNameNode = xmlDoc.CreateElement("MiddleName");
                middleNameNode.InnerText = objSterlingModel.MiddelName;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PersonName-FamilyName[Last Name]
                XmlNode familyNameNode = xmlDoc.CreateElement("FamilyName");
                familyNameNode.InnerText = objSterlingModel.LastName;
                XmlAttribute familyNameattribute = xmlDoc.CreateAttribute("primary");
                familyNameattribute.Value = "true";
                familyNameNode.Attributes.Append(familyNameattribute);

                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress
                XmlNode postalAddressNode = xmlDoc.CreateElement("PostalAddress");
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress-PostalCode
                XmlNode postalCodeNode = xmlDoc.CreateElement("PostalCode");
                postalCodeNode.InnerText = objSterlingModel.ZipCode;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress-Region[State]
                XmlNode regionNode = xmlDoc.CreateElement("Region");
                regionNode.InnerText = objSterlingModel.State;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress-Municipality[City]
                XmlNode municipalityNode = xmlDoc.CreateElement("Municipality");
                municipalityNode.InnerText = objSterlingModel.City;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress-AddressLine
                XmlNode deliveryAddressNode = xmlDoc.CreateElement("DeliveryAddress");
                XmlNode addressLineNode = xmlDoc.CreateElement("AddressLine");
                addressLineNode.InnerText = objSterlingModel.AddressLine1;

                //BackgroundCheck-BackgroundSearchPackage-PersonalData-ContactMethod
                XmlNode contactMethodNode = xmlDoc.CreateElement("ContactMethod");
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-ContactMethod-Telephone
                XmlNode telephoneNode = xmlDoc.CreateElement("Telephone");
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-ContactMethod-Telephone-FormattedNumber
                XmlNode formattedNumberNode = xmlDoc.CreateElement("FormattedNumber");
                formattedNumberNode.InnerText = objSterlingModel.PhoneNumber;
                //BackgroundCheck-BackgroundSearchPackage-PersonalData-ContactMethod-InternetEmailAddress
                XmlNode internetEmailAddressNode = xmlDoc.CreateElement("InternetEmailAddress");
                internetEmailAddressNode.InnerText = objSterlingModel.EmailAddress;

                //BackgroundCheck-BackgroundSearchPackage-Screenings
                XmlNode screeningsNode = xmlDoc.CreateElement("Screenings");
                //BackgroundCheck-BackgroundSearchPackage-Screenings-PackageId
                XmlNode packageIdNode = xmlDoc.CreateElement("PackageId");
                XmlAttribute packageIdattribute = xmlDoc.CreateAttribute("idOwner");
                packageIdattribute.Value = "Sterling";
                packageIdNode.Attributes.Append(packageIdattribute);
                //BackgroundCheck-BackgroundSearchPackage-Screenings-PackageId-IdValue
                XmlNode packageIdValueNode = xmlDoc.CreateElement("IdValue");
                packageIdValueNode.InnerText = objSterlingModel.PackageName;
                XmlAttribute packageIdValueattribute = xmlDoc.CreateAttribute("name");
                packageIdValueattribute.Value = "NumValue";
                packageIdValueNode.Attributes.Append(packageIdValueattribute);

                #endregion
                #region Added Nodes
                //BackgroundCheck-BackgroundSearchPackage
                rootNode.AppendChild(backgroundSearchPackageNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformationNode
                backgroundSearchPackageNode.AppendChild(processingInformationNode);

                //BackgroundCheck-BackgroundSearchPackage-ClientReferenceIdNode
                backgroundSearchPackageNode.AppendChild(clientReferenceIdNode);


                //BackgroundCheck-BackgroundSearchPackage-PersonalData
                backgroundSearchPackageNode.AppendChild(personalDataNode);

                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PersonName
                personalDataNode.AppendChild(personNameNode);
                personNameNode.AppendChild(givenNameNode);
                personNameNode.AppendChild(middleNameNode);
                personNameNode.AppendChild(familyNameNode);

                //BackgroundCheck-BackgroundSearchPackage-PersonalData-PostalAddress
                personalDataNode.AppendChild(postalAddressNode);
                postalAddressNode.AppendChild(postalCodeNode);
                postalAddressNode.AppendChild(regionNode);
                postalAddressNode.AppendChild(municipalityNode);
                postalAddressNode.AppendChild(deliveryAddressNode);
                deliveryAddressNode.AppendChild(addressLineNode);

                //BackgroundCheck-BackgroundSearchPackage-PersonalData-ContactMethod
                personalDataNode.AppendChild(contactMethodNode);
                contactMethodNode.AppendChild(telephoneNode);
                telephoneNode.AppendChild(formattedNumberNode);
                contactMethodNode.AppendChild(internetEmailAddressNode);


                //BackgroundCheck-BackgroundSearchPackage-Screenings
                backgroundSearchPackageNode.AppendChild(screeningsNode);
                screeningsNode.AppendChild(packageIdNode);
                packageIdNode.AppendChild(packageIdValueNode);

                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }

            return xmlDoc;
        }

        private void SaveRequestXMLFile(string folderDirectory, string fileName, XmlDocument xmlDoc)
        {
            try
            {
                bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderDirectory));
                if (!folderExists)
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderDirectory));
                xmlDoc.Save(HttpContext.Current.Server.MapPath(folderDirectory + "/" + fileName));
            }
            catch (Exception ex)
            {
                throw;

            }

        }
        /// <summary>
        /// get Text Formate from XML File
        /// </summary>
        /// <param name="objXmlDocument"></param>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 Feb 2015</CreatedDate>
        public string GetTextFromXMlFile(XmlDocument objXmlDocument)
        {
            try
            {
                if (!object.Equals(objXmlDocument, null))
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        using (XmlTextWriter tx = new XmlTextWriter(sw))
                        {
                            objXmlDocument.WriteTo(tx);
                            xmlTextFormat = sw.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return xmlTextFormat;
        }

        /// <summary>
        /// Use for manage sterling request response log in database
        /// </summary>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 mar 2015</createddate>
        private int SaveSterlingLog(SterlingModel objSterlingModel)
        {
            int inserted = 0;
            try
            {
                if (object.Equals(objSterlingModel.UserId, 0))
                {
                    objSterlingModel.UserId = Convert.ToInt32(HttpContext.Current.Session["superAdmin"], CultureInfo.InvariantCulture);
                }
                if (object.Equals(objSterlingModel.CompanyId, 0))
                {
                    objSterlingModel.CompanyId = Convert.ToString(HttpContext.Current.Session["superAdminCompanyId"], CultureInfo.InvariantCulture);
                }
                objSterlingResponse = new SterlingResponse()
                {
                    DpCompanyId = objSterlingModel.CompanyId,
                    DpUserId = objSterlingModel.UserId,
                    ModifiedBy = objSterlingModel.UserId,
                    ModifiedCompanyId = objSterlingModel.CompanyId,
                    RequestFileUrl = Path.Combine(HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SterlingRequestDocPath"], CultureInfo.InvariantCulture)), objSterlingModel.UniqueRequestId + Shared.XmlFileExt),
                    PackageId = objSterlingModel.PackageId,
                    PackageName = objSterlingModel.PackageName,
                    Type = 0,
                    UniqueRequestId = objSterlingModel.UniqueRequestId,
                    SterlingOrderId = objSterlingModel.BackgroundCheckOrderNumber
                };
                objDecisionPointEngine = new DecisionPointEngine();
                objDecisionPointEngine.SaveSterlingLog(objSterlingResponse);



            }
            catch (Exception ex)
            {
                throw;
            }

            return inserted;
        }

        /// <summary>
        /// used for genrate Unique request Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>string</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 mar 2015</createddate>
        private string GenerateUniqueRequestId(int userId)
        {
            string uniqueRequestId = string.Empty;
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                //genrate request id and check that already genrated by system or not
                if (objDecisionPointEngine.CheckUnqiueSterlingRequest(BusinessCore.GenrateRandomnumber() + Shared.SingleDash + userId))
                {
                    GenerateUniqueRequestId(userId);
                }
                else
                {
                    uniqueRequestId = BusinessCore.GenrateRandomnumber() + Shared.SingleDash + userId;
                }
                return uniqueRequestId;
            }
            catch
            {
                throw;
            }
        }
        private int SaveAndUpdatePackages(SterlingModel objSterlingModel)
        {
            int inserted = 0;
            try
            {
                if (object.Equals(objSterlingModel.UserId, 0))
                {
                    userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                    modifiedBy = Convert.ToInt32(HttpContext.Current.Session["superAdmin"], CultureInfo.InvariantCulture);
                    objSterlingModel.UserId = userId;
                }
                else
                {
                    modifiedBy = objSterlingModel.UserId;
                }
                if (object.Equals(objSterlingModel.CompanyId, 0))
                {
                    companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objSterlingModel.CompanyId = companyId;
                }

                clientList = new List<string>();

                objSterlingModel.ICTypeIds = objDecisionPointEngine.GetCompanyVendorType(objSterlingModel.CompanyId, Shared.IC).ToList().Where(x => x.IsUserBased == true).Select(x => Convert.ToString(x.VendorTypeId)).Distinct().ToList();
                int parentUserId = objDecisionPointEngine.GetParentUserId(objSterlingModel.CompanyId, Shared.IC);
                objUserDashBoardResponse = new UserDashBoardResponse();
                objUserDashBoardResponse = objDecisionPointEngine.GetAccountDetails(parentUserId);
                if (objSterlingModel.PayType.Equals(Shared.One))
                {
                    clientList.Add(parentUserId + Shared.Colon + objUserDashBoardResponse.CompanyId);
                }
                else
                {
                    clientList.Add(objSterlingModel.UserId + Shared.Colon + objSterlingModel.CompanyId);
                }
                objDecisionPointEngine = new DecisionPointEngine();
                objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest()
                {
                    UserId = objSterlingModel.UserId,
                    CompanyId = objSterlingModel.CompanyId,
                    ICTypeStaffTitleIds = objSterlingModel.ICTypeIds,
                    ClientIds = clientList,
                    BGCheckId = Convert.ToString(objSterlingModel.PackageId, CultureInfo.InvariantCulture),
                    ModifiedBy = modifiedBy,
                    OperationType = objSterlingModel.OperationType,
                    PayType = objSterlingModel.PayType,
                    StateAbbre = objSterlingModel.StateAbbre
                };
                inserted = objDecisionPointEngine.SetBackgroundMapping(objBackGroundCheckMasterRequest);


            }
            catch (Exception)
            {
                throw;
            }
            return inserted;
        }
        /// <summary>
        /// Create the request xml format for initiate/registred the candidate on sterling
        /// </summary>
        /// <param name="objSterlingModel"></param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 Feb 2015</CreatedDate>
        public XmlDocument CreateHRXMLForReviewBGCheck(string SterlingOrderId)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                objSterlingModel = new SterlingModel();
                #region Create Nodes
                //create root node
                #region Sterling Credentials
                objSterlingModel.SterlingUserName = Convert.ToString(ConfigurationManager.AppSettings["SterlingUserName"], CultureInfo.InvariantCulture);
                objSterlingModel.SterlingPassword = Convert.ToString(ConfigurationManager.AppSettings["SterlingPassword"], CultureInfo.InvariantCulture);
                objSterlingModel.SterlingAccount = Convert.ToString(ConfigurationManager.AppSettings["SterlingGroup"], CultureInfo.InvariantCulture);

                #endregion
                XmlNode rootNode = xmlDoc.CreateElement("BackgroundCheck");

                xmlDoc.AppendChild(rootNode);

                xmlDoc.DocumentElement.SetAttribute("account", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributeGroup"], CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("password", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributePassword"], CultureInfo.InvariantCulture));
                xmlDoc.DocumentElement.SetAttribute("userId", Convert.ToString(ConfigurationManager.AppSettings["SterlingBgAttributeUserId"], CultureInfo.InvariantCulture));

                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmlDoc.InsertBefore(xmldecl, rootNode);

                //BackgroundCheck-ReferenceId
                XmlNode ReferenceIdNode = xmlDoc.CreateElement("ReferenceId");
                XmlAttribute referenceIdattribute = xmlDoc.CreateAttribute("idOwner");
                referenceIdattribute.Value = "sterling";
                ReferenceIdNode.Attributes.Append(referenceIdattribute);
                //BackgroundCheck-ReferenceId-IdValue
                XmlNode clientReferenceIdValueNode = xmlDoc.CreateElement("IdValue");
                clientReferenceIdValueNode.InnerText = SterlingOrderId;
                ReferenceIdNode.AppendChild(clientReferenceIdValueNode);

                //BackgroundCheck-BackgroundSearchPackage
                XmlNode backgroundSearchPackageNode = xmlDoc.CreateElement("BackgroundSearchPackage");
                //XmlAttribute backgroundSearchPackageattribute = xmlDoc.CreateAttribute("type");
                //backgroundSearchPackageattribute.Value = objSterlingModel.PackageName;
                //backgroundSearchPackageNode.Attributes.Append(backgroundSearchPackageattribute);

                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation
                XmlNode processingInformationNode = xmlDoc.CreateElement("ProcessingInformation");
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=UserName]
                XmlNode accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessUserNameCredentialattribute = xmlDoc.CreateAttribute("type");
                accessUserNameCredentialattribute.Value = "UserName";
                accessCredentialNode.Attributes.Append(accessUserNameCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingUserName;
                processingInformationNode.AppendChild(accessCredentialNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=Password]
                accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessPasswordCredentialattribute = xmlDoc.CreateAttribute("type");
                accessPasswordCredentialattribute.Value = "Password";
                accessCredentialNode.Attributes.Append(accessPasswordCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingPassword;
                processingInformationNode.AppendChild(accessCredentialNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation-AccessCredential With [Type=Account]
                accessCredentialNode = xmlDoc.CreateElement("AccessCredential");
                XmlAttribute accessAccountCredentialattribute = xmlDoc.CreateAttribute("type");
                accessAccountCredentialattribute.Value = "Account";
                accessCredentialNode.Attributes.Append(accessAccountCredentialattribute);
                accessCredentialNode.InnerText = objSterlingModel.SterlingAccount;
                processingInformationNode.AppendChild(accessCredentialNode);



                #endregion
                #region Added Nodes
                //BackgroundCheck-ReferenceId
                rootNode.AppendChild(ReferenceIdNode);
                //BackgroundCheck-BackgroundSearchPackage
                rootNode.AppendChild(backgroundSearchPackageNode);
                //BackgroundCheck-BackgroundSearchPackage-ProcessingInformation
                backgroundSearchPackageNode.AppendChild(processingInformationNode);





                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }

            return xmlDoc;
        }
        #endregion

        #region Others
        /// <summary>
        /// Used for Get the Spend time of particular user in company
        /// </summary>
        /// <param name="registerDate">registerDate</param>
        /// <returns>string</returns>
        ///<createddate>apr 4 2014</createddate>
        /// <returns>string type site root</returns>
        public static string GetSpendTime(DateTime registerDate)
        {
            try
            {
                string finalDate = string.Empty;
                int month = 0;
                int year = 0;
                int day = 0;
                DateTime registeredDate = new DateTime(registerDate.Year, registerDate.Month, registerDate.Day);
                DateTime currentDate = DateTime.UtcNow;
                TimeSpan difference = currentDate - registeredDate;
                day = difference.Days;
                if (difference.Days > 30 || difference.Days > 31)
                {
                    month = difference.Days / 30;
                    day = difference.Days % 30;
                    if (difference.Days > 365)
                    {
                        year = difference.Days / 365;
                        day = difference.Days % 365;
                        if (day > 30 || day > 31)
                        {
                            month = day / 30;
                            day = day % 30;
                        }
                    }
                }
                // if (!year.Equals(0))
                // {
                finalDate = month + Shared.Month;
                //}
                if (!year.Equals(0))
                {
                    finalDate = finalDate + Shared.SingleSpace + year + Shared.Year;
                }


                return finalDate;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for create the col from CSV File for Vendor
        /// </summary>
        /// <param name="objVendorCSV"></param>
        /// <returns>VendorCSV</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public VendorCSV BindColFromCSVFileForVendor(CsvReader objCSVReader, InviteVendorBulk invitevendor, int countIndex)
        {
            try
            {
                int flowID = 2;
                string flowName = Shared.SendTo;
                int docFlowID = 0;
                string docFlowName = string.Empty;
                objDecisionPointEngine = new DecisionPointEngine();
                // check Flow ID....
                if (!string.IsNullOrEmpty(Convert.ToString(objCSVReader.GetField<string>("Relationship"), CultureInfo.InvariantCulture)))
                {
                    var flowResult = (from q in invitevendor.flowDetails
                                      where q.flowName.Equals(objCSVReader.GetField<string>("Relationship"), StringComparison.OrdinalIgnoreCase)
                                      select q).FirstOrDefault();
                    if (flowResult != null)
                    {
                        flowID = flowResult.flowId;
                        flowName = flowResult.flowName;
                    }
                }

                // check DOC Flow ID....
                if (!string.IsNullOrEmpty(objCSVReader.GetField<string>("Flow")))
                {
                    var docFlowResult = (from q in objDecisionPointEngine.GetDocFlow()
                                         where q.flowName.Equals(objCSVReader.GetField<string>("Flow"), StringComparison.OrdinalIgnoreCase)
                                         select q).FirstOrDefault();
                    if (docFlowResult != null)
                    {
                        docFlowID = docFlowResult.flowId;
                        docFlowName = docFlowResult.flowName;
                    }
                }

                VendorCSV objVendorCSV = new VendorCSV()
                {
                    No = countIndex,
                    FName = objCSVReader.GetField<string>("First Name"),
                    LName = objCSVReader.GetField<string>("Last Name"),
                    EmailId = objCSVReader.GetField<string>("Email"),
                    CompanyName = objCSVReader.GetField<string>("Company Name"),
                    FlowId = flowID,
                    FlowValue = flowName,
                    DocFlowId = docFlowID,
                    DocFlowValue = docFlowName,
                    CheckType = Shared.VendorWithoutId


                };
                return objVendorCSV;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for create the col from CSV File for staff
        /// </summary>
        /// <param name="objVendorCSV"></param>
        /// <returns>CSV</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public CSV BindColFromCSVFileForStaff(CsvReader objCSVReader, InviteStaffModel inviteStaffModel, int countIndex)
        {
            try
            {
                string Title = string.Empty;
                int TittleID = 0;
                string Permission = Shared.User;
                int errorIndex = 0;

                if (!string.IsNullOrEmpty(Convert.ToString(objCSVReader.GetField<string>("Title"), CultureInfo.InvariantCulture)))
                {
                    if (!object.Equals(inviteStaffModel.titleDetails, null))
                    {
                        // comparing Title 
                        var Titleresult = (from q in inviteStaffModel.titleDetails
                                           where q.titleName.Equals(objCSVReader.GetField<string>("Title"), StringComparison.OrdinalIgnoreCase)
                                           select q).FirstOrDefault();
                        if (Titleresult != null)
                        {
                            Title = Titleresult.titleName;
                            TittleID = Titleresult.id;
                            errorIndex = +1;
                        }
                    }
                }

                bool status = (errorIndex == 3) ? false : true;

                CSV objCSV = new CSV()
                {
                    No = countIndex,
                    StaffFName = objCSVReader.GetField<string>("First Name"),
                    StaffLName = objCSVReader.GetField<string>("Last Name"),
                    staffEmail = objCSVReader.GetField<string>("Email"),
                    StaffPermission = Permission,
                    StaffTitle = Title,
                    StaffTitleID = TittleID,
                    ErrorStatus = status
                };
                errorIndex = 0;

                return objCSV;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for update and delete the record from CSV file for vendor : 0 used for update record and 1 used for delete record from CSv file
        /// </summary>
        /// <param name="serverpathname"></param>
        /// <param name="objInviteVendorBulk"></param>
        /// <param name="objUpdateVendorModel"></param>
        /// <param name="type"></param>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public void ReadAndWriteContentInCSVForvendor(string serverpathname, InviteVendorBulk objInviteVendorBulk, UpdateVendorModel objUpdateVendorModel, int type)
        {
            try
            {
                int rowno = 0, countIndex = 0;
                //Read the CSV file and check that in which CSv file user want to update the record
                CsvReader objCsvReader = CSVFileReader.ReadCSvFile(serverpathname);
                while (objCsvReader.Read())
                {


                    if (countIndex.Equals(objUpdateVendorModel.No.Value - 1))
                    {
                        rowno = countIndex;
                    }
                    //added the CSV file record in collection
                    objInviteVendorBulk.LstVendorCsv.Add(new VendorCSV()
                    {
                        No = countIndex + 1,
                        FName = objCsvReader.GetField<string>("First Name"),
                        LName = objCsvReader.GetField<string>("Last Name"),
                        EmailId = objCsvReader.GetField<string>("Email"),
                        CompanyName = objCsvReader.GetField<string>("Company Name"),
                        FlowValue = objCsvReader.GetField<string>("Relationship"),
                        DocFlowValue = objCsvReader.GetField<string>("Flow"),
                        CheckType = Shared.VendorWithoutId

                    });
                    countIndex++;
                }
                if (countIndex.Equals(objUpdateVendorModel.No.Value - 1))
                {
                    rowno = countIndex;
                }
                //Dispose the CSV Reader object
                objCsvReader.Dispose();
                //check that record is edit or delete : 0 for edit and 1 for delete 
                if (type.Equals(1))
                {
                    VendorCSV item = objInviteVendorBulk.LstVendorCsv.Where(x => x.No == rowno + 1).SingleOrDefault();
                    objInviteVendorBulk.LstVendorCsv.Remove(item);

                }
                int count = 0;
                //Update the record in CSV file : create the object of CSV writer for write in CSV file
                CsvWriter objCsvWriter = CSVFileReader.WriteCSvFile(serverpathname);
                //added the header in CSV file
                objCsvWriter.Configuration.HasHeaderRecord = true;
                objCsvWriter.WriteField("Company Name");
                objCsvWriter.WriteField("Relationship");
                objCsvWriter.WriteField("Flow");
                objCsvWriter.WriteField("First Name");
                objCsvWriter.WriteField("Last Name");
                objCsvWriter.WriteField("Email");
                objCsvWriter.NextRecord();
                foreach (var item in objInviteVendorBulk.LstVendorCsv)
                {
                    // if user going to edit the record
                    if (type.Equals(0))
                    {
                        if (count.Equals(rowno))
                        {
                            objInviteVendorBulk.LstVendorCsv[count].FName = objUpdateVendorModel.FName;
                            objInviteVendorBulk.LstVendorCsv[count].LName = objUpdateVendorModel.LName;
                            objInviteVendorBulk.LstVendorCsv[count].CompanyName = objUpdateVendorModel.CName;
                            objInviteVendorBulk.LstVendorCsv[count].EmailId = objUpdateVendorModel.Email;
                            objInviteVendorBulk.LstVendorCsv[count].FlowValue = objUpdateVendorModel.FlowText;
                            objInviteVendorBulk.LstVendorCsv[count].DocFlowValue = objUpdateVendorModel.DocFlowText;

                        }
                    }
                    //Update the record
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].CompanyName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].FlowValue);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].DocFlowValue);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].FName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].LName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].EmailId);
                    objCsvWriter.NextRecord();
                    count++;
                }
                //Dispose the CSV writer object
                objCsvWriter.Dispose();

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// used for update and delete the record from CSV file for staff : 0 used for update record nad 1 used for delete record from CSv file
        /// </summary>
        /// <param name="serverpathname"></param>
        /// <param name="objInviteVendorBulk"></param>
        /// <param name="objUpdateVendorModel"></param>
        /// <param name="type"></param>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public void ReadAndWriteContentInCSVForStaff(string serverpathname, InviteStaffModel objInviteStaffModel, CSV objCSV, int type)
        {
            try
            {
                int rowno = 0, countIndex = 0;
                //Read the CSV file and check that in which CSv file user want to update the record
                CsvReader objCsvReader = CSVFileReader.ReadCSvFile(serverpathname);
                while (objCsvReader.Read())
                {


                    if (countIndex.Equals(objCSV.No - 1))
                    {
                        rowno = countIndex;
                    }
                    //added the CSV file record in collection
                    objInviteStaffModel.lst_Csv.Add(new CSV()
                    {
                        No = countIndex + 1,
                        StaffFName = objCsvReader.GetField<string>("First Name"),
                        StaffLName = objCsvReader.GetField<string>("Last Name"),
                        staffEmail = objCsvReader.GetField<string>("Email"),
                        StaffTitle = objCsvReader.GetField<string>("Title"),

                    });
                    countIndex++;
                }
                if (countIndex.Equals(objCSV.No - 1))
                {
                    rowno = countIndex;
                }
                //Dispose the CSV Reader object
                objCsvReader.Dispose();
                //check that record is edit or delete : 0 for edit and 1 for delete 
                if (type.Equals(1))
                {
                    CSV item = objInviteStaffModel.lst_Csv.Where(x => x.No == rowno + 1).SingleOrDefault();
                    objInviteStaffModel.lst_Csv.Remove(item);

                }
                int count = 0;
                //Update the record in CSV file : create the object of CSV writer for write in CSV file
                CsvWriter objCsvWriter = CSVFileReader.WriteCSvFile(serverpathname);
                //added the header in CSV file
                objCsvWriter.Configuration.HasHeaderRecord = true;
                objCsvWriter.WriteField("First Name");
                objCsvWriter.WriteField("Last Name");
                objCsvWriter.WriteField("Email");
                objCsvWriter.WriteField("Title");
                objCsvWriter.NextRecord();
                foreach (var item in objInviteStaffModel.lst_Csv)
                {
                    // if user going to edit the record
                    if (type.Equals(0))
                    {
                        if (count.Equals(rowno))
                        {
                            objInviteStaffModel.lst_Csv[count].StaffFName = objCSV.StaffFName;
                            objInviteStaffModel.lst_Csv[count].StaffLName = objCSV.StaffLName;
                            objInviteStaffModel.lst_Csv[count].staffEmail = objCSV.staffEmail;
                            objInviteStaffModel.lst_Csv[count].StaffTitle = objCSV.StaffTitle;

                        }
                    }
                    //Update the record
                    objCsvWriter.WriteField(objInviteStaffModel.lst_Csv[count].StaffFName);
                    objCsvWriter.WriteField(objInviteStaffModel.lst_Csv[count].StaffLName);
                    objCsvWriter.WriteField(objInviteStaffModel.lst_Csv[count].staffEmail);
                    objCsvWriter.WriteField(objInviteStaffModel.lst_Csv[count].StaffTitle);
                    objCsvWriter.NextRecord();
                    count++;
                }
                //Dispose the CSV writer object
                objCsvWriter.Dispose();

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Used for create the col from CSV File for IC
        /// </summary>
        /// <param name="objVendorCSV"></param>
        /// <returns>VendorCSV</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public VendorCSV BindColFromCSVFileForIC(CsvReader objCSVReader, InviteVendorBulk invitevendor, int countIndex)
        {
            try
            {
                int flowID = 2;
                string flowName = Shared.SendTo;
                int paymentID = 0;
                string paymentType = string.Empty;
                int VTID = 0;
                string VType = string.Empty;
                string BGC = string.Empty;
                objDecisionPointEngine = new DecisionPointEngine();
                //Chek Background Check
                if (!string.IsNullOrEmpty(Convert.ToString(objCSVReader.GetField<string>("Background Check"), CultureInfo.InvariantCulture)))
                {
                    if (objCSVReader.GetField<string>("Background Check").Equals(Shared.Yes, StringComparison.OrdinalIgnoreCase) || objCSVReader.GetField<string>("Background Check").Equals(Shared.No, StringComparison.OrdinalIgnoreCase))
                    {
                        BGC = objCSVReader.GetField<string>("Background Check");
                    }
                }
                //Check Background Check
                if (!string.IsNullOrEmpty(objCSVReader.GetField<string>("Profession Type")))
                {
                    if (!object.Equals(invitevendor.CompanyVendorTypeDetails, null))
                    {
                        if (invitevendor.CompanyVendorTypeDetails.Count() > 0)
                        {
                            var VTresult = (from q in invitevendor.CompanyVendorTypeDetails
                                            where q.VendorTypeName.Equals(objCSVReader.GetField<string>("Profession Type"), StringComparison.OrdinalIgnoreCase)
                                            select q).FirstOrDefault();
                            if (VTresult != null)
                            {
                                VType = VTresult.VendorTypeName;
                                VTID = VTresult.VendorTypeId;
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(objCSVReader.GetField<string>("Payment")))
                {
                    var paymentresult = (from q in objDecisionPointEngine.GetICPaymentMode()
                                         where q.PaymentMode.Equals(objCSVReader.GetField<string>("Payment"), StringComparison.OrdinalIgnoreCase)
                                         select q).FirstOrDefault();
                    if (paymentresult != null)
                    {
                        paymentID = paymentresult.PaymentId;
                        paymentType = paymentresult.PaymentMode;
                    }
                }
                VendorCSV objvendorCSV = new VendorCSV()
                {
                    No = countIndex,
                    FName = objCSVReader.GetField<string>("First Name"),
                    LName = objCSVReader.GetField<string>("Last Name"),
                    EmailId = objCSVReader.GetField<string>("Email"),
                    CompanyName = objCSVReader.GetField<string>("Company Name"),
                    FlowId = flowID,
                    FlowValue = flowName,
                    CheckType = Shared.IcWithoutId,
                    PaymentID = paymentID,
                    PaymentType = paymentType,
                    VTID = VTID,
                    VType = VType,
                    BGCheck = BGC

                };
                return objvendorCSV;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for update and delete the record from CSV file for IC : 0 used for update record nad 1 used for delete record from CSv file
        /// </summary>
        /// <param name="serverpathname"></param>
        /// <param name="objInviteVendorBulk"></param>
        /// <param name="objUpdateVendorModel"></param>
        /// <param name="type"></param>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public void ReadAndWriteContentInCSVForIC(string serverpathname, InviteVendorBulk objInviteVendorBulk, UpdateVendorModel objUpdateVendorModel, int type)
        {
            try
            {
                int rowno = 0, countIndex = 0;
                //Read the CSV file and check that in which CSv file user want to update the record
                CsvReader objCsvReader = CSVFileReader.ReadCSvFile(serverpathname);
                while (objCsvReader.Read())
                {


                    if (countIndex.Equals(objUpdateVendorModel.No.Value - 1))
                    {
                        rowno = countIndex;
                    }
                    //added the CSV file record in collection
                    objInviteVendorBulk.LstVendorCsv.Add(new VendorCSV()
                    {
                        No = countIndex + 1,
                        FName = objCsvReader.GetField<string>("First Name"),
                        LName = objCsvReader.GetField<string>("Last Name"),
                        EmailId = objCsvReader.GetField<string>("Email"),
                        CompanyName = objCsvReader.GetField<string>("Company Name"),
                        FlowValue = Shared.Vendor,
                        CheckType = Shared.IcWithoutId,
                        PaymentType = objCsvReader.GetField<string>("Payment"),
                        VType = objCsvReader.GetField<string>("Profession Type"),
                        BGCheck = objCsvReader.GetField<string>("Background Check")

                    });
                    countIndex++;
                }
                if (countIndex.Equals(objUpdateVendorModel.No.Value - 1))
                {
                    rowno = countIndex;
                }
                //Dispose the CSV Reader object
                objCsvReader.Dispose();
                //check that record is edit or delete : 0 for edit and 1 for delete 
                if (type.Equals(1))
                {
                    VendorCSV item = objInviteVendorBulk.LstVendorCsv.Where(x => x.No == rowno + 1).SingleOrDefault();
                    objInviteVendorBulk.LstVendorCsv.Remove(item);

                }
                int count = 0;
                //Update the record in CSV file : create the object of CSV writer for write in CSV file
                CsvWriter objCsvWriter = CSVFileReader.WriteCSvFile(serverpathname);
                //added the header in CSV file
                objCsvWriter.Configuration.HasHeaderRecord = true;
                objCsvWriter.WriteField("Company Name");
                objCsvWriter.WriteField("Background Check");
                objCsvWriter.WriteField("Payment");
                objCsvWriter.WriteField("Profession Type");
                objCsvWriter.WriteField("First Name");
                objCsvWriter.WriteField("Last Name");
                objCsvWriter.WriteField("Email");
                objCsvWriter.NextRecord();
                foreach (var item in objInviteVendorBulk.LstVendorCsv)
                {
                    // if user going to edit the record
                    if (type.Equals(0))
                    {
                        if (count.Equals(rowno))
                        {
                            objInviteVendorBulk.LstVendorCsv[count].FName = objUpdateVendorModel.FName;
                            objInviteVendorBulk.LstVendorCsv[count].LName = objUpdateVendorModel.LName;
                            objInviteVendorBulk.LstVendorCsv[count].CompanyName = objUpdateVendorModel.CName;
                            objInviteVendorBulk.LstVendorCsv[count].EmailId = objUpdateVendorModel.Email;
                            objInviteVendorBulk.LstVendorCsv[count].PaymentType = objUpdateVendorModel.PaymentMode;
                            objInviteVendorBulk.LstVendorCsv[count].VType = objUpdateVendorModel.VType;
                            objInviteVendorBulk.LstVendorCsv[count].BGCheck = objUpdateVendorModel.BGCheck;
                        }
                    }
                    //Update the record
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].CompanyName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].BGCheck);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].PaymentType);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].VType);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].FName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].LName);
                    objCsvWriter.WriteField(objInviteVendorBulk.LstVendorCsv[count].EmailId);
                    objCsvWriter.NextRecord();
                    count++;
                }
                //Dispose the CSV writer object
                objCsvWriter.Dispose();

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// used for get the all tabs as per permission of particular user
        /// </summary>
        /// <returns>IList<PermissionTableResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public IList<PermissionTableResponse> GetFuntionalPermissionAsPerUserTitle(ConfiguratonSettingRequest objConfiguratonSettingRequest)
        {
            try
            {
                IList<PermissionTableResponse> permissionTableResult = null;
                if (!object.Equals(HttpContext.Current.Session["UserId"], null))
                {
                    userId = Convert.ToInt32(HttpContext.Current.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                    userType = Convert.ToString(HttpContext.Current.Session["UserType"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();

                    PermissionTableRequest objPermissionTableRequest = new PermissionTableRequest()
                    {
                        UserId = userId,
                        CreatedCompanyId = companyId,
                        UserType = userType
                    };
                    //call method for get all permissions as per title
                    permissionTableResult = objDecisionPointEngine.GetFuntionalPermissionAsPerUserTitle(objPermissionTableRequest);
                    if (objDecisionPointEngine.GetMyContract(userId).ToList().Count <= 0)
                    {
                        permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => x.TabName == Shared.MyContracts).ToList(), permissionTableResult);
                    }

                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        //if login user not  super admin
                        if (!string.Equals(userType, Shared.SuperAdmin) && !object.Equals(Convert.ToString(HttpContext.Current.Session["IsSuperAdmin"], CultureInfo.InvariantCulture), Shared.Yes))
                        {
                            //permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.MyPartner || x.TabName == Shared.ManageAnnouncement
                            //      || x.TabName == Shared.ManageFeeMaster || x.TabName == Shared.ManageLicenseMaster || x.TabName == Shared.ManageProfessionType))
                            //      .ToList(), permissionTableResult);

                            //if vendor and client not apply by config setting
                            if (!objConfiguratonSettingRequest.IsVendor && !objConfiguratonSettingRequest.IsClient)
                            {
                                permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.AddVendorClient
                                    || x.TabName == Shared.AddBulkVendorClient)).ToList(), permissionTableResult);
                            }
                            //if vendor or client any one from them not applied
                            else if (!objConfiguratonSettingRequest.IsVendor || !objConfiguratonSettingRequest.IsClient)
                            {
                                if (!objConfiguratonSettingRequest.IsVendor)
                                {
                                    permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.ManageVendor
                                        || x.TabName == Shared.Vendor)).ToList(), permissionTableResult);
                                }
                                if (!objConfiguratonSettingRequest.IsClient)
                                {
                                    permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.ManageClient
                                        || x.TabName == Shared.Client)).ToList(), permissionTableResult);
                                }
                            }
                            //if ic not apply by config setting
                            if (!objConfiguratonSettingRequest.IsIc)
                            {
                                permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.IC
                                    || x.TabName == Shared.ManageIC || x.TabName == Shared.AddIC || x.TabName == Shared.AddBulkIC)).ToList(), permissionTableResult);
                            }
                            //if service not apply by config setting
                            if (!objConfiguratonSettingRequest.IsServices)
                            {
                                permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.ManageServices
                                    || x.TabName == Shared.ServiceTranslationTable)).ToList(), permissionTableResult);
                            }
                            //if contract not apply by config setting
                            if (!objConfiguratonSettingRequest.IsContractApply)
                            {
                                permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.Contract)).ToList(), permissionTableResult);
                            }
                        }
                        //else
                        //{
                        //    if (!string.Equals(userType, Shared.SuperAdmin))
                        //    {
                        //        permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.MyPartner || x.TabName == Shared.ManageAnnouncement
                        //                  || x.TabName == Shared.ManageFeeMaster || x.TabName == Shared.ManageLicenseMaster || x.TabName == Shared.ManageProfessionType))
                        //                  .ToList(), permissionTableResult);
                        //        if (!string.Equals(userType, Shared.IC))
                        //        {
                        //            permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.MyLibrary || x.TabName == Shared.MyContracts)).ToList(), permissionTableResult);
                        //        }
                        //    }

                        //}
                    }
                    //else
                    //{
                    //    if (!string.Equals(userType, Shared.SuperAdmin))
                    //    {
                    //        permissionTableResult = RemoveRecordFromPermissiontable(permissionTableResult.Where(x => (x.TabName == Shared.MyPartner || x.TabName == Shared.ManageAnnouncement
                    //                  || x.TabName == Shared.ManageFeeMaster || x.TabName == Shared.ManageLicenseMaster || x.TabName == Shared.ManageProfessionType))
                    //                  .ToList(), permissionTableResult);
                    //    }

                    //}
                }

                return permissionTableResult;

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for remove the record from list
        /// </summary>
        /// <param name="removePerCol"></param>
        /// <param name="finalPerCol"></param>
        /// <returns>IList<PermissionTableResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        /// 
        public IList<PermissionTableResponse> RemoveRecordFromPermissiontable(IList<PermissionTableResponse> removeTabCol, IList<PermissionTableResponse> finalTabCol)
        {
            try
            {
                //apply loop on collection for remove from final tabs collection
                foreach (var perCol in removeTabCol)
                {
                    finalTabCol.Remove(perCol);
                }
                return finalTabCol;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for get config setting collection
        /// </summary>
        /// <returns>ConfiguratonSettingRequest</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Dec 2014</createdDate>
        public ConfiguratonSettingRequest GetConfigSetting()
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                if (!object.Equals(HttpContext.Current.Session["UserId"], null))
                {
                    companyId = Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objConfiguratonSettingRequest = objDecisionPointEngine.GetConfigSetting(companyId);
                }
                return objConfiguratonSettingRequest;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion
    }
}