using System.Collections.Generic;
using System.Web;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class InviteVendorBulk
    {
        #region Global Variable
        private bool defaultvalueforisclient = true;
        private bool defaultvalueforisvendor = true;

        #endregion
        #region paging
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        #endregion
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// Used for get the list of Flow
        /// </summary>
        public IEnumerable<RoleResponse> flowDetails { get; set; }
        public IEnumerable<RoleResponse> DocflowDetails { get; set; }
        /// <summary>
        /// get & set Vendor apply or not
        /// </summary>
        public bool IsVendorApply { get { return defaultvalueforisvendor; } set { defaultvalueforisvendor = value; } }
        /// <summary>
        /// get & set client apply or not
        /// </summary>
        public bool IsClientApply { get { return defaultvalueforisclient; } set { defaultvalueforisclient = value; } }
        public IList<VendorCSV> LstVendorCsv { get; set; }
        public string CsvFileName { get; set; }
        public InviteVendorBulk()
        {
            LstVendorCsv = new List<VendorCSV>();
        }
        public IEnumerable<ICPaymentModeResponse> GetICPaymentMode { get; set; }
        public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }
    }
}