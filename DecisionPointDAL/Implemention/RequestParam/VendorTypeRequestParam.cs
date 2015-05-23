using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.RequestParam
{
   public class VendorTypeRequestParam
    {
        /// <summary>
        /// get and set UserCompanyId
        /// </summary>
        public string UserCompanyId { get; set; }
        /// <summary>
        /// get and set CreatorCompanyID
        /// </summary>
        public string CreatorCompanyID { get; set; }
        /// <summary>
        /// get and set VendroTypeIds
        /// </summary>
        public string VendroTypeIds { get; set; }
        /// <summary>
        /// get and set TYPE
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// get and set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get and set CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
    }
}
