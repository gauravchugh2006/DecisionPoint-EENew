

// ******************************************************************************************************************************
//                                                 class:CountyRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 27, 2014     |Sumit saurav          | This class holds the interaction logic for CountyRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class CountyRequestParam
    {
        #region << Public Variables >>
        public int? StateId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }
         public string CompanyId { get; set; }
        public string CountyName { get; set; }
        public string CountyAbbre { get; set; }
        public string StateAbbre { get; set; }
        /// <summary>
        /// get & set CoverageAreaFor
        /// </summary>
        public int CoverageAreaFor { get; set; }
         /// <summary>
        /// get & set modifiedby
        /// </summary>
        public int ModifiedBy { get; set; }
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
        public CountyRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
