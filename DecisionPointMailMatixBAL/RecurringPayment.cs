using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using DecisionPointDAL.Implemention;
using DecisionPointDAL.Implemention.ResponseParam;
using DecisionPointMailMatixBAL.Request;
using Xamarin.Payments.Stripe;

// ******************************************************************************************************************************
//                                                 class:RecurringPayment.cs
// ******************************************************************************************************************************
//                                     M O D I F I C A T I O N   M I L E S T O N E S
// ------------------------------------------------------------------------------------------------------------------------------
//  Date             |  Created By          | Description 
// --------+-------------------+-------------------------------------------------------------------------------------------------
// July. 09, 2014     |Sumit Saurav            | This class holds the interaction logic for RecurringPayment.cs
// ******************************************************************************************************************************

namespace DecisionPointMailMatixBAL
{
    /// <summary>
    /// for recurring payment
    /// </summary>
    public class RecurringPayment
    {
        #region << Public Variables >>

        #endregion

        #region << Private Variables >>
        private static string remarkAnnualPlan = "Annual Plan";
        private static string remarkMonthlyPlan = "Monthly Plan";
        #endregion

        #region << Properties  >>

        #endregion

        #region << Constructors  >>

        /// <summary>
        /// The default Constructor.
        /// </summary>
        public RecurringPayment()
        {

        }

        #endregion

        #region << Public Methods >>

