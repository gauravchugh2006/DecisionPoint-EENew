
namespace DecisionPointDAL.Implemention.ResponseParam
{
   public  class UserCorrectAsstAnswerParam
    {
        public int QuestionId { get; set; }
        public int OptionAnserId { get; set; }
        public string Answer { get; set; }
        public int GivenAnsId { get; set; }
        public bool Iscorrect { get; set; }
    }
}
