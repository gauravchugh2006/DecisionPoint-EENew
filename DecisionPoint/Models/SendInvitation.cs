
namespace DecisionPoint.Models
{
    public class SendInvitation
    {
        public string firstName { get; set; }
       
        public string lastName { get; set; }
        
        public int titleId { get; set; }
        
        public int roleId { get; set; }
       
        public int permissionId { get; set; }
       
        public string emailId { get; set; }
       
        public string companyId { get; set; }
       
        public string companyName { get; set; }
        
        public int flowId { get; set; }
       
        public int vendorTypeId { get; set; }
        
        public int docflowId { get; set; }
       
        public string type { get; set; }
       
        public string PaymentType { get; set; }
        /// <summary>
        /// Used for get the IsBackgroundCheck
        /// </summary>
        public bool IsBackgroundCheck { get; set; }

        public bool IsMailSent { get; set; }
        public bool AllowToView { get; set; }
    }
}