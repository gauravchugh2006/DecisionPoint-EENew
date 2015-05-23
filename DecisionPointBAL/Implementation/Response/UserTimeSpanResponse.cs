using System;

namespace DecisionPointBAL.Implementation.Response
{
  public  class UserTimeSpanResponse
    {
        public int docid { get; set; }
        public int TimeSpan { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<DateTime> ViewDate { get; set; }
    }

 
}
