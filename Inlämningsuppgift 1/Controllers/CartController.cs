using Inlämningsuppgift_1.Dtos.Carts;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inlämningsuppgift_1.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        public CartController(ICartService cartService, IAuthService authService)
        {
            _cartService = cartService;
            _authService = authService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem(
            [FromHeader(Name = "X-Auth-Token")] string token,
            [FromBody] AddCartItemRequest request)
        {
            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null)
                return Unauthorized("Invalid or missing authentication token.");

            if (request.Quantity <= 0)
                return BadRequest("Quantity must be greater than zero.");

            await _cartService.AddItem(userId.Value, request.ProductId, request.Quantity);
            return Ok("Item added to cart.");
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCart(
            [FromHeader(Name = "X-Auth-Token")] string token)
        {
            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null)
                return Unauthorized("Invalid or missing authentication token.");

            return Ok(await _cartService.Getcart(userId.Value));
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItem(
        [FromHeader(Name = "X-Auth-Token")] string token,
        [FromQuery] int productId)
        {
            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null) return Unauthorized();

            await _cartService.RemoveItem(userId.Value, productId);
            return Ok();
        }

        [HttpPost("clear")]
        public async Task<IActionResult> ClearCart(
            [FromHeader(Name = "X-Auth-Token")] string token)
        {
            var userId = await _authService.GetUserIdFromToken(token);
            if (userId == null) return Unauthorized();

            await _cartService.ClearCart(userId.Value);
            return Ok();
        }
    }
        
}
