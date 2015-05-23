using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DecisionPointBAL.Implementation.Response;
using DecisionPoint.Models.Webinar;

namespace DecisionPoint.Models
{
    public class WebinarModel
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        [Required(ErrorMessage = "Please enter Email Id")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter valid mail id")]
        public string emailId { get; set; }

        [Required(ErrorMessage = "Please Api Key")]
        public string apiKey { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string password { get; set; }

        public string OrganiserId { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }
    }

    public class WebinarDashboardModel 
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        //[Required(ErrorMessage = "Please enter Email Id")]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter valid mail id")]
        public string emailId { get; set; }

        //[Required(ErrorMessage = "Please webinar Key")]
        public string webinarKey { get; set; }

        public string UpcomingWebinarString { get; set; }
        public IList<WebinarUsersResponse> WebinarUsersList { get; set; }

        public int pagesize { get; set; }

        public int rowperpage { get; set; }

        public int UserId { get; set; }

        public string OrganiserId { get; set; }

        public string AppKey { get; set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ContactName { get; set; }

        public int CreatedBy { get; set; }

        public bool IsActive { get; set; }

        public IList<ResponseGetUpcomingWebinar> UpcomingWebinarList { get; set; }
    }
}