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
    
    public partial class DP_BackGroundCheckMaster
    {
        public int Id { get; set; }
        public Nullable<bool> IsBackGroundCheck { get; set; }
        public int UserId { get; set; }
        public string UserCompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedCompanyId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual DP_User DP_User { get; set; }
    }
}
