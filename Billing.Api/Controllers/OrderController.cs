using Billing.Api.Models;
using Billing.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost(Name = "PostOrder")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Receipt))]
        public async Task<IActionResult> NewOrder([FromBody] OrderRequest order)
        {
            return Ok(await _orderService.ProcessOrder(order));
        }
    }
}
