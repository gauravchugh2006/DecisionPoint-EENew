using System;
using System.Net.Mail;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace DecisionPointBAL.Core
{
    #region<<Business core class for creating new password and send through Email>>
    public  class BusinessCore
    {

        /// <summary>
        /// Method to Create new password
        /// </summary>
        /// <returns></returns>
        public static string CreatePassword()
        {
            ////CustomerRepository customerRepository = new CustomerRepository();
            //return customerRepository.GetCustomerPassword(customerEmail);
            int length = 8;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            Thread.Sleep(500);
            while (0 < length--)
            {
                res += valid[rnd.Next(valid.Length)];
            }
             BusinessCryptography objBusinessCryptography = new BusinessCryptography();
             string finalpwd= objBusinessCryptography.base64Encode(res);
             return finalpwd;
        }
        public static string GenrateRandomnumber()
        {
             int length = 8;
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            Thread.Sleep(500);
            while (0 < length--)
            {
                res += valid[rnd.Next(valid.Length)];
            }
            return res;
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
        /// <summary>
        /// method to send new password through Email
        /// </summary>
        /// <param name="businessEmail"></param>
        public static void SendMailWithAttachements(BusinessEmail businessEmail)
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
                        foreach (var attachedFiles in businessEmail.AttachementFiles)
                        {
                            Attachment objAttachment = new Attachment(attachedFiles);
                            mail.Attachments.Add(objAttachment); 
                        }
                        
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
        /// <summary>
        /// Used for resize the uploaded image with same picture quality
        /// </summary>
        /// <param name="OriginalImage">OriginalImage</param>
        /// <param name="ThumbSize">ThumbSize</param>
        /// <returns>Image</returns>
        /// <createdby>bobi</createdby>
        /// <createddate>11 june 2014</createddate>
        public static void GetThumbnailImage(Image OriginalImage, string NewPath)
        {
            //set fixed with and height
            Int32 thWidth = 315;
            Int32 thHeight = 150;
            Image i = OriginalImage;
            Int32 w = i.Width;
            Int32 h = i.Height;
            Int32 th = thHeight;
            Int32 tw = thWidth;

            Bitmap target = new Bitmap(tw,th);
            try
            {
                if (OriginalImage.Width > thWidth) 
                {
                    tw = thWidth;
                }
                if (OriginalImage.Height > thHeight)
                {
                    th = thHeight;
                } 
                 Graphics g = Graphics.FromImage(target);
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.High;
                    Rectangle rect = new Rectangle(0, 0, tw, th);
                    //draw the image with diffrent size and same quality
                    g.DrawImage(i, rect, 0, 0, w, h, GraphicsUnit.Pixel);
                    OriginalImage.Dispose();
                    Image imgActual = (Image)target;
                    imgActual.Save(NewPath);
                    imgActual.Dispose();
                
            }
            catch
            {
                throw;
            }
            finally
            {
                target.Dispose();
            }
            
        }

        //public static byte[] GetLocalReport(LocalReport localReport)
        //{
        //    try
        //    {
        //        string reportType = "pdf";
        //        string mimeType;
        //        string encoding;
        //        string fileNameExtension;

        //        string deviceInfo = "<DeviceInfo>" +
        //            "  <OutputFormat>jpeg</OutputFormat>" +
        //            "  <PageWidth>8.5in</PageWidth>" +
        //            "  <PageHeight>11in</PageHeight>" +
        //            "  <MarginTop>0.5in</MarginTop>" +
        //            "  <MarginLeft>.5in</MarginLeft>" +
        //            "  <MarginRight>.25in</MarginRight>" +
        //            "  <MarginBottom>0.25in</MarginBottom>" +
        //            "</DeviceInfo>";
        //        Warning[] warnings;
        //        string[] streams;
        //        byte[] renderedBytes;
        //        //Render the report            
        //        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
        //    }
        //    catch 
        //    {
        //        throw;
        //    }
        //}
    }
    #endregion
}
