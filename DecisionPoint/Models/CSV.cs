
namespace DecisionPoint.Models
{
    public class CSV
    {
        public int No { get; set; }
        public string StaffFName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string StaffLName { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string staffEmail { get; set; }

        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string StaffRole { get; set; }

        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int StaffRoleID { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public string StaffPermission { get; set; }
        /// <summary>
        /// Used for get the staff details
        /// </summary>
        public int StaffPermissionID { get; set; }

        public string StaffTitle { get; set; }

        public int StaffTitleID { get; set; }

        public bool ErrorStatus { get; set; }
        public string CsvFileName { get; set; }
        

        #region paging
        public int pagesize { get; set; }
        public int rowperpage { get; set; }
        #endregion
    }
}