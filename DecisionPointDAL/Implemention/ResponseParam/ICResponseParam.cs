
using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class ICResponseParam
    {
        /// <summary>
        /// get & set id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// get & set Fname
        /// </summary>
        public string Fname { get; set; }
        /// <summary>
        /// get & set Mname
        /// </summary>
        public string Mname { get; set; }
        /// <summary>
        /// get & set Lname
        /// </summary>
        public string Lname { get; set; }
        /// <summary>
        /// get & set email id
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// get & set Service
        /// </summary>
        public string Service { get; set; }
        /// <summary>
        /// get & set Zipcode
        /// </summary>
        public string Zipcode { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// get & set isactive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// get & set businessName
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// get & set companyid
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set InvitationStatus
        /// </summary>
        public bool InvitationStatus { get; set; }
        /// <summary>
        /// get & set VTId
        /// </summary>
        public int VTId { get; set; }
        /// <summary>
        /// get & set VType
        /// </summary>
        public string VType { get; set; }
        /// <summary>
        /// get & set Last Invitation Mail Date
        /// </summary>
        public DateTime? LastInviteMailDate { get; set; }
        public string BGStatus { get; set; }
        public string Source { get; set; }
        public string SterlingOrderId { get; set; }
        public bool IsRegistered { get; set; }
        
        public int? ICUserId { get; set; }
        public string ICCompanyId { get; set; }

    }
}
