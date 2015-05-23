using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using DecisionPoint.Models;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using System.Xml.Schema;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation.Request;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;

namespace DecisionPoint.Controllers
{
    public class SterlingController : Controller
    {
        #region GlobalVariable
        #region Members
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        string Url = string.Empty;
        string xmlTextFormat = string.Empty;
        string sterlingOrderInitiateUrl = string.Empty;
        string sterlingReviewResultUrl = string.Empty;
        int userId = 0;
        string companyId = string.Empty;
        int modifiedBy = 0;
        List<string> clientList = new List<string>();
        ViewModel objViewModel = null;
        #endregion

        #region Objects
        SterlingModel objSterlingModel = null;
        HttpWebRequest objHttpWebRequest = null;
        HttpWebResponse objHttpWebResponse = null;
        Stream objRequestStream = null;
        Stream objResponseStream = null;
        XmlTextReader objXMLReader;
        XmlDocument XMLResponse = null;
        DecisionPointEngine objDecisionPointEngine = null;
        SterlingResponse objSterlingResponse = null;
        BackGroundCheckMasterRequest objBackGroundCheckMasterRequest = null;
        ActionResult objactionResult = null;
        #endregion

        #endregion
        /// <summary>
        /// Used for go to sterling site for initiate the candidate
        /// </summary>
        /// <param name="objSterlingModel"></param>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 Feb 2015</CreatedDate>
        [HttpPost]
        public string GoToSterlingForScreening(SterlingModel objSterlingModel)
        {
            string sterlingResponseStatus = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                   
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return sterlingResponseStatus;
        }
        

        [HttpGet]
        public string ReviewSterlingResult(string SterlingOrderId)
        {
            string link = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                XmlDocument doc = new XmlDocument();
                //doc.Load("D://MOREDETAILS.xml");
                objViewModel=new ViewModel();
                doc = objViewModel.CreateHRXMLForReviewBGCheck(SterlingOrderId);
                HttpWebRequest objHttpWebRequest = null;
                HttpWebResponse objHttpWebResponse = null;
                Stream objRequestStream = null;
                
                string xmlTextFormat =objViewModel.GetTextFromXMlFile(doc);
                #region Sterling Credentials
                string SterlingUserName = Convert.ToString(ConfigurationManager.AppSettings["SterlingUserName"], CultureInfo.InvariantCulture);
                string SterlingPassword = Convert.ToString(ConfigurationManager.AppSettings["SterlingPassword"], CultureInfo.InvariantCulture);
                sterlingReviewResultUrl = Convert.ToString(ConfigurationManager.AppSettings["SterlingMoreDetailsURL"], CultureInfo.InvariantCulture);
                #endregion
                string Url = sterlingReviewResultUrl;
                objHttpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
                System.Net.ServicePointManager.Expect100Continue = false;
                //Post method
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(xmlTextFormat);
                objHttpWebRequest.Method = "POST";
                objHttpWebRequest.ContentLength = bytes.Length;
                objHttpWebRequest.ContentType = "application/json; charset=utf-8";

                objHttpWebRequest.Credentials = new NetworkCredential(SterlingUserName, SterlingPassword);
                
                objRequestStream = objHttpWebRequest.GetRequestStream();
                objRequestStream.Write(bytes, 0, bytes.Length);

                ////Close stream
                //objRequestStream.Close();

                objHttpWebResponse = (HttpWebResponse)objHttpWebRequest.GetResponse();
                string backstr = string.Empty;

                //---------- Start HttpResponse
                if (objHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader sr = new StreamReader(objHttpWebResponse.GetResponseStream(), System.Text.Encoding.Default);
                    backstr = sr.ReadToEnd();
                    XmlDocument sterlingOrderDoc = new XmlDocument();
                    sterlingOrderDoc.LoadXml(backstr);
                    XmlNodeList nodeList = sterlingOrderDoc.GetElementsByTagName("BackgroundReports");

                    //apply loop on nodes
                    foreach (XmlNode node in nodeList)
                    {
                        foreach (XmlNode backgroundReportPackageNode in node.ChildNodes)
                        {
                            switch (backgroundReportPackageNode.Name)
                            {
                                case "BackgroundReportPackage":
                                    foreach (XmlNode abc in backgroundReportPackageNode.ChildNodes)
                                    {
                                        switch (abc.Name)
                                        {
                                            case "SupportingDocumentation":
                                                link = abc.InnerText;
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return link;
        }
        [HttpGet]
        public ActionResult SterlingResultView()
        {
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionResult = View();
                }
                else
                {
                    objactionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;
        }
        [HttpGet]
        public ActionResult ICSterlingReviewResult()
        {
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objSterlingModel = new SterlingModel();
                    objSterlingModel.ICReviewResult = System.Configuration.ConfigurationManager.AppSettings["ICReviewResult"];
                    ViewData.Model = objSterlingModel;
                    objactionResult = View();
                }
                else
                {
                    objactionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;
        }

    }
}
