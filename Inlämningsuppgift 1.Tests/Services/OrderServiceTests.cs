using Inlämningsuppgift_1.Dtos.Carts;
using Inlämningsuppgift_1.Repositories.Orders;
using Inlämningsuppgift_1.Services;
using Moq;

namespace Inlämningsuppgift_1.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task CreateOrderFromCart_Throws_WhenCartIsEmpty()
        {
            // Arrange
            var cartService = new Mock<ICartService>();
            cartService.Setup(C => C.Getcart(1))
                .ReturnsAsync(new CartDto { Items = new List<CartItemDto>() });

            var productService = new Mock<IProductService>();
            var orderRepo = new Mock<IOrderRepository>();

            var service = new OrderService(
                orderRepo.Object,
                cartService.Object,
                productService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateOrderFromCart(1));
        }
    }
}
