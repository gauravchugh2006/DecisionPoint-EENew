
using System;
namespace DecisionPoint.Models
{
    public class Companies
    {
        public string CompanyID { get; set; }
        public string BusinessName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public int UserID { get; set; }
        public string UserType { get; set; }
        public string VendorType { get; set; }
        public string Phone { get; set; }
        public int TableId { get; set; }
        public int PaymentType { get; set; }
        public bool InvitationStatus { get; set; }
        public DateTime? InvitationDate { get; set; }
    }
}