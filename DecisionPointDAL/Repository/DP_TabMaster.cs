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
    
    public partial class DP_TabMaster
    {
        public int Id { get; set; }
        public string TabName { get; set; }
        public string TabId { get; set; }
        public string TabUrl { get; set; }
        public Nullable<int> FunPerId { get; set; }
        public bool IsDelete { get; set; }
        public Nullable<bool> IsMainTab { get; set; }
        public string MainTabName { get; set; }
        public string TabSectionName { get; set; }
        public string TabDashBoard { get; set; }
    }
}