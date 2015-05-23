using DecisionPointDAL.Implemention;
using DecisionPointMailMatixBAL.Request;
using System.Net.Mail;

namespace DecisionPointMailMatixBAL.Core
{
   public static class BusinessCore
    {
        /// <summary>
        /// get smtp details for mail sending
        /// </summary>
        /// <param name="emailTo">email will send to</param>
        /// <param name="password">password </param>
        /// <param name="body">message body</param>
        public static void GetSmtpDetail(string emailTo, string body, string subject)
        {
            try
            {

                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                // return objdecisionPointRepository.Getsmtpdetails();
                DecisionPointMailMatixBAL.Request.SMTPDetail objSMTPDetail = new DecisionPointMailMatixBAL.Request.SMTPDetail();//objdecisionPointRepository.Getsmtpdetails();

                //Set Mail credentials
                BusinessEmail objBusinessEmail = new BusinessEmail();
                objBusinessEmail.EmailBody = body;
                objBusinessEmail.EmailFrom = objdecisionPointRepository.Getsmtpdetails().EmailSmtpServer;//objSMTPDetail.EmailSmtpServer;
                objBusinessEmail.EmailTo = emailTo;
                objBusinessEmail.EmailSubject = subject;
                objBusinessEmail.EmailSmtpServerHost = objdecisionPointRepository.Getsmtpdetails().EmailSmtpServerHost;
                objBusinessEmail.EmailSmtpServerPort = objdecisionPointRepository.Getsmtpdetails().EmailSmtpServerPort;
                objBusinessEmail.EmailSmtpServerSSL = objdecisionPointRepository.Getsmtpdetails().EmailSmtpServerSSL;
                objBusinessEmail.EmailPassword = objdecisionPointRepository.Getsmtpdetails().PasswordSmtpServer;
                objBusinessEmail.PasswordSmtpServer = objdecisionPointRepository.Getsmtpdetails().PasswordSmtpServer;
                objBusinessEmail.EmailSmtpServer = objdecisionPointRepository.Getsmtpdetails().EmailSmtpServer;
                SendMail(objBusinessEmail);

            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// method to send new password through Email
        /// </summary>
        /// <param name="businessEmail"></param>
        public static void SendMail(BusinessEmail businessEmail)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {

                    using (SmtpClient SmtpServer = new SmtpClient(businessEmail.EmailSmtpServerHost))
                    {
                        mail.From = new MailAddress(businessEmail.EmailFrom);
                        mail.To.Add(businessEmail.EmailTo);
                        mail.Subject = businessEmail.EmailSubject;
                        mail.Body = businessEmail.EmailBody;
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.High;
                        SmtpServer.Port = businessEmail.EmailSmtpServerPort;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(businessEmail.EmailSmtpServer, businessEmail.PasswordSmtpServer);
                        SmtpServer.EnableSsl = businessEmail.EmailSmtpServerSSL;
                        SmtpServer.Send(mail);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
