using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;
using DecisionPointHandler.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;


// ******************************************************************************************************************************
//                                                 class:DecisionPointHandler.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Feb. 24, 2014     |Bobi           | This class holds the interaction logic for DecisionPointHandler.cs
// ******************************************************************************************************************************

namespace DecisionPointHandler
{
    public class DecisionPointHandler : HttpTaskAsyncHandler
    {
        #region Global Variables
        #region Variables
        StringBuilder logMessage = null;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion
        #region Objects
        SterlingResponse objSterlingResponse = null;
        DecisionPointEngine objDecisionPointEngine = null;
        ResponseFromDPToSterling objResponseFromDPToSterling = null;
        #endregion
        #endregion

        #region Process Request From Sterling
        /// <summary>
        /// ProcessRequestAsync handel the all responses which we get from sterling
        /// </summary>
        /// <param name="objHttpContext"></param>
        /// <returns></returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreateDate>24 Feb 2015</CreateDate>
        public override async Task ProcessRequestAsync(HttpContext objHttpContext)
        {

            log4net.Config.XmlConfigurator.Configure();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                HttpRequest objHttpRequest = objHttpContext.Request;
                HttpResponse objHttpResponse = objHttpContext.Response;
                string userIP = objHttpContext.Request["REMOTE_ADDR"];
                string targetUrl = objHttpContext.Request.QueryString["url"];
                string userAgent = objHttpContext.Request.Browser.Browser;

                //get all sterling responses
                string responseMessage = await GetAllResponsesAndParseAsync(objHttpRequest);
                objHttpContext.Response.ContentType = "application/xml";
                objHttpContext.Response.Write(responseMessage);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(IHttpHandler).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

        }
        #endregion

        #region Async Calls
        /// <summary>
        /// Used for get all response from sterling which async
        /// </summary>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreateDate>24 Feb 2015</CreateDate>
        private async Task<string> GetAllResponsesAndParseAsync(HttpRequest objHttpRequest)
        {
            var func = Task<string>.Factory.StartNew(() => ParseAndSaveResponses(objHttpRequest));
            await func;
            return func.Result;
        }


        #endregion

        #region Parse XML And Save Record in DP

