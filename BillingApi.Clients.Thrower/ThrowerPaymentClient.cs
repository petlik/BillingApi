using BillingApi.Interfaces;

namespace BillingApi.Clients.Thrower
{
    public class ThrowerPaymentClient : IPaymentGateway
    {
        public Task<bool> RequestPayment(string orderNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}