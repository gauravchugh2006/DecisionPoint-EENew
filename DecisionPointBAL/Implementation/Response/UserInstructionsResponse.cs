

// ******************************************************************************************************************************
//                                                 class:UserInstructionsResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 28, 2014     |Mamta Gupta file seprated by Sumit Saurav         | This class holds the interaction logic for UserInstructionsResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class UserInstructionsResponse
    {
        #region << Public Variables >>
        public int Id { get; set; }
        public bool RandQues { get; set; }
        public bool RandAns { get; set; }
        public bool ReqReTest { get; set; }
        public string PassingScore { get; set; }
        public string Attempts { get; set; }
        public bool ShowWrongeAns { get; set; }
        public int DocId { get; set; }
        public string Instruction { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public UserInstructionsResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
