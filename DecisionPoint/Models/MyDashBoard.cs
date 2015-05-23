using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;

namespace DecisionPoint.Models
{
    public class MyDashBoard
    {
        #region MyRegion
        private bool defaultvalueforiscoveragearea = true;
        private bool defaultvalueforisic = true;
        private bool defaultvalueforisvendor = true;
        private bool defaultvalueForIsservice = true;
        #endregion
        /// <summary>
        /// get & set IncomFromCompCommAlerts
        /// </summary>
        public IList<UserDashBoardResponse> IncomFromCompCommAlerts { get; set; }
        /// <summary>
        /// get & set IncomFromCompCommAlerts
        /// </summary>
        public IList<UserDashBoardResponse> LicAndInsAsDetails { get; set; }
        /// <summary>
        /// get & set IncomFromOutCompCommAlerts
        /// </summary>
        public IList<UserDashBoardResponse> IncomFromOutCompCommAlerts { get; set; }
        /// <summary>
        /// get & set CompanyBasedCommAlerts
        /// </summary>
        public IList<UserDashBoardResponse> CompanyBasedCommAlerts { get; set; }
        /// <summary>
        /// get & set ProfileDetailsAlerts
        /// </summary>
        public IList<ProfileAlertResponse> ProfileDetailsAlerts { get; set; }
        /// <summary>
        /// get & set JCRDetailsAlerts
        /// </summary>
        public IList<ReceiverReqDocResponse> JCRDetailsAlerts { get; set; }
        public IList<CreateContractResponse> ContractsAlerts { get; set; }
        /// <summary>
        /// get & set IncomInviteAlerts
        /// </summary>
        public IList<InviteResponse> IncomInviteAlerts { get; set; }
        /// <summary>
        /// get & set OutgoICVendorInviteAlerts
        /// </summary>
        public IList<InviteResponse> OutgoICVendorInviteAlerts { get; set; }
        /// <summary>
        /// get & set OutgoStaffInviteAlerts
        /// </summary>
        public IList<InviteResponse> OutgoStaffInviteAlerts { get; set; }
        /// <summary>
        /// get & set IsCoverageAreaApply
        /// </summary>
        public bool IsCoverageAreaApply { get { return defaultvalueforiscoveragearea; } set { defaultvalueforiscoveragearea = value; } }
        /// <summary>
        /// get & set IsICApply
        /// </summary>
        public bool IsICApply { get { return defaultvalueforisic; } set { defaultvalueforisic = value; } }
        /// <summary>
        /// get & set IsVendorApply
        /// </summary>
        public bool IsVendorApply { get { return defaultvalueforisvendor; } set { defaultvalueforisvendor = value; } }
        /// <summary>
        /// get & set IsServiceApply
        /// </summary>
        public bool IsServiceApply { get { return defaultvalueForIsservice; } set { defaultvalueForIsservice = value; } }
        /// <summary>
        /// get & set Selected Date
        /// </summary>
       public DateTime SelectedDate { get; set; }
       /// <summary>
       /// Used for get the list of client details
       /// </summary>
       public IEnumerable<RoleResponse> RoleDetails { get; set; }
       
    }
}