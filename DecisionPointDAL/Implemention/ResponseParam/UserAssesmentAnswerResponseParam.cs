using System;


// ******************************************************************************************************************************
//                                                 class:UserAssesmentAnswerResponseParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 28, 2014     |Class Created By Mamta Gupta new file added by Sumit           | This class holds the interaction logic for UserAssesmentAnswerResponseParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class UserAssesmentAnswerParamcs
    {
        public Int32 Id { get; set; }
        public string Answer { get; set; }
        public Int32 QuestionId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
