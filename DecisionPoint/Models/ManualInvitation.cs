
namespace DecisionPoint.Models
{
    public class ManualInvitation
    {
        public string CompanyName { get; set; }
        public string CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool CompanyWorkingForMe { get; set; }
        public bool CompanyNotWorkingForMe { get; set; }
        public bool VendorInvitation { get; set; }
        public bool UserInvitation { get; set; }
    }
}