

// ******************************************************************************************************************************
//                                                 class:NonMemberResponseParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Dec. 17, 2014     |Sumit Saurav           | This class holds the interaction logic for NonMemberResponseParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class NonMemberResponseParam
    {
        #region << Public Variables >>
        /// <summary>
        /// Used for set fName
        /// </summary>
        public string fName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string lName { get; set; }      
        /// <summary>
        /// Used for set emailId
        /// </summary>
        public string emailId { get; set; }
       
        public int? UserId { get; set; }
        /// <summary>
        /// Used for set companyId
        /// </summary>
        public string companyId { get; set; }          
       
        public int flowId { get; set; }      

        public int PaymentType { get; set; }       

        public bool IsMailSent { get; set; }

        public string CompanyName { get; set; }

        public int DocFlowId { get; set; }

        public int CreatedBy { get; set; }

        public string UserCompanyId { get; set; }

        public string CreatorCompanyId { get; set; }

        public string Password { get; set; }

        public string CreatorCompanyName { get; set; }


        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public NonMemberResponseParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
