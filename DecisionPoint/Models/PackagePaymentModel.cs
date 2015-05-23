using DecisionPointBAL.Implementation.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DecisionPoint.Models
{
    public class PackagePaymentModel
    {
        /// <summary>
        /// get & set BackgroundList
        /// </summary>
        public IList<BackGroundCheckMasterResponse> BackgroundPkgList { get; set; }
        public IList<BackGroundCheckMasterResponse> BackgroundList { get; set; }
        public IList<BackGroundCheckMasterResponse> RequirementSetByEntity { get; set; }
        
        public IEnumerable<StateRespone> StateList { get; set; }
        public int StateId { get; set; }
        public string StateAbbre { get; set; }
        public string PackageIds { get; set; }
        public string RedirectType { get; set; }
        public string PayType { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsPayment { get; set; }
        public float PackageAmount { get; set; }
        public string RootUrl { get; set; }
        public string InviteeCompanyName { get; set; }
        public string PaymentGetwayActionUrl { get; set; }
        public string PayPalAccount { get; set; }
        public bool IsBasicFree { get; set; }
      
    }
}