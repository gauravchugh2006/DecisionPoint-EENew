using System.Collections.Generic;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using System;


namespace DecisionPoint.Models
{
    public class LicenseModel
    {
        #region Global Variable
        private bool defaultvalueforisclient = true;
        private bool defaultvalueforisvendor = true;
        private bool defaultvalueforiscoveragearea = true;
        private bool defaultvalueforisic = true;
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
        /// Used to get and set LicenseInsuranceList 
        /// </summary>
        public IList<LicenseInsuranceResponse> LicenseInsuranceList { get; set; }
        /// <summary>
        /// Used to get and set PastLicenseInsuranceList 
        /// </summary>
        public IList<LicenseInsuranceResponse> PastLicenseInsuranceList { get; set; }
        /// <summary>
        /// get & set Vendor apply or not
        /// </summary>
        public bool IsVendorApply { get { return defaultvalueforisvendor; } set { defaultvalueforisvendor = value; } }
        /// <summary>
        /// get & set client apply or not
        /// </summary>
        public bool IsClientApply { get { return defaultvalueforisclient; } set { defaultvalueforisclient = value; } }

        /// <summary>
        /// get & set IC apply or not
        /// </summary>
        public bool IsICApply { get { return defaultvalueforisic; } set { defaultvalueforisic = value; } }
        /// <summary>
        /// get & set IsCoverageAreaApply
        /// </summary>
        public bool IsCoverageAreaApply { get { return defaultvalueforiscoveragearea; } set { defaultvalueforiscoveragearea = value; } }
        public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }

        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// get & set Isdeleted
        /// </summary>
        public bool? isDeleted { get; set; }
        /// <summary>
        /// get & set IsActive
        /// </summary>
        public bool? isActive { get; set; }
        /// <summary>
        /// get & set id 
        /// </summary>
        public int id { get; set; }

      
        #endregion

        #region License Check
         /// <summary>
        /// get & set LicenseNumber
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// get & set StateAbb
        /// </summary>
        public string StateAbbre { get; set; }
        //get & set LicenseType
        public string LicenseType { get; set; }
        public string CompanyName { get; set; }

        public string LisenceNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }
        #endregion
    }
}