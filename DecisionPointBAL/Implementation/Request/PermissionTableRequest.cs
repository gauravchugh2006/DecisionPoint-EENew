using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
    public class PermissionTableRequest
    {
        /// <summary>
        /// get & set FunPermissionIds
        /// </summary>
        public string FunPermissionIds { get; set; }
        /// <summary>
        /// get & set TitleIds
        /// </summary>
        public int TitleId { get; set; }
        /// <summary>
        /// get & set CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// get & set ModifiedBy
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// get & set CreatedCompanyId
        /// </summary>
        public string CreatedCompanyId { get; set; }
        /// <summary>
        /// get & set Type
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set UserType
        /// </summary>
        public string UserType { get; set; }
    }
}
