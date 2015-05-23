using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class RegisterStep1
    {
        /// <summary>
        /// first name
        /// </summary>
       public string FirstName { get; set; }

        /// <summary>
        /// middle initials
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// get & set Company Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// user type
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// email id
        /// </summary>        
        public string EmailId { get; set; }

        /// <summary>
        /// business name
        /// </summary>
         //[Required(ErrorMessage = "Business Name Required")]
        [RegularExpression("^[._&,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string BusinessName { get; set; }

        /// <summary>
        /// office phone part 1
        /// </summary> 
         [Required(ErrorMessage = "Office Phone Required.")] 
        public string OfficePhone1 { get; set; }
        /// <summary>
        /// office phone part 2
        /// </summary> 
      [Required(ErrorMessage = "Office Phone Required.")] 
        public string OfficePhone2 { get; set; }
        /// <summary>
        /// office phone part 3
        /// </summary>
        [Required(ErrorMessage = "Office Phone Required.")] 
        public string OfficePhone3 { get; set; }

        /// <summary>
        /// cell area code
        /// </summary>
        public string CellAreaCode { get; set; }
        /// <summary>
        /// cell number
        /// </summary>
        public string CellNumber { get; set; }
        /// <summary>
        /// security Id
        /// </summary>
        public int SecurityId { get; set; }
        /// <summary>
        /// answer
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// business address
        /// </summary>
        public string BusinessAddress { get; set; }
        /// <summary>
        /// direction
        /// </summary>
        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string Direction { get; set; }
        /// <summary>
        /// street name
        /// </summary>
         [RegularExpression("^[_&.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string StreetName { get; set; }
        /// <summary>
        /// city name
        /// </summary>
        public string City { get; set; }

        public int? CityId { get; set; }

        public int? StateId { get; set; }

        public string ZipCode { get; set; }

        public string CompanyLogo { get; set; }

        [Required(ErrorMessage = "Password Required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required.")]
        public string ConfirmPassword { get; set; }

        public string fax1 { get; set; }
        public string fax2 { get; set; }
        public string fax3 { get; set; }

        [Required(ErrorMessage="Email Id Required.")]        
        public string Email { get; set; }

        public int RoleId { get; set; }
        public IEnumerable<StateRespone> StateList { get; set; }
        public IEnumerable<CityResponse> CityList { get; set; }

        public IList<SecurityQuestionResponse> SecurityList { get; set; }

        public string ConfirmEmail { get; set; }

        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }

        public string CompanyName { get; set; }
        public string StateName { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string CityName { get; set; }

        public int? SecurityQuestion1 { get; set; }
        public int? SecurityQuestion2 { get; set; }
        public int? SecurityQuestion3 { get; set; }

         [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]
        public string StreetNumber { get; set; }

        public string RedirectType { get; set; }
        public string PaymentType { get; set; }
        public List<BusinessClassResponse> BusinessClassDetails { get; set; }
        public string CertifyingAgency { get; set; }
        public string CerificationNumber { get; set; }
        public string BusinessClass { get; set; }
        public string FlowType { get; set; }
        public DateTime? CertificateExpDate { get; set; }
        
    }
}