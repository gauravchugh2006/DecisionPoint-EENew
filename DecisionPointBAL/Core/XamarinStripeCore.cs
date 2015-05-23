using System;
using DecisionPointBAL.Implementation.Request;
using DecisionPointDAL.Implemention;
using DecisionPointDAL.Implemention.ResponseParam;
using Xamarin.Payments.Stripe;



namespace DecisionPointBAL.Core
{
    static class XamarinStripeCore
    {
        /// <summary>
        /// get credit card info
        /// </summary>
        /// <param name="paymentResponse">PaymentResponse</param>
        ///  <createdby>Sumit Saurav</createdby>
        /// <createdDate>may/22/2014</createdDate>
        /// <returns>Stripe Credit Card Info</returns>
        static StripeCreditCardInfo GetCC(PaymentResponse paymentResponse)
        {
            StripeCreditCardInfo cc = new StripeCreditCardInfo();
            cc.ExpirationMonth = Convert.ToInt32(paymentResponse.ExpiryMonth);
            cc.ExpirationYear = Convert.ToInt32(paymentResponse.ExpiryYear);
            cc.AddressLine1 = paymentResponse.StreetNumber + ", " + paymentResponse.Directions + ", " + paymentResponse.Street;
            cc.Number = paymentResponse.CardNumber;
            cc.FullName = paymentResponse.NameOnCard;
            cc.ZipCode = paymentResponse.Zip;
            cc.CVC = paymentResponse.CVC;
            return cc;
        }


        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="payment">StripePayment</param>
        /// <param name="paymentResponse">Payment Response</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createdDate>may/22/2014</createdDate>
        /// <returns>string type Customer id.</returns>
        static string CreateCustomer(StripePayment payment, PaymentResponse paymentResponse)
        {
            StripeCustomerInfo customer = new StripeCustomerInfo()
            {
                Email = paymentResponse.CustomerEmail,
                Description = paymentResponse.NameOnCard + "-" + paymentResponse.BusinessName,
                Card = GetCC(paymentResponse),

            };
            StripeCustomer response = payment.CreateCustomer(customer);
            string customer_id = response.ID;
            return customer_id;

        }
        /// <summary>
        /// Decision Point Registration Charge
        /// </summary>
        /// <param name="payment">StripePayment</param>
        /// <param name="paymentResponse">PaymentResponse</param>
        /// <createdby>Sumit Saurav</createdby>
        /// <createdDate>may/22/2014</createdDate>
        /// <returns>string type result.</returns>
        public static string DecisionPointRegistrationCharge(StripePayment payment, PaymentResponse paymentResponse)
        {
            RecurringPaymentResponseParam objRecurring = null;
            DecisionPointRepository decisionPointRepository = null;

            #region Create Customer

            //create customer and set annual plan.
            string AnnualCustomerId = CreateCustomer(payment, paymentResponse);

            //save customer details in the database
            objRecurring = new RecurringPaymentResponseParam()
            {
                UserId = paymentResponse.UserId,
                CustomerId = AnnualCustomerId,
                Amount = Convert.ToInt32(paymentResponse.CompanyFee),
                Remark = "Annual and Monthly Plans",
                Type = "insert"
            };
            decisionPointRepository = new DecisionPointRepository();
            decisionPointRepository.MakeRecurringPayment(objRecurring);
            #endregion


            #region Make Registration payment
            //get credit card details
            StripeCreditCardInfo cc = GetCC(paymentResponse);
            //make first time payment.
            StripeCharge charge = payment.Charge(Convert.ToInt32(paymentResponse.Amount), "usd", cc, paymentResponse.TransactionType);
            string charge_id = charge.ID;
            StripeCharge charge_info = payment.GetCharge(charge_id);
            #endregion

            return charge_id;

        }
        /// <summary>
        /// Annual Monthly Payment Charge deduction method
        /// </summary>
        /// <param name="payment">StripePayment</param>
        /// <param name="paymentResponse"><RecurringPaymentResponseParam/param>
        ///  <createdby>Sumit Saurav</createdby>
        /// <createdDate>may/22/2014</createdDate>
        /// <returns>string result</returns>
        public static string AnnualMonthlyPaymentCharge(StripePayment payment, RecurringPaymentResponseParam paymentResponse)
        {
            StripeCharge charge = payment.Charge(Convert.ToInt32(paymentResponse.Amount), "usd", paymentResponse.CustomerId, paymentResponse.Remark);
            string charge_id = charge.ID;
            StripeCharge charge_info = payment.GetCharge(charge_id);
            string result = string.Empty;
            return result;
        }

        public static int UpdateCustomerDetails(StripePayment payment, PaymentResponse paymentResponse)
        {
            RecurringPaymentResponseParam objRecurring = null;
            DecisionPointRepository decisionPointRepository = null;
            #region Create Customer

            //create customer and set annual plan.
            string AnnualCustomerId = CreateCustomer(payment, paymentResponse);

            //save customer details in the database
            objRecurring = new RecurringPaymentResponseParam()
            {
                UserId = paymentResponse.UserId,
                CustomerId = AnnualCustomerId,
                Amount = Convert.ToInt32(paymentResponse.CompanyFee),
                Remark = "Annual and Monthly Plans",
                Type = "update"
            };
            decisionPointRepository = new DecisionPointRepository();
            return decisionPointRepository.MakeRecurringPayment(objRecurring);
            #endregion
        }

        /// <summary>
        /// Annual Monthly Payment Charge deduction method
        /// </summary>
        /// <param name="payment">StripePayment</param>
        /// <param name="paymentResponse"><RecurringPaymentResponseParam/param>
        ///  <createdby>Sumit Saurav</createdby>
        /// <createdDate>july/19/2014</createdDate>
        /// <returns>string result</returns>
        public static int AnnualMonthlyPaymentFailCharge(StripePayment payment, RecurringPaymentResponseParam paymentResponse)
        {
            RecurringPaymentResponseParam objRecurring = null;
            DecisionPointRepository decisionPointRepository = null;
            StripeCharge charge = payment.Charge(Convert.ToInt32(paymentResponse.Amount), "usd", paymentResponse.CustomerId, paymentResponse.Remark);
            string charge_id = charge.ID;
            StripeCharge charge_info = payment.GetCharge(charge_id);
            //save customer details in the database
            objRecurring = new RecurringPaymentResponseParam()
            {
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
