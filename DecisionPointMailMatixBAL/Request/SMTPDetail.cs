
namespace DecisionPointMailMatixBAL.Request
{
    public class SMTPDetail
    {
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
        /// gets or sets EmailSmtpServer 
        /// </summary>
        public string EmailSmtpServer { get; set; }
        /// <summary>
        /// gets or sets PasswordSmtpServer 
        /// </summary>
        public string PasswordSmtpServer { get; set; }
    }
}
