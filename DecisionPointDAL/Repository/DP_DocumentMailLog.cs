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
    
    public partial class DP_DocumentMailLog
    {
        public int Id { get; set; }
        public Nullable<int> RecevierId { get; set; }
        public Nullable<int> SenderId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> RecipientTblId { get; set; }
        public Nullable<bool> IsmailSend { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}