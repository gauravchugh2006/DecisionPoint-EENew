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
    
    public partial class Dp_CommAsstAtempts
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> Attempts { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}