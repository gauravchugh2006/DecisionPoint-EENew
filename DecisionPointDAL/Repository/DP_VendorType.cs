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
    
    public partial class DP_VendorType
    {
        public DP_VendorType()
        {
            this.DP_ReqDocVendorType = new HashSet<DP_ReqDocVendorType>();
        }
    
        public int Id { get; set; }
        public string VendorType { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string CompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual ICollection<DP_ReqDocVendorType> DP_ReqDocVendorType { get; set; }
    }
}
