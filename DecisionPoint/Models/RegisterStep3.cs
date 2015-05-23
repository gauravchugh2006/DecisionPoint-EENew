using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class RegisterStep3
    {
        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string FirstName { get; set; }

        [RegularExpression("^[A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string MiddleName { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string LastName { get; set; }
        public string NickName { get; set; }

        public string UserType { get; set; }
               
        public string EmailId { get; set; }

        public string BusinessName { get; set; }

        public string OfficePhone1 { get; set; }
        public string OfficePhone2 { get; set; }
        public string OfficePhone3 { get; set; }

        public string CellAreaCode { get; set; }

        public string CellNumber1 { get; set; }
        public string CellNumber2 { get; set; }
        public string CellNumber3 { get; set; }

        public int SecurityId { get; set; }

        public string Answer { get; set; }

        public string BusinessAddress { get; set; }

        public string Direction { get; set; }

        public string StreetName { get; set; }

        public string City { get; set; }

        public int StateId { get; set; }

        public string ZipCode { get; set; }

        public string CompanyLogo { get; set; }

        [Required(ErrorMessage = "Password Required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required.")]
        public string ConfirmPassword { get; set; }

        public string fax { get; set; }

         [Required(ErrorMessage = "Email Id Required.")]
        public string Email { get; set; }

        public int RoleId { get; set; }
        public IEnumerable<StateRespone> StateList { get; set; }
        public IEnumerable<CityResponse> CityList { get; set; }
        public IList<SecurityQuestionResponse> SecurityList { get; set; }
       
        public string ConfirmEmail { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]       
        public string SecurityAnswer1 { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]     
         public string SecurityAnswer2 { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]      
        public string SecurityAnswer3 { get; set; }

        public string CompanyName { get; set; }
        public string StateName { get; set; }

        public int? SecurityQuestion1 { get; set; }
        public int? SecurityQuestion2 { get; set; }
        public int? SecurityQuestion3 { get; set; }
                
    }
}