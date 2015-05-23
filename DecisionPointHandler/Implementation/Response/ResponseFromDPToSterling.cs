using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPointHandler.Implementation.Response
{
    public class ResponseFromDPToSterling
    {
        public string EntityNoException { get; set; }
        public string ExceptionMessage { get; set; }
        public string EntityIdentifierMessage { get; set; }
        /// <summary>
        /// get & set PackageId
        /// </summary>
        public int PackageId { get; set; }
        /// <summary>
        /// get & set DPCompanyId
        /// </summary>
        public string DPCompanyId { get; set; }
        /// <summary>
        /// get & set DpUserId
        /// </summary>
        public int DpUserId { get; set; }
    }
}