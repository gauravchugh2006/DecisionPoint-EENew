
namespace DecisionPointBAL.Implementation.Request
{
    public class PaymentResponse
    {
       
        public string BusinessName { get; set; }

        public string TransactionType { get; set; }

        public string AnnualTransaction { get; set; }

        
        public string CreditCardType { get; set; }
       
        public string ExpiryMonth { get; set; }
                
        public string ExpiryYear { get; set; }
        
        public string CardCode { get; set; }
               
        public string CardNumber { get; set; }
                
        public string NameOnCard { get; set; }

        public string Address { get; set; }
               
        public int? StateId { get; set; }
               
        public int? CityId { get; set; }

        public string Zip { get; set; }
       
        public bool IsSameAddress { get; set; }
        public string Directions { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }

        public int NoOfEmployee { get; set; }
        public decimal Amount { get; set; }
        public int? CompanyId { get; set; }
        public double? CompanyFee { get; set; }
        public bool? IsInvoice { get; set; }
        public string CompanyCode { get; set; }
        public string CVC { get; set; }
        public string CustomerEmail { get; set; }
        public int UserId { get; set; }
        public bool isMonthlyPaymentDone { get; set; }
        public bool isAnnualPaymentDone { get; set; }
    }
}