        /// <summary>
        /// make company monthly payment means recurring payments
        /// </summary>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>09/july/2014</createddate>
        public static void MakeCompanyRecurringMonthlyPayment()
        {
            try
            {
                #region Valiables
                DecisionPointRepository objDecisionPointRepository = null;
                RecurringPaymentResponseParam planDetails = null;
                objDecisionPointRepository = new DecisionPointRepository();
                IList<int> lstStaff = new List<int>();
                IList<int> lstBusinessPartner = new List<int>();
                IList<int> lstWePayIc = new List<int>();
                IList<CompanyIdResponseParam> lstCompany = objDecisionPointRepository.getAllPayeeCompanyId();
                IList<PaymentAmountResponseParam> PaymentAmount = null;
                int amount = 0;
                #endregion
                if (lstCompany != null && lstCompany.Count > 0)
                {
                    foreach (var item in lstCompany)
                    {
                        try
                        {
                            #region Company Payment Calculation

                            // get payment amount of a compnay inclusing staff, business, IC rates
                            PaymentAmount = objDecisionPointRepository.getPaymentAmount(item.CompanyId).ToList();

                            planDetails = new RecurringPaymentResponseParam();
                            //get recurring payment customer details
                            planDetails = objDecisionPointRepository.getPlanDetails(Convert.ToInt32(item.Id));
                            if (planDetails != null)
                            {
                                int IsPaymentDone = objDecisionPointRepository.IsRecurringPaymentDone(planDetails.CustomerId, remarkMonthlyPlan);
                                if (IsPaymentDone == 0)
                                {
                                    //check wether is invoice is false or not??
                                    if (!Convert.ToBoolean(PaymentAmount[0].IsInvoice))
                                    {
                                        // code for staff calculation and deduction
                                        lstStaff = objDecisionPointRepository.GetAllStaff(item.CompanyId);

                                        // code for business partner calculation and deduction
                                        lstBusinessPartner = objDecisionPointRepository.GetAllBusinessPartners(item.CompanyId);

                                        // code for we pay IC calculation and deduction
                                        lstWePayIc = objDecisionPointRepository.GetAllWePayIc(item.CompanyId);


                                        if (PaymentAmount != null && PaymentAmount.Count > 0)
                                        {
                                            decimal staffamount = Convert.ToDecimal(lstStaff.Count()) * Convert.ToDecimal(PaymentAmount[0].PerFieldStaffFee);
                                            decimal businessAmount = Convert.ToDecimal(lstBusinessPartner.Count()) * Convert.ToDecimal(PaymentAmount[0].PerOfficeStaffFee);
                                            decimal IcAmount = Convert.ToDecimal(lstWePayIc.Count()) * Convert.ToDecimal(PaymentAmount[0].PerIcFee);
                                            amount = Convert.ToInt32(((staffamount + businessAmount + IcAmount) * 100));
                                        }
                                        planDetails.Amount = amount;
                                        planDetails.Remark = remarkMonthlyPlan;
                                        planDetails.UserId = item.Id;
                                        //update plan in stripe.

                                        StripePayment payment = new StripePayment(Convert.ToString(ConfigurationManager.AppSettings["StripeKey"]));
                                        XamarinStripeCore.AnnualMonthlyPaymentCharge(payment, planDetails);
                                    }
                                }
                            }
                            #endregion
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        /// <summary>
        /// make They Pay IC recuring payment monthly
        /// </summary>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>09/july/2014</createddate>
        public static void MakeTheyPayIcRecurringMontthlyPayment()
        {
            try
            {
                #region Valiables
                DecisionPointRepository objDecisionPointRepository = null;
                RecurringPaymentResponseParam planDetails = null;
                objDecisionPointRepository = new DecisionPointRepository();
                IList<CompanyIdResponseParam> lstTheyPayIc = objDecisionPointRepository.getAllPayeeIcCompanyId();
                IList<PaymentAmountResponseParam> PaymentAmount = null;
                int amount = 0;
                #endregion

                if (lstTheyPayIc != null && lstTheyPayIc.Count > 0)
                {
                    foreach (var item in lstTheyPayIc)
                    {
                        try
                        {
                            #region Ic Payment Calculation & Deductions

                            // get payment amount of an They Pay Ic. 
                            PaymentAmount = objDecisionPointRepository.getPaymentAmount(item.CompanyId).ToList();

                            planDetails = new RecurringPaymentResponseParam();
                            //get recurring payment customer details
                            planDetails = objDecisionPointRepository.getPlanDetails(Convert.ToInt32(item.Id));
                            if (planDetails != null)
                            {
                                int IsPaymentDone = objDecisionPointRepository.IsRecurringPaymentDone(planDetails.CustomerId, remarkMonthlyPlan);
                                // check is payment already done for this month or not??
                                if (IsPaymentDone == 0)
                                {
                                    //check wether is invoice is false or not??
                                    if (!Convert.ToBoolean(PaymentAmount[0].IsInvoice))
                                    {
                                        if (PaymentAmount != null && PaymentAmount.Count > 0)
                                        {
                                            decimal IcAmount = Convert.ToDecimal(lstTheyPayIc.Count()) * Convert.ToDecimal(PaymentAmount[0].PerIcFee);
                                            amount = Convert.ToInt32((IcAmount * 100));
                                        }

                                        planDetails.Amount = amount;
                                        planDetails.Remark = remarkMonthlyPlan;
                                        planDetails.UserId = item.Id;
                                        //update plan in stripe.                        
                                        StripePayment payment = new StripePayment(Convert.ToString(ConfigurationManager.AppSettings["StripeKey"]));
                                        XamarinStripeCore.AnnualMonthlyPaymentCharge(payment, planDetails);
                                    }
                                }
                            }
                            #endregion
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// make company monthly payment means recurring payment
        /// </summary>
        /// <createdby>Sumit Saurav</createdby>
        /// <createddate>09/july/2014</createddate>
        public static void MakeCompanyRecurringAnnualPayment()
        {
            try
            {
                #region Valiables
                DecisionPointRepository objDecisionPointRepository = null;
                RecurringPaymentResponseParam planDetails = null;
                objDecisionPointRepository = new DecisionPointRepository();
                IList<CompanyIdResponseParam> lstCompany = objDecisionPointRepository.getAllAnnualPaymentCompanyId();
                IList<PaymentAmountResponseParam> PaymentAmount = null;
                int amount = 0;
                #endregion
                if (lstCompany != null && lstCompany.Count > 0)
                {
                    foreach (var item in lstCompany)
                    {
                        try
                        {

                            #region Company Annual Payment Calculation & Deductions

                            // get payment amount of a compnay 
                            PaymentAmount = objDecisionPointRepository.getPaymentAmount(item.CompanyId).ToList();

                            planDetails = new RecurringPaymentResponseParam();
                            planDetails = objDecisionPointRepository.getPlanDetails(Convert.ToInt32(item.Id));
                            if (planDetails != null)
                            {
                                int IsPaymentDone = objDecisionPointRepository.IsRecurringPaymentDone(planDetails.CustomerId, remarkAnnualPlan);
                                // check is payment already done for this year or not??
                                if (IsPaymentDone == 0)
                                {
                                    //check wether is invoice is false or not??
                                    if (!Convert.ToBoolean(PaymentAmount[0].IsInvoice))
                                    {
                                        if (PaymentAmount != null && PaymentAmount.Count > 0)
                                        {
                                            decimal annualAmount = Convert.ToDecimal(PaymentAmount[0].CompanyFee);

                                            amount = Convert.ToInt32((annualAmount * 100));
                                        }

                                        planDetails.Amount = amount;
                                        planDetails.Remark = remarkAnnualPlan;
                                        planDetails.UserId = item.Id;
                                        //update plan in stripe.
                                        StripePayment payment = new StripePayment(Convert.ToString(ConfigurationManager.AppSettings["StripeKey"]));
                                        XamarinStripeCore.AnnualMonthlyPaymentCharge(payment, planDetails);
                                    }
                                }
                            }
                            #endregion
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }
        #endregion


    }
}
