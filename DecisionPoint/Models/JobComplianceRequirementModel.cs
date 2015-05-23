using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class JobComplianceRequirementModel
    {
        /// <summary>
        /// get & set ICTypeDetails
        /// </summary>
        public IEnumerable<CompanyDashBoardResponse> ICTypeDetails { get; set; }
        /// <summary>
        /// get & set ClientsList
        /// </summary>
        public IEnumerable<VendorsList> ClientsList { get; set; }
        /// <summary>
        /// get & set StateDetails
        /// </summary>
        public IList<StateRespone> StateDetails { get; set; }
        /// <summary>
        /// get & set BackgroundList
        /// </summary>
        public IList<BackGroundCheckMasterResponse> BackgroundPkgList { get; set; }
        /// <summary>
        /// get & set BackgroundMasterList
        /// </summary>
        public IList<BackGroundCheckMasterRequest> BackgroundMasterList { get; set; }
        /// <summary>
        /// get & set ProfessionalLicense
        /// </summary>
        public IList<LicenseInsuranceResponse> ProfLicMasterList { get; set; }
        /// <summary>
        /// get & set InsuranceMasterList
        /// </summary>
        public IList<LicenseInsuranceResponse> InsuranceMasterList { get; set; }
        /// <summary>
        /// get & set AddReqMasterList
        /// </summary>
        public IList<LicenseInsuranceResponse> AddReqMasterList { get; set; }

        /// <summary>
        /// get & set current running host url
        /// </summary>
        public string HostURL { get; set; }
        /// <summary>
        /// get & set current running Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// get & set AdditioalReqDoc
        /// </summary>
        public string AdditioalReqDoc { get; set; }
        public IEnumerable<StateRespone> StateList { get; set; }

        public IEnumerable<CompanyDashBoardResponse> TitleList { get; set; }
        #region Save BackgroundMappings/Professional License
        /// <summary>
        /// get & set ICTypeIds
        /// </summary>
        public List<string> ICTypeStaffTitleIds { get; set; }
        /// <summary>
        /// get & set ClientsIds
        /// </summary>
        public List<string> ClientsIds { get; set; }
        /// <summary>
        /// get & set BgCheckPkgId
        /// </summary>
        public string BgCheckPkgId { get; set; }
        /// <summary>
        /// get & set Type
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// get & set OperationType
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// get & set Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// get & set LicenseType
        /// </summary>
        public string LicenseType { get; set; }
        /// <summary>
        /// get & set LicenseNumber
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// get & set LicInsMapId
        /// </summary>
        public int LicInsMapId { get; set; }
        /// <summary>
        /// get & set LicenseNumber
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// get & set ExpirationDate
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// get & set CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// get & set Aggregate
        /// </summary>
        public double Aggregate { get; set; }
        /// <summary>
        /// get & set SingleOccurance
        /// </summary>
        public double SingleOccurance { get; set; }
        /// <summary>
        /// get & set State Id
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// get & set StateAbbre
        /// </summary>
        public string StateAbbre { get; set; }
        /// <summary>
        /// get & set InsuranceType
        /// </summary>
        public string InsuranceType { get; set; }
        /// <summary>
        /// get & set Uploaded By
        /// </summary>
        public string UploadedBy { get; set; }
        /// <summary>
        /// get & set Review
        /// </summary>
        public int Review { get; set; }
        /// <summary>
        /// get & set Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// get & set Acknowleagement
        /// </summary>
        public List<string> Acknowleagement { get; set; }

        /// <summary>
        /// get & set BGCheckTblId
        /// </summary>
        public int BGCheckTblId { get; set; }
        /// <summary>
        /// get & set ProfLicTblId
        /// </summary>
        public int ProfLicTblId { get; set; }
        /// <summary>
        /// get & set InsTblId
        /// </summary>
        public int InsTblId { get; set; }
        /// <summary>
        /// get & set AdditionalReqTblId
        /// </summary>
        public int AdditionalReqTblId { get; set; }
        /// <summary>
        /// get & set UploadedDocs
        /// </summary>
        public string UploadedDocs { get; set; }

        /// <summary>
        /// get & set InsuranceVerType
        /// </summary>
        public string InsuranceVerType { get; set; }
        /// <summary>
        /// get & set ICTypeId
        /// </summary>
        public int ICTypeId { get; set; }
        /// <summary>
        /// get & set IsStaffTitle
        /// </summary>
        public bool IsStaffTitle { get; set; }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ComplianceSection
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("")]
        None = -1,
        /// <summary>
        /// JCR Compliance Section partial view for background check
        /// </summary>
        [Description("_BackgroundCheck")]
        Background = 0,
        /// <summary>
        /// JCR Compliance Section partial view for professional license
        /// </summary>
        [Description("_ProfessionalLicense")]
        ProfessionalLicense = 1,
        /// <summary>
        /// JCR Compliance Section partial view for insurance
        /// </summary>
        [Description("_Insurance")]
        Insurance = 2,
        /// <summary>
        /// JCR Compliance Section partial view for additional requirement
        /// </summary>
        [Description("_AdditionalReqiurement")]
        AdditionalCredentials = 3,
    }


}