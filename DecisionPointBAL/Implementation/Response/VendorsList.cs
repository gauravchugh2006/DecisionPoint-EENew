

// ******************************************************************************************************************************
//                                                 class:VendorsList.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Ankur file file seprated by sumit           | This class holds the interaction logic for VendorsList.cs
// ******************************************************************************************************************************

using System;
namespace DecisionPointBAL.Implementation.Response
{
    public class VendorsList
    {
        #region << Public Variables >>
        /// <summary>
        /// get & set id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string contact { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        /// <summary>
        /// get & set email id
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string vendor { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// get & set stateAbbre
        /// </summary>
        public string stateAbbre { get; set; }
        /// <summary>
        /// get & set zipCode
        /// </summary>
        public string zipCode { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        public string DocFlow { get; set; }
        public int DocFTblId { get; set; }

        /// <summary>
        /// get & set businessName
        /// </summary>
        public bool invitationStatus { get; set; }
        /// <summary>
        /// get & set VendorType
        /// </summary>
        public string VendorType { get; set; }
        public int VendorTypeId { get; set; }
        /// <summary>
        /// get & set LastInviteMailDate
        /// </summary>
        public DateTime? LastInviteMailDate { get; set; }
        /// <summary>
        /// get & set User Type
        /// </summary>
        public string UserType { get; set; }

        public bool? IsNonMember { get; set; }
        /// <summary>
        /// get & set Status
        /// </summary>
        public string Status { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public VendorsList()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
