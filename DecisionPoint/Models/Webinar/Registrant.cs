
namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the serialization of a registrant
    /// to be provided as the request body in the Create Registrant call.
    /// </summary>
    public class Registrant
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }
    }
}