using System;
using System.Globalization;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using DecisionPointMailMatixBAL;
using System.Configuration;
using System.Timers;


namespace RecurringPaymentService
{
    /// <summary>
    /// services for recurring payment and reminder services.
    /// </summary>
    ///<createdby>Sumit Saurav</createdby>
    /// <createddate>09/july/2014</createddate>
    public partial class ReminderService : ServiceBase
    {

        private bool monthlyHasRun = false;
        private bool yearlyHasRun = false;
        private bool resendInviteHasRun = false;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ReminderService));
        private static System.Timers.Timer t;
        private DateTime lastRun = DateTime.Now.AddDays(-1);
        StringBuilder objdStringBuilder = null;
        public ReminderService()
        {
            //string[] arg = new string[1] { "abcv" };
            //OnStart(arg);
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            t = new System.Timers.Timer(5 * 60 * 1000);//
            t.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            t.Start();
        }

        protected override void OnStop()
        {
            Thread.Sleep(40000);
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected override void OnPause()
        {
            Thread.Sleep(40000);
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            DateTime startAt = DateTime.Today.AddHours(9).AddMinutes(48);
            if (lastRun < startAt && DateTime.Now >= startAt)
            {
                log4net.Config.XmlConfigurator.Configure();
                objdStringBuilder = new StringBuilder();
                DateTime temp = DateTime.Now;
                try
                {
                    objdStringBuilder.AppendLine("Start Window Service");
                    
                    #region Send Invitation
                    MailMatrixEngine.SendInvitationMail();
                    #endregion

                    #region Resend Invitation Mail
                    if (Convert.ToInt32(temp.Day) % 2 == 0 && !resendInviteHasRun) // if date is even then run
                    {
                        MailMatrixEngine.ResendInvitationMail();
                        resendInviteHasRun = true;
                    }
                    #endregion

                    #region Old Funtionality
                   
                    //#region Monthly Recurring Payment
                    //int monthlyDateToRun = Convert.ToInt32(ConfigurationManager.AppSettings["MonthlyDateKey"]);
                    //// if today is first of month day is 1
                    //if (temp.Day == monthlyDateToRun && !monthlyHasRun)
                    //{
                    //    objdStringBuilder.AppendLine("Start Company Monthly Recurring payment Service");
                    //    RecurringPayment.MakeCompanyRecurringMonthlyPayment();
                    //    objdStringBuilder.AppendLine("End of Company Monthly Recurring payment Service");

                    //    objdStringBuilder.AppendLine("Start IC Monthly Recurring payment Service");
                    //    RecurringPayment.MakeTheyPayIcRecurringMontthlyPayment();
                    //    objdStringBuilder.AppendLine("End IC Monthly Recurring payment Service");
                    //    monthlyHasRun = true;

                    //}
                    //else
                    //{
                    //    monthlyHasRun = false;

                    //}
                    //#endregion

                    //#region Yearly Recurring Payment
                    //// if today yearly recurring payment service is not run.
                    //if (!yearlyHasRun)
                    //{
                    //    objdStringBuilder.AppendLine("Start Company Annual Recurring payment Service");
                    //    RecurringPayment.MakeCompanyRecurringAnnualPayment();
                    //    objdStringBuilder.AppendLine("End Company Annual Recurring payment Service");
                    //    yearlyHasRun = true;
                    //}
                    //else
                    //{
                    //    yearlyHasRun = false;
                    //}
                    //#endregion
                    //#region JCR Retake
                    ////Used for retake funtionality in JCR
                    //CommunicationMailEngine.JCRRetake();
                    //#endregion
                    #endregion

                    #region MailMatrix
                    //used for sending the mail for communication due dates
                    MailMatrixEngine.mailReminder();
                    #endregion

                    #region Communication Retake
                    //Used for retake funtionality in Communication
                    CommunicationMailEngine.CommunicationRetake();
                    #endregion

                    #region Communication Mail Sending

                    //used for document mail sending
                    CommunicationMailEngine.DocumentMailSending();
                    #endregion

                    #region Contracts Mail Sending
                    //used for set the contract in alerts
                    ContractsMailEngine.SetContractInAlerts();
                    //used for contracts mail sending to owner of contract
                    ContractsMailEngine.ContractsMailSending();
                    #endregion

                    #region Contracts Events Mail Sending
                    //used for set the contract events in alerts
                    ContractsMailEngine.SetContractEventsAlerts();
                    //used for contracts events mail sending to owner of contract
                    ContractsMailEngine.ContractsEventsMailSending();
                    #endregion

                    objdStringBuilder.AppendLine("End Window Service");
                    lastRun = DateTime.Now;
                    t.Start();

                }
                catch (Exception ex)
                {
                    monthlyHasRun = false;
                    yearlyHasRun = false;
                    log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(ReminderService).Name, MethodBase.GetCurrentMethod().Name);
                }
                finally
                {
                    objdStringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                    log.Info(objdStringBuilder.ToString());
                }
            }
        }
    }
}
