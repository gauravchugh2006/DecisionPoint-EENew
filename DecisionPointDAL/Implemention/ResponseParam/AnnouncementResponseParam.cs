using System;

namespace DecisionPointDAL.Implemention.ResponseParam
{

    #region Public Variables
    /// <summary>
    /// Class to define public properties for login response param.
    /// </summary>
    public class AnnouncementResponseParam
    {
        /// <summary>
        /// Id of Announcement 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Announcement Message
        /// </summary>
        public string Announcement { get; set; }
        /// <summary>
        ///  userId
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        ///  userId
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        ///  userId
        /// </summary>
        public bool IsClose { get; set; }
        /// <summary>
        ///  get & set ReleaseDate
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
    }
    #endregion

}
