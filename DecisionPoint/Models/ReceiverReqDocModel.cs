using System;


namespace DecisionPoint.Models
{
    /// <summary>
    /// ReceiverReqDocModel
    /// </summary>
    /// <createdby>sumit saurav</createdby>
    /// <createddate>june 6 2014</createddate>
    public class ReceiverReqDocModel
    {
        #region << Public Variables >>

        public string CompanyName { get; set; }

        public string LisenceNumber { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string StateAbbre { get; set; }

        public byte IsCompleted { get; set; }

        public string DocLoc { get; set; }

        public int DocSeqNo { get; set; }

        public int UserId { get; set; }

        public string CompanyId { get; set; }

        public int ReqDocId { get; set; }

        public string DocSequence { get; set; }

        public int Type { get; set; }
        /// <summary>
        /// get and set Ack
        /// </summary>
        public string Ack { get; set; }
        /// <summary>
        /// get and set Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// get and set LiceAndInsType
        /// </summary>
        public string LiceAndInsType { get; set; }
        #endregion

    }
}