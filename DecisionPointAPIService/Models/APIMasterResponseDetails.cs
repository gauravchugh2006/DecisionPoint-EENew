using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPointAPIService.Models
{
    public class APIMasterResponseDetails
    {
        public string ResultId { get; set; }

        public int ResultCode { get; set; }
        public int Id { get; set; }
        public dynamic JCRList { get; set; }
        public string Name { get; set; }
    }
}