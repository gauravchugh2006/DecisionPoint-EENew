using System;

namespace DecisionPointDAL.Implemention.ResponseParam
{
    //*****************************************************************************************************************************************
    //   Class used for set the document details
    //*****************************************************************************************************************************************
    public class ViewDocumentResponseParam
    {
        #region ReplyMessage
        /// <summary>
        /// Used for get reply body of message
        /// </summary>
        public int? docId { get; set; }
        /// <summary>
        /// Used for get message recipient person name
        /// </summary>
        public DateTime? duedate { get; set; }

        #endregion
    }
}
