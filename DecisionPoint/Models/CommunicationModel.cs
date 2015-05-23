using System.Collections.Generic;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class CommunicationModel
    {
        #region paging
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        #endregion
        public IEnumerable<StaffPerformaceResponse> staffPerformanceLst { get; set; }
        public IEnumerable<StaffPerformaceResponse> ICPerformanceLst { get; set; }
       
    }
}