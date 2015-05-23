
namespace DecisionPointDAL.Implemention.RequestParam
{
    public class AdminProfileRequestParam
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string LastName { get; set; }

        public string NickName { get; set; }
       
        public string CellNumber { get; set; }
        
        
        public string Password { get; set; }

        public string Email { get; set; }

        public string ConfirmEmail { get; set; }

        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }       

        public int? SecurityQuestion1 { get; set; }
        public int? SecurityQuestion2 { get; set; }
        public int? SecurityQuestion3 { get; set; }
        public string OfficePhone { get; set; }
        public string CompanyId { get; set; }   
    }
}
