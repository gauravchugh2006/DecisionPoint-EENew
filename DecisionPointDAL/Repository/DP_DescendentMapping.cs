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
    
    public partial class DP_DescendentMapping
    {
        public int DescendentMappingID { get; set; }
        public int ParentCompanyID { get; set; }
        public int ChildCompanyId { get; set; }
        public bool isActive { get; set; }
    }
}
