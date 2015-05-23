using System;

namespace DecisionPointBAL.Implementation.Request
{
    public class publishCommRequest
    {
        public string totalStaff { get; set; }
        public string totalIC { get; set; }
        public string totalVendor { get; set; }
        public string totalClient { get; set; }
        public int docId { get; set; }
        public DateTime dueDate { get; set; }
        public int versionno { get; set; }
        public string Doctype { get; set; }
        public string Companyid { get; set; }
    }
}
