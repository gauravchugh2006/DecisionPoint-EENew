using System;
using System.Collections.Generic;
using System.Configuration;
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
using DecisionPoint.Models.DashBoardViewModel.ViewModel;
using DecisionPointBAL.Implementation;
using DecisionPointBAL.Implementation.Request;
using DecisionPointBAL.Implementation.Response;
using DecisionPointCAL;
using DecisionPointCAL.Common;


namespace DecisionPoint.Controllers
{
    /// <summary>
    /// Classed used for maintain user dashboard funtionality after login
    /// </summary>
    public class UserDashBoardController : Controller
    {
        #region GlobalVariables & Objects
        #region Variables
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        int userId = 0;
        string companyId = string.Empty;
        string parentUserId = string.Empty;
        int modifiedById = 0;
        List<string> serach1 = null;
        List<string> serach2 = null;
        List<string> serachFinal = null;
        BackGroundCheckMasterRequest objBackGroundCheckMasterRequest = null;
        #endregion
        #region Objects
        StringBuilder logMessage = null;
        DecisionPointEngine objDecisionPointEngine = null;
        UserDashBoard objUserDashBoard = null;
        JavaScriptSerializer javaScriptSerializer = null;
        ActionResult objactionresult = null;
        UserDashBoardRequest objUserDashBoardRequest = null;
        LicenseCheckRequest objLicenseCheckRequest = null;
        MyDashBoard objMyDashBoard = null;
        ViewModel objViewModel = null;
       
        #endregion

        #endregion

        #region Public Region

