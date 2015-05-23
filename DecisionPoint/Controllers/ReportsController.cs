using DecisionPoint.Models;
using DecisionPointBAL.Implementation;
using DecisionPointCAL;
using Microsoft.Reporting.WebForms;
using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using DecisionPointBAL.Implementation.Response;
using System.Configuration;

namespace DecisionPoint.Controllers
{
    public class ReportsController : Controller
    {
        #region Global Variable
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        ActionResult objActionResult = null;
        Reports objReport = null;
        int userId = 0;
        string companyId = string.Empty;
        ReportResponse objReportResponse = null;
        #endregion

        /// <summary>
        /// Usd for View the details Users in system
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createby>Bobi</createby>
        /// <createddate>25 Aug 2014</createddate>
        [HttpGet]
        public ActionResult Reports()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objReport = new Models.Reports();
                    objReport.DateFrom = System.DateTime.Now.Date;
                    objReport.DateTo = System.DateTime.Now.Date;
                    objReport.Reportdetails = objDecisionPointEngine.GetSavedReportDetails(companyId);
                    if (!object.Equals(objReport.Reportdetails, null))
                    {
                        objReport.pagesize = objReport.Reportdetails.Count;
                    }
                    objReport.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                   
                    ViewData.Model = objReport;
                    objActionResult = View();
                }
                else
                {
                    objActionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objActionResult;
        }
        /// <summary>
        /// Used for redirect to report viwer page which contain the option for save the report details for future use
        /// </summary>
        /// <param name="objReports"></param>
        /// <returns></returns>
        /// <createby>Bobi</createby>
        /// <createddate>25 Aug 2014</createddate>
        [HttpPost]
        public ActionResult ReportViwer(Reports objReports)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objReport = new Models.Reports();
                    objReport.DateFrom = objReports.DateFrom;
                    objReport.DateTo = objReports.DateTo;
                    objReport.Status = objReports.Status;
                    ViewData.Model = objReport;
                    objActionResult = View();
                }
                else
                {
                    objActionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objActionResult;
        }

        /// <summary>
        /// Used for Open the genrated report in IFrame
        /// </summary>
        /// <param name="datefrom"></param>
        /// <param name="dateto"></param>
        /// <param name="status"></param>
        /// <returns>ActionResult</returns>
        /// <createby>Bobi</createby>
        /// <createddate>25 Aug 2014</createddate>
        [HttpGet]
        public ActionResult ReportIFrame(string datefrom, string dateto,string status)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objReport = new Models.Reports();
                    objReport.DateFrom =Convert.ToDateTime(datefrom);
                    objReport.DateTo = Convert.ToDateTime(dateto);
                    objReport.Status =status;
                    ViewData.Model = objReport;
                    objActionResult = View();
                }
                else
                {
                    objActionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objActionResult;
        }
       
        /// <summary>
        /// used for genrate the report for Users View as per selected cretariya
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <returns>FileContentResult</returns>
        /// <createby>Bobi</createby>
        /// <createddate>25 Aug 2014</createddate>
        public FileContentResult GenrateUsersViewReport(string dateFrom, string dateTo, string status)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                using (LocalReport localReport = new LocalReport())
                {
                    ////set the report path 
                    localReport.ReportPath = Server.MapPath("~/Content/documents/Reports/UsersView.rdlc");
                    objDecisionPointEngine = new DecisionPointEngine();
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "GetUsersDetails";
                    objReportResponse = new ReportResponse()
                    {
                        DateFrom = Convert.ToDateTime(dateFrom, CultureInfo.InvariantCulture),
                        DateTo = Convert.ToDateTime(dateTo, CultureInfo.InvariantCulture),
                        Status = status,
                        CompanyId = companyId,
                        UserId = userId
                    };
                    //set the value of datasorce of ReportDataSource
                    reportDataSource.Value = objDecisionPointEngine.GetAllUsersDetailsInSystem(objReportResponse);
                    //added the ReportDataSource to LocalReport for Bind in RDLC report
                    localReport.DataSources.Add(reportDataSource);

                    string reportType = "pdf";
                    string mimeType;
                    string encoding;
                    string fileNameExtension;
                    //The DeviceInfo settings should be changed based on the reportType            
                    //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
                    string deviceInfo =
                        "<DeviceInfo>" +
                        "  <OutputFormat>PDF</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.2in</MarginTop>" +
                        "  <MarginLeft>.5in</MarginLeft>" +
                        "  <MarginRight>.25in</MarginRight>" +
                        "  <MarginBottom>0.25in</MarginBottom>" +
                        "</DeviceInfo>";
                    Warning[] warnings;
                    string[] streams;
                    byte[] renderedBytes;
                    //Render the report            
                    renderedBytes = localReport.Render(
                        reportType,
                        deviceInfo,
                        out mimeType,
                        out encoding,
                        out fileNameExtension,
                        out streams,
                        out warnings);
                    //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);            
                    return File(renderedBytes, mimeType);
                };
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }
        [HttpPost]
        public int SavedReportLog(Reports objReports)
        {
            int IsInserted = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                     objReportResponse=new ReportResponse(){
                         DateFrom = objReports.DateFrom,
                         DateTo = objReports.DateTo,
                         Status = objReports.Status,
                         CompanyId = companyId,
                        UserId=userId
                    };
                    IsInserted = objDecisionPointEngine.SavedReportDetails(objReportResponse);
                }
                else
                {
                     RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return IsInserted;
        }
        

    }
}
