

// ******************************************************************************************************************************
//                                                 class:LicenseInsuranceResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Aug. 26, 2014     |Sumit Saurav           | This class holds the interaction logic for LicenseInsuranceResponse.cs
// ******************************************************************************************************************************

using System;
namespace DecisionPointBAL.Implementation.Response
{
    public class LicenseInsuranceResponse
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
        /// get and set title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// get and set ReqDocFor
        /// </summary>
        public string ReqDocFor { get; set; }
        /// <summary>
        /// get and set ReqDocType
        /// </summary>
        public byte? ReqDocType { get; set; }
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
        public bool IsDocUpload { get; set; }
        public string SterlingPkgName { get; set; }
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
        public int? VendorTypeId { get; set; }
        /// <summary>
        /// get and set Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// get & set IsActive
        /// </summary>
        public bool IsActive { get; set; }
        public string InsuranceVerType { get; set; }
        /// <summary>
        /// get & set JCRType
        /// </summary>
        public int JCRType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsStaffTitle { get; set; }

        public string VendorType { get; set; }
        public string ReqCompanyName { get; set; }
        public int RequiredByUserId { get; set; }
        public string RequiredByCompanyId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int LicInsMapId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Status { get; set; }

        public int LicInsId { get; set; }
        public string LicenseType { get; set; }
        public string LicenseNumber { get; set; }

        public string InsuranceType { get; set; }
        public string PolicyNumber { get; set; }
        public string CompanyName { get; set; }
        public double SingleOccurance { get; set; }

        public double Aggregate { get; set; }

        public int? Review { get; set; }
        public string Source { get; set; }
        public DateTime? CompletedDate { get; set; }
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public LicenseInsuranceResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
