using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using DecisionPointDAL.Implemention;
using DecisionPointMailMatixBAL.Request;
using DecisionPointDAL.Implemention.ResponseParam;
using DecisionPointMailMatixBAL.Core;
using DecisionPointCAL.Common;
using System.Globalization;
using System.IO;
using System.Configuration;


namespace DecisionPointMailMatixBAL
{
    public class MailMatrixEngine
    {
      
        #region Mail matrix
        /// <summary>
        /// Mail reminder
        /// </summary>
        /// <createdBy>Nilesh Dubey</createdBy>
        /// <createdDate>Jan 20 2014</createdDate>
        public static void mailReminder()
        {
            List<DecisionPointMailMatixBAL.Request.MailReminder> lstreceipentUserID = new List<DecisionPointMailMatixBAL.Request.MailReminder>();
            StringBuilder objStringBuilder = new StringBuilder();
            try
            {
                string url = DecisionPointRepository.GetSiteUrl();
                objStringBuilder.AppendLine("start method Mail Reminder in BAL");
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                lstreceipentUserID = objdecisionPointRepository.mailReminder().Select(x => new DecisionPointMailMatixBAL.Request.MailReminder
                {
                    EmailID = x.EmailID,
                    BusinessName = x.BusinessName,
                    flow = x.flow,
                    Fname = x.Fname,
                    ID = x.ID,
                    name = x.name,
                    Password = x.Password,
                    SenderId = x.SenderId,
                    UserId = x.UserId,
                    Count = x.Count,
                    Catagory = x.Catagory,
                    DocId = x.DocId,
                    TblId = x.TblId,
                    UserType=x.UserType,
                    DueDate = x.DueDate
                }).ToList();

                string body = string.Empty;
                string subject = string.Empty;
                for (int iCounter = 0; iCounter < lstreceipentUserID.Count; iCounter++)
                {
                    MailMatrixResponseParam objMailMatrixResponseParam = new MailMatrixResponseParam()
                    {
                        DocId = lstreceipentUserID[iCounter].DocId,
                        SenderId = lstreceipentUserID[iCounter].SenderId,
                        RecevierId = lstreceipentUserID[iCounter].ID,
                        TblId = lstreceipentUserID[iCounter].TblId,
                        Type = 1
                    };
                    if (!objdecisionPointRepository.CheckDocumentMailSentOrNotToUser(objMailMatrixResponseParam))
                    {
                        objStringBuilder.AppendLine("sender Id=>" + lstreceipentUserID[iCounter].SenderId.ToString());

                        string signature = objdecisionPointRepository.GetSignature(lstreceipentUserID[iCounter].SenderId);
                        if (signature != null)
                        {
                            string[] sign = signature.Split(new string[] { "body>" }, StringSplitOptions.None);
                            subject = "Reminder";
                            signature = sign[1].Substring(0, sign[1].Length - 2);
                            MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                            {
                                PersonName = lstreceipentUserID[iCounter].Fname + Shared.SingleSpace + lstreceipentUserID[iCounter].name,
                                Signature = signature,
                                InviteeCompanyName=lstreceipentUserID[iCounter].BusinessName,
                                Category = lstreceipentUserID[iCounter].Catagory,
                                DueDate =Convert.ToString(lstreceipentUserID[iCounter].DueDate,CultureInfo.InvariantCulture),
                                DomainUrl=url
                            };
                            if (!string.IsNullOrEmpty(lstreceipentUserID[iCounter].UserType))
                            {
                                if (string.Equals(lstreceipentUserID[iCounter].UserType,Shared.IC))
                                {
                                    objMailInviteFormat.FilePath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["ICInviteMailAlert"], CultureInfo.InvariantCulture);
                                }
                                else
                                {
                                    objMailInviteFormat.FilePath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["StaffInviteMailAlert"], CultureInfo.InvariantCulture);
                                }
                            }
                            else
                            {
                                objMailInviteFormat.FilePath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["StaffInviteMailAlert"], CultureInfo.InvariantCulture);
                            }
                            if (lstreceipentUserID[iCounter].Count > 0)
                            {
                                objMailInviteFormat.Action = "Please complete the " + lstreceipentUserID[iCounter].Catagory;
                                body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                                //body = "<div style='line-height: 25px;'><div style='width:100%'>To: " + lstreceipentUserID[iCounter].Fname + "</div><div style='width:100%'>From: " + lstreceipentUserID[iCounter].BusinessName + "</div><div style='width:100%'>Subject: Reminder</div>You have a " + lstreceipentUserID[iCounter].Catagory + " that is to be completed in " + lstreceipentUserID[iCounter].Count + " Days.</br></br></br></br></div>" + signature;
                                //body = "Dear " + lstreceipentUserID[iCounter].name + "<br/><br/>" + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + " has requested to Join Compliance Tracker.Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='http://Web.chetu.com/DecisionPoint-EE?id=" + lstreceipentUserID[iCounter].ID + "'>click here</a> to get to log in page:</br><br/>Your username is:  " + lstreceipentUserID[iCounter].UserId + "<br/> password is:  " + lstreceipentUserID[iCounter].Password + "</br></br>" + signature;
                            }
                            else if (lstreceipentUserID[iCounter].Count < 0)
                            {
                                objMailInviteFormat.Action = "Please complete the " + lstreceipentUserID[iCounter].Catagory;
                                body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                                //body = "<div style='line-height: 25px;'><div style='width:100%'>To: " + lstreceipentUserID[iCounter].Fname + "</div><div style='width:100%'>From: " + lstreceipentUserID[iCounter].BusinessName + "</div><div style='width:100%'>Subject: Reminder</div></div>You have a " + lstreceipentUserID[iCounter].Catagory + " that is past due " + lstreceipentUserID[iCounter].Count + " Days.</br></br></br></br></div>" + signature;
                                //body = "Dear " + lstreceipentUserID[iCounter].name + "<br/><br/>" + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + " has requested to Join Compliance Tracker.Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='http://Web.chetu.com/DecisionPoint-EE?id=" + lstreceipentUserID[iCounter].ID + "'>click here</a> to get to log in page:</br><br/>Your username is:  " + lstreceipentUserID[iCounter].UserId + "<br/> password is:  " + lstreceipentUserID[iCounter].Password + "</br></br>" + signature;
                            }
                            else
                            {
                                objMailInviteFormat.Action = "Please complete the " + lstreceipentUserID[iCounter].Catagory;
                                body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                                //body = "<div style='line-height: 25px;'><div style='width:100%'>To: " + lstreceipentUserID[iCounter].Fname + "</div><div style='width:100%'>From: " + lstreceipentUserID[iCounter].BusinessName + "</div><div style='width:100%'>Subject: Reminder</div>You have a " + lstreceipentUserID[iCounter].Catagory + " that is due to be completed today.</br></br></br></br></div>" + signature;
                                //body = "Dear " + lstreceipentUserID[iCounter].name + "<br/><br/>" + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + " has requested to Join Compliance Tracker.Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + Convert.ToString(lstreceipentUserID[iCounter].BusinessName) + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='http://Web.chetu.com/DecisionPoint-EE?id=" + lstreceipentUserID[iCounter].ID + "'>click here</a> to get to log in page:</br><br/>Your username is:  " + lstreceipentUserID[iCounter].UserId + "<br/> password is:  " + lstreceipentUserID[iCounter].Password + "</br></br>" + signature;
                            }
                            //lstreceipentUserID[iCounter].EmailID = "nileshd@yopmail.com";
                            BusinessCore.GetSmtpDetail(lstreceipentUserID[iCounter].EmailID, body, subject);

                            //Insert mail log into database
                            objdecisionPointRepository.DocumentMailSndLog(objMailMatrixResponseParam);
                            objStringBuilder.AppendLine("end method Mail Reminder in BAL");
                        }
                    }
                }

            }
            catch
            {

                throw;
            }
            finally
            {
                //  Log.WriteTraceLog(objStringBuilder);
            }
        }

        /// <summary>
        /// Resend Invitation mail
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 13 2014</CreatedDate>
        public static void ResendInvitationMail()
        {

            StringBuilder objStringBuilder = new StringBuilder();
            try
            {

                objStringBuilder.AppendLine("start method inviation Mail sending in BAL");
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                string signature = string.Empty;
                string subject = string.Empty;
                string businessName = string.Empty;
                string text = string.Empty;
                string password = string.Empty;
                //get site url
                string url = DecisionPointRepository.GetSiteUrl();

                #region get user list for sending mail

                List<DecisionPointMailMatixBAL.Request.MailReminder> lstUserList = objdecisionPointRepository.GetUserListWithNoRegistration().Select(x => new DecisionPointMailMatixBAL.Request.MailReminder
                {
                    EmailID = x.EmailId,
                    BusinessName = x.BusinessName,
                    Fname = x.FirstName,
                    name = x.LastName,
                    Password = x.Password,
                    SenderId = x.ParentUserId,
                    ID = x.UserId,
                    UserType = x.UserType,
                }).ToList();
                #endregion
                if (lstUserList != null && lstUserList.Count() > 0)
                {
                    foreach (var item in lstUserList)
                    {
                        #region Get Signature of parent
                        //get the signature of invitee company
                        signature = objdecisionPointRepository.GetSignature(item.SenderId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }
                        if (!string.IsNullOrEmpty(item.Password))
                        {
                            password = BusinessCryptography.base64Decode2(item.Password);
                        }
                        #endregion

                        item.Url = url;
                        item.Signature = signature;
                        item.DirectoryPath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture);
                        item.Password = password;
                        text = SetMailFormat(item);
                        if (item.UserType == Shared.Individual || item.UserType == Shared.IC)
                        {
                            #region Staff & IC mail format

                            subject = "Compliance Tracker Account";

                            //text = "<div style='line-height:25px'>To: " + item.Fname + " " + item.name + "<br/>From: " + item.BusinessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + item.BusinessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + password
                            //    + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }
                        if (item.UserType == Shared.Company)
                        {
                            #region Client & Vendor Mail format

                            subject = "Compliance Tracker";
                           //text = "<div style='line-height:25px'>To: " + item.Fname + " " + item.name + "<br/>From: " + item.BusinessName + "<br/>Subject: Compliance Tracker<br/><br/>" + item.BusinessName + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + item.BusinessName + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }
                        BusinessCore.GetSmtpDetail(item.EmailID, text, subject);
                        // objdecisionPointRepository.UpdateMailStatus(Convert.ToInt32(item.UserId), item.SenderId, item.UserType);
                    }
                }


            }
            catch
            {

                throw;
            }
            finally
            {
                //  Log.WriteTraceLog(objStringBuilder);
            }
        }

        /// <summary>
        /// Send Invitation mail
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Oct 16 2014</CreatedDate>
        public static void SendInvitationMail()
        {

            StringBuilder objStringBuilder = new StringBuilder();
            try
            {

                objStringBuilder.AppendLine("start method inviation Mail sending in BAL");
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                string signature = string.Empty;
                string subject = string.Empty;
                string businessName = string.Empty;
                string text = string.Empty;
                string password = string.Empty;
                //get site url
                string url = DecisionPointRepository.GetSiteUrl();

                #region Staff


                #region get Staff list for sending mail

                List<DecisionPointMailMatixBAL.Request.MailReminder> lstStaffList = objdecisionPointRepository.GetStaffListWithNoInvitationMail().Select(x => new DecisionPointMailMatixBAL.Request.MailReminder
                {
                    EmailID = x.EmailId,
                    BusinessName = x.BusinessName,
                    Fname = x.FirstName,
                    name = x.LastName,
                    Password = x.Password,
                    SenderId = x.ParentUserId,
                    ID = x.UserId,
                    UserType = x.UserType,
                }).ToList();
                #endregion
                if (lstStaffList != null && lstStaffList.Count() > 0)
                {
                    foreach (var item in lstStaffList)
                    {
                        #region Get Signature of parent
                        //get the signature of invitee company
                        signature = objdecisionPointRepository.GetSignature(item.SenderId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }
                        if (!string.IsNullOrEmpty(item.Password))
                        {
                            password = BusinessCryptography.base64Decode2(item.Password);
                        }
                        #endregion

                        if (item.UserType == Shared.Individual)
                        {
                            
                            item.Url = url;
                            item.Signature = signature;
                            item.DirectoryPath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture);
                            text = SetMailFormat(item);
                            item.Password = password;
                            #region Staff mail format

                            subject = "Compliance Tracker Account";
                            //text = "<div style='line-height:25px'>To: " + item.Fname + " " + item.name + "<br/>From: " + item.BusinessName + "<br/>Subject: Compliance Tracker Account<br/><br/>" + item.BusinessName + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + password
                            // + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }

                        BusinessCore.GetSmtpDetail(item.EmailID, text, subject);
                        objdecisionPointRepository.UpdateMailStatus(item.ID, item.SenderId, item.UserType);
                    }
                }
                #endregion

                #region Client/Vendor & IC
                #region get Client/Vendor & IC list for sending mail

                List<DecisionPointMailMatixBAL.Request.MailReminder> lstVendorList = objdecisionPointRepository.GetVendorListWithNoInvitationMail().Select(x => new DecisionPointMailMatixBAL.Request.MailReminder
                {
                    EmailID = x.EmailId,
                    BusinessName = x.BusinessName,
                    Fname = x.FirstName,
                    name = x.LastName,
                    Password = x.Password,
                    SenderId = x.ParentUserId,
                    ID = x.UserId,
                    UserType = x.UserType,
                    SenderCompanyId = x.ParentCompanyId,
                }).ToList();
                #endregion
                if (lstVendorList != null && lstVendorList.Count() > 0)
                {
                    foreach (var item in lstVendorList)
                    {
                        #region Get Signature of parent
                        //get the signature of invitee company
                        signature = objdecisionPointRepository.GetSignature(item.SenderId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }
                        if (!string.IsNullOrEmpty(item.Password))
                        {
                            password = BusinessCryptography.base64Decode2(item.Password);
                        }
                        #endregion

                        if (item.UserType == Shared.IC)
                        {
                            #region IC mail format
                            item.Url = url;
                            item.Signature = signature;
                            item.DirectoryPath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture);
                            text = SetMailFormat(item);
                            item.Password = password;
                            subject = "Compliance Tracker Account";
                            //text = "<div style='line-height:25px'>To: " + item.Fname + " " + item.name + "<br/>From: " + item.SenderCompanyId + "<br/>Subject: Compliance Tracker Account<br/><br/>" + item.SenderCompanyId + " welcome to Compliance Tracker.Below you will find your username and a temporary password to gain access to the application. With Compliance Tracker you will be able to access and read all your company’s policy and procedures, directives, e-learning and much more! You also get a library where all the documents and e-learning are stored for your easy reference.Log in, reset you temporary password and complete your profile.If you have any trouble please feel free to contact your system administrator.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + password
                            //    + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }
                        if (item.UserType == Shared.Company)
                        {
                            #region Client & Vendor Mail format
                            item.Url = url;
                            item.Signature = signature;
                            item.DirectoryPath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture);
                            text = SetMailFormat(item);
                            item.Password = password;
                            subject = "Compliance Tracker";
                            //text = "<div style='line-height:25px'>To: " + item.Fname + " " + item.name + "<br/>From: " + item.SenderCompanyId + "<br/>Subject: Compliance Tracker<br/><br/>" + item.SenderCompanyId + " has requested to join Compliance Tracker. Compliance Tracker is first industry wide tool that was  born out of federal regulations requiring that we can communicate state, federal, client and " + item.SenderCompanyId + "’s best practices, policies and procedures to everyone relevant to process down to the ‘boots on the street’ and be able to provide an audit trail.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + password + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }
                        BusinessCore.GetSmtpDetail(item.EmailID, text, subject);
                        objdecisionPointRepository.UpdateMailStatus(Convert.ToInt32(item.ID), item.SenderId, item.UserType);
                    }
                }
                #endregion

                #region IC Non Client
                List<DecisionPointMailMatixBAL.Request.MailReminder> lstICNonClientList = objdecisionPointRepository.GetICNonClientWithNoInvitationMail().Select(x => new DecisionPointMailMatixBAL.Request.MailReminder
               {
                   EmailID = x.EmailId,
                   BusinessName = x.BusinessName,
                   Fname = x.FirstName,
                   name = x.LastName,
                   Password = x.Password,
                   SenderId = x.ParentUserId,
                   SenderCompanyId = x.ParentCompanyId,
                   ID = x.UserId,
                   UserType = x.UserType,
               }).ToList();

                if (lstICNonClientList != null && lstICNonClientList.Count() > 0)
                {
                    foreach (var item in lstICNonClientList)
                    {
                        #region Get Signature of parent
                        //get the signature of invitee company
                        signature = objdecisionPointRepository.GetSignature(item.SenderId);

                        if (!string.IsNullOrEmpty(signature))
                        {
                            string[] sign = signature.Split(new string[] { Shared.SplitSignature }, StringSplitOptions.None);
                            signature = sign[1].Trim();
                        }
                        if (!string.IsNullOrEmpty(item.Password))
                        {
                            password = BusinessCryptography.base64Decode2(item.Password);
                        }
                        #endregion

                        if (item.UserType == Shared.NonClient)
                        {
                            #region IC Non Client Mail format
                            item.Url = url;
                            item.Signature = signature;
                            item.DirectoryPath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture);
                            text = SetMailFormat(item);
                            item.Password = password;
                            subject = "Compliance Tracker";
                            //text = "<div style='line-height:25px'>To: " + item.Fname + Shared.SingleSpace + item.name + "<br/>From: " + item.BusinessName + "<br/>Subject: Compliance Tracker<br/><br/>Dear " + item.Fname + Shared.SingleSpace + item.name + "<br/>" + item.SenderCompanyId + " from " + item.BusinessName + " has allowed you access to their profile to consider assigning them work.  Your access is included at the bottom of the e-mail along with the link to access the log in.<br/><br/>Thank you for using Compliance Tracker.<br/><br/> Please <a href='" + url + "?id=" + item.SenderId + "'>click here</a> to get to log in page</br><br/>User Name: " + item.EmailID + "<br/> Password: " + item.Password + "<br/><br/></br></br>" + signature + "</div>";
                            #endregion
                        }
                        BusinessCore.GetSmtpDetail(item.EmailID, text, subject);
                        objdecisionPointRepository.UpdateMailStatus(Convert.ToInt32(item.ID), item.SenderId, item.UserType);
                    }
                }
                #endregion
            }
            catch
            {

                throw;
            }
            finally
            {
                //  Log.WriteTraceLog(objStringBuilder);
            }
        }
        /// <summary>
        /// Used for set the mail invite formate
        /// </summary>
        /// <param name="objMailReminder"></param>
        /// <returns></returns>
        private static string SetMailFormat(DecisionPointMailMatixBAL.Request.MailReminder objMailReminder)
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                UserDashBoardResponseParam objUserDashBoardResponseParam = new UserDashBoardResponseParam();
                objUserDashBoardResponseParam=objdecisionPointRepository.GetAccountDetails(objMailReminder.SenderId);
                MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                {
                    PersonName = objMailReminder.Fname + Shared.SingleSpace + objMailReminder.name,
                    PersonCompanyName = objMailReminder.BusinessName,
                    PersonUserType=objMailReminder.UserType,
                    Passwrod = objMailReminder.Password,
                    Signature = objMailReminder.Signature,
                    DomainUrl = objMailReminder.Url,
                    EmailId = objMailReminder.EmailID,
                    InviteeUserId = Convert.ToString(objMailReminder.SenderId, CultureInfo.InvariantCulture),
                    FilePath = objMailReminder.DirectoryPath,
                    InviteeCompanyName = objUserDashBoardResponseParam.companyName,
                    InviteePersonName = objUserDashBoardResponseParam.fName + Shared.SingleSpace + objUserDashBoardResponseParam.lName
                };

               string mailFormat = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
               return mailFormat;
            }
            catch 
            {
            throw;
            }
        }

        #endregion
    }

}
