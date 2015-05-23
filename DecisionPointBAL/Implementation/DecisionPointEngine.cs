// ********************************************************************************************************************************
//                                                  class:IDecisionPointEngine
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 18, 2013    |Arun Kumar     | This class implement all methods which define in IDecisionPointEngine.cs interface
// *********************************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointDAL.Implemention;
using DecisionPointDAL.Implemention.RequestParam;
using DecisionPointDAL.Implemention.ResponseParam;
using Xamarin.Payments.Stripe;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using System.Globalization;
using System.Text;
using System.Configuration;

namespace DecisionPointBAL.Implementation
{
    /// <summary>
    /// This class implement all methods which define in IDecisionPointEngine.cs interface
    /// </summary>
    public class DecisionPointEngine : IDecisionPointEngine, IDisposable
    {
        #region Global Area
        private DecisionPointRepository objdecisionPointRepository = null;
        LoginDetailsResponseParam loginDetailsResponseparam = null;
        UserDashBoardResponseParam objUserDashBoardResponseParam = null;
        LoginDetailResponse loginDetailResponse = null;
        BusinessEmail objBusinessEmail = null;
        BusinessCryptography businessCryptography = null;
        UserDashBoardRequestParam objUserDashBoardRequestParam = null;
        CompanyDashBoardRequestParam objCompanyDashBoardRequestParam = null;
        ServiceTranslationTableRequestParam objServiceTranslationTableRequestParam = null;
        FilterRequestParam filterRequestParam = null;
        SubmitReqDocRequestParam objSubmitReqDocRequestParam = null;
        ConfigurationSettingRequestParam objConfigurationSettingRequestParam = null;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        UserDashBoardResponse objUserDashBoardResponse = null;
        MyDashBoardResponse objMyDashBoardResponse = null;
        MyDashBoardRequestParam objMyDashBoardRequestParam = null;
        MyDashBoardResponseParam objMyDashBoardResponseParam = null;
        LicenseInsuranceRequestParam objLicenseInsuranceRequestParam = null;
        ReportResponseParam objReportResponseParam = null;
        PermissionTableRequestParam objPermissionTableRequestParam = null;
        CreateContractResponseParam objCreateContractResponseParam = null;
        BackGroundCheckMasterRequestParam objBackGroundCheckMasterRequestParam = null;
        SterlingResponseParam objSterlingResponseParam = null;
        APIMasterResponse objAPIMasterResponse = null;
        APIComplianceStatusRequestParam objAPIComplianceStatusRequestParam = null;
        APIMasterResponseParam objAPIMasterResponseParam = null;
        SterlingWithDpResponse objSterlingWithDpResponse = null;
        private bool _disposing = false;
        #endregion


