using System;

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class publishCommRequestParam
    {
        public string totalStaff { get; set; }
        public string totalIC { get; set; }
        public string totalVendor { get; set; }
        public string totalClient { get; set; }
        public int docId { get; set; }
        public DateTime dueDate { get; set; }
        public int versionno { get; set; }
        public string Doctype { get; set; }
        public string CompanyId { get; set; }

    }
}
