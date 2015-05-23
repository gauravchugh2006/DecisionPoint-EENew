
namespace DecisionPointBAL.Implementation.Response
{
    
    public class StaffPerformaceResponse
    {

        public int StaffId { get; set; }

        public string StaffName { get; set; }
        public string Title { get; set; }
        public int TotalDocument { get; set; }

        public int CompletedDocument { get; set; }

        public int InCompletedDocument { get; set; }
        public int Performance { get; set; }

    }
}