        /// <summary>
        /// get recevied communication details of particular login user
        /// </summary>
        /// <param name="id">string id</param>
        /// <createdby>bobi s</createdby>
        /// <createdDate>13 Nov 2013</createdDate>
        /// <returns>view document</returns>
        [HttpGet]
        public ActionResult DocumentAction(string id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    //get communication Details of particular login user
                    objUserDashBoard = objViewModel.GetMyCommunicationDetails(id, Shared.StaffCommunication);
                    ViewData.Model = objUserDashBoard;
                    if (Convert.ToBoolean(Session["UserDashboarInst"]).Equals(true))
                    {
                        ViewBag.UserDashboarInst = true;
                    }
                    objactionresult = View("UserDashBoard", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// history action
        /// </summary>
        /// <param name="id">string id</param>
        /// <createdby>Bobi s</createdby>
        ///  <createdDate>13 jan 2014</createdDate>
        /// <returns>view of history</returns>
        [HttpGet]
        public ActionResult HistoryAction(string id)
        {
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    //get communication Details of particular login user
                    objUserDashBoard = objViewModel.GetHistoryCommunicationDetails(id, Shared.Staff);

                    //assign the view model
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("_History", objUserDashBoard);

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }

        /// <summary>
        /// my profile view
        /// </summary>
        /// <createdby>Bobis</createdby>
        ///  <createdDate>13 feb 2014</createdDate>
        /// <returns>view of my profile</returns>
        [HttpGet]
        public ActionResult MyProfileAction()
        {
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {

                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    objUserDashBoard = objViewModel.GetMyProfileDetails(Shared.Staff);

                    //assign the view model
                    ViewData.Model = objUserDashBoard;
                    objactionresult = View("_AccountProfile", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// view document
        /// </summary>
        /// <param name="id">int id</param>
        /// <param name="tbId">int tblid</param>
        /// <createdby>bobis </createdby>
        ///  <createdDate>13 feb 2014</createdDate>
        /// <returns>view view document</returns>
        public ActionResult ViewDocument(int id, int tbId)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                objViewModel = new ViewModel();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    objUserDashBoard.GetUserViewList = objDecisionPointEngine.getUserViewList(Convert.ToInt32(Session["UserId"]), id).ToList();
                    objUserDashBoard.GetAcknowledgment = objDecisionPointEngine.GetAcknowledgment(id).ToList();
                    //check assessment exist or not
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    //if ((Convert.ToString(Session["Permission"], CultureInfo.InvariantCulture).Trim().ToUpper(CultureInfo.InvariantCulture).Equals("Admin".Trim().ToUpper(CultureInfo.InvariantCulture))) || Convert.ToString(Session["Permission"], CultureInfo.InvariantCulture).Trim().ToUpper(CultureInfo.InvariantCulture).Equals("ComplianceManager".Trim().ToUpper(CultureInfo.InvariantCulture)) || objDecisionPointEngine.GetUserPermission(Convert.ToInt32(Session["UserId"]), companyId).ToList()[0].permissionName.Trim().ToUpper(CultureInfo.InvariantCulture).Equals("Admin".Trim().ToUpper(CultureInfo.InvariantCulture)))
                    //{
                    //    ViewBag.PermissonType = "Admin";
                    //}
                    //else
                    //{
                    //    ViewBag.PermissonType = Convert.ToString(Session["Permission"], CultureInfo.InvariantCulture);
                    //}

                    Session["DocumentID"] = id;
                    Session["tableId"] = tbId;
                    //check assessment exists or not by sumit
                    objUserDashBoard.GetAssesmentViewList = objDecisionPointEngine.getAssesmentViewList(Convert.ToInt32(id, CultureInfo.InvariantCulture), false).ToList();
                    objDecisionPointEngine = new DecisionPointEngine();
                    //Check the impersination case and get the visitor userId
                    parentUserId = objViewModel.GetVisitorUserId(companyId);

                    ViewData.Model = objUserDashBoard;
                    ViewBag.tableId = tbId;
                    objactionresult = View(objUserDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }

        /// <summary>
        /// saveMarkAsComplete
        /// </summary>
        /// <param name="accepted">string accepted</param>
        /// <returns>json type result</returns>
        /// <createdby>Mamta</createdby>
        /// <createddate>13 feb 2014</createddate>
        [HttpPost]
        public JsonResult SaveMarkAsComplete(string accepted)
        {
            bool accept;
            int result = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (accepted.Trim().ToUpper(CultureInfo.InvariantCulture).Equals(Shared.TrueStr.Trim().ToUpper(CultureInfo.InvariantCulture)))
                {
                    accept = true;
                }
                else
                {
                    accept = false;
                }
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();

                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login");
                }
                else
                {
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    bool chkStatus = objDecisionPointEngine.CheckDocStatusAsPerCommunication(userId, Convert.ToInt32(Session["DocumentID"]));
                    if (chkStatus)
                    {
                        //used for complete the document
                        result = objDecisionPointEngine.SaveMarkAsComplete(Convert.ToInt32(Session["tableId"]), accept, Convert.ToInt32(Session["DocumentID"]));
                    }
                    else
                    {
                        result = 3;
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
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// save doc time
        /// </summary>
        /// <param name="documentID">int type documentID</param>
        /// <param name="countTimer">int countTimer</param>
        /// <createdby>Mamtag </createdby>
        ///  <createdDate>13 oct 2013</createdDate>
        /// <returns>return action save doc view time</returns>
        [HttpGet]
        public int SaveDocViewTime(int documentID, int countTimer)
        {
            int saveDocTime = 0;
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["tableId"], CultureInfo.InvariantCulture)) && string.IsNullOrEmpty(Convert.ToString(Session["DocumentID"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    DocTimeSpentRequest docTimeSpentRequest = new DocTimeSpentRequest()
                    {
                        userid = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                        docid = documentID,
                        DeleiverUserId = Convert.ToInt32(Session["tableId"], CultureInfo.InvariantCulture),
                        timespan = countTimer,
                        Completion = 0,

                    };
                    //used for save the doc spend time
                    saveDocTime = objDecisionPointEngine.SaveDocTimeSpent(docTimeSpentRequest);
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
            return saveDocTime;
        }

        /// <summary>
        /// ChangeDocumentStatus
        /// </summary>
        /// <param name="documentID">int document id</param>
        /// <param name="countTimer">int count timer</param>
        /// <createdby>MAMTA GUPTA</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>action result </returns>
        [HttpGet]
        public int ChangeDocumentStatus(int documentID, int countTimer)
        {
            int statusChanged = 0;
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    objUserDashBoard = new UserDashBoard();
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    //if (!Convert.ToString(Session["Permission"], CultureInfo.InvariantCulture).Equals("Admin") || !Convert.ToString(Session["Permission"], CultureInfo.InvariantCulture).Equals("Compliance Manager"))
                    //{
                    DocTimeSpentRequest docTimeSpentRequest = new DocTimeSpentRequest()
                    {
                        userid = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                        docid = documentID,
                        DeleiverUserId = Convert.ToInt32(Session["tableId"], CultureInfo.InvariantCulture),
                        timespan = countTimer,
                        Completion = 1,

                    };
                    statusChanged = objDecisionPointEngine.SaveDocTimeSpent(docTimeSpentRequest);
                    // }

                    objUserDashBoard.GetUserViewList = objDecisionPointEngine.getUserViewList(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToInt32(Session["DocumentID"], CultureInfo.InvariantCulture)).ToList();

                    ViewData.Model = objUserDashBoard;
                    objactionresult = View(objUserDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return statusChanged;

        }

        /// <summary>
        /// AssesssmentOpen
        /// </summary>
        /// <param name="page">int page</param>
        /// <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view assesment open</returns>
        [HttpGet]
        public ActionResult AssesssmentOpen(int? page)
        {
            bool randomQues, randomeAns;
            int id = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (object.Equals(Session["DocumentID"], null))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    id = Convert.ToInt32(Session["DocumentID"]);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objUserDashBoard = new UserDashBoard();
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                    {

                        return RedirectToAction("Login", "Login");
                    }
                    objUserDashBoard.GetInstructions = objDecisionPointEngine.getInstructions(Convert.ToInt32(id, CultureInfo.InvariantCulture)).ToList();
                    if (objUserDashBoard.GetInstructions.ToList() != null && objUserDashBoard.GetInstructions.Count() > 0)
                    {
                        randomQues = objUserDashBoard.GetInstructions.ToList()[0].RandQues;
                        randomeAns = objUserDashBoard.GetInstructions.ToList()[0].RandAns;

                    }
                    else
                    {
                        randomQues = false;
                        randomeAns = false;
                    }
                    objUserDashBoard.GetAssesmentViewList = objDecisionPointEngine.getAssesmentViewList(Convert.ToInt32(id, CultureInfo.InvariantCulture), randomQues).ToList();
                    objUserDashBoard.GetAssesmentAnsViewList = objDecisionPointEngine.getAssesmentAnsViewList(Convert.ToInt32(id, CultureInfo.InvariantCulture), randomeAns).ToList();

                    objUserDashBoard.Instructions = objUserDashBoard.GetInstructions.Select(x => x.Instruction).FirstOrDefault();
                    Session["DocumentID"] = id;

                    objDecisionPointEngine = new DecisionPointEngine();

                    ViewData.Model = objUserDashBoard;
                    objactionresult = View(objUserDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;

        }

        /// <summary>
        /// save assessment
        /// </summary>
        /// <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>saved assessment</returns>
        [HttpGet]
        public ActionResult SaveAsst()
        {
            int id = 0;
            decimal getScore = 0;
            int totalScore, countcorrectAns = 0;
            string asstResult;
            logMessage = new StringBuilder();


            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (object.Equals(Session["DocumentID"], null))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    id = Convert.ToInt32(Session["DocumentID"]);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objUserDashBoard = new UserDashBoard();
                    //called method from business layer for get the instruction details of particular communication
                    objUserDashBoard.GetInstructions = objDecisionPointEngine.getInstructions(Convert.ToInt32(id, CultureInfo.InvariantCulture)).ToList();
                    //called method from business layer for get the assessments details of particular communication
                    objUserDashBoard.GetAssesmentViewList = objDecisionPointEngine.getAssesmentViewList(Convert.ToInt32(id, CultureInfo.InvariantCulture), false).ToList();
                    //called method from business layer for get the correct nswers details of particular communication
                    objUserDashBoard.GetCorrectAnswer = objDecisionPointEngine.GetCorrectAnswer(id, Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture));
                    objUserDashBoard.Instructions = objUserDashBoard.GetInstructions.Select(x => x.Instruction).FirstOrDefault();
                    if (objUserDashBoard.GetAssesmentViewList.ToList() != null && objUserDashBoard.GetAssesmentViewList.Count() > 0)
                    {
                        totalScore = objUserDashBoard.GetAssesmentViewList.Count();
                        //set the result of assesments of particular user
                        foreach (var item in objUserDashBoard.GetAssesmentViewList)
                        {
                            countcorrectAns = objUserDashBoard.GetCorrectAnswer.Where(x => x.QuestionId == item.Id && x.OptionAnserId == x.GivenAnsId && x.Iscorrect == true).Count();

                        }
                        getScore = (Convert.ToDecimal(countcorrectAns)) / (Convert.ToDecimal(totalScore, CultureInfo.InvariantCulture));
                        getScore = getScore * 100;
                        if (Convert.ToInt32(getScore) >= Convert.ToInt32(objUserDashBoard.GetInstructions.ToList()[0].PassingScore))
                        {
                            asstResult = DecisionPointR.passinsmall;
                            ViewBag.AssessmentResult = DecisionPointR.pass;

                        }
                        else
                        {
                            asstResult = DecisionPointR.failinsmall;
                            ViewBag.AssessmentResult = DecisionPointR.Fail;
                        }

                        objDecisionPointEngine = new DecisionPointEngine();
                        objDecisionPointEngine.SaveAssessmentResult(Convert.ToInt32(Session["tableId"], CultureInfo.InvariantCulture), asstResult);

                    }
                    if (objUserDashBoard.GetInstructions.ToList() != null && objUserDashBoard.GetInstructions.Count() > 0)
                    {
                        ViewBag.ShowWronquestion = objUserDashBoard.GetInstructions.ToList()[0].ShowWrongeAns;
                    }
                    else
                    {
                        ViewBag.ShowWronquestion = false;
                    }
                    objactionresult = View(objUserDashBoard);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return objactionresult;

        }

        /// <summary>
        /// save assessment anser
        /// </summary>
        /// <param name="JsonData">json jsondata</param>
        /// <param name="attempts">int attempts</param>
        /// <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>json SaveAssessmentAnswers</returns>
        [HttpPost]
        public JsonResult SaveAssessmentAnswers(string JsonData, int attempts)
        {

            int result = 0;
            int totalAns = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                javaScriptSerializer = new JavaScriptSerializer();
                List<UserCorrectAsstAnswerResponse> objAnswer = new List<UserCorrectAsstAnswerResponse>();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login");
                }
                else
                {
                    if (!string.IsNullOrEmpty(JsonData))
                    {
                        objAnswer = (List<UserCorrectAsstAnswerResponse>)javaScriptSerializer.Deserialize(JsonData, typeof(List<UserCorrectAsstAnswerResponse>));
                        totalAns = objAnswer.Count();
                        objDecisionPointEngine = new DecisionPointEngine();
                        objUserDashBoard = new UserDashBoard();
                        UserSelectedAnswersRequest UserSelectedAnswersRequest = null;
                        foreach (var item in objAnswer)
                        {
                            UserSelectedAnswersRequest = new UserSelectedAnswersRequest()
                            {
                                userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                                docId = Convert.ToInt32(Session["DocumentID"], CultureInfo.InvariantCulture),
                                questionId = item.QuestionId,
                                AnswerId = item.GivenAnsId,
                                attempt = attempts,
                                tableid = Convert.ToInt32(Session["tableId"], CultureInfo.InvariantCulture)
                            };
                            //call the method for save the user selected answers
                            int res = objDecisionPointEngine.SaveUserSelectedAnswers(UserSelectedAnswersRequest);
                            if (res.Equals(2))
                            {
                                result = 2;
                                break;
                            }
                            else if (res.Equals(1))
                            {
                                result = result + 1;
                            }
                        }

                        if (result.Equals(totalAns))
                        {
                            result = 1;

                        }

                    }
                    else
                    {
                        result = 3;

                    }
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// SaveAsstAtempts
        /// </summary>
        /// <param name="attempts">int attempts</param>
        /// <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>json type saved or not</returns>
        [HttpPost]
        public JsonResult SaveAsstAtempts(int attempts)
        {
            int res = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    RedirectToAction("Login");
                }
                else
                {

                    objDecisionPointEngine = new DecisionPointEngine();
                    objUserDashBoard = new UserDashBoard();
                    res = objDecisionPointEngine.SaveUserAsstAttempts(Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture), Convert.ToInt32(Session["DocumentID"], CultureInfo.InvariantCulture), attempts);
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// training document
        /// </summary>
        /// <param name="docid">docid</param>
        /// <param name="status">status</param>
        /// <param name="filetype">filetype</param>
        /// <param name="fileTitle">fileTitle</param>
        /// <param name="FileLocation">FileLocation</param>
        /// <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>ActionResult</returns>
        [HttpGet]
        public ActionResult TrainingDocument(int docid, bool status, string filetype, string fileTitle, string FileLocation)
        {

            objDecisionPointEngine = new DecisionPointEngine();
            objUserDashBoard = new UserDashBoard();
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    return RedirectToAction("Login", "Login");
                }

                objDecisionPointEngine = new DecisionPointEngine();
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);

                ViewData.Model = objUserDashBoard;
                ViewBag.DocId = docid; ViewBag.Status = status; ViewBag.FileName = fileTitle; ViewBag.FileType = filetype; ViewBag.FileLocation = FileLocation;
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
            return View();
        }

        /// <summary>
        /// assessment
        /// </summary>
        ///  <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>view of assessment</returns>
        [HttpGet]
        public ActionResult Assesment()
        {
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
                else
                {
                    objactionresult = View();
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Used for delete any paricular document
        /// </summary>
        /// <param name="docId">int docId</param>
        /// <param name="">int type</param>
        ///  <createdby>Mamta gUpta</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>int</returns>
        public int RemoveDocument(int docId, int type)
        {
            logMessage = new StringBuilder();
            int removed = 0;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    removed = objViewModel.RemovedCommunication(docId, type);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return removed;
        }

        /// <summary>
        /// Used for Search in history section
        /// </summary>
        /// <param name="term">string term</param>
        ///  <createdby>bobi s</createdby>
        ///  <createdDate>18 mar 2014</createdDate>
        /// <returns></returns>
        [HttpGet]
        public JsonResult SerachInhistory(string term)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            List<string> serachByBoth = new List<string>();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    serachByBoth=objViewModel.SerachInHistoryCommunications(term);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                serachByBoth = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return this.Json(serachByBoth.Where(t => t.StartsWith(term)), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// used for view services details of any company
        /// </summary>
        /// <param name="UserId">string CompanyId</param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        /// <returns>string</returns>
        [HttpGet]
        public string ViewUserServices(string companyId)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objUserDashBoard = new UserDashBoard();
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard.ServiceDetails = objDecisionPointEngine.GetServices("user", companyId).Select(x => x.serviceName).ToList();

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard.ServiceDetails);
        }

        /// <summary>
        /// Updatemyprofile
        /// </summary>
        /// <param name="objUserDashBoard">UserDashBoard class</param>
        /// <param name="file"> file</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Updatemyprofile(UserDashBoard objUserDashBoard, HttpPostedFileBase file)
        {
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {

                    objViewModel = new ViewModel();
                    objViewModel.UpdateMyProfile(objUserDashBoard, file);
                    if (objUserDashBoard.ReqType.Equals(0))
                    {
                        objactionresult = RedirectToAction("MyProfileAction");
                    }
                    else if (objUserDashBoard.ReqType.Equals(1))
                    {
                        objactionresult = RedirectToAction("ICProfile");
                    }

                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// Update my profile services
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>21 mar 2014</createdDate>
        /// <returns>int </returns>
        [HttpPost]
        public int Updatemyprofileservices(string serviceId, int type)
        {
            logMessage = new StringBuilder();
            int inserted = 0;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);

                    objUserDashBoardRequest = new UserDashBoardRequest();
                    //Update My profileservices
                    objUserDashBoardRequest.ServiceId = serviceId;
                    objUserDashBoardRequest.UserId = userId;
                    objUserDashBoardRequest.CompanyCode = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objUserDashBoardRequest.Type = type;
                    //Update my profile details  
                    inserted = objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, Shared.Service);

                    if (Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture).Equals(Shared.Individual))
                    {
                        JobReqForNewHireRequest objJobReqForNewHireRequest = new JobReqForNewHireRequest()
                        {
                            companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture),
                            userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture),
                            userType = Shared.Staff,
                            inviteCompanyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture),
                            parentuserId = objDecisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Shared.Individual)
                        };

                        objDecisionPointEngine.UpdateJobComplianceReqAsPerService(serviceId, objJobReqForNewHireRequest);
                    }

                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return inserted;
        }

        /// <summary>
        /// Updatemyprofileclient
        /// </summary>
        /// <param name="clientId">string clientId</param>
        /// <returns>int</returns>
        ///  <createdby>bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        [HttpPost]
        public int Updatemyprofileclient(string clientId)
        {
            logMessage = new StringBuilder();
            int inserted = 0;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objUserDashBoardRequest = new UserDashBoardRequest();
                    objUserDashBoardRequest.ClientId = clientId;
                    objUserDashBoardRequest.UserId = userId;
                    //Update client details on my profile view
                    inserted = objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, Shared.Client.ToLower(CultureInfo.InvariantCulture));
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return inserted;
        }

        /// <summary>
        /// Updatemyprofilepassword
        /// </summary>
        /// <param name="newpwd">new password</param>
        /// <returns>int</returns>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>13 mar 2014</createdDate>
        [HttpPost]
        public int Updatemyprofilepassword(string newpwd)
        {
            logMessage = new StringBuilder();
            int inserted = 0;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objUserDashBoardRequest = new UserDashBoardRequest();
                    //Update My profileservices
                    objUserDashBoardRequest.NewPwd = newpwd.Trim();
                    objUserDashBoardRequest.UserId = userId;
                    //Update my profile details  
                    inserted = objDecisionPointEngine.Updatemyprofile(objUserDashBoardRequest, Shared.Password);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return inserted;
        }


        /// <summary>
        /// GetCityDetails
        /// </summary>
        /// <param name="stateName">stateName</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>string</returns>
        [HttpGet]
        public string GetCityDetails(string stateName, string type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    if (string.IsNullOrEmpty(type))
                    {
                        objUserDashBoard.CityDetails = objDecisionPointEngine.GetCityListByStateName(stateName, userId);
                    }
                    else
                    {
                        objUserDashBoard.CityDetails = objDecisionPointEngine.GetCityList(userId, companyId, 0);
                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard.CityDetails = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard.CityDetails);
        }
        /// <summary>
        /// GetCountyDetail
        /// </summary>
        /// <param name="stateName">stateName</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>string</returns>
        [HttpGet]
        public string GetCountyDetail(string stateName, string type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objUserDashBoard = new UserDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    if (!string.IsNullOrEmpty(type))
                    {
                        if (type.Trim().ToUpper(CultureInfo.InvariantCulture).Equals(Shared.Vendor.Trim().ToUpper(CultureInfo.InvariantCulture)))
                        {
                            //user session user id
                            userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            //get parent user id for get the coverage area for staff
                            userId = objDecisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                        }
                    }
                    objUserDashBoard.CountyDetails = objDecisionPointEngine.GetCountyListByStateName(stateName, userId).ToList();
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard.CountyDetails = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard.CountyDetails);
        }
        /// <summary>
        /// GetZipDetails
        /// </summary>
        /// <param name="stateName">stateName</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>string</returns>
        [HttpGet]
        public string GetZipDetails(string stateName, string type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objUserDashBoard = new UserDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    if (string.IsNullOrEmpty(type))
                    {
                        objUserDashBoard.ZipDetails = objDecisionPointEngine.GetzipListByStateName(stateName, userId);
                    }
                    else
                    {
                        objUserDashBoard.ZipDetails = objDecisionPointEngine.GetzipList(userId, companyId, 0);
                    }

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard.ZipDetails = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard.ZipDetails);
        }

        /// <summary>
        /// GetZipDetailBySelectedCity
        /// </summary>
        /// <param name="cityName">CityName</param>
        /// <param name="type">type</param>
        ///  <createdby>sumit saurav</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        /// <returns>string</returns>
        public string GetZipDetailBySelectedCity(string cityName, string type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objUserDashBoard = new UserDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    if (string.IsNullOrEmpty(type))
                    {
                        objUserDashBoard.ZipDetails = objDecisionPointEngine.GetzipListBySelectedCity(cityName, userId);
                    }
                    else
                    {
                        userId = objDecisionPointEngine.GetParentUserId(Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture), Convert.ToString(Session["UserType"], CultureInfo.InvariantCulture));
                        objUserDashBoard.ZipDetails = objDecisionPointEngine.GetzipListBySelectedCity(cityName, userId);
                    }

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objUserDashBoard.ZipDetails = null;
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(objUserDashBoard.ZipDetails);
        }

