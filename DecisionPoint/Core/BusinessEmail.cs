// ********************************************************************************************************************************
//                                                  class:BusinessEmail
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 18, 2013    |Arun Kumar     | this class contains the properties for BusinessCore.cs
// *********************************************************************************************************************************


namespace DecisionPoint.Core
{
    #region<<BusinessEmail Class for Email fields like EmailFrom ,EmailTo etc>>
    public class BusinessEmail
    {

        /// <summary>
        /// gets or sets EmailFrom
        /// </summary>
        public string EmailFrom { get; set; }
        /// <summary>
        /// gets or sets EmailTo
        /// </summary>
        public string EmailTo { get; set; }
        /// <summary>
        /// gets or sets EmailSubject
        /// </summary>
        public string EmailSubject { get; set; }
        /// <summary>
        /// gets or sets EmailBody
        /// </summary>
        public string EmailBody { get; set; }
        /// <summary>
        /// gets or sets EmailSmtpServerHost
        /// </summary>
        public string EmailSmtpServerHost { get; set; }
        /// <summary>
        /// gets or sets EmailSmtpServerPort 
        /// </summary>
        public int EmailSmtpServerPort { get; set; }
        /// <summary>
        /// gets or sets EmailSmtpServerCredentials
        /// </summary>
        public string EmailSmtpServerCredentials { get; set; }
        /// <summary>
        /// gets or sets EmailSmtpServerSSL 
        /// </summary>
        public bool EmailSmtpServerSSL { get; set; }
        /// <summary>
        /// gets or sets EmailPassword
        /// </summary>
        public string EmailPassword { get; set; }
        /// <summary>
        /// gets or sets Autopassword
        /// </summary>
        public string AutoPassword { get; set; }
    }
    #endregion
}
