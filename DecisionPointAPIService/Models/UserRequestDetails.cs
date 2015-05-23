using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPointAPIService.Models
{
   public class UserRequestDetails
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string RoleType { get; set; }

        public string ClientId { get; set; }

        public int PackageId { get; set; }

        public string SubClientId { get; set; }

        public int ProductId { get; set; }

        public int RoleTypeId { get; set; }

        public int TitleId { get; set; }

        public int DocFlowId { get; set; }

        public int PaymentTypeId { get; set; }

        public int ICTypeId { get; set; }

        public string BusinessName { get; set; }

        public string ApiUserName { get; set; }

        public string ApiPassword { get; set; }

        public string ReferenceId { get; set; }
        public string StaffId { get; set; }

        public string EntityId { get; set; }
    }
   public class APICredenrials
   {
       public string ApiUserName { get; set; }

       public string ApiPassword { get; set; }
       public string ClientId { get; set; }

   }
}