
namespace DecisionPointBAL.Implementation.Request
{
    public class UserRegisterRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

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

        //public IList<RoleResponse> RoleList { get; set; }

        //public IList<SecurityQuestionResponse> SecurityQuestion { get; set; }

        public string Password { get; set; }

        public string StaffFileLocation { get; set; }

        public string fax { get; set; }

        public int RoleId { get; set; }
        //public IList<> StateList{ get; set; }

        //Added new fields by Ajay Shukla

        public string SecurityAnswer1 { set; get; }

        public string SecurityAnswer2 { set; get; }

        public string SecurityAnswer3 { set; get; }
    }
}
