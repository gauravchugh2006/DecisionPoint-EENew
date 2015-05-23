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
using System.Text;
using System.Threading.Tasks;

namespace DecisionPointMailMatixBAL
{
    public class ContractsMailEngine
    {

        /// <summary>
        /// Contracts Mail reminder
        /// </summary>
        /// <createdBy>Bobis</createdBy>
        /// <createdDate>Apr 15 2014</createdDate>
        public static void ContractsMailSending()
        {
            List<MailMatrixResponseParam> lstreceipentUserID = new List<MailMatrixResponseParam>();

            try
            {
                string url = DecisionPointRepository.GetSiteUrl();
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                lstreceipentUserID = objdecisionPointRepository.ContractsMailSending().Select(x => new MailMatrixResponseParam
                {
                    Recevier = x.OwnerName,
                    RecevierEmail = x.EmailId,
                    Sender = x.ManagerName,
                    TblId = x.Id,
                    SenderId = x.CreatedBy
                }).ToList();

                string body = string.Empty;
                string subject = string.Empty;
                for (int iCounter = 0; iCounter < lstreceipentUserID.Count; iCounter++)
                {
                    string signature = objdecisionPointRepository.GetSignature(lstreceipentUserID[iCounter].SenderId);
                    if (signature != null)
                    {
                        string[] sign = signature.Split(new string[] { "body>" }, StringSplitOptions.None);
                        subject = "Contract Reminder Mail";
                        signature = sign[1].Substring(0, sign[1].Length - 2);
                        MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                        {
                            PersonName = lstreceipentUserID[iCounter].Recevier,
                            Signature = signature,
                            InviteeCompanyName = lstreceipentUserID[iCounter].Sender,
                            DueDate = Convert.ToString(lstreceipentUserID[iCounter].DueDate, CultureInfo.InvariantCulture),
                            DomainUrl=url
                        };
                        objMailInviteFormat.FilePath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["StaffInviteMailAlert"], CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(lstreceipentUserID[iCounter].Recevier))
                        {
                            objMailInviteFormat.Action = "You have received " + subject + " from " + lstreceipentUserID[iCounter].Sender + ". Please complete it.";
                            body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);

                            //body = "<div style='line-height:25px'>To: " + lstreceipentUserID[iCounter].Recevier + "<br/>From: " +
                            //    lstreceipentUserID[iCounter].Sender + "<br/>Subject: '" + subject + "'<br/><br/>" + lstreceipentUserID[iCounter].Recevier +
                            //    "Dear " + lstreceipentUserID[iCounter].Recevier + ",<br/> you have received a '" + subject + "' from " + lstreceipentUserID[iCounter].Sender + ". Please log at compliance tracker and review.<br/><br/></br></br>" +
                            //    signature + "</div>";
                        }
                        BusinessCore.GetSmtpDetail(lstreceipentUserID[iCounter].RecevierEmail, body, subject);
                        //update last invite date and next mail sending date in contracts log
                        UpdateContractLogInviteDate(lstreceipentUserID[iCounter].TblId);
                    }
                }

            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// used for update last invite date and next mail sending date in contracts log
        /// </summary>
        /// <param name="tblId"></param>
        private static void UpdateContractLogInviteDate(int tblId)
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                objdecisionPointRepository.UpdateContractLogInviteDate(tblId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// used for Set the contracts in alert section
        /// </summary>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Jan 3 2015</createdDate>
        public static int SetContractInAlerts()
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SetContractInAlerts();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Contracts Event Mail reminder
        /// </summary>
        /// <createdBy>Bobis</createdBy>
        /// <createdDate>Apr 15 2014</createdDate>
        public static void ContractsEventsMailSending()
        {
            List<int> recevierIds = null;
            List<MailMatrixResponseParam> lstContractEventsDetails = new List<MailMatrixResponseParam>();
            MailMatrixResponseParam lstreceipentUserID = new MailMatrixResponseParam();

            try
            {
                string url = DecisionPointRepository.GetSiteUrl();
                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directoryPath = Path.GetDirectoryName(location);
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                lstContractEventsDetails = objdecisionPointRepository.ContractsEventsMailSending().Select(x => new MailMatrixResponseParam
                 {
                     Recevier = x.OwnerName,
                     Sender = x.ManagerName,
                     TblId = x.Id,
                     SenderId = x.CreatedBy
                 }).ToList();

                string body = string.Empty;
                string subject = string.Empty;
                for (int iCounter = 0; iCounter < lstContractEventsDetails.Count; iCounter++)
                {
                    if (!object.Equals(lstContractEventsDetails[iCounter].Recevier, null))
                    {
                        recevierIds = lstContractEventsDetails[iCounter].Recevier.Split(',').Select(x => int.Parse(x)).ToList();
                        foreach (int item in recevierIds)
                        {
                            lstreceipentUserID = objdecisionPointRepository.GetAdminProfile(item).Select(x => new MailMatrixResponseParam
                                                    {
                                                        Recevier = x.FirstName,
                                                        RecevierEmail = x.Email,
                                                    }).FirstOrDefault();
                            string signature = objdecisionPointRepository.GetSignature(lstContractEventsDetails[iCounter].SenderId);
                            if (signature != null)
                            {
                                string[] sign = signature.Split(new string[] { "body>" }, StringSplitOptions.None);
                                subject = "Contract Reminder Mail";
                                signature = sign[1].Substring(0, sign[1].Length - 2);
                                MailInviteFormat objMailInviteFormat = new MailInviteFormat()
                                {
                                    PersonName = lstContractEventsDetails[iCounter].Recevier,
                                    Signature = signature,
                                    InviteeCompanyName = lstContractEventsDetails[iCounter].Sender,
                                    DueDate = Convert.ToString(lstContractEventsDetails[iCounter].DueDate, CultureInfo.InvariantCulture),
                                    DomainUrl=url
                                };
                                objMailInviteFormat.FilePath = directoryPath + Convert.ToString(ConfigurationManager.AppSettings["StaffInviteMailAlert"], CultureInfo.InvariantCulture);
                                if (!string.IsNullOrEmpty(lstreceipentUserID.Recevier))
                                {
                                    objMailInviteFormat.Action = "You have received " + subject + " from " + lstContractEventsDetails[iCounter].Sender + ". Please complete it.";
                                    body = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);

                                    //body = "<div style='line-height:25px'>To: " + lstreceipentUserID.Recevier + "<br/>From: " +
                                    //    lstContractEventsDetails[iCounter].Sender + "<br/>Subject: '" + subject + "'<br/><br/>" + lstreceipentUserID.Recevier +
                                    //    "Dear " + lstreceipentUserID.Recevier + ",<br/> you have received a '" + subject + "' from " + lstContractEventsDetails[iCounter].Sender + ". Please log at compliance tracker and review.<br/><br/></br></br>" +
                                    //    signature + "</div>";
                                }
                                BusinessCore.GetSmtpDetail(lstreceipentUserID.RecevierEmail, body, subject);
                                //update last invite date and next mail sending date in contracts log
                                UpdateContractLogInviteDate(lstContractEventsDetails[iCounter].TblId);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// used for Set the contracts events in alert section
        /// </summary>
        /// <returns>int</returns>
        /// <createdBy>Bobi</createdBy>
        /// <createdDate>Jan 3 2015</createdDate>
        public static int SetContractEventsAlerts()
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                return objdecisionPointRepository.SetContractEventsAlerts();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// used for update last invite date and next mail sending date in contracts log
        /// </summary>
        /// <param name="tblId"></param>
        private static void UpdateContractEventLogInviteDate(int tblId)
        {
            try
            {
                DecisionPointRepository objdecisionPointRepository = new DecisionPointRepository();
                objdecisionPointRepository.UpdateContractEventLogInviteDate(tblId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
