using System;
using System.Collections.Generic;

namespace DecisionPointBAL.Implementation.Response
{
    public class UserDashBoardResponse
    {
        #region MessagesDetails
        /// <summary>
        /// Messages Sent Date
        /// </summary>
        public DateTime? msgDate { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string msgTitle { get; set; }
        /// <summary>
        /// Messages Send By
        /// </summary>
        public string msgFrom { get; set; }
        /// <summary>
        /// Used for display message id
        /// </summary>
        public int msgId { get; set; }
        /// <summary>
        /// Used for get user details 
        /// </summary>
        public string emailIdTo { get; set; }
        /// <summary>
        /// Used for put user id
        /// </summary>
        public int deliveredUserId { get; set; }
        /// <summary>
        /// Used for put user id
        /// </summary>
        public int recipientUserId { get; set; }
        /// <summary>
        /// Used for message status
        /// </summary>
        public string messageStatus { get; set; }
        /// Used for display unique id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Used for message body
        /// </summary>
        public string messageBody { get; set; }
        #endregion

        #region DocumentsDetails
        /// <summary>
        /// Messages Sent Date
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// Messages Send By
        /// </summary>
        public string DocTitle { get; set; }
        /// <summary>
        /// Document sended By
        /// </summary>
        public string Docfrom { get; set; }
        /// <summary>
        /// Used for display message id
        /// </summary>
        public int DocId { get; set; }
        public bool reqnewhire { get; set; }
        public string retake { get; set; }
        public string introduction { get; set; }
        public string CreatorCompanyid { get; set; }
        public bool IsCompliance { get; set; }
        #endregion

        #region History
        /// <summary>
        /// Messages/document/courses Type
        /// </summary>
        public string reqDocType { get; set; }
        /// <summary>
        /// Messages/document/courses Title
        /// </summary>
        public string title { get; set; }
        public int? CreatedBy { get; set; }
        /// <summary>
        /// Messages/document/courses from
        /// </summary>
        public string commFromPersonName { get; set; }
        /// <summary>
        /// Messages/document/courses from [company name]
        /// </summary>
        public string commFromComapnyName { get; set; }
        /// <summary>
        /// Messages/document/courses status
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// Messages/document/courses receviedDate
        /// </summary>
        public DateTime? receviedDate { get; set; }
        /// <summary>
        /// Messages/document/courses receviedDate
        /// </summary>
        public DateTime? completeDate { get; set; }
        /// <summary>
        /// Messages/document/courses receviedDate
        /// </summary>
        public DateTime? effectiveDate { get; set; }
        /// <summary>
        /// Messages/document/courses archiveDate
        /// </summary>
        public DateTime? archiveDate { get; set; }
        /// <summary>
        /// Messages/document/courses accecpted
        /// </summary>
        public bool? accecpted { get; set; }
        /// <summary>
        /// Messages/document/courses assesmentStatus
        /// </summary>
        public string assesmentStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serachByFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serachByVType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serachByTitle { get; set; }
        /// <summary>
        /// get & set reference
        /// </summary>
        public string reference { get; set; }
        /// <summary>
        /// get & set timespend
        /// </summary>
        public string timeSpend { get; set; }
        /// <summary>
        /// get & set timespend
        /// </summary>
        public string policyNo { get; set; }
        /// <summary>
        /// get & set timespend
        /// </summary>
        public int versionno { get; set; }
        /// <summary>
        /// get & set docid
        /// </summary>
        public int? docTypeId { get; set; }
        /// <summary>
        /// get & set docSeqno
        /// </summary>
        public int? docSeqno { get; set; }
        /// <summary>
        /// get & set docid
        /// </summary>
        public int? tableId { get; set; }
        /// <summary>
        ///get & set hourofcredit
        /// </summary>
        public string hourofcredit { get; set; }
        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfStaff { get; set; }
        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfCompStaff { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfIc { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfCompIc { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfVendor { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfCompVendor { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfDD { get; set; }

        /// <summary>
        /// get & set docid
        /// </summary>
        public float NoOfCompDD { get; set; }
        /// <summary>
        /// get & set MoveInHistory
        /// </summary>
        public bool MoveInHistory { get; set; }
        /// <summary>
        /// get & set Group
        /// </summary>
        public string Group { get; set; }
        public string CertifyingAgency { get; set; }
        public string CertificationNumber { get; set; }
        public string BusinessClass { get; set; }
        public DateTime? CertificateExpDate { get; set; }
        #endregion

        #region Account Profile
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
       /// Used for put NickName
       /// </summary>
       /// 
       public string NickName { get; set; }
       /// <summary>
       /// Used for put UserType
       /// </summary>
       /// 
       public string UserType { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        /// 
        public string emailId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public IList<string> services { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public IList<CoverageArea> coverageArea { get; set; }
        
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
        public string companyName { get; set; }
        /// <summary>
        /// get & set profilephoto
        /// </summary>
        public string profilephoto { get; set; }
        /// <summary>
        /// get & set profilephoto
        /// </summary>
        public string companylogo { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        public string StreetNumber { get; set; }
        /// <summary>
        /// get & set direction
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// get & set street name
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// get & set City Name
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// get & set StateName
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// get & set License Number
        /// </summary>
        public string LicenseNumber { get; set; }
        /// <summary>
        /// get & set Policy Number
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// get & set Stateid
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// get & set ZipmCode
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// get & set free Membership
        /// </summary>

        public bool IsFreeBasicMembership { get; set; }
        /// <summary>
        /// get & set BioInfo
        /// </summary>
        public string BioInfo { get; set; }
        /// <summary>
        /// get & set CoverageAreaStatus
        /// </summary>
        public string CoverageAreaStatus { get; set; }
        /// <summary>
        /// get & set ServicesStatus
        /// </summary>
        public int ServicesStatus { get; set; }
        /// <summary>
        /// get & set RegisteredDate
        /// </summary>
        public DateTime? RegisteredDate { get; set; }
        #endregion

        #region Reqiured Documents
        /// <summary>
        /// Used for documentName of user/company
        /// </summary>
        public string reqiuredDoctName { get; set; }
        /// <summary>
        /// Used for id of reqiured document by user/company
        /// </summary>
        public int reqiuredDocId { get; set; }
        /// <summary>
        /// Used for id of reqiured document by user/company
        /// </summary>
        public string  reqiuredDocLoc { get; set; }
        /// <summary>
        /// Used for id of reqiured document by user/company
        /// </summary>
        public DateTime?  expirationDate { get; set; }
        /// <summary>
        /// to check global or specific
        /// </summary>
        public int ReqType { get; set; }
        /// <summary>
        /// to check status
        /// </summary>
        public int IsCompleted { get; set; }
        /// <summary>
        /// to check IsElectronically
        /// </summary>
        public bool IsElectronically { get; set; }
        public DateTime LicCreatedDate { get; set; }
         public int ServiceId { get; set; }

        public string ServiceName { get; set; }
        #endregion

        #region ReplyMessage
        /// <summary>
        /// Used for get reply body of message
        /// </summary>
        public string replyBody { get; set; }
        /// <summary>
        /// Used for get message recipient person name
        /// </summary>
        public string replyto { get; set; }
        /// <summary>
        /// Used for get message delivered person name
        /// </summary>
        public string replyfrom { get; set; }
        /// <summary>
        /// Used for get date of message reply
        /// </summary>
        public DateTime? replysentdate { get; set; }
        #endregion

        #region payment detail
        public string CompanyCode { get; set; }
        public double? CompanyFee { get; set; }
        public double? PerFieldStaffFee { get; set; }
        public double? PerOfficeStaffFee { get; set; }
        public double? PerICFee { get; set; }
        public bool IsInvoice { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        #endregion

        #region ProfessionalLic

        public int ReqiredById { get; set; }

        public string RequiredByName { get; set; }

        public string LicType { get; set; }

        public decimal SingleOccurance { get; set; }

        public decimal Aggregate { get; set; }

        public string Source { get; set; }
        #endregion
    }     
}

