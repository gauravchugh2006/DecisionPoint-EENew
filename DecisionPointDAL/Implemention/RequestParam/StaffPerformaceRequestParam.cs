
namespace DecisionPointDAL.Implemention.RequestParam
{
    public class StaffPerformaceRequestParam
    {

        public int StaffId { get; set; }

        public string StaffName { get; set; }

        public string Title { get; set; }

        public int TotalDocument { get; set; }

        public int CompletedDocument { get; set; }

        public int InCompletedDocument { get; set; }

        public int Performance { get; set; }

        public int WithdrawnCompletedDocument { get; set; }

        public int WithdrawnInCompletedDocument { get; set; }

    }
}
