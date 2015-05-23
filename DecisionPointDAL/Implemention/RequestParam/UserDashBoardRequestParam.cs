
using System;
namespace DecisionPointDAL.Implemention.RequestParam
{
    //*****************************************************************************************************************************************
    //   Class used for set properties used in user dashboard section
    //*****************************************************************************************************************************************
  public  class UserDashBoardRequestParam
    {
        #region MessageReply
        /// <summary>
        /// set ReplyFrom
        /// </summary>
        public int ReplyFrom { get; set; }
        /// <summary>
        /// set ReplyTo
        /// </summary>
        public int ReplyTo { get; set; }
        /// <summary>
        /// set ReplyBody
        /// </summary>
        public string ReplyBody { get; set; }
        /// <summary>
        /// set ReplyMsgId
        /// </summary>
        public int ReplyMsgId { get; set; }
        /// <summary>
        /// set recipient id
        /// </summary>
        public int recipientid { get; set; }
        #endregion

        #region My Profile
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string fName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string mName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string lName { get; set; }
        /// <summary>
        /// Used for NickName of user/company
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string emailId { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? UserId { get; set; }
        public int FlowId { get; set; }
        public int FlowTblId { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string officePhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string directPhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string profilephoto { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? roleId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? titleId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? permissionId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string serviceId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string clientId { get; set; }
        /// <summary>
        /// get & set profilephoto
        /// </summary>
        public string newpwd { get; set; }
        /// <summary>
        /// get & set modifiedby
        /// </summary>
        public int modifiedby { get; set; }
        public string StreetNumber { get; set; }
        /// <summary>
        /// direction
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// street name
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// get & set City Name
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// get & set State Name
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// Get & set Zip Code
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Get & set Type
        /// </summary>
        public int Type { get; set; }
        public string CertifyingAgency { get; set; }
        public string CertificationNumber { get; set; }
        public string BusinessClass { get; set; }
        public DateTime? CertificateExpDate { get; set; }
        #endregion

        #region payment detail
        public string CompanyCode { get; set; }
        public double? CompanyFee { get; set; }
        public double? PerFieldStaffFee { get; set; }
        public double? PerOfficeStaffFee { get; set; }
        public double? PerICFee { get; set; }
        public bool IsInvoice { get; set; }

        #endregion
    }
}
