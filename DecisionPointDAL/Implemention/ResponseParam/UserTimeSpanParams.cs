using System;

namespace DecisionPointDAL.Implemention.ResponseParam
{
   public class UserTimeSpanParams
    {
        public int docid { get; set; }
        public int TimeSpan { get; set; }
        public Nullable<DateTime> CompletionDate { get; set; }
        public Nullable<DateTime> ViewDate { get; set; }
    }

}
