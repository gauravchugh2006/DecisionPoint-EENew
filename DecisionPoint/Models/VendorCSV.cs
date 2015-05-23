
namespace DecisionPoint.Models
{
    public class VendorCSV
    {

        public int No { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string EmailId { get; set; }


        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int FlowId { get; set; }
        public string FlowValue { get; set; }
        public int DocFlowId { get; set; }
        public string DocFlowValue { get; set; }
        public string CheckType { get; set; }
        public string PaymentType { get; set; }
        public int PaymentID { get; set; }
        public string VType { get; set; }
        public int VTID { get; set; }
        public string BGCheck { get; set; }
    }
}