using Microsoft.AspNetCore.Mvc;
using MyAppKafka.Application;

namespace MyAppKafka.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _svc;
        public OrdersController(IOrderService svc) => _svc = svc;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrderCommand cmd)
        {
            var id = await _svc.CreateOrderAsync(cmd);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            // example stub — in real app you would query read-model / DB
            return Ok(new { id });
        }
    }
}
