
using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{

    public class VendorClientList
    {
        /// <summary>
        /// get & set id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// get & set email id
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string Vendor { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// get & set stateAbbre
        /// </summary>
        public string stateAbbre { get; set; }
        /// <summary>
        /// get & set zipCode
        /// </summary>
        public string zipCode { get; set; }
        public string companyId { get; set; }
        public string DocFlow { get; set; }
        public int DocFTblId { get; set; }

        /// <summary>
        /// get & set businessName
        /// </summary>
        public bool invitationStatus { get; set; }
        /// <summary>
        /// get & set LastInviteMailDate
        /// </summary>
        public DateTime? LastInviteMailDate { get; set; }
        /// <summary>
        /// get & set User Type
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// get & set Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// get & set VendorType
        /// </summary>
        public string VendorType { get; set; }
        public int VendorTypeId { get; set; }


        public bool? IsNonMember { get; set; }

       
    }
}
