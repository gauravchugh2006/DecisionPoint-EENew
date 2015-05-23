using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class Reports
    {
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        ///get & set User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        ///get & set Company Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// get & set Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// get & set Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// get & set InvitedBY
        /// </summary>
        public string InvitedBY { get; set; }
        /// <summary>
        /// get & set DateFrom
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// get & set DateTo
        /// </summary>
        public DateTime DateTo { get; set; }
        /// <summary>
        /// get & set Reportdetails
        /// </summary>
        public IList<ReportResponse> Reportdetails { get; set; }

        /// <summary>
        /// get & set rowperpage
        /// </summary>
        public int rowperpage { get; set; }
        /// <summary>
        /// get & set pagesize
        /// </summary>
        public int pagesize { get; set; }
    }
}