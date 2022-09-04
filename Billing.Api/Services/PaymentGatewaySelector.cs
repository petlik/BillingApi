using Billing.Interfaces;
using Billing.Api.Models;
using Billing.Clients.Example;
using Billing.Clients.Thrower;
using Billing.Api.Exceptions;

namespace Billing.Api.Services
{
    public interface IPaymentGatewaySelector
    {
        IPaymentGateway SelectPaymentGateway(PaymentGatewayEnum paymantGateway);
    }

    public class PaymentGatewaySelector : IPaymentGatewaySelector
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentGatewaySelector(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentGateway SelectPaymentGateway(PaymentGatewayEnum paymantGateway)
        {
            var paymentGateway = _serviceProvider.GetService(GetServiceType(paymantGateway)) as IPaymentGateway;
            if (paymentGateway == null)
                throw new PaymentGatewayException("Unable to create payment gateway client");
            return paymentGateway;
        }

        private Type GetServiceType(PaymentGatewayEnum paymantGateway)
        {
            switch (paymantGateway)
            {
                case PaymentGatewayEnum.Example:
                    return typeof(ExamplePaymentClient);
                case PaymentGatewayEnum.ExceptionThrower:
                    return typeof(ThrowerPaymentClient);
            }
            throw new PaymentGatewayException("Unable to determine payment gateway");
        }
    }
}
