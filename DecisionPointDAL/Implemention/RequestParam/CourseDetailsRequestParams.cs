using System;

namespace DecisionPointDAL.Implemention.RequestParam
{
   public class CourseDetailsRequestParams
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<bool> ReqiuredESign { get; set; }
        public Nullable<bool> ReqiuedForward { get; set; }
        public string PPFile_Loc { get; set; }
        public string PDFFile_Loc { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string NumberOfTimesReadDoc { get; set; }
        public string DocType { get; set; }     
    }
}
