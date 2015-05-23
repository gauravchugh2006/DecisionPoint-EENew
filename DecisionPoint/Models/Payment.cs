using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DecisionPointBAL.Implementation.Response;
using DecisionPoint.Models;

namespace DecisionPoint.Models
{
    public class  Payment
    {
        #region Global Variables
        private bool defaultvalueforiscoveragearea = true;
        #endregion
        [Required(ErrorMessage = "BusinessName Required")]
        public string BusinessName { get; set; }

        public string TransactionType { get; set; }

        public string AnnualTransaction { get; set; }

        [Required(ErrorMessage = "Select Credit Card Type")]
        public string CreditCardType { get; set; }

       
        [Required(ErrorMessage = "Expiry Month Required")]
        public string ExpiryMonth { get; set; }

        [Required(ErrorMessage = "Expiry Year Required")]
        public string ExpiryYear { get; set; }
        

       // [Required(ErrorMessage = "Card Code Required")]
        public string CardCode { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special characters not allowed")]
        [Required(ErrorMessage = "Card Number Required")]
        public string CardNumber { get; set; }

        [RegularExpression(@"^[A-Za-z0-9- ]+$",ErrorMessage="Special characters not allowed")]
        [Required(ErrorMessage = "Name On Card Required")]
        public string NameOnCard { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Select State")]
        public int? StateId { get; set; }

        [Required(ErrorMessage = "Select City")]
        public int? CityId { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Numbers Only")]  
        public string Zip { get; set; }

        public IList<StateRespone> StateList { get; set; }

        public IList<CityResponse> CityList { get; set; }

        public bool IsSameAddress { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]  
        public string Directions { get; set; }

        [RegularExpression("^[_&.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]  
        public string Street { get; set; }

        [RegularExpression("^[.,A-Za-z0-9- ]+$", ErrorMessage = "Special Characters Not Allowed")]  
        public string StreetNumber { get; set; }
        

        public int NoOfEmployee { get; set; }

        //[DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        public decimal MemberShipAmount { get; set; }

        public int? CompanyId { get; set; }
       
         [Required(ErrorMessage = "No. of Field Staff Required")]     
         public int NoOfFieldStaff { get; set; }

        public decimal FieldStaffFee { get; set; }

         [Required(ErrorMessage = "No. of Office Staff Required")]
        public int NoOfOfficeStaff { get; set; }

        public decimal? OfficeStaffFee { get; set; }

        public int NoOfIc { get; set; }

        public double IcFee { get; set; }

        public double? CompanyFee { get; set; }

        public double? PerFieldStaffFee { get; set; }

        public double? PerOfficeStaffFee { get; set; }

        public bool? IsInvoice { get; set; }

        public string CompanyCode { get; set; }

        public IList<PaymentAmountResponse> PaymentAmountList { get; set; }
        
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Special characters not allowed")]
        public string CVC { get; set; }

        public string RedirectType { get; set; }

        public string PaymentType { get; set; }

        public string ParentUserType { get; set; }

        public int RegPaymentId { get; set; }

        public bool isMonthlyPaymentDone { get; set; }

        public bool isAnnualPaymentDone { get; set; }

        public int ParentUserId { get; set; }
        public bool? ShowCardDiv { get; set; }
        public string  FirstName { get; set; }
        public string MiddelName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<VendorTypeResponse> ICType { get; set; }
        public IList<BackGroundCheckMasterResponse> BackgroundList { get; set; }
        public IEnumerable<LicenseInsuranceResponse> LicAndInsAsDetails { get; set; }
        public string RootUrl { get; set; }
        public string PaymentGetwayActionUrl { get; set; }
        public string PayPalAccount { get; set; }

       
        #region Config Setting
        /// <summary>
        /// get & set IsCoverageAreaApply
        /// </summary>
        public bool IsCoverageAreaApply { get { return defaultvalueforiscoveragearea; } set { defaultvalueforiscoveragearea = value; } }
        #endregion
    }

}