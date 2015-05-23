using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DecisionPoint.Models;
using DecisionPointBAL.Implementation;

namespace DecisionPoint.Controllers
{
    public class SuperAdminDashBoardController : Controller
    {
        #region Global Valriables
        //log declatration
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        SuperAdminDashBoard objSuperAdminDashBoard = null;
        int UserId = 0;
        ActionResult objactionresult = null;
        #endregion


        //
        // GET: /SuperAdminDashBoard/

        public ActionResult SuperAdminDashBoard()
        {
            return View("SuperAdminDashBoard");
        }

        public ActionResult FeeCreation()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetCompanies()
        {

            StringBuilder logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                objSuperAdminDashBoard = new SuperAdminDashBoard();
               // objSuperAdminDashBoard.companies = objDecisionPointEngine.GetCompanyList().Select(x => new Companies { BusinessName=x.BusniessName, CompanyID =x.BusniessID, Address=x.Address, ContactName=x.UserName }).ToList();
                //objSuperAdminDashBoard.pagesize = objSuperAdminDashBoard.companies.Count();
                //objSuperAdminDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                //objactionresult = PartialView("GetCompanies", objSuperAdminDashBoard);
                //if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                //{
                //    UserId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                //    string CompanyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);

                //    objComapnyDashBoard.vendorPerformance = objDecisionPointEngine.VendorPerformance(CompanyId, true, "Vendor", Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture));
                //    objComapnyDashBoard.pagesize = objComapnyDashBoard.vendorPerformance.Count();
                //    objComapnyDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                //    ViewData.Model = objComapnyDashBoard;
                //    ViewBag.Permission = objDecisionPointEngine.GetUserPermission(UserId);
                //    objactionresult = PartialView("VendorPerformance", objComapnyDashBoard);
                //}
                //else
                //{
                //    objactionresult = RedirectToAction("Login", "Login");

                //}
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

    }
}
