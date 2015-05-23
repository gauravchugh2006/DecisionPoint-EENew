using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;


namespace DecisionPoint.Models
{
    /// <summary>
    /// Model for user login 
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// EmailId of User 
        /// </summary>
        /// 
        [Required(ErrorMessage = "Please enter your user id")]
        public string Emailid { get; set; }


        /// <summary>
        /// Password of user for their Decision Point account
        /// </summary>
        /// 
        [Required(ErrorMessage = "Please enter your password ")]
       
        public string Password { get; set; }
        /// <summary>
        /// property for password remember flag 
        /// </summary>
        public Boolean RememberMe { get; set; }
        /// <summary>
        /// property for announcement 
        /// </summary>
        public IList<AnnouncementResponse> Announcement { get; set; }
      
        public string NewPassword { get; set; }
       
        public string ConfirmPassword { get; set; }

        public string SucessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
        public string RedirectType { get; set; }
        public string PaymentType { get; set; }
    }
}