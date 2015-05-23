

// ******************************************************************************************************************************
//                                                 class:WebinarUsersResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Sep. dd, 2014     |Sumit Saurav        | This class holds the interaction logic for WebinarUsersResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class WebinarUsersResponse
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>
        public int UserId { get; set; }

        public string FirstName { get; set; }       

        public string LastName { get; set; }

        public string Email { get; set; }

        public string CompanyId { get; set; }

        public string Role { get; set; }

        public string CompanyName { get; set; }

        public string OrganiserId { get; set; }

        public string AppKey { get; set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ContactName { get; set; }

        public int CreatedBy { get; set; }

        public bool IsActive { get; set; } 
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public WebinarUsersResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
