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
    
    public partial class DP_AdditionalReqAck
    {
        public int Id { get; set; }
        public string Ackknow { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> AddReqId { get; set; }
    
        public virtual DP_AdditionalReqMaster DP_AdditionalReqMaster { get; set; }
    }
}
