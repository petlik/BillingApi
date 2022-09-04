using BillingApi.Interfaces;

namespace BillingApi.Clients.Example
{
    public class ExamplePaymentClient : IPaymentGateway
    {
        public Task<bool> RequestPayment(string orderNumber, decimal amount)
        {
            return Task.FromResult(true);
        }
    }
}