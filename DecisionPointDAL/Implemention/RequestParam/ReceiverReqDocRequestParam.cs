using System;


// ******************************************************************************************************************************
//                                                 class:ReceiverReqDocRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// June. 06, 2014     |Sumit Saurav           | This class holds the interaction logic for ReceiverReqDocRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class ReceiverReqDocRequestParam
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

        /// <summary>
        /// get and set Ack
        /// </summary>
        public string Ack { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// get and set Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// get and set ModifiedById
        /// </summary>
        public int ModifiedById { get; set; }

        public int VisitorId { get; set; }

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ReceiverReqDocRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
