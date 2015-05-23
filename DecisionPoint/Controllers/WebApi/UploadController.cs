using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Threading;
using System.Web.UI;
using System.Globalization;
using System.Configuration;
using System.IO;
using DecisionPointCAL.Common;
using DecisionPoint.Models.DashBoardViewModel.ViewModel;

namespace DecisionPoint.Controllers.WebApi
{
    public class UploadController : ApiController
    {

        // Enable both Get and Post so that our jquery call can send data, and get a status
        [HttpGet]
        [HttpPost]
        public static HttpResponseMessage Upload(string id)
        {
            FileStream output = null;
            try
            {

                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["uploadedcontentpath"], CultureInfo.InvariantCulture) + id;

                // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                // Now we need to wire up a response so that the calling script understands what happened
                HttpContext.Current.Response.ContentType = "text/plain";
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var result = new { name = file.FileName };

                HttpContext.Current.Response.Write(serializer.Serialize(result));
                HttpContext.Current.Response.StatusCode = 200;

                if (File.Exists(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName)))
                {
                    Stream input = file.InputStream;
                    output = new FileStream(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName), FileMode.Append);
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, len);
                    }
                    input.Close();
                    output.Close();

                }
                else
                {
                    bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderDirectory));
                    if (!folderExists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderDirectory));
                    file.SaveAs(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName));
                }
            }
            catch
            {
                throw;
            }
            finally { output.Dispose(); }
            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //Enable both Get and Post so that our jquery call can send data, and get a status
        [HttpGet]
        [HttpPost]
        public static HttpResponseMessage Uploadempdocdoc(string id, string newFileName)
        {
            FileStream output = null;
            try
            {
                string folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["globaluploadedempreqdocpath"], CultureInfo.InvariantCulture) + id;
                if (!string.IsNullOrEmpty(newFileName))
                {
                    if (newFileName.Length > 2)
                    {
                        if (newFileName.Substring(0, 1) == Shared.One && !newFileName.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["specificuploadedempreqdocpath"], CultureInfo.InvariantCulture) + id;
                        }
                        else if (newFileName.Substring(0, 1) == Shared.Two && !newFileName.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["ProfLicuploadeddocpathelec"], CultureInfo.InvariantCulture) + id;
                        }
                        else if (newFileName.Substring(0, 1) == Shared.Three && !newFileName.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["Insuranceuploadeddocpathnonlec"], CultureInfo.InvariantCulture) + id;
                        }
                        else if (newFileName.Substring(0, 1) == "4" && !newFileName.Contains(char.Parse(Shared.DollarSign)))
                        {
                            folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["AdditionalReqUploadedDocPath"], CultureInfo.InvariantCulture) + id;
                        }

                        //else if (newFileName.Contains(char.Parse(Shared.DollarSign)))
                        //{
                        //    ViewModel objViewModel = new ViewModel();
                        //    string parentUserId = objViewModel.GetVisitorUserId(Convert.ToString(HttpContext.Current.Session["CompanyId"], CultureInfo.InvariantCulture));
                        //   if (!string.IsNullOrEmpty(parentUserId))
                        //   {
                        //       folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["BackgroundCheckuploadeddocpath"], CultureInfo.InvariantCulture) + id+Shared.UnderScore +parentUserId;
                        //   }
                        //   else { folderDirectory = Convert.ToString(ConfigurationManager.AppSettings["BackgroundCheckuploadeddocpath"], CultureInfo.InvariantCulture) + id; }
                        //}
                    }
                }
                //string ext = Path.GetExtension(newFileName);
                //string exactname = Path.GetFileName(newFileName);
                if (newFileName.Contains(char.Parse(Shared.DollarSign)))
                {
                    newFileName = newFileName.Split(char.Parse(Shared.DollarSign))[0] + Shared.ExclamationMark + newFileName.Split(char.Parse(Shared.DollarSign))[1].Substring(1, (newFileName.Split(char.Parse(Shared.DollarSign))[1].Length - 1));
                }
                else { newFileName = newFileName.Substring(1, (newFileName.Length - 1)); }

                // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                // Now we need to wire up a response so that the calling script understands what happened
                HttpContext.Current.Response.ContentType = "text/plain";
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var result = new { name = file.FileName };

                HttpContext.Current.Response.Write(serializer.Serialize(result));
                HttpContext.Current.Response.StatusCode = 200;

                if (File.Exists(HttpContext.Current.Server.MapPath(folderDirectory + "/" + newFileName)))
                {
                    Stream input = file.InputStream;
                    output = new FileStream(HttpContext.Current.Server.MapPath(folderDirectory + "/" + newFileName), FileMode.Append);
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, len);
                    }
                    input.Close();
                    output.Close();
                }
                else
                {
                    bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderDirectory));
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderDirectory));
                    }
                    file.SaveAs(HttpContext.Current.Server.MapPath(folderDirectory + "/" + newFileName));
                }
            }
            catch
            {
                throw;
            }
            finally { output.Dispose(); }
            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        // Enable both Get and Post so that our jquery call can send data, and get a status
        /// <summary>
        /// Upload DocBy FolderPath
        /// </summary>
        /// <param name="path">path</param>
        /// <CreatedBy>Sumit Saurav</CreatedBy>
        /// <CreatedDate>21 nov 2014</CreatedDate>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        [HttpPost]
        public static HttpResponseMessage UploadDocByFolderPath(string path)
        {
            FileStream output = null;
            try
            {

                string folderDirectory = path;

                // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                // Now we need to wire up a response so that the calling script understands what happened
                HttpContext.Current.Response.ContentType = "text/plain";
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var result = new { name = file.FileName };

                HttpContext.Current.Response.Write(serializer.Serialize(result));
                HttpContext.Current.Response.StatusCode = 200;

                if (File.Exists(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName)))
                {
                    Stream input = file.InputStream;
                    output = new FileStream(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName), FileMode.Append);
                    byte[] buffer = new byte[8 * 1024];
                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, len);
                    }
                    input.Close();
                    output.Close();

                }
                else
                {
                    bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(folderDirectory));
                    if (!folderExists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderDirectory));
                    file.SaveAs(HttpContext.Current.Server.MapPath(folderDirectory + "/" + file.FileName));
                }
            }
            catch
            {
                throw;
            }
            finally { output.Dispose(); }
            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
