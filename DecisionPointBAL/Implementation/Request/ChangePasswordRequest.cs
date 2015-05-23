
namespace DecisionPointBAL.Implementation.Request
{
  public class ChangePasswordRequest
    {
        /// <summary>
        /// Password of User 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// user Id
        /// </summary>
        public int UserId { get; set; }
        public string email { get; set; }
        public string OldPassword { get; set; }
    }
}
