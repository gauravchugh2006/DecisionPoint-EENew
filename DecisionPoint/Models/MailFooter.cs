using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DecisionPoint.Models
{
    public class MailFooter
    {
        public int SignatureId { get; set; }
        public int UserID { get; set; }
        [Required(ErrorMessage="Signature Name required!!")]
        [StringLength(50)]
        public string SignatureName { get; set; }
        [Required(ErrorMessage="Signature required!!")]
        [StringLength(1000)]
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Signauture { get; set; }
    }
}