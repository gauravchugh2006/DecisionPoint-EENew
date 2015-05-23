

// ******************************************************************************************************************************
//                                                 class:ZipRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Arun Kumar            | This class holds the interaction logic for ZipRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class ZipRequestParam
    {
        #region << Public Variables >>
        public int? StateId { get; set; }
        public string CityName { get; set; }
        public string CompanyId { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public string CountyAbbre { get; set; }
        public string StateAbbre { get; set; }
        public string ZipCode { get; set; }
        /// <summary>
        /// ModifiedBy
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// get & set CoverageAreaFor
        /// </summary>
        public int CoverageAreaFor { get; set; }

        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public ZipRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion  
    }
}
