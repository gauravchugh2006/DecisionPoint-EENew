

// ******************************************************************************************************************************
//                                                 class:LicenseInsuranceRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Aug. 21 2014     |Arun Kumar            | This class holds the interaction logic for LicenseInsuranceRequestParam.cs
// ******************************************************************************************************************************

using System;
using System.Collections.Generic;
namespace DecisionPointDAL.Implemention.RequestParam
{
    public class LicenseInsuranceRequestParam
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>
        /// <summary>
        /// get and set IsCompanyReq
        /// </summary>
        public bool IsCompanyReq { get; set; }
        /// <summary>
        /// get and set IsPolicyReq
        /// </summary>
        public bool IsPolicyReq { get; set; }
        /// <summary>
        /// get and set IsLicenseReq
        /// </summary>
        public bool IsLicenseReq { get; set; }
        /// <summary>
        /// get and set IsExpDateReq
        /// </summary>
        public bool IsExpDateReq { get; set; }
        /// <summary>
        /// get and set DoNotShow
        /// </summary>
        public bool DoNotShow { get; set; }
        /// <summary>
        /// get and set IsStateReq
        /// </summary>
        public bool IsStateReq { get; set; }
        /// <summary>
        /// get and set ReqDocId
        /// </summary>
        public int ReqDocId { get; set; }
        /// <summary>
        /// get and set ModifiedById
        /// </summary>
        public int ModifiedById { get; set; }
        /// <summary>
        /// get and set UserPer
        /// </summary>
        public bool UserPer { get; set; }
        /// <summary>
        /// get and set Ack
        /// </summary>
        public string Ack { get; set; }
        /// <summary>
        /// get and set UploadedDoc
        /// </summary>
        public string UploadedDoc { get; set; }

        /// <summary>
        /// get and set ReqDocFor
        /// </summary>
        public string ReqDocFor { get; set; }
        /// <summary>
        /// get and set ReqDocType
        /// </summary>
        public byte ReqDocType { get; set; }
        /// <summary>
        /// get and set ServicesId
        /// </summary>
        public string ServicesId { get; set; }
        /// <summary>
        /// get and set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get and set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get and set UploadedDocDate
        /// </summary>
        public DateTime UploadedDocDate { get; set; }
        /// <summary>
        /// get & set LicInsMasterId
        /// </summary>
        public int LicInsMasterId { get; set; }
        /// <summary>
        /// get and set Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// get and set ServiceId
        /// </summary>
        public int? ServiceId { get; set; }
        /// <summary>
        /// get and set VendorTypeId
        /// </summary>
        public string VendorTypeId { get; set; }
        /// <summary>
        /// get and set Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// get & set IsActive
        /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// get & set JCRType
        /// </summary>
        public int JCRType { get; set; }

        /// <summary>
        /// get and set Acknowleagement
        /// </summary>
        public List<string> Acknowleagement { get; set; }
        /// <summary>
        /// get and set Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// get and set ICTypeIds
        /// </summary>
        public List<string> ICTypeIds { get; set; }
        /// <summary>
        /// get and set ClientTypeIds
        /// </summary>
        public List<string> ClientIds { get; set; }
        /// <summary>
        /// get & set OperationType : 0 for save & 1 for update
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// get & set LicenseType
        /// </summary>
        public string LicenseType { get; set; }
        /// <summary>
        /// get & set Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// get & set InsuranceType
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// get & set Review
        /// </summary>
        public int Review { get; set; }
        /// <summary>
        /// get & set UploadedBy
        /// </summary>
        public string UploadedBy { get; set; }
        public int LicInsMapId { get; set; }
        public int LicInsId { get; set; }
        public string VendorType { get; set; }
        public string ReqCompanyName { get; set; }
        public int RequiredByUserId { get; set; }
        public string RequiredByCompanyId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int StateId { get; set; }
        public string StateAbbre { get; set; }
        public string StateName { get; set; }
        
        public string Status { get; set; }

        public string LicenseNumber { get; set; }
        public string SterlingOrderStatus { get; set; }
        public string SterlingOrderId { get; set; }

        public string PolicyNumber { get; set; }
        public string CompanyName { get; set; }
        public double SingleOccurance { get; set; }

        public double Aggregate { get; set; }

        public DateTime? CompletedDate { get; set; }
        /// <summary>
        /// get & set InsuranceVerType
        /// </summary>
        public string InsuranceVerType { get; set; }
        /// <summary>
        /// get & set ICTypeId
        /// </summary>
        public int ICTypeId { get; set; }
        /// <summary>
        /// get & set IsStaffTitle
        /// </summary>
        public bool IsStaffTitle { get; set; }
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public LicenseInsuranceRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
