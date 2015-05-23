using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
   public class MyDashBoardRequest
    {
        /// <summary>
        /// get & set User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set Company Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set User Type
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// get & set Selected Date
        /// </summary>
        public DateTime SelectedDate { get; set; }
    }
}
