using Billing.Api.Controllers;
using Billing.Api.Models;
using Billing.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Billing.Api.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<ILogger<OrderController>> _logger;
        private readonly Mock<IOrderService> _orderService;

        public OrderControllerTests()
        {
            _logger = new Mock<ILogger<OrderController>>();
            _orderService = new Mock<IOrderService>();
        }

        private OrderController BuildSut()
        {
            return new OrderController(_logger.Object, _orderService.Object);
        }

        [Theory]
        [AutoData]
        public async Task NewOrder_Success(OrderRequest orderRequest, Receipt receipt)
        {
            _orderService
                .Setup(x => x.ProcessOrder(It.Is<OrderRequest>(x => x == orderRequest)))
                .Returns(Task.FromResult(receipt));

            var sut = BuildSut();

            var actionResult = await sut.NewOrder(orderRequest);

            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType<OkObjectResult>();

            var okResult = actionResult as OkObjectResult;
            okResult.Should().NotBeNull();

            var result = okResult!.Value as Receipt;
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(receipt);
        }
    }
}
