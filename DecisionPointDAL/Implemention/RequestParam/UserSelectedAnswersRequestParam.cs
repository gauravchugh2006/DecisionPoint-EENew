﻿

// ******************************************************************************************************************************
//                                                 class:UserSelectedAnswersRequestParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 29, 2014     |sumit saurav           | This class holds the interaction logic for UserSelectedAnswersRequestParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class UserSelectedAnswersRequestParam
    {
        
        #region << Public Variables >>
            public int userId { get; set; }
            public int docId { get; set; }
            public int questionId { get; set; }
            public int AnswerId { get; set; }
            public int attempt { get; set; }
            public int tableid { get; set; }
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public UserSelectedAnswersRequestParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
