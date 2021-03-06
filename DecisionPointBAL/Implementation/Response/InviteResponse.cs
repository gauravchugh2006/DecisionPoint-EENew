﻿using System;

namespace DecisionPointBAL.Implementation.Response
{
  public  class InviteResponse
    {
        #region BasicDetail
        /// <summary>
        /// Get & set Date
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Get & set Status
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// Get & set RelationShip
        /// </summary>
        public string RelationShip { get; set; }
        /// <summary>
        /// Get & set CompanyName
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Get & set Contact
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// Get & set EmailId
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// Get & set Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Get & set Phone
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// Get & set UserId
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Get & set TableId
        /// </summary>
        public int TableId { get; set; }
        /// <summary>
        /// Get & set FlowTableId
        /// </summary>
        public int FlowTableId { get; set; }
        /// <summary>
        /// Get & set FlowTableId
        /// </summary>
        public string DocFlow { get; set; }
        /// <summary>
        /// Get & set FlowTableId
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Get & set UserType
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Get & set Isdeleted
        /// </summary>
        public int Isdeleted { get; set; }
        #endregion
    }
}
