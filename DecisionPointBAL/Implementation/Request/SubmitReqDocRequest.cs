
using System;
namespace DecisionPointBAL.Implementation.Request
{
  public class SubmitReqDocRequest
    {
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
        /// get and set IsStateReq
        /// </summary>
        public bool IsStateReq { get; set; }
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
        public byte ReqDocType { get; set; }
        /// <summary>
        /// get and set ServicesId
        /// </summary>
        public string ServicesId { get; set; }
        /// <summary>
        /// get and set VendorTypeId
        /// </summary>
        public string VendorTypeId { get; set; }
        /// <summary>
        /// get and set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get and set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get and set Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// get and set ReqDocId
        /// </summary>
        public int ReqDocId { get; set; }
        /// <summary>
        /// get and set Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// get and set ServiceId
        /// </summary>
        public int? ServiceId { get; set; }
        /// <summary>
        /// get and set DoNotShow
        /// </summary>
        public bool DoNotShow { get; set; }
      /// <summary>
      /// get & set IsActive
      /// </summary>
        public int IsActive { get; set; }
        /// <summary>
        /// get & set JCRType
        /// </summary>
        public int JCRType { get; set; }
        /// <summary>
        /// get & set Retake
        /// </summary>
        public string Retake { get; set; }
    }
  public class UploadDocDetailsRequest
  {
      /// <summary>
      /// Used to get & set DocSeq
      /// </summary>
      public int DocSeq { get; set; }
      /// <summary>
      /// Used to get & set DocLoc
      /// </summary>
      public string DocLoc { get; set; }
      /// <summary>
      /// Used to get & set DocTblId
      /// </summary>
      public int DocTblId { get; set; }
      /// <summary>
      /// Used to get & set UploadedDocDate
      /// </summary>
      public DateTime? UploadedDocDate { get; set; }
  }
}
