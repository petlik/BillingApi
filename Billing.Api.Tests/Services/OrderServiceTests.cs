using Billing.Api.Exceptions;
using Billing.Api.Models;
using Billing.Api.Services;
using Billing.Interfaces;

namespace Billing.Api.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IPaymentGateway> _paymentGateway;
        private readonly Mock<IPaymentGatewaySelector> _paymentGatewaySelector;
        private readonly Mock<IRecieptService> _recieptService;

        public OrderServiceTests()
        {
            _paymentGateway = new Mock<IPaymentGateway>();
            _paymentGatewaySelector = new Mock<IPaymentGatewaySelector>();
            _recieptService = new Mock<IRecieptService>();
        }

        private OrderService BuildSut()
        {
            return new OrderService(_paymentGatewaySelector.Object,
                _recieptService.Object);
        }
        private void SetupPaymentGatewayResponse(OrderRequest orderRequest, bool response)
        {
            _paymentGateway
                .Setup(x => x.RequestPayment(
                    It.Is<string>(x => x.Equals(orderRequest.OrderNumber)),
                    It.Is<decimal>(x => x.Equals(orderRequest.PayableAmount))))
                .Returns(Task.FromResult(response));

            _paymentGatewaySelector
                .Setup(x => x.SelectPaymentGateway(It.Is<PaymentGatewayEnum>(x => x == orderRequest.PaymentGateway)))
                .Returns(_paymentGateway.Object);
        }

        [Theory]
        [AutoData]
        public async Task ProcessOrder_Positive(OrderRequest orderRequest, Receipt receipt)
        {
            SetupPaymentGatewayResponse(orderRequest, true);

            _recieptService
                .Setup(x => x.Generate(It.Is<OrderRequest>(x => x == orderRequest)))
                .Returns(receipt);

            var sut = BuildSut();

            var response = await sut.ProcessOrder(orderRequest);
            response.Should().Be(receipt);
        }

        [Theory]
        [AutoData]
        public async Task ProcessOrder_PaymentError(OrderRequest orderRequest)
        {
            SetupPaymentGatewayResponse(orderRequest, false);

            var sut = BuildSut();

            await sut.Invoking(y => y.ProcessOrder(orderRequest))
                .Should().ThrowAsync<PaymentGatewayException>()
                .WithMessage("Payment gateway failed to process order");
        }
    }
}
