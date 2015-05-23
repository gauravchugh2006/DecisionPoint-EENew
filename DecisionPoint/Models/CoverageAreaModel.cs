using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class CoverageAreaModel
    {
        #region Save Coverage Area
        
        /// <summary>
        /// get & set StateCol
        /// </summary>
        public string StateCol { get; set; }
        /// <summary>
        /// get & set StateCol
        /// </summary>
        public string CountyCol { get; set; }
        /// <summary>
        /// get & set StateCol
        /// </summary>
        public string CityCol { get; set; }
        /// <summary>
        /// get & set StateCol
        /// </summary>
        public string ZipCol { get; set; }
        /// <summary>
        /// get & set Type
        /// </summary>
        public string CoverageAreaMode { get; set; }
        /// <summary>
        /// get & set userId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set modifiedby
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set coverageAreaFor
        /// </summary>
        public int CoverageAreaFor { get; set; }
        /// <summary>
        /// get & set CoverageAreaType
        /// </summary>
        public string CoverageAreaType { get; set; }

        #endregion

        #region Get Coverage Area
          /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<StateRespone> StateDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<CityResponse> CityDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<CountyResponse> CountyDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IList<ZipResponse> ZipDetails { get; set; }
        /// <summary>
        /// Used for get coverage area status
        /// </summary>
        public string CoverageAreaStatus { get; set; }
      
        #endregion
    }
}