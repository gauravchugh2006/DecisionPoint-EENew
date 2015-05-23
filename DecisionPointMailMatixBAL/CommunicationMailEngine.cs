using DecisionPointCAL.Common;
using DecisionPointDAL.Implemention;
using DecisionPointDAL.Implemention.ResponseParam;
using DecisionPointMailMatixBAL.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;

// ********************************************************************************************************************************
//                                                  class:CommunicationMailEngine
// ********************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// -------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+--------------------------------------------------------------------------------------------------
// July 14, 2014   | Bobi S | This class represents for sending the mail during communication publishing and Retake funtionality. 
// *********************************************************************************************************************************


namespace DecisionPointMailMatixBAL
{
  public class CommunicationMailEngine
  {
     
      /// <summary>
        /// Mail reminder
        /// </summary>
        /// <createdBy>Bobis</createdBy>
        /// <createdDate>Apr 15 2014</createdDate>
        public static void DocumentMailSending()
        {
            List<MailMatrixResponseParam> lstreceipentUserID = new List<MailMatrixResponseParam>();

            try
            {

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                string url = DecisionPointRepository.GetSiteUrl();
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                lstreceipentUserID = objdecisionPointRepository.DocumentMailSending().Select(x => new MailMatrixResponseParam
                {
                    Recevier = x.Recevier,
                    RecevierEmail = x.RecevierEmail,
                    Sender = x.Sender,
                    Category = x.Category,
                    Flow = x.Flow,
                    DocId = x.DocId,
                    TblId = x.TblId,
                    SenderId = x.SenderId,
                    RecevierId = x.RecevierId,
                    DueDate=x.DueDate,
                    UserType=x.UserType
                }).ToList();

                string body = string.Empty;
                string subject = string.Empty;
                for (int iCounter = 0; iCounter < lstreceipentUserID.Count; iCounter++)
                {
                    // if (!objdecisionPointRepository.CheckDocumentMailSentOrNotToUser(lstreceipentUserID[iCounter]))
                    // {

                    string signature = objdecisionPointRepository.GetSignature(lstreceipentUserID[iCounter].SenderId);
                    if (signature != null)
                    {
                        string[] sign = signature.Split(new string[] { "body>" }, StringSplitOptions.None);
                        if (!string.IsNullOrEmpty(lstreceipentUserID[iCounter].Category))
                        {
                            subject = "New " + lstreceipentUserID[iCounter].Category;
                        }
                        else { subject = "New Category"; }
                        signature = sign[1].Substring(0, sign[1].Length - 2);
                        if (!string.IsNullOrEmpty(lstreceipentUserID[iCounter].Recevier))
                        {
                            MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                            {
                                PersonName = lstreceipentUserID[iCounter].Recevier,
                                Signature = signature,
                                InviteeCompanyName = lstreceipentUserID[iCounter].Sender,
                                DueDate = Convert.ToString(lstreceipentUserID[iCounter].DueDate, CultureInfo.InvariantCulture),
                                DomainUrl=url
                            };
                            if (!string.IsNullOrEmpty(lstreceipentUserID[iCounter].UserType))
                            {
                                if (string.Equals(lstreceipentUserID[iCounter].UserType, Shared.IC))
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
                            objMailInviteFormat.Action = "You have received " + subject + " from " + lstreceipentUserID[iCounter].Sender+". Please complete it.";
                            body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);

                            
                                //if (lstreceipentUserID[iCounter].Flow.Equals(2))
                                //{
                                //    body = "<div style='line-height:25px'>To: " + lstreceipentUserID[iCounter].Recevier + "<br/>From: " + lstreceipentUserID[iCounter].Sender + "<br/>Subject: '" + subject + "'<br/><br/>" + lstreceipentUserID[iCounter].Sender + "A '" + subject + "' has been assigned to you. Please log and complete.<br/><br/></br></br>" + signature + "</div>";
                                //}
                                //else { body = "<div style='line-height:25px'>To: " + lstreceipentUserID[iCounter].Recevier + "<br/>From: " + lstreceipentUserID[iCounter].Sender + "<br/>Subject: '" + subject + "'<br/><br/>" + lstreceipentUserID[iCounter].Recevier + " you have received a '" + subject + "' from " + lstreceipentUserID[iCounter].Sender + ". Please log in and review.<br/><br/></br></br>" + signature + "</div>"; }
                            
                            
                        }



                        BusinessCore.GetSmtpDetail(lstreceipentUserID[iCounter].RecevierEmail, body, subject);
                        lstreceipentUserID[iCounter].Type = 0;
                        //Insert mail log into database
                        objdecisionPointRepository.DocumentMailSndLog(lstreceipentUserID[iCounter]);

                    }

                    //}
                }

            }
            catch
            {

                throw;
            }
            finally
            {

            }
        }
        /// <summary>
        /// Used for send the retake communications
        /// </summary>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>14 july 2014</createdDate>
        public static int CommunicationRetake()
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SetRetakeDocument();
            }
            catch
            {

                throw;
            }
        }
        /// <summary>
        /// Used for send the retake JCR
        /// </summary>
        /// <returns>int</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>3 Nov 2014</createdDate>
        public static int JCRRetake()
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SetRetakeJCR();
            }
            catch
            {

                throw;
            }
        }
    }
}
