using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DecisionPoint.Controllers.WebApi;
using DecisionPoint.Models;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using RusticiSoftware.HostedEngine.Client;
using AppResource = DecisionPoint.Content.Resource1;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;

namespace DecisionPoint.Models.DashBoardViewModel.CompanyDashBoardViewModel
{
    public class SuperAdminViewModel
    {
        #region GlobalVariable
        DecisionPointEngine objDecisionPointEngine = null;
       // int userId = 0;
        SuperAdminDashBoard objSuperAdminDashBoard = null;
        Companies objCompanies = null;
        #endregion

        /// <summary>
        /// Used to get the my partners details : Display on My partners View in Super Admin Login
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="search">search</param>
        /// <returns>SuperAdminDashBoard</returns>
        ///<createddate>apr 4 2014</createddate>
        /// <returns>string type site root</returns>
        public SuperAdminDashBoard GetMyPartnersDetails(int id, string search ,int type)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
               // userId = Convert.ToInt32(HttpContext.Current.Session["superAdmin"], CultureInfo.InvariantCulture);
                objSuperAdminDashBoard = new SuperAdminDashBoard();
                objSuperAdminDashBoard.rowperpage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                //check current/past partners
                if (id == 1)
                {
                    //call method for get the active[current] My partners details
                    objSuperAdminDashBoard.CompanyList = objDecisionPointEngine.GetCompanyList(true, search).Select
                        (x => new Companies
                        {
                            BusinessName = x.BusniessName,
                            CompanyID = x.BusniessID,
                            Address = x.Address,
                            ContactName = x.UserName,
                            UserID = x.UserId,
                            UserType = x.UserType,
                            VendorType = x.VendorType,
                            Phone = x.Phone,
                            InvitationStatus = x.InvitationStatus,
                            InvitationDate=x.InvitationDate
                        }).ToList();
                   objSuperAdminDashBoard.pagesize = objSuperAdminDashBoard.CompanyList.Count();
                }
                else
                {
                    //call method for get the deactive[past] My partners details
                    objSuperAdminDashBoard.PastcompanyList = objDecisionPointEngine.GetCompanyList(false, search).Select(x => new Companies
                    {
                        BusinessName = x.BusniessName,
                        CompanyID = x.BusniessID,
                        Address = x.Address,
                        ContactName = x.UserName,
                        UserID = x.UserId,
                        UserType = x.UserType,
                        VendorType = x.VendorType,
                        TableId = x.Tableid,
                        Phone = x.Phone,
                        PaymentType = x.PaymentType,
                        InvitationStatus = x.InvitationStatus
                    }).ToList();
                    objSuperAdminDashBoard.pagesize = objSuperAdminDashBoard.PastcompanyList.Count();
                }
                //get my partners list as per filter selection
                if (!object.Equals(objSuperAdminDashBoard.CompanyList, null))
                {
                    if (type.Equals(1))
                    {
                        objSuperAdminDashBoard.CompanyList = objSuperAdminDashBoard.CompanyList.Where(x => x.UserType == Shared.Company).ToList();
                    }
                    else if (type.Equals(2))
                    {
                        objSuperAdminDashBoard.CompanyList = objSuperAdminDashBoard.CompanyList.Where(x => x.UserType == Shared.IC).ToList();
                    }
                }
                if (!object.Equals(objSuperAdminDashBoard.PastcompanyList, null))
                {
                    if (type.Equals(1))
                    {
                        objSuperAdminDashBoard.PastcompanyList = objSuperAdminDashBoard.PastcompanyList.Where(x => x.UserType == Shared.Company).ToList();
                    }
                    else if (type.Equals(2))
                    {
                        objSuperAdminDashBoard.PastcompanyList = objSuperAdminDashBoard.PastcompanyList.Where(x => x.UserType == Shared.IC).ToList();
                    }
                }

                #region Groupby Status
                int count = 0;

                IList<Companies> companyDetails = new List<Companies>().ToList();
                objCompanies = new Companies()
                {
                    BusinessName = string.Empty,
                    CompanyID = string.Empty,
                    Address =string.Empty,
                    ContactName = string.Empty,
                    UserID = 0,
                    UserType = string.Empty,
                    VendorType = string.Empty,
                    TableId =0,
                    Phone = string.Empty,
                    PaymentType = 0,
                    InvitationStatus =false
                };
                if (!object.Equals(objSuperAdminDashBoard.CompanyList, null))
                {
                    var col = objSuperAdminDashBoard.CompanyList;
                    companyDetails = col.ToList();
                    foreach (var list in objSuperAdminDashBoard.CompanyList.ToList())
                    {
                        if (list.InvitationStatus)
                        {
                            companyDetails.Insert(count, objCompanies);
                            count++;
                            break;
                        }

                        count++;
                    }
                    objSuperAdminDashBoard.CompanyList = companyDetails.ToList();
                }
                else if (!object.Equals(objSuperAdminDashBoard.PastcompanyList, null))
                {
                    var col = objSuperAdminDashBoard.PastcompanyList;
                    companyDetails = col.ToList();
                    foreach (var list in objSuperAdminDashBoard.PastcompanyList.ToList())
                    {
                        if (list.InvitationStatus)
                        {
                            companyDetails.Insert(count, objCompanies);
                            count++;
                        }

                        count++;
                    }
                    objSuperAdminDashBoard.PastcompanyList = companyDetails.ToList();
                }
               
                
                #endregion

                objSuperAdminDashBoard.Active = true;
                return objSuperAdminDashBoard;
            }
            catch
            {

                throw;
            }
        }
    }
}