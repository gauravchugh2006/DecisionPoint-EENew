

// ******************************************************************************************************************************
//                                                 class:StateRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Dec. 05, 2013     |Arun Kumar |Sumit Saurav            | This class holds the interaction logic for StateRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class StateRequestParam
    {
        #region << Public Variables >>
       /// <summary>
       /// state id
       /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// state name
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// Abbrebiation
        /// </summary>
        public string Abbrebiation { get; set; }
        /// <summary>
        /// get & set modifiedby
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// user id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set CoverageAreaFor
        /// </summary>
        public int CoverageAreaFor { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public StateRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
   
}
