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
    
    public partial class DP_DefaultPaymentMaster
    {
        public int Id { get; set; }
        public Nullable<double> CompanyFee { get; set; }
        public Nullable<double> PerFieldStaffFee { get; set; }
        public Nullable<double> PerOfficeStaffFee { get; set; }
        public Nullable<double> PerIcFee { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsInvoice { get; set; }
    }
}
