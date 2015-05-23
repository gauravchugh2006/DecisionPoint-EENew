using System.Collections.Generic;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class ServiceTranslationModel
    {
        /// <summary>
        /// Used for get the list of service details of parent
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ParentServiceList { get; set; }
        /// <summary>
        /// Used for get the list of service details of child
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ChildServiceList { get; set; }
        /// <summary>
        /// Used for get the list of service details of child
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> MappdedServiceList { get; set; }
        public string companyName { get; set; }
        public string pCompanyId { get; set; }
    }
}