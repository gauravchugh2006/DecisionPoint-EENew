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
    
    public partial class DP_ContractEventsMailLog
    {
        public int Id { get; set; }
        public Nullable<int> ContractEventId { get; set; }
        public Nullable<System.DateTime> LastMailSentDate { get; set; }
        public Nullable<int> MailDuration { get; set; }
        public Nullable<int> MailSentCount { get; set; }
        public Nullable<System.DateTime> NextMailSendingDate { get; set; }
        public string MailSentTo { get; set; }
    }
}
