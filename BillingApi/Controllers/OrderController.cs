using BillingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostOrder")]
        public IActionResult NewOrder([FromBody] OrderRequest order)
        {
            throw new NotImplementedException();
        }
    }
}