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
    
    public partial class DP_ServiceTranslationTable
    {
        public int Id { get; set; }
        public Nullable<int> ParentServiceId { get; set; }
        public Nullable<int> ChildServiceId { get; set; }
        public string ParentCompanyId { get; set; }
        public string ChildCompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}