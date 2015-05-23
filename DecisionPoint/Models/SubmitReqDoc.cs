/*//////////////////////////////////////////////////////////////////////////////////////
This class Used for submit the Employee Reqiured document || Created By : Bobi  || Created Date : 30 May 2014
//////////////////////////////////////////////////////////////////////////////////////*/

using System;
namespace DecisionPoint.Models
{
    public class SubmitReqDoc
    {
        #region Sender
       
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
        /// get and set UserPer
        /// </summary>
        public bool UserPer { get; set; }
        /// <summary>
        /// get and set DoNotShow
        /// </summary>
        public bool DoNotShow { get; set; }
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
        /// get and set Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// get and set ServicesId
        /// </summary>
        public string ServicesId { get; set; }
        /// <summary>
        /// get and set VendorTypeId
        /// </summary>
        public string VendorTypeId { get; set; }
        /// <summary>
        /// get and set ReqDocId
        /// </summary>
        public int ReqDocId { get; set; }
        /// <summary>
        /// get and set Retake
        /// </summary>
        public string Retake { get; set; }
        #endregion
    }
}