using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DecisionPoint.Models;
using DecisionPointBAL.Core;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;
using DecisionPointCAL.Common;
using System.Net;
using System.Xml;

namespace DecisionPoint.Controllers
{
    public class CompanyController : Controller
    {
        #region Global Valriables
        //log declatration
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        StringBuilder logMessage = null;
        DecisionPointEngine decisionPointEngine = null;
        ActionResult objactionResult = null;
        RegisterStep2 registerStep2 = null;
        int userId = 0;
        string companyId = string.Empty;
        string parentCompanyId = string.Empty;
        int parentUserId = 0;
        string userType = string.Empty;
        ConfiguratonSettingRequest objConfiguratonSettingRequest = null;
        ViewModel objViewModel = null;
        CoverageAreaModel objSaveCoverageArea = null;
        UserDashBoardResponse objUserDashBoardResponse = null;
        BusinessCryptography businessCryptography = null;
        PackagePaymentModel objPackagePaymentModel = null;
        BackGroundCheckMasterRequest objBackGroundCheckMasterRequest = null;
        MailInviteFormat objMailInviteFormat = null;
        SterlingModel objSterlingModel = null;
        RegisterStep1 registerStep1 = null;
        #endregion


        /// <summary>
        /// get: load page to setup company profile
        /// </summary>
        ///  <createdBy>sumit saurav</createdBy>
        /// <createdDate>13 nov 2013</createdDate>
        /// <returns>return to company profile</returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult SetupCompnayProfile()
        {
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                registerStep1 = new RegisterStep1();
                registerStep1.StateList = decisionPointEngine.GetStateList();

                userId = Convert.ToInt32(Session["UserId"]);
                CompanyProfileResponse comp = decisionPointEngine.GetCompanyProfileDetails(userId);
                businessCryptography = new BusinessCryptography();
                #region Parameter Assignment

                if (!object.Equals(comp,CultureInfo.InvariantCulture))
                {

                    registerStep1.BusinessName = comp.BusinessName;
                    registerStep1.BusinessAddress = comp.BusinessAddress;
                    registerStep1.Direction = comp.Direction;
                    registerStep1.StreetName = comp.StreetName;
                    registerStep1.ZipCode = comp.ZipCode;
                    registerStep1.Email = comp.Email;
                    registerStep1.SecurityAnswer1 = comp.SecurityAnswer1;
                    registerStep1.SecurityAnswer2 = comp.SecurityAnswer2;
                    registerStep1.SecurityAnswer3 = comp.SecurityAnswer3;
                    registerStep1.StateId = comp.StateId;
                    registerStep1.CityId = comp.CityId;
                    registerStep1.CityName = comp.CityName;
                    registerStep1.StreetNumber = comp.StreetNumber;
                    registerStep1.CerificationNumber = comp.CerificationNumber;
                    registerStep1.FlowType = comp.FlowType;
                    if (!object.Equals(comp.CertificateExpDate,null))
                    {
                        registerStep1.CertificateExpDate =comp.CertificateExpDate;
                    }
                    registerStep1.CertifyingAgency = comp.CertifyingAgency;
                    registerStep1.BusinessClass = comp.BusinessClass;
                    if (!Convert.ToBoolean(Session["IsTemp"]))
                    {
                        registerStep1.Password = businessCryptography.base64Decode2(comp.Password);
                        registerStep1.ConfirmPassword = businessCryptography.base64Decode2(comp.Password);
                    }
                    if (string.IsNullOrEmpty(comp.CompanyLogo))
                    {
                        registerStep1.CompanyLogo = "no-img-icon.jpg";
                    }
                    else
                    {
                        registerStep1.CompanyLogo = comp.CompanyLogo;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(comp.OfficePhone)) && Convert.ToString(comp.OfficePhone).Length == 10)
                    {
                        registerStep1.OfficePhone1 = Convert.ToString(comp.OfficePhone).Substring(0, 3);
                        registerStep1.OfficePhone2 = Convert.ToString(comp.OfficePhone).Substring(3, 3);
                        registerStep1.OfficePhone3 = Convert.ToString(comp.OfficePhone).Substring(6, 4);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(comp.fax)) && Convert.ToString(comp.fax).Length == 10)
                    {
                        registerStep1.fax1 = Convert.ToString(comp.fax).Substring(0, 3);
                        registerStep1.fax2 = Convert.ToString(comp.fax).Substring(3, 3);
                        registerStep1.fax3 = Convert.ToString(comp.fax).Substring(6, 4);
                    }
                }
                #endregion
                if (!string.IsNullOrEmpty(Convert.ToString(Session["BusinessName"])))
                {
                    registerStep1.BusinessName = Convert.ToString(Session["BusinessName"]);
                }
                registerStep1.RedirectType = Request["Type"];
                registerStep1.PaymentType = Request["Res"];
                registerStep1.BusinessClassDetails= decisionPointEngine.GetBusinessClass();
                ViewData.Model = registerStep1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View();
        }

        /// <summary>
        /// post values to save company profile info
        /// </summary>
        /// <param name="registerStep1">parameters of model register step1</param>
        ///  <createdBy>sumit saurav & bobi</createdBy>
        /// <createdDate>13 Nov 2013</createdDate>
        /// <returns>return company profile page with sucess and fail message</returns>
        [HttpPost]
        public ActionResult SetCompanyProfile(RegisterStep1 registerStep1)
        {
            logMessage = new StringBuilder();
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                if (!object.Equals(Session["UserId"], null))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userType = Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture);
                    if (ModelState.IsValid)
                    {
                        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                        string FileWithId = string.Empty;
                        CompanyProfileRequest companyProfileRequest = null;
                        //save person photo after rename
                        #region Save Company Logo
                        if (Request.Files.Count > 0)
                        {
                            var file = Request.Files[0];
                            if (file != null && file.ContentLength > 0)
                            {
                                FileWithId = companyId + DecisionPointR.CmpLogo + Path.GetExtension(file.FileName);
                                string NewPath = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["Regcompanylogo"]), FileWithId);
                                file.SaveAs(NewPath);
                                Image imgOriginal = Image.FromFile(NewPath);
                                BusinessCore.GetThumbnailImage(imgOriginal, NewPath);
                            }
                        }
                        if (string.IsNullOrEmpty(FileWithId))
                        {
                            FileWithId = registerStep1.CompanyLogo;
                        }
                        #endregion
                        companyProfileRequest = new CompanyProfileRequest()
                        {
                            #region Parmeters

                            UserId = userId,
                            BusinessName = (string.IsNullOrEmpty(registerStep1.BusinessName)) ? string.Empty : registerStep1.BusinessName.Trim(),
                            OfficePhone = registerStep1.OfficePhone1 + registerStep1.OfficePhone2 + registerStep1.OfficePhone3,
                            BusinessAddress = (string.IsNullOrEmpty(registerStep1.BusinessAddress)) ? string.Empty : registerStep1.BusinessAddress.Trim(),
                            Direction = registerStep1.Direction,
                            StreetName = registerStep1.StreetName,
                            StateId = registerStep1.StateId,
                            CityId = registerStep1.CityId,
                            CityName = (string.IsNullOrEmpty(registerStep1.CityName)) ? string.Empty : registerStep1.CityName.Trim(),
                            ZipCode = registerStep1.ZipCode,
                            fax = registerStep1.fax1 + registerStep1.fax2 + registerStep1.fax3,
                            Email = registerStep1.Email.Trim(),
                            StreetNumber = registerStep1.StreetNumber,
                            CompanyLogo = FileWithId,
                            UserType = userType,
                            CerificationNumber = registerStep1.CerificationNumber,
                            CertificateExpDate = registerStep1.CertificateExpDate,
                            CertifyingAgency = registerStep1.CertifyingAgency,
                            BusinessClass = registerStep1.BusinessClass,
                            #endregion
                        };
                        if (userType.Equals(Shared.NonClient) || userType.Equals(Shared.IC))
                        {
                            companyProfileRequest.Password = registerStep1.Password;
                        }
                        else { companyProfileRequest.Password = string.Empty; }
                        #region Redirect Conditions

                        Session["logopath"] = ViewModel.GetSiteRoot() + Convert.ToString(ConfigurationManager.AppSettings["companylogo"]) + FileWithId;
                        int result = decisionPointEngine.SetCompanyProfile(companyProfileRequest);
                        if (result > 0)
                        {
                            if (Convert.ToString(Session["UserType"]).Equals(Shared.NonClient))
                            {
                                Session["IsTemp"] = "false";
                                objactionResult = RedirectToAction("LisenceAggrement", "Company", new { Type = Shared.NonClient });
                            }
                            else
                            {
                                if (registerStep1.RedirectType != null && registerStep1.PaymentType != null)
                                {
                                    Session["IsTemp"] = "false";
                                    if (registerStep1.PaymentType == "2")
                                    {
                                        objactionResult = RedirectToAction("LisenceAggrement", "Company", new { Type = registerStep1.RedirectType, Res = registerStep1.PaymentType });
                                    }
                                    else
                                    {
                                        if (Convert.ToString(Session["UserType"]).Equals(Shared.IC))
                                        {
                                            objactionResult = RedirectToAction("LisenceAggrement", "Company", new { Type = registerStep1.RedirectType, Res = registerStep1.PaymentType });
                                        }
                                        else
                                        {
                                            //int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                                            //result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.IC, Convert.ToString(Session["CompanyId"]));
                                            objactionResult = RedirectToAction("Welcome", "Company");
                                        }
                                    }
                                }
                                else
                                {
                                    objactionResult = RedirectToAction("AdminAccount", "Company");
                                }
                            }
                        }
                        else
                        {
                            registerStep1.StateList = decisionPointEngine.GetStateList();
                            objactionResult = View("SetupCompnayProfile", registerStep1);
                        }
                        #endregion
                    }
                    else
                    {
                        registerStep1.StateList = decisionPointEngine.GetStateList();
                        objactionResult = View("SetupCompnayProfile", registerStep1);
                    }
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
        /// <summary>
        /// load create services page
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>13 dec 2013</createdDate>
        /// <returns>returns to create services page</returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CreateServices()
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                registerStep2.StateList = decisionPointEngine.GetStateList().ToList();
                ViewData.Model = registerStep2;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View();
        }
        /// <summary>
        /// post data to server for saving create services
        /// </summary>
        /// <param name="registerStep2">parameters of register step2</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>13 Nov 2013</createdDate>
        /// <returns>returns sucess or fail message with save</returns>
        [HttpPost]
        public ActionResult CreateServices(RegisterStep2 registerStep2)
        {
            return View();
        }

        /// <summary>
        /// loads admin account section
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>13 Nov 2013</createdDate>
        /// <returns>return admin account section</returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult AdminAccount()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    decisionPointEngine = new DecisionPointEngine();
                    userId = Convert.ToInt32(Session["UserId"]);
                    RegisterStep3 registerStep3 = new RegisterStep3();
                    IList<AdminProfileResponse> admin = decisionPointEngine.GetAdminProfile(userId).ToList();
                    businessCryptography = new BusinessCryptography();
                    registerStep3.SecurityList = decisionPointEngine.GetSecurityQuestion().ToList();
                    #region Model Assignment

                    if (admin != null)
                    {
                        registerStep3.FirstName = admin[0].FirstName;
                        registerStep3.MiddleName = admin[0].MiddleName;
                        registerStep3.LastName = admin[0].LastName;
                        registerStep3.NickName = admin[0].NickName;
                        registerStep3.Email = admin[0].Email;
                        registerStep3.ConfirmEmail = admin[0].Email;
                        if (!Convert.ToBoolean(Session["IsTemp"]))
                        {
                            registerStep3.Password = businessCryptography.base64Decode2(admin[0].Password);
                            registerStep3.ConfirmPassword = businessCryptography.base64Decode2(admin[0].Password);
                        }
                        registerStep3.SecurityAnswer1 = admin[0].SecurityAnswer1;
                        registerStep3.SecurityAnswer3 = admin[0].SecurityAnswer3;
                        registerStep3.SecurityAnswer2 = admin[0].SecurityAnswer2;
                        if (!string.IsNullOrEmpty(Convert.ToString(admin[0].OfficePhone)) && Convert.ToString(admin[0].OfficePhone).Length == 10)
                        {
                            registerStep3.OfficePhone1 = Convert.ToString(admin[0].OfficePhone).Substring(0, 3);
                            registerStep3.OfficePhone2 = Convert.ToString(admin[0].OfficePhone).Substring(3, 3);
                            registerStep3.OfficePhone3 = Convert.ToString(admin[0].OfficePhone).Substring(6, 4);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(admin[0].CellNumber)) && Convert.ToString(admin[0].CellNumber).Length == 10)
                        {
                            registerStep3.CellNumber1 = Convert.ToString(admin[0].CellNumber).Substring(0, 3);
                            registerStep3.CellNumber2 = Convert.ToString(admin[0].CellNumber).Substring(3, 3);
                            registerStep3.CellNumber3 = Convert.ToString(admin[0].CellNumber).Substring(6, 4);
                        }


                    }
                    #endregion
                    ViewData.Model = registerStep3;
                    return View();
                }
                else
                {
                    objactionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;
        }
        /// <summary>
        /// set admin profile
        /// </summary>
        /// <param name="registerStep3">list of parameters of model RegisterStep3</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>20 Nov 2013</createdDate>
        /// <returns>return to page with sucess and fail message</returns>
        [HttpPost]
        public ActionResult SetAdminProfile(RegisterStep3 registerStep3)
        {
            logMessage = new StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    decisionPointEngine = new DecisionPointEngine();
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                    AdminProfileRequest adminProfileRequest = new AdminProfileRequest()
                    {
                        #region Paremeters

                        UserId = Convert.ToInt32(Session["UserId"]),
                        FirstName = (string.IsNullOrEmpty(registerStep3.FirstName)) ? string.Empty : registerStep3.FirstName.Trim(),
                        MiddleName = (string.IsNullOrEmpty(registerStep3.MiddleName)) ? string.Empty : registerStep3.MiddleName.Trim(),
                        LastName = (string.IsNullOrEmpty(registerStep3.LastName)) ? string.Empty : registerStep3.LastName.Trim(),
                        NickName = (string.IsNullOrEmpty(registerStep3.NickName)) ? string.Empty : registerStep3.NickName.Trim(),
                        Password = (string.IsNullOrEmpty(registerStep3.Password)) ? string.Empty : registerStep3.Password.Trim(),
                        Email = registerStep3.Email.Trim(),
                        CellNumber = registerStep3.CellNumber1 + registerStep3.CellNumber2 + registerStep3.CellNumber3,
                        SecurityQuestion1 = registerStep3.SecurityQuestion1,
                        SecurityQuestion2 = registerStep3.SecurityQuestion2,
                        SecurityQuestion3 = registerStep3.SecurityQuestion3,
                        SecurityAnswer1 = registerStep3.SecurityAnswer1,
                        SecurityAnswer2 = registerStep3.SecurityAnswer2,
                        SecurityAnswer3 = registerStep3.SecurityAnswer3,
                        OfficePhone = registerStep3.OfficePhone1 + registerStep3.OfficePhone2 + registerStep3.OfficePhone3,
                        CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture),
                        #endregion
                    };
                    Session["IsTemp"] = "false";
                    int result = decisionPointEngine.SetAdminProfile(adminProfileRequest);
                    if (result > 0)
                    {
                        return RedirectToAction("LisenceAggrement", "Company", new { });
                    }
                    else
                    {
                        return View("AdminAccount", registerStep3);
                    }
                }
                else
                {
                    return View("AdminAccount", registerStep3);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return RedirectToAction("LisenceAggrement", "Company", new { });
        }




        /// <summary>
        /// get city list
        /// </summary>
        /// <param name="stateAbbre">state abbreviations</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        [HttpGet]
        public JsonResult GetCityListByState(string stateAbbre)
        {
            logMessage = new StringBuilder();
            registerStep1 = new RegisterStep1();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (stateAbbre != null)
                {
                    registerStep1.CityList = decisionPointEngine.GetCityListByState(stateAbbre).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep1.CityList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// to load payemnt page
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>20 dec 2013</createdDate>
        /// <returns>return to payment page</returns>
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult MakePayment()
        {
            logMessage = new StringBuilder();
            Payment payment = new Payment();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                bool isPayment = decisionPointEngine.getPaymentStatus(Convert.ToInt32(Session["UserId"]));
                if (isPayment != true)
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    payment.BusinessName = Convert.ToString(Session["BusinessName"]);
                    int ParentUserId = decisionPointEngine.GetParentUserId(companyId, Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                    payment.ParentUserType = decisionPointEngine.GetParentUserType(ParentUserId);
                    payment.ShowCardDiv = decisionPointEngine.getPaymentAmount(companyId).ToList().Select(x => x.IsInvoice).FirstOrDefault();

                    Session["checked"] = "checked";
                    objConfiguratonSettingRequest = new ConfiguratonSettingRequest();
                    objConfiguratonSettingRequest = decisionPointEngine.GetConfigSetting(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture));
                    payment.RootUrl = ViewModel.GetSiteRoot();
                    payment.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        payment.IsCoverageAreaApply = objConfiguratonSettingRequest.IsCoveragearea;
                    }
                    payment.PayPalAccount = System.Configuration.ConfigurationManager.AppSettings["PayPalAccount"];
                }
                else
                {
                    return RedirectToAction("LogOut", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View(payment);

        }

        /// <summary>
        /// make payment
        /// </summary>
        /// <param name="payment">paramters for payment gateway</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>return with payemnt success or fail message</returns>
        [HttpPost]
        public ActionResult MakePayment(Payment payment)
        {
            // write code for payment gateway and change status of database
            decisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            var RedirectType = string.Empty;
            var PayType = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (ModelState.IsValid)
                {
                    RedirectType = payment.RedirectType;
                    PayType = payment.PaymentType;
                    #region Parameters
                    PaymentResponse paymentResponse = new PaymentResponse
                      {
                          Amount = payment.Amount * 100,
                          TransactionType = payment.TransactionType,
                          ExpiryMonth = payment.ExpiryMonth,
                          ExpiryYear = payment.ExpiryYear,
                          CardNumber = payment.CardNumber,
                          CreditCardType = payment.CreditCardType,
                          NameOnCard = payment.NameOnCard,
                          BusinessName = payment.BusinessName,
                          Street = payment.Street,
                          StreetNumber = payment.StreetNumber,
                          Directions = payment.Directions,
                          Zip = payment.Zip,
                          CVC = payment.CVC,
                          CompanyFee = payment.CompanyFee * 100,
                          CustomerEmail = Convert.ToString(Session["Emailid"]),
                          UserId = Convert.ToInt32(Session["UserId"])
                      };
                    PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                    {
                        CompanyCode = payment.CompanyCode,
                        CompanyFee = payment.CompanyFee,
                        PerFieldStaffFee = payment.PerFieldStaffFee,
                        PerOfficeStaffFee = payment.PerOfficeStaffFee,
                        PerIcFee = payment.IcFee,
                        IsInvoice = payment.IsInvoice,
                        NoOfPartners = payment.NoOfFieldStaff,
                        NoOfStaff = payment.NoOfOfficeStaff,
                        NoOfIc = payment.NoOfIc,
                        BusinessName = payment.BusinessName,
                        TransactionType = payment.BusinessName,
                        userId = Convert.ToInt32(Session["UserId"]),
                        InviteeCompanyId = Convert.ToInt32(Session["InviteeCompany"])
                    };
                    #endregion
                    int result = decisionPointEngine.companyPayment(paymentResponse, paymentAmountResponse);
                    if (result > 0)
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        #region Send Mail

                        string email = Convert.ToString(Session["Emailid"]);
                        string text = "<div style='line-height:25px'>Dear " + payment.BusinessName + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + payment.Amount + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";
                        string subject = "Compliance Tracker Payment Success";
                        decisionPointEngine.GetSmtpDetail(email, text, subject);

                        #endregion
                    }
                    else
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                    }
                }
                else
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    return View(payment);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                TempData["ErrorMessage"] = ex.Message;

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            if (!string.IsNullOrEmpty(RedirectType) && object.Equals(TempData["ErrorMessage"], string.Empty))
            {
                return RedirectToAction("ChangePassword", "login", new { @type = RedirectType, @Res = PayType });
            }
            else if (object.Equals(TempData["ErrorMessage"], string.Empty))
            {
                return RedirectToAction("CheckPasswordLogin", "login", new { @type = RedirectType, @Res = PayType });
            }
            else
            {
                payment.StateList = decisionPointEngine.GetStateList().ToList();
                payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                return View(payment);

            }
        }

        /// <summary>
        /// get company profile
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>return company profile page</returns>
        public JsonResult GetCompanyProfile()
        {
            userId = Convert.ToInt32(Session["UserId"]);
            logMessage = new StringBuilder();
            CompanyProfileResponse comp = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                comp = decisionPointEngine.GetCompanyProfileDetails(userId);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(comp, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get payement amount as per company id 
        /// </summary>
        /// <param name="CompanyId">string CompanyId</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>paymeny amount list for office staff field staff etc.</returns>
        [HttpGet]
        public JsonResult GetPaymentAmount(string CompanyId)
        {

            Payment payment = new Payment();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (CompanyId != null)
                {
                    payment.PaymentAmountList = decisionPointEngine.getPaymentAmount(CompanyId).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(payment.PaymentAmountList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// search state for auto fill
        /// </summary>
        /// <param name="term">autofill serach term</param>
        ///  <createdBy>sumit saurav</createdBy>
        /// <createdDate>11 dec 2013</createdDate>
        /// <returns>list of state searched by term</returns>
        public JsonResult SearchState(string term)
        {

            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            IEnumerable<StateRespone> list = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                registerStep2.StateList = decisionPointEngine.GetStateList().ToList();
                registerStep2.StateList.Insert(0, new StateRespone
                {
                    Abbrebiation = "All",
                    SateName = "All",
                    StateId = 0
                });
                list = registerStep2.StateList.Where(m => m.SateName.ToLower().StartsWith(term.ToLower()));
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        ///// <summary>
        ///// search county as per state selection and autofill search term
        ///// </summary>
        ///// <param name="term">autofill search term</param>
        ///// <param name="StateAbbre">state abbrevaition</param>
        /////  <createdBy>sumit saurav</createdBy>
        ///// <createdDate>11 dec 2013</createdDate>
        ///// <returns> json type county list</returns>
        //public JsonResult SearchCounty(string term, string StateAbbre)
        //{

        //    registerStep2 = new RegisterStep2();
        //    logMessage = new StringBuilder();
        //    IEnumerable<CountyResponse> list = null;
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        decisionPointEngine = new DecisionPointEngine();
        //        if (StateAbbre != null)
        //        {
        //            registerStep2.CountyList = decisionPointEngine.GetCountyList(StateAbbre).ToList();
        //        }
        //        list = registerStep2.CountyList.Where(m => m.CountyName.ToLower().StartsWith(term.ToLower()));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        /// <summary>
        /// get zip list as per city selection
        /// </summary>
        /// <param name="CityList"> comma seprated selected city values</param>
        ///  <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of zip codes</returns>
        [HttpPost]
        public JsonResult GetZipList(string CityList, string stateabbrelist, string county)
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (CityList != null)
                {
                    registerStep2.ZipList = decisionPointEngine.GetZipListByCity(CityList, stateabbrelist, county).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Post:Get City List By Zip On Communication
        /// </summary>
        /// <param name="ZipList">zip list</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>json list of city</returns>
        [HttpPost]
        public JsonResult GetCityListByZipOnComm(string ZipList)
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (ZipList != null)
                {
                    registerStep2.ZipList = decisionPointEngine.GetCityListByZipOnComm(ZipList).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get state list by zip code
        /// </summary>
        /// <param name="ZipList">comma seprated zip list</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>list of zip codes </returns>
        [HttpPost]
        public JsonResult GetStateListByZip(string ZipList)
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (ZipList != null)
                {
                    registerStep2.ZipList = decisionPointEngine.GetStateListByZip(ZipList).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get state list by zip code
        /// </summary>
        /// <param name="ZipList">comma seprated zip list</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>list of zip codes </returns>
        [HttpPost]
        public JsonResult GetCountyListByZip(string ZipList)
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (ZipList != null)
                {
                    registerStep2.ZipList = decisionPointEngine.GetCountyListByZip(ZipList).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// email sending 
        /// </summary>
        /// <param name="abc">string type companyId</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>11 nov 2013</createdDate>
        /// <returns>message</returns>
        [HttpGet]
        public JsonResult SendEmail(string companyId)
        {

            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!object.Equals(Session["UserId"], null))
                {
                    decisionPointEngine = new DecisionPointEngine();
                    //get the admin userId
                    int adminUserId = decisionPointEngine.GetAdminUserId(companyId);
                    string text = string.Empty;
                    //get admin person details
                    objUserDashBoardResponse = decisionPointEngine.GetAccountDetails(adminUserId);
                    UserDashBoardResponse objParentUserDashBoard = new UserDashBoardResponse();
                    //get admin invitee person details for get the signature
                    int parentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture) + Shared.Comma + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture), Shared.Staff);
                    objParentUserDashBoard = decisionPointEngine.GetAccountDetails(parentUserId);
                    string signature = decisionPointEngine.GetSignature(userId);
                    objMailInviteFormat = new MailInviteFormat()
                    {
                        PersonName = objUserDashBoardResponse.fName + Shared.SingleSpace + objUserDashBoardResponse.lName,
                        Signature = signature,
                        DomainUrl = ViewModel.GetSiteRoot(),
                        InviteeUserId = Convert.ToString(parentUserId, CultureInfo.InvariantCulture),
                        FilePath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["InviteMailBody"], CultureInfo.InvariantCulture)),
                        InviteePersonName = objParentUserDashBoard.fName + Shared.SingleSpace + objParentUserDashBoard.lName,
                        InviteeCompanyName = objParentUserDashBoard.companyName,
                        Type = Shared.One
                    };

                    text = DPInviteMailFormat.PersonInviteMailFormat(objMailInviteFormat);
                    decisionPointEngine.GetSmtpDetail(objUserDashBoardResponse.emailId, text, "Compliance Tracker");
                    userId = Convert.ToInt32(Session["userId"], CultureInfo.InvariantCulture);
                    decisionPointEngine.DeactivateUser(userId);
                    Session.Abandon();
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(objUserDashBoardResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// temp pay for testing purpose before implementation of payment gateway to complete regsitration steps.
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>payment sucess or not?</returns>
        [HttpGet]
        public JsonResult SetTempPay()
        {

            int result = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    int ParentUserId = 0;
                    if (object.Equals(Session["InviteeCompany"], null))
                    {
                        //ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                        result = decisionPointEngine.SetTempPay(Convert.ToInt32(Session["UserId"]));
                    }
                    else
                    {
                        result = decisionPointEngine.SetTempPay(Convert.ToInt32(Session["UserId"]));
                    }

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// welcome page view
        /// </summary>
        /// <createdby>Sumit saurav</createdby>
        ///  <createdDate>10 mar 2014</createdDate>
        /// <returns>view of welcome page with all data</returns>
        [HttpGet]
        public ActionResult Welcome()
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId= Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    int parentUserId = decisionPointEngine.GetParentUserId(companyId, Shared.IC);
                    AdminProfileResponse parentUserName = decisionPointEngine.GetAdminProfile(parentUserId).FirstOrDefault();
                    ViewBag.UserName = parentUserName.FirstName;
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

        /// <summary>
        /// welcome page view
        /// </summary>
        /// <createdby>Sumit saurav</createdby>
        ///  <createdDate>10 mar 2014</createdDate>
        /// <returns>view of welcome page with all data</returns>
        [HttpGet]
        public ActionResult WelcomeInDP()
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
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
        /// <summary>
        /// validate vendor resgistration steps completed or not
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>comma seprated count of details from tables</returns>
        [HttpGet]
        public JsonResult ValidateVendor()
        {

            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                result = decisionPointEngine.validateVendorProfile(Convert.ToInt32(Session["UserId"]));
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// check staff registration steps completed or not?
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>comma seprated count of tables</returns>
        [HttpGet]
        public JsonResult ValidateStaff()
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                result = decisionPointEngine.validateStaffProfile(Convert.ToInt32(Session["UserId"]));
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// for completing the registration steps
        /// </summary>
        /// <param name="type">type of user</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>11 mar 2014</createdDate>
        /// <modifiedby>Bobis</modifiedby>  
        /// <modifieddate>14 july 2014</modifieddate>
        /// <returns>status chaged or not?</returns>
        [HttpGet]
        public JsonResult ChangeRegistrationStatus(string type)
        {
            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)))
                {
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                    decisionPointEngine = new DecisionPointEngine();
                    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture) + Shared.Comma + Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture), Shared.Staff);
                    result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, type, Convert.ToString(Session["CompanyId"]));
                    //Insert job Reqiure  documents tonew hired staff or IC
                    //companyid as per user id
                    //string ParentCompanyId = string.Empty;
                    //ParentCompanyId = Convert.ToString(decisionPointEngine.GetParentUserId(Convert.ToString(ParentUserId, CultureInfo.InvariantCulture), Shared.GetCompanyid), CultureInfo.InvariantCulture);
                    //companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture) == Shared.IC)
                    //{
                    //    JobReqForNewHireRequest objJobReqForNewHireRequest = new JobReqForNewHireRequest()
                    //    {
                    //        companyId = companyId,
                    //        userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                    //        userType = Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture),
                    //        parentuserId = ParentUserId,
                    //        inviteCompanyId = ParentCompanyId

                    //    };
                    //    objJobReqForNewHireRequest.ICTypeId = decisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).ToList().Where(x => x.IsUserBased == true).Select(x => x.VendorTypeId).FirstOrDefault();
                    //    decisionPointEngine.InsertJobComplianceReqforNewHired(objJobReqForNewHireRequest);
                    //}

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get view of staff coverage area
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view of staff coverage area</returns>
        [HttpGet]
        public ActionResult StaffCoverageArea()
        {
            return View();
        }

        /// <summary>
        /// get company name
        /// </summary>
        /// <param name="CompanyId"> company id</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>11 dec 2013</createdDate>
        /// <returns>json type company name</returns>
        [HttpGet]
        public JsonResult GetCompanyName(string CompanyId)
        {
            logMessage = new StringBuilder();
            string cmpName = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                cmpName = decisionPointEngine.GetCompanyName(CompanyId);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(cmpName, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// IC payment page 
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>14 mar 2014</createdDate>
        /// <returns>returns view with model data</returns>
        [HttpGet]
        //  [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult MakeIcPayment()
        {
            logMessage = new StringBuilder();

            Payment payment = new Payment();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    bool isPayment = decisionPointEngine.getPaymentStatus(Convert.ToInt32(Session["UserId"]));
                    if (!isPayment)
                    {
                        payment.StateList = decisionPointEngine.GetStateList().ToList();
                        payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                        payment.BusinessName = Convert.ToString(Session["BusinessName"]);
                        payment.RedirectType = Request["Type"];
                        payment.PaymentType = Request["Res"];
                        payment.RootUrl = ViewModel.GetSiteRoot();
                        payment.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
                        payment.PayPalAccount = System.Configuration.ConfigurationManager.AppSettings["PayPalAccount"];
                        int ParentUserId = decisionPointEngine.GetParentUserId(companyId, Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                        payment.ParentUserType = decisionPointEngine.GetParentUserType(ParentUserId);
                        payment.ShowCardDiv = decisionPointEngine.getPaymentAmount(companyId).ToList().Select(x => x.IsInvoice).FirstOrDefault();
                        if (!object.Equals(Request["PkgAmt"], null))
                        {
                            payment.MemberShipAmount = Convert.ToDecimal(Request["PkgAmt"], CultureInfo.InvariantCulture);
                        }
                        Session["checked"] = "checked";
                        objactionResult = View(payment);
                    }
                    else
                    {
                        if (Convert.ToBoolean(Session["IsRegistered"], CultureInfo.InvariantCulture))
                        {
                            objactionResult = RedirectToAction("ICProfile", "UserDashBoard");
                        }
                        else
                        {
                            objactionResult = RedirectToAction("Welcome", "Company");
                        }

                    }
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
        /// <summary>
        /// lisence aggrement page
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>view of lisence aggrement</returns>
        [HttpGet]
        public ActionResult LisenceAggrement()
        {
            return View();
        }

        /// <summary>
        /// make payment
        /// </summary>
        /// <param name="payment">paramters for payment gateway</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>return with payemnt success or fail message</returns>
        [HttpPost]
        public ActionResult MakeIcPayment(Payment payment)
        {

            // var tbl = ConfigurationManager.AppSettings["StripeKey"].ToString();  
            // write code for payment gateway and change status of database
            decisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            var RedirectType = string.Empty;
            var PayType = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (ModelState.IsValid)
                {
                    RedirectType = payment.RedirectType;
                    PayType = payment.PaymentType;
                    #region Parameters
                    PaymentResponse paymentResponse = new PaymentResponse
                    {
                        Amount = (payment.Amount + payment.MemberShipAmount) * 100,
                        TransactionType = payment.TransactionType,
                        ExpiryMonth = payment.ExpiryMonth,
                        ExpiryYear = payment.ExpiryYear,
                        CardNumber = payment.CardNumber,
                        CreditCardType = payment.CreditCardType,
                        NameOnCard = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        Street = payment.Street,
                        StreetNumber = payment.StreetNumber,
                        Directions = payment.Directions,
                        Zip = payment.Zip,
                        CVC = payment.CVC,
                        CompanyFee = payment.CompanyFee * 100,
                        CustomerEmail = Convert.ToString(Session["Emailid"]),
                        UserId = Convert.ToInt32(Session["UserId"])

                    };
                    PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                    {
                        CompanyCode = payment.CompanyCode,
                        CompanyFee = payment.CompanyFee,
                        PerFieldStaffFee = payment.PerFieldStaffFee,
                        PerOfficeStaffFee = payment.PerOfficeStaffFee,
                        PerIcFee = payment.IcFee,
                        IsInvoice = payment.IsInvoice,
                        NoOfPartners = payment.NoOfFieldStaff,
                        NoOfStaff = payment.NoOfOfficeStaff,
                        NoOfIc = payment.NoOfIc,
                        CardType = payment.CreditCardType,
                        CardHolderName = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        TransactionType = payment.BusinessName,
                        userId = Convert.ToInt32(Session["UserId"]),
                        InviteeCompanyId = Convert.ToInt32(Session["InviteeCompany"])
                    };
                    #endregion
                    int result = decisionPointEngine.companyPayment(paymentResponse, paymentAmountResponse);
                    if (result > 0)
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        #region Send Mail

                        string email = Convert.ToString(Session["Emailid"]);
                        string text = "<div style='line-height:25px'>Dear " + payment.BusinessName + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + payment.Amount + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";

                        string subject = "Compliance Tracker Payment Success";
                        decisionPointEngine.GetSmtpDetail(email, text, subject);

                        #endregion
                    }
                    else
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                    }
                }
                else
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    objactionResult = View(payment);
                }
                if (!string.IsNullOrEmpty(RedirectType) && object.Equals(TempData["ErrorMessage"], string.Empty))
                {
                    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                    decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.IC, Convert.ToString(Session["CompanyId"]));
                    objactionResult = RedirectToAction("Welcome", "Company");//RedirectToAction("ChangePassword", "login", new { @type = RedirectType, @Res = PayType });
                }
                else if (object.Equals(TempData["ErrorMessage"], string.Empty))
                {
                    objactionResult = RedirectToAction("ICProfile", "UserDashBoard");//RedirectToAction("CheckPasswordLogin", "login", new { @type = RedirectType, @Res = PayType });
                }
                else
                {
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    objactionResult = View(payment);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                TempData["ErrorMessage"] = ex.Message;

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;
        }
        /// <summary>
        /// used to get state & cirt by zip code
        /// </summary>
        /// <param name="zip">zip</param>
        /// <createdby>Sumit</createdby>
        /// <createddate>1 may 2014</createddate>
        /// <returns>Json</returns>
        [HttpGet]
        public JsonResult getStateCityByZip(string zip)
        {

            logMessage = new StringBuilder();
            IList<ZipResponse> result = new List<ZipResponse>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                result = decisionPointEngine.getStateCityByZip(zip);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult NewCoverageArea()
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                registerStep2.StateList = decisionPointEngine.GetStateList().ToList();
                ViewData.Model = registerStep2;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View();
        }


        //[HttpGet]
        //public JsonResult getEmployeeScreening()
        //{
        //    //create the web service proxy object
        //    ASCQuerySvc.ASCQuerySvcSoapClient oSvc = new ASCQuerySvc.ASCQuerySvcSoapClient();
        //    //query the service to get a list of all available fields.
        //    ASCQuerySvc.FieldHelper[] AllFields = oSvc.GetQueryableFields(null, false);
        //    //load all the available field names into a string list
        //    List<string> FieldNames = new List<string>();
        //    foreach (ASCQuerySvc.FieldHelper curField in AllFields)
        //    {
        //        FieldNames.Add(curField.FieldName);
        //    }
        //    //create a 3 filter condition for St_Abbr, Lic_Number, and Lic_Type. All Operators
        //    //except "Between" and "In" have only one operand
        //    ASCQuerySvc.FilterCondition oFilter1 = new ASCQuerySvc.FilterCondition();
        //    oFilter1.FieldName = "St_Abbr";
        //    oFilter1.Operator = ASCQuerySvc.AvailableOperators.Equals;
        //    oFilter1.Operands = new string[] { "CA" };
        //    ASCQuerySvc.FilterCondition oFilter2 = new ASCQuerySvc.FilterCondition();
        //    oFilter2.FieldName = "Lic_Number";
        //    oFilter2.Operator = ASCQuerySvc.AvailableOperators.Equals;
        //    oFilter2.Operands = new string[] { "AR023287" };
        //    ASCQuerySvc.FilterCondition oFilter3 = new ASCQuerySvc.FilterCondition();
        //    oFilter3.FieldName = "Lic_Type";
        //    oFilter3.Operator = ASCQuerySvc.AvailableOperators.Equals;
        //    oFilter3.Operands = new string[] { "3" };

        //    var oResults = oSvc.RunQuery(null, FieldNames.ToArray(), new ASCQuerySvc.FilterCondition[] { oFilter1, oFilter2, oFilter3 }, true, 0);
        //    return Json(oResults, JsonRequestBehavior.AllowGet);
        //}

        #region Coverage Area Changes
        ///// <summary>
        ///// get city list by zip code
        ///// </summary>
        ///// <param name="ZipList">comma seprated zip list</param>
        /////  <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 dec 2013</createdDate>
        ///// <returns>list of zip codes </returns>
        //[HttpPost]
        //public JsonResult GetCityListByZip(string ZipList)
        //{
        //    registerStep2 = new RegisterStep2();
        //    logMessage = new StringBuilder();
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        decisionPointEngine = new DecisionPointEngine();
        //        if (ZipList != null)
        //        {
        //            registerStep2.ZipList = decisionPointEngine.GetCityListByzip(ZipList).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult CoverageArea()
        {
            logMessage = new StringBuilder();
            try
            {
                int type = 0;
                objSaveCoverageArea = new CoverageAreaModel();
                decisionPointEngine = new DecisionPointEngine();
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (Convert.ToInt32(Request["type"], CultureInfo.InvariantCulture).Equals(1)) { type = 1; }
                    else if (Convert.ToInt32(Request["type"], CultureInfo.InvariantCulture).Equals(3))
                    {
                        type = 3;
                        //Use UserId As Doc Id When user is on coverage area popup from communication recipient page
                        userId = Convert.ToInt32(Request["docId"], CultureInfo.InvariantCulture);
                    }
                    //get coverage area status
                    objSaveCoverageArea.CoverageAreaStatus = decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                    //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.IC))
                    //{
                    //    type = 0;
                    //}
                    //get states
                    objSaveCoverageArea.StateDetails = decisionPointEngine.GetStateList(userId, companyId, type);
                    //get City
                    objSaveCoverageArea.CityDetails = decisionPointEngine.GetCityList(userId, companyId, type);
                    //get County
                    objSaveCoverageArea.CountyDetails = decisionPointEngine.GetCountyList(userId, companyId, type);

                    ViewData.Model = objSaveCoverageArea;
                    objactionResult = View();

                }
                else
                {
                    objactionResult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;
        }

        /// <summary>
        /// get city list by zip code
        /// </summary>
        /// <param name="ZipList">comma seprated zip list</param>
        ///  <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of zip codes </returns>
        [HttpPost]
        public JsonResult GetCityListByZip(string ZipList)
        {
            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                if (ZipList != null)
                {
                    registerStep2.ZipList = decisionPointEngine.GetCityListByzip(ZipList).ToList();
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.ZipList, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// search zip list as per city list for auto fill
        /// </summary>
        /// <param name="term">autofill term</param>
        /// <param name="CityList">list of comma seprated cities</param>
        ///  <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of zip codes for autofill textbox </returns>
        [HttpGet]
        public JsonResult SearchZip(string term, string CityList)
        {

            registerStep2 = new RegisterStep2();

            List<string> list = null;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                registerStep2.ZipList = decisionPointEngine.GetZipListByCity(CityList, "", "").ToList();

                list = registerStep2.ZipList.Where(m => m.ZipCode.ToLower().StartsWith(term.ToLower())).Select(x => x.ZipCode).ToList();
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return this.Json(list, JsonRequestBehavior.AllowGet);
            // return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int SaveCoverageArea(CoverageAreaModel objSaveCoverageArea)
        {
            int Inserted = 0;
            logMessage = new StringBuilder();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objSaveCoverageArea.UserId = userId;
                    objSaveCoverageArea.CompanyId = companyId;
                    objViewModel = new ViewModel();
                    //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.IC))
                    //{
                    //    objSaveCoverageArea.CoverageAreaFor = 1;
                    //}
                    objViewModel.SaveCoverageArea(objSaveCoverageArea);
                }
                else { RedirectToAction("Login", "Login"); }
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));
                return Inserted;
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                throw;
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }



        /// <summary>
        /// saves zip name
        /// </summary>
        /// <param name="zip">zip cods</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>json type zip saved or not?</returns>
        [HttpPost]
        public JsonResult SaveZipMapping(string zip, string type, string isdelete)
        {
            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    decisionPointEngine = new DecisionPointEngine();
                    ZipRequest zipRequest = new ZipRequest
                    {
                        ZipCode = zip,
                        UserId = Convert.ToInt32(Session["UserId"]),
                        CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)
                    };
                    result = decisionPointEngine.SaveZipMapping(zipRequest, type);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));

            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// return page to upload zip code list
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>return page to upload zip code list</returns>
        [HttpGet]
        public ActionResult UploadZipList()
        {
            logMessage = new StringBuilder();
            try
            {
                int type = 0;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    UploadZipModel uploadZipModel = new UploadZipModel();
                    decisionPointEngine = new DecisionPointEngine();
                    //get zip list
                    if (Convert.ToInt32(Request["type"], CultureInfo.InvariantCulture).Equals(3))
                    {
                        //consider user id parameter as a doc Id if user is on communication recipient page
                        uploadZipModel.ZipList = decisionPointEngine.GetzipList(Convert.ToInt32(Request["docId"], CultureInfo.InvariantCulture), companyId, 3);
                    }
                    else
                    {
                        uploadZipModel.ZipList = decisionPointEngine.GetzipList(userId, companyId, 1);
                    }
                    //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.IC))
                    //{
                    //    type = 1;
                    //}
                    uploadZipModel.CompanyBasedZipList = decisionPointEngine.GetzipList(userId, companyId, type);
                    ViewData.Model = uploadZipModel;
                    objactionResult = View();
                }

                else
                {
                    objactionResult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionResult;

        }
        /// <summary>
        /// upload zip code csv file and return list of zips without comma.
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>18 nov 2013</createdDate>
        /// <returns>upload zip code csv file and return list of zips without comma.</returns>
        [HttpPost]
        public ActionResult UploadZip(UploadZipModel uploadZipModel)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (ModelState.IsValid)
                {
                    var file = Request.Files[0];
                    var filePath = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        filePath = Path.Combine(HttpContext.Server.MapPath(ConfigurationManager.AppSettings["downloadcsvfile"]),
                                                          Path.GetFileName(file.FileName));
                        file.SaveAs(filePath);

                        CsvSplitter csvSplitter = new CsvSplitter();
                        List<string[]> parsedData = csvSplitter.parseCSV(filePath, uploadZipModel.Seprator[0], uploadZipModel.FirstRowHeader);
                        uploadZipModel.UploadedZipList = parsedData;
                        decisionPointEngine = new DecisionPointEngine();
                        //get zip list
                        uploadZipModel.ZipList = decisionPointEngine.GetzipList(userId, companyId, 1);
                        uploadZipModel.CompanyBasedZipList = decisionPointEngine.GetzipList(userId, companyId, 0);
                    }
                    ViewData.Model = uploadZipModel;
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return View("UploadZipList");
        }

        /// <summary>
        /// DownloadZipSample
        /// </summary>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>file type</returns>
        public FileResult DownloadZipSample()
        {
            var fileName = "Zip Csv.csv";
            var filePath = Server.MapPath("~/Content/documents/Sample/ziplist.csv");
            return File(filePath, "application/octet-stream", fileName);
        }

        /// <summary>
        /// get all  state 
        /// </summary>  
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 nov 2013</createdDate>
        /// <returns>list of states </returns>
        [HttpPost]
        public JsonResult getAllStates(int type)
        {

            registerStep2 = new RegisterStep2();
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    decisionPointEngine = new DecisionPointEngine();
                    //0 for showing all state list 1 for showing state list as per company 
                    if (type.Equals(1))
                    {
                        string coverageAreaStatus = decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                        if (!string.IsNullOrEmpty(coverageAreaStatus))
                        {
                            if ((coverageAreaStatus).Trim().Equals("all"))
                            {
                                registerStep2.StateList = decisionPointEngine.GetStateList().ToList();

                            }
                            else { registerStep2.StateList = decisionPointEngine.GetStateList(userId, companyId, 0); }
                        }
                        else { registerStep2.StateList = decisionPointEngine.GetStateList(userId, companyId, 0); }
                    }
                    else if (type.Equals(0)) { registerStep2.StateList = decisionPointEngine.GetStateList().ToList(); }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.StateList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get county list
        /// </summary>
        /// <param name="StateAbbre">state abbreviation</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of counties</returns>
        [HttpGet]
        public JsonResult GetCountyList(string StateAbbre, int type)
        {
            logMessage = new StringBuilder();
            registerStep2 = new RegisterStep2();
            int coverageAreaStatus = -1;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    decisionPointEngine = new DecisionPointEngine();
                    if (StateAbbre != null)
                    {
                        //0 for showing all state list 1 for showing state list as per company
                        if (type.Equals(0))
                        {
                            registerStep2.CountyList = decisionPointEngine.GetCountyList(StateAbbre).ToList();
                        }
                        else if (type.Equals(1))
                        {
                            string coverageAreaType = decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                            var companybasedstate = decisionPointEngine.GetStateList(userId, companyId, 0).Where(x => x.Abbrebiation == StateAbbre).FirstOrDefault();//decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                            if (!object.Equals(companybasedstate, null))
                            {
                                coverageAreaStatus = companybasedstate.StateType;
                            }
                            if ((coverageAreaStatus).Equals(0) || coverageAreaType.Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.All.Trim().ToLower(CultureInfo.InvariantCulture)))
                            {
                                registerStep2.CountyList = decisionPointEngine.GetCountyList(StateAbbre).ToList();
                            }
                            else
                            {
                                registerStep2.CountyList = decisionPointEngine.GetCountyList(userId, companyId, 0).Where(x => x.OptionsVal.Split('_')[1] == StateAbbre).ToList();
                            }

                        }

                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.CountyList, JsonRequestBehavior.AllowGet);
        }

        #region MyRegion Commented Code
        ///// <summary>
        ///// get city list by city id
        ///// </summary>
        ///// <param name="CountyId">county id</param>
        ///// <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 dec 2013</createdDate>
        ///// <returns>list of cities</returns>
        //[HttpGet]
        //public JsonResult GetCityList(string CountyId, string type)
        //{
        //    logMessage = new StringBuilder();
        //    registerStep2 = new RegisterStep2();
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        decisionPointEngine = new DecisionPointEngine();
        //        if (CountyId != null)
        //        {
        //            if (!string.IsNullOrEmpty(type))
        //            {
        //                if (type.Equals("vendor"))
        //                {
        //                    //user session user id
        //                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
        //                    registerStep2.CityList = decisionPointEngine.GetCityList(CountyId).ToList();
        //                }
        //                else
        //                {
        //                    //get parent user id for get the coverage area for staff
        //                    userId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
        //                    registerStep2.CityList = decisionPointEngine.GetCityList(CountyId, userId).ToList();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(registerStep2.CityList, JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// save states selected by a company
        ///// </summary>
        ///// <param name="stateName">state name</param>
        ///// <param name="Abbre">abbreviation</param>
        ///// <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 nov 2013</createdDate>
        ///// <returns>json type result saved or not</returns>
        //[HttpPost]
        //public JsonResult SaveStateMapping(string stateName, string Abbre, string type, string options)
        //{
        //    logMessage = new StringBuilder();
        //    var result = 0;
        //    try
        //    {

        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            decisionPointEngine = new DecisionPointEngine();
        //            StateRequest stateRequest = new StateRequest
        //            {
        //                StateName = stateName,
        //                Abbrebiation = Abbre,
        //                UserId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
        //                CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)

        //            };
        //            result = decisionPointEngine.SaveStateMapping(stateRequest, type);
        //        }
        //        else
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));

        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        ///// <summary>
        ///// saves county list selected by a company
        ///// </summary>
        ///// <param name="countyName">county name</param>
        ///// <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 nov 2013</createdDate>
        ///// <returns>json type saved or not?</returns>
        //[HttpPost]
        //public JsonResult SaveCountyMapping(string countyName, string type)
        //{
        //    logMessage = new StringBuilder();
        //    var result = 0;
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            decisionPointEngine = new DecisionPointEngine();
        //            CountyRequest countyRequest = new CountyRequest
        //            {
        //                CountyName = countyName,
        //                UserId = Convert.ToInt32(Session["UserId"]),
        //                CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)
        //            };
        //            result = decisionPointEngine.SaveCountyMapping(countyRequest, type);

        //        }
        //        else
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);

        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        ///// <summary>
        ///// saves city name
        ///// </summary>
        ///// <param name="cityName">city name</param>
        ///// <createdBy>sumit saurav</createdBy>
        ///// <createdDate>10 dec 2013</createdDate>
        ///// <returns>json type ciy saved or not?</returns>
        //[HttpPost]
        //public JsonResult SaveCityMapping(string cityName, string type)
        //{
        //    logMessage = new StringBuilder();
        //    var result = 0;
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            decisionPointEngine = new DecisionPointEngine();
        //            CityRequest cityRequest = new CityRequest
        //            {
        //                CityName = cityName,
        //                UserId = Convert.ToInt32(Session["UserId"]),
        //                CompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture)
        //            };
        //            result = decisionPointEngine.SaveCityMapping(cityRequest, type);
        //        }
        //        else
        //        {
        //            RedirectToAction("Login", "Login");
        //        }
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0} function executed successfully.", MethodBase.GetCurrentMethod().Name));

        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //        throw;
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //[HttpGet]
        //public JsonResult GetCAOrServiceDoesNotApply()
        //{
        //    logMessage = new StringBuilder();
        //    userId = Convert.ToInt32(Session["UserId"]);
        //    companyId = Convert.ToString(Session["CompanyId"]);
        //    string result = string.Empty;
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        decisionPointEngine = new DecisionPointEngine();
        //        result = decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        #endregion


        /// <summary>
        /// get city list by city id
        /// </summary>
        /// <param name="CountyId">county id</param>
        /// <createdBy>sumit saurav</createdBy>
        /// <createdDate>10 dec 2013</createdDate>
        /// <returns>list of cities</returns>
        [HttpGet]
        public JsonResult GetCityList(string CountyId, int type)
        {
            logMessage = new StringBuilder();
            registerStep2 = new RegisterStep2();

            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    string stateAbbre = string.Empty;
                    int exactCountyId = 0;
                    int coverageAreaStatus = -1;
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    decisionPointEngine = new DecisionPointEngine();
                    if (CountyId != null)
                    {

                        //0 for showing all state list 1 for showing state list as per company
                        if (type.Equals(0))
                        {
                            registerStep2.CityList = decisionPointEngine.GetCityList(CountyId).ToList();
                        }
                        else if (type.Equals(1))
                        {
                            string coverageAreaType = decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                            if (!string.IsNullOrEmpty(CountyId))
                            {
                                List<string> myList = CountyId.Split(char.Parse(Shared.UnderScore)).ToList();
                                if (!object.Equals(myList, null))
                                {
                                    if (myList.Count > 1)
                                    {
                                        stateAbbre = myList[1];
                                        exactCountyId = Convert.ToInt32(myList[0]);
                                    }
                                }
                            }
                            var statestatus = decisionPointEngine.GetStateList(userId, companyId, 0).Where(x => x.Abbrebiation == stateAbbre).FirstOrDefault();//decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                            if (!object.Equals(statestatus, null))
                            {
                                if (!statestatus.StateType.Equals(0))
                                {
                                    var countystatus = decisionPointEngine.GetCountyList(userId, companyId, 0).Where(x => x.OptionsVal.Split('_')[1] == stateAbbre && Convert.ToInt32(x.OptionsVal.Split(char.Parse(Shared.UnderScore))[0], CultureInfo.InvariantCulture) == exactCountyId).FirstOrDefault();//decisionPointEngine.GetCAOrServiceDoesNotApply(userId, companyId, 0);
                                    if (!object.Equals(countystatus, null))
                                    {
                                        coverageAreaStatus = countystatus.CountyType;
                                    }
                                }
                                else { coverageAreaStatus = statestatus.StateType; }
                            }
                            if ((coverageAreaStatus).Equals(0) || coverageAreaType.Trim().ToLower(CultureInfo.InvariantCulture).Equals(Shared.All.Trim().ToLower(CultureInfo.InvariantCulture)))
                            {
                                registerStep2.CityList = decisionPointEngine.GetCityList(CountyId).ToList();
                            }
                            else
                            {
                                registerStep2.CityList = decisionPointEngine.GetCityList(userId, companyId, 0).Where(x => Convert.ToInt32(x.OptionsVal.Split(char.Parse(Shared.UnderScore))[1], CultureInfo.InvariantCulture) == exactCountyId && x.OptionsVal.Split(char.Parse(Shared.UnderScore))[2] == stateAbbre).ToList();
                            }


                        }


                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(registerStep2.CityList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// change status in db if coverage area does not apply is selected by user
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>updated or not?</returns>
        [HttpGet]
        public JsonResult CoverageAreaDoesNotApply(int type, string coverageAreaStatus)
        {

            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    decisionPointEngine = new DecisionPointEngine();
                    result = decisionPointEngine.CoverageAreaDoesNotApply(userId, companyId, type, coverageAreaStatus);

                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get details if coverage area doesn't apply for a user
        /// </summary>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>result of coverage area apply or not?</returns>

        #endregion
        #region IC Non CLient Payment
        /// <summary>
        /// MakeNonClientPayment
        /// </summary>
        /// <createdby>Bobi</createdby>
        ///  <createdDate>19 jan 2015</createdDate>
        /// <returns>returns view with model data</returns>
        [HttpGet]
        public ActionResult MakeNonClientPayment()
        {
            logMessage = new StringBuilder();
            Payment payment = new Payment();
            List<int> vendorTypeCol = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                decisionPointEngine = new DecisionPointEngine();
                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                //bool isPayment = decisionPointEngine.getPaymentStatus(Convert.ToInt32(Session["UserId"]));
                //if (isPayment != true)
                //{
                    payment.StateList = decisionPointEngine.GetStateList().ToList();
                    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
                    int ParentUserId = decisionPointEngine.GetParentUserId(companyId, Shared.IC);
                    objUserDashBoardResponse = new UserDashBoardResponse();
                    objUserDashBoardResponse = decisionPointEngine.GetAccountDetails(ParentUserId);
                    if (!objUserDashBoardResponse.Equals(null))
                    {
                        payment.FirstName = objUserDashBoardResponse.fName;
                        payment.MiddelName = objUserDashBoardResponse.mName;
                        payment.LastName = objUserDashBoardResponse.lName;
                        payment.BusinessName = Convert.ToString(Session["BusinessName"]);
                    }
                    companyId = decisionPointEngine.GetCompanyIdByUserId(ParentUserId);
                    payment.ICType = decisionPointEngine.GetCompanyVendorType(companyId, Shared.IC).ToList().Where(x => x.IsUserBased == true);
                    if (!object.Equals(payment.ICType, null))
                    {
                        vendorTypeCol = payment.ICType.Select(x => x.VendorTypeId).ToList();
                    }
                    payment.LicAndInsAsDetails = decisionPointEngine.GetProfessionalLicense(ParentUserId, companyId).ToList().
                                                 Union(decisionPointEngine.GetProfessionalInsurance(ParentUserId, companyId).ToList());
                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = companyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                    payment.BackgroundList = decisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                    payment.RootUrl = ViewModel.GetSiteRoot();
                    payment.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
                    payment.PayPalAccount = System.Configuration.ConfigurationManager.AppSettings["PayPalAccount"];
                    objactionResult = View(payment);
                //}
                //else
                //{
                //    objactionResult = RedirectToAction("LogOut", "Login");
                //}
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
        /// <summary>
        /// make payment
        /// </summary>
        /// <param name="payment">paramters for payment gateway</param>
        /// <createdby>sumit saurav</createdby>
        ///  <createdDate>12 mar 2014</createdDate>
        /// <returns>return with payemnt success or fail message</returns>
        [HttpPost]
        public ActionResult MakeNonClientPayment(Payment payment)
        {

            // var tbl = ConfigurationManager.AppSettings["StripeKey"].ToString();  
            // write code for payment gateway and change status of database
            decisionPointEngine = new DecisionPointEngine();
            logMessage = new StringBuilder();
            var RedirectType = string.Empty;
            var PayType = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                {
                    #region Parameters
                    PaymentResponse paymentResponse = new PaymentResponse
                    {
                        Amount = payment.Amount * 100,
                        TransactionType = payment.TransactionType,
                        ExpiryMonth = payment.ExpiryMonth,
                        ExpiryYear = payment.ExpiryYear,
                        CardNumber = payment.CardNumber,
                        CreditCardType = payment.CreditCardType,
                        NameOnCard = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        Street = payment.Street,
                        StreetNumber = payment.StreetNumber,
                        Directions = payment.Directions,
                        Zip = payment.Zip,
                        CVC = payment.CVC,
                        CompanyFee = payment.CompanyFee * 100,
                        CustomerEmail = Convert.ToString(Session["Emailid"]),
                        UserId = Convert.ToInt32(Session["UserId"])
                    };
                    PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                    {
                        CompanyCode = Convert.ToString(Session["CompanyId"]),
                        CompanyFee = payment.CompanyFee,
                        CardType = payment.CreditCardType,
                        CardHolderName = payment.NameOnCard,
                        BusinessName = payment.BusinessName,
                        TransactionType = payment.BusinessName,
                        userId = Convert.ToInt32(Session["UserId"]),
                        InviteeCompanyId = Convert.ToInt32(Session["InviteeCompany"])
                    };
                    #endregion
                    int result = decisionPointEngine.ICNonClientPayment(paymentResponse, paymentAmountResponse);
                    if (result > 0)
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        #region Send Mail

                        string email = Convert.ToString(Session["Emailid"]);
                        string text = "<div style='line-height:25px'>Dear " + payment.BusinessName + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + payment.Amount + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";

                        string subject = "Compliance Tracker Payment Success";
                        decisionPointEngine.GetSmtpDetail(email, text, subject);
                        int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                        result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.NonClient, Convert.ToString(Session["CompanyId"]));
                        objactionResult = RedirectToAction("Welcome", "Company");
                        #endregion
                    }
                    else
                    {
                        TempData["SucessMessage"] = string.Empty;
                        TempData["ErrorMessage"] = string.Empty;
                        objactionResult = RedirectToAction("MakeNonClientPayment", payment);
                    }
                }
                else
                {
                    objactionResult = RedirectToAction("Logic", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                TempData["ErrorMessage"] = ex.Message;
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            //if (!string.IsNullOrEmpty(RedirectType) && object.Equals(TempData["ErrorMessage"], string.Empty))
            //{
            //    return RedirectToAction("ChangePassword", "login", new { @type = RedirectType, @Res = PayType });
            //}
            //else if (object.Equals(TempData["ErrorMessage"], string.Empty))
            //{
            //    return RedirectToAction("CheckPasswordLogin", "login", new { @type = RedirectType, @Res = PayType });
            //}
            //else
            //{
            //    payment.StateList = decisionPointEngine.GetStateList().ToList();
            //    payment.CityList = decisionPointEngine.GetCityListByState("").ToList();
            //    return View(payment);
            //    // return RedirectToAction("MakeIcPayment", "Company", new { @type = RedirectType, @Res = PayType });

            //}
            return objactionResult;
        }
        #endregion

        #region Package Payment
        [HttpGet]
        public ActionResult PackagePayment()
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                decisionPointEngine = new DecisionPointEngine();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["userId"], CultureInfo.InvariantCulture);
                    objPackagePaymentModel = new PackagePaymentModel();
                    decisionPointEngine = new DecisionPointEngine();
                    objPackagePaymentModel.BackgroundPkgList = decisionPointEngine.GetPackagesDetails();
                    objPackagePaymentModel.StateList = decisionPointEngine.GetStateList();
                    //get saved packages details by IC
                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = companyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                    objPackagePaymentModel.BackgroundList = decisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                    if (!object.Equals(objPackagePaymentModel.BackgroundList, null))
                    {
                        if (objPackagePaymentModel.BackgroundList.Count > 0)
                        {
                            objPackagePaymentModel.IsPurchase = true;
                        }
                        else { objPackagePaymentModel.IsPurchase = false; }
                    }
                    else { objPackagePaymentModel.IsPurchase = false; }

                    objPackagePaymentModel.RedirectType = Request["type"];
                    objPackagePaymentModel.PayType = Request["Res"];
                   
                    if (objPackagePaymentModel.PayType.Equals(Shared.Two))
                    {
                        objPackagePaymentModel.BackgroundPkgList.Remove(objPackagePaymentModel.BackgroundPkgList.Where(x => x.SterlingPkgName.Contains("Enhanced Nationwide")).FirstOrDefault());
                    }
                    else if (objPackagePaymentModel.PayType.Equals(Shared.Three))
                    {
                      objPackagePaymentModel.BackgroundPkgList=  objPackagePaymentModel.BackgroundPkgList.Where(x => x.SterlingPkgName.Contains("Basic")).ToList();
                    }
                    //get Invite Company Name
                    parentUserId = decisionPointEngine.GetParentUserId(companyId, Shared.IC);
                    objPackagePaymentModel.InviteeCompanyName = decisionPointEngine.GetAccountDetails(parentUserId).companyName;
                    //Invitee Company Name
                    parentCompanyId = decisionPointEngine.GetCompanyIdByUserId(parentUserId);
                    ConfiguratonSettingRequest objConfiguratonSettingRequest = decisionPointEngine.GetConfigSetting(parentCompanyId);
                    if (!object.Equals(objConfiguratonSettingRequest, null))
                    {
                        if (!objConfiguratonSettingRequest.IsICUsePackages)
                        {
                            objPackagePaymentModel.BackgroundPkgList = objPackagePaymentModel.BackgroundPkgList.Where(x => x.SterlingPkgName.Contains("Basic")).ToList();
                        }
                    }
                    objPackagePaymentModel.RootUrl = ViewModel.GetSiteRoot();
                    objPackagePaymentModel.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
                    objPackagePaymentModel.PayPalAccount = System.Configuration.ConfigurationManager.AppSettings["PayPalAccount"];
                    //get saved packages details by Invitee Company
                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = parentCompanyId, UserId = userId, CompanyId = companyId, OperationType = 3 };
                    objPackagePaymentModel.RequirementSetByEntity = decisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                    ViewData.Model = objPackagePaymentModel;
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
        [HttpPost]
        public JsonResult PackagePayment(PackagePaymentModel objPackagePaymentModel)
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    //if (objPackagePaymentModel.IsPurchase)
                    //{
                    //    if (objPackagePaymentModel.PayType.Equals("1"))
                    //    {
                    //        objactionResult = RedirectToAction("Welcome", "Company");
                    //    }
                    //    else
                    //    {
                    //        objactionResult = RedirectToAction("MakeIcPayment", "Company", new { @type = objPackagePaymentModel.RedirectType, @Res = objPackagePaymentModel.PayType, @PkgAmt = objPackagePaymentModel.PackageAmount });
                    //    }
                    //}
                    if (!objPackagePaymentModel.IsPurchase)
                    {

                        #region Objects
                        decisionPointEngine = new DecisionPointEngine();
                        objUserDashBoardResponse = new UserDashBoardResponse();
                        objViewModel = new ViewModel();
                        #endregion
                        objUserDashBoardResponse = decisionPointEngine.GetAccountDetails(userId);
                        if (!string.IsNullOrEmpty(objPackagePaymentModel.PackageIds))
                        {
                            List<int> packageIds = objPackagePaymentModel.PackageIds.Split(char.Parse(Shared.Comma)).Select(x => int.Parse(x)).ToList();
                            foreach (int pkgId in packageIds)
                            {
                                string packageName = decisionPointEngine.GetPackageNameAsPerId(pkgId);
                                objSterlingModel = new SterlingModel()
                                {
                                    FirstName = objUserDashBoardResponse.fName,
                                    LastName = objUserDashBoardResponse.lName,
                                    MiddelName = objUserDashBoardResponse.mName,
                                    EmailAddress = objUserDashBoardResponse.emailId,
                                    PackageId = pkgId,
                                    PackageName = packageName,
                                    State = objUserDashBoardResponse.StateName,
                                    City = objUserDashBoardResponse.CityName,
                                    ZipCode = objUserDashBoardResponse.ZipCode,
                                    AddressLine1 = objUserDashBoardResponse.StreetNumber + objUserDashBoardResponse.Direction + objUserDashBoardResponse.StreetName,
                                    PhoneNumber = objUserDashBoardResponse.officePhone,
                                    OperationType = 2,
                                    PayType = objPackagePaymentModel.PayType,
                                    UserId = 0,
                                    CompanyId = string.Empty,
                                    StateAbbre = objPackagePaymentModel.StateAbbre

                                };
                                objViewModel.InitiateOnSterling(objSterlingModel);
                            }
                        }
                        else
                        {
                            objPackagePaymentModel.PackageAmount = 0;
                        }
                        //if (objPackagePaymentModel.PayType.Equals("1"))
                        //{
                        //    objactionResult = RedirectToAction("Welcome", "Company");
                        //}
                        //else
                        //{
                        //    objactionResult = RedirectToAction("MakeIcPayment", "Company", new { @type = objPackagePaymentModel.RedirectType, @Res = objPackagePaymentModel.PayType, @PkgAmt = objPackagePaymentModel.PackageAmount });
                        //}
                        if (objPackagePaymentModel.PayType.Equals("3"))
                        {
                            decisionPointEngine.SetTempPay(userId);
                           
                        }
                    }
                    

                    //check is payment status of user
                    objPackagePaymentModel.IsPayment = decisionPointEngine.getPaymentStatus(userId);
                }
                else
                {
                    objPackagePaymentModel.PayType = Shared.Fail;
                    objactionResult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
                objPackagePaymentModel = null;
                objactionResult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(objPackagePaymentModel, JsonRequestBehavior.AllowGet);
            //            return objactionResult;
        }
        [HttpGet]
        public ActionResult UpgradePackage(string packageId, int TblId)
        {
            logMessage = new StringBuilder();
            string result = string.Empty;
            List<int> pkgIds = new List<int>();
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objPackagePaymentModel = new PackagePaymentModel();
                    decisionPointEngine = new DecisionPointEngine();

                    if (!string.IsNullOrEmpty(packageId))
                    {
                        pkgIds = packageId.Split(char.Parse(Shared.Comma)).Select(x => int.Parse(x)).ToList();
                    }
                    objPackagePaymentModel.BackgroundPkgList = decisionPointEngine.GetPackagesDetails().Where(x => pkgIds.Contains(x.Id)).ToList();

                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = companyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                    objPackagePaymentModel.BackgroundList = decisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                    var userPkgCollection = objPackagePaymentModel.BackgroundList.Select(x => x.SterlingPkgId).Distinct().ToList();
                    var mappedPkgCol = objPackagePaymentModel.BackgroundList.Select(x => x.MappedSterlingPkg).Distinct().ToList();
                    foreach (var item in mappedPkgCol)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                          userPkgCollection= userPkgCollection.Union(item.Split(char.Parse(Shared.Comma)).Select(x => int.Parse(x))).ToList();
                        }
                    }
                    objPackagePaymentModel.BackgroundPkgList = objPackagePaymentModel.BackgroundPkgList.Where(x => !userPkgCollection.Contains(x.Id)).ToList();

                    objPackagePaymentModel.StateList = decisionPointEngine.GetStateList();
                    objPackagePaymentModel.RootUrl = ViewModel.GetSiteRoot();
                    objPackagePaymentModel.PaymentGetwayActionUrl = System.Configuration.ConfigurationManager.AppSettings["PaymentGetwayActionUrl"];
                    objPackagePaymentModel.PayPalAccount = System.Configuration.ConfigurationManager.AppSettings["PayPalAccount"];
                    ViewData.Model = objPackagePaymentModel;
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
        [HttpPost]
        public void UpgradePackage()
        {
            logMessage = new StringBuilder();
            try
            {
                decisionPointEngine = new DecisionPointEngine();
                string payPalUrl = Convert.ToString(ConfigurationManager.AppSettings["PaymentGetwayActionUrl"], CultureInfo.InvariantCulture);
                //string strLive = "https://www.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(payPalUrl);
                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(System.Web.HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                string strResponse_copy = strRequest;
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;
                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();
                streamIn.Close();

                if (strResponse == "VERIFIED")
                {
                    #region Parameters
                    Payment payment = new Payment();
                    string[] strArr = Request.Form["custom"].Split(',');
                    if (strArr.Length > 0)
                    {
                        #region Objects And Variables
                        objUserDashBoardResponse = new UserDashBoardResponse();
                        objViewModel = new ViewModel();
                        userId = Convert.ToInt32(strArr[1], CultureInfo.InvariantCulture);
                        companyId = Convert.ToString(strArr[0], CultureInfo.InvariantCulture);
                        string packageIds = strArr[4];
                        #endregion

                        #region Purchase Packages

                        objUserDashBoardResponse = decisionPointEngine.GetAccountDetails(userId);
                        if (!string.IsNullOrEmpty(packageIds))
                        {
                            List<int> packageIdList = packageIds.Split(char.Parse(Shared.Comma)).Select(x => int.Parse(x)).ToList();
                            foreach (int pkgId in packageIdList)
                            {
                                string packageName = decisionPointEngine.GetPackageNameAsPerId(pkgId);
                                objSterlingModel = new SterlingModel()
                                {
                                    FirstName = objUserDashBoardResponse.fName,
                                    LastName = objUserDashBoardResponse.lName,
                                    MiddelName = objUserDashBoardResponse.mName,
                                    EmailAddress = objUserDashBoardResponse.emailId,
                                    PackageId = pkgId,
                                    PackageName = packageName,
                                    State = objUserDashBoardResponse.StateName,
                                    City = objUserDashBoardResponse.CityName,
                                    ZipCode = objUserDashBoardResponse.ZipCode,
                                    AddressLine1 = objUserDashBoardResponse.StreetNumber + objUserDashBoardResponse.Direction + objUserDashBoardResponse.StreetName,
                                    PhoneNumber = objUserDashBoardResponse.officePhone,
                                    OperationType = 2,
                                    UserId = userId,
                                    CompanyId = companyId,
                                    PayType = Shared.Zero

                                };
                                objViewModel.InitiateOnSterling(objSterlingModel);
                            }
                        }
                        #endregion

                        #region Payment
                        payment.PaymentAmountList = decisionPointEngine.getPaymentAmount(strArr[0]).ToList();
                        if (payment.PaymentAmountList != null)
                        {
                            PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                            {
                                CompanyCode = strArr[0],
                                CompanyFee = Convert.ToDouble(Request.Form["payment_gross"]),
                                PerFieldStaffFee = 0,
                                PerOfficeStaffFee = 0,
                                PerIcFee = 0,
                                IsInvoice = false,

                                NoOfPartners = 0,
                                NoOfStaff = 0,
                                NoOfIc = 0,

                                BusinessName = strArr[2],
                                TransactionType = strArr[5],
                                userId = userId,
                                InviteeCompanyId = 0,
                                Result = Request.Form["txn_id"],
                                TransactionMessage = Request.Form["payment_status"],
                                PayAmount = Convert.ToDouble(Request.Form["payment_gross"]),
                                PayerEmailId = Request.Form["payer_email"],
                                ReceiverEmailId = Request.Form["receiver_email"],
                                Currency = Request.Form["mc_currency"],
                                PaymentDate = Request.Form["payment_date"]
                            };

                            int result = decisionPointEngine.companyPaymentbyPayPal(paymentAmountResponse);
                            if (result > 0)
                            {
                                #region Send Mail

                                string email = strArr[3];
                                string text = "<div style='line-height:25px'>Dear " + strArr[2] + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + Request.Form["payment_gross"] + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";
                                string subject = "Compliance Tracker Payment Success";
                                decisionPointEngine.GetSmtpDetail(email, text, subject);
                                #endregion
                            }

                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    logMessage.AppendLine("INVALID");
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
        }
        #endregion

        #region PayPal

        /// <summary>
        /// Used to Set the payment status for particular user and genrate the payment log in database
        /// </summary>
        /// <Createdby>Bobi</Createdby>
        /// <CreatedDate>9 Apr 2015</CreatedDate>
        [HttpPost]
        public void PayPalResponse()
        {
            logMessage = new StringBuilder();
            decisionPointEngine = new DecisionPointEngine();
            try
            {

                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Start", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string payPalUrl = Convert.ToString(ConfigurationManager.AppSettings["PaymentGetwayActionUrl"], CultureInfo.InvariantCulture);
                //string strLive = "https://www.paypal.com/cgi-bin/webscr";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(payPalUrl);
                //Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                byte[] param = Request.BinaryRead(System.Web.HttpContext.Current.Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                string strResponse_copy = strRequest;
                strRequest += "&cmd=_notify-validate";
                req.ContentLength = strRequest.Length;
                //Send the request to PayPal and get the response
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                streamOut.Close();
                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();
                streamIn.Close();

                if (strResponse == "VERIFIED")
                {
                    #region Parameters
                    Payment payment = new Payment();
                    string[] strArr = Request.Form["custom"].Split(',');
                    if (strArr.Length > 0)
                    {

                        payment.PaymentAmountList = decisionPointEngine.getPaymentAmount(strArr[0]).ToList();
                        if (payment.PaymentAmountList != null)
                        {
                            PaymentAmountResponse paymentAmountResponse = new PaymentAmountResponse
                             {
                                 CompanyCode = strArr[0],
                                 CompanyFee = payment.PaymentAmountList[0].CompanyFee,
                                 PerFieldStaffFee = payment.PaymentAmountList[0].PerFieldStaffFee,
                                 PerOfficeStaffFee = payment.PaymentAmountList[0].PerOfficeStaffFee,
                                 PerIcFee = payment.PaymentAmountList[0].PerIcFee,
                                 IsInvoice = payment.PaymentAmountList[0].IsInvoice,

                                 NoOfPartners = Convert.ToInt32(strArr[5], CultureInfo.InvariantCulture),
                                 NoOfStaff = Convert.ToInt32(strArr[6], CultureInfo.InvariantCulture),
                                 NoOfIc = Convert.ToInt32(strArr[7], CultureInfo.InvariantCulture),

                                 BusinessName = strArr[2],
                                 TransactionType = strArr[8],
                                 userId = Convert.ToInt32(strArr[1]),
                                 InviteeCompanyId = Convert.ToInt32(strArr[3]),
                                 Result = Request.Form["txn_id"],
                                 TransactionMessage = Request.Form["payment_status"],
                                 PayAmount = Convert.ToDouble(Request.Form["payment_gross"]),
                                 PayerEmailId = Request.Form["payer_email"],
                                 ReceiverEmailId = Request.Form["receiver_email"],
                                 Currency = Request.Form["mc_currency"],
                                 PaymentDate = Request.Form["payment_date"]
                             };

                            int result = decisionPointEngine.companyPaymentbyPayPal(paymentAmountResponse);
                            if (result > 0)
                            {
                                #region Send Mail

                                string email = strArr[4];
                                string text = "<div style='line-height:25px'>Dear " + strArr[2] + ", <br /> <br />Congratulations for joining Compliance Tracker. We have received payment of $" + Request.Form["payment_gross"] + ". <br /> <br /> <br /><br />Thank you.  <br />Compliance Tracker.</div>";
                                string subject = "Compliance Tracker Payment Success";
                                decisionPointEngine.GetSmtpDetail(email, text, subject);
                                //if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.NonClient))
                                //{
                                //    int ParentUserId = decisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.IC);
                                //    result = decisionPointEngine.ChangeRegistrationStatus(Convert.ToInt32(Session["UserId"]), ParentUserId, Shared.NonClient, Convert.ToString(Session["CompanyId"]));
                                //}

                                #endregion
                            }

                        }
                    }
                    #endregion
                }
                else
                {
                    logMessage.AppendLine("INVALID");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error", ex.ToString(), typeof(CompanyController).Name, MethodBase.GetCurrentMethod().Name);
                logMessage.AppendLine(ex.Message);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage);

            }
        }

        #endregion

        #region Admin Confirmation
        /// <summary>
        /// Used for show th admin notified Confirmation page
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>ActionResult</returns>
        /// <Createdby>Bobi</Createdby>
        /// <CreatedDate>15 Apr 2015</CreatedDate>
        public ActionResult AdminConfirmation(string companyId)
        {
            logMessage = new StringBuilder();
            try
            {
                registerStep1 = new RegisterStep1();
                registerStep1.CompanyId = companyId;
                ViewData.Model = registerStep1;
                objactionResult = View();
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
        #endregion

        #region Sterling Doc Attachement Mail to IC Guest
        /// <summary>
        /// Used for send the mail to IC guest SendMailToICGuest
        /// </summary>
        /// <Createdby>Bobi</Createdby>
        /// <CreatedDate>1 May 2015</CreatedDate>
        [HttpGet]
        public void SendMailToICGuest()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "Called {2} function ::{0} {1}.", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string filePath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["SterlingAggrementDoc"], CultureInfo.InvariantCulture)));
                List<string> attachedFiles = Directory.GetFiles(filePath).ToList();
                decisionPointEngine = new DecisionPointEngine();
                string body = "Hi<br/><br/>Please review and complete all attached files(s) of Sterling.";
                decisionPointEngine.GetSmtpWithAttachementDetail(Convert.ToString(Session["Emailid"], CultureInfo.InvariantCulture), body, attachedFiles);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("Error : {0}\n By : {1}-{2}", ex.ToString(), typeof(LoginController).Name, MethodBase.GetCurrentMethod().Name);
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, "End {0} function.", MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
        }
        #endregion

    }
}
