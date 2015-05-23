using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
   public class ConfiguratonSettingRequest
    {
       #region For IC
        /// <summary>
        /// get & set IsBgCheckForIC
        /// </summary>
        public bool IsBgCheckForIC { get; set; }

        /// <summary>
        /// get & set IsAddCreForIC
        /// </summary>
        public bool IsAddCreForIC { get; set; }

        /// <summary>
        /// get & set IsInterCheckForIC
        /// </summary>
        public bool IsInterCheckForIC { get; set; }
         /// <summary>
        /// get & set IsLiceInsForIC
        /// </summary>
        public bool IsLiceInsForIC { get; set; }
        
        /// <summary>
        /// get & set IsCoverageAreaForIC
        /// </summary>
        public bool IsCoverageAreaForIC { get; set; }
        /// <summary>
        /// get & set IsServicesForIC
        /// </summary>
        public bool IsServicesForIC { get; set; }

        /// <summary>
        /// get & set IsICClientOnMyProfile
        /// </summary>
        public bool IsICClientOnMyProfile { get; set; }

        #endregion

        #region For Staff

        /// <summary>
        /// get & set IsBgCheckForStaff
        /// </summary>
        public bool IsBgCheckForStaff { get; set; }

        /// <summary>
        /// get & set IsAddCreForStaff
        /// </summary>
        public bool IsAddCreForStaff { get; set; }

        /// <summary>
        /// get & set IsInterCheckForStaff
        /// </summary>
        public bool IsInterCheckForStaff { get; set; }
        /// <summary>
        /// get & set IsLicenseForStaff
        /// </summary>
        public bool IsLicenseForStaff { get; set; }
        /// <summary>
        /// get & set IsCoverageAreaForStaff
        /// </summary>
        public bool IsCoverageAreaForStaff { get; set; }
        /// <summary>
        /// get & set IsServicesForStaff
        /// </summary>
        public bool IsServicesForStaff { get; set; }

        /// <summary>
        /// get & set IsStaffClientOnMyProfile
        /// </summary>
        public bool IsStaffClientOnMyProfile { get; set; }

        #endregion

        #region For Modules

        /// <summary>
        /// get & set IsWebinarApply
        /// </summary>
        public bool IsWebinarApply { get; set; }

        /// <summary>
        /// get & set IsScormApply
        /// </summary>
        public bool IsScormApply { get; set; }

        /// <summary>
        /// get & set IsVendor
        /// </summary>
        public bool IsVendor { get; set; }

        /// <summary>
        /// get & set IsIc
        /// </summary>
        public bool IsIc { get; set; }

        /// <summary>
        /// get & set IsClient
        /// </summary>
        public bool IsClient { get; set; }

        /// <summary>
        /// get & set IsCoveragearea
        /// </summary>
        public bool IsCoveragearea { get; set; }

        /// <summary>
        /// get & set IsServices
        /// </summary>
        public bool IsServices { get; set; }

        /// <summary>
        /// get & set IsClient
        /// </summary>
        public bool IsClientOnMyProfile { get; set; }
        /// <summary>
        /// get & set IsICFreebasicMembership
        /// </summary>
        public bool IsICFreeBasicMembership { get; set; }
        /// <summary>
        /// get & set IsPackagePaymentHide
        /// </summary>
        public bool IsICUsePackages { get; set; }

        /// <summary>
        /// get & set IsStaffInsApply
        /// </summary>
        public bool IsStaffInsApply { get; set; }
        /// <summary>
        /// get & set IsICInsApply
        /// </summary>
        public bool IsICInsApply { get; set; }
        /// <summary>
        /// get & set IsStaffCommApply
        /// </summary>
        public bool IsStaffCommApply { get; set; }
        /// <summary>
        /// get & set IsICCommApply
        /// </summary>
        public bool IsICCommApply { get; set; }
        /// <summary>
        /// get & set IsContractApply
        /// </summary>
        public bool IsContractApply { get; set; }
        /// <summary>
        /// get & set IsClientNameApply
        /// </summary>
        public bool IsClientNameApplyForIC { get; set; }
       

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
        /// get & set CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
       /// <summary>
       /// get & set config setting collection
       /// </summary>
        public IList<ConfiguratonSettingRequest> ConfigSettingCollection { get; set; }
        #endregion
       
       
    }
}
