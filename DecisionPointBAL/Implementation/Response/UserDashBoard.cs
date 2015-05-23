using System;

namespace DecisionPointDAL.Implemention.ResponseParam
{
   public class UserDashBoard
    {
        /// <summary>
        /// Messages Sent Date
        /// </summary>
        public DateTime? MsgDate { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string MsgTitle { get; set; }
        /// <summary>
        /// Messages Send By
        /// </summary>
        public string MsgFrom { get; set; }
       /// <summary>
       /// Used for display message id
       /// </summary>
        public int MsgId { get; set; }
       
    }
}
