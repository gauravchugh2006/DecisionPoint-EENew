using System.Collections.Generic;

namespace DecisionPointBAL.Implementation.Response
{
   public class StateRespone
    {
        public int StateId { get; set; }
        public double? DriverLicenseCost { get; set; }
        public string SateName { get; set; }
        public string Abbrebiation { get; set; }
        public int StateType { get; set; }
        public IList<StateRespone> ResponseState { get; set; }
    }
}
