using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
    public class APIComplianceStatusRequest
    {
        /// <summary>
        /// get & set ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// get & set SubClientId
        /// </summary>
        public string SubClientId { get; set; }
        /// <summary>
        /// get & set PackageId
        /// </summary>
        public int PackageId { get; set; }
        /// <summary>
        /// get & set CandidateIdsCol
        /// </summary>
        public List<string> CandidateIdsCol { get; set; }
        /// <summary>
        /// get & set User Id
        /// </summary>
        public string UserId { get; set; }
    }
}
