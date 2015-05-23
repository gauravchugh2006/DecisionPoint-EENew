

// ******************************************************************************************************************************
//                                                 class:DSTInviteRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// 02 19, 2015     |Sumit Saurav           | This class holds the interaction logic for DSTInviteRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class DSTInviteRequestParam
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string RoleType { get; set; }

        public string ClientId { get; set; }
        public string EntityId { get; set; }

        public int PackageId { get; set; }

        public string SubClientId { get; set; }
        

        public int ProductId { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
        public string StaffId { get; set; }

        public string BusinessName { get; set; }

        public int RoleTypeId { get; set; }

        public bool IsMailSent { get; set; }

        public int TitleId { get; set; }

        public int DocFlowId { get; set; }

        public int PaymentTypeId { get; set; }

        public int ICTypeId { get; set; }
        public string UserCompanyId { get; set; }

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public DSTInviteRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
