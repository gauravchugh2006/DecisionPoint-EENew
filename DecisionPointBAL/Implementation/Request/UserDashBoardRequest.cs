
using System;
namespace DecisionPointBAL.Implementation.Request
{
   public class UserDashBoardRequest
    {
        #region MessageReply
        /// <summary>
        /// Messages ReplyFrom
        /// </summary>
        public int ReplyFrom { get; set; }
        /// <summary>
        /// Messages ReplyTo
        /// </summary>
        public int ReplyTo { get; set; }
        /// <summary>
        /// Messages ReplyBody
        /// </summary>
        public string ReplyBody { get; set; }
        /// <summary>
        /// Messages ReplyMsgId
        /// </summary>
        public int ReplyMsgId { get; set; }
       /// <summary>
        /// Messages recipient id
        /// </summary>
        public int Recipientid { get; set; }
       
        #endregion

        #region My Profile
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string LName { get; set; }
        /// <summary>
        /// Used for NickName of user/company
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? UserId { get; set; }
        public int FlowId { get; set; }
        public int FlowTblId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? RoleId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? TitleId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int? PermissionId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string  ServiceId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string DirectPhone { get; set; }
        /// <summary>
        /// get & set profilephoto
        /// </summary>
        public string ProfilePhoto { get; set; }
        /// <summary>
        /// get & set profilephoto
        /// </summary>
        public string NewPwd { get; set; }
       /// <summary>
       /// get & set modified by
       /// </summary>
        public int ModifiedBy { get; set; }
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

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string CertifyingAgency { get; set; }
        public string CerificationNumber { get; set; }
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
