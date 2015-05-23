using System;
using System.Collections.Generic;
using DecisionPointDAL.Implemention.RequestParam;
// ********************************************************************************************************************************
//                                                  class:IDecisionPointRepository
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 16, 2013    |Arun Kumar | Bobi S     | This Interface represents represents methods of DAL for whole application
// *********************************************************************************************************************************
using DecisionPointDAL.Implemention.ResponseParam;

namespace DecisionPointDAL
{
    /// <summary>
    /// contain defitions of methods which implemented with DecisionPointRepository.cs
    /// </summary>
    interface IDecisionPointRepository
    {
        #region dashboard

         /// <summary>
        /// Used For get withdrawn item from Database as Per particular User
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>retrun the messages/document/courses details in ienumberable form</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 11 2013</createdDate>
        IEnumerable<UserDashBoardResponseParam> GetWithdrawnHistoryDetails(int UserId, string type, string companyId, string filtervalues);
         /// <summary>
        /// Used For get History from Database as Per particular company
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>retrun the messages/document/courses details in ienumberable form</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>may 10 2014</createdDate>
        IEnumerable<UserDashBoardResponseParam> GetGlobalLibrary(string CompanyId, string filtervalues);
        
        

      /// <summary>
        ///  This method used for remove the document of particular user [We just update the deleted status of that message in loval database]
        /// </summary>
        /// <param name="Id">Id</param>
        /// <param name="type">type</param>
        /// <returns>int if messages is deleted return 1 else return zero</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 5 2013</createdDate>
         int ReactiveDocument(int Id, int type,string companyID);
        /// <summary>
        /// This method used for get documents details as Per particular user
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 5 2013</createdDate>
        /// <returns>UserDashBoardResponseParam</returns>
        IEnumerable<UserDashBoardResponseParam> GetDocumentsDetails(int UserId, string type, string CompanyId, string filtervalues);

        /// <summary>
        /// Used for get the correct Answer of assessment Question 
        /// </summary>
        /// <param name="UserId">UserId</param>
        /// <param name="DocId">DocId</param>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 5 2013</createdDate>
        /// <returns>UserCorrectAsstAnswerParam</returns>
        IList<UserCorrectAsstAnswerParam> GetCorrectAnswer(int DocId, int UserId);
        /// <summary>
        /// This method used for remove the document of particular user [We just update the deleted status of that message in loval database]
        /// </summary>
        /// <param name="docId">docId</param>
        /// <param name="">type</param>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 5 2013</createdDate>
        /// <returns>int</returns>
        int RemoveDocument(int docId, int type,string companyId);

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

       int SaveDocTimeSpent(DocTimeSpentRequestParam docTimeSpentRequestParam);

       IList<UserTimeSpanParams> GetUSerTimeSpan(int deliveredUserId);
       

        /// <summary>
        /// Used for get history section details from local database
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<UserDashBoardResponseParam> GetHistoryDetails(int UserId,string filtervalues,string CompanyId,string type);
        /// <summary>
        /// Used for get the profile details from local dadbase
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        UserDashBoardResponseParam GetAccountDetails(int UserId);
        /// <summary>
        /// Used For get the reqiured documents details by company/individuals
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IEnumerable<UserDashBoardResponseParam> GetReqiuredDocuments(int UserId,string companyId);
         /// <summary>
        /// Used for submit the reqiurement documents of particular user
        /// </summary>
        /// <param name="objUserDashBoardResponse"></param>
        /// <returns>retrun one if expirtaion date submitted sucessfully otherwise retrun zero</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>nov 1 2013</createdDate>
        int SubmitRequireDocument(SubmitReqDocRequestParam objSubmitReqDocRequestParam);

