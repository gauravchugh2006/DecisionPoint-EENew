using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.RequestParam
{
   public class JobReqForNewHireRequestParam
    {
        public string inviteCompanyId { get; set; }
        public string companyId { get; set; }
        public int userId { get; set; }
        public string userType { get; set; }
        public int parentuserId { get; set; }
        public int ICTypeId { get; set; }
        
    }
}
