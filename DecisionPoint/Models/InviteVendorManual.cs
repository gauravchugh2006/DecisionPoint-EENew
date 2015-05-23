using System.Collections.Generic;
using System.Web;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class InviteVendorManual
    {
        #region Global Variable
        private bool defaultvalueforisclient = true;
        private bool defaultvalueforisvendor = true;
        
        #endregion
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
        public IEnumerable<RoleResponse> DocflowDetails { get; set; }
        /// <summary>
        /// Used for get the list of client details
        /// </summary>
        public IEnumerable<RoleResponse> permissionDetails { get; set; }
        public IEnumerable<ICPaymentModeResponse> GetICPaymentMode { get; set; }
        public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }
        
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
        /// <summary>
        /// get & set Vendor apply or not
        /// </summary>
        public bool IsVendorApply { get { return defaultvalueforisvendor; } set { defaultvalueforisvendor = value; } }
        /// <summary>
        /// get & set client apply or not
        /// </summary>
        public bool IsClientApply { get { return defaultvalueforisclient; } set { defaultvalueforisclient = value; } }

        #endregion
        #endregion
        //[Required, FileExtensions(Extensions = "csv", ErrorMessage = "Only CSV files")]
        public HttpPostedFileBase File { get; set; }

        public string Seprator { get; set; }
        public bool FirstRowHeader { get; set; }
    }
   
}