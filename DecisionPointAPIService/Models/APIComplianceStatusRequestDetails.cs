using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPointAPIService.Models
{
    public class APIComplianceStatusRequestDetails
    {
        /// <summary>
        /// get & set ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// get & set SubClientId
        /// </summary>
        public string SubClientId { get; set; }
        ///// <summary>
        ///// get & set PackageId
        ///// </summary>
        //public int PackageId { get; set; }
        /// <summary>
        /// get & set User Id
        /// </summary>
        public string UserId { get; set; }

        public string ApiUserName { get; set; }

        public string ApiPassword { get; set; }

        public string ReferenceId { get; set; }
        /// <summary>
        /// get & set CandidateIdsCol
        /// </summary>
        public List<string> CandidateIdsCol { get; set; }
    }
}