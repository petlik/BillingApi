using Billing.Api.Models;
using Billing.Api.Services;

namespace Billing.Api.Tests.Services
{
    public class RecieptServiceTests
    {
        private RecieptService BuildSut()
        {
            return new RecieptService();
        }

        [Theory]
        [AutoData]
        public void GenerateReceipt(OrderRequest orderRequest)
        {
            var sut = BuildSut();
            var receipt = sut.Generate(orderRequest);

            receipt
                .Should().NotBeNull()
                .And.BeOfType<Receipt>();

            receipt.OrderNumber.Should().Be(orderRequest.OrderNumber);
            receipt.PayableAmount.Should().Be(orderRequest.PayableAmount);
        }
    }
}
