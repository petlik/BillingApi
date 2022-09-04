using Billing.Interfaces;

namespace Billing.Clients.Thrower
{
    public class ThrowerPaymentClient : IPaymentGateway
    {
        public Task<bool> RequestPayment(string orderNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
