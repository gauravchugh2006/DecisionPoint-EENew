using System;



// ******************************************************************************************************************************
//                                                 class:ReceiverReqDocResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// June. 05, 2014     |Sumit Saurav          | This class holds the interaction logic for ReceiverReqDocResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class ReceiverReqDocResponse
    {
        #region << Public Variables >>
        #region Receiver

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
        /// get and set ReqDocId
        /// </summary>
        public int ReqDocId { get; set; }
        #endregion

        #region SavedByReceiver

        public string CompanyName { get; set; }

        public string LisenceNumber { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string Stateabbre { get; set; }

        public byte IsCompleted { get; set; }

        public bool IsMailSent { get; set; }

        #endregion

        #region Acknoledgement and Upload

        public string Acknoledgment { get; set; }

        public string DocLoc { get; set; }

        public int DocSeqNo { get; set; }
        public int DocReceiverUserId { get; set; }
        public int DocSenderUserId { get; set; }
        public int DocUploadTblId { get; set; }
        public bool DNA { get; set; }
        #endregion

        #region Audit

        public string VisitorName { get; set; }

        public bool IsDocUploaded { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string StateName { get; set; }

        public int ReqDocReceiverId { get; set; }
        #endregion
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ReceiverReqDocResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
