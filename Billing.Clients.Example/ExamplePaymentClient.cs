using Billing.Interfaces;

namespace Billing.Clients.Example
{
    public class ExamplePaymentClient : IPaymentGateway
    {
        private readonly IExamplePaymentConfig _config;

        public ExamplePaymentClient(IExamplePaymentConfig examplePaymentConfig)
        {
            _config = examplePaymentConfig;
        }

        public async Task<bool> RequestPayment(string orderNumber, decimal amount)
        {
            using var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_config.BaseUrl)
            };

            var result = await httpClient.GetAsync("/");

            if (!result.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}
