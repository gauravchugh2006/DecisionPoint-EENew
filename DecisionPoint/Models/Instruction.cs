using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DecisionPoint.Models
{
    public class Instruction
    {
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string GuidInstruction { get; set; }
        public int ID { get; set; }
    }
}