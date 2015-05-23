using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class ConfigurationSettingDetail
    {
        #region Global Variable
        //IC
        private bool defaultIsBgCheckForIC = true;
        private bool defaultIsAddCreForIC = true;
        private bool defaultIsInterCheckForIC = true;
        private bool defaultIsLicenseForIC = true;
        private bool defaultIsCoverageAreaForIC = true;
        private bool defaultIsServicesForIC = true;
        private bool defaultIsICClientOnMyProfile = true;
        //Staff
        private bool defaultIsBgCheckForStaff = true;
        private bool defaultIsAddCreForStaff = true;
        private bool defaultIsInterCheckForStaff = true;
        private bool defaultIsLicenseForStaff = true;
        private bool defaultIsCoverageAreaForStaff = true;
        private bool defaultIsServicesForStaff = true;
        private bool defaultIsStaffClientOnMyProfile = true;
        //Module
        private bool defaultIsWebinarApply = true;
        private bool defaultIsScormApply = true;
        private bool defaultIsVendor = true;
        private bool defaultIsIc = true;
        private bool defaultIsClient = true;
        private bool defaultIsCoveragearea = true;
        private bool defaultIsServices = true;
        private bool defaultIsClientOnMyProfile = true;
        private bool defaultIsICFreeBasicMembership = true;
        private bool defaultIsICUsePackages = true;
        private bool defaultIsStaffCommApply = true;
        private bool defaultIsICCommApply = true;
        private bool defaultIsICInsApply = true;
        private bool defaultIsStaffInsApply = true;
        private bool defaultIsContractApply = true;
        private bool defaultIsClientNameApplyForIC = true;
        #endregion

        #region For IC
        /// <summary>
        /// get & set IsBgCheckForIC
        /// </summary>
        public bool IsBgCheckForIC { get { return defaultIsBgCheckForIC; } set { defaultIsBgCheckForIC = value; } }

        /// <summary>
        /// get & set IsAddCreForIC
        /// </summary>
        public bool IsAddCreForIC { get { return defaultIsAddCreForIC; } set { defaultIsAddCreForIC = value; } }

        /// <summary>
        /// get & set IsInterCheckForIC
        /// </summary>
        public bool IsInterCheckForIC { get { return defaultIsInterCheckForIC; } set { defaultIsInterCheckForIC = value; } }
        /// <summary>
        /// get & set IsLicenseApply
        /// </summary>
        public bool IsICLicenseApply { get { return defaultIsLicenseForIC; } set { defaultIsLicenseForIC = value; } }

        /// <summary>
        /// get & set IsCoverageAreaForIC
        /// </summary>
        public bool IsCoverageAreaForIC { get { return defaultIsCoverageAreaForIC; } set { defaultIsCoverageAreaForIC = value; } }
        /// <summary>
        /// get & set IsServicesForIC
        /// </summary>
        public bool IsServicesForIC { get { return defaultIsServicesForIC; } set { defaultIsServicesForIC = value; } }

        /// <summary>
        /// get & set IsICClientOnMyProfile
        /// </summary>
        public bool IsICClientOnMyProfile { get { return defaultIsICClientOnMyProfile; } set { defaultIsICClientOnMyProfile = value; } }

        #endregion

        #region For Staff

        /// <summary>
        /// get & set IsBgCheckForStaff
        /// </summary>

        public bool IsBgCheckForStaff { get { return defaultIsBgCheckForStaff; } set { defaultIsBgCheckForStaff = value; } }

        /// <summary>
        /// get & set IsAddCreForStaff
        /// </summary>
        public bool IsAddCreForStaff { get { return defaultIsAddCreForStaff; } set { defaultIsAddCreForStaff = value; } }

        /// <summary>
        /// get & set IsInterCheckForStaff
        /// </summary>
        public bool IsInterCheckForStaff { get { return defaultIsInterCheckForStaff; } set { defaultIsInterCheckForStaff = value; } }
        /// <summary>
        /// get & set IsLicenseForStaff
        /// </summary>
        public bool IsLicenseForStaff { get { return defaultIsLicenseForStaff; } set { defaultIsLicenseForStaff = value; } }
        /// <summary>
        /// get & set IsCoverageAreaForStaff
        /// </summary>
        public bool IsCoverageAreaForStaff { get { return defaultIsCoverageAreaForStaff; } set { defaultIsCoverageAreaForStaff = value; } }
        /// <summary>
        /// get & set IsServicesForStaff
        /// </summary>
        public bool IsServicesForStaff { get { return defaultIsServicesForStaff; } set { defaultIsServicesForStaff = value; } }

        /// <summary>
        /// get & set IsStaffClientOnMyProfile
        /// </summary>
        public bool IsStaffClientOnMyProfile { get { return defaultIsStaffClientOnMyProfile; } set { defaultIsStaffClientOnMyProfile = value; } }

        #endregion

        #region For Modules

        /// <summary>
        /// get & set IsWebinarApply
        /// </summary>
        public bool IsWebinarApply { get { return defaultIsWebinarApply; } set { defaultIsWebinarApply = value; } }

        /// <summary>
        /// get & set IsScormApply
        /// </summary>
        public bool IsScormApply { get { return defaultIsScormApply; } set { defaultIsScormApply = value; } }
        /// <summary>
        /// get & set IsContractApply
        /// </summary>
        public bool IsContractApply { get { return defaultIsContractApply; } set { defaultIsContractApply = value; } }
        /// <summary>
        /// get & set IsClientNameApply
        /// </summary>
        public bool IsClientNameApplyForIC { get { return defaultIsClientNameApplyForIC; } set { defaultIsClientNameApplyForIC = value; } }
        /// <summary>
        /// get & set IsICFreebasicMembership
        /// </summary>
        public bool IsICFreeBasicMembership { get { return defaultIsICFreeBasicMembership; } set { defaultIsICFreeBasicMembership = value; } }
        /// <summary>
        /// get & set IsPackagePaymentHide
        /// </summary>
        public bool IsICUsePackages { get { return defaultIsICUsePackages; } set { defaultIsICUsePackages = value; } }
       
        /// <summary>
        /// get & set IsStaffInsApply
        /// </summary>
        public bool IsStaffInsApply { get { return defaultIsStaffInsApply; } set { defaultIsStaffInsApply = value; } }
        /// <summary>
        /// get & set IsICInsApply
        /// </summary>
        public bool IsICInsApply { get { return defaultIsICInsApply; } set { defaultIsICInsApply = value; } }
        /// <summary>
        /// get & set IsStaffCommApply
        /// </summary>
        public bool IsStaffCommApply { get { return defaultIsStaffCommApply; } set { defaultIsStaffCommApply = value; } }
        /// <summary>
        /// get & set IsICCommApply
        /// </summary>
        public bool IsICCommApply { get { return defaultIsICCommApply; } set { defaultIsICCommApply = value; } }
       
        /// <summary>
        /// get & set IsVendor
        /// </summary>
        public bool IsVendor { get { return defaultIsVendor; } set { defaultIsVendor = value; } }

        /// <summary>
        /// get & set IsIc
        /// </summary>
        public bool IsIc { get { return defaultIsIc; } set { defaultIsIc = value; } }

        /// <summary>
        /// get & set IsClient
        /// </summary>
        public bool IsClient { get { return defaultIsClient; } set { defaultIsClient = value; } }

        /// <summary>
        /// get & set IsCoveragearea
        /// </summary>
        public bool IsCoveragearea { get { return defaultIsCoveragearea; } set { defaultIsCoveragearea = value; } }

        /// <summary>
        /// get & set IsServices
        /// </summary>
        public bool IsServices { get { return defaultIsServices; } set { defaultIsServices = value; } }

        /// <summary>
        /// get & set IsClient
        /// </summary>
        public bool IsClientOnMyProfile { get { return defaultIsClientOnMyProfile; } set { defaultIsClientOnMyProfile = value; } }

        #endregion

        #region Others

        /// <summary>
        /// get & set UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// get & set tblId
        /// </summary>
        public int tblId { get; set; }

        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set VendorTypeId
        /// </summary>
        public string VendorTypeId { get; set; }
        /// <summary>
        /// get & set Type
        /// </summary>
        public string Type { get; set; }
        

        /// <summary>
        /// Used for get the list of VendorTypeDetails
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> VendorTypeDetails { get; set; }

        /// <summary>
        /// Used for get the list of CompanyVendorTypeDetails
        /// </summary>
        public IEnumerable<VendorTypeResponse> CompanyVendorTypeDetails { get; set; }

        #endregion
    }
}