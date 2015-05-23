
using System;
namespace DecisionPointBAL.Implementation.Response
{
   public class CompanyProfileResponse
    {
        public int UserId { get; set; }

        public string BusinessName { get; set; }
        public string OfficePhone { get; set; }
        public string BusinessAddress { get; set; }
        public string Direction { get; set; }

        public string StreetName { get; set; }
        public int? CityId { get; set; }

        public int? StateId { get; set; }
        public string ZipCode { get; set; }
        public string fax { get; set; }
        public string Email { get; set; }

        public string SecurityAnswer1 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string SecurityAnswer3 { get; set; }
        public int? SecurityQuestion1 { get; set; }
        public int? SecurityQuestion2 { get; set; }
        public int? SecurityQuestion3 { get; set; }
        public string StreetNumber { get; set; }
        public string CityName { get; set; }
        public string CompanyLogo { get; set; }
        public string Password { get; set; }
        public string CertifyingAgency { get; set; }
        public string CerificationNumber { get; set; }
        public string BusinessClass { get; set; }
        public string FlowType { get; set; }
        public DateTime? CertificateExpDate { get; set; }
    }
}
