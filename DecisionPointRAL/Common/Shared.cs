

// ******************************************************************************************************************************
//                                                 class:Shared.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 26, 2014     |Sumit Saurav            | This class holds the interaction logic for Shared.cs
// ******************************************************************************************************************************

using System;
using System.ComponentModel;
using System.Reflection;
namespace DecisionPointCAL.Common
{
    public static class Shared
    {
        #region << Public Variables >>

        public static string IC = "IC";
        public static string Individual = "Individual";
        public static string Company = "Company";
        public static string SuperAdmin = "SuperAdmin";
        public static string Staff = "staff";
        public static string Sent = "sent";
        public static string Read = "read";
        public static string Unread = "unread";
        public static string history = "history";
        public static string Document = "document";
        public static string Profile = "profile";
        public static string CompanyProfile = "companyprofile";
        public static string Service = "service";
        public static string Client = "Client";
        public static string Password = "password";
        public static string StaffEdit = "staffedit";
        public static string All = "All";
        public static string GetCompanyid = "getcompanyid";
        public static string VendorWithoutId = "vendorwithoutid";
        public static string VendorWithId = "vendorwithid";
        public static string IcWithId = "icwithid";
        public static string IcWithoutId = "icwithoutid";
        public static string User = "User";
        public static string Update = "update";
        public static string Apply = "Apply";
        public static string DoesNotApply = "Does Not Apply";
        public static string Admin = "Admin";
        public static string Comm = "comm";
        public static string Recipient = "recipient";
        public static string Vendor = "Vendor";
        public static string DdVendor = "ddvendor";
        public static string DdClient = "ddclient";
        public static string Completed = "Completed";
        public static string None = "none";
        public static string Location = "Location";
        public static string Pass = "Pass";
        public static string Fail = "Fail";
        public static string NotCompleted = "notcompleted";
        public static string UserView = "userview";
        public static string DenyO = "denyO";
        public static string AccecptedO = "accecptedO";
        public static string ReceivedO = "receivedO";
        public static string PendingO = "pendingO";
        public static string DenyI = "denyI";
        public static string AccecptedI = "accecptedI";
        public static string ReceivedI = "receivedI";
        public static string DocumentDetail = "documentdetail";
        public static string Links = "links";
        public static string Content = "content";
        public static string Reqaction = "reqaction";
        public static string TestRule = "testrule";
        public static string Ques = "ques";
        public static string Ans = "ans";
        public static string Save = "save";
        public static string Edit = "edit";
        public static string Jobreqdoc = "jobreqdoc";
        public static string DNA = "DNA";
        public static string MyCommunication = "mycommunication";
        public static string StaffCommunication = "staffcommunication";
        public static string Incomming = "incomming";
        public static string DefaultSignature = "<!DOCTYPE html><html><head></head><body><hr /><p>Disclaimer:-This message may contain confidential and/or privileged information.If you have received this message in error,please notify us immediately by responding to this e-mail and then delete it from your system.Thanks for your cooperation.</p></body></html>";
        public static string Both = "both";
        public static string SendTo = "Send To";
        public static string Forward = "forward";
        public static string Yes = "Yes";
        public static string No = "No";
        public static string SingleSpace = " ";
        public static string Comma = ",";
        public static string SystemDown = "System is temporarily down. Please contact your admin.";
        public static string New = "new";
        public static string BusinessManager = "Business Manager";
        public static string ComplianceManager = "Compliance Manager";
        public static string TrueStr = "true";
        public static string SingleDash = "-";
        public static string RightParanThesis = "(";
        public static string LeftParanThesis = ")";
        public static string UnderScore = "_";
        public static string One = "1";
        public static string Two = "2";
        public static string Three = "3";
        public static string Day = "Day";
        public static string Month = "Month";
        public static string Year = "Year";
        public static string DollarSign = "$";
        public static string Astrik = "*";
        public static string Colon = ":";
        public static string Category = "Category";
        public static string SubCategory = "Sub Category";
        public static string Zero = "0";
        public static string SemiColon = ";";
        public static string Hash = "#";
        public static string ExclamationMark = "!";
        public static string IReceived = "IReceived";
        public static string OPending = "OPending";
        public static string IOAccepted = "IOAccepted";
        public static string IDenied = "IDenied";
        public static string CsvFileExt = ".CSV";
        public static string SplitSignature = "body>";
        public static string Clients = "Clients";
        public static string BusinessType = "Business Type";
        public static string State = "state";
        public static string County = "county";
        public static string City = "city";
        public static string Zip = "zip";
        public static string Clone = "clone";
        public static string IncomingUpdate = "incomingupdate";
        public static string Type = "type";
        public static string Title = "title";
        public static string ProfessionalType = "professionalType";
        public static string Enable = "enable";
        public static string Communication = "Communication";
        public static string MyPartner = "My Partners";
        public static string MyContracts = "My Contracts";
        public static string Invite = "Invite";
        public static string AddVendorClient = "Add Vendor/Client";
        public static string AddBulkVendorClient = "Bulk Vendor/Client Upload";
        public static string AddIC = "Add IC";
        public static string AddBulkIC = "Bulk IC Upload";
        public static string ManageIC = "Manage IC";
        public static string ManageVendor = "Manage Vendor";
        public static string ManageClient = "Manage Client";
        public static string ManageServices = "Manage Services";
        public static string Administration = "Administration";
        public static string Contract = "Contract";
        public static string MyLibrary = "My Library";
        public static string MyCalender = "My Calender";
        public static string ICVendorClient = "IC/Vendor/Client";
        public static string ServiceTranslationTable = "Service Translation Table";
        public static string Pending = "Pending";
        public static string NonMember = "Non-Member";
        public static string Member = "Member";
        public static string Staging = "Staging";
        public static string Inbox = "Inbox";
        public static string ManageStaff = "Manage Staff";
        public static string AddStaff = "Add Staff";
        public static string ManageStaffTitles = "Manage Staff Titles";
        public static string Executed = "Executed";
        public static string Expired = "Expired";
        public static string Alerts = "Alerts";
        public static string NonClient = "Guest";
        public static string XmlFileExt = ".xml";
        public static string OK = "Ok";
        public static string Error = "Error";
        public static string Consider = "Consider";
        public static string Review = "Review";
        public static string Accepted = "Accepted";
        public static string Sterling = "Sterling";
        public static string InProgress = "In Progress";
        public static string Insurance = "Insurance";
        public static string License = "License";
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
    public enum APIResponseStatusCode : int
    {
        Success = 200,
        Subclientnotfound = 201,
        Clientnotfound = 202,
        Productnotfoundforclient = 203,
        Sent = 204,
        Resent = 205,
        EmailIdExist = 206,
        Approved = 207,
        DoNotUse = 208,
        Unreviewed = 209,
        Unregistered = 210,
        MissingRequirements = 211,
        Unreleased = 212,
        Fail = 213,
        Exception = 214,
        AlreadyRegistered = 215,
        Unauthorized = 216,
        AccessCredentailRequired = 217,
        ReferenceIdAlreadyExist = 218,
        RefrenceIdRequired = 219,
        NOJCR = 220
    }

    public class MailInviteFormat
    {
        public string PersonName { get; set; }
        public string PersonCompanyName { get; set; }
        public string PersonUserType { get; set; }
        public string EmailId { get; set; }
        public string Passwrod { get; set; }
        public string Signature { get; set; }
        public string DomainUrl { get; set; }
        public string InviteeUserId { get; set; }
        public string InviteeCompanyName { get; set; }
        public string Type { get; set; }
        public string InviteePersonName { get; set; }
        public string UserType { get; set; }
        public string FilePath { get; set; }
        public string DueDate { get; set; }
        public string Action { get; set; }
        public string Category { get; set; }
    }

}
