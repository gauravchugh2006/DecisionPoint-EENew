using System.Collections.Generic;
using System.Web;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    
    public class InviteStaffModel
    {
        #region Title & Client & Role & Flow & Permmossion
        /// <summary>
        /// Used for get the list of title details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> titleDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> clientDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> roleDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> flowDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> permissionDetails { get; set; }
        #endregion

        #region paging
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        #endregion

        #region Invitaion
        #region Manual Invitation
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffFName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffLName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffTitleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffRoleId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int staffPermissionId { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffEmailId { get; set; }
        //[Required, FileExtensions(Extensions = "csv", ErrorMessage = "Only CSV files")]
        public HttpPostedFileBase File { get; set; }

        public string Seprator { get; set; }
        public bool FirstRowHeader { get; set; }

        public IList<CSV> lst_Csv { get; set; }
        public string CsvFileName { get; set; }
        
        public InviteStaffModel()
        {
            
            lst_Csv = new List<CSV>();
        }

        #endregion
        #endregion
    }     
}