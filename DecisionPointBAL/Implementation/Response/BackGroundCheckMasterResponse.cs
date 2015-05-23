using System;

// ******************************************************************************************************************************
//                                                 class:BackGrounndCheckMasterResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Aug. 14, 2014     |Sumit Saurav            | This class holds the interaction logic for BackGrounndCheckMasterResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class BackGroundCheckMasterResponse
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        public int Id { get; set; }

        public string BackgroundTitle { get; set; }

        public string BackgroundSource { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
        public double PacakgeCost { get; set; }
        public int PacakgeType { get; set; }
        public int PacakgeGroup { get; set; }

        public string Content { get; set; }
        public double PacakgeDetailCost { get; set; }
        public string Source { get; set; }

        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string RequiredByCompanyId { get; set; }

        public string RequiredByCompanyName { get; set; }
        public string SterlingPkgName { get; set; }
        public string SterlingOrderId { get; set; }
        public string UpgradePkgIds { get; set; }
        public int BgCheckMasterTblId { get; set; }
        public int SterlingPkgId { get; set; }
        /// <summary>
        /// get & set DriverLicNumber
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// get & set DriverLicCountryCode
        /// </summary>
        public string LicenseCountryCode { get; set; }
        public string StateAbbre { get; set; }
        public string MappedSterlingPkg { get; set; }
        /// <summary>
        /// get & set DriverlicStateCode
        /// </summary>
        public string LicenseStateCode { get; set; }
        /// <summary>
        /// get & set DriverLicExpDate
        /// </summary>
        public DateTime? LicenseExpDate { get; set; }

        public DateTime? ReceivedDate { get; set; }
        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public BackGroundCheckMasterResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
