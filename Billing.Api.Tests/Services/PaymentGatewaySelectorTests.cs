using Billing.Api.Exceptions;
using Billing.Api.Services;
using Billing.Clients.Example;
using Billing.Clients.Thrower;

namespace Billing.Api.Tests.Services
{
    public class PaymentGatewaySelectorTests
    {
        private readonly Mock<IServiceProvider> _serviceProvider;

        public PaymentGatewaySelectorTests()
        {
            _serviceProvider = new Mock<IServiceProvider>();
        }

        private PaymentGatewaySelector BuildSut()
        {
            return new PaymentGatewaySelector(_serviceProvider.Object);
        }

        [Fact]
        public void SelectPaymenyGateway_SuccessfulPaths()
        {
            _serviceProvider
                .Setup(x => x.GetService(It.Is<Type>(x => x == typeof(ExamplePaymentClient))))
                .Returns(new ExamplePaymentClient(null));

            _serviceProvider
                .Setup(x => x.GetService(It.Is<Type>(x => x == typeof(ThrowerPaymentClient))))
                .Returns(new ThrowerPaymentClient());

            var sut = BuildSut();

            var exampleResult = sut.SelectPaymentGateway(Models.PaymentGatewayEnum.Example);
            exampleResult.Should().NotBeNull();
            exampleResult.Should().BeOfType(typeof(ExamplePaymentClient));

            var throwerResult = sut.SelectPaymentGateway(Models.PaymentGatewayEnum.ExceptionThrower);
            throwerResult.Should().NotBeNull();
            throwerResult.Should().BeOfType(typeof(ThrowerPaymentClient));
        }

        [Fact]
        public void SelectPaymenyGateway_UnsuccessfulPaths()
        {
            var sut = BuildSut();

            sut.Invoking(x => x.SelectPaymentGateway(Models.PaymentGatewayEnum.Example))
                .Should().Throw<PaymentGatewayException>()
                .WithMessage("Unable to create payment gateway client");

            sut.Invoking(x => x.SelectPaymentGateway((Models.PaymentGatewayEnum)9))
                .Should().Throw<PaymentGatewayException>()
                .WithMessage("Unable to determine payment gateway");
        }
    }
}
