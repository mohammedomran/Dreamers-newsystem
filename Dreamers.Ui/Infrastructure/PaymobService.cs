using X.Paymob.CashIn.Models.Orders;
using X.Paymob.CashIn.Models.Payment;
using X.Paymob.CashIn;
using Microsoft.Extensions.Options;

namespace Dreamers.Ui.Infrastructure
{
    public class PaymobService
    {

        private readonly int integrationId;
        private readonly IOptions<PaymobConfiguration> paymobOptions;
        private readonly IPaymobCashInBroker _broker;

        public PaymobService(IPaymobCashInBroker broker,
            int integrationId,
            IOptions<PaymobConfiguration> paymobOptions)
        {
            _broker = broker;
            this.integrationId = integrationId;
            this.paymobOptions = paymobOptions;
        }


        public async Task<string> RequestCardPaymentKey(string merchantOrderId, decimal amount, CashInBillingData billingInfo)
        {
            try
            {

                int amountCents = (int)Math.Ceiling(amount * 100); // 10 LE
                var orderRequest = CashInCreateOrderRequest.CreateOrder(amountCents, "EGP", merchantOrderId);

                var orderResponse = await _broker.CreateOrderAsync(orderRequest);



                // Request card payment key.
                var paymentKeyRequest = new CashInPaymentKeyRequest(
                    integrationId: integrationId,
                    orderId: orderResponse.Id,
                    billingData: billingInfo,
                    amountCents: amountCents);

                var paymentKeyResponse = await _broker.RequestPaymentKeyAsync(paymentKeyRequest);

                // Create iframe src.
                return $"https://accept.paymob.com/api/acceptance/iframes/323300?payment_token={paymentKeyResponse.PaymentKey}";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetPaymentIFrameSrc(string merchantOrderId, string firstName, string lastName, string Phone, string Email, decimal totalCost)
        {
            var billingInfo = new CashInBillingData(
                            firstName: firstName,
                            lastName: lastName,
                            phoneNumber: Phone,
                            email: Email);
            var egpAmount = totalCost;

            return RequestCardPaymentKey(merchantOrderId, egpAmount, billingInfo).Result;
        }


    }
}
