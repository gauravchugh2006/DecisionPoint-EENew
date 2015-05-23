using System;


// ******************************************************************************************************************************
//                                                 class:UserAssesmentAnswerResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Mon. dd, 2014     |Mamta Gupta   file seprated by sumit          | This class holds the interaction logic for UserAssesmentAnswerResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class UserAssesmentAnswerResponse
    {
        #region << Public Variables >>
        public Int32? QuestionId { get; set; }
        public string Answer { get; set; }
        public bool? IsCorrect { get; set; }
        public Int32 Id { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public UserAssesmentAnswerResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
