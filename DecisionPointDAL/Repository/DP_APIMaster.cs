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
    
    public partial class DP_APIMaster
    {
        public DP_APIMaster()
        {
            this.DP_APILog = new HashSet<DP_APILog>();
        }
    
        public int Id { get; set; }
        public string APIName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<DP_APILog> DP_APILog { get; set; }
    }
}
