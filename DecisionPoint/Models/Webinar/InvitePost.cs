using System.Collections.Generic;

namespace DecisionPoint.Models.Webinar
{
    public class InvitePost
    {
        public List<string> Fname { get; set; }

        public List<string> Lname { get; set; }

        public List<string> Email { get; set; }

        public List<string> WebinarId { get; set; }

        public List<bool> Ischeck { get; set; }
    }
}