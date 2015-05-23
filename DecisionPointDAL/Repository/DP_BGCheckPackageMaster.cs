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
    
    public partial class DP_BGCheckPackageMaster
    {
        public DP_BGCheckPackageMaster()
        {
            this.DP_BGCheckMaster = new HashSet<DP_BGCheckMaster>();
            this.DP_BGCheckPackageDetailMaster = new HashSet<DP_BGCheckPackageDetailMaster>();
        }
    
        public int Id { get; set; }
        public string BGCheckPackage { get; set; }
        public string SterlingPkgName { get; set; }
        public bool IsDeleted { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public Nullable<double> Cost { get; set; }
        public Nullable<byte> Type { get; set; }
        public string UpgradePackage { get; set; }
        public int PacakgeGroup { get; set; }
        public string MappedPackage { get; set; }
        public Nullable<double> OldCost { get; set; }
    
        public virtual ICollection<DP_BGCheckMaster> DP_BGCheckMaster { get; set; }
        public virtual ICollection<DP_BGCheckPackageDetailMaster> DP_BGCheckPackageDetailMaster { get; set; }
        public virtual DP_BGCheckPackageMaster DP_BGCheckPackageMaster1 { get; set; }
        public virtual DP_BGCheckPackageMaster DP_BGCheckPackageMaster2 { get; set; }
    }
}
