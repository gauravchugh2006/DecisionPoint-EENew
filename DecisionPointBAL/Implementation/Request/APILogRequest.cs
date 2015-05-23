

// ******************************************************************************************************************************
//                                                 class:APILogRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Arun Kumar            | This class holds the interaction logic for APILogRequest.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Request
{
    public class APILogRequest
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>
        public string RefrenceId { get; set; }

        public string APIUserName { get; set; }

        public string APIPassword { get; set; }

        public string RequestData { get; set; }

        public string ResponseData { get; set; }
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public APILogRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
