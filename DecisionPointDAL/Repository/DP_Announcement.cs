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
    
    public partial class DP_Announcement
    {
        public int Id { get; set; }
        public string Announcement { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsClose { get; set; }
    
        public virtual DP_User DP_User { get; set; }
    }
}
