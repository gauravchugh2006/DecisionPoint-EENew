
namespace DecisionPoint.Models
{
    /// <summary>
    /// UpdateVendorModel
    /// </summary>
    /// <createdby>Sumit saurav</createdby>
    /// <createddate>29/05/2014</createddate>
    public class UpdateVendorModel
    {
        public int? No { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string CName { get; set; }

        public string Email { get; set; }

        public int? FlowID { get; set; }

        public string FlowText { get; set; }

        public int DocFlowID { get; set; }

        public string DocFlowText { get; set; }
     
        public int PaymentType { get; set; }
        public int VTID { get; set; }
        public string VType { get; set; }
        public string BGCheck { get; set; }

        public string PaymentMode { get; set; }
        public string CsvFileName { get; set; }
    }
}