

// ******************************************************************************************************************************
//                                                 class:RecurringPaymentResponseParam.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// May. 27, 2014     |Sumit saurav          | This class holds the interaction logic for RecurringPaymentResponseParam.cs
// ******************************************************************************************************************************

namespace DecisionPointDAL.Implemention.ResponseParam
{
    public class RecurringPaymentResponseParam
    {
        #region << Public Variables >>
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CustomerId { get; set; }
        public int Amount { get; set; }
        public string PlanId { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public string ChargeId { get; set; }
     

        #endregion

        #region << Private Variables >>
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public RecurringPaymentResponseParam()
        {

        }

        #endregion

        #region << Public Methods >>

        #endregion

        #region << Public Methods >>

        #endregion
    }
}
