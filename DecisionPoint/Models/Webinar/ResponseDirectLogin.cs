using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the JSON response
    /// to the direct login call.
    /// </summary>
    [DataContract]
    public class ResponseDirectLogin
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "organizer_key")]
        public string OrganizerKey { get; set; }

        [DataMember(Name = "account_key")]
        public string AccountKey { get; set; }

        [DataMember(Name = "account_type")]
        public string AccountType { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "platform")]
        public string Platform { get; set; }
    }
}