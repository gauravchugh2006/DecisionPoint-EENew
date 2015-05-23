using System.Collections.Generic;

// ******************************************************************************************************************************
//                                                 class:APIMasterResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// feb. 25, 2015     |Arun Kumar            | This class holds the interaction logic for APIMasterResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class APIMasterResponse
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        public int Id { get; set; }

        public string Name { get; set; }
        public string ResultId { get; set; }

        public IEnumerable<APIMasterResponseJCRInfo> APIMasterResponseJCRInfoList { get; set; }

        public int ResultCode { get; set; }

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public APIMasterResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }

    public class APIMasterResponseJCRInfo
    {
        public string JCR { get; set; }
        public int JCRStatus { get; set; }
    }
}
