using Billing.Clients.Example;

namespace Billing.Api.Configs
{
    public class ExamplePaymentConfig : IExamplePaymentConfig
    {
        private readonly IConfiguration _configuration;

        public ExamplePaymentConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl => _configuration.GetValue<string>("ExamplePayment:BaseUrl");
    }
}
