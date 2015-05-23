using System;

namespace DecisionPointBAL.Implementation.Response
{
    public class CompanyDashBoardResponse
    {
        #region TitleDetails, ServiceDetails & ClientDetails & ContractDetails
        /// <summary>
        /// get & set Service Name
        /// </summary>
        public string serviceName { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string titleName { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string referenceName { get; set; }
        public string groupName { get; set; }
        /// <summary>
        /// Messages Title
        /// </summary>
        public string clientName { get; set; }
        /// <summary>
        /// Messages Send By
        /// </summary>
        public bool? isDeleted { get; set; }
        /// <summary>
        /// Used for display message id
        /// </summary>
        public bool? isActive { get; set; }
        /// <summary>
        /// Used for id of title
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// get & set ParentcompanyId 
        /// </summary>
        public int ParentserviceId { get; set; }
        /// <summary>
        /// get & set ParentcompanyId 
        /// </summary>
        public int ChildserviceId { get; set; }
        /// <summary>
        /// get & set Childservicename 
        /// </summary>
        public string Childservicename { get; set; }

        /// <summary>
        /// Messages Title
        /// </summary>
        public string categoryName { get; set; }

        /// <summary>
        /// Contract Name
        /// </summary>
        public string ContractTypeName { get; set; }
        #endregion
    }



}
