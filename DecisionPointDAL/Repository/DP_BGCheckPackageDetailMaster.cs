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
    
    public partial class DP_BGCheckPackageDetailMaster
    {
        public DP_BGCheckPackageDetailMaster()
        {
            this.DP_BGCheckDetailMapping = new HashSet<DP_BGCheckDetailMapping>();
        }
    
        public int Id { get; set; }
        public string Content { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> PackageId { get; set; }
        public Nullable<double> Cost { get; set; }
        public string SterlingScreenType { get; set; }
        public string SterlingQualifier { get; set; }
    
        public virtual ICollection<DP_BGCheckDetailMapping> DP_BGCheckDetailMapping { get; set; }
        public virtual DP_BGCheckPackageMaster DP_BGCheckPackageMaster { get; set; }
    }
}