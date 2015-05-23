using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class CompanyDashBoard
    {
        #region Global Variable
        private bool defaultValueForIsClient = true;
        private bool defaultValueForIsVendor = true;
        private bool defaultValueForIsCoveragearea = true;
        private bool defaultValueForIsScorm = true;
        private bool defaultValueForIsIc = true;
        private bool defaultvalueForIsServices = true;
        #endregion
        #region Title & Client & Role & Flow & Permmossion
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> titleDetails { get; set; }
        /// <summary>
        /// get & set CommFilterValue
        /// </summary>
        public IList<SaveCommFilterResponse> CommFilterValue { get; set; }

        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> referenceDetails { get; set; }
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> categoryDetails { get; set; }
        /// <summary>
        /// Used for get the list of group details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> groupDetails { get; set; }
        /// <summary>
        /// Used for get the list of VendorTypeDetails
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> VendorTypeDetails { get; set; }
       
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> clientDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> roleDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> flowDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> permissionDetails { get; set; }

        /// <summary>
        /// Used for get the list of Services details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> serviceDetails { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<LstProp> lstBind { get; set; }
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> serviceList { get; set; }
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IList<string> userserviceDetails { get; set; }
        /// <summary>
        /// Used for get ServiceStatus
        /// </summary>
        public int ServiceStatus { get; set; }

        /// <summary>
        /// Used for get the list of contract type details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ContractTypeDetails { get; set; }

        #endregion

        #region paging
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        //Pageg Size for vendor list
        public int vendorpagesize { get; set; }


        #endregion

        #region Invitaion
        #region Manual Invitation
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffFName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffLName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffTitleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffRoleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffPermissionId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffEmailId { get; set; }

        #endregion
        #endregion

        #region Internal staff & IC
        /// <summary>
        /// Used for get the list of active staff details
        /// </summary>
        public IEnumerable<InternalstaffResponse> activeStaffdetails { get; set; }
        /// <summary>
        /// Used for get the list of staff details
        /// </summary>
        public IEnumerable<InternalstaffResponse> SearchStaffdetails { get; set; }
        /// <summary>
        /// Used for get the list of inactive staff details
        /// </summary>
        public IEnumerable<InternalstaffResponse> inactiveStaffdetails { get; set; }
        /// <summary>
        /// Used for get the list of active ic details
        /// </summary>
        public IEnumerable<ICResponse> activeICdetails { get; set; }
        /// <summary>
        /// Used for get the list of ICBGReviewDetail
        /// </summary>
        public IEnumerable<ICResponse> ICBGReviewDetail { get; set; }
        /// <summary>
        /// Used for get the list of ic details
        /// </summary>
        public IEnumerable<ICResponse> SearchICdetails { get; set; }
        /// <summary>
        /// Used for get the list of inactive ic details
        /// </summary>
        public IEnumerable<ICResponse> inactiveICdetails { get; set; }
        public IEnumerable<GuideInstructionResponse> GetGuideInstruction { get; set; }

        #endregion
        #region vendor Performance
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<VendorsList> vendordetails { get; set; }
        public IEnumerable<VendorsList> DDvendordetails { get; set; }
        public IEnumerable<VendorsList> CurrentvendorsList { get; set; }
        public IEnumerable<VendorsList> DDClientList { get; set; }
        public IEnumerable<VendorsList> PastvendorsList { get; set; }
        public IEnumerable<VendorsList> SerachInVendors { get; set; }
        public IEnumerable<VendorPerformance> vendorPerformance { get; set; }
        #endregion

        #region Communication
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Content { get; set; }
        /// <summary>
        /// get & set uploaded content path
        /// </summary>
        public string uploadedcontentpath { get; set; }

        /// <summary>
        /// get & set current running host url
        /// </summary>
        public string hostURL { get; set; }
        #region Commbasicdetail
        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Introduction { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string DocTitle { get; set; }
        /// <summary>
        /// get & set Reference
        /// </summary>
        
        public string Reference { get; set; }
        /// <summary>
        /// get & set Group
        /// </summary>

        public string Group { get; set; }
         
        /// <summary>
        /// get & set Attempts
        /// </summary>
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// get & set Effectivedate
        /// </summary>
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime? Effectivedate { get; set; }
        /// <summary>
        /// get & set Retake
        /// </summary>
        public string Retake { get; set; }
        /// <summary>
        /// get & set Retake
        /// </summary>
        public string HourOfCredit { get; set; }
        /// <summary>
        /// get & set Retake
        /// </summary>
        public string Policyno { get; set; }
        /// <summary>
        /// get & set DaysOfCompletion
        /// </summary>
        public int DaysOfCompletion { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhirestaff { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhireic { get; set; }
        /// <summary>
        /// get & set Reqnewhire
        /// </summary>
        public bool Reqnewhirevendor { get; set; }
        /// <summary>
        /// get & set DocTitles
        /// </summary>
        public string DocTitles { get; set; }
        /// <summary>
        /// get & set VideoTitles
        /// </summary>
        public string VideoTitles { get; set; }
        /// <summary>
        /// get & set ScormTitles
        /// </summary>
        public string ScormTitles { get; set; }
        #endregion
        /// <summary>
        /// get & set linksDetails
        /// </summary>
        public IList<CommDetailsResponse> linksDetails { get; set; }
        /// <summary>
        /// get & set contentsDetails
        /// </summary>
        public IList<CommDetailsResponse> contentsDetails { get; set; }
        /// <summary>
        /// get & set reqactionDetails
        /// </summary>
        public IList<CommDetailsResponse> reqactionDetails { get; set; }
        /// <summary>
        /// get & set reqactionDetails
        /// </summary>
        public IList<CommDetailsResponse> assesmentDetails { get; set; }
        /// <summary>
        /// get & set reqactionDetails
        /// </summary>
        public IList<CommDetailsResponse> ansDetails { get; set; }
        /// <summary>
        /// get & set Vendor apply or not
        /// </summary>
        public bool IsVendorApply { get { return defaultValueForIsVendor; } set { defaultValueForIsVendor = value; } }
        /// <summary>
        /// get & set client apply or not
        /// </summary>
        public bool IsClientApply { get { return defaultValueForIsClient; } set { defaultValueForIsClient= value; } }
        /// <summary>
        /// get & set IsServicesApply
        /// </summary>
        public bool IsServicesApply { get { return defaultvalueForIsServices; } set { defaultvalueForIsServices = value; } }
        /// <summary>
        /// get & set Coverage area apply or not
        /// </summary>
        public bool IsCoverageareaApply { get { return defaultValueForIsCoveragearea; } set { defaultValueForIsCoveragearea = value; } }
        /// <summary>
        /// get & set IsScormApply apply or not
        /// </summary>
        public bool IsScormApply { get { return defaultValueForIsScorm; } set { defaultValueForIsScorm = value; } }
        /// <summary>
        /// get & set IC apply or not
        /// </summary>
        public bool IsICApply { get { return defaultValueForIsIc; } set { defaultValueForIsIc = value; } }
        #region Commtestrule
        /// <summary>
        /// get & set RandQues
        /// </summary>
        public bool RandQues { get; set; }
        /// <summary>
        /// get & set RandAns
        /// </summary>
        public bool RandAns { get; set; }

        /// <summary>
        /// get & set ReqReTest
        /// </summary>
        public bool ReqReTest { get; set; }
        /// <summary>
        /// get & set ShowWrongeAns
        /// </summary>
        public bool ShowWrongeAns { get; set; }
        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string PassingScore { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Instruction { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string Attempts { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public int Testruleid { get; set; }
        #endregion

        #endregion

        #region Annoucement
        /// <summary>
        /// property for announcement 
        /// </summary>
        public IList<AnnouncementResponse> Announcement { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string CloseAnnounce { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public int CloseAnnounceId { get; set; }
        #endregion
        #region Invite
        /// <summary>
        /// Used for get the list of inviteDetails
        /// </summary>
        public IEnumerable<InviteResponse> currenrinviteDetails { get; set; }
        /// <summary>
        /// Used for get the list of inviteDetails
        /// </summary>
        public IEnumerable<InviteResponse> removedinviteDetails { get; set; }
        /// <summary>
        /// Used for get the list of searchInviteDetails
        /// </summary>
        public IEnumerable<InviteResponse> searchInviteDetails { get; set; }
        #endregion

        #region Contract
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
        /// Used for emailid of user/company
        /// </summary>
        public IEnumerable<string> Services { get; set; }
       
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IList<string> ServiceDetails { get; set; }
       
       
        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ServiceList { get; set; }
                
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
      
        public IEnumerable<StateRespone> StateList { get; set; }
       
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

         public bool SaveClose { get; set; }

         public int StateId { get; set; }

         public string ContactPerson { get; set; }

         public string BusinessName { get; set; }
        #endregion
        #region Background
        
        #endregion
    }
        
 
}