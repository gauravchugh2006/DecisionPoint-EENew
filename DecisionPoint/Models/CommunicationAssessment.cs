
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DecisionPoint.Models
{
    public class CommunicationAssessment
    {

        /// <summary>
        /// get & set PassingScore
        /// </summary>
        public int docId { get; set; }
        /// <summary>
        /// get & set Instruction
        /// </summary>
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string question { get; set; }
        /// <summary>
        /// get & set Attempts
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// get & set Reference
        /// </summary>
        public string savestatus { get; set; }
        /// <summary>
        /// get & set assessmentid
        /// </summary>
        public int assessmentid { get; set; }


    }
}