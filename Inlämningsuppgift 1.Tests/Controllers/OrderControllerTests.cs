using Inlämningsuppgift_1.Controllers;
using Inlämningsuppgift_1.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Inlämningsuppgift_1.Tests.Controllers
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task CreateFromCart_ReturnsUnauthorized_WhenTokenInvalid()
        {
            // Arrange
            var orderService = new Mock<IOrderService>();
            var authService = new Mock<IAuthService>();

            authService.Setup(a => a.GetUserIdFromToken("bad-token"))
                .ReturnsAsync((int?)null);

            var controller = new OrderController(orderService.Object, authService.Object);

            // Act
            var result = await controller.CreateFromCart("bad-token");

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
