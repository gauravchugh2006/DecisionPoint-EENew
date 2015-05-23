
namespace DecisionPointBAL.Implementation.Request
{
    public class CommTestRulesRequest
    {
        /// <summary>
        /// get & set RandQues
        /// </summary>
        public bool RandQues { get; set; }
        /// <summary>
        /// get & set RandAns
        /// </summary>
        public bool RandAns { get; set; }

        /// <summary>
        /// get & set ReqReTest
        /// </summary>
        public bool ReqReTest { get; set; }
        /// <summary>
        /// get & set ShowWrongeAns
        /// </summary>
        public bool ShowWrongeAns { get; set; }
        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string PassingScore { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        public string Instruction { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string Attempts { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public int docId { get; set; }
        /// <summary>
        /// get & set Testruleid
        /// </summary>
        public int Testruleid { get; set; }
    }
}
