using System;
using System.Runtime.Serialization;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the JSON response
    /// to the Get Registrant call.
    /// </summary>
    [DataContract]
    public class ResponseGetRegistrant
    {
        [DataMember(Name = "registrantKey")]
        public string RegistrantKey { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "joinUrl")]
        public string JoinUrl { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "registrationDate")]
        public DateTime RegistrationDate { get; set; }
    }
}