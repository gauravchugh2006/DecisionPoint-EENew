
namespace DecisionPointDAL.Implemention.RequestParam
{
  public   class VendorBulk
    {
        public string FName { get; set; }
      
        public string LName { get; set; }

        public string EmailId { get; set; }

        public string CompanyId { get; set; }
      
        public string CompanyName { get; set; }
    
        public int FlowId { get; set; }
        public string FlowValue { get; set; }
        public int DocFlowId { get; set; }
        public string DocFlowValue { get; set; }
        public int PaymentId { get; set; }
        public string Paymenttype { get; set; }
    }
}
