using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the JSON response
    /// to an unsuccessful Create Registrant call (e.g. when the registrant
    /// already exists).
    /// </summary>
    [DataContract]
    public class ResponseCreateRegistrantDuplicate
    {
        [DataMember(Name = "registrantKey")]
        public string RegistrantKey { get; set; }

        [DataMember(Name = "joinUrl")]
        public string JoinUrl { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "incident")]
        public string Incident { get; set; }
    }
}