using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Response
{
   public class ReportResponse
    {
        #region GenrateResponse
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string ChildUserName { get; set; }
        /// <summary>
        ///get & set User Id
        /// </summary>
        public int ChildID { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string ChildEmailId { get; set; }
        /// <summary>
        /// get & set Phone
        /// </summary>
        public string ChildPhoneNo { get; set; }
        /// <summary>
        /// get & set Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// get & set InvitedBY
        /// </summary>
        public string ParentUserName { get; set; }
        /// <summary>
        /// get & set InvitedBY
        /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int UserId { get; set; }
        #endregion

        #region SavedREportDetails

        /// <summary>
        /// get & set DateFrom
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// get & set DateTo
        /// </summary>
        public DateTime? DateTo { get; set; }
        #endregion
    }
}
