

// ******************************************************************************************************************************
//                                                 class:APIMasterResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Feb. 25, 2015     |Arun Kumar            | This class holds the interaction logic for APIMasterResponse.cs
// ******************************************************************************************************************************

using System.Collections.Generic;
namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class APIMasterResponseParam
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        public string ResultId { get; set; }

        public int ResultCode { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<APIJCRInfo> PackageInfoDetails { get; set; }
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public APIMasterResponseParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
    public class APIJCRInfo
    {
        public string JCR { get; set; }
        public int JCRStatus { get; set; }
    }
}
