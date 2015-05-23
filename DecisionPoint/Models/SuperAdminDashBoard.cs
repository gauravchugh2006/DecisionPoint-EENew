using System.Collections.Generic;

namespace DecisionPoint.Models
{
    public class SuperAdminDashBoard
    {
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        public bool Active { get; set; }
        public bool InActive { get; set; }
       
        public List<Companies> CompanyList { get; set; }
        public List<Companies> PastcompanyList { get; set; }
    }

}