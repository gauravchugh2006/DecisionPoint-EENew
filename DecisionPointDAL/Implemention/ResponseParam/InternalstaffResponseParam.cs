
using System;
namespace DecisionPointDAL.Implemention.ResponseParam
{
    //*****************************************************************************************************************************************
    //   Class used for set the document details
    //*****************************************************************************************************************************************

    public class InternalstaffResponseParam
    {
        /// <summary>
        /// get & set id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string fname { get; set; }
        /// <summary>
        /// get & set name
        /// </summary>
        public string lname { get; set; }
        /// <summary>
        /// get & set email id
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string role { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// get & set role
        /// </summary>
        public string zipcode { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// get & set phone
        /// </summary>
        public string permission { get; set; }
        /// <summary>
        /// get & set isactive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// get & set businessName
        /// </summary>
        public string businessName { get; set; }
        /// <summary>
        /// get & set businessName
        /// </summary>
        public bool invitationStatus { get; set; }
        /// <summary>
        /// get & set companyId
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// get & set User type
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        /// get & set last invitation mail date
        /// </summary>
        public DateTime? LastInviteMailDate { get; set; }
    }
}