          /// <summary>
        /// Used to get the rquire documents detail of particular user[Like NDA , Criminal background]
        /// </summary>
        /// <param name="reqdocid">int</param>
        /// <param name="userId">int</param>
        /// <returns>retrun req doc of particular user</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>june 3 2014</createdDate>
        IEnumerable<SubmitReqDocRequestParam> GetReqDocBySender(SubmitReqDocRequestParam objSubmitReqDocRequestParam);

       
        /// <summary>
        /// Used for serach appply in history section
        /// </summary>
        /// <param name="term"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<UserDashBoardResponseParam> Serach(string term, int UserId, string type,int searchtype);
        ///// <summary>
        ///// Get the Roles of particular user
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //IEnumerable<RoleResponseParam> GetUserRoles(int UserId);
         /// <summary>
        /// Used to get permission of particular user
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="roleId">roleId</param>
        /// <param name="titleid">titleid</param>
        /// <param name="permissionId">permissionId</param>
        /// <returns>int</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>nov 20 2013</createdDate>
         int UpdateUserPermisson(int id, int titleid, int permissionId);
        /// <summary>
        /// Used for get the skill of aprticular user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetUserSkills(int UserId);
        /// <summary>
        /// get title from title master of any user
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>retrun the roles of particular user</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>nov 20 2013</createdDate>
        IEnumerable<CompanyDashBoardResponseParam> GetUserTitle(int UserId, string companyId);
         /// <summary>
        /// get services from service master
        /// </summary>
        /// <returns>retrun the roles of particular user</returns>
        IList<string> GetUserServices(int UserId, string companyId);
        /// <summary>
        /// get client from client master
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>retrun the roles of particular user</returns>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>nov 22 2013</createdDate>
         IList<string> GetUserClients(int UserId, string companyId);
        
       
         /// <summary>
        /// get DocFlow from Docflow master
        /// </summary>
        /// <returns>retrun the Docflow details in ienumberable form</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 13 2013</createdDate>
         IEnumerable<RoleResponseParam> GetDocFlow();
        /// <summary>
        /// get roles from flow master
        /// </summary>
        /// <returns>retrun the flow details in ienumberable form</returns>
        IEnumerable<RoleResponseParam> GetFlow();
      
         /// <summary>
        /// Used to get the services from lcal database
        /// </summary>RemoveDocument
        /// <returns>retrun the services details in ienumberable form</returns>
        IEnumerable<CompanyDashBoardResponseParam> GetServices(string type, string ID);
         /// <summary>
        /// Used to get the mapped services from local database for service translation table
        /// </summary>
        /// <param name="cCompanyId"></param>
        /// <param name="pCompanyId"></param>
        /// <returns>retrun the services details in ienumberable form</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>nov 16 2013</createdDate>
         IEnumerable<CompanyDashBoardResponseParam> GetMappdedServices(string pCompanyId, string cCompanyId);

        /// <summary>
        /// Used for update my profile
        /// </summary>
        /// <param name="objUserDashBoardRequestParam"></param>
        /// <returns></returns>
        int Updatemyprofile(UserDashBoardRequestParam objUserDashBoardRequestParam, string type);
        IList<StateResponseParam> GetStateList(int userId, string companyId, int type);
        IList<CityResponseParam> GetCityListByStateName(string stateName, int userId);
        IList<CountyResponseParam> GetCountyListByStateName(string stateName, int userId);
        IList<ZipResponseParam> GetZipListByStateName(string stateName, int userId);
        IList<CityResponseParam> GetCityList(int userId,string companyId,int type);
        IList<CountyResponseParam> GetCountyList(int userId, string companyId, int type);
        IList<ZipResponseParam> GetZipList(int userId, string companyId, int type);
        /// <summary>
        /// Reactive the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Reactivestaff(int id,string companyId);
         /// <summary>
        /// remove the staff
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Removetaff(int id,string companyId);
        
        int ReactiveVendor(int id,string companyid);

        int RemoveVendor(int id,string CompanyId);
         /// <summary>
        /// used for added the new title name
        /// </summary>
        /// <param name="titleName"></param>
        /// <returns>return one if title is saved else retrun zero</returns>
        int AddTitle(string titleName, string CompanyId, int UserId);
        /// <summary>
        /// used for added the new service name
        /// </summary>
        /// <param name="ServiceName"></param>
        /// <returns>return one if service is saved else retrun zero</returns>
        int AddService(string serviceName, int UserId, string CompanyId);
        #endregion

        #region Admin & Super Admin Settings
        /// <summary>
        /// used for get the title details
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDashBoardResponseParam> GetTitle(string type, string ID);