        /// <summary>
        /// ICClientList
        /// </summary>
        /// <param name="id">int id</param>
        /// <returns>actionresult</returns>
        ///  <createdby>Bobis</createdby>
        ///  <createdDate>10 jan 2014</createdDate>
        [HttpGet]
        public ActionResult ICClientList(int id)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                objUserDashBoard = new UserDashBoard();

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    if (id.Equals(0))
                    {
                        objUserDashBoard.PastClientList = objDecisionPointEngine.GetICClientList(userId, false);
                        objUserDashBoard.PageSize = objUserDashBoard.PastClientList.Count();
                        TempData["isCurrent"] = "0";
                    }
                    else
                    {
                        objUserDashBoard.CurrentClientList = objDecisionPointEngine.GetICClientList(userId, true);
                        objUserDashBoard.PageSize = objUserDashBoard.CurrentClientList.Count();
                        TempData["isCurrent"] = "1";
                    }

                    objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    objViewModel = new ViewModel();

                    ViewData.Model = objUserDashBoard;
                    objactionresult = View(objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// GetGuideInstruction
        /// </summary>
        /// <param name="IsActive">IsActive</param>
        /// <returns>string</returns>
        ///  <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 mar 2014</createdDate>
        [HttpGet]
        public string GetGuideInstruction(bool isActive)
        {
            logMessage = new StringBuilder();
            string instruction = string.Empty;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objViewModel = new ViewModel();
                    instruction = objViewModel.GetGuidInstruction(isActive);
                    if (isActive.Equals(true))
                    {
                        TempData["GuideInstruction"] = instruction;
                        TempData["GuideStatus"] = true;
                    }
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return instruction;
        }

        /// <summary>
        /// SetTempData
        /// </summary>
        /// <param name="instructions">Instruction class</param>
        /// <createdby>Mamta Gupta</createdby>
        ///  <createdDate>10 mar 2014</createdDate>
        /// <returns>int</returns>
        [HttpPost]
        public int SetTempData(Instruction instructions)
        {
            logMessage = new StringBuilder();
            int setTemp = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(instructions.GuidInstruction))
                {
                    TempData.Keep("GuideInstruction");
                    TempData.Keep("GuideStatus");
                    TempData["GuideInstruction"] = instructions.GuidInstruction;
                    TempData["GuideStatus"] = true;
                }
                else
                {
                    TempData.Remove("GuideInstruction");
                    TempData.Remove("GuideStatus");
                }
                setTemp = 1;
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return setTemp;
        }

