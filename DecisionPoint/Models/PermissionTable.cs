using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class PermissionTable
    {
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> TitleDetails { get; set; }
        /// <summary>
        /// Used for get the list of group details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> GroupDetails { get; set; }
        /// <summary>
        /// Used for get the list of group details
        /// </summary>
        public IList<PermissionTableResponse> FuntionalPermissions { get; set; }
        /// <summary>
        /// Used for get the list of funtional permission details as per particular company
        /// </summary>
        public IList<PermissionTableResponse> CompanyBasedPermissions { get; set; }
    }
}