using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointDAL.Implemention.ResponseParam
{
   public class SterlingWithDpResponseParam
    {
       /// <summary>
        /// get & set PackageId
       /// </summary>
        public int PackageId { get; set; }
       /// <summary>
        /// get & set DPCompanyId
       /// </summary>
        public string DPCompanyId { get; set; }
       /// <summary>
       /// get & set DpUserId
       /// </summary>
        public int DpUserId { get; set; }
    }
}
