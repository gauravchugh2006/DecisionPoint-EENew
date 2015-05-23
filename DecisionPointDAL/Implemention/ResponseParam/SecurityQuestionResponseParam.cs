
namespace DecisionPointDAL.Implemention.ResponseParam
{
    /// <summary>
    /// get Security Question list to bind on registration page
    /// </summary>
    public class SecurityQuestionResponseParam
    {
        /// <summary>
        /// Id of security question
        /// </summary>
        public int SecurityId { get; set; }

        /// <summary>
        /// Security Question
        /// </summary>
        public string SecurityQuestion { get; set; }
    }
}
