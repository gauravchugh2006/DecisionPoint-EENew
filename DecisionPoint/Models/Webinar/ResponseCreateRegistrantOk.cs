using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the JSON response
    /// to a successful Create Registrant call.
    /// </summary>
    [DataContract]
    public class ResponseCreateRegistrantOk
    {

        [DataMember(Name = "registrantKey")]
        public string RegistrantKey { get; set; }

        [DataMember(Name = "joinUrl")]
        public string JoinUrl { get; set; }
    }
}