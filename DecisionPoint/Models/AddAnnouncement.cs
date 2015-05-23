
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DecisionPoint.Models
{
    public class AddAnnouncement
    {
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string announcement { get; set; }
        public string status { get; set; }
        public int announcementId { get; set; }
    }
}