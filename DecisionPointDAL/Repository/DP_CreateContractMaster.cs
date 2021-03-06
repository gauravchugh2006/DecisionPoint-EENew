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
    
    public partial class DP_CreateContractMaster
    {
        public DP_CreateContractMaster()
        {
            this.DP_ContractEvent = new HashSet<DP_ContractEvent>();
        }
    
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public Nullable<int> OwnerId { get; set; }
        public Nullable<int> ContractWithId { get; set; }
        public Nullable<int> ExecutedById { get; set; }
        public Nullable<System.DateTime> ExecutedDate { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<System.DateTime> ContractDate { get; set; }
        public Nullable<int> ExpirationDateReminder { get; set; }
        public string GeneralComments { get; set; }
        public string Paragraph { get; set; }
        public string Section { get; set; }
        public string SubSection { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public Nullable<int> EventDateReminder { get; set; }
        public string CreatorCompanyId { get; set; }
        public string ContractorCompanyId { get; set; }
        public string Role { get; set; }
        public string Title { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<DP_ContractEvent> DP_ContractEvent { get; set; }
    }
}
