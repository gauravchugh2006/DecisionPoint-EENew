
namespace DecisionPointBAL.Implementation.Request
{
    public class CompanyDashBoardRequest
    {
        #region Invitaion
        #region Manual Invitation
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string fName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string lName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int titleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int roleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int permissionId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Used for get the IsBackgroundCheck
        /// </summary>
        public bool IsBackgroundCheck { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string  companyId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int flowId { get; set; }
       
        public string CompId { get; set; }
           
        public int docflowId { get; set; }
          
        public int vendorTypeId { get; set; }
       
        public int PaymentType { get; set; }

        public bool IsMailSent { get; set; }

        public bool AllowToView { get; set; }
       
        #endregion
        #endregion
    }
}
