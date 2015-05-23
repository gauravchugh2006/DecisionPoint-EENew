// ********************************************************************************************************************************
//                                                  class:BusinessCore
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// October 18, 2013    |Arun Kumar     | This is multi purpose class basically used for emailing, date format and password creation etc.
// *********************************************************************************************************************************


using System;
using System.Net.Mail;
namespace DecisionPoint.Core
{
    /// <summary>
    ///  October 17, 2013    |Arun Kumar     | This is multi purpose class basically used for emailing, date format and password creation etc.
    /// </summary>
    #region<<Business core class for creating new password and send through Email>>
    public class BusinessCore
    {

        /// <summary>
        /// Method to Create new password
        /// </summary>
        /// <returns></returns>
        public string CreatePassword(string customerEmail)
        {
            ////CustomerRepository customerRepository = new CustomerRepository();
            ////return customerRepository.GetCustomerPassword(customerEmail);
            //int length = 8;
            //string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            //string res = "";
            //Random rnd = new Random();
            //while (0 < length--)
            //    res += valid[rnd.Next(valid.Length)];
            //return res;
            return "";
        }

        /// <summary>
        /// method to send new password through Email
        /// </summary>
        /// <param name="businessEmail"></param>
        public void SendMail(BusinessEmail businessEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //add smtp server host name or ip
                SmtpClient SmtpServer = new SmtpClient(businessEmail.EmailSmtpServerHost);
                //email addess from where mail is sending
                mail.From = new MailAddress(businessEmail.EmailFrom);
                //email address where mail will send
                mail.To.Add(businessEmail.EmailTo);
                //Subject of mail
                mail.Subject = businessEmail.EmailSubject;
                //body of mail message
                mail.Body = businessEmail.EmailBody;
                mail.IsBodyHtml = true;
                //smtp server port
                SmtpServer.Port = businessEmail.EmailSmtpServerPort;
                //Email credential
                SmtpServer.Credentials = new System.Net.NetworkCredential(businessEmail.EmailFrom, businessEmail.EmailPassword);
                SmtpServer.EnableSsl = businessEmail.EmailSmtpServerSSL;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        /// <summary>
        /// Method to set the format of date
        ///  </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string SetDateFormat(DateTime dt)
        {
            try
            {
                return (dt).ToString("MM/dd/yyyy");
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        /// <summary>
        /// Method for set time format
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string SetTimeFormat(DateTime dt)
        {
            try
            {
                return (dt).ToString("t");
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        /// <summary>
        /// Method for changing phone number format
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public string ChangePhoneNumberFormat(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber) && phoneNumber.Length >= 10)
            {
                return string.Format("({0}) {1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6, 4));
            }
            else
            {
                return "";
            }
        }
    }
    #endregion
}