        /// <summary>
        /// get receiver required doc
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>june 5 2014</createddate>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult GetReceiverDoc(int reqDocId, int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            IList<ReceiverReqDocResponse> GetReceiverDoc = null;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (type.Equals(0) || type.Equals(1))
                    {
                        GetReceiverDoc = objDecisionPointEngine.GetReceiverRequiredDoc(reqDocId, userId, companyId);
                    }
                    else { GetReceiverDoc = objDecisionPointEngine.GetLicAndInsAsPerUnqiueId(reqDocId, userId, companyId).ToList(); }

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(GetReceiverDoc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// SetReceiverReqDocDetails
        /// </summary>
        /// <param name="objModel">ReceiverReqDocModel</param>
        /// <createdby>sumit saurav</createdby>
        /// <createddate>june 6 2014</createddate>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult SetReceiverReqDocDetails(ReceiverReqDocModel objModel)
        {
            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    #region Parameter Assignment
                    int visitorId = string.IsNullOrEmpty(Convert.ToString(Session["ParentUserId"])) ? Convert.ToInt32(Session["UserId"]) : Convert.ToInt32(Session["superAdmin"]);
                    ReceiverReqDocRequest receiverReqDocRequest = new ReceiverReqDocRequest()
                    {
                        CompanyName = objModel.CompanyName,
                        PolicyNumber = objModel.PolicyNumber,
                        LisenceNumber = objModel.LisenceNumber,
                        ExpirationDate = objModel.ExpirationDate,
                        StateAbbre = objModel.StateAbbre,
                        CompanyId = Convert.ToString(Session["CompanyId"]),
                        UserId = Convert.ToInt32(Session["UserId"]),
                        ReqDocId = objModel.ReqDocId,
                        DocLoc = objModel.DocLoc,
                        IsCompleted = objModel.IsCompleted,
                        VisitorId = visitorId,
                    };
                    #endregion
                    result = objDecisionPointEngine.SetReceiverReqDocDetails(receiverReqDocRequest);

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetReqDocAudit
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>Nov 10 2014</CreatedDate>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult GetReqDocAudit(int reqDocId)
        {

            //Used for display log text
            logMessage = new StringBuilder();
            IList<ReceiverReqDocResponse> GetReceiverDocAudit = null;
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    GetReceiverDocAudit = objDecisionPointEngine.GetReceiverReqDocAudit(reqDocId);
                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(GetReceiverDocAudit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// MarkDocComplete
        /// </summary>
        /// <param name="reqDocId">reqDocId</param>
        ///  <createdby>sumit saurav</createdby>
        /// <createddate>june 6 2014</createddate>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult MarkDocComplete(int reqDocId)
        {
            logMessage = new StringBuilder();
            int result = 0;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    result = objDecisionPointEngine.MarkReceiverDocComplete(reqDocId, Convert.ToInt32(Session["UserId"]), Convert.ToString(Session["CompanyId"]));

                }
                else
                {
                    RedirectToAction("Login", "Login");
                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// upload Emp req Doc
        /// </summary>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>28 may 2014</createdDate>
        public void UploadEmpReqDoc()
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                string title = string.Empty;
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    if (!object.Equals(Request.Cookies["usertitlename"], null))
                    {
                        title = Request.Cookies["usertitlename"].Value;
                        //call method of web API for upload the selected file in folder
                        UploadController.Uploadempdocdoc(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture), title);
                    }
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

        }

        /// <summary>
        /// Used to set the titlename for set the uploaded document
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        /// <createdBy>bobis</createdBy>
        /// <createdDate>28 may 2014</createdDate>
        public string SettheEmpDocName(string title, int type)
        {
            string newFileName = string.Empty;
            logMessage = new StringBuilder();
            try
            {
                int count = 1;
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                string laststr = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                if (type.Equals(1))
                {
                    laststr = Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture);
                }
                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr;
                if (!string.IsNullOrEmpty(title))
                {
                    if (title.Length > 2)
                    {
                        if (title.Substring(0, 1) == Shared.One && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == Shared.Two && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == Shared.Three && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture) + laststr;
                        }
                        else if (title.Substring(0, 1) == "4" && !title.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture) + laststr;
                        }
                    }

                    //set the new file name
                    if (Directory.Exists(HttpContext.Server.MapPath(folderDirectory)))
                    {

                        foreach (var fn in Directory.GetFiles(HttpContext.Server.MapPath(folderDirectory)))
                        {
                            if (!string.IsNullOrEmpty(Path.GetFileName(fn)))
                            {
                                if (title.Contains(char.Parse(Shared.DollarSign)))
                                {
                                    if (Path.GetFileName(fn).Split(char.Parse(Shared.ExclamationMark))[1].Split(char.Parse(Shared.UnderScore))[0].Equals(Regex.Replace(title.Split(char.Parse(Shared.DollarSign))[1].Substring(1, (title.Split(char.Parse(Shared.DollarSign))[1].Length - 1)), @"\s+", " ")))
                                    {
                                        count += 1;
                                    }
                                }
                                else
                                {
                                    if (Path.GetFileName(fn).Split('_')[0].Equals(Regex.Replace(title.Substring(1, (title.Length - 1)), @"\s+", " ")))
                                    {
                                        count += 1;
                                    }
                                }
                            }
                        }
                        //set the file name as per title
                        newFileName = title + Shared.UnderScore + count;
                    }
                    else
                    {
                        //set the file name as per title
                        newFileName = title + Shared.UnderScore + 1;
                    }
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
            return newFileName;
        }

        /// <summary>
        /// Used to create & erase the cookie for title name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        [HttpPost]
        public void CreateEraseTitleNameCookie(string name, int type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (type.Equals(0))
                {
                    HttpCookie mytitleCookie = new HttpCookie("usertitlename");
                    mytitleCookie.Value = name;
                    HttpContext.Response.Cookies.Add(mytitleCookie);
                }
                else if (type.Equals(1))
                {
                    if (!object.Equals(Request.Cookies["usertitlename"], null))
                    {
                        HttpCookie myCookie = new HttpCookie("usertitlename");
                        myCookie.Expires = DateTime.Now.AddDays(-1d);
                    }
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
        }

        /// <summary>
        /// Used to get the IC profile details
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createdDate>23 july 2014</createdDate>
        [HttpGet]
        public ActionResult ICProfile(string parentCompanyId)
        {
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objViewModel = new ViewModel();
                    objUserDashBoard = objViewModel.GetMyProfileDetails(Shared.IC);
                    //set type for retrun IC profile or user profile as per condition
                    objUserDashBoard.ReqType = 1;
                    if (!string.IsNullOrEmpty(parentCompanyId))
                    {
                        #region Get Client View By IC
                        objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = parentCompanyId, UserId = userId, CompanyId = companyId, OperationType = 3 };
                        objUserDashBoard.ProfessionalLicense = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalInsurance = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.AdditionalRequirements = objDecisionPointEngine.GetAdditionalRequirement(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                        objUserDashBoard.ProfessionalCommunications = objDecisionPointEngine.GetProfessionalCommunication(userId, companyId).Where(x => x.CreatorCompanyid == parentCompanyId).ToList();

                        objUserDashBoard.BackgroundList = objDecisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                        objUserDashBoard.StateList = objDecisionPointEngine.GetStateList();
                        #endregion
                    }
                    objUserDashBoard.ParentCompanyId = parentCompanyId;
                    string currentClientCompanyId = string.Empty;
                    
                    //assign the view model
                    ViewData.Model = objUserDashBoard;
                    objUserDashBoard.BusinessClassDetails = objDecisionPointEngine.GetBusinessClass();
                    objactionresult = View("ICProfile", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
       
        [HttpPost]
        public int SetICClientPermissions(int isVisible, string visibleTo)
        {
            int inserted = 0;
            logMessage = new StringBuilder();
            IList<ICClientPermissionRequest> icClientList = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    ICClientPermissionRequest objICClientPermissionRequest = new ICClientPermissionRequest()
                    {
                        ICCompanyId = companyId,
                        ICUserId = userId,
                        IsVisible = isVisible == 0 ? false : true,
                        VisibleTo = visibleTo
                    };
                    //call method for Set IC Client Permissions
                    inserted = objDecisionPointEngine.SetICClientPermissions(objICClientPermissionRequest);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return inserted;
        }
        [HttpGet]
        public JsonResult GetICClientPermissions()
        {
            logMessage = new StringBuilder();
            IList<ICClientPermissionRequest> icClientList = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    ICClientPermissionRequest objICClientPermissionRequest = new ICClientPermissionRequest()
                    {
                        ICCompanyId = companyId,
                        ICUserId = userId
                    };
                    icClientList = objDecisionPointEngine.GetICClientPermissions(objICClientPermissionRequest);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return Json(icClientList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Used for rendered he global reqiurement in partial view
        /// </summary>
        /// <param name="userCcompanyId">companyId</param>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createdate>14 Aug 2014</createdate>
        [HttpGet]
        public ActionResult GlobalReqiurement(string parentCcompanyId)
        {
            logMessage = new StringBuilder();
            logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
            try
            {
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    objUserDashBoard.ReqiuredDocumentsDetails = objDecisionPointEngine.GetReqiuredDocuments(userId, companyId).Where(x => x.ReqType == 1 && x.CreatorCompanyid == parentCcompanyId);
                    objUserDashBoard.SpecificDocumentsDetails = objDecisionPointEngine.GetReqiuredDocuments(userId, companyId).Where(x => x.ReqType == 2 && x.CreatorCompanyid == parentCcompanyId);
                    ViewData.Model = objUserDashBoard;
                    objactionresult = PartialView("GlobalReqiurement", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }


            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }

        /// <summary>
        /// Used for update the bio info of IC
        /// </summary>
        /// <param name="bioInfo">string bioInfo</param>
        /// <returns>int</returns>
        /// <createdby>Bobi</createby>
        /// <crateddate>30 AUg 2014</crateddate>
        [HttpPost]
        public int UpdateBioInfo(string bioInfo)
        {
            int updated = 0;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    //call method for update the Bio Information on my profile view
                    updated = objDecisionPointEngine.UpdateBioInfo(bioInfo, userId);
                }
                else
                {
                    RedirectToAction("Login", "Login");

                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });

            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return updated;
        }

        ///// <summary>
        ///// Used for verfy the license details
        ///// </summary>
        ///// <param name="objLicenseModel"></param>
        ///// <returns>int</returns>
        ///// <createdby>Bobi</createdby>
        ///// <createddate>31 july 2014</createddate>
        //[HttpPost]
        //public int CheckLicense(LicenseModel objLicenseModel)
        //{
        //    int checkedLic = 0;
        //    logMessage = new StringBuilder();
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            objDecisionPointEngine = new DecisionPointEngine();
        //            userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
        //            companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
        //            using (ASCQuerySvcSoapClient oSvc = new ASCQuerySvcSoapClient())
        //            {
        //                //get the all querable fields
        //                FieldHelper[] allFields = oSvc.GetQueryableFields(null, false);
        //                //set the all qrerable fields in Array
        //                List<string> fieldNames = new List<string>();
        //                foreach (FieldHelper curField in allFields)
        //                {
        //                    fieldNames.Add(curField.FieldName);
        //                }
        //                //set the filter values for verfy the license
        //                FilterCondition oFilter1 = new FilterCondition();
        //                oFilter1.FieldName = "St_Abbr";
        //                oFilter1.Operator = AvailableOperators.Equals;
        //                oFilter1.Operands = new string[] { objLicenseModel.StateAbbre };
        //                FilterCondition oFilter2 = new FilterCondition();
        //                oFilter2.FieldName = "Lic_Number";
        //                oFilter2.Operator = AvailableOperators.Equals;
        //                oFilter2.Operands = new string[] { objLicenseModel.LicenseNumber };
        //                FilterCondition oFilter3 = new FilterCondition();
        //                oFilter3.FieldName = "Exp_Date";
        //                oFilter3.Operator = AvailableOperators.Equals;
        //                oFilter3.Operands = new string[] { Convert.ToString(objLicenseModel.ExpirationDate) };

        //                //get the response after verfy the license
        //                DataSet LicenseResult = oSvc.RunQuery(null, fieldNames.ToArray(), new FilterCondition[] { oFilter1, oFilter2, oFilter3 }, true, 0);
        //                if (!object.Equals(LicenseResult, null))
        //                {
        //                    if (LicenseResult.Tables[0].Rows.Count > 0)
        //                    {
        //                        objLicenseCheckRequest = new LicenseCheckRequest()
        //                        {
        //                            CompanyId = companyId,
        //                            UserId = userId,
        //                            LicenseNumber = Convert.ToString(LicenseResult.Tables[0].Rows[0]["lic_number"], CultureInfo.InvariantCulture),
        //                            LicenseType = Convert.ToString(LicenseResult.Tables[0].Rows[0]["lic_type"], CultureInfo.InvariantCulture),
        //                            LName = Convert.ToString(LicenseResult.Tables[0].Rows[0]["lname"], CultureInfo.InvariantCulture),
        //                            FName = Convert.ToString(LicenseResult.Tables[0].Rows[0]["fname"], CultureInfo.InvariantCulture),
        //                            StateAbbre = Convert.ToString(LicenseResult.Tables[0].Rows[0]["state"], CultureInfo.InvariantCulture),
        //                            County = Convert.ToString(LicenseResult.Tables[0].Rows[0]["county_code"], CultureInfo.InvariantCulture),
        //                            Zip = Convert.ToString(LicenseResult.Tables[0].Rows[0]["zip"], CultureInfo.InvariantCulture),
        //                            City = Convert.ToString(LicenseResult.Tables[0].Rows[0]["city"], CultureInfo.InvariantCulture),
        //                            Phone = Convert.ToString(LicenseResult.Tables[0].Rows[0]["phone"], CultureInfo.InvariantCulture),
        //                        };


        //                        if (!string.IsNullOrEmpty(Convert.ToString(LicenseResult.Tables[0].Rows[0]["eff_date"], CultureInfo.InvariantCulture)))
        //                        {
        //                            objLicenseCheckRequest.EffectiveDate = Convert.ToDateTime(LicenseResult.Tables[0].Rows[0]["eff_date"], CultureInfo.InvariantCulture);
        //                        }
        //                        if (!string.IsNullOrEmpty(Convert.ToString(LicenseResult.Tables[0].Rows[0]["eff_date"], CultureInfo.InvariantCulture)))
        //                        {
        //                            objLicenseCheckRequest.IssueDate = Convert.ToDateTime(LicenseResult.Tables[0].Rows[0]["issue_date"], CultureInfo.InvariantCulture);
        //                        }
        //                        if (!string.IsNullOrEmpty(Convert.ToString(LicenseResult.Tables[0].Rows[0]["eff_date"], CultureInfo.InvariantCulture)))
        //                        {
        //                            objLicenseCheckRequest.ExpDate = Convert.ToDateTime(LicenseResult.Tables[0].Rows[0]["exp_date"], CultureInfo.InvariantCulture);
        //                        }
        //                        //call method for save the log details of licence and insurance in database
        //                        checkedLic = objDecisionPointEngine.InsertLicenseCheckSumm(objLicenseCheckRequest);
        //                    }
        //                    else
        //                    {
        //                        checkedLic = -1;
        //                    }
        //                }
        //                else
        //                {
        //                    checkedLic = -1;
        //                }
        //            };
        //        }
        //        else
        //        {
        //            RedirectToAction("Login", "Login");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return checkedLic;
        //}

        ///// <summary>
        ///// SetReceiverLicAndInsDetails
        ///// </summary>
        ///// <param name="objModel">ReceiverReqDocModel</param>
        ///// <createdby>Bobi</createdby>
        ///// <createddate>Sep 4 2014</createddate>
        ///// <returns>JsonResult</returns>
        //[HttpPost]
        //public JsonResult SetReceiverLicAndInsDetails(ReceiverReqDocModel objModel)
        //{
        //    logMessage = new StringBuilder();
        //    int result = 0;
        //    bool chkStatus = true;
        //    try
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
        //        {
        //            userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
        //            companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
        //            modifiedById = Convert.ToInt32(Session["superAdmin"], CultureInfo.InvariantCulture);
        //            objDecisionPointEngine = new DecisionPointEngine();
        //            if (!object.Equals(objModel, null))
        //            {
        //                if (objModel.Type.Equals(2) && objModel.IsCompleted.Equals(1) && objModel.LiceAndInsType.Equals(Shared.Zero))
        //                {
        //                    LicenseModel objLicenseModel = new LicenseModel()
        //                    {
        //                        LicenseNumber = objModel.LisenceNumber,
        //                        StateAbbre = objModel.StateAbbre,
        //                        ExpirationDate = objModel.ExpirationDate,
        //                        CompanyName = objModel.CompanyName
        //                    };
        //                    result = CheckLicense(objLicenseModel);
        //                    if (result.Equals(-1))
        //                    {
        //                        chkStatus = false;
        //                    }
        //                }
        //            }
        //            if (chkStatus)
        //            {
        //                #region Parameter Assignment

        //                ReceiverReqDocRequest receiverReqDocRequest = new ReceiverReqDocRequest()
        //                {
        //                    CompanyName = objModel.CompanyName,
        //                    PolicyNumber = objModel.PolicyNumber,
        //                    LisenceNumber = objModel.LisenceNumber,
        //                    ExpirationDate = objModel.ExpirationDate,
        //                    StateAbbre = objModel.StateAbbre,
        //                    CompanyId = companyId,
        //                    UserId = userId,
        //                    ReqDocId = objModel.ReqDocId,
        //                    DocLoc = objModel.DocLoc,
        //                    IsCompleted = objModel.IsCompleted,
        //                    Type = objModel.Type,
        //                    Ack = objModel.Ack,
        //                    Title = objModel.Title,
        //                    ModifiedById = modifiedById
        //                };
        //                #endregion


        //                result = objDecisionPointEngine.SetReceiverLicAndInsDetails(receiverReqDocRequest);
        //            }
        //        }
        //        else
        //        {
        //            RedirectToAction("Login", "Login");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
        //        RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
        //    }
        //    finally
        //    {
        //        logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
        //        log.Info(logMessage.ToString());
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Used for show MY dashboard page
        /// </summary>
        /// <param name="date">string date</param>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>5 aug 2014</createddate>
        [HttpGet]
        public ActionResult MyDashBoard(string date)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objMyDashBoard = new Models.MyDashBoard();
                    objViewModel = new ViewModel();
                    objMyDashBoard = objViewModel.GetMyDashBoardAlerts(date);
                    ViewData.Model = objMyDashBoard;
                    objactionresult = View();
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }


        /// <summary>
        /// Used for get background mapping details 
        /// </summary>
        /// <param name="backMasterId"></param>
        /// <returns>string</returns>
        /// <createdby>Sumit</createdby>
        /// <createddate>22 Nov 2014</createddate>
        [HttpGet]
        public string GetBackgroundMapping(int backMasterId)
        {
            logMessage = new StringBuilder();
            IList<BackGroundCheckMasterRequest> result = null;
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                objDecisionPointEngine = new DecisionPointEngine();
                string companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                objViewModel = new ViewModel();
                int parentUserId = Convert.ToInt32(objViewModel.GetVisitorUserId(companyId), CultureInfo.InvariantCulture);
                int userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                result = objDecisionPointEngine.GetBackgroundCheckMaster(companyId).ToList();

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(result);
            // return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// used for upload the document in background details section
        /// </summary>
        /// <param name="reqDocId"></param>
        /// <returns>string</returns>
        /// <createdby>Sumit</createdby>
        /// <createddate>22 Nov 2014</createddate>
        public string GetBackgroundUploadDocById(int reqDocId)
        {
            IList<UploadDocDetailsRequest> uploadlist = null;
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    RedirectToAction("Login", "Login");
                }
                else
                {
                    objDecisionPointEngine = new DecisionPointEngine();
                    uploadlist = objDecisionPointEngine.GetBackgroundUploadDocById(reqDocId);
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
            javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(uploadlist);

        }

        /// <summary>
        /// used for get non client ic list
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <createdby>Bobi</createdby>
        /// <createddate>1 Jan 2015</createddate>
        [HttpGet]
        public ActionResult NonClientICList(int id, string type)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));

                //Create Object for Get User DashBoard Details
                objDecisionPointEngine = new DecisionPointEngine();
                objUserDashBoard = new UserDashBoard();
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    if (id.Equals(0))
                    {
                        //Called method for get internal staff details of particular company
                        objUserDashBoard.CurrentClientList = objDecisionPointEngine.GetICNonClientDetails(companyId, id);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (type.Contains(Shared.Colon))
                            {
                                if (!type.Split(char.Parse(Shared.Colon))[0].Equals(Shared.All))
                                {
                                    objUserDashBoard.CurrentClientList = objUserDashBoard.CurrentClientList.Where(x => x.VendorType == type.Split(char.Parse(Shared.Colon))[0]).ToList();
                                }
                            }
                            else if (!string.Equals(type, Shared.All))
                            {
                                objUserDashBoard.CurrentClientList = objUserDashBoard.CurrentClientList.Where(x => x.vendor.StartsWith(type) || x.contact.StartsWith(type)).ToList();
                            }
                        }
                        objUserDashBoard.VendorTypeDetails = objUserDashBoard.CurrentClientList.Select(x => x.VendorType).Distinct().ToList();
                        objUserDashBoard.PageSize = objUserDashBoard.CurrentClientList.Count();
                        objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }
                    else if (id.Equals(1))
                    {
                        //Called method for get internal staff details of particular company
                        objUserDashBoard.PastClientList = objDecisionPointEngine.GetICNonClientDetails(companyId, id);
                        if (!string.IsNullOrEmpty(type))
                        {
                            if (type.Contains(Shared.Colon))
                            {
                                if (!type.Split(char.Parse(Shared.Colon))[0].Equals(Shared.All))
                                {
                                    objUserDashBoard.PastClientList = objUserDashBoard.PastClientList.Where(x => x.VendorType == type.Split(char.Parse(Shared.Colon))[0]).ToList();
                                }
                            }
                            else if (!string.Equals(type, Shared.All))
                            {
                                objUserDashBoard.PastClientList = objUserDashBoard.PastClientList.Where(x => x.vendor.StartsWith(type) || x.contact.StartsWith(type)).ToList();
                            }
                        }
                        objUserDashBoard.VendorTypeDetails = objUserDashBoard.PastClientList.Select(x => x.VendorType).Distinct().ToList();
                        objUserDashBoard.PageSize = objUserDashBoard.PastClientList.Count();
                        objUserDashBoard.RowperPage = Convert.ToInt32(ConfigurationManager.AppSettings["rowperpage"], CultureInfo.InvariantCulture);
                    }

                    ViewData.Model = objUserDashBoard;
                    objactionresult = PartialView("NonClientICList", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");

                }

            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(CompanyDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = View("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        /// <summary>
        /// RemoveICNonClient
        /// </summary>
        /// <param name="id">unique id of vendor</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>jan 21 2015</createddate>
        /// <returns>result in inetger type</returns>
        [HttpPost]
        public int RemoveICNonClient(int id)
        {
            logMessage = new StringBuilder();
            int response = 0;
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.RemoveICNonClient(id, companyId);
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
            return response;
        }
        /// <summary>
        /// ReactiveICNonClient
        /// </summary>
        /// <param name="id">unique id of vendor</param>
        /// <createdby>Bobi s</createdby>
        /// <createddate>jan 21 2015</createddate>
        /// <returns>result updated or not in int type</returns>
        [HttpPost]
        public int ReactiveICNonClient(int id)
        {
            int response = 0;
            logMessage = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    response = objDecisionPointEngine.ReactiveICNonClient(id, companyId);
                    logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
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
            return response;
        }
        /// <summary>
        /// search in IC
        /// </summary>
        /// <param name="term">search by term</param>
        /// <createdBy>bobi</createdBy>
        /// <createdDate>23 dec 2014</createdDate>
        /// <returns>json type result</returns>
        [HttpPost]
        public JsonResult SerachInICNonClients(string term, int type)
        {
            //Used for display log text
            logMessage = new StringBuilder();
            try
            {
                objUserDashBoard = new UserDashBoard();
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    objUserDashBoard.CurrentClientList = null;
                }
                else
                {
                    userId = Convert.ToInt32(HttpContext.Session["UserId"], CultureInfo.InvariantCulture);
                    companyId = Convert.ToString(HttpContext.Session["CompanyId"], CultureInfo.InvariantCulture);
                    objDecisionPointEngine = new DecisionPointEngine();
                    objUserDashBoard.CurrentClientList = objDecisionPointEngine.GetICNonClientDetails(companyId, type);
                    if (!string.IsNullOrEmpty(term))
                    {
                        serach1 = objUserDashBoard.CurrentClientList.Where(x => (x.vendor.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(y => y.vendor).Distinct().ToList();
                        serach2 = objUserDashBoard.CurrentClientList.Where(x => (x.contact.StartsWith(term, StringComparison.OrdinalIgnoreCase))).Select(y => y.contact).Distinct().ToList();
                        serachFinal = serach1.Union(serach2).ToList();
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
            return this.Json(serachFinal, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClientViewByIC(string parentCompanyId)
        {
            logMessage = new StringBuilder();
            try
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagestart, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MethodBase.GetCurrentMethod().Name));
                if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"], CultureInfo.InvariantCulture)))
                {
                    companyId = Convert.ToString(Session["CompanyId"], CultureInfo.InvariantCulture);
                    userId = Convert.ToInt32(Session["UserId"], CultureInfo.InvariantCulture);
                    objUserDashBoard = new UserDashBoard();
                    objDecisionPointEngine = new DecisionPointEngine();
                    #region Background Package Details
                    objBackGroundCheckMasterRequest = new BackGroundCheckMasterRequest() { CreatorCompanyId = parentCompanyId, UserId = userId, CompanyId = companyId, OperationType = 0 };
                    objUserDashBoard.ProfessionalLicense = objDecisionPointEngine.GetProfessionalLicense(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                    objUserDashBoard.ProfessionalInsurance = objDecisionPointEngine.GetProfessionalInsurance(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                    objUserDashBoard.AdditionalRequirements = objDecisionPointEngine.GetAdditionalRequirement(userId, companyId).Where(x => x.CompanyId == parentCompanyId).ToList();
                    objUserDashBoard.ProfessionalCommunications = objDecisionPointEngine.GetProfessionalCommunication(userId, companyId).Where(x => x.CreatorCompanyid == parentCompanyId).ToList();

                    objUserDashBoard.BackgroundList = objDecisionPointEngine.GetBackgroundMapping(objBackGroundCheckMasterRequest).ToList();
                    objUserDashBoard.StateList = objDecisionPointEngine.GetStateList();
                    #endregion
                    objactionresult = View("ClientViewByIC", objUserDashBoard);
                }
                else
                {
                    objactionresult = RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                log.ErrorFormat(DecisionPointR.logmessageerror, ex.ToString(), typeof(UserDashBoardController).Name, MethodBase.GetCurrentMethod().Name);
                objactionresult = RedirectToAction("Error", "Login", new { errorMsg = DecisionPointR.PageErrorMsg });
            }
            finally
            {
                logMessage.AppendLine(string.Format(CultureInfo.InvariantCulture, DecisionPointR.logmessagend, MethodBase.GetCurrentMethod().Name));
                log.Info(logMessage.ToString());
            }
            return objactionresult;
        }
        #endregion

        public ActionResult MockUpPage()
        {
            return View();
        }


    }
}

