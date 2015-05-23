
namespace DecisionPointMailMatixBAL.Request
{
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
        /// gets or sets EmailSmtpServerSSL 
        /// </summary>
        public bool EmailSmtpServerSSL { get; set; }

        /// <summary>
        /// gets or sets EmailSmtpServer 
        /// </summary>
        public string EmailSmtpServer { get; set; }
        /// <summary>
        /// gets or sets PasswordSmtpServer 
        /// </summary>
        public string PasswordSmtpServer { get; set; }

        public string EmailPassword { get; set; }
    }
}
