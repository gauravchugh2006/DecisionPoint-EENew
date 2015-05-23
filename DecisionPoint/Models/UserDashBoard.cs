using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;
using DecisionPointBAL.Implementation.Request;

namespace DecisionPoint.Models
{
    public class UserDashBoard
    {
        #region Global Variable
        private bool defaultValueForIsClient = true;
        private bool defaultValueForIsVendor = true;
        private bool defaultValueForIsCoveragearea = true;
        private bool defaultValueForIsBgc = true;
        private bool defaultValueForIsAddcre = true;
        private bool defaultValueForIsLicIns = true;
        private bool defaultValueForIsIc = true;
        private bool defaultValueForIsServices = true;
        #endregion
        #region Meaages
        /// <summary>
        /// Used for get the list of messages details
        /// </summary>
        public IEnumerable<UserDashBoardResponse> MessagesDetails { get; set; }
        #endregion

        #region Document
        /// <summary>
        /// Used for get the list of documents and courses
        /// </summary>
        public IEnumerable<UserDashBoardResponse> DocumentsDetails { get; set; }
        #endregion

        #region History
        /// <summary>
        /// Used for get the list of history form lical database
        /// </summary>
        public IEnumerable<UserDashBoardResponse> HistoryDetails { get; set; }
        /// <summary>
        /// Used for get the list of history form lical database
        /// </summary>
        public IList<string> Referencelist { get; set; }
        /// <summary>
        /// Used for get the list of history form lical database
        /// </summary>
        public IList<UserDashBoardResponse> Search { get; set; }
        #endregion

        #region Account Profile
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string LName { get; set; }
        /// <summary>
        /// Used for NickName of user
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Get & Set User Type
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set Address 1
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// get & set Address 2
        /// </summary>
        public string AddressLine2 { get; set; }
        /// <summary>
        /// get & set Free Basic Membership
        /// </summary>

        public bool IsFreeBasicMembership { get; set; }
        /// <summary>
        /// get & set BioInfo
        /// </summary>
        public string BioInfo { get; set; }

