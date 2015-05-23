
// ********************************************************************************************************************************
//                                                  class:LoginDetailsResponseparam
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 17, 2013    |Arun Kumar     | Define Response parameter for login page
// *********************************************************************************************************************************

namespace DecisionPointDAL.Implemention.ResponseParam
{
    #region Public Variables
    /// <summary>
    /// Class to define public properties for login response param.
    /// </summary>
    public  class LoginDetailsResponseParam
    {
        /// <summary>
        /// EmailId of User 
        /// </summary>
        public string Emailid { get; set; }
        /// <summary>
        /// Type of user (Individual /Business with no employee/Business with employee)
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// First name of user
        /// </summary>
        public string Firstname { get; set; }
        /// <summary>
        /// Middle name of user
        /// </summary>
        public string MiddelName { get; set; }
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// user Id
        /// </summary>
        public int UserId { get; set; }

        public bool? IsTemp { get; set; }
        public bool IsPayment { get; set; }
        public string BusinessName { get; set; }
        public string CompanyId { get; set; }
        public string permission { get; set; }
        public bool? IsRegistered { get; set; }
        public bool IsActive { get; set;}
    }
    #endregion
}
