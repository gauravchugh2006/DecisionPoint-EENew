using System;

namespace DecisionPointBAL.Implementation.Response
{
    public class UserViewResponse
    {
        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string UserTitle { get; set; }
        public DateTime DueDate { get; set; }
        public string DocType { get; set; }
        public string Status { get; set; }
        public string FileTitle { get; set; }
        public string Filetype { get; set; }
        public string FileLoc { get; set; }
        public int DocID { get; set; }
        public string CourseName{ get; set; }
        public string Reference{ get; set; }
        public string Instruction { get; set; }
        public string DocTitle { get; set; }
        public string VideoTitle { get; set; }
        public string ScormTitle { get; set; }
    }
    
}