        /// <summary>
        /// get & set ServicesStatus
        /// </summary>
        public int ServicesStatus { get; set; }
        /// <summary>
        /// get & set TitleName
        /// </summary>
        public string TitleName { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public IEnumerable<string> Services { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public IEnumerable<CoverageAreaModel> CoverageArea { get; set; }
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> TitleDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> RoleDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> PermissionDetails { get; set; }
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IList<string> ServiceDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> Clientlist { get; set; }

        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IList<string> ClientDetails { get; set; }
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ServiceList { get; set; }

        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> TitleList { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> RoleList { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> PermissionList { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string DirectPhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? TitleId { get; set; }
        /// <summary>
        /// get & set Vendor apply or not
        /// </summary>
        public bool IsVendorApply { get { return defaultValueForIsVendor; } set { defaultValueForIsVendor = value; } }
        /// <summary>
        /// get & set client apply or not
        /// </summary>
        public bool IsClientApply { get { return defaultValueForIsClient; } set { defaultValueForIsClient = value; } }

        /// <summary>
        /// get & set IC apply or not
        /// </summary>
        public bool IsICApply { get { return defaultValueForIsIc; } set { defaultValueForIsIc = value; } }

        /// <summary>
        /// get & set IsCoverageAreaApply
        /// </summary>
        public bool IsCoverageAreaApply { get { return defaultValueForIsCoveragearea; } set { defaultValueForIsCoveragearea = value; } }
        /// <summary>
        /// get & set IsBgCheckApply
        /// </summary>
        public bool IsBgCheckApply { get { return defaultValueForIsBgc; } set { defaultValueForIsBgc = value; } }
        /// <summary>
        /// get & set IsServicesApply
        /// </summary>
        public bool IsServicesApply { get { return defaultValueForIsServices; } set { defaultValueForIsServices = value; } }
        /// <summary>
        /// get & set IsLicInspply
        /// </summary>
        public bool IsLicInspply { get { return defaultValueForIsLicIns; } set { defaultValueForIsLicIns = value; } }
        /// <summary>
        /// get & set IsAddCreApply
        /// </summary>
        public bool IsAddCreApply { get { return defaultValueForIsAddcre; } set { defaultValueForIsAddcre = value; } }

        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? PermissionId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? ServiceId { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<StateRespone> StateDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<CityResponse> CityDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<CountyResponse> CountyDetails { get; set; }
        // public IList<string> countyDetail { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<ZipResponse> ZipDetails { get; set; }
        /// <summary>
        /// get & set RegisteredDate
        /// </summary>
        public DateTime? RegisteredDate { get; set; }
        /// <summary>
        /// Used for get the list of CompanyVendorTypeDetails
        /// </summary>
        public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }
        /// <summary>
        /// get & set prefessional License Details
        /// </summary>
        public IList<LicenseInsuranceResponse> ProfessionalLicense { get; set; }

        /// <summary>
        /// get & set prefessional Insurance Details
        /// </summary>
        public IList<LicenseInsuranceResponse> ProfessionalInsurance { get; set; }

        public IList<LicenseInsuranceResponse> AdditionalRequirements { get; set; }

        public IList<UserDashBoardResponse> ProfessionalCommunications { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string StreetNumber { get; set; }
        /// <summary>
        /// direction
        /// </summary>
        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string Direction { get; set; }
        /// <summary>
        /// street name
        /// </summary>
        [RegularExpression("^[_&.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string StreetName { get; set; }
        /// <summary>
        /// get & set City Name
        /// </summary>
        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string CityName { get; set; }
        /// <summary>
        /// get & set State Name
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// Get & set Zip Code
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// get & set Services count
        /// </summary>
        public int ServiceCount { get; set; }
        /// <summary>
        /// get & set CoverageAreaCount
        /// </summary>
        public int CoverageAreaCount { get; set; }
        /// <summary>
        /// get & set ParentCompanyName
        /// </summary>
        public string ParentCompanyName { get; set; }
        /// <summary>
        /// get & set ParentUserId
        /// </summary>
        public string ParentUserId { get; set; }
        /// <summary>
        /// get & set Coverage Area Status
        /// </summary>
        public string CoverageAreaStatus { get; set; }
        /// <summary>
        /// get & set SaveClose
        /// </summary>
        public bool SaveClose { get; set; }
        /// <summary>
        /// get & set VendorTypeDetails
        /// </summary>
        public List<string> VendorTypeDetails { get; set; }
        /// <summary>
        /// get & set ParentCompanyId
        /// </summary>
        public string ParentCompanyId { get; set; }
        /// <summary>
        /// get & set IsEditVisibilityDone
        /// </summary>
        public bool IsEditVisibilityDone { get; set; }
        public string CertifyingAgency { get; set; }
        public string CertificationNumber { get; set; }
        public string BusinessClass { get; set; }
        public DateTime? CertificateExpDate { get; set; }
        public List<BusinessClassResponse> BusinessClassDetails { get; set; }
        #endregion

        #region paging
        public int PageSize { get; set; }
        public int RowperPage { get; set; }
        #endregion

        #region Reqiured Documents
        /// <summary>
        /// Used for get the list of reqiured documents dertails
        /// </summary>
        public IEnumerable<UserDashBoardResponse> ReqiuredDocumentsDetails { get; set; }
        /// <summary>
        /// Used for get the location of particluar document
        /// </summary>
        public string Reqdocloc { get; set; }
        /// <summary>
        /// Used for get the name of particluar document
        /// </summary>
        public string Reqdocname { get; set; }
        /// <summary>
        /// Used for get the id of particluar document
        /// </summary>
        public int Reqdocid { get; set; }
        /// <summary>
        /// to check global or specific
        /// </summary>
        public int ReqType { get; set; }
        /// <summary>
        /// to check status
        /// </summary>
        public int IsCompleted { get; set; }
        /// <summary>
        /// Used for get the list of specific documents dertails
        /// </summary>
        public IEnumerable<UserDashBoardResponse> SpecificDocumentsDetails { get; set; }
        public IEnumerable<StateRespone> StateList { get; set; }
        public int StateId { get; set; }
        public int StateAbbre { get; set; }
        public string Globauploadedcontentpath { get; set; }
        public string Specificuploadedcontentpath { get; set; }
        public string ProfLicuploadedcontentpath { get; set; }
        public string Insuranceuploadedcontentpath { get; set; }
        public string AdditionalRequploadedcontentpath { get; set; }
        public string HostURL { get; set; }
        #endregion



        #region Document View/Communication View
        public IEnumerable<UserViewResponse> GetUserViewList { get; set; }
        public IEnumerable<UserAssesmentResponse> GetAssesmentViewList { get; set; }
        public IEnumerable<UserAssesmentAnswerResponse> GetAssesmentAnsViewList { get; set; }
        public IEnumerable<UserInstructionsResponse> GetInstructions { get; set; }
        public IEnumerable<UserTimeSpanResponse> GetUSerTimeSpan { get; set; }
        public IEnumerable<UserTimeSpanResponse> GetUserCompletionDate { get; set; }
        public string Instructions { get; set; }
        public IEnumerable<UserAcknowledgmentResponse> GetAcknowledgment { get; set; }
        public IEnumerable<UserTrainingMaterialsResponse> GetUserDocStatus { get; set; }
        public int Checkassesment { get; set; }
        #endregion

        #region payment detail
        public string CompanyCode { get; set; }
        [Required(ErrorMessage = "Please enter Company Fee ")]
        public double? CompanyFee { get; set; }

        [Required(ErrorMessage = "Please enter Satff Fee ")]
        public double? PerFieldStaffFee { get; set; }


        [Required(ErrorMessage = "Please enter Office Fee")]
        public double? PerOfficeStaffFee { get; set; }

        [Required(ErrorMessage = "Please enter IC Fee")]
        public double? PerICFee { get; set; }

        [Display(Name = "Invoice")]
        public bool IsInvoice { get; set; }

        public DateTime? LastUpdatedDate { get; set; }
        #endregion

        public int Id { get; set; }

        #region Incoming
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ReferenceDetails { get; set; }
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> GroupDetails { get; set; }
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> CategoryDetails { get; set; }
        #endregion

        #region Get Correct Answer
        public IList<UserCorrectAsstAnswerResponse> GetCorrectAnswer { get; set; }
        #endregion

        #region IC Clien List
        public IEnumerable<VendorsList> CurrentClientList { get; set; }
        public IEnumerable<VendorsList> ICProspectiveClientList { get; set; }
        public IEnumerable<VendorsList> ICPerProspectiveClientList { get; set; }
        public IEnumerable<VendorsList> PastClientList { get; set; }
        #endregion

        #region Background
        public IList<BackGroundCheckMasterResponse> BackgroundList { get; set; }

        public string BackgroundTitle { get; set; }

        public string BackgroundSource { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public string Documents { get; set; }

        public string BackgroundCheckStatus { get; set; }

        public DateTime DateTime { get; set; }

        public string Source { get; set; }

        public string CreatorName { get; set; }

        public bool IsActive { get; set; }

        public string BackgroundCheckDocPath { get; set; }

        public int MasterId { get; set; }

        public string Notes { get; set; }
        public bool IsDNA { get; set; }

        public DateTime ExpirationDate { get; set; }

        public IList<BackGroundCheckMasterRequest> BackgroundListByVisitor { get; set; }
        #endregion


    }


}