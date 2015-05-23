using System;
using DecisionPointDAL.Implemention;
using DecisionPointDAL.Implemention.ResponseParam;
using Xamarin.Payments.Stripe;


// ******************************************************************************************************************************
//                                                 class:XamarinStripeCore.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date        |  Created By       | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// may. 24, 2014     |sumit saurav              | This class holds the interaction logic for XamarinStripeCore.cs
// ******************************************************************************************************************************

namespace DecisionPointMailMatixBAL.Request
{
    static class XamarinStripeCore
    {

        /// <summary>
        /// Annual Monthly Payment Charge deduction method
        /// </summary>
        /// <param name="payment">StripePayment</param>
        /// <param name="paymentResponse"><RecurringPaymentResponseParam/param>
        ///  <createdby>Sumit Saurav</createdby>
        /// <createdDate>may/2/2014</createdDate>
        /// <returns>string result</returns>
        public static int AnnualMonthlyPaymentCharge(StripePayment payment, RecurringPaymentResponseParam paymentResponse)
        {
            RecurringPaymentResponseParam objRecurring = null;
            DecisionPointRepository decisionPointRepository = null;
            StripeCharge charge = payment.Charge(Convert.ToInt32(paymentResponse.Amount), "usd", paymentResponse.CustomerId, paymentResponse.Remark);
            string charge_id = charge.ID;
            StripeCharge charge_info = payment.GetCharge(charge_id);
            //save customer details in the database
            objRecurring = new RecurringPaymentResponseParam()
            {
               // UserId = paymentResponse.Id,
                UserId = paymentResponse.UserId,
                CustomerId = paymentResponse.CustomerId,
                Amount = Convert.ToInt32(paymentResponse.Amount),
                Remark = paymentResponse.Remark,
                ChargeId = charge_id,
            };
            decisionPointRepository = new DecisionPointRepository();
            return decisionPointRepository.MakeRecurringPaymentTransaction(objRecurring);
        }

    }
}
