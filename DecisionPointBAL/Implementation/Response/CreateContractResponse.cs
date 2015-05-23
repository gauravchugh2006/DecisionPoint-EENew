using System;
using System.Collections.Generic;


// ******************************************************************************************************************************
//                                                 class:CreateContractResponse.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// Dec. 04, 2014     |Sumit Saurav       | This class holds the interaction logic for CreateContractResponse.cs
// ******************************************************************************************************************************

namespace DecisionPointBAL.Implementation.Response
{
    public class CreateContractResponse
    {
        #region << Public Variables >>
        #region Contract
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string MName { get; set; }
        /// <summary>
        /// Used for Name of user/company
        /// </summary>
        public string LName { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// get & set UserId
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// get & set Address 1
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// get & set Address 2
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public IEnumerable<string> Services { get; set; }

        /// <summary>
        /// Used for get the list of service details
        /// </summary>
        public IList<string> ServiceDetails { get; set; }


        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string DirectPhone { get; set; }
        /// <summary>
        /// Used for emailid of user/company
        /// </summary>
        public string CompanyName { get; set; }


        /// <summary>
        /// StreetNumber
        /// </summary>  
        public string StreetNumber { get; set; }
        /// <summary>
        /// direction
        /// </summary>       
        public string Direction { get; set; }
        /// <summary>
        /// street name
        /// </summary>       
        public string StreetName { get; set; }
        /// <summary>
        /// get & set City Name
        /// </summary>        
        public string CityName { get; set; }
        /// <summary>
        /// get & set State Name
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// Get & set Zip Code
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// get & set ParentCompanyName
        /// </summary>
        public string ParentCompanyName { get; set; }
        /// <summary>
        /// get & set ParentUserId
        /// </summary>
        public string ParentUserId { get; set; }
        
        public bool SaveClose { get; set; }

        public int StateId { get; set; }

        public string ContactPerson { get; set; }

        public string BusinessName { get; set; }

        public int FlowId { get; set; }

        public string GenPassword { get; set; }

        #endregion
        public int Id { get; set; }

        public string ManagerName { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public int ContractWithId { get; set; }

        public string ContractWithName { get; set; }

        public int ExecutedById { get; set; }

        public string ExecutedByName { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? ContractDate { get; set; }

        public int ExpirationDateReminder { get; set; }

        public string GeneralComments { get; set; }

        public string Paragraph { get; set; }

        public string Section { get; set; }

        public string SubSection { get; set; }

        public string CreatorCompanyId { get; set; }

        public string ContractorCompanyId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsCompleted { get; set; }

        public int CreatedBy { get; set; }

        public string Notes { get; set; }

        public DateTime? EventDate { get; set; }

        public int EventDateReminder { get; set; }

        public IList<int> ServiceList { get; set; }

        // public IList<CommContentRequestParam> DocList { get; set; }

        public List<string> FilePathStr { get; set; }

        public string Role { get; set; }

        public string Title { get; set; }

        public string ServiceName { get; set; }
        public int ServiceId { get; set; }

        public string Status { get; set; }

        public string RefrenceNo { get; set; }

        

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public CreateContractResponse()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
