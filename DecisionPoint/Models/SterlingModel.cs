using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class SterlingModel
    {
        /// <summary>
        /// get & set FirstName
        /// </summary>
        public string FirstName{get;set;}
        /// <summary>
        /// get & set LastName
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// get & set MiddelName
        /// </summary>
        public string MiddelName { get; set; }
        /// <summary>
        /// get & set EmailAddress
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// get & set PackageId
        /// </summary>
        public int PackageId { get; set; }
        /// <summary>
        /// get & set PackageName
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// get & set State
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// get & set City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// get & set ZipCode
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// get & set AddressLine1
        /// </summary>
        public string AddressLine1 { get; set; }
       
        /// <summary>
        /// get & set PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// get & set User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// get & set CompanyId
        /// </summary>
        public string CompanyId { get; set; }
        public string StateAbbre { get; set; }

        public string SterlingUserName { get; set; }

        public string SterlingPassword { get; set; }
        public string SterlingAccount { get; set; }

        public string UniqueRequestId { get; set; }
        public string BackgroundCheckOrderNumber { get; set; }
        public int OperationType { get; set; }
        public string PayType { get; set; }
        public int PackageTblId { get; set; }
        public string PacakgeSource { get; set; }
        public List<string> ICTypeIds { get; set; }
        public string ICReviewResult { get; set; }

    }
}