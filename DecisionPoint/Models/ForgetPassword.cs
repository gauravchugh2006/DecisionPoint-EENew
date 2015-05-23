using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace DecisionPoint.Models
{
    public class ForgetPassword
    {
        /// <summary>
        /// EmailId of User 
        /// </summary>
        /// 
        [Required(ErrorMessage = "Please enter your Email id")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter valid mail id")]
        public string Emailid { get; set; }
    }
     
}