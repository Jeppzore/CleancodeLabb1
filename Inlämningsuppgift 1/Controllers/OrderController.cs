using Inlämningsuppgift_1.Dtos.Orders;
using Inlämningsuppgift_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inlämningsuppgift_1.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public OrderController(IOrderService orderService, IAuthService authService)
        {
            _orderService = orderService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _orderService.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var userId = _authService.GetUserIdFromToken(request.UserId.ToString());
            if (userId == null)
                return Forbid();

            var createdOrder = await _orderService.Create(request);
            return CreatedAtAction(nameof(Get), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPost("createfromcart")]
        public async Task<IActionResult> CreateFromCart(
            [FromHeader(Name = "X-Auth-Token")] string token)
        {
            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null)
                return Unauthorized();

            try
            {
                var order = await _orderService.CreateOrderFromCart(userId.Value);
                return Ok(new { OrderId = order.Id, order.Total });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
