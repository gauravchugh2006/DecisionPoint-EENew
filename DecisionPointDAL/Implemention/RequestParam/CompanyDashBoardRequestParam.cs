
namespace DecisionPointDAL.Implemention.RequestParam
{
    //*****************************************************************************************************************************************
    //   Class used for set properties used in user company section
    //*****************************************************************************************************************************************
    public class CompanyDashBoardRequestParam
    {
        #region Invitaion
        #region Manual Invitation
        /// <summary>
        /// Used for set fName
        /// </summary>
        public string fName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string lName { get; set; }
        /// <summary>
        /// Used for set the titleId
        /// </summary>
        public int titleId { get; set; }
        /// <summary>
        /// Used for set roleId
        /// </summary>
        public int roleId { get; set; }
        /// <summary>
        /// Used for set permissionId
        /// </summary>
        public int permissionId { get; set; }
        /// <summary>
        /// Used for set emailId
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// Used for set password
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Used for set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Used for set companyId
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// Used for set companyName
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// Used for get the IsBackgroundCheck
        /// </summary>
        public bool IsBackgroundCheck { get; set; }
        /// <summary>
        /// Used for set flowId
        /// </summary>
        public int flowId { get; set; }

        public int docflowId { get; set; }

        public int vendortypeId { get; set; }

        public int PaymentType { get; set; }

        public string CompId { get; set; }

        public bool IsMailSent { get; set; }
        public bool AllowToView { get; set; }

        #endregion
        #endregion
    }

}
