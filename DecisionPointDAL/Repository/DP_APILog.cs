//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DecisionPointDAL.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class DP_APILog
    {
        public int Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string RefrenceId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public Nullable<System.DateTime> RequestTime { get; set; }
        public Nullable<System.DateTime> ResponseTime { get; set; }
    
        public virtual DP_APIMaster DP_APIMaster { get; set; }
    }
}
