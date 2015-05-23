

// ******************************************************************************************************************************
//                                                 class:CommContentRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |Sumi Saurav          | This class holds the interaction logic for CommContentRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class CommContentRequestParam
    {
        #region << Public Variables >>

        public int docId { get; set; }
        public string files { get; set; }
        public string filetype { get; set; }
        public int userId { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string scormname { get; set; }

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public CommContentRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