        /// <summary>
        /// parse and save all responses which we got from sterling
        /// </summary>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreateDate>24 Feb 2015</CreateDate>
        private string ParseAndSaveResponses(HttpRequest objHttpRequest)
        {
            XmlDocument xm = new XmlDocument();
            XmlNodeList nodeList = null;
            SterlingMapping objSterlingMapping = new SterlingMapping();
            SterlingWithDpResponse objSterlingWithDpResponse = new SterlingWithDpResponse();
            string responseMessage = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                string timeStamp;
                timeStamp = System.DateTime.Now.ToString("s");

                try
                {
                    if (objHttpRequest.ContentType.Contains("text/xml"))
                    {
                        objHttpRequest.InputStream.Seek(0, SeekOrigin.Begin);
                        xm.Load(objHttpRequest.InputStream);

                        #region Save Dumy Resonse
                        xm.Save(Convert.ToString(ConfigurationManager.AppSettings["dumyres"], CultureInfo.InvariantCulture) + "ResXML" + timeStamp.Replace(':', '-') + Shared.XmlFileExt);
                        #endregion

                        #region Parse XML record
                        //get Dp User Id
                        if (xm.DocumentElement.Attributes["dpuserId"] != null)
                        {
                            objSterlingMapping.DPUserId = xm.DocumentElement.Attributes["dpuserId"].Value;

                            //XML parsing for order confirmation
                            nodeList = xm.GetElementsByTagName("BackgroundReportPackage");
                            
                            //apply loop on nodes
                            foreach (XmlNode node in nodeList)
                            {
                                if (node.Attributes["type"] != null)
                                {
                                    objSterlingMapping.PackageType = node.Attributes["type"].Value;
                                }
                                foreach (XmlNode backgroundReportPackageNode in node.ChildNodes)
                                {
                                    switch (backgroundReportPackageNode.Name)
                                    {
                                        case "ProcessingInformation":
                                            foreach (XmlNode processingInformationnode in backgroundReportPackageNode.ChildNodes)
                                            {
                                                switch (processingInformationnode.Name)
                                                {
                                                    case "ScopeOfWork":
                                                        objSterlingMapping.ReviewResultUrl = processingInformationnode.InnerText;
                                                        break;
                                                }
                                            }
                                            break;
                                        case "ProviderReferenceId":
                                            objSterlingMapping.SterlingOrderId = backgroundReportPackageNode.InnerText;
                                            break;
                                        case "ClientReferenceId":
                                            objSterlingMapping.SterlingClientRefId = backgroundReportPackageNode.InnerText;
                                            objSterlingMapping.UniqueRequestId = backgroundReportPackageNode.InnerText;
                                            break;
                                        case "ScreeningStatus":
                                            #region ScreeningStatus
                                            foreach (XmlNode screeningsSummarynode in backgroundReportPackageNode.ChildNodes)
                                            {
                                                switch (screeningsSummarynode.Name)
                                                {
                                                    case "OrderStatus":
                                                        objSterlingMapping.OrderStatus = screeningsSummarynode.InnerText;
                                                        break;
                                                    case "Score":
                                                        objSterlingMapping.OrderScore = screeningsSummarynode.InnerText;
                                                        break;
                                                    case "DateOrderReceived":
                                                        objSterlingMapping.OrderReceivedDate = screeningsSummarynode.InnerText;
                                                        break;
                                                    case "AdditionalItems":
                                                        if (!object.Equals(screeningsSummarynode.Attributes["qualifier"], null))
                                                        {
                                                            if (screeningsSummarynode.Attributes["qualifier"].InnerText.Contains("DateOrderClosed"))
                                                                objSterlingMapping.OrderCloseDate = screeningsSummarynode.InnerText;
                                                        }
                                                        if (!object.Equals(screeningsSummarynode.Attributes["qualifier"], null))
                                                        {
                                                            if (screeningsSummarynode.Attributes["qualifier"].InnerText.Contains("DateOrderSubmitted"))
                                                                objSterlingMapping.OrderSubmittedDate = screeningsSummarynode.InnerText;
                                                        }
                                                        break;
                                                }
                                            }
                                            #endregion
                                            break;
                                        case "ScreeningsSummary":
                                            #region ScreeningsSummary
                                            foreach (XmlNode screeningsSummarynode in backgroundReportPackageNode.ChildNodes)
                                            {
                                                switch (screeningsSummarynode.Name)
                                                {
                                                    case "Organization":
                                                        objSterlingMapping.OrganizationId = screeningsSummarynode.InnerText;
                                                        break;
                                                    case "PersonalData":
                                                        foreach (XmlNode PersonalDatanode in screeningsSummarynode.ChildNodes)
                                                        {
                                                            switch (PersonalDatanode.Name)
                                                            {
                                                                case "PersonName":
                                                                    foreach (XmlNode personNamenode in PersonalDatanode.ChildNodes)
                                                                    {
                                                                        switch (personNamenode.Name)
                                                                        {
                                                                            case "GivenName":
                                                                                objSterlingMapping.FirstName = personNamenode.InnerText;
                                                                                break;
                                                                            case "MiddleName":
                                                                                objSterlingMapping.MiddelName = personNamenode.InnerText;
                                                                                break;
                                                                            case "FamilyName":
                                                                                if (personNamenode.Attributes["primary"].Value.Contains("true"))
                                                                                    objSterlingMapping.LastName = personNamenode.InnerText;
                                                                                break;
                                                                        }
                                                                    }
                                                                    break;
                                                                case "PostalAddress":
                                                                    foreach (XmlNode postalAddressnode in PersonalDatanode.ChildNodes)
                                                                    {
                                                                        switch (postalAddressnode.Name)
                                                                        {
                                                                            case "CountryCode":
                                                                                objSterlingMapping.FirstName = postalAddressnode.InnerText;
                                                                                break;
                                                                            case "PostalCode":
                                                                                objSterlingMapping.ZipCode = postalAddressnode.InnerText;
                                                                                break;
                                                                            case "Region":
                                                                                objSterlingMapping.State = postalAddressnode.InnerText;
                                                                                break;
                                                                            case "Municipality":
                                                                                objSterlingMapping.City = postalAddressnode.InnerText;
                                                                                break;
                                                                            case "DeliveryAddress":
                                                                                objSterlingMapping.Address = postalAddressnode.InnerText;
                                                                                break;
                                                                        }
                                                                    }
                                                                    break;
                                                                case "ContactMethod":
                                                                    foreach (XmlNode contactMethodnode in PersonalDatanode.ChildNodes)
                                                                    {
                                                                        switch (contactMethodnode.Name)
                                                                        {
                                                                            case "Telephone":
                                                                                objSterlingMapping.PhoneNumber = contactMethodnode.InnerText;
                                                                                break;
                                                                            case "InternetEmailAddress":
                                                                                objSterlingMapping.EmailId = contactMethodnode.InnerText;
                                                                                break;
                                                                        }
                                                                    }
                                                                    break;
                                                                case "DemographicDetail":
                                                                    foreach (XmlNode demographicDetailnode in PersonalDatanode.ChildNodes)
                                                                    {
                                                                        switch (demographicDetailnode.Name)
                                                                        {
                                                                            case "DateOfBirth":
                                                                                objSterlingMapping.DOB = demographicDetailnode.InnerText;
                                                                                break;
                                                                            case "GovernmentId":
                                                                                if (!object.Equals(demographicDetailnode.Attributes["issuingAuthority"], null))
                                                                                {
                                                                                    if (demographicDetailnode.Attributes["issuingAuthority"].InnerText == "DMV")
                                                                                    {
                                                                                        objSterlingMapping.LicenseNumber = demographicDetailnode.InnerText;
                                                                                    }
                                                                                }
                                                                                if (!object.Equals(demographicDetailnode.Attributes["countryCode"], null))
                                                                                {
                                                                                    objSterlingMapping.LicenseCountryCode = demographicDetailnode.Attributes["countryCode"].InnerText;
                                                                                }

                                                                                break;
                                                                        }
                                                                    }
                                                                    break;
                                                            }
                                                        }

                                                        break;
                                                }
                                            }
                                            #endregion
                                            break;
                                        case "Screenings":

                                            //foreach (XmlNode row in xm.SelectNodes("/BackgroundReport/BackgroundReportPackage/Screenings/Screening"))
                                            //{
                                            //    var rowName = row.SelectSingleNode("ScreeningResults").InnerText;
                                            //}
                                            foreach (XmlNode screeningsnode in backgroundReportPackageNode.ChildNodes)
                                            {
                                                switch (screeningsnode.Name)
                                                {
                                                    case "Screening":
                                                        foreach (XmlNode screeningnode in screeningsnode.ChildNodes)
                                                        {
                                                            switch (screeningnode.Name)
                                                            {
                                                                case "ScreeningResults":
                                                                    objSterlingMapping.ScreenResultText = screeningnode.InnerText;
                                                                    #region Professional License Data
                                                                    if (!string.IsNullOrEmpty(objSterlingMapping.PackageType))
                                                                    {
                                                                        if (objSterlingMapping.PackageType.Contains("Professional"))
                                                                        {
                                                                            XmlDocument xmlDoc = new XmlDocument();
                                                                            string myXML = objSterlingMapping.ScreenResultText;
                                                                            xmlDoc.LoadXml(myXML);
                                                                            objSterlingMapping.LicenseExpDate = xmlDoc.DocumentElement.SelectSingleNode("/Results/Result/Field[8]").InnerText;
                                                                            objSterlingMapping.LicenseStateCode = xmlDoc.DocumentElement.SelectSingleNode("/Results/Result/Field[6]").InnerText;
                                                                        }
                                                                    }
                                                                    #endregion
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            xm.Save(Convert.ToString(ConfigurationManager.AppSettings["SterlingResponseDocPath"], CultureInfo.InvariantCulture) + objSterlingMapping.UniqueRequestId + Shared.XmlFileExt);
                            objResponseFromDPToSterling = new ResponseFromDPToSterling() { EntityNoException = "true", EntityIdentifierMessage = DecisionPointR.EntityIdentifierMessage };
                            responseMessage = SuccessfullResponse(objResponseFromDPToSterling);
                        }
                        else
                        {
                            xm.Save(Convert.ToString(ConfigurationManager.AppSettings["SterlingResponseDocPath"], CultureInfo.InvariantCulture) + objSterlingMapping.UniqueRequestId + Shared.XmlFileExt);
                            objResponseFromDPToSterling = new ResponseFromDPToSterling() { EntityNoException = "false", ExceptionMessage = "dpuserId is null" };
                            responseMessage = FailedResponse(objResponseFromDPToSterling);
                        }
                        #endregion
                        //string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["SterlingResponseDocPath"], CultureInfo.InvariantCulture) + objSterlingMapping.UniqueRequestId;
                        //bool folderExists = Directory.Exists(folderDirectory);
                        //if (!folderExists)
                        //    Directory.CreateDirectory(folderDirectory);
                        //xm.Save(folderDirectory + "/" + objSterlingMapping.DPUserId + Shared.XmlFileExt);

                    }
                    else
                    {
                        objResponseFromDPToSterling = new ResponseFromDPToSterling() { ExceptionMessage = DecisionPointR.ContentTypeNotMatchMsg, EntityNoException = "false" };
                        responseMessage = FailedResponse(objResponseFromDPToSterling);
                    }
                }
                catch (Exception)
                {
                    objResponseFromDPToSterling = new ResponseFromDPToSterling() { ExceptionMessage = DecisionPointR.ExceptionMsg, EntityNoException = "false" };
                    responseMessage = FailedResponse(objResponseFromDPToSterling);
                }
                if (!object.Equals(nodeList, null))
                {
                    if (nodeList.Count > 0)
                    {
                        #region Save Master Record

                        objSterlingResponse = new SterlingResponse()
                        {
                            OrderStatus = objSterlingMapping.OrderStatus,
                            OrderScore = objSterlingMapping.OrderScore,
                            OrganizationId = objSterlingMapping.OrganizationId,
                            SterlingClientRefId = objSterlingMapping.SterlingClientRefId,
                            SterlingOrderId = objSterlingMapping.SterlingOrderId,
                            ResponseFileUrl = Convert.ToString(ConfigurationManager.AppSettings["SterlingResponseDocPath"], CultureInfo.InvariantCulture) + objSterlingMapping.UniqueRequestId + Shared.XmlFileExt,
                            Type = 1,
                            UniqueRequestId = objSterlingMapping.UniqueRequestId,
                            FirstName = objSterlingMapping.FirstName,
                            LastName = objSterlingMapping.LastName,
                            MiddelName = objSterlingMapping.MiddelName,
                            EmailId = objSterlingMapping.EmailId,
                            PhoneNumber = objSterlingMapping.PhoneNumber,
                            State = objSterlingMapping.State,
                            City = objSterlingMapping.City,
                            CountyCode = objSterlingMapping.CountyCode,
                            ZipCode = objSterlingMapping.ZipCode,
                            Address = objSterlingMapping.Address,
                            DOB = objSterlingMapping.DOB,
                            ReviewResultUrl = objSterlingMapping.ReviewResultUrl,
                            LicenseNumber = objSterlingMapping.LicenseNumber,
                            LicenseCountryCode = objSterlingMapping.LicenseCountryCode,
                            LicenseStateCode = objSterlingMapping.LicenseStateCode,
                            DpUserId = Convert.ToInt32(objSterlingMapping.DPUserId, CultureInfo.InvariantCulture)
                        };
                        if (!string.IsNullOrEmpty(objSterlingMapping.LicenseExpDate))
                        {
                            objSterlingResponse.LicenseExpDate = Convert.ToDateTime(objSterlingMapping.LicenseExpDate, CultureInfo.InvariantCulture);
                        }
                        objDecisionPointEngine = new DecisionPointEngine();
                        objSterlingWithDpResponse = objDecisionPointEngine.SaveSterlingLog(objSterlingResponse);

                        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Save Sterling Log", MethodBase.GetCurrentMethod().Name));
                        #endregion

                        #region Save Sccreening record

                        //save screening record which we get from sterling
                        foreach (XmlNode node in nodeList)
                        {
                            foreach (XmlNode backgroundReportPackageNode in node.ChildNodes)
                            {
                                switch (backgroundReportPackageNode.Name)
                                {
                                    case "Screenings":
                                        #region Screenings
                                        foreach (XmlNode screeningsnode in backgroundReportPackageNode.ChildNodes)
                                        {
                                            switch (screeningsnode.Name)
                                            {
                                                case "Screening":
                                                    #region Parse nodes and set in property
                                                    if (screeningsnode.Attributes["type"] != null)
                                                    {
                                                        objSterlingMapping.ScreenType = screeningsnode.Attributes["type"].Value;
                                                    }
                                                    if (screeningsnode.Attributes["qualifier"] != null)
                                                    {
                                                        objSterlingMapping.ScreenQualifier = screeningsnode.Attributes["qualifier"].Value;
                                                    }
                                                    foreach (XmlNode screeningnode in screeningsnode.ChildNodes)
                                                    {
                                                        switch (screeningnode.Name)
                                                        {
                                                            case "ClientReferenceId":
                                                                objSterlingMapping.ScreenClientRefId = screeningnode.InnerText;
                                                                break;
                                                            case "ScreeningStatus":
                                                                foreach (XmlNode screeningStatusnode in screeningnode.ChildNodes)
                                                                {
                                                                    switch (screeningStatusnode.Name)
                                                                    {
                                                                        case "OrderStatus":
                                                                            objSterlingMapping.ScreenOrderStatus = screeningStatusnode.InnerText;
                                                                            break;
                                                                        case "ResultStatus":
                                                                            objSterlingMapping.ScreenResultStatus = screeningStatusnode.InnerText;
                                                                            break;
                                                                    }
                                                                }
                                                                break;
                                                            case "ScreeningHistory":
                                                                if (screeningnode.InnerText.Contains("Open Date"))
                                                                {
                                                                    foreach (XmlNode screeningHistorynode in screeningnode.ChildNodes)
                                                                    {
                                                                        switch (screeningHistorynode.Name)
                                                                        {
                                                                            case "HistoryTimeStamp":
                                                                                objSterlingMapping.ScreenOpenDate = screeningHistorynode.InnerText;
                                                                                break;
                                                                        }
                                                                    }
                                                                }
                                                                else if (screeningnode.InnerText.Contains("Close Date"))
                                                                {
                                                                    foreach (XmlNode screeningHistorynode in screeningnode.ChildNodes)
                                                                    {
                                                                        switch (screeningHistorynode.Name)
                                                                        {
                                                                            case "HistoryTimeStamp":
                                                                                objSterlingMapping.ScreenCloseDate = screeningHistorynode.InnerText;
                                                                                break;
                                                                        }
                                                                    }
                                                                }

                                                                break;

                                                        }
                                                    }
                                                    #endregion
                                                    #region Save Screening record
                                                    objSterlingResponse = new SterlingResponse()
                                                        {
                                                            ScreenType = objSterlingMapping.ScreenType,
                                                            ScreenClientRefId = objSterlingMapping.ScreenClientRefId,
                                                            ScreenCloseDate = objSterlingMapping.ScreenCloseDate,
                                                            ScreenOpenDate = objSterlingMapping.ScreenOpenDate,
                                                            ScreenOrderStatus = objSterlingMapping.ScreenOrderStatus,
                                                            ScreenQualifier = objSterlingMapping.ScreenQualifier,
                                                            ScreenResultStatus = objSterlingMapping.ScreenResultStatus,
                                                            UniqueRequestId = objSterlingMapping.UniqueRequestId,
                                                            DpUserId = Convert.ToInt32(objSterlingMapping.DPUserId, CultureInfo.InvariantCulture),
                                                            DpCompanyId = objSterlingWithDpResponse.DPCompanyId,
                                                            PackageId = objSterlingWithDpResponse.PackageId
                                                        };
                                                    objDecisionPointEngine = new DecisionPointEngine();
                                                    objDecisionPointEngine.SaveSterlingScreeningLog(objSterlingResponse);
                                                    #endregion
                                                    break;
                                            }
                                        }
                                        #endregion
                                        break;
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(IHttpHandler).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return responseMessage;
        }

        private string SuccessfullResponse(ResponseFromDPToSterling objResponseFromDPToSterling)
        {
            string xmlTextFormat = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("ApplicationAcknowledgement");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.DocumentElement.SetAttribute("xmlns", "http://ns.hr-xml.org/2006-02-28");
                xmlDoc.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlDoc.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-16", null);
                xmlDoc.InsertBefore(xmldecl, rootNode);
                //ApplicationAcknowledgement-PayloadResponseSummary
                XmlNode payloadResponseSummaryNode = xmlDoc.CreateElement("PayloadResponseSummary");

                XmlNode processingTimestampNode = xmlDoc.CreateElement("ProcessingTimestamp");
                processingTimestampNode.InnerText = Convert.ToString(System.DateTime.Now, CultureInfo.InvariantCulture);

                XmlNode acknowledgementCreationTimestampNode = xmlDoc.CreateElement("AcknowledgementCreationTimestamp");
                acknowledgementCreationTimestampNode.InnerText = Convert.ToString(System.DateTime.Now, CultureInfo.InvariantCulture);

                payloadResponseSummaryNode.AppendChild(processingTimestampNode);
                payloadResponseSummaryNode.AppendChild(acknowledgementCreationTimestampNode);

                XmlNode payloadDispositionNode = xmlDoc.CreateElement("PayloadDisposition");

                XmlNode entityDispositionNode = xmlDoc.CreateElement("EntityDisposition");
                XmlNode entityIdentifierNode = xmlDoc.CreateElement("EntityIdentifier");
                XmlNode entityIdentifieridvalueNode = xmlDoc.CreateElement("IdValue");
                entityIdentifieridvalueNode.InnerText = objResponseFromDPToSterling.EntityIdentifierMessage;
                entityIdentifierNode.AppendChild(entityIdentifieridvalueNode);
                XmlNode entityNoExceptionNode = xmlDoc.CreateElement("EntityNoException");
                entityNoExceptionNode.InnerText = objResponseFromDPToSterling.EntityNoException;

                payloadDispositionNode.AppendChild(entityDispositionNode);
                entityDispositionNode.AppendChild(entityNoExceptionNode);
                entityDispositionNode.AppendChild(entityIdentifierNode);

                rootNode.AppendChild(payloadResponseSummaryNode);
                rootNode.AppendChild(payloadDispositionNode);
                xmlTextFormat = GetTextFromXMlFile(xmlDoc);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(IHttpHandler).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return xmlTextFormat;
        }

        private string FailedResponse(ResponseFromDPToSterling objResponseFromDPToSterling)
        {
            string xmlTextFormat = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("ApplicationAcknowledgement");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.DocumentElement.SetAttribute("xmlns", "http://ns.hr-xml.org/2006-02-28");
                xmlDoc.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                xmlDoc.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                XmlDeclaration xmldecl;
                xmldecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-16", null);
                xmlDoc.InsertBefore(xmldecl, rootNode);
                //ApplicationAcknowledgement-PayloadResponseSummary
                XmlNode payloadResponseSummaryNode = xmlDoc.CreateElement("PayloadResponseSummary");

                XmlNode processingTimestampNode = xmlDoc.CreateElement("ProcessingTimestamp");
                XmlAttribute processingTimestampNodeattribute = xmlDoc.CreateAttribute("description");
                processingTimestampNodeattribute.Value = DecisionPointCAL.DecisionPointR.processingTimestampNodeAttributeValue;
                processingTimestampNode.Attributes.Append(processingTimestampNodeattribute);
                processingTimestampNode.InnerText = Convert.ToString(System.DateTime.Now, CultureInfo.InvariantCulture);

                XmlNode acknowledgementCreationTimestampNode = xmlDoc.CreateElement("AcknowledgementCreationTimestamp");
                acknowledgementCreationTimestampNode.InnerText = Convert.ToString(System.DateTime.Now, CultureInfo.InvariantCulture);

                payloadResponseSummaryNode.AppendChild(processingTimestampNode);
                payloadResponseSummaryNode.AppendChild(acknowledgementCreationTimestampNode);

                XmlNode payloadDispositionNode = xmlDoc.CreateElement("PayloadDisposition");

                XmlNode entityDispositionNode = xmlDoc.CreateElement("EntityDisposition");
                XmlNode entityNoExceptionNode = xmlDoc.CreateElement("EntityNoException");
                entityNoExceptionNode.InnerText = objResponseFromDPToSterling.EntityNoException;

                XmlNode entityExceptionNode = xmlDoc.CreateElement("EntityException");
                XmlNode exceptionNode = xmlDoc.CreateElement("Exception");
                XmlNode exceptionMessageNode = xmlDoc.CreateElement("ExceptionMessage");
                exceptionMessageNode.InnerText = objResponseFromDPToSterling.ExceptionMessage;
                entityExceptionNode.AppendChild(exceptionNode);
                exceptionNode.AppendChild(exceptionMessageNode);

                payloadDispositionNode.AppendChild(entityDispositionNode);
                entityDispositionNode.AppendChild(entityNoExceptionNode);
                entityDispositionNode.AppendChild(entityExceptionNode);

                rootNode.AppendChild(payloadResponseSummaryNode);
                rootNode.AppendChild(payloadDispositionNode);
                xmlTextFormat = GetTextFromXMlFile(xmlDoc);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(IHttpHandler).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return xmlTextFormat;
        }

        /// <summary>
        /// get Text Formate from XML File
        /// </summary>
        /// <param name="objXmlDocument"></param>
        /// <returns>string</returns>
        /// <CreatedBy>Bobi</CreatedBy>
        /// <CreatedDate>19 Feb 2015</CreatedDate>
        private string GetTextFromXMlFile(XmlDocument objXmlDocument)
        {
            string xmlTextFormat = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!object.Equals(objXmlDocument, null))
                {
                    using (StringWriter sw = new StringWriter())
                    {
                        using (XmlTextWriter tx = new XmlTextWriter(sw))
                        {
                            objXmlDocument.WriteTo(tx);
                            xmlTextFormat = sw.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(IHttpHandler).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return xmlTextFormat;
        }

        #endregion
    }
}