using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
  public  class ICClientPermissionRequest
    {
        /// <summary>
        /// get & set VisibleTo
        /// </summary>
        public string VisibleTo { get; set; }
        /// <summary>
        /// get & set IC User Id
        /// </summary>
        public int ICUserId { get; set; }
        /// <summary>
        /// get & set IC Company Id
        /// </summary>
        public string ICCompanyId { get; set; }
        /// <summary>
        /// get & set IsVisible
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
