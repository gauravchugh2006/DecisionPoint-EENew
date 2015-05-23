using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using DecisionPointAPIService.Models;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Response;
using DecisionPointBAL.Implementation.Request;
using System.Web.Script.Serialization;
using System.Web;

namespace DecisionPointAPIService
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode
        = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service
    {

        #region Global Variables

        DecisionPointEngine objDecisionPointEngine = null;
        APIMasterResponseDetails objAPIMasterResponseDetails = null;
        APIMasterResponse objAPIMasterResponse = null;
        IList<APIMasterResponseDetails> objMasterResponseDetails = null;
        APIComplianceStatusRequest objAPIComplianceStatusRequest = null;
        APILogRequest objAPILogRequest = null;

        #endregion

        #region Send Invitation
        /// <summary>
        /// Send invite to users
        /// </summary>
        /// <param name="objUserRequestDetails">UserRequestDetails</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>13 feb 2015</CreatedDate>
        /// <returns>UserResponseDetails</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SendInvitation", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public APIMasterResponseDetails SendInvitation(UserRequestDetails objUserRequestDetails)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objAPIMasterResponse = new APIMasterResponse();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objUserRequestDetails.ApiUserName, objUserRequestDetails.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objAPILogRequest = new APILogRequest()
                    {
                        APIUserName = objUserRequestDetails.ApiUserName,
                        APIPassword = objUserRequestDetails.ApiPassword,
                        RefrenceId = objUserRequestDetails.ReferenceId,
                        RequestData = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString,
                    };
                    objAPIMasterResponse = objDecisionPointEngine.InsertAPILog(objAPILogRequest);
                    if (objAPIMasterResponse.ResultCode != 218)
                    {
                        DSTInviteRequest objDSTInviteRequest = new DSTInviteRequest()
                        {
                            FirstName = objUserRequestDetails.FirstName,
                            LastName = objUserRequestDetails.LastName,
                            EmailId = objUserRequestDetails.EmailId,
                            RoleTypeId = objUserRequestDetails.RoleTypeId,
                            ClientId = objUserRequestDetails.ClientId,
                            SubClientId = objUserRequestDetails.SubClientId,
                            PackageId = objUserRequestDetails.PackageId,
                            ProductId = objUserRequestDetails.ProductId,
                            PaymentTypeId = objUserRequestDetails.PaymentTypeId,
                            ICTypeId = objUserRequestDetails.ICTypeId,
                            BusinessName = objUserRequestDetails.BusinessName,
                            RoleType = objUserRequestDetails.RoleType,
                        };
                        objAPIMasterResponse = objDecisionPointEngine.SendInvitationByDST(objDSTInviteRequest);
                    }
                }
                if (!object.Equals(objAPIMasterResponse, null))
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                }
                return objAPIMasterResponseDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Used to Send the bulk invitaion
        /// </summary>
        /// <param name="objUserRequestDetails"></param>
        /// <returns>IList<APIMasterResponseDetails></returns>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>13 feb 2015</CreatedDate>
        /// <ModifiedBy>Bobi</ModifiedBy>
        /// <ModifiedDate>10 mar 2015</ModifiedDate>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SendBulkInvitation", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> SendBulkInvitation(List<UserRequestDetails> objUserRequestDetails)
        {
            try
            {

                IList<APIMasterResponseDetails> objresult = new List<APIMasterResponseDetails>();
                //apply loop on User collection
                foreach (var item in objUserRequestDetails)
                {
                    objAPIMasterResponse = new APIMasterResponse();
                    objAPIMasterResponseDetails = new APIMasterResponseDetails();
                    DSTInviteRequest objDSTInviteRequest = new DSTInviteRequest()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        EmailId = item.EmailId,
                        RoleType = item.RoleType,
                        ClientId = item.ClientId,
                        SubClientId = item.SubClientId,
                        PackageId = item.PackageId,
                        ProductId = item.ProductId,
                        PaymentTypeId = item.PaymentTypeId,
                        ICTypeId = item.ICTypeId,
                        BusinessName = item.BusinessName,
                        RoleTypeId = item.RoleTypeId,
                    };
                    objDecisionPointEngine = new DecisionPointEngine();

                    //call method for send invitation
                    objAPIMasterResponse = objDecisionPointEngine.SendInvitationByDST(objDSTInviteRequest);
                    if (!object.Equals(objAPIMasterResponse, null))
                    {
                        objAPIMasterResponseDetails = new APIMasterResponseDetails()
                        {
                            ResultId = objAPIMasterResponse.ResultId,
                            ResultCode = objAPIMasterResponse.ResultCode
                        };
                    }
                    //added result in result of user collection
                    objresult.Add(objAPIMasterResponseDetails);
                }
                return objresult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region VerifyAssignment
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "VerifyComplianceStatus", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public APIMasterResponseDetails VerifyComplianceStatus(APIComplianceStatusRequestDetails objAPIComplianceStatusRequestDetails)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objAPIMasterResponse = new APIMasterResponse();
                // objAPIMasterResponseDetails = new APIMasterResponseDetails();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPIComplianceStatusRequestDetails.ApiUserName, objAPIComplianceStatusRequestDetails.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objAPILogRequest = new APILogRequest()
                    {
                        APIUserName = objAPIComplianceStatusRequestDetails.ApiUserName,
                        APIPassword = objAPIComplianceStatusRequestDetails.ApiPassword,
                        RefrenceId = objAPIComplianceStatusRequestDetails.ReferenceId,
                        RequestData = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString,
                    };
                    objAPIMasterResponse = objDecisionPointEngine.InsertAPILog(objAPILogRequest);
                    if (objAPIMasterResponse.ResultCode != 218)
                    {
                        objAPIComplianceStatusRequest = new APIComplianceStatusRequest()
                        {
                            ClientId = objAPIComplianceStatusRequestDetails.ClientId,
                            SubClientId = objAPIComplianceStatusRequestDetails.SubClientId,
                            //PackageId = objAPIComplianceStatusRequestDetails.PackageId,
                            UserId = objAPIComplianceStatusRequestDetails.UserId
                        };
                        objDecisionPointEngine = new DecisionPointEngine();
                        objAPIMasterResponse = objDecisionPointEngine.VerifyAssignment(objAPIComplianceStatusRequest);
                    }
                }
                if (!object.Equals(objAPIMasterResponse, null))
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode,
                        JCRList = objAPIMasterResponse.APIMasterResponseJCRInfoList
                    };
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string output = jss.Serialize(objAPIMasterResponseDetails);
                return objAPIMasterResponseDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Compliance Sections
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "BatchVerifyComplianceStatus", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> BatchVerifyComplianceStatus(APIComplianceStatusRequestDetails objAPIComplianceStatusRequestDetails)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                IList<APIMasterResponseDetails> responselist = new List<APIMasterResponseDetails>();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPIComplianceStatusRequestDetails.ApiUserName, objAPIComplianceStatusRequestDetails.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objAPILogRequest = new APILogRequest()
                    {
                        APIUserName = objAPIComplianceStatusRequestDetails.ApiUserName,
                        APIPassword = objAPIComplianceStatusRequestDetails.ApiPassword,
                        RefrenceId = objAPIComplianceStatusRequestDetails.ReferenceId,
                        RequestData = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString,
                    };
                    objAPIMasterResponse = objDecisionPointEngine.InsertAPILog(objAPILogRequest);
                    if (objAPIMasterResponse.ResultCode != 218)
                    {
                        objAPIComplianceStatusRequest = new APIComplianceStatusRequest()
                        {
                            ClientId = objAPIComplianceStatusRequestDetails.ClientId,
                            SubClientId = objAPIComplianceStatusRequestDetails.SubClientId,
                            //PackageId = objAPIComplianceStatusRequestDetails.PackageId,
                            CandidateIdsCol = objAPIComplianceStatusRequestDetails.CandidateIdsCol
                        };
                        responselist = objDecisionPointEngine.GetCandidatesComplianceStatus(objAPIComplianceStatusRequest).Select(x => new APIMasterResponseDetails
                             {
                                 ResultId = x.ResultId,
                                 ResultCode = x.ResultCode,
                                 JCRList = x.APIMasterResponseJCRInfoList
                             }).ToList();
                    }
                }
                else
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                    responselist.Add(objAPIMasterResponseDetails);
                }
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string output = jss.Serialize(responselist);
                return responselist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Masters
        /// <summary>
        /// get vendor type list
        /// </summary>
        /// <param name="clientId">companyId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>MasterResponseDetails</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetICList", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> GetICList(APICredenrials objAPICredenrials)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objAPIMasterResponseDetails = new APIMasterResponseDetails();
                objMasterResponseDetails = new List<APIMasterResponseDetails>();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPICredenrials.ApiUserName, objAPICredenrials.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objMasterResponseDetails = objDecisionPointEngine.GetVendorTypeList(objAPICredenrials.ClientId).Select(x => new APIMasterResponseDetails { Name = x.Name, Id = x.Id }).ToList();
                }
                else
                {
                    objAPIMasterResponseDetails.ResultId = objAPIMasterResponse.ResultId;
                    objAPIMasterResponseDetails.ResultCode = objAPIMasterResponse.ResultCode;
                    objMasterResponseDetails.Add(objAPIMasterResponseDetails);
                }
                return objMasterResponseDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// GetRoleType
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>MasterResponseDetails</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetRoleType", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> GetRoleType(APICredenrials objAPICredenrials)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objMasterResponseDetails = new List<APIMasterResponseDetails>();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPICredenrials.ApiUserName, objAPICredenrials.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objMasterResponseDetails = objDecisionPointEngine.GetRoleType().Select(x => new APIMasterResponseDetails { Name = x.Name, Id = x.Id }).ToList();
                }
                else
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                    objMasterResponseDetails.Add(objAPIMasterResponseDetails);
                }
                return objMasterResponseDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// GetPaymentType
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>MasterResponseDetails</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPaymentType", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> GetPaymentType(APICredenrials objAPICredenrials)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objMasterResponseDetails = new List<APIMasterResponseDetails>();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPICredenrials.ApiUserName, objAPICredenrials.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objMasterResponseDetails = objDecisionPointEngine.GetPaymentType().Select(x => new APIMasterResponseDetails { Name = x.Name, Id = x.Id }).ToList();
                }
                else
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                    objMasterResponseDetails.Add(objAPIMasterResponseDetails);
                }
                return objMasterResponseDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// GetPaymentType
        /// </summary>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>25 feb 2015</CreatedDate>
        /// <returns>MasterResponseDetails</returns>
        /// 
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPackageIds", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public IList<APIMasterResponseDetails> GetPackageIds(APICredenrials objAPICredenrials)
        {

            objDecisionPointEngine = new DecisionPointEngine();
            objMasterResponseDetails = new List<APIMasterResponseDetails>();
            objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objAPICredenrials.ApiUserName, objAPICredenrials.ApiPassword);
            if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
            {
                objMasterResponseDetails = objDecisionPointEngine.GetBackgroundCheckPackages().Where(x => x.PacakgeType == 0).Select(x => new APIMasterResponseDetails { Name = x.BackgroundTitle, Id = x.Id }).ToList();
            }
            else
            {
                objAPIMasterResponseDetails = new APIMasterResponseDetails()
                {
                    ResultId = objAPIMasterResponse.ResultId,
                    ResultCode = objAPIMasterResponse.ResultCode
                };
                objMasterResponseDetails.Add(objAPIMasterResponseDetails);
            }
            return objMasterResponseDetails;
            //Random rd = new Random();
            //Student aStudent = new Student
            //{
            //    ID = System.Convert.ToInt16(id),
            //    Name = "Name No. " + id,
            //    Score = Convert.ToInt16(60 + rd.NextDouble() * 40),
            //    State = "GA"
            //};

            //return aStudent;

        }
        #endregion

        #region Add UpdateUser
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddUpdateUser", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public APIMasterResponseDetails AddUpdateUser(UserRequestDetails objUserRequestDetails)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objAPIMasterResponse = new APIMasterResponse();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objUserRequestDetails.ApiUserName, objUserRequestDetails.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objAPILogRequest = new APILogRequest()
                    {
                        APIUserName = objUserRequestDetails.ApiUserName,
                        APIPassword = objUserRequestDetails.ApiPassword,
                        RefrenceId = objUserRequestDetails.ReferenceId,
                        RequestData = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString,
                    };
                    objAPIMasterResponse = objDecisionPointEngine.InsertAPILog(objAPILogRequest);
                    if (objAPIMasterResponse.ResultCode != 218)
                    {
                        DSTInviteRequest objDSTInviteRequest = new DSTInviteRequest()
                        {
                            FirstName = objUserRequestDetails.FirstName,
                            LastName = objUserRequestDetails.LastName,
                            EmailId = objUserRequestDetails.EmailId,
                            EntityId = objUserRequestDetails.EntityId,
                            BusinessName = objUserRequestDetails.BusinessName,
                            StaffId = objUserRequestDetails.StaffId,
                            IsMailSent = true,
                            RoleType = objUserRequestDetails.RoleType,
                        };
                        objAPIMasterResponse = objDecisionPointEngine.AddUpdateUser(objDSTInviteRequest);
                    }
                }
                if (!object.Equals(objAPIMasterResponse, null))
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                }
                return objAPIMasterResponseDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Sub Client
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddUpdateSubClient", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public APIMasterResponseDetails AddUpdateSubClient(UserRequestDetails objUserRequestDetails)
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objAPIMasterResponse = new APIMasterResponse();
                objAPIMasterResponse = objDecisionPointEngine.ValidateAPIUser(objUserRequestDetails.ApiUserName, objUserRequestDetails.ApiPassword);
                if (objAPIMasterResponse.ResultCode != 216 && objAPIMasterResponse.ResultCode != 217)
                {
                    objAPILogRequest = new APILogRequest()
                    {
                        APIUserName = objUserRequestDetails.ApiUserName,
                        APIPassword = objUserRequestDetails.ApiPassword,
                        RefrenceId = objUserRequestDetails.ReferenceId,
                        RequestData = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString,
                    };
                    objAPIMasterResponse = objDecisionPointEngine.InsertAPILog(objAPILogRequest);
                    if (objAPIMasterResponse.ResultCode != 218)
                    {
                        DSTInviteRequest objDSTInviteRequest = new DSTInviteRequest()
                        {
                            SubClientId = objUserRequestDetails.SubClientId,
                            ClientId = objUserRequestDetails.ClientId,
                            BusinessName = objUserRequestDetails.BusinessName
                        };
                        objAPIMasterResponse = objDecisionPointEngine.AddUpdateSubClient(objDSTInviteRequest);
                    }
                }
                if (!object.Equals(objAPIMasterResponse, null))
                {
                    objAPIMasterResponseDetails = new APIMasterResponseDetails()
                    {
                        ResultId = objAPIMasterResponse.ResultId,
                        ResultCode = objAPIMasterResponse.ResultCode
                    };
                }
                return objAPIMasterResponseDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}