        int DisaEnaTitle(int titleId, bool isActive);
        /// <summary>
        /// Used for update the title
        /// </summary>
        /// <param name="titleId"></param>
        /// <returns></returns>
        int UpdateTitle(int titleId, string titlename, string CompanyID);
        /// <summary>
        /// used for added the new client name
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns>return one if client is saved else retrun zero</returns>
        int AddClient(string clientName, int UserId, string CompanyId);
        /// <summary>
        /// used for get the client details
        /// </summary>
        /// <returns>return client detial in ienumerbale form</returns>
        IEnumerable<CompanyDashBoardResponseParam> GetClient(string type, string ID);
        /// <summary>
        /// Used for disable and enable the client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="isActive"></param>
        /// <returns>return one if client is disable else retrun zero</returns>
        int DisaEnaClient(int clientId, bool isActive);
        /// <summary>
        /// Used for update the client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientname"></param>
        /// <returns>return one if client is update else retrun zero</returns>
        int UpdateClient(int clientId, string clientname);
        /// <summary>
        /// Manual Invitaion
        /// </summary>
        /// <param name="objCompanyDashBoardRequestParam"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        string SendInvitation(CompanyDashBoardRequestParam objCompanyDashBoardRequestParam, string type);
        /// <summary>
        /// get ADMIN profile
        /// </summary>
        /// <param name="CompanyId">company id</param>
        /// <returns>profile details</returns>
        IEnumerable<AdminProfileResponseParam> GetAdminProfile(string CompanyId);
        /// <summary>
        /// saves state mapping
        /// </summary>
        /// <param name="stateRequest">state details</param>
        /// <returns>int type saved or not</returns>
        int SaveStateMapping(StateRequestParam stateRequestParam, string type);
        /// <summary>
        /// saves county mapping
        /// </summary>
        /// <param name="stateRequest">county details</param>
        /// <returns>int type saved or not</returns>
        int SaveCountyMapping(CountyRequestParam countyRequestParam, string type);
        /// <summary>
        /// saves city mapping
        /// </summary>
        /// <param name="stateRequest">city details</param>
        /// <returns>int type saved or not</returns>
        int SaveCityMapping(CityRequestParam cityRequestParam, string type);
        /// <summary>
        /// saves zip mapping
        /// </summary>
        /// <param name="stateRequest">zip details</param>
        /// <returns>int type saved or not</returns>
        int SaveZipMapping(ZipRequestParam zipRequestParam, string type);

        /// <summary>
        /// get user name as per user id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        string GetUserNameFromUserId(string companyId);
        /// <summary>
        /// This method is used to infomation of vendor on the basis of comapny ID
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        VendorBulk GetVendorInfo(string companyId, string Usertype);

        int GetStaffCompanyId(string companyId);
        
        /// <summary>
        /// used for get the service details
        /// </summary>
        /// <returns></returns>
        IEnumerable<CompanyDashBoardResponseParam> GetService(string type);
        /// <summary>
        /// used for Enable and Disable property of the service details
        /// </summary>
        /// <returns></returns>
        int DisaEnaService(int serviceId, bool isActive);
        /// <summary>
        /// Used for update the service
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        int UpdateService(int serviceId, string titlename, String companyId);
        int GetAdminUserId(string companyId);
        string GetInvitationFlow(string UserId);
        int SetTempPay(int userId);
        byte SetDefaultFeeDetail(UserDashBoardRequestParam objUserDashBoardRequestParam);
        UserDashBoardResponseParam GetDefaultFeeDetail();
        string validateVendorProfile(int userId);
        int ChangeRegistrationStatus(int userId,int inviteid,string type,string companyId);
        #region Setup Training
        /// <summary>
        /// Used to save the deatil for setup training
        /// </summary>
        /// <param name="doctitle"></param>
        /// <param name="duedate"></param>
        /// <param name="intro"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        string SaveCommunication(CommunicationBasicDetailsRequestParam objCommunicationBasicDetailsRequestParam, string type, string docid);
        /// <summary>
        /// Used to save the likns inculded in setup traininhs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        int SaveCommLinks(int docId, string linkValue, string type,int linkid);
        /// <summary>
        /// Used to save the likns inculded in setup traininhs
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="linkValue"></param>
        /// <returns></returns>
        int SaveCommReqActions(int docId, string reqActionval, string type,int reqaction);
        #endregion
        #endregion
        

        #region << Engine Source code of Sumit >>
        
        /// <summary>
        /// get state list
        /// </summary>
        /// <returns>state list</returns>
        IEnumerable<StateResponseParam> GetStateList();
        /// <summary>
        /// get city list as per county id
        /// </summary>
        /// <param name="countyId">county id</param>
        /// <returns>city list</returns>
        IEnumerable<CityResponseParam> GetCityList(string countyId);

