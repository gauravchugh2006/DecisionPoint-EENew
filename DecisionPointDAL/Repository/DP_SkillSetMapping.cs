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
    
    public partial class DP_SkillSetMapping
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> SkillId { get; set; }
    
        public virtual DP_SkillSetMaster DP_SkillSetMaster { get; set; }
    }
}
