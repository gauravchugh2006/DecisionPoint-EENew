using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    /// <summary>
    /// Model for user Registration
    /// </summary>
    public class UserRegister
    {
        [Required(ErrorMessage = "First Name Required")]        
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]       
        public string LastName { get; set; }

        public string UserType { get; set; }

        
        public string EmailId { get; set; }

        public string BusinessName { get; set; }

        public string OfficePhone { get; set; }      

        public string CellAreaCode { get; set; }

        public string CellNumber { get; set; }

        public int SecurityId { get; set; }

        public string Answer { get; set; }

        public string BusinessAddress { get; set; }

        public string Direction { get; set; }

        public string StreetName { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }

        public string ZipCode { get; set; }

        public string CoverageArea { get; set; }

        public string NoOfEmployees { get; set; }

        public string CompanyLogo { get; set; }

        public IList<RoleResponse> RoleList { get; set; }

        public IList<SecurityQuestionResponse> SecurityQuestion { get; set; }

        public string Password { get; set; }

        public string StaffFileLocation { get; set; }

        public string fax { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]      
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        public int RoleId { get; set; }
        //public IList<> StateList{ get; set; }

        public string ConfirmEmail { get; set; }

        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }

        [Required(ErrorMessage = "Company Name Required")]
        public string CompanyName { get; set; }
        public string StateName { get; set; }
    }
}