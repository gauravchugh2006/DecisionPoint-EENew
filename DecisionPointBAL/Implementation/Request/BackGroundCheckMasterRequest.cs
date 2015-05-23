

// ******************************************************************************************************************************
//                                                 class:BackGrounndCheckMasterRequest.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Aug. 14, 2014     |Sumit Saurav          | This class holds the interaction logic for BackGrounndCheckMasterRequest.cs
// ******************************************************************************************************************************

using System;
using System.Collections.Generic;
namespace DecisionPointBAL.Implementation.Request
{
    public class BackGroundCheckMasterRequest
    {
        #region << Public Variables >>
        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        public int Id { get; set; }

        public string BackgroundTitle { get; set; }

        public string BackgroundSource { get; set; }

        public bool Status { get; set; }

        public int RequiredByUserId { get; set; }
        public string RequiredByCompanyId { get; set; }
        public string CreatorCompanyId { get; set; }

        public int UserId { get; set; }

        public string CompanyId { get; set; }

        public string Documents { get; set; }

        public string BackgroundCheckStatus { get; set; }

        public DateTime? DateTime { get; set; }

        public string Source { get; set; }

        public string ReqCompanyName { get; set; }

        public bool IsActive { get; set; }

        public int MasterId { get; set; }

        public string Notes { get; set; }

        public bool IsDNA { get; set; }

        public DateTime? ExpirationDate { get; set; }
        public string BGCheckSource { get; set; }

        public int? ICTypeId { get; set; }
        public string ICType { get; set; }



        public string BGCheckId { get; set; }
        public int BGCheckPkgId { get; set; }
        

        public string BGCheckStatus { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public List<string> ICTypeStaffTitleIds { get; set; }
        public List<string> ClientIds { get; set; }
        public int OperationType { get; set; }
        public string PayType { get; set; }
        public string StateAbbre { get; set; }
        public int ModifiedBy { get; set; }
        public int RequirmentType { get; set; }


        #endregion


        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public BackGroundCheckMasterRequest()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