        /// <summary>
        /// update or set company profile
        /// </summary>
        /// <param name="companyProfileRequestParam">paramters to save company details</param>
        /// <returns>int type save sucess or fails</returns>
        int SetCompanyProfile(CompanyProfileRequestParam companyProfileRequestParam);

        /// <summary>
        /// get company profile details
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns> company profiles</returns>
        CompanyProfileResponseParam GetCompanyProfileDetails(int userId);

        /// <summary>
        /// set admin profile
        /// </summary>
        /// <param name="adminProfileRequestParam">parameters of admin profile request</param>
        /// <returns>int type sucess or fails</returns>
        int SetAdminProfile(AdminProfileRequestParam adminProfileRequestParam);
        /// <summary>
        /// get admin profile of a user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>admin profile details</returns>        
        IEnumerable<AdminProfileResponseParam> GetAdminProfile(int userId);
        /// <summary>
        /// get county list according to state Abbrebiations
        /// </summary>
        /// <param name="StateAbbre">state Abbrebiations</param>
        /// <returns>list of all counties under state abbreviations</returns>
        IEnumerable<CountyResponseParam> GetCountyList(string StateAbbre);
        /// <summary>
        /// get city list by state
        /// </summary>
        /// <param name="StateAbbre">state abbreviations</param>
        /// <returns>list of city</returns>
        IEnumerable<CityResponseParam> GetCityListByState(string StateAbbre);
       
        /// <summary>
        /// get annual fee of a company for office staff fiels staff etc.
        /// </summary>
        /// <param name="CompanyId">company id</param>
        /// <returns>detals of company fee</returns>
        IEnumerable<PaymentAmountResponseParam> getPaymentAmount(string CompanyId);
        /// <summary>
        /// get zip list by city
        /// </summary>
        /// <param name="CityName">city name</param>
        /// <returns>list of zip codes</returns>
        IEnumerable<ZipResponseParam> GetZipListByCity(string CityName, string stateabbrelist, string county);
        /// <summary>
        /// get city list by zip codes
        /// </summary>
        /// <param name="Zip">zip code</param>
        /// <returns>list of zip code by city</returns>
        IEnumerable<ZipResponseParam> GetCityListByzip(string Zip);
        /// <summary>
        /// validate email exists or not?
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>exists or not?</returns>
        string CheckEmail(string email);

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="userRegisterRequestParam"></param>
        /// <returns></returns>
        int GetCompanyRegister(UserRegisterRequestParam userRegisterRequestParam);
        /// <summary>
        /// checks existence of user id and password
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="Password">password</param>
        LoginDetailsResponseParam CheckLogin(string UserId, string Password);
        /// <summary>
        /// change pasword of user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="password">new password</param>
        /// <returns>int type password change sucess or not?</returns>
        int ChangePassword(ChangePasswordRequestParam changePasswordRequestParam);

        /// <summary>
        /// get security question list for registration page
        /// </summary>
        /// <returns>security question lists</returns>
        IEnumerable<SecurityQuestionResponseParam> GetSecurityQuestion();

        IEnumerable<CompanyIdResponseParam> getCompanyId();
        int MakePayment(PaymentAmountResponseParam objpayment);
        #endregion 

         #region Performance
        /// <summary>
        /// This method is used to calculate the Performance of Staff
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        IList<StaffPerformaceRequestParam> GetStaffPerformanceList(int userId, string companyId);

        #endregion

        #region Mail Footer
        int MailFooter(MailFooterRequestParam objMailFooter);
        #endregion

        
        #region Mail matrix
        List<MailReminder> mailReminder();
        #endregion

        IList<ReceiverReqDocResponseParam> GetReceiverRequiredDoc(int reqDocId, int userId, string companyId);
        int SetReceiverReqDocDetails(ReceiverReqDocRequestParam objRequestParam);
        int MarkReceiverDocComplete(int reqDocId, int userId, string companyId);
       // int SetBackgroundCheckMaster(BackGroundCheckMasterRequestParam objRequestParam);
        IEnumerable<BackGroundCheckMasterResponseParam> GetBackgroundMapping(BackGroundCheckMasterRequestParam objBackGroundCheckMasterRequestParam);
    }
}
