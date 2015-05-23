

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;

namespace DecisionPoint.Models
{
    public class BackGroundCheckMasterModel
    {
        #region << Properties  >>

        public int Id { get; set; }

        [Required(ErrorMessage = "Background Title Required.")]
        public string BackgroundTitle { get; set; }

        [Required(ErrorMessage = "Background Source Required.")]
        public string BackgroundSource { get; set; }

        public bool Status { get; set; }

        public int CreatedBy { get; set; }

        public IList<BackGroundCheckMasterResponse> BackgroundList { get; set; }
        #endregion
    }
}