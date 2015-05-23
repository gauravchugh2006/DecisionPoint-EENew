
using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class GetCompaniesParam
    {
        public string BusniessID { get; set; }
        public string BusniessName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public string VendorType { get; set; }
        public string UserType { get; set; }
        public int Tableid { get; set; }
        public string Phone { get; set; }
        public int PaymentType { get; set; }
        public bool InvitationStatus { get; set; }
        public DateTime? InvitationDate { get; set; }
    }
}
