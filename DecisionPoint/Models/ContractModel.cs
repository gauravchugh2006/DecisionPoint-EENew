using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL.Common;

namespace DecisionPoint.Models
{
    public class ContractModel
    {
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


        public int FlowId { get; set; }
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

        /// <summary>
        /// get & set uploaded content path
        /// </summary>
        public string uploadedcontentpath { get; set; }

        /// <summary>
        /// get & set current running host url
        /// </summary>
        public string hostURL { get; set; }

        /// <summary>
        /// get & set DocTitles
        /// </summary>
        public string DocTitles { get; set; }
        /// <summary>
        /// get & set contentsDetails
        /// </summary>
        public IList<CommDetailsResponse> contentsDetails { get; set; }

        public string ServiceName { get; set; }
        public int ServiceId { get; set; }
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> titleDetails { get; set; }
        #endregion

        #region Fileds for save
        public string ManagerName { get; set; }

        public int OwnerId { get; set; }
        
        public string OwnerName { get; set; }

        public int ContractWithId { get; set; }

        public string ContractWithName { get; set; }

        public int ExecutedById { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? ContractDate { get; set; }

        public int ExpirationDateReminder { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string GeneralComments { get; set; }

        public string Paragraph { get; set; }

        public string Section { get; set; }

        public string SubSection { get; set; }

        public string CreatorCompanyId { get; set; }

        public string ContractorCompanyId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsCompleted { get; set; }

        public int CreatedBy { get; set; }
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Notes { get; set; }

        public DateTime? EventDate { get; set; }

        public int EventDateReminder { get; set; }

        public List<string> FilePathStr { get; set; }

        public IList<int> SelectedServiceList { get; set; }

        public string Role { get; set; }
        public int TblId { get; set; }

        public string Title { get; set; }
        #endregion

        #region Contract List

        public IList<CreateContractResponse> ContractList { get; set; }

        public IList<CreateContractResponse> PastContractList { get; set; }

        public int PageSize { get; set; }

        public int RowperPage { get; set; }

        /// <summary>
        /// Used for get the ContractSearchResult
        /// </summary>
        public IList<CreateContractResponse> ContractSearchResult { get; set; }

        public string ExecutedByName { get; set; }

        public List<ContractEvent> EventList { get; set; }

        public string EventTitle { get; set; }
        #endregion
    }
}