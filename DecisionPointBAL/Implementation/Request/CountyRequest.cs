

// ******************************************************************************************************************************
//                                                 class:CountyRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 28, 2014     |Sumit Saurav          | This class holds the interaction logic for CountyRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class CountyRequest
    {
        #region << Public Variables >>
        public int? StateId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string CountyAbbre { get; set; }
        public string StateAbbre { get; set; }
        /// <summary>
        /// get & set CoverageAreaFor
        /// </summary>
        public int CoverageAreaFor { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ModifiedBy
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public CountyRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
