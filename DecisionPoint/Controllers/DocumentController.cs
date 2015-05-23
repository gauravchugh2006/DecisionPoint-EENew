using System;
using System.Web.Mvc;
using DecisionPoint.Models;
using DecisionPointBAL.Implemention.Request;
namespace DecisionPoint.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/
        /// <summary>
        /// add 
        /// </summary>
        /// <returns>view</returns>
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// add details
        /// </summary>
        /// <param name="addDocumentModel">AddDocumentModel class</param>
        /// <returns>view</returns>
        public ActionResult AddDetails(AddDocumentModel addDocumentModel)
        {
            DocumentDetailsRequest documentDetailsRequest = new DocumentDetailsRequest();
            documentDetailsRequest.CreatedBy = 101;
            documentDetailsRequest.CreatedDate = DateTime.Now;
            documentDetailsRequest.DocType = "Document";
            documentDetailsRequest.DueDate = DateTime.Now;
           // documentDetailsRequest.NumberOfTimesReadDoc=addDocumentModel.
            return View();

        }
    }
}
