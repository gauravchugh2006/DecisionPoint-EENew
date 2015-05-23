
namespace DecisionPointBAL.Implementation.Response
{
    public class PaymentAmountResponse
    {
        public int Id { get; set; }
        public string CompanyCode { get; set; }
        public double? CompanyFee { get; set; }
        public double? PerFieldStaffFee { get; set; }
        public double? PerOfficeStaffFee { get; set; }
        public double? PerIcFee { get; set; }
        public bool? IsInvoice { get; set; }

        public int NoOfPartners { get; set; }
        public int NoOfStaff { get; set; }
        public int NoOfIc { get; set; }
        public string CardType { get; set; }
        public string CardHolderName { get; set; }
        public string BusinessName { get; set; }
        public string TransactionMessage { get; set; }
        public string PayerEmailId { get; set; }
        public string ReceiverEmailId { get; set; }
        public string PaymentDate { get; set; }
        public string Currency { get; set; }
        public double PayAmount { get; set; }
        
        public string TransactionCode { get; set; }
        public string TransactionType { get; set; }
        public int userId { get; set; }
        public int InviteeCompanyId { get; set; }

        public string Result { get; set; }
    }
}
