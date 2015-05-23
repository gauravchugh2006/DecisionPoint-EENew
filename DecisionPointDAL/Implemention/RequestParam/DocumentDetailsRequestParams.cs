using System;

namespace DecisionPointDAL.Implemention.RequestParam
{
    public  class DocumentDetailsRequestParams
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> DueDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<bool> ReqiuredESign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<bool> ReqiuedForward { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PPFile_Loc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PDFFile_Loc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> CreatedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<long> CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<long> ModifyBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Nullable<System.DateTime> ModifyDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NumberOfTimesReadDoc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DocType { get; set; }     
    }
}
