using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    /// <summary>
    /// FilterModel
    /// </summary>
    /// <createdby>Sumit saurav</createdby>
    /// <createddate>may/29/2014</createddate>
    public class FilterModel
    {
        public string CompanyId { get; set; }
        public int type { get; set; }
        public string professionalfilter { get; set; }
        public string servicefilter { get; set; }
        public string titlefilter { get; set; }
        public string locationfilter { get; set; }
        public string typefilter { get; set; } 
    }
}