using System.Collections.Generic;

namespace DecisionPoint.Models
{
    public class AddDocumentModel
    {
        public string DocTitle { get; set; }
        public string DocDueDate { get; set; }
        public bool DocRequireFowward { get; set; }
        public bool DocRequireESign { get; set; }
        public string DocPdf { get; set; }
        public string DocPpt { get; set; }        
      
        public string DocVideo { get; set; }
        public bool DocRead { get; set; }
        public bool DocAgreeTerms { get; set; }
        public bool DocIncludeAssement { get; set; }


        public string MsgTitle { get; set; }
        public string MsgReplyDate { get; set; }
        public bool MsgRequireESign { get; set; }
        public string MsgPpt { get; set; }
      
        public string MsgVideo { get; set; }
        public string MsgBody { get; set; }

        public string CourseTitle { get; set; }
        public string CourseDueDate { get; set; }
        public bool CourseRequireFowward { get; set; }
        public bool CourseRequireESign { get; set; }
        public string CoursePdf { get; set; }
        public string CoursePpt { get; set; }
     
        public string CourseVideo { get; set; }
        public bool CourseRead { get; set; }
        public bool CourseAgreeTerms { get; set; }
        public bool CourseIncludeAssement { get; set; }
     

    }
    
}