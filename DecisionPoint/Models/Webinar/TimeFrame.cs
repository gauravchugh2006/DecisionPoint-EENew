using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DecisionPoint.Models.Webinar
{
    /// <summary>
    /// Wrapper class used for the deserialization of the times array
    /// found in the JSON response to the call Get Upcoming Webinars
    /// </summary>
    [DataContract]
    public class TimeFrame
    {
        [DataMember(Name = "startTime")]
        public DateTime StartTime { get; set; }

        [DataMember(Name = "endTime")]
        public DateTime EndTime { get; set; }
    }
}