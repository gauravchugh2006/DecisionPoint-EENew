
namespace DecisionPointDAL.Implemention.ResponseParam
{
    //*****************************************************************************************************************************************
    //   Class used for set properties used in user company section
    //*****************************************************************************************************************************************
   public class CompanyDashBoardResponseParam
    {
        #region TitleDetails, ServiceDetails & ClientDetails & refences & ContractType

        /// <summary>
        /// get & set Service Name
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string titleName { get; set; }
        /// <summary>
        /// get & set Title Name
        /// </summary>
        public string referenceName { get; set; }
        /// <summary>
        /// get & set groupName
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// get & set Client Name
        /// </summary>
        public string clientName { get; set; }
        /// <summary>
        /// get & set Isdeleted
        /// </summary>
        public bool? isDeleted { get; set; }
        /// <summary>
        /// get & set IsActive
        /// </summary>
        public bool? isActive { get; set; }
        /// <summary>
        /// get & set id 
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
        /// get & set Title Name
        /// </summary>
        public string categoryName { get; set; }

       /// <summary>
       /// contract type name
       /// </summary>
        public string ContractTypeName { get; set; }
       
        #endregion
      
    }
   
  
}
