using System.Collections.Generic;
using System.Web;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class RegisterStep2
    {
        public IList<StateRespone> StateList { get; set; }
        public IList<CityResponse> CityList { get; set; }
        public IList<ZipResponse> ZipList { get; set; }
        public IList<string> PostedState { get; set; }
        public IList<StateRespone> ResponseState { get; set; }

        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? CountyId { get; set; }
        public bool state { get; set; }
        public IList<CountyResponse> CountyList { get; set; }
        public string StateAbbre { get; set; }
      
    }
   
}