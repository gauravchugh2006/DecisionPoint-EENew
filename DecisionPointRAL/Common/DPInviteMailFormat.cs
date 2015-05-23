using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;

namespace DecisionPointCAL.Common
{
    public class DPInviteMailFormat
    {
        #region Global Variables
        static string mailFormat = string.Empty;

        #endregion
        public static string PersonInviteMailFormat(MailInviteFormat objMailInviteFormat)
        {
            try
            {
                using (StreamReader reader = new StreamReader(objMailInviteFormat.FilePath))
                {

                    string readFile = reader.ReadToEnd();
                    mailFormat = readFile;
                    mailFormat = mailFormat.Replace("$$PersonName$$", objMailInviteFormat.PersonName);
                    mailFormat = mailFormat.Replace("$$UserName$$", objMailInviteFormat.EmailId);
                    mailFormat = mailFormat.Replace("$$Password$$", objMailInviteFormat.Passwrod);
                    mailFormat = mailFormat.Replace("$$DueDate$$", "ASAP");
                    mailFormat = mailFormat.Replace("$$RootUrl$$", objMailInviteFormat.DomainUrl);
                    mailFormat = mailFormat.Replace("$$Signature$$", objMailInviteFormat.Signature);
                    mailFormat = mailFormat.Replace("$$UserId$$", objMailInviteFormat.InviteeUserId);
                    mailFormat = mailFormat.Replace("$$Action$$", objMailInviteFormat.Action);
                    if (!object.Equals(objMailInviteFormat.PersonUserType, null))
                    {
                        if (objMailInviteFormat.PersonUserType.Equals(Shared.IC))
                        {
                            mailFormat = mailFormat.Replace("$$InviteePersonName$$", objMailInviteFormat.InviteeCompanyName);
                        }
                        else
                        {
                            mailFormat = mailFormat.Replace("$$InviteePersonName$$", objMailInviteFormat.InviteePersonName);
                        }
                    }
                    mailFormat = mailFormat.Replace("$$InviteeCompanyName$$", objMailInviteFormat.InviteeCompanyName);
                    if (!object.Equals(objMailInviteFormat.Type, null))
                    {
                        if (objMailInviteFormat.Type.Equals(Shared.One))
                        {
                            mailFormat = mailFormat.Replace("USER NAME:", Shared.SingleSpace);
                            mailFormat = mailFormat.Replace("PASSWORD:", Shared.SingleSpace);
                            mailFormat = mailFormat.Replace("$$PersonName$$", Shared.SingleSpace);
                            mailFormat = mailFormat.Replace("$$UserName$$", Shared.SingleSpace);
                            mailFormat = mailFormat.Replace("the following temporary user name and password", "your credentials");
                        }
                    }

                    return mailFormat;
                }
            }
            catch
            {
                throw;
            }

        }
    }
}
