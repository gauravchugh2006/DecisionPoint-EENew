

// ******************************************************************************************************************************
//                                                 class:GetCompaniesResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Mamta Gupta file separted by Sumit            | This class holds the interaction logic for GetCompaniesResponse.cs
// ******************************************************************************************************************************

using System;
namespace DecisionPointBAL.Implementation.Response
{
    public class GetCompaniesResponse
    {
        #region << Public Variables >>
        public string BusniessID { get; set; }
        public string BusniessName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
        public string VendorType { get; set; }
        public string Phone { get; set; }
        public int Tableid { get; set; }
        public int PaymentType { get; set; }
        public bool InvitationStatus { get; set; }
        public DateTime? InvitationDate { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public GetCompaniesResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
