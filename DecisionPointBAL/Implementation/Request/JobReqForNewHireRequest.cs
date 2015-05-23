using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
    public class JobReqForNewHireRequest
    {
        public string inviteCompanyId { get; set; }
        public string companyId { get; set; }
        public string userType { get; set; }
        public int userId { get; set; }
        public int parentuserId { get; set; }
        public int ICTypeId { get; set; }
        
    }
}
