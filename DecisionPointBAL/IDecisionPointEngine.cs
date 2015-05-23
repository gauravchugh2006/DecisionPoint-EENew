// ********************************************************************************************************************************
//                                                  class:IDecisionPointEngine
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 18, 2013    |Arun Kumar  |Sumit Saurav | Bobis   | This interface is used for control all business layer methods.
// *********************************************************************************************************************************


using System;
using System.Collections.Generic;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPointBAL
{
    /// <summary>
    /// Interface to maintain all business layer methods
    /// </summary>
    interface IDecisionPointEngine
    {
        #region DashBoard

        /// <summary>
        /// This method used for remove the documents of particular user [We just update the deleted status of that message in loval database]
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="MsgId"></param>
        int RemoveDocument(int Id, int type, string companyId);
        /// <summary>
        /// Used for  update the Student view document  of particular user
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="accepted"></param>
        /// <param name="Assessmentresult"></param>
        /// <param name="status"></param>
        int SaveMarkAsComplete(int tableId, bool accepted, int DocID);

        /// <summary>
        /// To update Assessment Test result Pass or fail
        /// </summary>
        /// <param name="tableId">int</param>
        /// <param name="assessmentResult">string</param>
        /// <returns>int</returns>
        int SaveAssessmentResult(int tableId, string assessmentResult);

        int SaveUserAsstAttempts(int userId, int docId, int attempt);

        int SaveDocTimeSpent(DocTimeSpentRequest docTimeSpentRequest);
        IList<UserTimeSpanResponse> GetUSerTimeSpan(int deliveredUserId);
        /// <summary>
        /// Used for get the correct Answer of assessment Question 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="DocId"></param>
        /// <returns></returns>
        IList<UserCorrectAsstAnswerResponse> GetCorrectAnswer(int DocId, int UserId);
        /// <summary>
        /// This method used for get documents details as Per particular user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<UserDashBoardResponse> GetDocumentsDetails(int UserId, string type, string CompanyId, string filtervalues);


        /// <summary>
        /// ///
        /// </summary>
        /// <param name="UserId"></param>
        IEnumerable<UserDashBoardResponse> GetHistoryDetails(int UserId, string filtervalues, string Companyid, string type);

        /// <summary>
        /// Used For get account profile details from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        UserDashBoardResponse GetAccountDetails(int UserId);
        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<UserDashBoardResponse> GetReqiuredDocuments(int UserId, string companyId);

        /// <summary>
        /// Used for submit the reqiurement documents of particular user
        /// </summary>
        /// <param name="objUserDashBoardResponse"></param>
        /// <returns></returns>
        int SubmitRequireDocument(SubmitReqDocRequest objSubmitReqDocRequest);


        IList<UserDashBoardResponse> Search(string term, int UserId, string type, int searchtype);
        ///// <summary>
        ///// Get the Roles of particular user
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //IEnumerable<RoleResponse> GetUserRoles(int UserId);
        /// <summary>
        /// Used for get the skill of aprticular user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetUserSkills(int UserId);
        /// get services from service master
        /// </summary>
        /// <returns>retrun the roles of particular user</returns>
        IList<string> GetUserServices(int UserId, string companyId);
        /// <summary>
        /// Used for update my profile
        /// </summary>
        /// <param name="objUserDashBoardRequestParam"></param>
        /// <returns></returns>
        int Updatemyprofile(UserDashBoardRequest objUserDashBoardRequest, string type);
        IList<StateRespone> GetStateList(int UserId, string companyId, int type);
        IList<CityResponse> GetCityListByStateName(string statename, int UserId);
        IList<CountyResponse> GetCountyListByStateName(string statename, int UserId);
        IList<ZipResponse> GetzipListByStateName(string statename, int UserId);
        IList<CityResponse> GetCityList(int UserId, string companyId, int type);
        IList<CountyResponse> GetCountyList(int UserId, string companyId, int type);
        IList<ZipResponse> GetzipList(int UserId, string companyId, int type);
        /// <summary>
        /// Reactive the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Reactivestaff(int id, string companyId);
        /// <summary>
        /// remove the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Removetaff(int id, string companyId);
        #endregion
        #region Admin Super Admin Settings
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDashBoardResponse> GetTitle(string type, string ID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        int DisaEnaTitle(int titleId, bool isActive);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        int AddTitle(string title, string CompanyId, int UserId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        int AddClient(string title, int UserId, string CompanyId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDashBoardResponse> GetClient(string type, string ID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        int DisaEnaClient(int clientId, bool isActive);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="titlename"></param>
        /// <returns></returns>
        int UpdateClient(int clientId, string titlename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="titlename"></param>
        /// <returns></returns>
        int UpdateTitle(int titleId, string titlename, String CompanyID);
        /// <summary>
        /// get company id
        /// </summary>
        /// <returns>returns company id</returns>
        IEnumerable<CompanyIdResponse> getCompanyId();
        /// <summary>
        /// get ADMIN profile
        /// </summary>
        /// <param name="CompanyId">company id</param>
        /// <returns>profile details</returns>
        IEnumerable<AdminProfileResponse> GetAdminProfile(string CompanyId);
        /// <summary>
        /// saves state mapping
        /// </summary>
        /// <param name="stateRequest">state details</param>
        /// <returns>int type saved or not</returns>
        int SaveStateMapping(StateRequest stateRequest, string type);
        /// <summary>
        /// saves county mapping
        /// </summary>
        /// <param name="stateRequest">county details</param>
        /// <returns>int type saved or not</returns>
        int SaveCountyMapping(CountyRequest countyRequest, string type);
        /// <summary>
        /// saves city mapping
        /// </summary>
        /// <param name="stateRequest">city details</param>
        /// <returns>int type saved or not</returns>
        int SaveCityMapping(CityRequest cityRequest, string type);
        /// <summary>
        /// saves zip mapping
        /// </summary>
        /// <param name="stateRequest">zip details</param>
        /// <returns>int type saved or not</returns>
        int SaveZipMapping(ZipRequest zipRequest, string type);
        string GetUserNameFromUserId(string companyId);
        string GetCompanyNameByUserType(string companyId, string UserType);
        int GetStaffCompanyId(string companyId);
        int GetAdminUserId(string companyId);
        string GetInvitationFlow(string UserId);
        int SetTempPay(int userId);
        byte SetDefaultFeeDetail(UserDashBoardRequest objUserDashBoardRequest);
        UserDashBoardResponse GetDefaultFeeDetail();
        string validateVendorProfile(int userId);
        int ChangeRegistrationStatus(int userId, int inviteid, string type, string companyId);
        #region Service
        /// <summary>
        /// Get Service Details
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDashBoardResponse> GetService(string type);
        /// <summary>
        /// Enable and disable the service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        int DisaEnaService(int serviceId, bool isActive);
        /// <summary>
        /// Add the Service
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        int AddService(string service, int UserId, string CompanyId);
        /// <summary>
        /// Update the service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="servicename"></param>
        /// <returns></returns>
        int UpdateService(int serviceId, string serviceName, string CompanyId);
        /// <summary>
        /// Used to save the deatil for setup training
        /// </summary>
        /// <param name="doctitle"></param>
        /// <param name="duedate"></param>
        /// <param name="intro"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        string SaveCommunication(CommunicationBasicDetailsRequest objCommunicationBasicDetailsRequest, string type, string docid);
        /// <summary>
        /// Used to save the likns inculded in setup traininhs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        int SaveCommLinks(int docId, string linkValue, string type, int linkid);
        /// <summary>
        /// Used to save the likns inculded in setup traininhs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        int SaveCommReqActions(int docId, string reqActionval, string type, int reqaction);
        #endregion

        #endregion

        #region Sumit Code>>>>>>>

        /// <summary>
        /// change pasword of user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">new password</param>
        /// <returns>int type password change or not</returns>
        int ChangePassword(ChangePasswordRequest changePasswordRequest);

        /// <summary>
        /// get security question list for registration page
        /// </summary>
        /// <returns>security questions</returns>
        IEnumerable<SecurityQuestionResponse> GetSecurityQuestion();

        ///// <summary>
        ///// get  list OF ROLES for registration page
        ///// </summary>
        ///// <returns>roles list</returns>
        //IEnumerable<RoleResponse> GetRoles();
        /// <summary>
        /// Used for get all Announcement to show on login page
        /// </summary>
        IEnumerable<AnnouncementResponse> GetAnnouncement();
        /// <summary>
        /// login users with user id and password
        /// </summary>
        /// <param name="userid">user id</param>
        /// <param name="password">password</param>
        /// <returns>login response sucess or not</returns>
        LoginDetailResponse Login(string userid, string password);
        /// <summary>
        /// get payment amount for office staff, filed staff etc..
        /// </summary>
        /// <param name="CompanyId">company id</param>
        /// <returns>payment amount response</returns>
        IEnumerable<PaymentAmountResponse> getPaymentAmount(string CompanyId);
        /// <summary>
        /// get zip list by city
        /// </summary>
        /// <param name="CityName">city name</param>
        /// <returns>list of zip codes as per city name</returns>
        IEnumerable<ZipResponse> GetZipListByCity(string CityName, string stateabbrelist, string county);
        /// <summary>
        /// get city list as per zip code
        /// </summary>
        /// <param name="ZipCode">zip code</param>
        /// <returns>list of zip codes</returns>
        IEnumerable<ZipResponse> GetCityListByzip(string ZipCode);

        /// <summary>
        /// get state list
        /// </summary>
        /// <returns>state list</returns>
        IEnumerable<StateRespone> GetStateList();
        /// <summary>
        /// get citylist as per county id
        /// </summary>
        /// <param name="countyId">county id</param>
        /// <returns>list of city</returns>
        IEnumerable<CityResponse> GetCityList(string countyId);
        /// <summary>
        /// set company profile
        /// </summary>
        /// <param name="companyProfileRequest">parameters to save company profile</param>
        /// <returns>int type sucess or fails</returns>
        int SetCompanyProfile(CompanyProfileRequest companyProfileRequest);
        /// <summary>
        /// get compamy profile details usig user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>company profile details</returns>
        CompanyProfileResponse GetCompanyProfileDetails(int userId);
        /// <summary>
        /// save admin profile
        /// </summary>
        /// <param name="adminProfileRequest">parameters to save admin profile</param>
        /// <returns>save success or not </returns>
        int SetAdminProfile(AdminProfileRequest adminProfileRequest);
        /// <summary>
        /// get admin profile 
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>admin profile details</returns>
        IEnumerable<AdminProfileResponse> GetAdminProfile(int userId);
        /// <summary>
        /// get county list as per state abbreviations
        /// </summary>
        /// <param name="StateAbbre">state abbreviations</param>
        /// <returns>conty list</returns>
        IEnumerable<CountyResponse> GetCountyList(string StateAbbre);
        /// <summary>
        /// get city list by state abbreviations
        /// </summary>
        /// <param name="StateAbbre"></param>
        /// <returns></returns>
        IEnumerable<CityResponse> GetCityListByState(string StateAbbre);
        /// <param name="email">email</param>
        /// <returns>checks email</returns>
        string CheckEmail(string email);

        /// <summary>
        /// ///
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        int GetCompanyRegister(UserRegisterRequest userRegisterRequest);
        /// <summary>
        /// This method is used to infomation of vendor on the basis of comapny ID
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        VendorBulkResponse GetVendorInfo(string companyId, string Usertype);

        #endregion

        #region Performance
        /// <summary>
        /// This method is used to calculate the Performance of Staff
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        IEnumerable<StaffPerformaceResponse> GetStaffPerformanceList(int userId, string companyID);
        #endregion

        #region Assessment
        /// <summary>
        /// Used to get the Training Materials Details of a User for Student View
        /// </summary>
        /// <param name="UserSelectedAnswersRequest">UserSelectedAnswersRequest</param>        
        int SaveUserSelectedAnswers(UserSelectedAnswersRequest userSelectedAnswersRequest);

        #endregion


        #region Mail Footer
        int MailFooter(MailFooterRequest objMailFooter);
        #endregion

        #region Mail matrix
        void mailReminder();
        #endregion

        IList<ReceiverReqDocResponse> GetReceiverRequiredDoc(int reqDocId, int userId, string companyId);
        int SetReceiverReqDocDetails(ReceiverReqDocRequest objRequest);
        // int SetBackgroundCheckMaster(BackGroundCheckMasterRequest objRequest);
    }
}
