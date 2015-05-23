using System;

namespace DecisionPointDAL.Implemention.RequestParam
{
    public class CommunicationBasicDetailsRequestParam
    {

        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public string DocType { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        public string Instruction { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string DocTitle { get; set; }
        /// <summary>
        /// get & set Reference
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// get & set Onstaging
        /// </summary>
        public bool Onstaging { get; set; }
        /// <summary>
        /// get & set OnLib
        /// </summary>
        public bool OnLib { get; set; }
        /// <summary>
        /// get & set Group
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string retake { get; set; }
        /// <summary>
        /// get & set HOC
        /// </summary>
        public string HOC { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public bool RequHirestaff { get; set; }
        /// <summary>
        /// get & set DocTitles
        /// </summary>
        public string DocTitles { get; set; }
        /// <summary>
        /// get & set VideoTitles
        /// </summary>
        public string VideoTitles { get; set; }
        /// <summary>
        /// get & set ScormTitles
        /// </summary>
        public string ScormTitles { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public bool RequHireic { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public bool RequHirevendor { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public DateTime EffectiveDate { get; set; }
        /// <summary>
        /// get & set Daystocomplete
        /// </summary>
        public int DaysToComplete { get; set; }
        /// <summary>
        /// get & set Is Employee ment req
        /// </summary>
        public bool IsEmpReq { get; set; }

    }
}
