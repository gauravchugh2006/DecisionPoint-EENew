
namespace DecisionPoint.Models
{
    public class InternalStaffModel
    {
        public int id { get; set; }

       // public int roleid { get; set; }

        public int titleid { get; set; }

        public int permissionid { get; set; }

        public string fName { get; set; }

        public string lName { get; set; }

        public string phone { get; set; }

        public string emailId { get; set; }

        public string UserType { get; set; }

        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int FlowId { get; set; }
        public int FlowTblId { get; set; }
        public int UserId { get; set; }
    }
}