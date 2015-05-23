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
    
    public partial class DP_SterlingReqResMaster
    {
        public int Id { get; set; }
        public Nullable<int> DPUserId { get; set; }
        public string DPCompanyId { get; set; }
        public Nullable<int> PackageId { get; set; }
        public string PackageName { get; set; }
        public string SterlingClientRefId { get; set; }
        public string SterlingOrderId { get; set; }
        public string OrganizationId { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<bool> SentRequest { get; set; }
        public Nullable<bool> GetResponse { get; set; }
        public string RequestFileUrl { get; set; }
        public string ResponseFileUrl { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> ResponseDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatorCompanyId { get; set; }
        public Nullable<int> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifedCompanyId { get; set; }
        public string UniqueRequestId { get; set; }
        public string FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public string CountyCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string DOB { get; set; }
        public string OrderScore { get; set; }
        public string OrderReceivedDate { get; set; }
        public string OrderCloseDate { get; set; }
        public string OrderSubmittedDate { get; set; }
        public string ReviewResultUrl { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseCountryCode { get; set; }
        public string LicenseStateCode { get; set; }
        public Nullable<System.DateTime> LicenseExpDate { get; set; }
    }
}