        #region Public Methods
        #region DashBoard
        /// <summary>
        /// Used For get documents from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        public IEnumerable<UserDashBoardResponse> GetDocumentsDetails(int UserId, string type, string CompanyId, string filtervalues)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> documentsdetails = objdecisionPointRepository.GetDocumentsDetails(UserId, type, CompanyId, filtervalues).Select(x => new UserDashBoardResponse { DueDate = x.DueDate, DocType = x.DocType, DocTitle = x.DocTitle, Docfrom = x.Docfrom, tableId = x.tableId, deliveredUserId = x.deliveredUserId, reference = x.reference, CompanyId = x.CompanyId, docSeqno = x.docSeqno, DocId = x.DocId, effectiveDate = x.effectiveDate, reqnewhire = x.reqnewhire, retake = x.retake, introduction = x.introduction, hourofcredit = x.hourofcredit, policyNo = x.policyNo, versionno = x.versionno, Group = x.Group, reqDocType = x.reqDocType, CreatorCompanyid = x.CreatorCompanyid });
                return documentsdetails;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to Delete Video file from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteDocVideo(int id, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DeleteDocVideo(id, type);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to delete Assesments
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DeleteAssesment(int Id)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DeleteAssesment(Id);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  This method used for remove the document of particular user [We just update the deleted status of that message in loval database]
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>int if messages is deleted return 1 else return zero</returns>
        public int RemoveDocument(int Id, int type, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                return objdecisionPointRepository.RemoveDocument(Id, type, companyId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        ///  This method used for remove the document of particular user [We just update the deleted status of that message in loval database]
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>int if messages is deleted return 1 else return zero</returns>
        public int ReactiveDocument(int Id, int type, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.ReactiveDocument(Id, type, companyId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Reactive the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Reactivestaff(int id, string companyid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.Reactivestaff(id, companyid);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// remove the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Removetaff(int id, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.Removetaff(id, companyId);
            }
            catch
            {
                throw;
            }
        }




        /// <summary>
        /// Used For get history for messages/document/courses from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        public IEnumerable<UserDashBoardResponse> GetHistoryDetails(int UserId, string filtervalues, string CompanyId, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> historydetails = objdecisionPointRepository.GetHistoryDetails(UserId, filtervalues, CompanyId, type).Select(x => new UserDashBoardResponse { reqDocType = x.reqDocType, title = x.title, commFromPersonName = x.commFromPersonName, commFromComapnyName = x.commFromComapnyName, receviedDate = x.receviedDate, accecpted = x.accecpted, assesmentStatus = x.assesmentStatus, docTypeId = x.docTypeId, tableId = x.tableId, reference = x.reference, timeSpend = x.timeSpend, deliveredUserId = x.deliveredUserId, CompanyId = x.CompanyId, docSeqno = x.docSeqno, policyNo = x.policyNo, completeDate = x.completeDate, versionno = x.versionno, effectiveDate = x.effectiveDate, archiveDate = x.archiveDate, hourofcredit = x.hourofcredit, MoveInHistory = x.MoveInHistory, Group = x.Group, CreatorCompanyid = x.CreatorCompanyid, status = x.status });
                return historydetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For get history for messages/document/courses from Database as Per particular Company
        /// </summary>
        /// <param name="UserId"></param>
        public IEnumerable<UserDashBoardResponse> GetGlobalLibrary(string CompanyId, string filtervalues)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> historydetails = objdecisionPointRepository.GetGlobalLibrary(CompanyId, filtervalues).Select(x => new UserDashBoardResponse { reqDocType = x.reqDocType, title = x.title, commFromPersonName = x.commFromPersonName, commFromComapnyName = x.commFromComapnyName, receviedDate = x.receviedDate, accecpted = x.accecpted, assesmentStatus = x.assesmentStatus, docTypeId = x.docTypeId, tableId = x.tableId, reference = x.reference, timeSpend = x.timeSpend, deliveredUserId = x.deliveredUserId, CompanyId = x.CompanyId, docSeqno = x.docSeqno, policyNo = x.policyNo, completeDate = x.completeDate, versionno = x.versionno, effectiveDate = x.effectiveDate, archiveDate = x.archiveDate, hourofcredit = x.hourofcredit, NoOfStaff = x.NoOfStaff, NoOfCompStaff = x.NoOfCompStaff, NoOfIc = x.NoOfIc, NoOfDD = x.NoOfDD, NoOfCompDD = x.NoOfCompDD, NoOfVendor = x.NoOfVendor, NoOfCompVendor = x.NoOfCompVendor, NoOfCompIc = x.NoOfCompIc, Group = x.Group, CreatorCompanyid = x.CreatorCompanyid });
                return historydetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For  Get WithdrawnHistory Details for messages/document/courses from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        public IEnumerable<UserDashBoardResponse> GetWithdrawnHistoryDetails(int UserId, string type, string companyId, string filtervalues)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> historydetails = objdecisionPointRepository.GetWithdrawnHistoryDetails(UserId, type, companyId, filtervalues).Select(x => new UserDashBoardResponse { reqDocType = x.reqDocType, title = x.title, commFromPersonName = x.commFromPersonName, commFromComapnyName = x.commFromComapnyName, receviedDate = x.receviedDate, accecpted = x.accecpted, assesmentStatus = x.assesmentStatus, docTypeId = x.docTypeId, tableId = x.tableId, reference = x.reference, timeSpend = x.timeSpend, deliveredUserId = x.deliveredUserId, CompanyId = x.CompanyId, docSeqno = x.docSeqno, policyNo = x.policyNo, completeDate = x.completeDate, versionno = x.versionno, effectiveDate = x.effectiveDate, archiveDate = x.archiveDate, Group = x.Group, CreatorCompanyid = x.CreatorCompanyid, NoOfStaff = x.NoOfStaff, NoOfCompStaff = x.NoOfCompStaff, NoOfIc = x.NoOfIc, NoOfDD = x.NoOfDD, NoOfCompDD = x.NoOfCompDD, NoOfVendor = x.NoOfVendor, NoOfCompVendor = x.NoOfCompVendor, NoOfCompIc = x.NoOfCompIc });
                return historydetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For get account profile details from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        public UserDashBoardResponse GetAccountDetails(int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objUserDashBoardResponseParam = new UserDashBoardResponseParam();
                objUserDashBoardResponseParam = objdecisionPointRepository.GetAccountDetails(UserId);
                objUserDashBoardResponse = new UserDashBoardResponse();
                if (objUserDashBoardResponseParam != null)
                {
                    objUserDashBoardResponse = new UserDashBoardResponse()
                    {
                        fName = objUserDashBoardResponseParam.fName,
                        mName = objUserDashBoardResponseParam.mName,
                        lName = objUserDashBoardResponseParam.lName,
                        NickName = objUserDashBoardResponseParam.NickName,
                        emailId = objUserDashBoardResponseParam.emailId,
                        companyName = objUserDashBoardResponseParam.companyName,
                        officePhone = objUserDashBoardResponseParam.officePhone,
                        directPhone = objUserDashBoardResponseParam.directPhone,
                        UserId = objUserDashBoardResponseParam.UserId,
                        profilephoto = objUserDashBoardResponseParam.profilephoto,
                        companylogo = objUserDashBoardResponseParam.companylogo,
                        CompanyId = objUserDashBoardResponseParam.CompanyId,
                        RegisteredDate = objUserDashBoardResponseParam.RegisteredDate,
                        StreetNumber = objUserDashBoardResponseParam.StreetNumber,
                        StreetName = objUserDashBoardResponseParam.StreetName,
                        Direction = objUserDashBoardResponseParam.Direction,
                        CityName = objUserDashBoardResponseParam.CityName,
                        StateName = objUserDashBoardResponseParam.StateName,
                        ZipCode = objUserDashBoardResponseParam.ZipCode,
                        BioInfo = objUserDashBoardResponseParam.BioInfo,
                        StateId = objUserDashBoardResponseParam.StateId,
                        CoverageAreaStatus = objUserDashBoardResponseParam.CoverageAreaStatus,
                        ServicesStatus = objUserDashBoardResponseParam.ServicesStatus,
                        title = objUserDashBoardResponseParam.title,
                        CreatedBy = objUserDashBoardResponseParam.CreatedBy,
                        BusinessClass = objUserDashBoardResponseParam.BusinessClass,
                        CertificationNumber = objUserDashBoardResponseParam.CertificationNumber,
                        CertificateExpDate = objUserDashBoardResponseParam.CertificateExpDate,
                        CertifyingAgency = objUserDashBoardResponseParam.CertifyingAgency
                    };
                }
                return objUserDashBoardResponse;
            }
            catch
            {
                throw;
            }
        }





        #region Performance
        /// <summary>
        /// This method is used to calculate the Performance of Staff
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public IEnumerable<StaffPerformaceResponse> GetStaffPerformanceList(int userId, string companyId)
        {
            try
            {

                objdecisionPointRepository = new DecisionPointRepository();

                IList<StaffPerformaceResponse> lstStaffPerformance = objdecisionPointRepository.GetStaffPerformanceList(userId, companyId).ToList().Select(q => new StaffPerformaceResponse
                {
                    StaffId = q.StaffId,
                    TotalDocument = q.TotalDocument,
                    StaffName = q.StaffName,
                    CompletedDocument = q.CompletedDocument,
                    // InCompletedDocument = q.InCompletedDocument,
                    Performance = q.Performance,
                    Title = q.Title

                }).ToList();

                return lstStaffPerformance;


            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// This method is used to calculate the Performance of IC
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public IEnumerable<StaffPerformaceResponse> GetICPerformanceList(int userId, string companyId)
        {
            try
            {

                objdecisionPointRepository = new DecisionPointRepository();

                IList<StaffPerformaceResponse> lstICPerformance = objdecisionPointRepository.GetICPerformanceList(userId, companyId).ToList().Select(q => new StaffPerformaceResponse
                {
                    StaffId = q.StaffId,
                    TotalDocument = q.TotalDocument,
                    StaffName = q.StaffName,
                    CompletedDocument = q.CompletedDocument,
                    // InCompletedDocument = q.InCompletedDocument,
                    Performance = q.Performance,
                    //  Title = q.Title

                }).ToList();

                return lstICPerformance;


            }
            catch
            {

                throw;
            }

        }
        /// <summary>
        /// Used to get IC payments mode from database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ICPaymentModeResponse> GetICPaymentMode()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ICPaymentModeResponse> objICPayment = objdecisionPointRepository.GetICPaymentMode().ToList().Select(x => new ICPaymentModeResponse { PaymentId = x.PaymentId, PaymentMode = x.PaymentMode });
                return objICPayment;
            }
            catch
            {
                throw;
            }

        }
        #endregion




        /// <summary>
        /// Used to search in history
        /// </summary>
        /// <param name="term"></param>
        /// <param name="UserId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<UserDashBoardResponse> Search(string term, int UserId, string type, int searchtype)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<UserDashBoardResponse> serach = objdecisionPointRepository.Serach(term, UserId, type, searchtype).Select(x => new UserDashBoardResponse { serachByTitle = x.serachByTitle, serachByFrom = x.serachByFrom, serachByVType = x.serachByVtype }).ToList();
                return serach;
            }
            catch
            {
                throw;
            }
        }
        ///// <summary>
        ///// get roles from role master
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<RoleResponse> GetUserRoles(int UserId)
        //{
        //    try
        //    {
        //        objdecisionPointRepository = new DecisionPointRepository();
        //        IEnumerable<RoleResponse> rolelist = objdecisionPointRepository.GetUserRoles(UserId).Select(x => new RoleResponse { RoleId = x.RoleId, RoleName = x.RoleName });
        //        return rolelist;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        /// <summary>
        /// get roles from title master
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyDashBoardResponse> GetUserTitle(int UserId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> titlelist = objdecisionPointRepository.GetUserTitle(UserId, companyId).Select(x => new CompanyDashBoardResponse { id = x.id, titleName = x.titleName });
                return titlelist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from service master
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUserServices(int UserId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<string> servicelist = objdecisionPointRepository.GetUserServices(UserId, companyId);
                return servicelist;
            }
            catch
            {
                throw;
            }
        }

        /// get roles from clients master
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUserClients(int UserId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<string> clientlist = objdecisionPointRepository.GetUserClients(UserId, companyId);
                return clientlist;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// get Skill from skill master
        /// </summary>
        /// <returns></returns>
        public IList<string> GetUserSkills(int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<string> skilllist = objdecisionPointRepository.GetUserSkills(UserId);
                return skilllist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from state list from database
        /// </summary>
        /// <returns></returns>
        public IList<StateRespone> GetStateList(int UserId, string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<StateRespone> state = objdecisionPointRepository.GetStateList(UserId, companyId, type).Select(x => new StateRespone { SateName = x.SateName, Abbrebiation = x.Abbrebiation, StateType = x.StateType });
                return state.ToList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get paretnt user id
        /// </summary>
        /// <returns></returns>
        public int GetParentUserId(string companyid, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int ParentUserId = objdecisionPointRepository.GetParentUserId(companyid, type);
                return ParentUserId;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get paretnt user type
        /// </summary>
        /// <returns></returns>
        public string GetParentUserType(int companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string ParentUserType = objdecisionPointRepository.GetParentUserType(companyId);
                return ParentUserType;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from State List as per zip Code
        /// </summary>
        /// <returns></returns>
        public IList<StateRespone> GetStateListFromZip(int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<StateRespone> state = objdecisionPointRepository.GetStateListFromZip(UserId).Select(x => new StateRespone { SateName = x.SateName, Abbrebiation = x.Abbrebiation });
                return state.ToList();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from state list as per state name
        /// </summary>
        /// <returns></returns>
        public IList<CityResponse> GetCityListByStateName(string statename, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CityResponse> citylist = objdecisionPointRepository.GetCityListByStateName(statename, UserId).Select(x => new CityResponse { CityId = x.CityId, CityName = x.CityName }).ToList();
                return citylist;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get roles from county name as per state name
        /// </summary>
        /// <returns></returns>
        public IList<CountyResponse> GetCountyListByStateName(string statename, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CountyResponse> countylist = objdecisionPointRepository.GetCountyListByStateName(statename, UserId).Select(x => new CountyResponse { CountyId = x.CountyId, CountyName = x.CountyName, OptionsVal = x.OptionsVal }).ToList();
                return countylist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from Zip as per state name
        /// </summary>
        /// <returns></returns>
        public IList<ZipResponse> GetzipListByStateName(string statename, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ZipResponse> ziplist = objdecisionPointRepository.GetZipListByStateName(statename, UserId).Select(x => new ZipResponse { ZipId = x.ZipId, ZipCode = x.ZipCode }).ToList();
                return ziplist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get zip from selected city
        /// </summary>
        /// <returns></returns>
        public IList<ZipResponse> GetzipListBySelectedCity(string CityName, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ZipResponse> ziplist = objdecisionPointRepository.GetZipListBySelectedCity(UserId, CityName).Select(x => new ZipResponse { ZipId = x.ZipId, ZipCode = x.ZipCode, OptionsVal = x.OptionsVal }).ToList();
                return ziplist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to the city list from database
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<CityResponse> GetCityList(int UserId, string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CityResponse> citylist = objdecisionPointRepository.GetCityList(UserId, companyId, type).Select(x => new CityResponse { CityName = x.CityName, OptionsVal = x.OptionsVal }).ToList();
                return citylist;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get roles from county list
        /// </summary>
        /// <returns></returns>
        public IList<CountyResponse> GetCountyList(int UserId, string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CountyResponse> countylist = objdecisionPointRepository.GetCountyList(UserId, companyId, type).Select(x => new CountyResponse { CountyName = x.CountyName, OptionsVal = x.OptionsVal, CountyType = x.CountyType }).ToList();
                return countylist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get roles from zip list
        /// </summary>
        /// <returns></returns>
        public IList<ZipResponse> GetzipList(int UserId, string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ZipResponse> ziplist = objdecisionPointRepository.GetZipList(UserId, companyId, type).Select(x => new ZipResponse { ZipCode = x.ZipCode, OptionsVal = x.OptionsVal }).ToList();
                return ziplist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to validate coverage area section
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int CoverageAreaDoesNotApply(int userId, string companyId, int type, string coverageAreaStatus)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int result = objdecisionPointRepository.CoverageAreaDoesNotApply(userId, companyId, type, coverageAreaStatus);
                return result;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get that coverage area apply on particular company or not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetCAOrServiceDoesNotApply(int userId, string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string result = objdecisionPointRepository.GetCAOrServiceDoesNotApply(userId, companyId, type);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public int UpdateEmailId(string emailId, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int result = objdecisionPointRepository.UpdateEmailId(emailId, userId);
                return result;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for update my profile
        /// </summary>
        /// <param name="objUserDashBoardRequestParam"></param>
        /// <returns></returns>
        public int Updatemyprofile(UserDashBoardRequest objUserDashBoardRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                if (!string.IsNullOrEmpty(objUserDashBoardRequest.NewPwd))
                {
                    objUserDashBoardRequest.NewPwd = businessCryptography.base64Encode(objUserDashBoardRequest.NewPwd);
                }
                if (!objUserDashBoardRequest.Equals(null))
                {
                    objUserDashBoardRequestParam = new UserDashBoardRequestParam()
                    {
                        fName = objUserDashBoardRequest.FName,
                        mName = objUserDashBoardRequest.MName,
                        lName = objUserDashBoardRequest.LName,
                        NickName = objUserDashBoardRequest.NickName,
                        emailId = objUserDashBoardRequest.EmailId,
                        officePhone = objUserDashBoardRequest.OfficePhone,
                        directPhone = objUserDashBoardRequest.DirectPhone,
                        profilephoto = objUserDashBoardRequest.ProfilePhoto,
                        UserId = objUserDashBoardRequest.UserId,
                        modifiedby = objUserDashBoardRequest.ModifiedBy,
                        companyName = objUserDashBoardRequest.CompanyName,
                        serviceId = objUserDashBoardRequest.ServiceId,
                        CompanyCode = objUserDashBoardRequest.CompanyCode,
                        clientId = objUserDashBoardRequest.ClientId,
                        newpwd = objUserDashBoardRequest.NewPwd,
                        StreetName = objUserDashBoardRequest.StreetName,
                        StreetNumber = objUserDashBoardRequest.StreetNumber,
                        Direction = objUserDashBoardRequest.Direction,
                        CityName = objUserDashBoardRequest.CityName,
                        ZipCode = objUserDashBoardRequest.ZipCode,
                        StateId = objUserDashBoardRequest.StateId,
                        Type = objUserDashBoardRequest.Type,
                        CertificationNumber = objUserDashBoardRequest.CerificationNumber,
                        CertificateExpDate = objUserDashBoardRequest.CertificateExpDate,
                        CertifyingAgency = objUserDashBoardRequest.CertifyingAgency,
                        BusinessClass = objUserDashBoardRequest.BusinessClass,
                    };

                }
                return objdecisionPointRepository.Updatemyprofile(objUserDashBoardRequestParam, type);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<InternalstaffResponse> GetInternalstaffdetail(FilterRequest filterRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                filterRequestParam = new FilterRequestParam()
                {
                    CompanyId = filterRequest.CompanyId,
                    type = filterRequest.type,
                    DocId = filterRequest.DocId
                };
                IEnumerable<InternalstaffResponse> internalStaffdetail = objdecisionPointRepository.GetInternalstaffdetail(filterRequestParam).Select(x => new InternalstaffResponse
                {
                    fname = x.fname,
                    lname = x.lname,
                    emailId = x.emailId,
                    phone = x.phone,
                    title = x.title,
                    Id = x.Id,
                    IsActive = x.IsActive,
                    service = x.service,
                    zipcode = x.zipcode,
                    businessName = x.businessName,
                    invitationStatus = x.invitationStatus,
                    companyId = x.companyId,
                    UserType = x.UserType,
                    LastInviteMailDate = x.LastInviteMailDate
                });
                return internalStaffdetail;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Save Filter Value
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public int SaveCommFilterValue(FilterRequest objFilterRequest, int docId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                filterRequestParam = new FilterRequestParam()
                {
                    CompanyId = objFilterRequest.CompanyId,
                    filtertype = objFilterRequest.filtertype,
                    typefilter = objFilterRequest.typefilter,
                };
                return objdecisionPointRepository.SaveCommFilterValue(filterRequestParam, docId);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<ICResponse> GetICdetail(FilterRequest filterRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                filterRequestParam = new FilterRequestParam()
                {
                    CompanyId = filterRequest.CompanyId,
                    type = filterRequest.type,
                    DocId = filterRequest.DocId,
                    servicefilter = filterRequest.servicefilter,
                    titlefilter = filterRequest.titlefilter,
                    locationfilter = filterRequest.locationfilter
                };
                IEnumerable<ICResponse> ICdetail = objdecisionPointRepository.GetICdetail(filterRequestParam).Select(x => new ICResponse
                {
                    fname = x.Fname,
                    lname = x.Lname,
                    emailId = x.EmailId,
                    phone = x.Phone,
                    IsActive = x.IsActive,
                    service = x.Service,
                    zipcode = x.Zipcode,
                    businessName = x.BusinessName,
                    Id = x.Id,
                    companyid = x.CompanyId,
                    InvitationStatus = x.InvitationStatus,
                    VTId = x.VTId,
                    VType = x.VType,
                    LastInviteMailDate = x.LastInviteMailDate
                });
                return ICdetail;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For GetICNonClientDetails
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<VendorsList> GetICNonClientDetails(string companyId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<VendorsList> nonClientDetails = objdecisionPointRepository.GetICNonClientDetails(companyId, type).Select(x => new VendorsList { contact = x.Contact, emailId = x.emailId, phone = x.phone, vendor = x.Vendor, Id = x.Id, CompanyId = x.companyId, invitationStatus = x.invitationStatus, VendorType = x.VendorType });
                return nonClientDetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<VendorResponse> Getvendordetail(int UserId)
        {
            try
            {

                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<VendorResponse> vendordetail = null;// objdecisionPointRepository.Getvendordetail(UserId).Select(x => new VendorResponse { name = x.name, noOfStaff = x.noOfStaff, noOfVendors = x.noOfVendors });
                return vendordetail;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to update vendor type for IC
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titleId"></param>
        /// <param name="permissionId"></param>
        /// <returns></returns>
        public int UpdateVendorType(int userId, string userCompanyId, string creatorCompanyId, int vendorTypeId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateVendorType(userId, userCompanyId, creatorCompanyId, vendorTypeId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// method to get vendor list
        /// </summary>
        /// <param name="companyUserID"></param>
        /// <returns></returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>30 Dec 2013</createdDate>
        public IEnumerable<VendorsList> GetVendorList(FilterRequest filterRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                filterRequestParam = new FilterRequestParam()
                {
                    CompanyId = filterRequest.CompanyId,
                    isActive = filterRequest.isActive,
                    uType = filterRequest.uType,
                    filtertype = filterRequest.filtertype,
                    DocId = filterRequest.DocId,
                    servicefilter = filterRequest.servicefilter,
                    titlefilter = filterRequest.titlefilter,
                    locationfilter = filterRequest.locationfilter
                };
                IEnumerable<VendorsList> lstInternalstaffResponseParam = objdecisionPointRepository.GetVendorList(filterRequestParam).Select(x => new VendorsList
                {
                    Id = x.Id,
                    contact = x.Contact,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    vendor = x.Vendor,
                    emailId = x.emailId,
                    phone = x.phone,
                    service = x.service,
                    title = x.title,
                    stateAbbre = x.stateAbbre,
                    zipCode = x.zipCode,
                    CompanyId = x.companyId,
                    DocFlow = x.DocFlow,
                    DocFTblId = x.DocFTblId,
                    invitationStatus = x.invitationStatus,
                    LastInviteMailDate = x.LastInviteMailDate,
                    UserType = x.UserType,
                    IsNonMember = x.IsNonMember,
                    Status = x.Status
                }).ToList();
                return lstInternalstaffResponseParam;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// method to get Decendant's  list of vendor and client
        /// </summary>
        /// <param name="companyUserID"></param>
        /// <returns></returns>
        /// <createdBy>Mamta Gupta</createdBy>
        /// <createdDate>19 Mar 2014</createdDate>
        public IEnumerable<VendorsList> GetDecentantVendorList(FilterRequest filterRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                filterRequestParam = new FilterRequestParam()
                {
                    UserId = filterRequest.UserId,
                    isActive = filterRequest.isActive,
                    uType = filterRequest.uType,
                    filtertype = filterRequest.filtertype,
                    DocId = filterRequest.DocId,
                    servicefilter = filterRequest.servicefilter,
                    titlefilter = filterRequest.titlefilter,
                    locationfilter = filterRequest.locationfilter
                };
                IEnumerable<VendorsList> DecndantList = objdecisionPointRepository.GetDecentantVendorList(filterRequestParam).Select(x => new VendorsList { Id = x.Id, contact = x.Contact, vendor = x.Vendor, emailId = x.emailId, phone = x.phone, CompanyId = x.companyId }).ToList();
                return DecndantList;
            }
            catch
            {
                throw;

            }
            finally
            {

            }

        }

        /// <summary>
        /// reactive vendor by their id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 2 2013</createdDate>
        public int ReactiveVendor(int id, string companyId)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.ReactiveVendor(id, companyId);
            return response;
        }
        /// <summary>
        /// reactive vendor by their id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 2 2013</createdDate>
        public int ReactiveBusinessPartners(int id)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.ReactiveBusinessPartners(id);
            return response;
        }
        /// <summary>
        /// remove vendor by their id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 2 2013</createdDate>
        public int RemoveVendor(int id, string companyId)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.RemoveVendor(id, companyId);
            return response; ;
        }

        /// <summary>
        /// remove vendor by their id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Jan 2 2013</createdDate>
        public int RemoveBusinessPartners(int id)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.RemoveBusinessPartners(id);
            return response; ;
        }


        #endregion

        #region Admin SuperAdmin Setting
        /// <summary>
        /// Used to get company name
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>string</returns>
        public string GetCompanyName(string companyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string cmpName = decisionPointRepository.GetCompanyName(companyId);
                return cmpName;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get the title of particular user
        /// </summary>
        /// <param name="title">title</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int AddTitle(string title, string CompanyId, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddTitle(title, CompanyId, UserId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// This method used for add client
        /// </summary>
        /// <param name="title">title</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int AddClient(string title, int UserId, string CompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddClient(title, UserId, CompanyId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// This method used for add client
        /// </summary>
        /// <param name="title">title</param>
        /// <createdby>Bobis & sumit</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>int</returns>
        public int SaveServiceTranslation(ServiceTranslationTableRequest objServiceTranslationTableRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objServiceTranslationTableRequestParam = new ServiceTranslationTableRequestParam();
                objServiceTranslationTableRequestParam.ChildCompanyId = objServiceTranslationTableRequest.ChildCompanyId;
                objServiceTranslationTableRequestParam.ParentCompanyId = objServiceTranslationTableRequest.ParentCompanyId;
                objServiceTranslationTableRequestParam.MappedServices = objServiceTranslationTableRequest.MappedServices;
                objServiceTranslationTableRequestParam.CreatedByid = objServiceTranslationTableRequest.CreatedByid;
                return objdecisionPointRepository.SaveServiceTranslation(objServiceTranslationTableRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to get client list
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CompanyDashBoardResponse> GetClient(string type, string ID)
        {
            try
            {
                //serialNumber=x.serialNumber
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> clientdetails = objdecisionPointRepository.GetClient(type, ID).Select(x => new CompanyDashBoardResponse { clientName = x.clientName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return clientdetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to disable or enable any client
        /// </summary>
        /// <param name="clientId">clientId</param>
        /// <param name="isActive">isActive</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int DisaEnaClient(int clientId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaClient(clientId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to update particular client
        /// </summary>
        /// <param name="clientId">clientId</param>
        /// <param name="titlename">titlename</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>13 dec 2013</createdDate>
        /// <returns>int</returns>
        public int UpdateClient(int clientId, string titlename)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateClient(clientId, titlename);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get title details of particular company
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="ID">ID</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CompanyDashBoardResponse> GetTitle(string type, string ID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> titlesdetails = objdecisionPointRepository.GetTitle(type, ID).Select(x => new CompanyDashBoardResponse { titleName = x.titleName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return titlesdetails;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to get filteration selection of particular company
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IList</returns>
        public IList<SaveCommFilterResponse> GetCommFilterDetails(int docId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<SaveCommFilterResponse> commfilterdetails = objdecisionPointRepository.GetCommFilterDetails(docId, companyId).Select(x => new SaveCommFilterResponse { FilterValue = x.FilterValue, FilterType = x.FilterType, CoverageAreaVal = x.CoverageAreaVal }).ToList();
                return commfilterdetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for disable and enable the title
        /// </summary>
        /// <param name="titleId">titleId</param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int DisaEnaTitle(int titleId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaTitle(titleId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="titleId"titleId></param>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int UpdateTitle(int titleId, string titlename, String CompanyID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateTitle(titleId, titlename, CompanyID);

            }
            catch
            {
                throw;
            }
        }
        ///// <summary>
        ///// get roles from role master
        ///// </summary>
        ///// <createdby>Bobis</createdby>
        /////  <createdDate>10 dec 2013</createdDate>
        ///// <returns>IEnumerable</returns>
        //public IEnumerable<RoleResponse> GetRoles()
        //{
        //    try
        //    {
        //        objdecisionPointRepository = new DecisionPointRepository();
        //        IEnumerable<RoleResponse> roles = objdecisionPointRepository.GetRoles().Select(x => new RoleResponse { RoleId = x.RoleId, RoleName = x.RoleName });
        //        return roles;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        /// <summary>
        /// get flows from flow master
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<RoleResponse> GetFlow()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<RoleResponse> flow = objdecisionPointRepository.GetFlow().Select(x => new RoleResponse { flowId = x.flowId, flowName = x.flowName });
                return flow;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get doc flow from Docflow master
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<RoleResponse> GetDocFlow()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<RoleResponse> flow = objdecisionPointRepository.GetDocFlow().Select(x => new RoleResponse { flowId = x.flowId, flowName = x.flowName });
                return flow;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get services from service master
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CompanyDashBoardResponse> GetServices(string type, string ID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> service = objdecisionPointRepository.GetServices(type, ID).Select(x => new CompanyDashBoardResponse { id = x.id, serviceName = x.serviceName, isDeleted = x.isDeleted, isActive = x.isActive });
                return service;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get the services from lcal database
        /// </summary>
        /// <returns>retrun the services details in ienumberable form</returns>
        ///<param name="ID">string</param>
        ///<param name="type">string</param>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 14 2013</createdDate>
        public IEnumerable<CompanyDashBoardResponse> GetServicesOfNewHired(int ID, string companyID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> service = objdecisionPointRepository.GetServicesOfNewHired(ID, companyID).Select(x => new CompanyDashBoardResponse { id = x.id, serviceName = x.serviceName, isDeleted = x.isDeleted, isActive = x.isActive });
                return service;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get mapped service details from database
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CompanyDashBoardResponse> GetMappdedServices(string pCompanyId, string cCompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> mappedservice = objdecisionPointRepository.GetMappdedServices(pCompanyId, cCompanyId).Select(x => new CompanyDashBoardResponse { ParentserviceId = x.ParentserviceId, ChildserviceId = x.ChildserviceId, Childservicename = x.Childservicename });
                return mappedservice;
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Used to send invitaion
        /// </summary>
        /// <param name="objCompanyDashBoardRequest">CompanyDashBoardRequest class</param>
        /// <createdby>Bobis & sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>string</returns>
        public string SendInvitation(CompanyDashBoardRequest objCompanyDashBoardRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objCompanyDashBoardRequestParam = new CompanyDashBoardRequestParam()
                {
                    fName = objCompanyDashBoardRequest.fName,
                    lName = objCompanyDashBoardRequest.lName,
                    emailId = objCompanyDashBoardRequest.emailId,
                    titleId = objCompanyDashBoardRequest.titleId,
                    password = objCompanyDashBoardRequest.password,
                    UserId = objCompanyDashBoardRequest.UserId,
                    companyName = objCompanyDashBoardRequest.companyName,
                    companyId = objCompanyDashBoardRequest.companyId,
                    CompId = objCompanyDashBoardRequest.CompId,
                    flowId = objCompanyDashBoardRequest.flowId,
                    docflowId = objCompanyDashBoardRequest.docflowId,
                    PaymentType = objCompanyDashBoardRequest.PaymentType,
                    vendortypeId = objCompanyDashBoardRequest.vendorTypeId,
                    IsBackgroundCheck = objCompanyDashBoardRequest.IsBackgroundCheck,
                    IsMailSent = objCompanyDashBoardRequest.IsMailSent,
                    AllowToView = objCompanyDashBoardRequest.AllowToView
                };

                return objdecisionPointRepository.SendInvitation(objCompanyDashBoardRequestParam, type);

            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// get company Id
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>unique company Id</returns>
        public string GenerateCompanyId()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string compId = objdecisionPointRepository.GenrateCompanyId();
                return compId;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get company Id 
        /// </summary>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>list of company Id</returns>
        public bool CheckExistingEmailId(string emailId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                bool IsExist = objdecisionPointRepository.CheckExistingEmailId(emailId);
                return IsExist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get company Id 
        /// </summary>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>list of company Id</returns>
        public IEnumerable<CompanyIdResponse> getCompanyId()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyIdResponse> companyId = objdecisionPointRepository.getCompanyId().Select(x => new CompanyIdResponse { Id = x.Id, CompanyId = x.CompanyId, UserId = x.UserId });
                return companyId;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get admin profile details
        /// </summary>
        /// <param name="CompanyId">CompanyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<AdminProfileResponse> GetAdminProfile(string CompanyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                IEnumerable<AdminProfileResponse> admin = decisionPointRepository.GetAdminProfile(CompanyId).Select(y => new AdminProfileResponse
                {
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    MiddleName = y.MiddleName,
                    CellNumber = y.CellNumber,
                    OfficePhone = y.OfficePhone,
                    Password = y.Password,
                    Email = y.Email,
                    SecurityAnswer1 = y.SecurityAnswer1,
                    SecurityAnswer2 = y.SecurityAnswer2,
                    SecurityAnswer3 = y.SecurityAnswer3,
                });
                return admin;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// save state mapping
        /// </summary>
        /// <param name="stateRequestParam">contains state name and abbreviation</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>result save or not</returns>
        public int SaveStateMapping(StateRequest stateRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                StateRequestParam stateRequestParam = new StateRequestParam
                {
                    StateName = stateRequest.StateName,
                    UserId = stateRequest.UserId,
                    CompanyId = stateRequest.CompanyId,
                    ModifiedBy = stateRequest.ModifiedBy,
                    CoverageAreaFor = stateRequest.CoverageAreaFor
                };

                return objdecisionPointRepository.SaveStateMapping(stateRequestParam, type);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves county selected by a company
        /// </summary>
        /// <param name="countyRequestParam">county name and user id</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns> result saved or not</returns>
        public int SaveCountyMapping(CountyRequest countyRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CountyRequestParam countyRequestParam = new CountyRequestParam
                {
                    CountyName = countyRequest.CountyName,
                    UserId = countyRequest.UserId,
                    CompanyId = countyRequest.CompanyId,
                    ModifiedBy = countyRequest.ModifiedBy,
                    CoverageAreaFor = countyRequest.CoverageAreaFor
                };

                return objdecisionPointRepository.SaveCountyMapping(countyRequestParam, type);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves city selected by a company in dp_citymapping
        /// </summary>
        /// <param name="cityRequestParam">city details</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>int type result saved or not</returns>
        public int SaveCityMapping(CityRequest cityRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CityRequestParam cityRequestParam = new CityRequestParam
                {
                    CityName = cityRequest.CityName,
                    UserId = cityRequest.UserId,
                    CompanyId = cityRequest.CompanyId,
                    ModifiedBy = cityRequest.ModifiedBy,
                    CoverageAreaFor = cityRequest.CoverageAreaFor

                };

                return objdecisionPointRepository.SaveCityMapping(cityRequestParam, type);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// saves selected zip by a company
        /// </summary>
        /// <param name="zipRequestParam">zip codes</param>
        /// <param name="type">type</param>
        /// <param name="isdelete">isdelete</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>int type result saved or not</returns>
        public int SaveZipMapping(ZipRequest zipRequest, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                ZipRequestParam zipRequestParam = new ZipRequestParam
                {
                    ZipCode = zipRequest.ZipCode,
                    UserId = zipRequest.UserId,
                    CompanyId = zipRequest.CompanyId,
                    CoverageAreaFor = zipRequest.CoverageAreaFor,
                    ModifiedBy = zipRequest.ModifiedBy
                };

                return objdecisionPointRepository.SaveZipMapping(zipRequestParam, type);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get company name
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>string</returns>
        public string GetUserNameFromUserId(string companyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string cmpName = decisionPointRepository.GetUserNameFromUserId(companyId);
                return cmpName;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get company name
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>string</returns>
        public string GetCompanyNameByUserType(string companyId, string Usertype)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string cmpName = decisionPointRepository.GetCompanyNameByUserType(companyId, Usertype);
                return cmpName;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to get Company Id of particular staff
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>int</returns>
        public int GetStaffCompanyId(string companyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                int cmpId = decisionPointRepository.GetStaffCompanyId(companyId);
                return cmpId;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get email id of particular admin
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>string</returns>
        public int GetAdminUserId(string companyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                int cmpEmail = decisionPointRepository.GetAdminUserId(companyId);
                return cmpEmail;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get invitaion flow of user
        /// </summary>
        /// <param name="UserId">UserId</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 feb 2014</createdDate>
        /// <returns>string</returns>
        public string GetInvitationFlow(string UserId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string cmpEmail = decisionPointRepository.GetInvitationFlow(UserId);
                return cmpEmail;
            }
            catch
            {
                throw;
            }
        }
        #region Service
        /// <summary>
        /// Get Service Details
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyDashBoardResponse> GetService(string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> servicessdetails = objdecisionPointRepository.GetService(type).Select(x => new CompanyDashBoardResponse { serviceName = x.serviceName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return servicessdetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        ///To Add New Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public int AddService(string service, int UserId, string CompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddService(service, UserId, CompanyId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for disable and enable the service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public int DisaEnaService(int serviceId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaService(serviceId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public int UpdateService(int serviceId, string serviceName, String companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateService(serviceId, serviceName, companyId);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for save the IC services 
        /// </summary>
        /// <returns>int</returns>
        public int SaveICServices(string servicesid, int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveICServices(servicesid, userId, companyId);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region References
        /// <summary>
        /// used for added the new refrence name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        public int AddReference(string referenceName, int UserId, string CompanyId, int groupId)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddReference(referenceName, UserId, CompanyId, groupId);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used for get the title details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        public IEnumerable<CompanyDashBoardResponse> GetReference(string type, string ID, string dgroup)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetReference(type, ID, dgroup).Select(x => new CompanyDashBoardResponse { referenceName = x.referenceName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id, groupName = x.groupName }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for get the title details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        public IEnumerable<CompanyDashBoardResponse> GetUserReference(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetUserReference(companyId).Select(x => new CompanyDashBoardResponse { referenceName = x.referenceName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for disable and enable the title
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="isActive"></param>
        /// <returns>return one if title is disable else retrun zero</returns
        public int DisaEnaReference(int referenceId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaReference(referenceId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="titlename"></param>
        /// <returns>return one if client is update else retrun zero</returns>
        public int UpdateReference(int referenceId, string referenceName, string CompanyId, int groupId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateReference(referenceId, referenceName, CompanyId, groupId);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Set Up training
        /// <summary>
        /// Used to save the deatil for setup training
        /// </summary>
        /// <param name="doctitle"></param>
        /// <param name="duedate"></param>
        /// <param name="intro"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        public string SaveCommunication(CommunicationBasicDetailsRequest objCommunicationBasicDetailsRequest, string type, string docid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CommunicationBasicDetailsRequestParam objCommunicationBasicDetailsRequestParam = new CommunicationBasicDetailsRequestParam()
                {
                    DocTitle = objCommunicationBasicDetailsRequest.DocTitle,
                    DocType = objCommunicationBasicDetailsRequest.DocType,
                    DueDate = objCommunicationBasicDetailsRequest.DueDate,
                    Instruction = objCommunicationBasicDetailsRequest.Introduction,
                    UserId = objCommunicationBasicDetailsRequest.UserId,
                    Reference = objCommunicationBasicDetailsRequest.Reference,
                    CompanyId = objCommunicationBasicDetailsRequest.CompanyId,
                    RequHirestaff = objCommunicationBasicDetailsRequest.RequHirestaff,
                    RequHireic = objCommunicationBasicDetailsRequest.RequHireic,
                    RequHirevendor = objCommunicationBasicDetailsRequest.RequHirevendor,
                    retake = objCommunicationBasicDetailsRequest.retake,
                    HOC = objCommunicationBasicDetailsRequest.HOC,
                    DaysToComplete = objCommunicationBasicDetailsRequest.DaysToComplete,
                    EffectiveDate = objCommunicationBasicDetailsRequest.EffectiveDate,
                    Group = objCommunicationBasicDetailsRequest.Group,
                    DocTitles = objCommunicationBasicDetailsRequest.DocTitles,
                    VideoTitles = objCommunicationBasicDetailsRequest.VideoTitles,
                    ScormTitles = objCommunicationBasicDetailsRequest.ScormTitles,
                    Onstaging = objCommunicationBasicDetailsRequest.Onstaging,
                    OnLib = objCommunicationBasicDetailsRequest.OnLib,
                    IsEmpReq = objCommunicationBasicDetailsRequest.IsEmpReq

                };
                //Saved basic details of communication
                return objdecisionPointRepository.SaveCommunication(objCommunicationBasicDetailsRequestParam, type, docid);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to save the likns inculded in setup traininhs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        public int SaveCommLinks(int docId, string linkValue, string type, int linkid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveCommLinks(docId, linkValue, type, linkid);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to save the Reqiured action of communication
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        public int SaveCommReqActions(int docId, string reqActionval, string type, int reqactionid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveCommReqActions(docId, reqActionval, type, reqactionid);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to save the comm contents 
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="contentval"></param>
        /// <returns></returns>
        public int SaveCommContents(CommContentRequest commContentRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CommContentRequestParam commContentRequestParam = new CommContentRequestParam()
                {
                    docId = commContentRequest.docId,
                    files = commContentRequest.files,
                    filetype = commContentRequest.filetype,
                    userId = commContentRequest.userId,
                    type = commContentRequest.type,
                    title = commContentRequest.title,
                    scormname = commContentRequest.scormname
                };
                return objdecisionPointRepository.SaveCommContents(commContentRequestParam);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to save the deatil for setup training
        /// </summary>
        /// <param name="doctitle"></param>
        /// <param name="duedate"></param>
        /// <param name="intro"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        public int SaveCommQuesAns(CommunicationAssessmentRequest objCommunicationAssessmentRequest)
        {
            try
            {
                CommunicationAssessmentResponseParam objCommunicationAssessmentResponseParam = new CommunicationAssessmentResponseParam();
                objCommunicationAssessmentResponseParam.docId = objCommunicationAssessmentRequest.docId;
                objCommunicationAssessmentResponseParam.type = objCommunicationAssessmentRequest.type;
                objCommunicationAssessmentResponseParam.question = objCommunicationAssessmentRequest.question;
                objCommunicationAssessmentResponseParam.savestatus = objCommunicationAssessmentRequest.savestatus;
                objCommunicationAssessmentResponseParam.assessmentid = objCommunicationAssessmentRequest.assessmentid;
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveCommQuesAns(objCommunicationAssessmentResponseParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to publish the document on staff and vendors
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="files"></param>
        /// <param name="filetype"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int PublishComm(publishCommRequest objPublishComm, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                publishCommRequestParam objpublishCommRequestParam = new publishCommRequestParam();
                objpublishCommRequestParam.totalStaff = objPublishComm.totalStaff;
                objpublishCommRequestParam.totalClient = objPublishComm.totalClient;
                objpublishCommRequestParam.totalVendor = objPublishComm.totalVendor;
                objpublishCommRequestParam.totalIC = objPublishComm.totalIC;
                objpublishCommRequestParam.docId = objPublishComm.docId;
                objpublishCommRequestParam.dueDate = objPublishComm.dueDate;
                objpublishCommRequestParam.Doctype = objPublishComm.Doctype;
                objpublishCommRequestParam.versionno = objPublishComm.versionno;
                objpublishCommRequestParam.CompanyId = objPublishComm.Companyid;
                return objdecisionPointRepository.PublishComm(objpublishCommRequestParam, UserId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to save the deatil for setup training
        /// </summary>
        /// <param name="doctitle"></param>
        /// <param name="duedate"></param>
        /// <param name="intro"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        public int SaveCommTestRules(CommTestRulesRequest objCommTestRulesRequest, string type)
        {
            try
            {

                CommTestRulesRequestParam objCommTestRulesRequestParam = new CommTestRulesRequestParam();
                objCommTestRulesRequestParam.RandQues = objCommTestRulesRequest.RandQues;
                objCommTestRulesRequestParam.RandAns = objCommTestRulesRequest.RandAns;
                objCommTestRulesRequestParam.ReqReTest = objCommTestRulesRequest.ReqReTest;
                objCommTestRulesRequestParam.PassingScore = objCommTestRulesRequest.PassingScore;
                objCommTestRulesRequestParam.ShowWrongeAns = objCommTestRulesRequest.ShowWrongeAns;
                objCommTestRulesRequestParam.Attempts = objCommTestRulesRequest.Attempts;
                objCommTestRulesRequestParam.Instruction = objCommTestRulesRequest.Instruction;
                objCommTestRulesRequestParam.docId = objCommTestRulesRequest.docId;
                objCommTestRulesRequestParam.Testruleid = objCommTestRulesRequest.Testruleid;
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveCommTestRules(objCommTestRulesRequestParam, type);
            }
            catch
            {
                throw;
            }
        }


        public IList<CommDetailsResponse> GetCommDetails(int DocId, string type)
        {
            IList<CommDetailsResponse> finalList = null;
            objdecisionPointRepository = new DecisionPointRepository();
            try
            {
                //get the communication basis details
                if (type.Equals(Shared.DocumentDetail))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        DocTitle = x.DocTitle,
                        DocType = x.DocType,
                        DueDate = x.DueDate,
                        Reference = x.Reference,
                        Instruction = x.Instruction,
                        Reqnewhirestaff = x.Reqnewhirestaff,
                        Reqnewhireic = x.Reqnewhireic,
                        Reqnewhirevendor = x.Reqnewhirevendor,
                        Retake = x.Retake,
                        Effectivedate = x.Effectivedate,
                        policyno = x.policyno,
                        versionno = x.versionno,
                        HOC = x.HOC,
                        DaysOfCompletion = x.DaysOfCompletion,
                        Group = x.Group,
                        DocTitles = x.DocTitles,
                        VideoTitles = x.VideoTitles,
                        ScormTitles = x.ScormTitles
                    }).ToList();
                    finalList = commddetail;
                }
                //get the links of particular communication
                if (type.Equals(Shared.Links))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        LinkURl = x.LinkURl,
                        LinkId = x.LinkId
                    }).ToList();
                    finalList = commddetail;
                }
                //get the content of particular communication
                if (type.Equals(Shared.Content))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        FileLoc = x.FileLoc,
                        Filetitle = x.Filetitle,
                        Filetype = x.Filetype,
                        Contentid = x.Contentid
                    }).ToList();
                    finalList = commddetail;
                }
                //get the reqiured action details
                if (type.Equals(Shared.Reqaction))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        Condition = x.Condition,
                        Reqactionid = x.Reqactionid
                    }).ToList();
                    finalList = commddetail;
                }
                //get the test rules details of particlar communication
                if (type.Equals(Shared.TestRule))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        RandQues = x.RandQues,
                        RandAns = x.RandAns,
                        ShowWrongeAns = x.ShowWrongeAns,
                        ReqReTest = x.ReqReTest,
                        PassingScore = x.PassingScore,
                        Attempts = x.Attempts,
                        Instruction = x.Instruction,
                        Testruleid = x.Testruleid
                    }).ToList();
                    finalList = commddetail;
                }
                //get the recipeint details of particlar communication
                if (type.Equals(Shared.Recipient))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        fname = x.fname,
                        emailId = x.emailId,
                        phone = x.phone,
                        vendor = x.vendor,
                        contact = x.fname + " " + x.lname,
                        // role = x.role
                    }).ToList();
                    finalList = commddetail;
                }
                //get the questions details of particlar communication
                if (type.Equals(Shared.Ques))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        Question = x.Question,
                        AssesmentId = x.AssesmentId
                    }).ToList();
                    finalList = commddetail;
                }
                //get the answers details of particlar communication
                if (type.Equals(Shared.Ans))
                {
                    IList<CommDetailsResponse> commddetail = objdecisionPointRepository.GetCommDetails(DocId, type).Select(x => new CommDetailsResponse
                    {
                        Answer = x.Answer,
                        IsCorrect = (bool)x.IsCorrect,
                        QuestionId = (int)x.QuestionId
                    }).ToList();
                    finalList = commddetail;
                }
                return finalList;

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to update the staging status
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        /// <createdby>bobi</createdby>
        /// <createddate>12 june 2014</createddate>
        public int UpdateStagingstatus(int docid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateStagingstatus(docid);

            }
            catch
            {

                throw;
            }
        }
        #endregion
        /// <summary>
        /// Used to validate the vendor profile during registration flow
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string validateVendorProfile(int userId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string result = decisionPointRepository.validateVendorProfile(userId);
                return result;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to validate the staff profile during registration flow
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string validateStaffProfile(int userId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                string result = decisionPointRepository.validateStaffProfile(userId);
                return result;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to update the registration status of new added added user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="invitecompanyid"></param>
        /// <param name="type"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public int ChangeRegistrationStatus(int userId, int invitecompanyid, string type, string companyId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                int result = decisionPointRepository.ChangeRegistrationStatus(userId, invitecompanyid, type, companyId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used For get documents from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        public IEnumerable<UserDashBoardResponse> GetUnSentDocumentsDetails(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> unsentdocumentsdetails = objdecisionPointRepository.GetUnSentDocumentsDetails(userId, companyId).Select(x => new UserDashBoardResponse { DocType = x.DocType, DocTitle = x.DocTitle, Docfrom = x.Docfrom, reference = x.reference, policyNo = x.policyNo, DocId = x.DocId, effectiveDate = x.effectiveDate, hourofcredit = x.hourofcredit });
                return unsentdocumentsdetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to get the details is any recipient complete that comunication or not
        /// </summary>
        /// <param name="docid"></param>
        /// <returns></returns>
        /// <createdby>bobi</createdby>
        /// <createddate>18 june 2014</createddate>
        public bool GetIsAnyRecipient(int docId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetIsAnyRecipient(docId);

            }
            catch
            {
                throw;
            }
        }

        #region Category
        /// <summary>
        /// used for added the new refrence name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        public int AddCategory(CategoryRequest categoryRequest)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CategoryRequestParam categoryRequestParam = new CategoryRequestParam()
                {
                    categoryName = categoryRequest.categoryName,
                    UserId = categoryRequest.UserId,
                    CompanyId = categoryRequest.CompanyId,
                    sourceId = categoryRequest.sourceId,
                    groupId = categoryRequest.groupId
                };
                return objdecisionPointRepository.AddCategory(categoryRequestParam);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used for get the title details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        public IEnumerable<CompanyDashBoardResponse> GetCategory(string type, string ID, string sourceId, string group)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetCategory(type, ID, sourceId, group).Select(x => new CompanyDashBoardResponse { categoryName = x.categoryName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id, referenceName = x.referenceName, groupName = x.groupName }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to remove the reqiured action for communication
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="reqactionId"></param>
        /// <returns></returns>
        /// <createdby>bobi</createdby>
        /// <createddate>may 5 2014</createddate>
        public int RemoveReqActions(int reqactionId, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.RemoveReqActions(reqactionId, type);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for get the title details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        public IEnumerable<CompanyDashBoardResponse> GetUserCategory(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetUserCategory(companyId).Select(x => new CompanyDashBoardResponse { categoryName = x.categoryName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for disable and enable the title
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="isActive"></param>
        /// <returns>return one if title is disable else retrun zero</returns
        public int DisaEnaCategory(int categoryId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaCategory(categoryId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="titlename"></param>
        /// <returns>return one if client is update else retrun zero</returns>
        public int UpdateCategory(CategoryRequest categoryRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CategoryRequestParam categoryRequestParam = new CategoryRequestParam()
                {
                    categoryName = categoryRequest.categoryName,
                    categoryId = categoryRequest.categoryId,
                    CompanyId = categoryRequest.CompanyId,
                    sourceId = categoryRequest.sourceId,
                    groupId = categoryRequest.groupId,
                    UserId = categoryRequest.UserId
                };
                return objdecisionPointRepository.UpdateCategory(categoryRequestParam);

            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region Group
        /// <summary>
        /// used for added the new group name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>may 19 2014</createdDate>
        public int AddGroup(string groupName, int UserId, string CompanyId)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddGroup(groupName, UserId, CompanyId);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used for get the group details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>may 19 2014</createdDate>
        public IEnumerable<CompanyDashBoardResponse> GetGroup(string type, string ID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetGroup(type, ID).Select(x => new CompanyDashBoardResponse { categoryName = x.categoryName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id, referenceName = x.referenceName }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used for disable and enable the group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="isActive"></param>
        /// <returns>return one if title is disable else retrun zero</returns
        /// <createdby>bobi</createdby>
        /// <createdDate>may 19 2014</createdDate>
        public int DisaEnaGroup(int groupId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaGroup(groupId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns>return one if client is update else retrun zero</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>may 19 2014</createdDate>
        public int UpdateGroup(int groupId, string groupName, string CompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateGroup(groupId, groupName, CompanyId);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Vendor Type
        /// <summary>
        /// used for added the new group name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>july 7 2014</createdDate>
        public int AddVendorType(string vType, int UserId, string CompanyId)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddVendorType(vType, UserId, CompanyId);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used for added the new group name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>july 7 2014</createdDate>
        public int AddVendorTypeForCompany(VendorTypeRequest objVendorTypeRequest)
        {

            try
            {
                VendorTypeRequestParam objVendorTypeRequestParam = new VendorTypeRequestParam()
                {
                    UserCompanyId = objVendorTypeRequest.UserCompanyId,
                    UserId = objVendorTypeRequest.UserId,
                    CreatedBy = objVendorTypeRequest.CreatedBy,
                    CreatorCompanyID = objVendorTypeRequest.CreatorCompanyID,
                    VendroTypeIds = objVendorTypeRequest.VendroTypeIds,
                    TYPE = objVendorTypeRequest.TYPE

                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddVendorTypeForCompany(objVendorTypeRequestParam);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used for get the group details
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>july 7 2014</createdDate>
        public IEnumerable<CompanyDashBoardResponse> GetVendorType(string type, string ID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> referencedetails = objdecisionPointRepository.GetVendorType(type, ID).Select(x => new CompanyDashBoardResponse { categoryName = x.categoryName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id, referenceName = x.referenceName }).ToList();
                return referencedetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for get the company Vendor Type
        /// </summary>
        /// <returns>return title detial in ienumerbale form</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>july 9 2014</createdDate>
        public IEnumerable<VendorTypeResponse> GetCompanyVendorType(string companyId, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<VendorTypeResponse> companyvendortypedetails = objdecisionPointRepository.GetCompanyVendorType(companyId, type).Select(x => new VendorTypeResponse { VendorTypeName = x.VendorTypeName, tblId = x.tblId, VendorTypeId = x.VendorTypeId, UserCompanyId = x.UserCompanyId, CreatorCompanyId = x.CreatorCompanyId, IsUserBased = x.IsUserBased }).ToList();
                return companyvendortypedetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for disable and enable the group
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="isActive"></param>
        /// <returns>return one if title is disable else retrun zero</returns
        /// <createdby>bobi</createdby>
        /// <createdDate>july 7 2014</createdDate>
        public int DisaEnaVendorType(int vTypeId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaVendorType(vTypeId, isActive);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupName"></param>
        /// <returns>return one if client is update else retrun zero</returns>
        /// <createdby>bobi</createdby>
        /// <createdDate>july 7 2014</createdDate>
        public int UpdateVendorType(int vTypeId, string VTypeName, string CompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateVendorType(vTypeId, VTypeName, CompanyId);

            }
            catch
            {
                throw;
            }
        }
        #endregion
        #endregion

        #region << Engine Source code of Sumit >>
        /// <summary>
        /// Used to set the status of user after chnages password
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="InviteeCompanyId">InviteeCompanyId</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns></returns>
        public int SetTempPay(int userId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                int result = decisionPointRepository.SetTempPay(userId);
                return result;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to make payement of any new added user
        /// </summary>
        /// <param name="objpayment">PaymentAmountResponse class</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int MakePayment(PaymentAmountResponse objpayment)
        {
            try
            {
                return 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get smtp details for mail sending
        /// </summary>
        /// <param name="emailTo">email will send to</param>
        /// <param name="password">password </param>
        /// <param name="body">message body</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 nov 2013</createdDate>
        public void GetSmtpDetail(string emailTo, string body, string subject)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                SMTPDetail objSMTPDetail = objdecisionPointRepository.Getsmtpdetails();
                //Set Mail credentials
                objBusinessEmail = new BusinessEmail();
                objBusinessEmail.EmailBody = body;
                objBusinessEmail.EmailFrom = objSMTPDetail.EmailSmtpServer;
                objBusinessEmail.EmailTo = emailTo;
                objBusinessEmail.EmailSubject = subject;
                objBusinessEmail.EmailSmtpServerHost = objSMTPDetail.EmailSmtpServerHost;
                objBusinessEmail.EmailSmtpServerPort = objSMTPDetail.EmailSmtpServerPort;
                objBusinessEmail.EmailSmtpServerSSL = objSMTPDetail.EmailSmtpServerSSL;
                objBusinessEmail.EmailPassword = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.PasswordSmtpServer = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.EmailSmtpServer = objSMTPDetail.EmailSmtpServer;
                BusinessCore.SendMail(objBusinessEmail);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get smtp details for mail sending
        /// </summary>
        /// <param name="emailTo">email will send to</param>
        /// <param name="password">password </param>
        /// <param name="body">message body</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 nov 2013</createdDate>
        public void GetSmtpWithAttachementDetail(string emailTo, string body, List<string> attachements)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                SMTPDetail objSMTPDetail = objdecisionPointRepository.Getsmtpdetails();
                //Set Mail credentials
                objBusinessEmail = new BusinessEmail();
                objBusinessEmail.EmailBody = body;
                objBusinessEmail.EmailFrom = objSMTPDetail.EmailSmtpServer;
                objBusinessEmail.EmailTo = emailTo;
                objBusinessEmail.EmailSubject = "Sterling Aggreements & Certificates";
                objBusinessEmail.EmailSmtpServerHost = objSMTPDetail.EmailSmtpServerHost;
                objBusinessEmail.EmailSmtpServerPort = objSMTPDetail.EmailSmtpServerPort;
                objBusinessEmail.EmailSmtpServerSSL = objSMTPDetail.EmailSmtpServerSSL;
                objBusinessEmail.EmailPassword = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.PasswordSmtpServer = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.EmailSmtpServer = objSMTPDetail.EmailSmtpServer;
                objBusinessEmail.AttachementFiles = attachements;
                BusinessCore.SendMailWithAttachements(objBusinessEmail);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for set the last invitation date
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <createdby>Bobi</createdby>
        ///  <createdDate>11 nov 2014</createdDate>
        public int UpdateLastInvite(string companyId, int userId, string userType)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateLastInvite(companyId, userId, userType);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get State list
        /// </summary>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<StateRespone> GetStateList()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<StateRespone> state = objdecisionPointRepository.GetStateList().Select(x => new StateRespone { DriverLicenseCost = x.DriverLicenseCost, StateId = x.StateId, SateName = x.SateName, Abbrebiation = x.Abbrebiation });
                return state;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// get city list
        /// </summary>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CityResponse> GetCityList(string countyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CityResponse> state = objdecisionPointRepository.GetCityList(countyId).Select(x => new CityResponse { CityId = x.CityId, CityName = x.CityName, StateName = x.StateName, CountyName = x.CountyName, CountyId = x.CountyId });
                return state;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get city list for staff and Ic
        /// </summary>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<CityResponse> GetCityList(string countyId, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CityResponse> state = objdecisionPointRepository.GetCityList(countyId, userId).Select(x => new CityResponse { CityId = x.CityId, CityName = x.CityName, StateName = x.StateName, CountyName = x.CountyName, CountyId = x.CountyId });
                return state;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// set compamy profile request
        /// </summary>
        /// <param name="companyProfileRequest">parameters to save company profile</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>message with sucess or fail saving company profile</returns>
        public int SetCompanyProfile(CompanyProfileRequest companyProfileRequest)
        {
            try
            {
                businessCryptography = new BusinessCryptography();
                CompanyProfileRequestParam companyProfileRequestParam = new CompanyProfileRequestParam();
                objdecisionPointRepository = new DecisionPointRepository();
                companyProfileRequestParam.UserId = companyProfileRequest.UserId;
                companyProfileRequestParam.BusinessName = companyProfileRequest.BusinessName;
                companyProfileRequestParam.OfficePhone = companyProfileRequest.OfficePhone;
                companyProfileRequestParam.BusinessAddress = companyProfileRequest.BusinessAddress;
                companyProfileRequestParam.Direction = companyProfileRequest.Direction;
                companyProfileRequestParam.StreetName = companyProfileRequest.StreetName;
                companyProfileRequestParam.StateId = companyProfileRequest.StateId;
                companyProfileRequestParam.CityId = companyProfileRequest.CityId;
                companyProfileRequestParam.CityName = companyProfileRequest.CityName;
                companyProfileRequestParam.ZipCode = companyProfileRequest.ZipCode;
                companyProfileRequestParam.fax = companyProfileRequest.fax;
                companyProfileRequestParam.Email = companyProfileRequest.Email;
                companyProfileRequestParam.StreetNumber = companyProfileRequest.StreetNumber;
                companyProfileRequestParam.CompanyLogo = companyProfileRequest.CompanyLogo;
                companyProfileRequestParam.UserType = companyProfileRequest.UserType;
                companyProfileRequestParam.CerificationNumber = companyProfileRequest.CerificationNumber;
                companyProfileRequestParam.CertifyingAgency = companyProfileRequest.CertifyingAgency;
                companyProfileRequestParam.CertificateExpDate = companyProfileRequest.CertificateExpDate;
                companyProfileRequestParam.BusinessClass = companyProfileRequest.BusinessClass;
                if (!string.IsNullOrEmpty(companyProfileRequest.Password))
                {
                    companyProfileRequestParam.Password = businessCryptography.base64Encode(companyProfileRequest.Password);
                }
                return objdecisionPointRepository.SetCompanyProfile(companyProfileRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get company profile details
        /// </summary>
        /// <param name="userId">user id</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns> company profile details</returns>
        public CompanyProfileResponse GetCompanyProfileDetails(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CompanyProfileResponse company = null;
                CompanyProfileResponseParam objCompanyProfileResponseParam = objdecisionPointRepository.GetCompanyProfileDetails(userId);
                if (!object.Equals(objCompanyProfileResponseParam, null))
                {
                    company = new CompanyProfileResponse
                    {
                        BusinessName = objCompanyProfileResponseParam.BusinessName,
                        OfficePhone = objCompanyProfileResponseParam.OfficePhone,
                        BusinessAddress = objCompanyProfileResponseParam.BusinessAddress,
                        Direction = objCompanyProfileResponseParam.Direction,
                        StreetName = objCompanyProfileResponseParam.StreetName,
                        StateId = objCompanyProfileResponseParam.StateId,
                        CityId = objCompanyProfileResponseParam.CityId,
                        ZipCode = objCompanyProfileResponseParam.ZipCode,
                        fax = objCompanyProfileResponseParam.fax,
                        Email = objCompanyProfileResponseParam.Email,
                        SecurityAnswer1 = objCompanyProfileResponseParam.SecurityAnswer1,
                        SecurityAnswer2 = objCompanyProfileResponseParam.SecurityAnswer2,
                        SecurityAnswer3 = objCompanyProfileResponseParam.SecurityAnswer3,
                        StreetNumber = objCompanyProfileResponseParam.StreetNumber,
                        CityName = objCompanyProfileResponseParam.CityName,
                        CompanyLogo = objCompanyProfileResponseParam.CompanyLogo,
                        Password = objCompanyProfileResponseParam.Password,
                        CerificationNumber = objCompanyProfileResponseParam.CerificationNumber,
                        CertificateExpDate = objCompanyProfileResponseParam.CertificateExpDate,
                        CertifyingAgency = objCompanyProfileResponseParam.CertifyingAgency,
                        BusinessClass = objCompanyProfileResponseParam.BusinessClass,
                        FlowType = objCompanyProfileResponseParam.FlowType
                    };

                }
                return company;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// set admin profile
        /// </summary>
        /// <param name="adminProfileRequest">parameters to save admin profile details</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>12 dec 2013</createdDate>
        /// <returns>in type result sucess or fail</returns>
        public int SetAdminProfile(AdminProfileRequest adminProfileRequest)
        {
            try
            {
                businessCryptography = new BusinessCryptography();
                objdecisionPointRepository = new DecisionPointRepository();
                AdminProfileRequestParam adminProfileRequestParam = new AdminProfileRequestParam()
                {
                    UserId = adminProfileRequest.UserId,
                    FirstName = adminProfileRequest.FirstName,
                    LastName = adminProfileRequest.LastName,
                    NickName = adminProfileRequest.NickName,
                    MiddleName = adminProfileRequest.MiddleName,
                    Password = businessCryptography.base64Encode(adminProfileRequest.Password),
                    Email = adminProfileRequest.Email,
                    CellNumber = adminProfileRequest.CellNumber,
                    SecurityQuestion1 = adminProfileRequest.SecurityQuestion1,
                    SecurityQuestion2 = adminProfileRequest.SecurityQuestion2,
                    SecurityQuestion3 = adminProfileRequest.SecurityQuestion3,
                    SecurityAnswer1 = adminProfileRequest.SecurityAnswer1,
                    SecurityAnswer2 = adminProfileRequest.SecurityAnswer2,
                    SecurityAnswer3 = adminProfileRequest.SecurityAnswer3,
                    OfficePhone = adminProfileRequest.OfficePhone,
                    CompanyId = adminProfileRequest.CompanyId,
                };
                return objdecisionPointRepository.SetAdminProfile(adminProfileRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get admin profile details
        /// </summary>
        /// <param name="userId">user id</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>11 dec 2013</createdDate>
        /// <returns>admin profile info</returns>
        public IEnumerable<AdminProfileResponse> GetAdminProfile(int userId)
        {
            try
            {
                DecisionPointRepository decisionPointRepository = new DecisionPointRepository();
                IEnumerable<AdminProfileResponse> admin = decisionPointRepository.GetAdminProfile(userId).Select(y => new AdminProfileResponse
                {
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    MiddleName = y.MiddleName,
                    NickName = y.NickName,
                    CellNumber = y.CellNumber,
                    OfficePhone = y.OfficePhone,
                    Password = y.Password,
                    Email = y.Email,
                    SecurityAnswer1 = y.SecurityAnswer1,
                    SecurityAnswer2 = y.SecurityAnswer2,
                    SecurityAnswer3 = y.SecurityAnswer3
                });
                return admin;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get county list as per state abbrebiations
        /// </summary>
        /// <param name="StateAbbre">state abbreviation</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>12 dec 2013</createdDate>
        /// <returns>list of counties</returns>
        public IEnumerable<CountyResponse> GetCountyList(string StateAbbre)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CountyResponse> county = objdecisionPointRepository.GetCountyList(StateAbbre).Select(x => new CountyResponse { CountyName = x.CountyName, CountyId = x.CountyId, StateAbbre = x.StateAbbre });
                return county;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get city list by state abbrebiation for company setup page
        /// </summary>
        /// <param name="StateAbbre">state abbribiation</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>13 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        public IEnumerable<CityResponse> GetCityListByState(string StateAbbre)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CityResponse> state = objdecisionPointRepository.GetCityListByState(StateAbbre).Select(x => new CityResponse { CityId = x.CityId, CityName = x.CityName, StateName = x.StateName, CountyName = x.CountyName });
                return state;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to get Register user details
        /// </summary>
        /// <param name="userRegister">userRegister</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int GetCompanyRegister(UserRegisterRequest userRegisterRequest)
        {
            UserRegisterRequestParam userRegisterRequestParam = null;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                userRegisterRequestParam = new UserRegisterRequestParam()
                {
                    CellAreaCode = userRegisterRequest.CellAreaCode,
                    EmailId = userRegisterRequest.EmailId,
                    Password = BusinessCore.CreatePassword()
                };

                objdecisionPointRepository.GetCompanyRegister(userRegisterRequestParam);
                return 0;
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <param name="password"> password</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 nov 2013</createdDate>
        /// <returns>int type sucess or fail password change</returns>
        public int ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            try
            {
                businessCryptography = new BusinessCryptography();
                changePasswordRequest.Password = businessCryptography.base64Encode(changePasswordRequest.Password);
                ChangePasswordRequestParam changePasswordRequestParam = new ChangePasswordRequestParam();
                objdecisionPointRepository = new DecisionPointRepository();
                changePasswordRequestParam.UserId = changePasswordRequest.UserId;
                changePasswordRequestParam.email = changePasswordRequest.email;
                changePasswordRequestParam.Password = changePasswordRequest.Password;
                changePasswordRequestParam.OldPassword = changePasswordRequest.OldPassword;
                return objdecisionPointRepository.ChangePassword(changePasswordRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get security question list for registration page
        /// </summary>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of security questions</returns>
        public IEnumerable<SecurityQuestionResponse> GetSecurityQuestion()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<SecurityQuestionResponse> SecurityQuestion = objdecisionPointRepository.GetSecurityQuestion().Select(x => new SecurityQuestionResponse { SecurityId = x.SecurityId, SecurityQuestion = x.SecurityQuestion });
                return SecurityQuestion;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to check email in database or not during forgot password
        /// </summary>
        /// <param name="email">email</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>string</returns>
        public string CheckEmail(string email)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                string emailid = objdecisionPointRepository.CheckEmail(email);
                if (!string.IsNullOrEmpty(emailid))
                {
                    emailid = businessCryptography.base64Decode2(emailid);
                }

                return emailid;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get smtp details
        /// </summary>
        /// <param name="emailTo">emailTo</param>
        /// <param name="password">password</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        public void Getsmtpdetails(string emailTo, string password)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                SMTPDetail objSMTPDetail = objdecisionPointRepository.Getsmtpdetails();
                //Set Mail credentials
                objBusinessEmail = new BusinessEmail();
                objBusinessEmail.EmailBody = "Your Password is " + password + "";
                objBusinessEmail.EmailFrom = objSMTPDetail.EmailSmtpServer;
                objBusinessEmail.EmailTo = emailTo;
                objBusinessEmail.EmailSubject = "Your Password ";
                objBusinessEmail.EmailSmtpServerHost = objSMTPDetail.EmailSmtpServerHost;
                objBusinessEmail.EmailSmtpServerPort = objSMTPDetail.EmailSmtpServerPort;
                objBusinessEmail.EmailSmtpServerSSL = objSMTPDetail.EmailSmtpServerSSL;
                objBusinessEmail.EmailPassword = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.PasswordSmtpServer = objSMTPDetail.PasswordSmtpServer;
                objBusinessEmail.EmailSmtpServer = objSMTPDetail.EmailSmtpServer;
                BusinessCore.SendMail(objBusinessEmail);

            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// fetch user details from data layer through login id and password and call from controller
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="password">password</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 nov 2013</createdDate>
        /// <returns>login sucess or not if sucess then redirect to other area.else login again</returns>
        public LoginDetailResponse Login(string userid, string password)
        {
            try
            {
                businessCryptography = new BusinessCryptography();
                password = businessCryptography.base64Encode(password);
                // Instantiated DecisionPointRepository class of DAL
                objdecisionPointRepository = new DecisionPointRepository();
                // Instantiated LoginDetailsResponseparam class for properties
                loginDetailsResponseparam = new LoginDetailsResponseParam();
                // get User Details from database using login id and password
                loginDetailsResponseparam = objdecisionPointRepository.CheckLogin(userid, password);
                loginDetailResponse = new LoginDetailResponse();
                if (loginDetailsResponseparam != null)
                {
                    loginDetailResponse.Emailid = loginDetailsResponseparam.Emailid;
                    loginDetailResponse.UserType = loginDetailsResponseparam.UserType;
                    loginDetailResponse.Firstname = loginDetailsResponseparam.Firstname;
                    loginDetailResponse.MiddelName = loginDetailsResponseparam.MiddelName;
                    loginDetailResponse.LastName = loginDetailsResponseparam.LastName;
                    loginDetailResponse.UserId = loginDetailsResponseparam.UserId;
                    loginDetailResponse.IsPayment = loginDetailsResponseparam.IsPayment;
                    loginDetailResponse.IsTemp = loginDetailsResponseparam.IsTemp;
                    loginDetailResponse.BusinessName = loginDetailsResponseparam.BusinessName;
                    loginDetailResponse.CompanyId = loginDetailsResponseparam.CompanyId;
                    loginDetailResponse.IsRegistered = loginDetailsResponseparam.IsRegistered;
                    loginDetailResponse.IsActive = loginDetailsResponseparam.IsActive;
                }
                return loginDetailResponse;

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Used to get IC PAYMENT TYPE
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="companyId">companyId</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>int</returns>
        public int GetIcPaymentType(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetIcPaymentType(userId, companyId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// payment gateway of stripe
        /// </summary>
        /// <param name="paymentResponse">parameters with info for payment gateway</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns> sucess or fail payment message</returns>
        public int companyPayment(PaymentResponse paymentResponse, PaymentAmountResponse objPaymentAmountResponse)
        {
            try
            {
                string stripeKey = System.Configuration.ConfigurationManager.AppSettings["StripeKey"].ToString();
                StripePayment payment = new StripePayment(stripeKey);
                string result = XamarinStripeCore.DecisionPointRegistrationCharge(payment, paymentResponse);
                objdecisionPointRepository = new DecisionPointRepository();
                if (!string.IsNullOrEmpty(Convert.ToString(result)))
                {
                    PaymentAmountResponseParam paymentAmountResponseParam = new PaymentAmountResponseParam
                    {
                        CompanyCode = objPaymentAmountResponse.CompanyCode,
                        CompanyFee = objPaymentAmountResponse.CompanyFee,
                        PerFieldStaffFee = objPaymentAmountResponse.PerFieldStaffFee,
                        PerOfficeStaffFee = objPaymentAmountResponse.PerOfficeStaffFee,
                        PerIcFee = objPaymentAmountResponse.PerIcFee,
                        IsInvoice = objPaymentAmountResponse.IsInvoice,
                        NoOfPartners = objPaymentAmountResponse.NoOfPartners,
                        NoOfStaff = objPaymentAmountResponse.NoOfStaff,
                        NoOfIc = objPaymentAmountResponse.NoOfIc,
                        BusinessName = objPaymentAmountResponse.BusinessName,
                        TransactionType = objPaymentAmountResponse.TransactionType,
                        TransactionCode = result,
                        TransactionMessage = "Payment Sucesss",
                        userId = objPaymentAmountResponse.userId,
                        InviteeCompanyId = objPaymentAmountResponse.InviteeCompanyId
                    };
                    int results = objdecisionPointRepository.MakePayment(paymentAmountResponseParam);
                    return results;
                }
                else
                {

                    return 0;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get payment amount as per company id
        /// </summary>
        /// <param name="CompanyId">company id</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>list of payment details for office staff, field staff</returns>
        public IEnumerable<PaymentAmountResponse> getPaymentAmount(string CompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<PaymentAmountResponse> state = objdecisionPointRepository.getPaymentAmount(CompanyId).Select(x => new PaymentAmountResponse
                {
                    Id = x.Id,
                    CompanyCode = x.CompanyCode,
                    CompanyFee = x.CompanyFee,
                    PerFieldStaffFee = x.PerFieldStaffFee,
                    PerOfficeStaffFee = x.PerOfficeStaffFee,
                    IsInvoice = x.IsInvoice,
                    PerIcFee = x.PerIcFee,

                });
                return state;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get zip list as per city selection
        /// </summary>
        /// <param name="CityName">list of comma seprated city in string format</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of zip codes</returns>
        public IEnumerable<ZipResponse> GetZipListByCity(string CityName, string stateabbrelist, string county)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ZipResponse> zip = objdecisionPointRepository.GetZipListByCity(CityName, stateabbrelist, county).Select(x => new ZipResponse { ZipId = x.ZipId, CityName = x.CityName, StateAbbre = x.StateAbbre, CountyName = x.CountyName, ZipCode = x.ZipCode });
                return zip;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list as per zip selection
        /// </summary>
        /// <param name="ZipCode">comma seprated zip codes</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        public IEnumerable<ZipResponse> GetCityListByzip(string ZipCode)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ZipResponse> zip = objdecisionPointRepository.GetCityListByzip(ZipCode).Select(x => new ZipResponse { ZipId = x.ZipId, CityName = x.CityName, StateAbbre = x.StateAbbre, CountyName = x.CountyName, ZipCode = x.ZipCode, StateName = x.StateName });
                return zip;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list as per zip selection
        /// </summary>
        /// <param name="ZipCode">comma seprated zip codes</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        public IEnumerable<ZipResponse> GetCityListByZipOnComm(string ZipCode)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ZipResponse> zip = objdecisionPointRepository.GetCityListByZipOnComm(ZipCode).Select(x => new ZipResponse { CityName = x.CityName });
                return zip;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get city list as per zip selection
        /// </summary>
        /// <param name="ZipCode">comma seprated zip codes</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        public IEnumerable<ZipResponse> GetStateListByZip(string ZipCode)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ZipResponse> zip = objdecisionPointRepository.GetStateListByZip(ZipCode).Select(x => new ZipResponse { CityName = x.CityName, StateAbbre = x.StateAbbre });
                return zip;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get city list as per zip selection
        /// </summary>
        /// <param name="ZipCode">comma seprated zip codes</param>
        /// <createdby>sumit</createdby>
        ///  <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        public IEnumerable<ZipResponse> GetCountyListByZip(string ZipCode)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<ZipResponse> zip = objdecisionPointRepository.GetCountyListByZip(ZipCode).Select(x => new ZipResponse { ZipId = x.ZipId, CityName = x.CityName, StateAbbre = x.StateAbbre, CountyName = x.CountyName, ZipCode = x.ZipCode });
                return zip;
            }
            catch
            {
                throw;
            }
        }


        #endregion

        /// <summary>
        /// This method is used to infomation of vendor on the basis of comapny ID
        /// </summary>
        /// <param name="companyId">companyId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>VendorBulkResponse class</returns>
        public VendorBulkResponse GetVendorInfo(string companyId, string Usertype)
        {
            try
            {
                VendorBulkResponse GetVendorInfo = null;
                objdecisionPointRepository = new DecisionPointRepository();
                VendorBulk vendorRequest = objdecisionPointRepository.GetVendorInfo(companyId, Usertype);
                if (vendorRequest != null)
                {
                    GetVendorInfo = new VendorBulkResponse()
                    {
                        CompanyId = vendorRequest.CompanyId,
                        CompanyName = vendorRequest.CompanyName,
                        EmailId = vendorRequest.EmailId,
                        FName = vendorRequest.FName,
                        LName = vendorRequest.LName,
                        FlowId = vendorRequest.FlowId,
                        FlowValue = vendorRequest.FlowValue,
                        DocFlowId = vendorRequest.DocFlowId,
                        DocFlowValue = vendorRequest.DocFlowValue,
                        PaymentId = vendorRequest.PaymentId
                    };
                }


                return GetVendorInfo;

            }
            catch
            {

                throw;
            }
        }

        #region Performance
        /// <summary>
        /// vendor performace screen
        /// </summary>
        /// <param name="companyUserID">int</param>
        /// <param name="isActive">bool</param>
        /// <param name="type">vendor/client</param>
        /// <returns>vendorResponseParam</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>January 7 2014</createdDate>
        public IEnumerable<VendorPerformance> VendorPerformance(string companyID, bool isActive, string type, int UserId)
        {
            IEnumerable<VendorPerformance> lstVendorResponseParam = null;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                lstVendorResponseParam = objdecisionPointRepository.VendorPerformance(companyID, isActive, type, UserId).Select(x => new VendorPerformance { VendorID = x.VendorID, name = x.name, DocumentCompleteByVendors = x.DocumentCompleteByVendors, CompanyCompletion = x.CompanyCompletion, Received = x.Received, Forwarded = x.Forwarded });
                return lstVendorResponseParam;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

        }
        #endregion

        #region Trainng view
        /// <summary>
        /// getUserViewList
        /// </summary>
        /// <param name="userID">userID</param>
        /// <param name="docID">docID</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<UserViewResponse> getUserViewList(int userID, int docID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                IEnumerable<UserViewResponse> getUserViewList = objdecisionPointRepository.getUserViewList(userID, docID).ToList().Select(q => new UserViewResponse
                {
                    Name = q.Name,
                    BusinessName = q.BusinessName,
                    UserTitle = q.UserTitle,
                    DocType = q.DocType,
                    DueDate = q.DueDate,
                    FileLoc = q.FileLoc,
                    FileTitle = q.FileTitle,
                    Filetype = q.Filetype,
                    Status = q.Status,
                    DocID = q.DocID,
                    CourseName = q.CourseName,
                    Reference = q.reference,
                    Instruction = q.Instruction,
                    DocTitle = q.DocTitle,
                    VideoTitle = q.VideoTitle,
                    ScormTitle = q.ScormTitle
                });

                return getUserViewList;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Used to get the Training Materials Details of a User for Student View
        /// </summary>
        /// <param name="userID">ID of Current User</param>
        /// <param name="docID">ID of current document</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>List of UserTrainingMaterialsResponse Class</returns>

        public IEnumerable<UserTrainingMaterialsResponse> GetTrainingMaterialsDetails(int userID, int docID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                IEnumerable<UserTrainingMaterialsResponse> objTrainingMaterialsResponse = objdecisionPointRepository.GetTrainingMaterialsDetails(userID, docID).ToList()
                    .Select(training => new UserTrainingMaterialsResponse
                    {
                        CreatedBy = training.CreatedBy,
                        CreatedDate = training.CreatedDate,
                        DocMsgID = training.DocMsgID,
                        FileLocation = training.FileLocation,
                        FileTitle = training.FileTitle,
                        FileType = training.FileType,
                        ModifiedDate = training.ModifiedDate,
                        ModiifiedBy = training.ModiifiedBy,
                        status = training.status,
                        DocID = training.DocID,
                        ViewDate = training.ViewDate,
                        CompletionDate = training.CompletionDate,
                        TimeSpan = training.TimeSpan
                    });


                return objTrainingMaterialsResponse;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// GetUserDocStatus
        /// </summary>
        /// <param name="userid">userid</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IList</returns>
        public IList<UserTrainingMaterialsResponse> GetUserDocStatus(int userid)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<UserTrainingMaterialsResponse> objDoc = objdecisionPointRepository.GetUserDocStatus(userid).Select(x => new UserTrainingMaterialsResponse { DocID = x.DocID, status = x.status }).ToList();
                return objDoc;


            }
            catch
            {

                throw;

            }

        }

        /// <summary>
        /// ChecngeDocumentStatus
        /// </summary>
        /// <param name="docID">docID</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>bool</returns>
        public bool ChecngeDocumentStatus(int docID)
        {
            bool status = false;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                status = objdecisionPointRepository.ChangeDocumentStatus(docID);

                return status;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// getAssesmentViewList
        /// </summary>
        /// <param name="docID">docID</param>
        /// <param name="RandomQuestion">RandomQuestion</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>5 Apr 2014</createdDate>
        /// <returns>IList</returns>
        public IList<UserAssesmentResponse> getAssesmentViewList(int docID, bool RandomQuestion)
        {
            try
            {

                IList<UserAssesmentResponse> getUserAssessmentlist = null;

                objdecisionPointRepository = new DecisionPointRepository();
                getUserAssessmentlist = objdecisionPointRepository.getAssesmentViewList(docID, RandomQuestion).Select(x => new UserAssesmentResponse { Id = x.Id, Question = x.Question }).ToList();

                return getUserAssessmentlist;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// getAssesmentAnsViewList
        /// </summary>
        /// <param name="docID">docID</param>
        /// <param name="RandomAns">RandomAns</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IList</returns>
        public IList<UserAssesmentAnswerResponse> getAssesmentAnsViewList(int docID, bool RandomAns)
        {
            try
            {

                IList<UserAssesmentAnswerResponse> getUserAssessmentAns = null;
                objdecisionPointRepository = new DecisionPointRepository();
                getUserAssessmentAns = objdecisionPointRepository.getAssesmentAnsViewList(docID, RandomAns).Select(x => new UserAssesmentAnswerResponse { Id = x.Id, Answer = x.Answer, IsCorrect = x.IsCorrect, QuestionId = x.QuestionId }).ToList();
                return getUserAssessmentAns;

            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// GetCorrectAnswer
        /// </summary>
        /// <param name="DocId">DocId</param>
        /// <param name="UserId">UserId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IList</returns>
        public IList<UserCorrectAsstAnswerResponse> GetCorrectAnswer(int DocId, int UserId)
        {
            try
            {

                IList<UserCorrectAsstAnswerResponse> getCorretAssessmentAns = null;
                objdecisionPointRepository = new DecisionPointRepository();
                getCorretAssessmentAns = objdecisionPointRepository.GetCorrectAnswer(DocId, UserId).Select(x => new UserCorrectAsstAnswerResponse { QuestionId = x.QuestionId, OptionAnserId = x.OptionAnserId, GivenAnsId = x.GivenAnsId, Answer = x.Answer, Iscorrect = x.Iscorrect }).ToList();
                return getCorretAssessmentAns;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// getInstructions
        /// </summary>
        /// <param name="docID">docID</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IList</returns>
        public IList<UserInstructionsResponse> getInstructions(int docID)
        {
            try
            {

                IList<UserInstructionsResponse> getUserInstruction = null;
                objdecisionPointRepository = new DecisionPointRepository();
                getUserInstruction = objdecisionPointRepository.getInstructions(docID).Select(x => new UserInstructionsResponse { Id = x.Id, DocId = x.DocId, Attempts = x.Attempts, Instruction = x.Instruction, PassingScore = x.PassingScore, RandAns = x.RandAns, RandQues = x.RandQues, ReqReTest = x.ReqReTest, ShowWrongeAns = x.ShowWrongeAns }).ToList();
                return getUserInstruction;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// SaveAssessmentResult
        /// </summary>
        /// <param name="tableId">tableId</param>
        /// <param name="assessmentResult">assessmentResult</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int SaveAssessmentResult(int tableId, string assessmentResult)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveAssessmentResult(tableId, assessmentResult);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// SaveUserAsstAttempts
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="docId">docId</param>
        /// <param name="attempt">attempt</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int SaveUserAsstAttempts(int userId, int docId, int attempt)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveUserAsstAttempts(userId, docId, attempt);
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Used to get the Training Materials Details of a User for Student View
        /// </summary>
        /// <param name="userId">ID of current User</param>
        /// <param name="docId">ID of current document</param>
        /// <param name="questionId">ID of current Assessment Question</param>
        /// <param name="AnswerId">ID of current AnswerId of selected Question Of assessment</param>
        ///  /// <param name="attempt">attempt means how many retake the user can attempts for assessment</param>
        public int SaveUserSelectedAnswers(UserSelectedAnswersRequest objRequest)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                UserSelectedAnswersRequestParam userSelectedAnswersRequestParam = new UserSelectedAnswersRequestParam()
                {
                    userId = objRequest.userId,
                    docId = objRequest.docId,
                    questionId = objRequest.questionId,
                    AnswerId = objRequest.AnswerId,
                    attempt = objRequest.attempt,
                    tableid = objRequest.tableid
                };
                return objdecisionPointRepository.SaveUserSelectedAnswers(userSelectedAnswersRequestParam);
            }
            catch
            {
                throw;
            }



        }
        /// <summary>
        /// check particular doc status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="docId"></param>
        /// <returns>bool</returns>
        public bool CheckDocStatusAsPerCommunication(int userId, int docId)
        {
            bool IsCompleted = false;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IsCompleted = objdecisionPointRepository.CheckDocStatusAsPerCommunication(userId, docId);
                return IsCompleted;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Used to set marks as complete
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="accepted"></param>
        /// <param name="DocID"></param>
        /// <returns></returns>
        public int SaveMarkAsComplete(int tableId, bool accepted, int DocID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveMarkAsComplete(tableId, accepted, DocID);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to set spend time on document
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="docid"></param>
        /// <param name="DeleiverUserId"></param>
        /// <param name="timespan"></param>
        /// <param name="Completion"></param>
        /// <returns></returns>
        public int SaveDocTimeSpent(DocTimeSpentRequest docTimeSpentRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                DocTimeSpentRequestParam docTimeSpentRequestParam = new DocTimeSpentRequestParam()
                {
                    userid = docTimeSpentRequest.userid,
                    docid = docTimeSpentRequest.docid,
                    DeleiverUserId = docTimeSpentRequest.DeleiverUserId,
                    timespan = docTimeSpentRequest.timespan,
                    Completion = docTimeSpentRequest.Completion
                };
                return objdecisionPointRepository.SaveDocTimeSpent(docTimeSpentRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// sued to get spend time of any user on document
        /// </summary>
        /// <param name="deliveredUserId"></param>
        /// <returns></returns>
        public IList<UserTimeSpanResponse> GetUSerTimeSpan(int deliveredUserId)
        {
            try
            {
                IList<UserTimeSpanResponse> objUserTime = null;
                objdecisionPointRepository = new DecisionPointRepository();
                objUserTime = objdecisionPointRepository.GetUSerTimeSpan(deliveredUserId).Select(x => new UserTimeSpanResponse { docid = x.docid, TimeSpan = x.TimeSpan, CompletionDate = x.CompletionDate }).ToList();
                return objUserTime;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get user compeletion date
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IList<UserTimeSpanResponse> GetUserCompletionDate(int userid)
        {
            try
            {
                IList<UserTimeSpanResponse> objUserTime = null;
                objdecisionPointRepository = new DecisionPointRepository();
                objUserTime = objdecisionPointRepository.GetUserCompletionDate(userid).Select(x => new UserTimeSpanResponse { docid = x.docid, CompletionDate = x.CompletionDate, ViewDate = x.ViewDate }).ToList();
                return objUserTime;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used to get acknowledgment details
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public IList<UserAcknowledgmentResponse> GetAcknowledgment(int docId)
        {
            try
            {
                IList<UserAcknowledgmentResponse> objack = null;
                objdecisionPointRepository = new DecisionPointRepository();
                objack = objdecisionPointRepository.GetAcknowledgment(docId).Select(x => new UserAcknowledgmentResponse { id = x.id, Acknowlegment = x.Acknowlegment }).ToList();
                return objack;
            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// Used to save marks as complete document status
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="DocId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int SaveDocMarkAsComplete(int userid, int DocId, bool status)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveDocMarkAsComplete(userid, DocId, status);
            }
            catch
            {
                throw;
            }

        }
        #endregion


        #region Mail Footer
        /// <summary>
        /// Mail footer
        /// </summary>
        /// <param name="objMailFooter">MailFooterRequestParam</param>
        /// <returns>int</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 24 2014</createdDate>
        public int MailFooter(MailFooterRequest objMailFooter)
        {
            try
            {
                MailFooterRequestParam objMailFooterRequestParam = new MailFooterRequestParam();
                objMailFooterRequestParam.UserID = objMailFooter.UserID;
                objMailFooterRequestParam.Signauture = objMailFooter.Signauture;
                objMailFooterRequestParam.SignatureName = objMailFooter.SignatureName;
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.MailFooter(objMailFooterRequestParam);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get mail footer detail
        /// </summary>
        /// <param name="UserID">int</param>
        /// <returns>MailFooterRequestParam</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 24 2014</createdDate>
        public MailFooterRequest GetMailFooter(int UserID)
        {
            MailFooterRequest objMailFooterRequest = new MailFooterRequest();
            objdecisionPointRepository = new DecisionPointRepository();
            if (objdecisionPointRepository.GetMailFooter(UserID) != null)
            {
                objMailFooterRequest.UserID = objdecisionPointRepository.GetMailFooter(UserID).UserID;
                objMailFooterRequest.SignatureId = objdecisionPointRepository.GetMailFooter(UserID).SignatureId;
                objMailFooterRequest.SignatureName = objdecisionPointRepository.GetMailFooter(UserID).SignatureName;
                objMailFooterRequest.Signauture = objdecisionPointRepository.GetMailFooter(UserID).Signauture;
            }

            return objMailFooterRequest;
        }

        /// <summary>
        /// Get user signature
        /// </summary>
        /// <param name="UserID">int</param>
        /// <returns>Signature</returns>
        public string GetSignature(int UserID)
        {
            objdecisionPointRepository = new DecisionPointRepository();
            string Signature = objdecisionPointRepository.GetSignature(UserID);
            return Signature;
        }
        #endregion

        #region Mail matrix
        /// <summary>
        /// mailReminder
        /// </summary>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>15 Apr 2014</createdDate>
        public void mailReminder()
        {
            List<MailReminder> lstreceipentUserID = new List<MailReminder>();
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                lstreceipentUserID = objdecisionPointRepository.mailReminder();

                string body = string.Empty;
                string subject = string.Empty;
                for (int iCounter = 0; iCounter < lstreceipentUserID.Count; iCounter++)
                {
                    string signature = objdecisionPointRepository.GetSignature(lstreceipentUserID[iCounter].SenderId);
                    if (signature != null)
                    {
                        string[] sign = signature.Split(new string[] { "body>" }, StringSplitOptions.None);
                        signature = sign[1].Substring(0, sign[1].Length - 2);
                        subject = "Compliance Tracker";
                        //signature = sign[1].Replace("</", string.Empty);
                        body = "Dear " + lstreceipentUserID[iCounter].name + "<br/>" + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + " has requested to Join Compliance Tracker.Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='http://Web.chetu.com/DecisionPoint-EE?id=" + lstreceipentUserID[iCounter].ID + "'>click here</a> to get to log in page:</br><br/>Your username is:" + lstreceipentUserID[iCounter].UserId + "<br/> password is " + lstreceipentUserID[iCounter].Password + "</br>" + signature;

                        GetSmtpDetail(lstreceipentUserID[iCounter].EmailID, body, subject);
                    }

                }

            }
            catch
            {

                throw;
            }
        }


        #endregion

        #region Super Admin

        /// <summary>
        /// Get list of companies
        /// </summary>       
        /// <returns>GetCompanies</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Feb 4 2014</createdDate>
        public List<GetCompaniesResponse> GetCompanyList(bool isActive, string searh)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetCompanyList(isActive, searh).Select(x => new GetCompaniesResponse { BusniessID = x.BusniessID, BusniessName = x.BusniessName, Address = x.Address, UserId = x.UserId, UserName = x.UserName, UserType = x.UserType, Phone = x.Phone, InvitationStatus = x.InvitationStatus, VendorType = x.VendorType, InvitationDate = x.InvitationDate }).ToList();

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// method to get busniess credentail
        /// </summary>
        /// <param name="companyID">string</param>
        /// <param name="businessName">string</param>
        /// <returns>SuperAdminResponse</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Feb 10 2013</createdDate>
        public SuperAdminResponse LoginDetail(string companyID, string businessName)
        {

            try
            {
                SuperAdminResponse objLoginCredentailResponse = new SuperAdminResponse();
                SuperAdminResponseParam objsuperAdminResponseParam = new SuperAdminResponseParam();
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                objsuperAdminResponseParam = objdecisionPointRepository.LoginDetail(companyID, businessName);
                if (objsuperAdminResponseParam != null)
                {
                    objLoginCredentailResponse.UserID = objsuperAdminResponseParam.UserID;
                    objLoginCredentailResponse.Password = businessCryptography.base64Decode2(objsuperAdminResponseParam.Password);
                }
                return objLoginCredentailResponse;
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        #region Announcement
        /// <summary>
        /// Used for get all Announcement to show on login page
        /// </summary>
        public IEnumerable<AnnouncementResponse> GetAnnouncement()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<AnnouncementResponse> Announcement = objdecisionPointRepository.GetAnnouncement().Select(x => new AnnouncementResponse { Announcement = x.Announcement, Id = x.Id, UserId = x.UserId, IsActive = x.IsActive, ReleaseDate = x.ReleaseDate, IsClose = x.IsClose });
                return Announcement;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to add announcement & publish in database
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public int AddAnnoucement(string announcement, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddAnnoucement(announcement, UserId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to only add announcement with close status
        /// </summary>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public int AddCloseAnnoucement(string announcement, string status, int announcementId, int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddCloseAnnoucement(announcement, status, announcementId, UserId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to remove announcement
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int RemoveAnnoucement(int Id)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.RemoveAnnoucement(Id);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to update announcement
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="announcement"></param>
        /// <returns></returns>
        public int UpdateAnnoucement(int announcementId, string announcement)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateAnnoucement(announcementId, announcement);

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to enable & disable announcement 
        /// </summary>
        /// <param name="announcementId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public int DisaEnaAnnoucement(int announcementId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaAnnoucement(announcementId, isActive);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region Fee Structure
        /// <summary>
        /// Get company profile and fee detail
        /// </summary>
        /// <param name="userID">int</param>
        /// <param name="CompanyName">string</param>
        /// <returns>UserDashBoardResponseParam</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>12 jan 2014</createdDate>
        /// <modifyiedby>bobi</modifyiedby>
        /// <modifieddate>22 may 2014</modifieddate>
        public UserDashBoardResponse GetFeeDetail(int userID, string companyID)
        {
            try
            {
                objUserDashBoardResponse = new UserDashBoardResponse();
                objdecisionPointRepository = new DecisionPointRepository();
                objUserDashBoardResponseParam = new UserDashBoardResponseParam();
                objUserDashBoardResponseParam = objdecisionPointRepository.GetFeeDetail(userID, companyID);
                if (objUserDashBoardResponseParam != null)
                {
                    objUserDashBoardResponse.companyName = objUserDashBoardResponseParam.companyName;
                    objUserDashBoardResponse.fName = objUserDashBoardResponseParam.fName;
                    objUserDashBoardResponse.lName = objUserDashBoardResponseParam.lName;
                    objUserDashBoardResponse.mName = objUserDashBoardResponseParam.mName;
                    objUserDashBoardResponse.officePhone = objUserDashBoardResponseParam.officePhone;
                    objUserDashBoardResponse.directPhone = objUserDashBoardResponseParam.directPhone;
                    objUserDashBoardResponse.CompanyFee = objUserDashBoardResponseParam.CompanyFee;
                    objUserDashBoardResponse.PerOfficeStaffFee = objUserDashBoardResponseParam.PerOfficeStaffFee;
                    objUserDashBoardResponse.PerFieldStaffFee = objUserDashBoardResponseParam.PerFieldStaffFee;
                    objUserDashBoardResponse.PerICFee = objUserDashBoardResponseParam.PerICFee;
                    objUserDashBoardResponse.emailId = objUserDashBoardResponseParam.emailId;
                    objUserDashBoardResponse.IsInvoice = objUserDashBoardResponseParam.IsInvoice;
                    objUserDashBoardResponse.UserType = objUserDashBoardResponseParam.UserType;
                }
                return objUserDashBoardResponse;
            }
            catch
            {

                throw;
            }

        }



        /// <summary>
        /// Set fee detail
        /// </summary>
        /// <param name="objUserDashBoardRequestParam">objUserDashBoardRequestParam</param>
        /// <returns>byte</returns>
        /// <createdBy>Nielsh Dubey</createdBy>
        /// <createdDate>Feb 13 2014</createdDate>
        public byte SetFeeDetail(UserDashBoardRequest objUserDashBoardRequest)
        {
            byte result = 0;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                UserDashBoardRequestParam objUserDashBoardRequestParam = new UserDashBoardRequestParam();
                objUserDashBoardRequestParam.CompanyCode = objUserDashBoardRequest.CompanyCode;
                objUserDashBoardRequestParam.PerFieldStaffFee = objUserDashBoardRequest.PerFieldStaffFee;
                objUserDashBoardRequestParam.PerOfficeStaffFee = objUserDashBoardRequest.PerOfficeStaffFee;
                objUserDashBoardRequestParam.PerICFee = objUserDashBoardRequest.PerICFee;
                objUserDashBoardRequestParam.CompanyFee = objUserDashBoardRequest.CompanyFee;
                objUserDashBoardRequestParam.IsInvoice = objUserDashBoardRequest.IsInvoice;
                result = objdecisionPointRepository.SetFeeDetail(objUserDashBoardRequestParam);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Set Default fee detail
        /// </summary>
        /// <param name="objUserDashBoardRequestParam">objUserDashBoardRequestParam</param>
        /// <returns>byte</returns>
        /// <createdBy>Sumit Saurav</createdBy>
        /// <createdDate>Feb 25 2014</createdDate>
        public byte SetDefaultFeeDetail(UserDashBoardRequest objUserDashBoardRequest)
        {
            byte result = 0;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                UserDashBoardRequestParam objUserDashBoardRequestParam = new UserDashBoardRequestParam();
                objUserDashBoardRequestParam.PerFieldStaffFee = objUserDashBoardRequest.PerFieldStaffFee;
                objUserDashBoardRequestParam.PerOfficeStaffFee = objUserDashBoardRequest.PerOfficeStaffFee;
                objUserDashBoardRequestParam.PerICFee = objUserDashBoardRequest.PerICFee;
                objUserDashBoardRequestParam.CompanyFee = objUserDashBoardRequest.CompanyFee;
                objUserDashBoardRequestParam.IsInvoice = objUserDashBoardRequest.IsInvoice;
                result = objdecisionPointRepository.SetDefaultFeeDetail(objUserDashBoardRequestParam);
                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
            }
        }

        /// <summary>
        /// Get company profile and fee detail
        /// </summary>      
        /// <returns>UserDashBoardResponseParam</returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>25 feb 2014</createdDate>
        public UserDashBoardResponse GetDefaultFeeDetail()
        {
            try
            {
                objUserDashBoardResponse = new UserDashBoardResponse();
                objdecisionPointRepository = new DecisionPointRepository();

                objUserDashBoardResponse.CompanyFee = objdecisionPointRepository.GetDefaultFeeDetail().CompanyFee;
                objUserDashBoardResponse.PerOfficeStaffFee = objdecisionPointRepository.GetDefaultFeeDetail().PerOfficeStaffFee;
                objUserDashBoardResponse.PerFieldStaffFee = objdecisionPointRepository.GetDefaultFeeDetail().PerFieldStaffFee;
                objUserDashBoardResponse.PerICFee = objdecisionPointRepository.GetDefaultFeeDetail().PerICFee;
                objUserDashBoardResponse.LastUpdatedDate = objdecisionPointRepository.GetDefaultFeeDetail().LastUpdatedDate;
                objUserDashBoardResponse.IsInvoice = objdecisionPointRepository.GetDefaultFeeDetail().IsInvoice;
                return objUserDashBoardResponse;
            }
            catch
            {

                throw;
            }

        }
        #endregion

        #region Invite
        /// <summary>
        /// Used For get documents from Database as Per particular User
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        public IEnumerable<InviteResponse> GetInviteDetails(int UserId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<InviteResponse> invitsdetails = objdecisionPointRepository.GetInviteDetails(UserId, companyId).Select(x => new InviteResponse { Date = x.Date, CompanyName = x.CompanyName, Contact = x.Contact, EmailId = x.EmailId, Phone = x.Phone, RelationShip = x.RelationShip, Status = x.Status, CompanyId = x.CompanyId, UserId = x.UserId, TableId = x.TableId, FlowTableId = x.FlowTableId, DocFlow = x.DocFlow, Type = x.Type, UserType = x.UserType, Isdeleted = x.Isdeleted });
                return invitsdetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used For get documents from Database as Per particular User
        /// </summary>
        /// <param name="">UserId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        public IEnumerable<InviteResponse> GetICInvite(int UserId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<InviteResponse> invitsdetails = objdecisionPointRepository.GetICInvite(UserId).Select(x => new InviteResponse { Date = x.Date, CompanyName = x.CompanyName, Contact = x.Contact, EmailId = x.EmailId, Phone = x.Phone, RelationShip = x.RelationShip, Status = x.Status, CompanyId = x.CompanyId, UserId = x.UserId, TableId = x.TableId, FlowTableId = x.FlowTableId, DocFlow = x.DocFlow, Type = x.Type, UserType = x.UserType, Isdeleted = x.Isdeleted });
                return invitsdetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// InvitaionOperation
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="type">type</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int InvitaionOperation(int id, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int Isupdated = objdecisionPointRepository.InvitaionOperation(id, type);
                return Isupdated;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// UpdateFlow
        /// </summary>
        /// <param name="UniqueId">UniqueId</param>
        /// <param name="flowId">flowId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int UpdateFlow(int UniqueId, int flowId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int Isupdated = objdecisionPointRepository.UpdateFlow(UniqueId, flowId);
                return Isupdated;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// UpdateDocFlow
        /// </summary>
        /// <param name="UniqueId">UniqueId</param>
        /// <param name="flowId">flowId</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int UpdateDocFlow(UserDashBoardRequest objUserDashBoardRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objUserDashBoardRequestParam = new UserDashBoardRequestParam()
                {
                    companyName = objUserDashBoardRequest.CompanyName,
                    emailId = objUserDashBoardRequest.EmailId,
                    directPhone = objUserDashBoardRequest.DirectPhone,
                    UserId = objUserDashBoardRequest.UserId,
                    FlowId = objUserDashBoardRequest.FlowId,
                    FlowTblId = objUserDashBoardRequest.FlowTblId,
                    fName = objUserDashBoardRequest.FirstName,
                    lName = objUserDashBoardRequest.LastName
                };
                int Isupdated = objdecisionPointRepository.UpdateDocFlow(objUserDashBoardRequestParam);
                return Isupdated;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region IC client List
        /// <summary>
        /// method to get vendor list
        /// </summary>
        /// <param name="companyUserID"></param>
        /// <returns></returns>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>30 Dec 2013</createdDate>
        public IEnumerable<VendorsList> GetICClientList(int ID, bool type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<VendorsList> lstInternalstaffResponseParam = objdecisionPointRepository.GetICClientList(ID, type).Select(x => new VendorsList
                {
                    Id = x.Id,
                    contact = x.Contact,
                    vendor = x.Vendor,
                    emailId = x.emailId,
                    phone = x.phone,
                    service = x.service,
                    title = x.title,
                    stateAbbre = x.stateAbbre,
                    zipCode = x.zipCode,
                    CompanyId = x.companyId,
                    DocFTblId = x.DocFTblId
                }).ToList();
                return lstInternalstaffResponseParam;
            }
            catch
            {

                throw;
            }

        }
        #endregion

        #region Guide Instructions
        /// <summary>
        /// Used to save insurction in database
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="instruction">instruction</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>int</returns>
        public int SaveInstructions(int Id, string instruction)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveInstructions(Id, instruction);

            }
            catch
            {
                throw;
            }

        }
        /// <summary>
        /// used to get instruction in database
        /// </summary>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 Apr 2014</createdDate>
        /// <returns>IEnumerable</returns>
        public IEnumerable<GuideInstructionResponse> GetGuideInstruction()
        {
            IEnumerable<GuideInstructionResponse> objGuide = null;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objGuide = objdecisionPointRepository.GetGuideInstruction().Select(x => new GuideInstructionResponse { Id = x.Id, Instruction = x.Instruction, IsActive = x.IsActive }).ToList();
                return objGuide;

            }
            catch
            {
                throw;
            }

        }
        #endregion
        /// <summary>
        /// used to get state & cirt by zip code
        /// </summary>
        /// <param name="zip">zip</param>
        /// <createdby>Sumit</createdby>
        /// <createddate>1 may 2014</createddate>
        /// <returns>List</returns>
        public IList<ZipResponse> getStateCityByZip(string zip)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                var Res = objdecisionPointRepository.getStateCityByZip(zip).Select(x => new ZipResponse { ZipId = x.ZipId, ZipCode = x.ZipCode, StateAbbre = x.StateAbbre, CityName = x.CityName, StateName = x.StateName }).ToList();
                return Res;

            }
            catch
            {
                throw;
            }
        }
        #region Employe Requirment


        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<UserDashBoardResponse> GetReqiuredDocuments(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> requiredDocumentsDetails = objdecisionPointRepository.GetReqiuredDocuments(userId, companyId).Select(x => new UserDashBoardResponse { reqiuredDocId = x.reqiuredDocId, reqiuredDoctName = x.reqiuredDoctName, expirationDate = x.expirationDate, ReqType = x.ReqType, IsCompleted = x.IsCompleted, ServiceId = x.ServiceId, ServiceName = x.ServiceName, companyName = x.companyName, CreatorCompanyid = x.CreatorCompanyid });
                return requiredDocumentsDetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get reqiured documents
        /// </summary>
        /// <param name="reqdocid"></param>
        /// <returns></returns>
        public IEnumerable<SubmitReqDocRequest> GetReqDocBySender(SubmitReqDocRequest objSubmitReqDocRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objSubmitReqDocRequestParam = new SubmitReqDocRequestParam()
                {
                    UserId = objSubmitReqDocRequest.UserId,
                    CompanyId = objSubmitReqDocRequest.CompanyId,
                    IsActive = objSubmitReqDocRequest.IsActive,
                    JCRType = objSubmitReqDocRequest.JCRType
                };
                IEnumerable<SubmitReqDocRequest> ReqDocList = objdecisionPointRepository.GetReqDocBySender(objSubmitReqDocRequestParam).Select(x => new SubmitReqDocRequest
                {
                    title = x.title,
                    IsCompanyReq = x.IsCompanyReq,
                    IsPolicyReq = x.IsPolicyReq,
                    IsLicenseReq = x.IsLicenseReq,
                    IsExpDateReq = x.IsExpDateReq,
                    IsStateReq = x.IsStateReq,
                    UserPer = x.UserPer,
                    ReqDocId = x.ReqDocId,
                    ServiceId = x.ServiceId,
                    Service = x.Service,
                    ReqDocFor = x.ReqDocFor,
                    DoNotShow = x.DoNotShow,
                    Retake = x.Retake
                });

                return ReqDocList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get ack details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        public IList<string> GetAckByreqDocId(int reqDocId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<string> acklist = objdecisionPointRepository.GetAckByreqDocId(reqDocId);

                return acklist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get vendor type details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        public IList<int> GetReqDocVendorTypeByreqDocId(int reqDocId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<int> VTYPEist = objdecisionPointRepository.GetReqDocVendorTypeByreqDocId(reqDocId);

                return VTYPEist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get upload doc details as per req doc id
        /// </summary>
        /// <param name="reqdocid">reqdocid</param>
        /// <returns>IList<string></returns>
        /// <createdby>bobi</createdby>
        /// <creayeddate>4 june 2014</creayeddate>
        public IList<UploadDocDetailsRequest> GetUploadDocByreqDocId(int reqDocId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<UploadDocDetailsRequest> uploadlist = objdecisionPointRepository.GetUploadDocByreqDocId(reqDocId, companyId).Select(x => new UploadDocDetailsRequest { DocLoc = x.DocLoc, DocSeq = x.DocSeq, DocTblId = x.DocTblId }).ToList();

                return uploadlist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for submit the reqiurement documents of particular user
        /// </summary>
        /// <param name="objUserDashBoardResponse"></param>
        /// <returns></returns>
        public int SubmitRequireDocument(SubmitReqDocRequest objSubmitReqDocRequest)
        {
            try
            {
                objSubmitReqDocRequestParam = new SubmitReqDocRequestParam()
                {
                    //set the property in DAl layer class
                    title = objSubmitReqDocRequest.title,
                    UploadedDoc = objSubmitReqDocRequest.UploadedDoc,
                    IsExpDateReq = objSubmitReqDocRequest.IsExpDateReq,
                    IsCompanyReq = objSubmitReqDocRequest.IsCompanyReq,
                    IsLicenseReq = objSubmitReqDocRequest.IsLicenseReq,
                    Ack = objSubmitReqDocRequest.Ack,
                    IsStateReq = objSubmitReqDocRequest.IsStateReq,
                    UserPer = objSubmitReqDocRequest.UserPer,
                    IsPolicyReq = objSubmitReqDocRequest.IsPolicyReq,
                    ReqDocFor = objSubmitReqDocRequest.ReqDocFor,
                    ReqDocType = objSubmitReqDocRequest.ReqDocType,
                    ReqDocId = objSubmitReqDocRequest.ReqDocId,
                    UserId = objSubmitReqDocRequest.UserId,
                    DoNotShow = objSubmitReqDocRequest.DoNotShow,
                    CompanyId = objSubmitReqDocRequest.CompanyId,
                    Type = objSubmitReqDocRequest.Type,
                    ServicesId = objSubmitReqDocRequest.ServicesId,
                    ModifiedById = objSubmitReqDocRequest.ModifiedById,
                    VendorTypeId = objSubmitReqDocRequest.VendorTypeId,
                    Retake = objSubmitReqDocRequest.Retake
                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SubmitRequireDocument(objSubmitReqDocRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// GetReceiverRequiredDoc
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>june 5 2014</createddate>
        /// <returns>ReceiverReqDocResponseParam</returns>
        public IList<ReceiverReqDocResponse> GetReceiverRequiredDoc(int reqDocId, int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ReceiverReqDocResponse> GetRecvDoc = objdecisionPointRepository.GetReceiverRequiredDoc(reqDocId, userId, companyId).Select(x => new ReceiverReqDocResponse
                {
                    #region Parameter Assignments

                    title = x.title,
                    IsCompanyReq = x.IsCompanyReq,
                    IsExpDateReq = x.IsExpDateReq,
                    IsLicenseReq = x.IsLicenseReq,
                    IsPolicyReq = x.IsPolicyReq,
                    IsStateReq = x.IsStateReq,
                    UserPer = x.UserPer,
                    ReqDocFor = x.ReqDocFor,
                    ReqDocType = x.ReqDocType,
                    ReqDocId = x.ReqDocId,

                    CompanyName = x.CompanyName,
                    PolicyNumber = x.PolicyNumber,
                    LisenceNumber = x.LisenceNumber,
                    Stateabbre = x.StateAbbre,
                    ExpirationDate = x.ExpirationDate,
                    IsCompleted = x.IsCompleted,
                    Acknoledgment = x.Acknoledgment,
                    DocLoc = x.DocLoc,
                    DocSeqNo = x.DocSeqNo,
                    DNA = x.DNA,
                    DocReceiverUserId = x.DocReceiverUserId,
                    DocSenderUserId = x.DocSenderUserId,
                    DocUploadTblId = x.DocUploadTblId,
                    ReqDocReceiverId = x.ReqDocReceiverId
                    #endregion
                }).ToList();

                return GetRecvDoc;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// save details of required document of staff and ic
        /// </summary>
        /// <param name="objRequest">ReceiverReqDocRequest</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>june 6 2014</createddate>
        /// <returns>int saved or not?</returns>
        public int SetReceiverReqDocDetails(ReceiverReqDocRequest objRequest)
        {
            try
            {
                ReceiverReqDocRequestParam receiverReqDocRequestParam = new ReceiverReqDocRequestParam()
                {
                    CompanyId = objRequest.CompanyId,
                    CompanyName = objRequest.CompanyName,
                    PolicyNumber = objRequest.PolicyNumber,
                    LisenceNumber = objRequest.LisenceNumber,
                    ExpirationDate = objRequest.ExpirationDate,
                    StateAbbre = objRequest.StateAbbre,
                    UserId = objRequest.UserId,
                    ReqDocId = objRequest.ReqDocId,
                    DocLoc = objRequest.DocLoc,
                    IsCompleted = objRequest.IsCompleted,
                    VisitorId = objRequest.VisitorId,
                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SetReceiverReqDocDetails(receiverReqDocRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// GetReceiverReqDocAudit
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Nov 10 2014</CreatedDate>
        /// <returns>ReceiverReqDocResponse</returns>
        public IList<ReceiverReqDocResponse> GetReceiverReqDocAudit(int reqDocId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ReceiverReqDocResponse> GetRecvDoc = objdecisionPointRepository.GetReceiverReqDocAudit(reqDocId).Select(x => new ReceiverReqDocResponse
                {
                    #region Parameter Assignments

                    ReqDocId = x.ReqDocId,
                    CompanyName = x.CompanyName,
                    PolicyNumber = x.PolicyNumber,
                    LisenceNumber = x.LisenceNumber,
                    Stateabbre = x.StateAbbre,
                    ExpirationDate = x.ExpirationDate,
                    IsDocUploaded = x.IsDocUploaded,
                    VisitorName = x.VisitorName,
                    ModifyDate = x.ModifyDate,
                    StateName = x.StateName,
                    IsCompleted = x.IsCompleted,
                    #endregion
                }).ToList();

                return GetRecvDoc;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// MarkReceiverDocComplete
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <param name="userId">userId</param>
        /// <param name="companyId">companyId</param>
        ///  <createdby>sumit saurav</createdby>
        /// <createddate>june 6 2014</createddate>
        /// <returns>int</returns>
        public int MarkReceiverDocComplete(int reqDocId, int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int result = objdecisionPointRepository.MarkReceiverDocComplete(reqDocId, userId, companyId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for update the job comlimabxce reqiurement as per services changes
        /// </summary>
        /// <param name="servicesids"></param>
        /// <param name="objJobReqForNewHireRequestParam"></param>
        /// <returns></returns>
        /// <createdby>bobi</createdby>
        /// <createddate>27 June 2014</createddate>
        public int UpdateJobComplianceReqAsPerService(string servicesids, JobReqForNewHireRequest objJobReqForNewHireRequest)
        {

            try
            {
                JobReqForNewHireRequestParam objJobReqForNewHireRequestParam = new JobReqForNewHireRequestParam()
                {
                    inviteCompanyId = objJobReqForNewHireRequest.inviteCompanyId,
                    companyId = objJobReqForNewHireRequest.companyId,
                    userId = objJobReqForNewHireRequest.userId,
                    userType = objJobReqForNewHireRequest.userType,
                    parentuserId = objJobReqForNewHireRequest.parentuserId

                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateJobComplianceReqAsPerService(servicesids, objJobReqForNewHireRequestParam);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int InsertJobComplianceReqforNewHired(JobReqForNewHireRequest objJobReqForNewHireRequest)
        {
            try
            {
                JobReqForNewHireRequestParam objJobReqForNewHireRequestParam = new JobReqForNewHireRequestParam()
                {
                    inviteCompanyId = objJobReqForNewHireRequest.inviteCompanyId,
                    companyId = objJobReqForNewHireRequest.companyId,
                    userId = objJobReqForNewHireRequest.userId,
                    userType = objJobReqForNewHireRequest.userType,
                    parentuserId = objJobReqForNewHireRequest.parentuserId,
                    ICTypeId = objJobReqForNewHireRequest.ICTypeId

                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.InsertJobComplianceReqforNewHired(objJobReqForNewHireRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for perform the JCR operations
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationtype"></param>
        /// <returns>int</returns>
        /// <createdby>bobi</createdby>
        /// <createddate>5 july 2014</createddate>
        public int JCROperation(int id, int operationtype)
        {

            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.JCROperation(id, operationtype);
            }
            catch
            {

                throw;
            }
        }
        #endregion
        #endregion

        #region Recurring Payment
        /// <summary>
        /// get payment status of auser
        /// </summary>
        /// <param name="userId">user id</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>21/5/2014</createddate>
        /// <returns>bool type</returns>
        public bool getPaymentStatus(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                bool result = objdecisionPointRepository.getPaymentStatus(userId);
                return result;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// check wether Recurring Payment is Done or not for a month and year
        /// </summary>
        /// <param name="userId">user id of parent company</param>
        /// <param name="remark">remark for monthly or annual payment</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>july 16 2014</createddate>
        /// <returns>int</returns>
        public bool IsRecurringPaymentDone(int userId, string remark)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                bool result = objdecisionPointRepository.IsRecurringPaymentDone(userId, remark);
                return result;
                // return true;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// get staff ids of a company
        /// </summary>
        /// <param name="companyId">company id </param>
        /// <createdby>Sumit saurav</createdby>
        ///  <createdDate>17 July 2014</createdDate>
        /// <returns>list of staff</returns>
        public IList<int> GetAllStaff(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                var result = objdecisionPointRepository.GetAllStaff(companyId);
                return result;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get all business partner
        /// </summary>
        /// <param name="companyId">company id</param>
        ///<createdby>Sumit saurav</createdby>
        ///  <createdDate>17 July 2014</createdDate>
        /// <returns>list of all business partners</returns>
        public IList<int> GetAllBusinessPartners(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                var result = objdecisionPointRepository.GetAllBusinessPartners(companyId);
                return result;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get all ic with we Pay
        /// </summary>
        /// <param name="companyId">Company Id</param> 
        /// <createdby>Sumit saurav</createdby>
        ///  <createdDate>17 July 2014</createdDate>
        /// <returns>list of ic with we pay</returns>
        public IList<int> GetAllWePayIc(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                var result = objdecisionPointRepository.GetAllWePayIc(companyId);
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// check payment was done or not for checking is invoice true or false at the time of registration
        /// </summary>
        /// <param name="parentUserId">invitee company user id</param>
        /// <param name="userId">created by user id</param>
        /// <returns>int id of payment</returns>
        public int IsRegistrationPaymentDone(string parentUserId, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                int result = objdecisionPointRepository.IsRegistrationPaymentDone(parentUserId, userId);
                return result;
            }
            catch
            {

                throw;
            }
        }

        public int UpdateRecurringPayment(PaymentResponse paymentResponse, PaymentAmountResponse objPaymentAmountResponse)
        {
            //create customer and Update customer details in  recurring payment master
            int finalresult = 0;
            string stripeKey = System.Configuration.ConfigurationManager.AppSettings["StripeKey"].ToString();
            StripePayment payment = new StripePayment(stripeKey);

            int result = XamarinStripeCore.UpdateCustomerDetails(payment, paymentResponse);

            //then make recurring payment transaction

            RecurringPaymentResponseParam planDetails = null;
            planDetails = new RecurringPaymentResponseParam();
            objdecisionPointRepository = new DecisionPointRepository();
            //get recurring payment customer details

            planDetails = objdecisionPointRepository.getPlanDetails(Convert.ToInt32(objPaymentAmountResponse.userId));
            if (planDetails != null)
            {
                planDetails.Amount = Convert.ToInt32(paymentResponse.Amount);
                planDetails.UserId = objPaymentAmountResponse.userId;
                if (!paymentResponse.isMonthlyPaymentDone)
                {
                    planDetails.Remark = "Monthly Plan";
                    finalresult = XamarinStripeCore.AnnualMonthlyPaymentFailCharge(payment, planDetails);
                }
                if (!paymentResponse.isAnnualPaymentDone)
                {
                    planDetails.Remark = "Annual Plan";
                    finalresult = XamarinStripeCore.AnnualMonthlyPaymentFailCharge(payment, planDetails);
                }


            }
            return finalresult;

        }

        #endregion

        #region Configuration Setting
        /// <summary>
        /// Used for save the config details as per company
        /// </summary>
        /// <param name="objConfiguratonSettingRequest">objConfiguratonSettingRequest</param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>23 July 2014</createdDate>
        public int SaveConfigSetting(ConfiguratonSettingRequest objConfiguratonSettingRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objConfigurationSettingRequestParam = new ConfigurationSettingRequestParam()
                {
                    #region Config Properties
                    IsClient = objConfiguratonSettingRequest.IsClient,
                    IsIc = objConfiguratonSettingRequest.IsIc,
                    IsVendor = objConfiguratonSettingRequest.IsVendor,
                    IsCoveragearea = objConfiguratonSettingRequest.IsCoveragearea,
                    IsServices = objConfiguratonSettingRequest.IsServices,
                    IsClientOnMyProfile = objConfiguratonSettingRequest.IsClientOnMyProfile,
                    IsWebinarApply = objConfiguratonSettingRequest.IsWebinarApply,
                    IsScormApply = objConfiguratonSettingRequest.IsScormApply,
                    IsICFreeBasicMembership = objConfiguratonSettingRequest.IsICFreeBasicMembership,
                    IsICUsePackages = objConfiguratonSettingRequest.IsICUsePackages,
                    IsICInsApply = objConfiguratonSettingRequest.IsICInsApply,
                    IsICCommApply = objConfiguratonSettingRequest.IsICCommApply,
                    IsStaffCommApply = objConfiguratonSettingRequest.IsStaffCommApply,
                    IsStaffInsApply = objConfiguratonSettingRequest.IsStaffInsApply,

                    IsBgCheckForIC = objConfiguratonSettingRequest.IsBgCheckForIC,
                    IsLiceInsForIC = objConfiguratonSettingRequest.IsLiceInsForIC,
                    IsAddCreForIC = objConfiguratonSettingRequest.IsAddCreForIC,
                    IsCoverageAreaForIC = objConfiguratonSettingRequest.IsCoverageAreaForIC,
                    IsServicesForIC = objConfiguratonSettingRequest.IsAddCreForIC,
                    IsICClientOnMyProfile = objConfiguratonSettingRequest.IsICClientOnMyProfile,

                    IsAddCreForStaff = objConfiguratonSettingRequest.IsAddCreForStaff,
                    IsBgCheckForStaff = objConfiguratonSettingRequest.IsBgCheckForStaff,
                    IsCoverageAreaForStaff = objConfiguratonSettingRequest.IsCoverageAreaForStaff,
                    IsServicesForStaff = objConfiguratonSettingRequest.IsServicesForStaff,
                    IsStaffClientOnMyProfile = objConfiguratonSettingRequest.IsStaffClientOnMyProfile,
                    IsLicenseForStaff = objConfiguratonSettingRequest.IsLicenseForStaff,
                    IsClientNameApplyForIC = objConfiguratonSettingRequest.IsClientNameApplyForIC,
                    IsContractApply = objConfiguratonSettingRequest.IsContractApply,
                    #endregion
                    #region Other
                    UserId = objConfiguratonSettingRequest.UserId,
                    CompanyId = objConfiguratonSettingRequest.CompanyId,
                    CreatedBy = objConfiguratonSettingRequest.CreatedBy,
                    #endregion

                };
                return objdecisionPointRepository.SaveConfigSetting(objConfigurationSettingRequestParam);
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for get configuratiin setting details as per company
        /// </summary>
        /// <param name="companyId">companyId</param>
        /// <param name="UserId">UserId</param>
        /// <returns>ConfigurationSettingRequestParam</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>23 July 2014</createdDate>
        public ConfiguratonSettingRequest GetConfigSetting(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objConfigurationSettingRequestParam = new ConfigurationSettingRequestParam();
                objConfigurationSettingRequestParam = objdecisionPointRepository.GetConfigSetting(companyId);
                if (!object.Equals(objConfigurationSettingRequestParam, null))
                {
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest()
                    {
                        #region Config Properties
                        IsClient = objConfigurationSettingRequestParam.IsClient,
                        IsIc = objConfigurationSettingRequestParam.IsIc,
                        IsVendor = objConfigurationSettingRequestParam.IsVendor,
                        IsCoveragearea = objConfigurationSettingRequestParam.IsCoveragearea,
                        IsServices = objConfigurationSettingRequestParam.IsServices,
                        IsClientOnMyProfile = objConfigurationSettingRequestParam.IsClientOnMyProfile,
                        IsWebinarApply = objConfigurationSettingRequestParam.IsWebinarApply,
                        IsScormApply = objConfigurationSettingRequestParam.IsScormApply,
                        IsICFreeBasicMembership = objConfigurationSettingRequestParam.IsICFreeBasicMembership,
                        IsICUsePackages = objConfigurationSettingRequestParam.IsICUsePackages,
                        IsICInsApply = objConfigurationSettingRequestParam.IsICInsApply,
                        IsICCommApply = objConfigurationSettingRequestParam.IsICCommApply,
                        IsStaffInsApply = objConfigurationSettingRequestParam.IsStaffInsApply,
                        IsStaffCommApply = objConfigurationSettingRequestParam.IsStaffCommApply,
                        IsClientNameApplyForIC = objConfigurationSettingRequestParam.IsClientNameApplyForIC,
                        IsContractApply = objConfigurationSettingRequestParam.IsContractApply,

                        IsBgCheckForIC = objConfigurationSettingRequestParam.IsBgCheckForIC,
                        IsLiceInsForIC = objConfigurationSettingRequestParam.IsLiceInsForIC,
                        IsInterCheckForIC = objConfigurationSettingRequestParam.IsInterCheckForIC,
                        IsAddCreForIC = objConfigurationSettingRequestParam.IsAddCreForIC,
                        IsCoverageAreaForIC = objConfigurationSettingRequestParam.IsCoverageAreaForIC,
                        IsServicesForIC = objConfigurationSettingRequestParam.IsAddCreForIC,
                        IsICClientOnMyProfile = objConfigurationSettingRequestParam.IsICClientOnMyProfile,

                        IsAddCreForStaff = objConfigurationSettingRequestParam.IsAddCreForStaff,
                        IsBgCheckForStaff = objConfigurationSettingRequestParam.IsBgCheckForStaff,
                        IsInterCheckForStaff = objConfigurationSettingRequestParam.IsInterCheckForStaff,
                        IsCoverageAreaForStaff = objConfigurationSettingRequestParam.IsCoverageAreaForStaff,
                        IsServicesForStaff = objConfigurationSettingRequestParam.IsServicesForStaff,
                        IsStaffClientOnMyProfile = objConfigurationSettingRequestParam.IsStaffClientOnMyProfile,
                        IsLicenseForStaff = objConfigurationSettingRequestParam.IsLicenseForStaff,

                        UserId = objConfigurationSettingRequestParam.UserId,
                        CompanyId = objConfigurationSettingRequestParam.CompanyId,
                        CreatedBy = objConfigurationSettingRequestParam.CreatedBy,
                        tblId = objConfigurationSettingRequestParam.tblId,
                        #endregion

                    };
                }
                return objConfiguratonSettingRequest;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for get configuratiin setting details as per company
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <returns>ConfigurationSettingRequestParam</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>12 Sep 2014</createdDate>
        public IList<ConfiguratonSettingRequest> GetConfigSettingForIC(List<string> icClientList)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ConfiguratonSettingRequest> ConfigSettingCollection = objdecisionPointRepository.GetConfigSettingForIC(icClientList).Select(x => new ConfiguratonSettingRequest
                {
                    IsBgCheckForIC = x.IsBgCheckForIC,
                    IsLiceInsForIC = x.IsLiceInsForIC,
                    IsInterCheckForIC = x.IsInterCheckForIC,
                    IsAddCreForIC = x.IsAddCreForIC,
                    IsCoverageAreaForIC = x.IsCoverageAreaForIC,
                    IsServicesForIC = x.IsAddCreForIC,
                    IsICClientOnMyProfile = x.IsICClientOnMyProfile,
                    UserId = x.UserId,
                    CompanyId = x.CompanyId
                }).ToList();
                return ConfigSettingCollection;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region License & background



        /// <summary>
        /// Used for update the Bio Info
        /// </summary>
        /// <param name="bioInfo">bioInfo</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        ///<CreatedBy>Bobi</CreatedBy> 
        ///<createdDate>30 july 2014</createdDate>
        public int UpdateBioInfo(string bioInfo, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateBioInfo(bioInfo, userId);

            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// save details of license and insurance details for business entity and IC
        /// </summary>
        /// <param name="objRequest">ReceiverReqDocRequest</param>
        /// <createdby>Bobi</createdby>
        /// <createddate>Sep 4 2014</createddate>
        /// <returns>int saved or not?</returns>
        public int SetReceiverLicAndInsDetails(ReceiverReqDocRequest objRequest)
        {
            try
            {
                ReceiverReqDocRequestParam receiverReqDocRequestParam = new ReceiverReqDocRequestParam()
                {
                    CompanyId = objRequest.CompanyId,
                    CompanyName = objRequest.CompanyName,
                    PolicyNumber = objRequest.PolicyNumber,
                    LisenceNumber = objRequest.LisenceNumber,
                    ExpirationDate = objRequest.ExpirationDate,
                    StateAbbre = objRequest.StateAbbre,
                    UserId = objRequest.UserId,
                    ReqDocId = objRequest.ReqDocId,
                    DocLoc = objRequest.DocLoc,
                    IsCompleted = objRequest.IsCompleted,
                    Type = objRequest.Type,
                    Ack = objRequest.Ack,
                    Title = objRequest.Title,
                    ModifiedById = objRequest.ModifiedById
                };
                objdecisionPointRepository = new DecisionPointRepository();
                //called method used for save the license and insurance details
                return objdecisionPointRepository.SetReceiverLicAndInsDetails(receiverReqDocRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for insert the license verfication details
        /// </summary>
        /// <param name="objLicenseCheckRequestParam">objLicenseCheckRequestParam</param>
        /// <returns>int</returns>
        ///<CreatedBy>Bobi</CreatedBy> 
        ///<createdDate>31 july 2014</createdDate>
        public int InsertLicenseCheckSumm(LicenseCheckRequest objLicenseCheckRequest)
        {
            try
            {
                LicenseCheckRequestParam objLicenseCheckRequestParam = new LicenseCheckRequestParam()
                {
                    CompanyId = objLicenseCheckRequest.CompanyId,
                    UserId = objLicenseCheckRequest.UserId,
                    LicenseNumber = objLicenseCheckRequest.LicenseNumber,
                    LicenseType = objLicenseCheckRequest.LicenseType,
                    LName = objLicenseCheckRequest.LName,
                    FName = objLicenseCheckRequest.FName,
                    StateAbbre = objLicenseCheckRequest.StateAbbre,
                    County = objLicenseCheckRequest.County,
                    Zip = objLicenseCheckRequest.Zip,
                    City = objLicenseCheckRequest.City,
                    Phone = objLicenseCheckRequest.Phone,
                    EffectiveDate = objLicenseCheckRequest.EffectiveDate,
                    IssueDate = objLicenseCheckRequest.IssueDate,
                    ExpDate = objLicenseCheckRequest.ExpDate,
                };
                objdecisionPointRepository = new DecisionPointRepository();
                //called method used for save the license and insurance details
                return objdecisionPointRepository.InsertLicenseCheckSumm(objLicenseCheckRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for show alerts on my dashboard section
        /// </summary>
        /// <param name="objMyDashBoardRequestParam">objMyDashBoardRequestParam</param>
        /// <returns>MyDashBoardResponseParam</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>2 Aug 2014</createdDate>
        public MyDashBoardResponse GetLoginUserAlerts(MyDashBoardRequest objMyDashBoardRequest)
        {
            try
            {

                objdecisionPointRepository = new DecisionPointRepository();


                if (!object.Equals(objMyDashBoardRequest, null))
                {
                    objMyDashBoardRequestParam = new MyDashBoardRequestParam()
                    {
                        CompanyId = objMyDashBoardRequest.CompanyId,
                        SelectedDate = objMyDashBoardRequest.SelectedDate,
                        UserId = objMyDashBoardRequest.UserId,
                        UserType = objMyDashBoardRequest.UserType
                    };

                    objMyDashBoardResponseParam = new MyDashBoardResponseParam();
                    objMyDashBoardResponseParam = objdecisionPointRepository.GetLoginUserAlerts(objMyDashBoardRequestParam);
                    if (!object.Equals(objMyDashBoardResponseParam, null))
                    {
                        objMyDashBoardResponse = new MyDashBoardResponse();

                        if (!object.Equals(objMyDashBoardResponseParam.IncomFromCompCommAlerts, null))
                        {
                            objMyDashBoardResponse.IncomFromCompCommAlerts = objMyDashBoardResponseParam.IncomFromCompCommAlerts.Select(x => new UserDashBoardResponse { DocType = x.DocType, DueDate = x.DueDate, DocId = x.DocId }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.IncomFromOutCompCommAlerts, null))
                        {
                            objMyDashBoardResponse.IncomFromOutCompCommAlerts = objMyDashBoardResponseParam.IncomFromOutCompCommAlerts.Select(x => new UserDashBoardResponse { DocType = x.DocType, DueDate = x.DueDate, DocId = x.DocId }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.CompanyBasedCommAlerts, null))
                        {
                            objMyDashBoardResponse.CompanyBasedCommAlerts = objMyDashBoardResponseParam.CompanyBasedCommAlerts.Select(x => new UserDashBoardResponse { DocType = x.DocType, DueDate = x.DueDate, DocId = x.DocId, reqDocType = x.reqDocType }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.JCRDetailsAlerts, null))
                        {
                            objMyDashBoardResponse.JCRDetailsAlerts = objMyDashBoardResponseParam.JCRDetailsAlerts.Select(x => new ReceiverReqDocResponse { title = x.title, ReqDocId = x.ReqDocId }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.ContractsAlerts, null))
                        {
                            objMyDashBoardResponse.ContractsAlerts = objMyDashBoardResponseParam.ContractsAlerts.Select(x => new CreateContractResponse { Title = x.Title, ManagerName = x.ManagerName, ExpirationDate = x.ExpirationDate, ServiceName = x.ServiceName }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.ProfileDetailsAlerts, null))
                        {
                            objMyDashBoardResponse.ProfileDetailsAlerts = objMyDashBoardResponseParam.ProfileDetailsAlerts.Select(x => new ProfileAlertResponse { CoverageAreadetail = x.CoverageAreadetail, Serviecdetail = x.Serviecdetail, Titledetail = x.Titledetail, Staffdetail = x.Staffdetail, ICdetail = x.ICdetail }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.IncomInviteAlerts, null))
                        {
                            objMyDashBoardResponse.IncomInviteAlerts = objMyDashBoardResponseParam.IncomInviteAlerts.Select(x => new InviteResponse { Contact = x.Contact, EmailId = x.EmailId, TableId = x.TableId, Date = x.Date }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.OutgoICVendorInviteAlerts, null))
                        {
                            objMyDashBoardResponse.OutgoICVendorInviteAlerts = objMyDashBoardResponseParam.OutgoICVendorInviteAlerts.Select(x => new InviteResponse { Contact = x.Contact, EmailId = x.EmailId, TableId = x.TableId, Date = x.Date, UserType = x.UserType }).ToList();
                        }
                        if (!object.Equals(objMyDashBoardResponseParam.OutgoStaffInviteAlerts, null))
                        {
                            objMyDashBoardResponse.OutgoStaffInviteAlerts = objMyDashBoardResponseParam.OutgoStaffInviteAlerts.Select(x => new InviteResponse { Contact = x.Contact, EmailId = x.EmailId, TableId = x.TableId, Date = x.Date, UserType = x.UserType }).ToList();
                        }

                    }




                }
                return objMyDashBoardResponse;

            }
            catch
            {
                throw;
            }
        }

        ///// <summary>
        ///// Save and Update BackgroundCheckMaster
        ///// </summary>
        ///// <param name="objRequest">BackGrounndCheckMasterRequest</param>
        ///// <createdby>Sumit Saurav</createdby>
        ///// <createdDate>Aug 14 2014</createdDate>
        ///// <returns>int saved or not?</returns>
        //public int SetBackgroundCheckMaster(BackGroundCheckMasterRequest objRequest)
        //{
        //    try
        //    {
        //        objdecisionPointRepository = new DecisionPointRepository();
        //        BackGroundCheckMasterRequestParam objRequestParam = null;
        //        if (!object.Equals(objRequest, null))
        //        {
        //            objRequestParam = new BackGroundCheckMasterRequestParam()
        //            {
        //                BackgroundTitle = objRequest.BackgroundTitle,
        //                BackgroundSource = objRequest.BackgroundSource,
        //                Status = objRequest.Status,
        //                CreatedBy = objRequest.CreatedBy,
        //                Id = objRequest.Id,
        //            };
        //        }
        //        return objdecisionPointRepository.SetBackgroundCheckMaster(objRequestParam);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        /// <summary>
        /// GetBackgroundCheckMaster
        /// </summary>
        /// <createdby>Sumit Saurav</createdby>
        /// <createdDate>Aug 14 2014</createdDate>
        /// <returns>ienumerable BackGroundCheckMasterResponseParam</returns>
        public IEnumerable<BackGroundCheckMasterResponse> GetBackgroundMapping(BackGroundCheckMasterRequest objBackGroundCheckMasterRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objBackGroundCheckMasterRequestParam = new BackGroundCheckMasterRequestParam()
                {
                    CreatorCompanyId = objBackGroundCheckMasterRequest.CreatorCompanyId,
                    UserId = objBackGroundCheckMasterRequest.UserId,
                    CompanyId = objBackGroundCheckMasterRequest.CompanyId,
                    OperationType = objBackGroundCheckMasterRequest.OperationType
                };
                IEnumerable<BackGroundCheckMasterResponse> objResponse = objdecisionPointRepository.GetBackgroundMapping(objBackGroundCheckMasterRequestParam).Select(x => new BackGroundCheckMasterResponse
                {
                    BackgroundSource = x.BackgroundSource,
                    BackgroundTitle = x.BackgroundTitle,
                    Status = x.Status,
                    Id = x.Id,
                    Description = x.Description,
                    Source = x.Source,
                    CreatedDate = x.CreatedDate,
                    ReceivedDate = x.ReceivedDate,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    RequiredByCompanyName = x.RequiredByCompanyName,
                    SterlingPkgName = x.SterlingPkgName,
                    SterlingPkgId = x.SterlingPkgId,
                    SterlingOrderId = x.SterlingOrderId,
                    UpgradePkgIds = x.UpgradePkgIds,
                    BgCheckMasterTblId = x.BgCheckMasterTblId,
                    LicenseNumber = x.LicenseNumber,
                    LicenseExpDate = x.LicenseExpDate,
                    LicenseStateCode = x.LicenseStateCode,
                    LicenseCountryCode = x.LicenseCountryCode,
                    StateAbbre = x.StateAbbre,
                    MappedSterlingPkg = x.MappedSterlingPkg
                });
                return objResponse;



            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// GetBackgroundCheckPackages
        /// </summary>
        /// <createdby>Bobi</createdby>
        /// <createdDate>Jan 31 2015</createdDate>
        /// <returns>IList BackGroundCheckMasterResponse</returns>
        public IList<BackGroundCheckMasterResponse> GetBackgroundCheckPackages()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<BackGroundCheckMasterResponse> objResponse = objdecisionPointRepository.GetBackgroundCheckPackages().Select(x => new BackGroundCheckMasterResponse
                {
                    BackgroundTitle = x.BackgroundTitle,
                    Id = x.Id,
                    Source = x.Source,
                    Description = x.Description,
                    PacakgeCost = x.PacakgeCost,
                    SterlingPkgName = x.SterlingPkgName,
                    PacakgeType = x.PacakgeType,
                    PacakgeGroup = x.PacakgeGroup
                }).ToList();
                return objResponse;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// GetBackgroundCheckPackages
        /// </summary>
        /// <createdby>Bobi</createdby>
        /// <createdDate>Jan 31 2015</createdDate>
        /// <returns>IList BackGroundCheckMasterResponse</returns>
        public IList<BackGroundCheckMasterResponse> GetPackagesDetails()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<BackGroundCheckMasterResponse> objResponse = objdecisionPointRepository.GetPackagesDetails().Select(x => new BackGroundCheckMasterResponse
                {
                    BackgroundTitle = x.BackgroundTitle,
                    Id = x.Id,
                    Source = x.Source,
                    Description = x.Description,
                    PacakgeCost = x.PacakgeCost,
                    SterlingPkgName = x.SterlingPkgName,
                    PacakgeType = x.PacakgeType,
                    Content = x.Content,
                    PacakgeDetailCost = x.PacakgeDetailCost
                }).ToList();
                return objResponse;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to save the specific reqiurement
        /// </summary>
        /// <param name="objLicInsRequest">LicenseInsuranceRequest</param>
        /// <returns>int</returns>
        /// <createdBy>Sumit Saurav</createdBy>
        /// <createdDate>Aug 21 2014</createdDate>
        public int SaveProfessionalLicense(LicenseInsuranceRequest objLicInsRequest)
        {
            try
            {
                objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                {
                    //set the property in BAL layer class
                    Title = objLicInsRequest.Title,
                    Acknowleagement = objLicInsRequest.Acknowleagement,
                    UserId = objLicInsRequest.UserId,
                    CompanyId = objLicInsRequest.CompanyId,
                    ICTypeIds = objLicInsRequest.ICTypeStaffTitleIds,
                    ClientIds = objLicInsRequest.ClientIds,
                    LicenseType = objLicInsRequest.LicenseType,
                    OperationType = objLicInsRequest.OperationType,
                    Source = objLicInsRequest.Source,
                    ReqDocId = objLicInsRequest.ReqDocId,
                    ICTypeId = objLicInsRequest.ICTypeId,
                    LicenseNumber = objLicInsRequest.LicenseNumber,
                    StateId = objLicInsRequest.StateId,
                    ExpirationDate = objLicInsRequest.ExpirationDate,
                    IsStaffTitle = objLicInsRequest.IsStaffTitle
                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveProfessionalLicense(objLicenseInsuranceRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used to save the additional reqiurement
        /// </summary>
        /// <param name="objLicInsRequest">LicenseInsuranceRequest</param>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Feb 06 2015</createdDate>
        public int SaveAdditionalReq(LicenseInsuranceRequest objLicInsRequest)
        {
            try
            {
                objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                {
                    //set the property in BAL layer class
                    Title = objLicInsRequest.Title,
                    Acknowleagement = objLicInsRequest.Acknowleagement,
                    UserId = objLicInsRequest.UserId,
                    CompanyId = objLicInsRequest.CompanyId,
                    ICTypeIds = objLicInsRequest.ICTypeStaffTitleIds,
                    ClientIds = objLicInsRequest.ClientIds,
                    Review = objLicInsRequest.Review,
                    OperationType = objLicInsRequest.OperationType,
                    UploadedBy = objLicInsRequest.UploadedBy,
                    ReqDocId = objLicInsRequest.ReqDocId,
                    UploadedDoc = objLicInsRequest.UploadedDoc,
                    IsStaffTitle = objLicInsRequest.IsStaffTitle,
                    ExpirationDate = objLicInsRequest.ExpirationDate
                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveAdditionalReq(objLicenseInsuranceRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to save the specific reqiurement
        /// </summary>
        /// <param name="objLicInsRequest">LicenseInsuranceRequest</param>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Feb 2 2015</createdDate>
        public int SaveInsurance(LicenseInsuranceRequest objLicInsRequest)
        {
            try
            {
                objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                {
                    //set the property in BAL layer class
                    Title = objLicInsRequest.Title,
                    Acknowleagement = objLicInsRequest.Acknowleagement,
                    UserId = objLicInsRequest.UserId,
                    CompanyId = objLicInsRequest.CompanyId,
                    ICTypeIds = objLicInsRequest.ICTypeStaffTitleIds,
                    ClientIds = objLicInsRequest.ClientIds,
                    InsuranceType = objLicInsRequest.InsuranceType,
                    OperationType = objLicInsRequest.OperationType,
                    ReqDocId = objLicInsRequest.ReqDocId,
                    Source = objLicInsRequest.Source,
                    InsuranceVerType = objLicInsRequest.InsuranceVerType,
                    CompanyName = objLicInsRequest.CompanyName,
                    PolicyNumber = objLicInsRequest.PolicyNumber,
                    Aggregate = objLicInsRequest.Aggregate,
                    SingleOccurance = objLicInsRequest.SingleOccurance,
                    ExpirationDate = objLicInsRequest.ExpirationDate
                };
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SaveInsurance(objLicenseInsuranceRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get GetProfessionalLicenseMaster details 
        /// </summary>
        /// <param name="createdBy">created by </param>
        /// <param name="userId">user id</param>
        /// <param name="backMasterId">master Id</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Feb 09 2015</CreatedDate>
        /// <returns>list of professional license details</returns>
        public IList<LicenseInsuranceResponse> GetProfessionalLicenseMaster(string creatorCompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<LicenseInsuranceResponse> profLicenseMaster = objdecisionPointRepository.GetProfessionalLicenseMaster(creatorCompanyId).Select(x => new LicenseInsuranceResponse
                {
                    LicInsId = x.LicInsId,
                    VendorTypeId = x.VendorTypeId,
                    VendorType = x.VendorType,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByUserId = x.RequiredByUserId,
                    LicenseType = x.LicenseType,
                    UserId = x.UserId,
                    CompanyId = x.CompanyId,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    Source = x.Source,
                    IsActive = x.IsActive,
                    IsStaffTitle = x.IsStaffTitle
                }).ToList();
                return profLicenseMaster;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get GetInsuranceMaster details 
        /// </summary>
        /// <param name="createdBy">created by </param>
        /// <param name="userId">user id</param>
        /// <param name="backMasterId">master Id</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Feb 09 2015</CreatedDate>
        /// <returns>list of professional license details</returns>
        public IList<LicenseInsuranceResponse> GetInsuranceMaster(string creatorCompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<LicenseInsuranceResponse> profLicenseMaster = objdecisionPointRepository.GetInsuranceMaster(creatorCompanyId).Select(x => new LicenseInsuranceResponse
                {
                    LicInsId = x.LicInsId,
                    VendorTypeId = x.VendorTypeId,
                    VendorType = x.VendorType,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByUserId = x.RequiredByUserId,
                    InsuranceType = x.InsuranceType,
                    UserId = x.UserId,
                    CompanyId = x.CompanyId,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    IsActive = x.IsActive,
                    InsuranceVerType = x.InsuranceVerType
                }).ToList();
                return profLicenseMaster;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get GetAdditionareqMaster details 
        /// </summary>
        /// <param name="createdBy">created by </param>
        /// <param name="userId">user id</param>
        /// <param name="backMasterId">master Id</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Feb 09 2015</CreatedDate>
        /// <returns>list of professional license details</returns>
        public IList<LicenseInsuranceResponse> GetAdditionareqMaster(string creatorCompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<LicenseInsuranceResponse> profLicenseMaster = objdecisionPointRepository.GetAdditionareqMaster(creatorCompanyId).Select(x => new LicenseInsuranceResponse
                {
                    LicInsId = x.LicInsId,
                    VendorTypeId = x.VendorTypeId,
                    VendorType = x.VendorType,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByUserId = x.RequiredByUserId,
                    UploadedDoc = x.UploadedDoc,
                    title = x.title,
                    Review = x.Review,
                    UserId = x.UserId,
                    CompanyId = x.CompanyId,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    IsActive = x.IsActive,
                    IsStaffTitle = x.IsStaffTitle
                }).ToList();
                return profLicenseMaster;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used to get ack details as per License Insurance id
        /// </summary>
        /// <param name="licInsId">licInsId</param>
        /// <returns>IList<string></returns>
        /// <createdby>Sumit Saurav</createdby>
        /// <creayeddate>26 Aug 2014</creayeddate>
        public IList<string> GetAckByLicInsId(int tblMapId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<string> acklist = objdecisionPointRepository.GetAckByLicInsId(tblMapId, type);

                return acklist;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used to get upload doc details as per req doc id
        /// </summary>
        /// <param name="licInsId">licInsId</param>
        /// <returns>IList<string></returns>
        /// <createdby>Sumit Saurav</createdby>
        /// <creayeddate>26 Aug 2014</creayeddate>
        public IList<UploadDocDetailsRequest> GetUploadDocByLicInsId(int tblMapId, int type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<UploadDocDetailsRequest> uploadlist = objdecisionPointRepository.GetUploadDocByLicInsId(tblMapId, type).Select(x => new UploadDocDetailsRequest { DocLoc = x.DocLoc, DocSeq = x.DocSeq, DocTblId = x.DocTblId, UploadedDocDate = x.UploadedDocDate }).ToList();

                return uploadlist;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="companyId"></param>
        /// <param name="licInsId"></param>
        /// <returns>IEnumerable<ReceiverReqDocResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>Sep a 2014</createddate> 
        public IEnumerable<ReceiverReqDocResponse> GetLicAndInsAsPerUnqiueId(int licInsId, int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ReceiverReqDocResponse> licAndInsAsPerUnqiueIdDetails = objdecisionPointRepository.GetLicAndInsAsPerUnqiueId(licInsId, userId, companyId).Select(x => new ReceiverReqDocResponse
                {
                    #region Parameter Assignments

                    title = x.title,
                    IsCompanyReq = x.IsCompanyReq,
                    IsExpDateReq = x.IsExpDateReq,
                    IsLicenseReq = x.IsLicenseReq,
                    IsPolicyReq = x.IsPolicyReq,
                    IsStateReq = x.IsStateReq,
                    UserPer = x.UserPer,
                    ReqDocFor = x.ReqDocFor,
                    ReqDocType = x.ReqDocType,
                    ReqDocId = x.ReqDocId,
                    CompanyName = x.CompanyName,
                    PolicyNumber = x.PolicyNumber,
                    LisenceNumber = x.LisenceNumber,
                    Stateabbre = x.StateAbbre,
                    ExpirationDate = x.ExpirationDate,
                    IsCompleted = x.IsCompleted,
                    Acknoledgment = x.Acknoledgment,
                    DocLoc = x.DocLoc,
                    DocSeqNo = x.DocSeqNo,
                    DocSenderUserId = x.DocSenderUserId,
                    DocReceiverUserId = x.DocReceiverUserId,
                    DocUploadTblId = x.DocUploadTblId
                    #endregion
                }).ToList();

                return licAndInsAsPerUnqiueIdDetails;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns>IEnumerable<UserDashBoardResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>Sep a 2014</createddate> 
        public IEnumerable<UserDashBoardResponse> GetLicAndInsAsPerVendorType(int userId, string companyId, List<int> vendorTypeIds)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> licAndInsAsDetails = objdecisionPointRepository.GetLicAndInsAsPerVendorType(userId, companyId, vendorTypeIds).
                    Select(x => new UserDashBoardResponse
                    {
                        reqiuredDocId = x.reqiuredDocId,
                        reqiuredDoctName = x.reqiuredDoctName,
                        expirationDate = x.expirationDate,
                        ReqType = x.ReqType,
                        IsCompleted = x.IsCompleted,
                        ServiceId = x.ServiceId,
                        ServiceName = x.ServiceName,
                        companyName = x.companyName,
                        CreatorCompanyid = x.CreatorCompanyid,
                        IsElectronically = x.IsElectronically,
                        completeDate = x.completeDate,
                        LicCreatedDate = x.LicCreatedDate,
                        StateName = x.StateName,
                        LicenseNumber = x.LicenseNumber,
                        PolicyNumber = x.PolicyNumber
                    });
                return licAndInsAsDetails;
            }
            catch
            {
                throw;
            }
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
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.ActiveOrDeactivateJCR(tblId, status, type);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Reports
        /// <summary>
        /// Used for get the all users details which exist in system
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateto"></param>
        /// <param name="companyId"></param>
        /// <returns>IList<ReportResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>25 Aug 2014</createddate>
        public IList<ReportResponse> GetAllUsersDetailsInSystem(ReportResponse objReportResponse)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objReportResponseParam = new ReportResponseParam()
                {
                    DateFrom = objReportResponse.DateFrom,
                    DateTo = objReportResponse.DateTo,
                    Status = objReportResponse.Status,
                    CompanyId = objReportResponse.CompanyId,
                    UserId = objReportResponse.UserId
                };
                IList<ReportResponse> objResponse = objdecisionPointRepository.GetAllUsersDetailsInSystem(objReportResponseParam).Select(q => new ReportResponse
                {
                    ChildUserName = q.ChildUserName,
                    ChildID = q.ChildID,
                    ChildEmailId = q.ChildEmailId,
                    ChildPhoneNo = q.ChildPhoneNo,
                    Status = q.Status,
                    ParentUserName = q.ParentUserName,
                }).ToList();
                return objResponse;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for get the all saved report details 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>IList<ReportResponse></returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>25 Aug 2014</createddate>
        public IList<ReportResponse> GetSavedReportDetails(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ReportResponse> objResponse = objdecisionPointRepository.GetSavedReportDetails(companyId).Select(q => new ReportResponse
                {
                    DateFrom = q.DateFrom,
                    DateTo = q.DateTo,
                    Status = q.Status
                }).ToList();
                return objResponse;
            }
            catch
            {

                throw;
            }
        }

        public int SavedReportDetails(ReportResponse objReportResponse)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objReportResponseParam = new ReportResponseParam()
                {
                    DateFrom = objReportResponse.DateFrom,
                    DateTo = objReportResponse.DateTo,
                    Status = objReportResponse.Status,
                    CompanyId = objReportResponse.CompanyId,
                    UserId = objReportResponse.UserId
                };
                return objdecisionPointRepository.SavedReportDetails(objReportResponseParam);

            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Webinar

        /// <summary>
        /// get all webinar users
        /// </summary>
        /// <param name="companyId">companyId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sep 04 2014</CreatedDate>        
        /// <returns>WebinarUsersResponse</returns>
        public IList<WebinarUsersResponse> getAllWebinarUsers(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<WebinarUsersResponse> userList = objdecisionPointRepository.getAllWebinarUsers(companyId).Select(x => new WebinarUsersResponse
                {
                    UserId = x.UserId,
                    CompanyId = x.CompanyId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Role = x.Role,
                }).ToList();

                return userList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get list of all company with webinar details
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 04 2014</CreatedDate>
        /// <returns>WebinarUsersResponse list</returns>
        public IList<WebinarUsersResponse> GetAllCompanyAdmin()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                IList<WebinarUsersResponse> userList = objdecisionPointRepository.GetAllCompanyAdmin().Select(x => new WebinarUsersResponse
                {
                    Id = x.Id,
                    CompanyId = x.CompanyId,
                    UserId = x.UserId,
                    Email = x.Email,
                    CompanyName = x.CompanyName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    OrganiserId = x.OrganiserId,
                    AppKey = x.AppKey,
                    UserName = x.UserName,
                    Password = x.Password == null ? x.Password : businessCryptography.base64Decode2(x.Password),
                    ContactName = x.FirstName + Shared.SingleSpace + x.LastName,
                    IsActive = x.IsActive

                }).ToList();

                return userList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// save annd update webinar user details
        /// </summary>
        /// <param name="objWebinarUsersResponse">WebinarUsersResponse</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 9 2014</CreatedDate>
        /// <returns>int</returns>
        public int setWebinarUserDetails(WebinarUsersResponse objWebinarUsersResponse)
        {
            try
            {
                businessCryptography = new BusinessCryptography();
                objdecisionPointRepository = new DecisionPointRepository();
                WebinarUsersResponseParam objWebinarUsersResponseParam = new WebinarUsersResponseParam()
                {
                    Id = objWebinarUsersResponse.Id,
                    UserId = objWebinarUsersResponse.UserId,
                    UserName = objWebinarUsersResponse.UserName,
                    Password = objWebinarUsersResponse.Password == null ? objWebinarUsersResponse.Password : businessCryptography.base64Encode(objWebinarUsersResponse.Password),
                    AppKey = objWebinarUsersResponse.AppKey,
                    OrganiserId = objWebinarUsersResponse.OrganiserId,
                    IsActive = objWebinarUsersResponse.IsActive,
                    CreatedBy = objWebinarUsersResponse.CreatedBy,

                };
                return objdecisionPointRepository.setWebinarUserDetails(objWebinarUsersResponseParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get all webinar organisers
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 17 2014</CreatedDate>
        /// <returns>WebinarUsersResponse</returns>
        public IList<WebinarUsersResponse> GetWebinarOrganiser(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                IList<WebinarUsersResponse> userList = objdecisionPointRepository.GetWebinarOrganiser(userId).Select(x => new WebinarUsersResponse
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    OrganiserId = x.OrganiserId,
                    AppKey = x.AppKey,
                    UserName = x.UserName,
                    Password = x.Password == null ? x.Password : businessCryptography.base64Decode2(x.Password)
                }).ToList();

                return userList;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Bcakground check
        /// <summary>
        /// save and update background mapping details
        /// </summary>
        /// <param name="objBackGroundCheckMasterRequest">BackGroundCheckMasterRequest</param> 
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 13 2014</CreatedDate>
        /// <returns>int</returns>
        public int SetBackgroundMapping(BackGroundCheckMasterRequest objBackGroundCheckMasterRequest)
        {
            try
            {
                objBackGroundCheckMasterRequestParam = new BackGroundCheckMasterRequestParam()
                {
                    ICTypeStaffTitleIds = objBackGroundCheckMasterRequest.ICTypeStaffTitleIds,
                    BGCheckId = objBackGroundCheckMasterRequest.BGCheckId,
                    ClientIds = objBackGroundCheckMasterRequest.ClientIds,
                    ReceivedDate = objBackGroundCheckMasterRequest.ReceivedDate,
                    ModifiedBy = objBackGroundCheckMasterRequest.ModifiedBy,
                    UserId = objBackGroundCheckMasterRequest.UserId,
                    CompanyId = objBackGroundCheckMasterRequest.CompanyId,
                    OperationType = objBackGroundCheckMasterRequest.OperationType,
                    Source = objBackGroundCheckMasterRequest.Source,
                    PayType = objBackGroundCheckMasterRequest.PayType,
                    RequirmentType = objBackGroundCheckMasterRequest.RequirmentType,
                    StateAbbre = objBackGroundCheckMasterRequest.StateAbbre
                };
                objdecisionPointRepository = new DecisionPointRepository();
                //called method used for save the license and insurance details
                return objdecisionPointRepository.SetBackgroundMapping(objBackGroundCheckMasterRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get GetBackgroundMapping details 
        /// </summary>
        /// <param name="createdBy">created by </param>
        /// <param name="userId">user id</param>
        /// <param name="backMasterId">master Id</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 12 2014</CreatedDate>
        /// <returns>list of background details</returns>
        public IEnumerable<BackGroundCheckMasterRequest> GetBackgroundCheckMaster(string creatorCompanyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<BackGroundCheckMasterRequest> backList = objdecisionPointRepository.GetBackgroundCheckMaster(creatorCompanyId).Select(x => new BackGroundCheckMasterRequest
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CompanyId = x.CompanyId,
                    ReqCompanyName = x.ReqCompanyName,
                    ICType = x.ICType,
                    BackgroundTitle = x.BackgroundTitle,
                    RequiredByUserId = x.RequiredByUserId,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    ICTypeId = x.ICTypeId,
                    BGCheckId = x.BGCheckId,
                    IsActive = x.IsActive,
                    BGCheckPkgId = x.BGCheckPkgId
                }).ToList();
                return backList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// GetBackgroundUploadDocById
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 13 2014</CreatedDate>
        /// <returns>UploadDocDetailsRequest</returns>
        public IList<UploadDocDetailsRequest> GetBackgroundUploadDocById(int reqDocId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<UploadDocDetailsRequest> backList = objdecisionPointRepository.GetBackgroundUploadDocById(reqDocId).Select(x => new UploadDocDetailsRequest
                {
                    DocLoc = x.DocLoc,
                    DocSeq = x.DocSeq,
                    DocTblId = x.DocTblId
                }).ToList();

                return backList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// GetBackgroundByVisitorId
        /// </summary>
        /// <param name="createdBy">createdBy</param>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Sept 16 2014</CreatedDate>
        /// <returns>BackGroundCheckMasterRequest</returns>
        public IList<BackGroundCheckMasterRequest> GetBackgroundByVisitorId(int createdBy, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<BackGroundCheckMasterRequest> backList = objdecisionPointRepository.GetBackgroundByVisitorId(createdBy, userId).Select(x => new BackGroundCheckMasterRequest
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    MasterId = x.MasterId,
                    BackgroundCheckStatus = x.BackgroundCheckStatus,
                }).ToList();

                return backList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Set IC Client Permissions
        /// </summary>
        /// <param name="createdBy">createdBy</param>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Jan 9 2015</CreatedDate>
        /// <returns>int</returns>
        public int SetICClientPermissions(ICClientPermissionRequest objICClientPermissionRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                ICClientPermissionRequestParam objICClientPermissionRequestParam = new ICClientPermissionRequestParam()
                {
                    ICCompanyId = objICClientPermissionRequest.ICCompanyId,
                    ICUserId = objICClientPermissionRequest.ICUserId,
                    IsVisible = objICClientPermissionRequest.IsVisible,
                    VisibleTo = objICClientPermissionRequest.VisibleTo
                };

                return objdecisionPointRepository.SetICClientPermissions(objICClientPermissionRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get IC Client Permissions
        /// </summary>
        /// <param name="createdBy">createdBy</param>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Jan 9 2015</CreatedDate>
        /// <returns>int</returns>
        public IList<ICClientPermissionRequest> GetICClientPermissions(ICClientPermissionRequest objICClientPermissionRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                ICClientPermissionRequestParam objICClientPermissionRequestParam = new ICClientPermissionRequestParam()
                {
                    ICCompanyId = objICClientPermissionRequest.ICCompanyId,
                    ICUserId = objICClientPermissionRequest.ICUserId
                };

                return objdecisionPointRepository.GetICClientPermissions(objICClientPermissionRequestParam).Select(x => new ICClientPermissionRequest { VisibleTo = x.VisibleTo, IsVisible = x.IsVisible }).ToList();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region ResendMail

        public IList<InvitationMailSendResponse> ResendInvitationMail(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<InvitationMailSendResponse> objInvitationMailSendResponse = objdecisionPointRepository.GetUserDetailsForResendingMail(userId).Select(x => new InvitationMailSendResponse
                {

                    UserId = x.UserId,
                    ParentUserId = x.ParentUserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmailId = x.EmailId,
                    BusinessName = x.BusinessName,
                    Password = x.Password,
                    ParentCompanyId = x.ParentCompanyId
                }).ToList();

                return objInvitationMailSendResponse;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Contract Management
        /// <summary>
        /// GetContractType
        /// </summary>
        /// <param name="type">type</param>
        /// <param name="ID">ID</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 Nov 2014</CreatedDate>
        /// <returns>CompanyDashBoardResponse</returns>
        public IEnumerable<CompanyDashBoardResponse> GetContractType(string type, string ID)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<CompanyDashBoardResponse> contractDetails = objdecisionPointRepository.GetContractType(type, ID).Select(x => new CompanyDashBoardResponse { ContractTypeName = x.ContractTypeName, isDeleted = x.isDeleted, isActive = x.isActive, id = x.id }).ToList();
                return contractDetails;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// AddContractType
        /// </summary>
        /// <param name="contractType">contractType</param>
        /// <param name="companyId">companyId</param>
        /// <param name="userId">userId</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 Nov 2014</CreatedDate>
        /// <returns>int</returns>
        public int AddContractType(string contractType, string companyId, int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.AddContractType(contractType, companyId, userId);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// UpdateContractType
        /// </summary>
        /// <param name="contractTypeId">contractTypeId</param>
        /// <param name="contractType">contractType</param>
        /// <param name="companyId">companyId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 Nov 2014</CreatedDate>
        /// <returns>int</returns>
        public int UpdateContractType(int contractTypeId, string contractType, String companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.UpdateContractType(contractTypeId, contractType, companyId);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// DisaEnaContractType
        /// </summary>
        /// <param name="contractId">contractId</param>
        /// <param name="isActive">isActive</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 Nov 2014</CreatedDate>
        /// <returns>int</returns>
        public int DisaEnaContractType(int contractId, bool isActive)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DisaEnaContractType(contractId, isActive);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Set Create Contract
        /// </summary>
        /// <param name="createContractRequest">createContractRequest</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 01 2014</CreatedDate>
        /// <returns>int</returns>
        public int SetCreateContract(CreateContractRequest createContractRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CreateContractRequestParam objCreateContractRequestParam = new CreateContractRequestParam()
                {
                    ManagerName = createContractRequest.ManagerName,
                    OwnerId = createContractRequest.OwnerId,
                    ContractWithId = createContractRequest.ContractWithId,
                    ExecutedById = createContractRequest.ExecutedById,
                    ExecutedDate = createContractRequest.ExecutedDate,
                    ExpirationDate = createContractRequest.ExpirationDate,
                    ContractDate = createContractRequest.ContractDate,
                    ExpirationDateReminder = createContractRequest.ExpirationDateReminder,
                    GeneralComments = createContractRequest.GeneralComments,
                    Paragraph = createContractRequest.Paragraph,
                    Section = createContractRequest.Section,
                    SubSection = createContractRequest.SubSection,
                    Notes = createContractRequest.Notes,
                    EventDate = createContractRequest.EventDate,
                    EventDateReminder = createContractRequest.EventDateReminder,
                    CreatorCompanyId = createContractRequest.CreatorCompanyId,
                    ContractorCompanyId = createContractRequest.ContractorCompanyId,
                    IsActive = createContractRequest.IsActive,
                    IsDeleted = createContractRequest.IsDeleted,
                    IsCompleted = createContractRequest.IsCompleted,
                    CreatedBy = createContractRequest.CreatedBy,
                    ServiceList = createContractRequest.ServiceList,
                    FilePathStr = createContractRequest.FilePathStr,
                    Role = createContractRequest.Role,
                    Title = createContractRequest.Title,
                    EventList = createContractRequest.EventList,
                    Id = createContractRequest.Id
                };
                return objdecisionPointRepository.SetCreateContract(objCreateContractRequestParam);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Update Contract
        /// </summary>
        /// <param name="createContractRequest">createContractRequest</param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>Dec 26 2014</CreatedDate>
        /// <returns>int</returns>
        public int UpdateCreateContract(CreateContractRequest createContractRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CreateContractRequestParam objCreateContractRequestParam = new CreateContractRequestParam()
                {
                    ManagerName = createContractRequest.ManagerName,
                    OwnerId = createContractRequest.OwnerId,
                    ContractWithId = createContractRequest.ContractWithId,
                    ExecutedById = createContractRequest.ExecutedById,
                    ExecutedDate = createContractRequest.ExecutedDate,
                    ExpirationDate = createContractRequest.ExpirationDate,
                    ContractDate = createContractRequest.ContractDate,
                    ExpirationDateReminder = createContractRequest.ExpirationDateReminder,
                    GeneralComments = createContractRequest.GeneralComments,
                    Paragraph = createContractRequest.Paragraph,
                    Section = createContractRequest.Section,
                    SubSection = createContractRequest.SubSection,
                    Notes = createContractRequest.Notes,
                    EventDate = createContractRequest.EventDate,
                    EventDateReminder = createContractRequest.EventDateReminder,
                    CreatorCompanyId = createContractRequest.CreatorCompanyId,
                    ContractorCompanyId = createContractRequest.ContractorCompanyId,
                    IsActive = createContractRequest.IsActive,
                    IsDeleted = createContractRequest.IsDeleted,
                    IsCompleted = createContractRequest.IsCompleted,
                    CreatedBy = createContractRequest.CreatedBy,
                    ServiceList = createContractRequest.ServiceList,
                    FilePathStr = createContractRequest.FilePathStr,
                    Role = createContractRequest.Role,
                    Title = createContractRequest.Title,
                    EventList = createContractRequest.EventList,
                    Id = createContractRequest.Id
                };
                return objdecisionPointRepository.UpdateCreateContract(objCreateContractRequestParam);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get Company Id By UserId
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>28 Nov 2014</CreatedDate>
        /// <returns>string</returns>
        public string GetCompanyIdByUserId(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetCompanyIdByUserId(userId);
            }
            catch
            {
                throw;
            }
        }

        public int SaveNonMember(CreateContractRequest createContractRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                CreateContractRequestParam objCreateContractRequestParam = new CreateContractRequestParam()
                {
                    EmailId = createContractRequest.EmailId,
                    GenPassword = BusinessCore.CreatePassword(),
                    FName = createContractRequest.FName,
                    MName = createContractRequest.MName,
                    LName = createContractRequest.LName,
                    BusinessName = createContractRequest.BusinessName,
                    StreetNumber = createContractRequest.StreetNumber,
                    Direction = createContractRequest.Direction,
                    StreetName = createContractRequest.StreetName,
                    CityName = createContractRequest.CityName,
                    StateId = createContractRequest.StateId,
                    ZipCode = createContractRequest.ZipCode,
                    OfficePhone = createContractRequest.OfficePhone,
                    DirectPhone = createContractRequest.DirectPhone,
                    CreatedBy = createContractRequest.CreatedBy,
                    CreatorCompanyId = createContractRequest.CreatorCompanyId,
                    FlowId = createContractRequest.FlowId,
                };
                return objdecisionPointRepository.SaveNonMember(objCreateContractRequestParam);
            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// get contract list 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns>IList</returns>
        public IList<CreateContractResponse> GetContractList(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CreateContractResponse> objResponse = objdecisionPointRepository.GetContractList(userId).Select(q => new CreateContractResponse
                {
                    Id = q.Id,
                    BusinessName = q.BusinessName,
                    StreetName = q.StreetName,
                    StreetNumber = q.StreetNumber,
                    Direction = q.Direction,
                    CityName = q.CityName,
                    StateId = q.StateId,
                    ZipCode = q.ZipCode,
                    FName = q.FName,
                    MName = q.MName,
                    LName = q.LName,
                    OfficePhone = q.OfficePhone,
                    DirectPhone = q.DirectPhone,
                    EmailId = q.EmailId,
                    Role = q.Role,
                    ExecutedDate = q.ExecutedDate,
                    ContractDate = q.ContractDate,
                    EventDate = q.EventDate,
                    ExpirationDate = q.ExpirationDate,
                    ExpirationDateReminder = q.ExpirationDateReminder,
                    EventDateReminder = q.EventDateReminder,
                    Paragraph = q.Paragraph,
                    Section = q.Section,
                    SubSection = q.SubSection,
                    Notes = q.Notes,
                    GeneralComments = q.GeneralComments,
                    ManagerName = q.ManagerName,
                    ServiceName = q.ServiceName,
                    IsActive = q.IsActive,
                    Status = q.Status,
                    RefrenceNo = (q.CreatedDate.Value.Year % 100) + Shared.SingleDash + q.Id,
                }).ToList();
                return objResponse;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// get contract list 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns>IList</returns>
        public CreateContractResponse GetContractListAsPerId(int id)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objCreateContractResponseParam = new CreateContractResponseParam();
                objCreateContractResponseParam = objdecisionPointRepository.GetContractListAsPerId(id);
                CreateContractResponse objResponse = new CreateContractResponse
                {
                    Id = objCreateContractResponseParam.Id,
                    BusinessName = objCreateContractResponseParam.BusinessName,
                    StreetName = objCreateContractResponseParam.StreetName,
                    StreetNumber = objCreateContractResponseParam.StreetNumber,
                    Direction = objCreateContractResponseParam.Direction,
                    CityName = objCreateContractResponseParam.CityName,
                    StateId = objCreateContractResponseParam.StateId,
                    ZipCode = objCreateContractResponseParam.ZipCode,
                    FName = objCreateContractResponseParam.FName,
                    MName = objCreateContractResponseParam.MName,
                    LName = objCreateContractResponseParam.LName,
                    OfficePhone = objCreateContractResponseParam.OfficePhone,
                    DirectPhone = objCreateContractResponseParam.DirectPhone,
                    EmailId = objCreateContractResponseParam.EmailId,
                    Role = objCreateContractResponseParam.Role,
                    ExecutedDate = objCreateContractResponseParam.ExecutedDate,
                    ContractDate = objCreateContractResponseParam.ContractDate,
                    EventDate = objCreateContractResponseParam.EventDate,
                    ExpirationDate = objCreateContractResponseParam.ExpirationDate,
                    ExpirationDateReminder = objCreateContractResponseParam.ExpirationDateReminder,
                    EventDateReminder = objCreateContractResponseParam.EventDateReminder,
                    Paragraph = objCreateContractResponseParam.Paragraph,
                    Section = objCreateContractResponseParam.Section,
                    SubSection = objCreateContractResponseParam.SubSection,
                    Notes = objCreateContractResponseParam.Notes,
                    GeneralComments = objCreateContractResponseParam.GeneralComments,
                    ManagerName = objCreateContractResponseParam.ManagerName,
                    IsActive = objCreateContractResponseParam.IsActive,
                    Status = objCreateContractResponseParam.Status,
                    RefrenceNo = (objCreateContractResponseParam.CreatedDate.Value.Year % 100) + Shared.SingleDash + objCreateContractResponseParam.Id,
                    OwnerName = objCreateContractResponseParam.OwnerName,
                    ExecutedByName = objCreateContractResponseParam.ExecutedByName,
                    Title = objCreateContractResponseParam.Title,
                    ContractWithName = objCreateContractResponseParam.ContractWithName,
                    ServiceName = objCreateContractResponseParam.ServiceName,
                    ServiceId = objCreateContractResponseParam.ServiceId,
                    OwnerId = objCreateContractResponseParam.OwnerId,
                    ContractWithId = objCreateContractResponseParam.ContractWithId,
                    ContractorCompanyId = objCreateContractResponseParam.ContractorCompanyId,
                    ExecutedById = objCreateContractResponseParam.ExecutedById,
                    CreatorCompanyId = objCreateContractResponseParam.CreatorCompanyId,

                };
                return objResponse;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// get contract event list 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns>IList</returns>
        public IList<ContractEvent> GetContractEventsListAsPerId(int id)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetContractEventsListAsPerId(id);

            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// delete contrcat
        /// </summary>
        /// <param name="contractId">contractId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 05 2014</CreatedDate>
        /// <returns>int</returns>
        public int DeleteContract(int contractId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DeleteContract(contractId);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// ReActive contrcat
        /// </summary>
        /// <param name="contractId">contractId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 08 2014</CreatedDate>
        /// <returns>int</returns>
        public int ReActiveContract(int contractId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.ReActiveContract(contractId);
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get contract list 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 04 2014</CreatedDate>
        /// <returns>IList</returns>
        public IList<CreateContractResponse> GetMyContract(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CreateContractResponse> objResponse = objdecisionPointRepository.GetMyContract(userId).Select(q => new CreateContractResponse
                {
                    Id = q.Id,
                    BusinessName = q.BusinessName,
                    StreetName = q.StreetName,
                    StreetNumber = q.StreetNumber,
                    Direction = q.Direction,
                    CityName = q.CityName,
                    StateId = q.StateId,
                    ZipCode = q.ZipCode,
                    FName = q.FName,
                    MName = q.MName,
                    LName = q.LName,
                    OfficePhone = q.OfficePhone,
                    DirectPhone = q.DirectPhone,
                    EmailId = q.EmailId,
                    Role = q.Role,
                    ExecutedDate = q.ExecutedDate,
                    ContractDate = q.ContractDate,
                    EventDate = q.EventDate,
                    ExpirationDate = q.ExpirationDate,
                    ExpirationDateReminder = q.ExpirationDateReminder,
                    EventDateReminder = q.EventDateReminder,
                    Paragraph = q.Paragraph,
                    Section = q.Section,
                    SubSection = q.SubSection,
                    Notes = q.Notes,
                    GeneralComments = q.GeneralComments,
                    ManagerName = q.ManagerName,
                    ServiceName = q.ServiceName,
                    IsActive = q.IsActive,
                    Status = q.Status,
                    RefrenceNo = (q.CreatedDate.Value.Year % 100) + Shared.SingleDash + q.Id,
                }).ToList();
                return objResponse;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Used to search in contract
        /// </summary>
        /// <param name="term"></param>
        /// <param name="UserId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<CreateContractResponse> SerachInContract(string serachTerm)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CreateContractResponse> serach = objdecisionPointRepository.SerachInContract(serachTerm).Select(x => new CreateContractResponse { BusinessName = x.BusinessName, ContractorCompanyId = x.ContractorCompanyId, CreatorCompanyId = x.CreatorCompanyId, CompanyName = x.CompanyName, }).ToList();
                return serach;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get non member details
        /// </summary>
        /// <param name="userId">userId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 17 2014</CreatedDate>
        /// <returns>NonMemberResponse </returns>
        public List<NonMemberResponse> GetNonMemberDetails(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                List<NonMemberResponse> objResponse = objdecisionPointRepository.GetNonMemberDetails(userId).Select(q => new NonMemberResponse
                {
                    CompanyName = q.CompanyName,
                    flowId = q.flowId,
                    fName = q.fName,
                    lName = q.lName,
                    emailId = q.emailId,
                    UserId = q.UserId,
                    CreatedBy = q.CreatedBy,
                    UserCompanyId = q.UserCompanyId,
                    CreatorCompanyId = q.CreatorCompanyId,
                    Password = q.Password,
                    CreatorCompanyName = q.CreatorCompanyName,
                }).ToList();

                return objResponse;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// invite non member
        /// </summary>
        /// <param name="objResponseParam">objResponse</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 18 2014</CreatedDate>
        /// <returns>int</returns>
        public int InviteNonMember(NonMemberResponse objResponse)
        {
            try
            {
                int result = 0;
                objdecisionPointRepository = new DecisionPointRepository();
                NonMemberResponseParam objNonMemberResponseParam = new NonMemberResponseParam()
                {
                    DocFlowId = objResponse.DocFlowId,
                    UserId = Convert.ToInt32(objResponse.UserId, CultureInfo.InvariantCulture),
                    CreatedBy = objResponse.CreatedBy,
                    UserCompanyId = objResponse.UserCompanyId,
                    CreatorCompanyId = objResponse.CreatorCompanyId
                };
                result = objdecisionPointRepository.InviteNonMember(objNonMemberResponseParam);

                #region Mail Sending Code

                //decode password
                businessCryptography = new BusinessCryptography();
                string password = businessCryptography.base64Decode2(objResponse.Password);
                string email = objResponse.emailId;
                string signature = string.Empty;
                string subject = string.Empty;

                //get the signature of invitee company
                signature = GetSignature(objResponse.CreatedBy);

                if (!string.IsNullOrEmpty(signature))
                {
                    string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                    signature = sign[1].Trim();
                }
                string text = string.Empty;

                subject = "Compliance Tracker";
                text = "<div style='line-height:25px'>To: " + objResponse.fName + " " + objResponse.lName + "<br/>From: " + objResponse.CreatorCompanyName + "<br/>Subject: Compliance Tracker<br/><br/>" + objResponse.CreatorCompanyName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + objResponse.CreatorCompanyName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + objResponse.SiteRoot + "?id=" + objResponse.UserId + "'>click here</a> to get to log in page</br><br/>User Name: " + email + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";

                GetSmtpDetail(email, text, subject);
                //set the last invite date in mapping table
                UpdateLastInvite(objResponse.CreatorCompanyId, Convert.ToInt32(objResponse.UserId, CultureInfo.InvariantCulture), Shared.Vendor);

                #endregion
                return result;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get contrcat documents
        /// </summary>
        /// <param name="contrcatId">contrcatId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Dec 22 2014</CreatedDate>
        /// <returns>CommContentRequest</returns>
        public IList<CommContentRequest> GetContrcatDoc(int contrcatId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<CommContentRequest> docList = objdecisionPointRepository.GetContrcatDoc(contrcatId).Select(q =>
                    new CommContentRequest
                    {
                        files = q.files,
                        filetype = q.filetype != null ? q.filetype.Split('&')[0] : q.filetype,
                        docId = q.docId,
                    }).ToList();
                return docList;
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region Permission Table
        /// <summary>
        /// used for get the funtiona permission details
        /// </summary>
        /// <returns>List of funtional permissions</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>4 Dec 2014</createddate>
        public IList<PermissionTableResponse> GetFuntionalPermission(PermissionTableRequest objPermissionTableRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objPermissionTableRequestParam = new PermissionTableRequestParam()
                {
                    CreatedCompanyId = objPermissionTableRequest.CreatedCompanyId,
                    Type = objPermissionTableRequest.Type,
                    TitleId = objPermissionTableRequest.TitleId

                };
                return objdecisionPointRepository.GetFuntionalPermission(objPermissionTableRequestParam).Select(x => new PermissionTableResponse { FuntionalPermission = x.FuntionalPermission, TableId = x.TableId, TitleId = x.TitleId, TitleName = x.TitleName, FunPerId = x.FunPerId }).ToList();

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for get the funtional permission details as per user and and its title
        /// </summary>
        /// <returns>List of funtional permissions</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>12 Dec 2014</createddate>
        public IList<PermissionTableResponse> GetFuntionalPermissionAsPerUserTitle(PermissionTableRequest objPermissionTableRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objPermissionTableRequestParam = new PermissionTableRequestParam()
                {
                    CreatedCompanyId = objPermissionTableRequest.CreatedCompanyId,
                    Type = objPermissionTableRequest.Type,
                    TitleId = objPermissionTableRequest.TitleId,
                    UserId = objPermissionTableRequest.UserId,
                    UserType = objPermissionTableRequest.UserType

                };
                return objdecisionPointRepository.GetFuntionalPermissionAsPerUserTitle(objPermissionTableRequestParam).Select(x => new PermissionTableResponse { FuntionalPermission = x.FuntionalPermission, TableId = x.TableId, TitleId = x.TitleId, TitleName = x.TitleName, FunPerId = x.FunPerId, TabId = x.TabId, TabName = x.TabName, TabUrl = x.TabUrl, IsMainTab = x.IsMainTab, MainTabName = x.MainTabName, TabSectionName = x.TabSectionName }).ToList();

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for save the funtional permission mapping for internal staff's
        /// </summary>
        /// <param name="objPermissionTableResponse"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>4 Dec 2014</createddate>
        public int SaveFuntionalPermissionTableMapping(PermissionTableRequest objPermissionTableRequest)
        {
            try
            {
                objPermissionTableRequestParam = new PermissionTableRequestParam()
                {
                    TitleId = objPermissionTableRequest.TitleId,
                    FunPermissionIds = objPermissionTableRequest.FunPermissionIds,
                    CreatedBy = objPermissionTableRequest.CreatedBy,
                    CreatedCompanyId = objPermissionTableRequest.CreatedCompanyId,
                    ModifiedBy = objPermissionTableRequest.ModifiedBy
                };
                objdecisionPointRepository = new DecisionPointRepository();
                //call method for save the funtional permissions in mapping tbale as per title
                return objdecisionPointRepository.SaveFuntionalPermissionTableMapping(objPermissionTableRequestParam);

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region IC NonClient Payment
        /// <summary>
        ///ICNonClientPayment
        /// </summary>
        /// <param name="paymentResponse">parameters with info for payment gateway</param>
        /// <createdby>bobi</createdby>
        ///  <createdDate>19 jan 2015</createdDate>
        /// <returns> sucess or fail payment message</returns>
        public int ICNonClientPayment(PaymentResponse paymentResponse, PaymentAmountResponse objPaymentAmountResponse)
        {
            try
            {
                string stripeKey = System.Configuration.ConfigurationManager.AppSettings["StripeKey"].ToString();
                StripePayment payment = new StripePayment(stripeKey);
                string result = XamarinStripeCore.DecisionPointRegistrationCharge(payment, paymentResponse);
                objdecisionPointRepository = new DecisionPointRepository();
                if (!string.IsNullOrEmpty(Convert.ToString(result)))
                {
                    PaymentAmountResponseParam paymentAmountResponseParam = new PaymentAmountResponseParam
                    {
                        CompanyCode = objPaymentAmountResponse.CompanyCode,
                        CompanyFee = objPaymentAmountResponse.CompanyFee,
                        PerFieldStaffFee = objPaymentAmountResponse.PerFieldStaffFee,
                        PerOfficeStaffFee = objPaymentAmountResponse.PerOfficeStaffFee,
                        PerIcFee = objPaymentAmountResponse.PerIcFee,
                        IsInvoice = objPaymentAmountResponse.IsInvoice,
                        NoOfPartners = objPaymentAmountResponse.NoOfPartners,
                        NoOfStaff = objPaymentAmountResponse.NoOfStaff,
                        NoOfIc = objPaymentAmountResponse.NoOfIc,
                        BusinessName = objPaymentAmountResponse.BusinessName,
                        TransactionType = objPaymentAmountResponse.TransactionType,
                        TransactionCode = result,
                        TransactionMessage = "Payment Sucesss",
                        userId = objPaymentAmountResponse.userId,
                        InviteeCompanyId = objPaymentAmountResponse.InviteeCompanyId
                    };
                    int results = objdecisionPointRepository.ICNonClientPayment(paymentAmountResponseParam);
                    return results;
                }
                else
                {

                    return 0;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// RemoveICNonClient
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Jan 21 2015</createdDate>
        public int RemoveICNonClient(int id, string companyId)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.RemoveICNonClient(id, companyId);
            return response; ;
        }
        /// <summary>
        /// ReactiveICNonClient
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Jan 21 2015</createdDate>
        public int ReactiveICNonClient(int id, string companyId)
        {
            int response;
            objdecisionPointRepository = new DecisionPointRepository();
            response = objdecisionPointRepository.ReactiveICNonClient(id, companyId);
            return response; ;
        }
        #endregion

        #region WebService
        public APIMasterResponse InsertAPILog(APILogRequest objRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string pwd = string.Empty;
                if (!string.IsNullOrEmpty(objRequest.APIPassword))
                {
                    pwd = businessCryptography.base64Encode(objRequest.APIPassword);
                }
                APILogRequestParam objrequestParam = new APILogRequestParam()
                {
                    RefrenceId = objRequest.RefrenceId,
                    RequestData = objRequest.RequestData,
                    APIPassword = pwd,
                    APIUserName = objRequest.APIUserName,
                };
                objAPIMasterResponseParam = objdecisionPointRepository.InsertAPILog(objrequestParam);
                objAPIMasterResponse = new APIMasterResponse()
                {
                    ResultCode = objAPIMasterResponseParam.ResultCode,
                    ResultId = objAPIMasterResponseParam.ResultId,
                };
                return objAPIMasterResponse;
            }
            catch
            {

                throw;
            }
        }
        public APIMasterResponse ValidateAPIUser(string userId, string password)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                businessCryptography = new BusinessCryptography();
                objAPIMasterResponse = new APIMasterResponse();
                string pwd = string.Empty;
                bool result = false;
                if (!string.IsNullOrEmpty(password))
                {
                    pwd = businessCryptography.base64Encode(password);
                    result = objdecisionPointRepository.ValidateAPIUser(userId, pwd);
                    if (!result)
                    {
                        objAPIMasterResponse.ResultId = string.Empty;
                        objAPIMasterResponse.ResultCode = (int)APIResponseStatusCode.Unauthorized;
                    }
                }
                else
                {
                    objAPIMasterResponse.ResultId = string.Empty;
                    objAPIMasterResponse.ResultCode = (int)APIResponseStatusCode.AccessCredentailRequired;
                }
                return objAPIMasterResponse;
            }
            catch
            {

                throw;
            }
        }

        public ServiceResponse GetIcDetails(string icId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                ServiceResponseParam objServiceResponseParam = new ServiceResponseParam();
                objServiceResponseParam = objdecisionPointRepository.GetIcDetails(icId);
                ServiceResponse objServiceResponse = new ServiceResponse();
                if (objServiceResponseParam != null)
                {
                    objServiceResponse.fName = objServiceResponseParam.fName;
                    objServiceResponse.mName = objServiceResponseParam.mName;
                    objServiceResponse.lName = objServiceResponseParam.lName;
                    objServiceResponse.emailId = objServiceResponseParam.emailId;
                    objServiceResponse.companyName = objServiceResponseParam.companyName;
                    objServiceResponse.officePhone = objServiceResponseParam.officePhone;
                    objServiceResponse.directPhone = objServiceResponseParam.directPhone;
                    objServiceResponse.StreetNumber = objServiceResponseParam.StreetNumber;
                    objServiceResponse.StreetName = objServiceResponseParam.StreetName;
                    objServiceResponse.Direction = objServiceResponseParam.Direction;
                    objServiceResponse.CityName = objServiceResponseParam.CityName;
                    objServiceResponse.StateName = objServiceResponseParam.StateName;
                    objServiceResponse.ZipCode = objServiceResponseParam.ZipCode;
                    objServiceResponse.title = objServiceResponseParam.title;
                }
                return objServiceResponse;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Send Invitation By DST
        /// </summary>
        /// <param name="objRequest">DSTInviteRequest</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>24 feb 2015</CreatedDate>
        /// <returns>DSTInviteResponse</returns>
        public APIMasterResponse SendInvitationByDST(DSTInviteRequest objRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objAPIMasterResponseParam = new APIMasterResponseParam();
                objAPIMasterResponse = new APIMasterResponse();
                businessCryptography = new BusinessCryptography();
                DSTInviteRequestParam objParam = new DSTInviteRequestParam()
                {
                    FirstName = objRequest.FirstName,
                    LastName = objRequest.LastName,
                    EmailId = objRequest.EmailId,
                    RoleTypeId = objRequest.RoleTypeId,
                    ClientId = objRequest.ClientId,
                    SubClientId = objRequest.SubClientId,
                    PackageId = objRequest.PackageId,
                    ProductId = objRequest.ProductId,
                    Password = BusinessCore.CreatePassword(),
                    //change here to get user id and business name using client company id
                    UserId = objdecisionPointRepository.GetUserIdFromCompanyId(objRequest.ClientId),
                    BusinessName = objRequest.BusinessName,
                    IsMailSent = true,
                    PaymentTypeId = objRequest.PaymentTypeId,
                    ICTypeId = objRequest.ICTypeId,
                    RoleType = objRequest.RoleType,
                };
                objAPIMasterResponseParam = objdecisionPointRepository.SendInvitationByDST(objParam);
                if (!object.Equals(objAPIMasterResponseParam, null))
                {
                    #region Send Email
                    //code for sending mail
                    string password = businessCryptography.base64Decode2(objParam.Password);
                    string email = objParam.EmailId;
                    string signature = string.Empty;
                    string subject = string.Empty;
                    int receiverUserId = 0;


                    string[] split = objAPIMasterResponseParam.ResultId.Split(char.Parse(Shared.Comma));
                    //get reciver unique id
                    receiverUserId = Convert.ToInt32(split[1], CultureInfo.InvariantCulture);
                    //get password
                    if (!string.IsNullOrEmpty(split[2]))
                    {
                        password = businessCryptography.base64Decode2(split[2]);
                    }
                    //get status code
                    objAPIMasterResponse.ResultCode = Convert.ToInt32(split[3], CultureInfo.InvariantCulture);
                    //get result Id
                    objAPIMasterResponse.ResultId = split[0];


                    //get the signature of invitee company
                    signature = GetSignature(objParam.UserId);

                    if (!string.IsNullOrEmpty(signature))
                    {
                        string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                        signature = sign[1].Trim();
                    }

                    string text = string.Empty;

                    if (objRequest.RoleType.Trim().ToLower().Equals(Shared.Staff.Trim().ToLower()) || objRequest.RoleType.Trim().ToLower().Equals(Shared.IC.Trim().ToLower()))
                    {
                        subject = "Compliance Tracker Account";
                        text = "<div style='line-height:25px'>To: " + objRequest.FirstName + " " + objRequest.LastName + "<br/>From: " + objRequest.BusinessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + objRequest.BusinessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + ConfigurationManager.AppSettings["SiteName"] + "?id=" + objParam.UserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objRequest.EmailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    }
                    else if (objRequest.RoleType.Trim().ToLower().Equals(Shared.Vendor.Trim().ToLower()) || objRequest.RoleType.Trim().ToLower().Equals(Shared.Client.Trim().ToLower()))
                    {
                        subject = "Compliance Tracker";
                        text = "<div style='line-height:25px'>To: " + objRequest.FirstName + " " + objRequest.LastName + "<br/>From: " + objRequest.BusinessName + "<br/>Subject: Compliance Tracker<br/><br/>" + objRequest.BusinessName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + objRequest.BusinessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + ConfigurationManager.AppSettings["SiteName"] + "?id=" + objParam.UserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objRequest.EmailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                    }

                    GetSmtpDetail(email, text, subject);
                    //set the last invite date in mapping table
                    objdecisionPointRepository.UpdateLastInvite(objRequest.ClientId, receiverUserId, objRequest.RoleType);
                    #endregion


                }
                return objAPIMasterResponse;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        ///// <summary>
        ///// get tile list
        ///// </summary>
        ///// <param name="companyId">companyId</param>
        /////  <CreatedBy>Sumit Saurav</CreatedBy>
        ///// <CreatedDate>25 feb 2015</CreatedDate>
        ///// <returns>APIMasterResponse</returns>
        //public IEnumerable<APIMasterResponse> GetTitle(string companyId)
        //{
        //    try
        //    {
        //        objdecisionPointRepository = new DecisionPointRepository();
        //        IEnumerable<APIMasterResponse> titlelist = objdecisionPointRepository.GetTitle(companyId).Select(x => new APIMasterResponse { Name = x.Name, Id = x.Id });
        //        return titlelist;

        //    }
        //    catch
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// get tile list
        /// </summary>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>APIMasterResponse</returns>
        public IList<APIMasterResponse> GetRoleType()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<APIMasterResponse> rolelist = objdecisionPointRepository.GetRoleType().Select(x => new APIMasterResponse { Name = x.Name, Id = x.Id });
                return rolelist.ToList();

            }
            catch
            {

                throw;
            }
        }

        ///// <summary>
        ///// get doc flow list
        ///// </summary>
        /////  <CreatedBy>Sumit Saurav</CreatedBy>
        ///// <CreatedDate>25 feb 2015</CreatedDate>
        ///// <returns>APIMasterResponse</returns>
        //public IList<APIMasterResponse> GetDocFlowList()
        //{
        //    try
        //    {
        //        objdecisionPointRepository = new DecisionPointRepository();
        //        IEnumerable<APIMasterResponse> docflowlist = objdecisionPointRepository.GetDocFlowList().Select(x => new APIMasterResponse { Name = x.Name, Id = x.Id });
        //        return docflowlist;

        //    }
        //    catch
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// get doc flow list
        /// </summary>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>APIMasterResponse</returns>
        public IList<APIMasterResponse> GetPaymentType()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<APIMasterResponse> paylist = objdecisionPointRepository.GetPaymentType().Select(x => new APIMasterResponse { Name = x.Name, Id = x.Id }).ToList();
                return paylist;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get vendor type list
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>APIMasterResponse</returns>
        public IList<APIMasterResponse> GetVendorTypeList(string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<APIMasterResponse> vendorlist = objdecisionPointRepository.GetVendorTypeList(companyId).Select(x => new APIMasterResponse { Name = x.Name, Id = x.Id }).ToList();
                return vendorlist;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// get vendor type list
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>APIMasterResponse</returns>
        public IList<APIMasterResponse> GetCandidatesComplianceStatus(APIComplianceStatusRequest objAPIComplianceStatusRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objAPIComplianceStatusRequestParam = new APIComplianceStatusRequestParam()
                {
                    ClientId = objAPIComplianceStatusRequest.ClientId,
                    SubClientId = objAPIComplianceStatusRequest.SubClientId,
                    //PackageId = objAPIComplianceStatusRequest.PackageId,
                    CandidateIdsCol = objAPIComplianceStatusRequest.CandidateIdsCol
                };
                IList<APIMasterResponse> vendorlist = objdecisionPointRepository.GetCandidatesComplianceStatus(objAPIComplianceStatusRequestParam).Select(x => new APIMasterResponse
                {
                    ResultId = x.ResultId,
                    ResultCode = x.ResultCode,
                    APIMasterResponseJCRInfoList = x.PackageInfoDetails.Select(t => new APIMasterResponseJCRInfo { JCR = t.JCR, JCRStatus = t.JCRStatus }).ToList()
                }).ToList();
                return vendorlist;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get vendor type list
        /// </summary>
        /// <param name="companyId">companyId</param>
        ///  <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>APIMasterResponse</returns>
        public APIMasterResponse VerifyAssignment(APIComplianceStatusRequest objAPIComplianceStatusRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objAPIMasterResponseParam = new APIMasterResponseParam();
                objAPIComplianceStatusRequestParam = new APIComplianceStatusRequestParam()
                {
                    ClientId = objAPIComplianceStatusRequest.ClientId,
                    SubClientId = objAPIComplianceStatusRequest.SubClientId,
                    //PackageId = objAPIComplianceStatusRequest.PackageId,
                    UserId = objAPIComplianceStatusRequest.UserId
                };
                objAPIMasterResponseParam = objdecisionPointRepository.VerifyAssignment(objAPIComplianceStatusRequestParam);
                if (!object.Equals(objAPIMasterResponseParam, null))
                {
                    objAPIMasterResponse = new APIMasterResponse()
                    {
                        ResultCode = objAPIMasterResponseParam.ResultCode,
                        ResultId = objAPIMasterResponseParam.ResultId,
                        APIMasterResponseJCRInfoList = objAPIMasterResponseParam.PackageInfoDetails.Select(x => new APIMasterResponseJCRInfo { JCR = x.JCR, JCRStatus = x.JCRStatus })
                    };
                }
                return objAPIMasterResponse;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// AddUpdateUser
        /// </summary>
        /// <param name="objRequest">DSTInviteRequest</param>
        ///  <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>24 feb 2015</CreatedDate>
        /// <returns>DSTInviteResponse</returns>
        public APIMasterResponse AddUpdateUser(DSTInviteRequest objRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objAPIMasterResponseParam = new APIMasterResponseParam();
                objAPIMasterResponse = new APIMasterResponse();
                businessCryptography = new BusinessCryptography();
                DSTInviteRequestParam objParam = new DSTInviteRequestParam()
                {
                    FirstName = objRequest.FirstName,
                    LastName = objRequest.LastName,
                    EmailId = objRequest.EmailId,
                    // RoleTypeId = objRequest.RoleTypeId,
                    EntityId = objRequest.EntityId,
                    StaffId = objRequest.StaffId,
                    Password = BusinessCore.CreatePassword(),
                    UserId = objdecisionPointRepository.GetUserIdFromCompanyId(objRequest.EntityId),
                    //change here to get user id and business name using client company id
                    BusinessName = objRequest.BusinessName,
                    IsMailSent = true,
                    RoleType = objRequest.RoleType,
                };
                objAPIMasterResponseParam = objdecisionPointRepository.AddUpdateUser(objParam);
                if (string.IsNullOrEmpty(objRequest.StaffId))
                {
                    if (!object.Equals(objAPIMasterResponseParam, null))
                    {
                        #region Send Email
                        //code for sending mail
                        string password = businessCryptography.base64Decode2(objParam.Password);
                        string email = objParam.EmailId;
                        string signature = string.Empty;
                        string subject = string.Empty;
                        int receiverUserId = 0;


                        string[] split = objAPIMasterResponseParam.ResultId.Split(char.Parse(Shared.Comma));
                        //get reciver unique id
                        receiverUserId = Convert.ToInt32(split[1], CultureInfo.InvariantCulture);
                        //get password
                        if (!string.IsNullOrEmpty(split[2]))
                        {
                            password = businessCryptography.base64Decode2(split[2]);
                        }
                        //get status code
                        objAPIMasterResponse.ResultCode = Convert.ToInt32(split[3], CultureInfo.InvariantCulture);
                        //get result Id
                        objAPIMasterResponse.ResultId = split[0];


                        //get the signature of invitee company
                        signature = GetSignature(objParam.UserId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }

                        string text = string.Empty;

                        if (objRequest.RoleType.Trim().ToLower().Equals(Shared.Staff.Trim().ToLower()) || objRequest.RoleType.Trim().ToLower().Equals(Shared.IC.Trim().ToLower()))
                        {
                            subject = "Compliance Tracker Account";
                            text = "<div style='line-height:25px'>To: " + objRequest.FirstName + " " + objRequest.LastName + "<br/>From: " + objRequest.BusinessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + objRequest.BusinessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + ConfigurationManager.AppSettings["SiteName"] + "?id=" + objParam.UserId + "'>click here</a> to get to log in page</br><br/>User Name: " + objRequest.EmailId + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                        }

                        GetSmtpDetail(email, text, subject);
                        //set the last invite date in mapping table
                        objdecisionPointRepository.UpdateLastInvite(objRequest.ClientId, receiverUserId, objRequest.RoleType);
                        #endregion


                    }
                }
                return objAPIMasterResponse;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// UpdateSubClient
        /// </summary>
        /// <param name="objRequest">DSTInviteRequest</param>
        ///  <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>24 feb 2015</CreatedDate>
        /// <returns>DSTInviteResponse</returns>
        public APIMasterResponse AddUpdateSubClient(DSTInviteRequest objRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                objAPIMasterResponseParam = new APIMasterResponseParam();
                objAPIMasterResponse = new APIMasterResponse();
                DSTInviteRequestParam objParam = new DSTInviteRequestParam()
                {
                    SubClientId = objRequest.SubClientId,
                    ClientId = objRequest.ClientId,
                    UserId = objdecisionPointRepository.GetUserIdFromCompanyId(objRequest.ClientId),
                    BusinessName = objRequest.BusinessName,
                    Password = BusinessCore.CreatePassword(),
                    EmailId = string.Empty,
                    FirstName = String.Empty,
                    LastName = string.Empty

                };
                objAPIMasterResponseParam = objdecisionPointRepository.AddUpdateSubClient(objParam);
                if (!object.Equals(objAPIMasterResponseParam, null))
                {
                    objAPIMasterResponse.ResultId = objAPIMasterResponseParam.ResultId;
                    objAPIMasterResponse.ResultCode = objAPIMasterResponseParam.ResultCode;
                }
                return objAPIMasterResponse;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Validate Ic with email id and Ic type
        /// </summary>
        /// <param name="email">email</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>19 Jan 2015</CreatedDate>
        /// <returns>int</returns>
        public int ValidateIcWithIcType(string email)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.ValidateIcWithIcType(email);
            }
            catch
            {

                throw;
            }
        }

        #region Professional Lic & Insurance & Additional Req Mappings
        /// <summary>
        /// Get Professional License
        /// </summary>
        /// <param name="userId">ic user id</param>
        /// <param name="companyId">ic company id</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>03 feb 2015</CreatedDate>
        /// <returns>UserDashBoardResponseParam</returns>
        public IList<LicenseInsuranceResponse> GetProfessionalLicense(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<LicenseInsuranceResponse> licenseDetails = objdecisionPointRepository.GetProfessionalLicense(userId, companyId).Select(x => new LicenseInsuranceResponse
                {
                    RequiredByUserId = x.RequiredByUserId,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    LicenseType = x.LicenseType,
                    LicenseNumber = x.LicenseNumber,
                    StateName = x.StateName,
                    ExpirationDate = x.ExpirationDate,
                    Status = x.Status,
                    LicInsMapId = x.LicInsMapId,
                    Source = x.Source,
                    StateId = x.StateId,
                    LicInsId = x.LicInsId,
                    VendorTypeId = x.VendorTypeId,
                    CompanyId = x.CompanyId,
                    SterlingPkgName = x.SterlingPkgName,
                    IsDocUpload = x.IsDocUpload
                }).ToList();
                return licenseDetails;
            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// Get Professional Insurance
        /// </summary>
        /// <param name="userId">ic user id</param>
        /// <param name="companyId">ic company id</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>03 feb 2015</CreatedDate>
        /// <returns>UserDashBoardResponseParam</returns>
        public IEnumerable<LicenseInsuranceResponse> GetProfessionalInsurance(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<LicenseInsuranceResponse> insuranceDetails = objdecisionPointRepository.GetProfessionalInsurance(userId, companyId).Select(x => new LicenseInsuranceResponse
                {
                    RequiredByUserId = x.RequiredByUserId,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    InsuranceType = x.InsuranceType,
                    PolicyNumber = x.PolicyNumber,
                    ExpirationDate = x.ExpirationDate,
                    Status = x.Status,
                    LicInsMapId = x.LicInsMapId,
                    LicInsId = x.LicInsId,
                    CompanyName = x.CompanyName,
                    SingleOccurance = x.SingleOccurance,
                    Aggregate = x.Aggregate,
                    VendorTypeId = x.VendorTypeId,
                    CompanyId = x.CompanyId,
                    IsDocUpload = x.IsDocUpload,
                    InsuranceVerType = x.InsuranceVerType
                }).ToList();
                return insuranceDetails;
            }
            catch
            {

                throw;
            }
        }

        public IEnumerable<LicenseInsuranceResponse> GetAdditionalRequirement(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<LicenseInsuranceResponse> addReqDetails = objdecisionPointRepository.GetAdditionalRequirement(userId, companyId).Select(x => new LicenseInsuranceResponse
                {
                    RequiredByUserId = x.RequiredByUserId,
                    ReqCompanyName = x.ReqCompanyName,
                    RequiredByCompanyId = x.RequiredByCompanyId,
                    title = x.title,
                    CompletedDate = x.CompletedDate,
                    Status = x.Status,
                    LicInsMapId = x.LicInsMapId,
                    LicInsId = x.LicInsId,
                    VendorTypeId = x.VendorTypeId,
                    CompanyId = x.CompanyId,
                    UploadedDoc = x.UploadedDoc,
                    IsDocUpload = x.IsDocUpload
                }).ToList();
                return addReqDetails;

            }
            catch
            {

                throw;
            }
        }

        /// <summary>
        /// GetProfessionalCommunication
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="companyId">companyId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>14 feb 2015</CreatedDate>
        /// <returns>UserDashBoardResponse</returns>
        public IEnumerable<UserDashBoardResponse> GetProfessionalCommunication(int userId, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IEnumerable<UserDashBoardResponse> commDetails = objdecisionPointRepository.GetProfessionalCommunication(userId, companyId).Select(x => new UserDashBoardResponse
                {
                    IsCompliance = x.IsCompliance,
                    DocTitle = x.DocTitle,
                    completeDate = x.completeDate,
                    expirationDate = x.expirationDate,
                    Docfrom = x.Docfrom,
                    DocId = x.DocId,
                    tableId = x.tableId,
                    status = x.status,
                    CreatorCompanyid = x.CreatorCompanyid,
                    accecpted = x.accecpted,
                    assesmentStatus = x.assesmentStatus
                }).ToList();
                return commDetails;

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Used for professional license
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int EditProfessionalLic(LicenseInsuranceRequest objLicenseInsuranceRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                if (!object.Equals(objLicenseInsuranceRequest, null))
                {
                    objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                    {
                        LicenseNumber = objLicenseInsuranceRequest.LicenseNumber,
                        StateId = objLicenseInsuranceRequest.StateId,
                        ExpirationDate = objLicenseInsuranceRequest.ExpirationDate,
                        LicInsMapId = objLicenseInsuranceRequest.LicInsMapId,
                        ModifiedById = objLicenseInsuranceRequest.ModifiedById,
                        UploadedDoc = objLicenseInsuranceRequest.UploadedDoc,
                        UserId = objLicenseInsuranceRequest.UserId,
                        CompanyId = objLicenseInsuranceRequest.CompanyId,
                        LicInsMasterId = objLicenseInsuranceRequest.LicInsMasterId,
                        UploadedDocDate = objLicenseInsuranceRequest.UploadedDocDate
                    };
                }
                return objdecisionPointRepository.EditProfessionalLic(objLicenseInsuranceRequestParam);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Used for insurance
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int EditInsurance(LicenseInsuranceRequest objLicenseInsuranceRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                if (!object.Equals(objLicenseInsuranceRequest, null))
                {
                    objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                    {
                        PolicyNumber = objLicenseInsuranceRequest.PolicyNumber,
                        ExpirationDate = objLicenseInsuranceRequest.ExpirationDate,
                        CompanyName = objLicenseInsuranceRequest.CompanyName,
                        LicInsMapId = objLicenseInsuranceRequest.LicInsMapId,
                        ModifiedById = objLicenseInsuranceRequest.ModifiedById,
                        SingleOccurance = objLicenseInsuranceRequest.SingleOccurance,
                        Aggregate = objLicenseInsuranceRequest.Aggregate,
                        UploadedDoc = objLicenseInsuranceRequest.UploadedDoc,
                        UserId = objLicenseInsuranceRequest.UserId,
                        CompanyId = objLicenseInsuranceRequest.CompanyId,
                        LicInsMasterId = objLicenseInsuranceRequest.LicInsMasterId
                    };
                }
                return objdecisionPointRepository.EditInsurance(objLicenseInsuranceRequestParam);

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Used for Additional Req
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int EditAdditionalReq(LicenseInsuranceRequest objLicenseInsuranceRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                if (!object.Equals(objLicenseInsuranceRequest, null))
                {
                    objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                    {
                        ModifiedById = objLicenseInsuranceRequest.ModifiedById,
                        UploadedDoc = objLicenseInsuranceRequest.UploadedDoc,
                        UserId = objLicenseInsuranceRequest.UserId,
                        CompanyId = objLicenseInsuranceRequest.CompanyId,
                        LicInsMasterId = objLicenseInsuranceRequest.LicInsMasterId,
                        LicInsMapId = objLicenseInsuranceRequest.LicInsMapId,
                    };
                }
                return objdecisionPointRepository.EditAdditionalReq(objLicenseInsuranceRequestParam);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Used for Driver License
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int EditDriverLicense(LicenseInsuranceRequest objLicenseInsuranceRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                if (!object.Equals(objLicenseInsuranceRequest, null))
                {
                    objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                    {
                        LicenseNumber = objLicenseInsuranceRequest.LicenseNumber,
                        StateId = objLicenseInsuranceRequest.StateId,
                        ExpirationDate = objLicenseInsuranceRequest.ExpirationDate,
                        LicInsMapId = objLicenseInsuranceRequest.LicInsMapId,
                        ModifiedById = objLicenseInsuranceRequest.ModifiedById,
                        UploadedDoc = objLicenseInsuranceRequest.UploadedDoc,
                        UserId = objLicenseInsuranceRequest.UserId,
                        CompanyId = objLicenseInsuranceRequest.CompanyId,
                        LicInsMasterId = objLicenseInsuranceRequest.LicInsMasterId,
                        UploadedDocDate = objLicenseInsuranceRequest.UploadedDocDate,
                        StateAbbre = objLicenseInsuranceRequest.StateAbbre
                    };
                }
                return objdecisionPointRepository.EditDriverLicense(objLicenseInsuranceRequestParam);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Arroved Status
        /// </summary>
        /// <param name="objLicInsRequestParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int ApprovedStatus(LicenseInsuranceRequest objLicenseInsuranceRequest)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();

                if (!object.Equals(objLicenseInsuranceRequest, null))
                {
                    objLicenseInsuranceRequestParam = new LicenseInsuranceRequestParam()
                    {
                        UserId = objLicenseInsuranceRequest.UserId,
                        CompanyId = objLicenseInsuranceRequest.CompanyId,
                        ModifiedById = objLicenseInsuranceRequest.ModifiedById,
                        LicInsMapId = objLicenseInsuranceRequest.LicInsMapId,
                        Status = objLicenseInsuranceRequest.Status,
                        LicInsMasterId = objLicenseInsuranceRequest.LicInsMasterId,
                        OperationType = objLicenseInsuranceRequest.OperationType
                    };
                }
                return objdecisionPointRepository.ApprovedStatus(objLicenseInsuranceRequestParam);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Sterling Mapping
        /// <summary>
        /// Used for save sterling request and response details
        /// </summary>
        /// <param name="objSterlingResponseParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public SterlingWithDpResponse SaveSterlingLog(SterlingResponse objSterlingResponse)
        {
            try
            {
                SterlingWithDpResponseParam objSterlingWithDpResponseParam = new SterlingWithDpResponseParam();
                objdecisionPointRepository = new DecisionPointRepository();
                //assign all properties from BAL layer to DAl layer
                objSterlingResponseParam = new SterlingResponseParam()
                {
                    DpCompanyId = objSterlingResponse.DpCompanyId,
                    DpUserId = objSterlingResponse.DpUserId,
                    OrderStatus = objSterlingResponse.OrderStatus,
                    OrderScore = objSterlingResponse.OrderScore,
                    OrganizationId = objSterlingResponse.OrganizationId,
                    PackageId = objSterlingResponse.PackageId,
                    PackageName = objSterlingResponse.PackageName,
                    SterlingOrderId = objSterlingResponse.SterlingOrderId,
                    SterlingClientRefId = objSterlingResponse.SterlingClientRefId,
                    Type = objSterlingResponse.Type,
                    ModifiedBy = objSterlingResponse.ModifiedBy,
                    ResponseFileUrl = objSterlingResponse.ResponseFileUrl,
                    RequestFileUrl = objSterlingResponse.RequestFileUrl,
                    UniqueRequestId = objSterlingResponse.UniqueRequestId,
                    FirstName = objSterlingResponse.FirstName,
                    LastName = objSterlingResponse.LastName,
                    MiddelName = objSterlingResponse.MiddelName,
                    EmailId = objSterlingResponse.EmailId,
                    PhoneNumber = objSterlingResponse.PhoneNumber,
                    State = objSterlingResponse.State,
                    City = objSterlingResponse.City,
                    CountyCode = objSterlingResponse.CountyCode,
                    ZipCode = objSterlingResponse.ZipCode,
                    Address = objSterlingResponse.Address,
                    DOB = objSterlingResponse.DOB,
                    ReviewResultUrl = objSterlingResponse.ReviewResultUrl,
                    LicenseNumber = objSterlingResponse.LicenseNumber,
                    LicenseCountryCode = objSterlingResponse.LicenseCountryCode,
                    LicenseStateCode = objSterlingResponse.LicenseStateCode,
                    LicenseExpDate = objSterlingResponse.LicenseExpDate,

                };
                objSterlingWithDpResponseParam = objdecisionPointRepository.SaveSterlingLog(objSterlingResponseParam);
                if (!object.Equals(objSterlingWithDpResponseParam, null))
                {
                    objSterlingWithDpResponse = new SterlingWithDpResponse()
                    {
                        DPCompanyId = objSterlingWithDpResponseParam.DPCompanyId,
                        DpUserId = objSterlingWithDpResponseParam.DpUserId,
                        PackageId = objSterlingWithDpResponseParam.PackageId
                    };
                }
                if (!string.IsNullOrEmpty(objSterlingResponseParam.ResponseFileUrl))
                {
                    //send confirmation mail
                    string signature = objdecisionPointRepository.GetSignature(objSterlingResponseParam.DpUserId);
                    string subject = objSterlingResponseParam.PackageName + Shared.SingleSpace + DecisionPointR.SterlingResSubject;
                    if (signature != null)
                    {
                        MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                        {
                            PersonName = objSterlingResponseParam.FirstName + Shared.SingleSpace + objSterlingResponseParam.LastName,
                            Signature = signature,
                            DueDate = "ASAP",
                            DomainUrl = DecisionPointRepository.GetSiteUrl(),
                            Action = DecisionPointR.SterlingResAction1 + objSterlingResponseParam.PackageName + DecisionPointR.SterlingResAction2
                        };
                        objMailInviteFormat.FilePath = Convert.ToString(ConfigurationManager.AppSettings["ICInviteMailAlert"], CultureInfo.InvariantCulture);

                        string body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                        GetSmtpDetail(objSterlingResponseParam.EmailId, body, subject);
                    }
                }
                return objSterlingWithDpResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Used for save SterlingScreeningLog
        /// </summary>
        /// <param name="objSterlingResponseParam"></param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public int SaveSterlingScreeningLog(SterlingResponse objSterlingResponse)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                //assign all properties from BAL layer to DAl layer
                objSterlingResponseParam = new SterlingResponseParam()
                {
                    ScreenType = objSterlingResponse.ScreenType,
                    ScreenClientRefId = objSterlingResponse.ScreenClientRefId,
                    ScreenCloseDate = objSterlingResponse.ScreenCloseDate,
                    ScreenOpenDate = objSterlingResponse.ScreenOpenDate,
                    ScreenOrderStatus = objSterlingResponse.ScreenOrderStatus,
                    ScreenQualifier = objSterlingResponse.ScreenQualifier,
                    ScreenResultStatus = objSterlingResponse.ScreenResultStatus,
                    UniqueRequestId = objSterlingResponse.UniqueRequestId,
                    DpUserId = objSterlingResponse.DpUserId,
                    PackageId = objSterlingResponse.PackageId,
                    DpCompanyId = objSterlingResponse.DpCompanyId
                };
                return objdecisionPointRepository.SaveSterlingScreeningLog(objSterlingResponseParam);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Used for check uniqueRequestId no already genrated by system or not
        /// </summary>
        /// <param name="uniqueRequestId"></param>
        /// <returns>bool</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>3 Mar 2015</createddate>
        public bool CheckUnqiueSterlingRequest(string uniqueRequestId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.CheckUnqiueSterlingRequest(uniqueRequestId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// get packageName as per pacakge Id
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns>string</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>20 Mar 2015</createddate>
        public string GetPackageNameAsPerId(int packageId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetPackageNameAsPerId(packageId);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Check User Existence
        /// <summary>
        /// Used for check email exist or not in DP System
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="type"></param>
        /// <returns>string</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createddate>30 Mar 2015</createddate>
        public string CheckUserExistence(string emailId, string type)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string result = objdecisionPointRepository.CheckUserExistence(emailId, type);
                return result;
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for check email exist or not with other user Id in DP System
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="type"></param>
        /// <returns>string</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createddate>30 Mar 2015</createddate>
        public string CheckUserEmailExistence(string emailId, int userId, string userType, string companyId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                string result = objdecisionPointRepository.CheckUserEmailExistence(emailId, userId, userType, companyId);
                return result;
            }
            catch
            {

                throw;
            }
        }
        #endregion



        #region IC Approval LiIst
        public IList<ICResponse> GetICBackgroundReviewDetails(string companyId, int type, string filterTerm)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                IList<ICResponse> icBackGroundReviewList = objdecisionPointRepository.GetICBackgroundReviewDetails(companyId, type, filterTerm).Select(x => new ICResponse
                {
                    fname = x.Fname,
                    mname = x.Mname,
                    lname = x.Lname,
                    businessName = x.BusinessName,
                    emailId = x.EmailId,
                    VTId = x.VTId,
                    VType = x.VType,
                    BGStatus = x.BGStatus,
                    Source = x.Source,
                    ICUserId = x.ICUserId,
                    ICCompanyId = x.ICCompanyId,
                    SterlingOrderId = x.SterlingOrderId,
                    IsRegistred = x.IsRegistered,
                    LastInviteMailDate = x.LastInviteMailDate
                }).ToList();
                return icBackGroundReviewList;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        public int companyPaymentbyPayPal(PaymentAmountResponse objPaymentAmountResponse)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                if (!string.IsNullOrEmpty(Convert.ToString(objPaymentAmountResponse.Result)))
                {
                    PaymentAmountResponseParam paymentAmountResponseParam = new PaymentAmountResponseParam
                    {
                        CompanyCode = objPaymentAmountResponse.CompanyCode,
                        CompanyFee = objPaymentAmountResponse.CompanyFee,
                        PerFieldStaffFee = objPaymentAmountResponse.PerFieldStaffFee,
                        PerOfficeStaffFee = objPaymentAmountResponse.PerOfficeStaffFee,
                        PerIcFee = objPaymentAmountResponse.PerIcFee,
                        IsInvoice = objPaymentAmountResponse.IsInvoice,
                        NoOfPartners = objPaymentAmountResponse.NoOfPartners,
                        NoOfStaff = objPaymentAmountResponse.NoOfStaff,
                        NoOfIc = objPaymentAmountResponse.NoOfIc,
                        BusinessName = objPaymentAmountResponse.BusinessName,
                        TransactionType = objPaymentAmountResponse.TransactionType,
                        TransactionCode = objPaymentAmountResponse.Result,
                        TransactionMessage = objPaymentAmountResponse.TransactionMessage,
                        userId = objPaymentAmountResponse.userId,
                        InviteeCompanyId = objPaymentAmountResponse.InviteeCompanyId,
                        PayAmount = objPaymentAmountResponse.PayAmount,
                        ReceiverEmailId = objPaymentAmountResponse.ReceiverEmailId,
                        PaymentDate = objPaymentAmountResponse.PaymentDate,
                        Currency = objPaymentAmountResponse.Currency,
                        PayerEmailId = objPaymentAmountResponse.PayerEmailId
                    };
                    int results = objdecisionPointRepository.MakePayment(paymentAmountResponseParam);
                    return results;
                }
                else
                {

                    return 0;
                }
            }
            catch
            {
                throw;
            }
        }

        #region DeActivate
        /// <summary>
        /// Used for deactivate the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>int</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>11 Apr 2015</CreatedDate>
        public int DeactivateUser(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.DeactivateUser(userId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region IC Prspective Clients
        /// <summary>
        /// used for get ic prspective clients list
        /// </summary>
        /// <returns>IEnumerable<VendorClientList></returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>14 Apr 2015</CreatedDate>
        public IEnumerable<VendorsList> ICProspectiveClients()
        {
            IEnumerable<VendorsList> icProspectiveClients = null;
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                icProspectiveClients = objdecisionPointRepository.ICProspectiveClients().Select(x => new VendorsList
                {
                    vendor = x.Vendor,
                    contact = x.Contact,
                    emailId = x.emailId,
                    phone = x.phone,
                    Id = x.Id,
                    CompanyId = x.companyId,
                    VendorTypeId = x.VendorTypeId
                }).ToList();
                return icProspectiveClients;
            }
            catch
            {
                throw;
            }
        }

        #endregion
        #region Check Basic Free
        public bool GetIsBasicFreeForIC(int userId)
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Business Class
        /// <summary>
        /// Used for get the list of Business Class
        /// </summary>
        /// <returns>List<BusinessClass></returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>12 may 2015</CreatedDate>
        public List<BusinessClassResponse> GetBusinessClass()
        {
            try
            {
                objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.GetBusinessClass().Select(x => new BusinessClassResponse { BusinessClass = x.BusinessClass, IsActive = x.IsActive }).ToList();
            }
            catch
            {
                throw;
            }
        }
        #endregion
        /// <summary>
        /// dispose repository object if require
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (_disposing)
            {
                return;
            }

            if (disposing)
            {
                if (objdecisionPointRepository != null)
                {
                    objdecisionPointRepository.Dispose();
                    objdecisionPointRepository = null;
                }
            }
            _disposing = true;
        }
    }
}
