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
    
    public partial class DP_DocumentDetails
    {
        public DP_DocumentDetails()
        {
            this.DP_CommAssesment = new HashSet<DP_CommAssesment>();
            this.DP_CommContents = new HashSet<DP_CommContents>();
            this.DP_CommLinks = new HashSet<DP_CommLinks>();
            this.DP_CommRecipientFilter = new HashSet<DP_CommRecipientFilter>();
            this.DP_CommRquiredActions = new HashSet<DP_CommRquiredActions>();
            this.DP_CommTestRules = new HashSet<DP_CommTestRules>();
        }
    
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string DocType { get; set; }
        public string Introduction { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Reference { get; set; }
        public string CompanyId { get; set; }
        public string PolicyNo { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<int> VersionNo { get; set; }
        public string Retake { get; set; }
        public string HOC { get; set; }
        public Nullable<bool> ReqNewVendor { get; set; }
        public Nullable<bool> ReqNewStaff { get; set; }
        public Nullable<bool> ReqNewIC { get; set; }
        public Nullable<int> DaysToComplete { get; set; }
        public string DocGroup { get; set; }
        public string DocTitles { get; set; }
        public string VideoTitle { get; set; }
        public string ScormTitles { get; set; }
        public Nullable<bool> OnStaging { get; set; }
        public Nullable<bool> OnLib { get; set; }
        public Nullable<System.DateTime> WithdrawnDate { get; set; }
        public Nullable<bool> IsEmployementReq { get; set; }
    
        public virtual ICollection<DP_CommAssesment> DP_CommAssesment { get; set; }
        public virtual ICollection<DP_CommContents> DP_CommContents { get; set; }
        public virtual ICollection<DP_CommLinks> DP_CommLinks { get; set; }
        public virtual ICollection<DP_CommRecipientFilter> DP_CommRecipientFilter { get; set; }
        public virtual ICollection<DP_CommRquiredActions> DP_CommRquiredActions { get; set; }
        public virtual ICollection<DP_CommTestRules> DP_CommTestRules { get; set; }
        public virtual DP_User DP_User { get; set; }
    }
}