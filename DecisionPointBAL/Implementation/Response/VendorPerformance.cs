
namespace DecisionPointBAL.Implementation.Response
{
    public class VendorPerformance
    {
        /// <summary>
        /// Get & set venodrID
        /// </summary>
        public int? VendorID { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Used for get number of vendor details
        /// </summary>
        public int Received { get; set; }
        /// <summary>
        /// get & set % of document completed by distant descendents
        /// </summary>
        public float Forwarded { get; set; }
        /// <summary>
        /// get & set number of staff detail
        /// </summary>
        public int CompanyCompletion { get; set; }
        /// <summary>
        /// get & set % of Document Complete By vendor's Staff
        /// </summary>
        public float DocumentCompleteByVendors { get; set; }

    }
}
