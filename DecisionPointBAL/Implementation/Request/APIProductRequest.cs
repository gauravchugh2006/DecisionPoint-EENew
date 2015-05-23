using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointBAL.Implementation.Request
{
    public class APIProductRequest
    {
        /// <summary>
        /// get & set ProductId
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// get & set ProductName
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// get & set ClientId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// get & set SubClientId
        /// </summary>
        public string SubClientId { get; set; }
    }
}
