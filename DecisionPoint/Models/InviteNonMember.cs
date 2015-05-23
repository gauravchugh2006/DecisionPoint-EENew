using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class InviteNonMember
    {
        #region << Public Variables >>
        /// <summary>
        /// Used for set fName
        /// </summary>
        public string fName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string lName { get; set; }
        /// <summary>
        /// Used for set emailId
        /// </summary>
        public string emailId { get; set; }

        public int? UserId { get; set; }
        /// <summary>
        /// Used for set companyId
        /// </summary>
        public string companyId { get; set; }

        public int flowId { get; set; }

        public int PaymentType { get; set; }

        public bool IsMailSent { get; set; }

        public string CompanyName { get; set; }

        public int DocFlowId { get; set; }

        public int CreatedBy { get; set; }

        public string UserCompanyId { get; set; }

        public string CreatorCompanyId { get; set; }

        public string CreatorCompanyName { get; set; }

        public string Credentails { get; set; }

        #endregion
    }
}