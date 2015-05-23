using System.Collections.Generic;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class EmpReq
    {
        #region Global Variable
        private bool defaultValueForIsClient = true;
        private bool defaultValueForIsVendor = true;
        private bool defaultValueForIsCoveragearea = true;
        private bool defaultValueForIsService = true;
        private bool defaultValueForIsIc = true;
        #endregion

        #region BasicDetails
         /// <summary>
        /// get & set state list
        /// </summary>
        public IEnumerable<StateRespone> StateList { get; set; }
        /// <summary>
        /// Used for get the list of Services details
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> serviceDetails { get; set; }
        /// <summary>
        /// get & set global uploaded content path
        /// </summary>
        public string globauploadedcontentpath { get; set; }
        /// <summary>
        /// get & set specific uploaded content path
        /// </summary>
        public string specificuploadedcontentpath { get; set; }
        /// <summary>
        /// get & set current running host url
        /// </summary>
        public string hostURL { get; set; }
        #endregion
        #region Req Doc Details By Sender
        /// <summary>
        /// Used to set global req doc details setup by Admin
        /// </summary>
       public IList<SubmitReqDocRequest> CurrentGlobalReqDocList { get; set; }
       /// <summary>
       /// Used to set specific req doc details setup by Admin
       /// </summary>
       public IList<SubmitReqDocRequest> CurrentSpecificReqDocList { get; set; }
       /// <summary>
       /// Used to set global req doc details setup by Admin
       /// </summary>
       public IList<SubmitReqDocRequest> PastGlobalReqDocList { get; set; }
       /// <summary>
       /// Used to set specific req doc details setup by Admin
       /// </summary>
       public IList<SubmitReqDocRequest> PastSpecificReqDocList { get; set; }
       /// <summary>
       /// get & set Vendor apply or not
       /// </summary>
       public bool IsVendorApply { get { return defaultValueForIsVendor; } set { defaultValueForIsVendor = value; } }
       /// <summary>
       /// get & set client apply or not
       /// </summary>
       public bool IsClientApply { get { return defaultValueForIsClient; } set { defaultValueForIsClient = value; } }
      
       /// <summary>
       /// get & set IC apply or not
       /// </summary>
       public bool IsICApply { get { return defaultValueForIsIc; } set { defaultValueForIsIc = value; } }
       /// <summary>
       /// get & set IsCoverageAreaApply
       /// </summary>
       public bool IsCoverageAreaApply { get { return defaultValueForIsCoveragearea; } set { defaultValueForIsCoveragearea = value; } }
       /// <summary>
       /// get & set IsServiceApply
       /// </summary>
       public bool IsServiceApply { get { return defaultValueForIsService; } set { defaultValueForIsService = value; } }
       public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }
        #endregion

    }
}