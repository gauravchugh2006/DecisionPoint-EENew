using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
   public class LicenseCheckRequest
    {
        /// <summary>
        /// get & set User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set LicenseNumber
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// get & set LicenseType
        /// </summary>
        public string LicenseType { get; set; }
        /// <summary>
        /// get & set ExpDate
        /// </summary>
        public DateTime ExpDate { get; set; }
        /// <summary>
        /// get & set IssueDate
        /// </summary>
        public DateTime IssueDate { get; set; }
        /// <summary>
        /// get & set EffectiveDate
        /// </summary>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// get & set FName
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// get & set LName
        /// </summary>
        public string LName { get; set; }
        /// <summary>
        /// get & set Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// get & set StateAbbre
        /// </summary>
        public string StateAbbre { get; set; }
        /// <summary>
        /// get & set county
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// get & set city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// get & set zip
        /// </summary>
        public string Zip { get; set; }
    }
}
