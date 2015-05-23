using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the JSON response
    /// to the call Get Upcoming Webinars
    /// </summary>
    [DataContract]
    public class ResponseGetUpcomingWebinar
    {
        [DataMember(Name = "times")]
        public List<TimeFrame> Times { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "inSession")]
        public bool InSession { get; set; }

        [DataMember(Name = "organizerKey")]
        public string OrganizerKey { get; set; }

        [DataMember(Name = "webinarKey")]
        public string WebinarKey { get; set; }

        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "registrationUrl")]
        public string RegistrationUrl { get; set; }
    }
}