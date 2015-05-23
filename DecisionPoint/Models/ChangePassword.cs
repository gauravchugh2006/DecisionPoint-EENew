using System.ComponentModel.DataAnnotations;

namespace DecisionPoint.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter your New password ")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter your Confirm password ")]
        public string ConfirmPassword { get; set; }
    }
}