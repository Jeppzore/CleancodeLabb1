using Inlämningsuppgift_1.Dtos.Carts;
using Inlämningsuppgift_1.Dtos.Products;
using Inlämningsuppgift_1.Models;
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
            cartService.Setup(c => c.Getcart(1))
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

        [Fact]
        public async Task CreateOrderFromCart_Throws_WhenStockIsInsufficient()
        {
            // Arrange
            var cartService = new Mock<ICartService>();
            cartService.Setup(c => c.Getcart(1))
                .ReturnsAsync(new CartDto
                {
                    Items = new List<CartItemDto>
                    {
                        new CartItemDto
                        {
                            ProductId = 10,
                            UnitPrice = 100,
                            Quantity = 3
                        }
                    }
                });

            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetById(10))
                .ReturnsAsync(new ProductDto
                {
                    Id = 10,
                    Name = "Expensive Item",
                    Price = 100,
                    Stock = 2 // Insufficient stock
                });

            var orderRepo = new Mock<IOrderRepository>();

            var service = new OrderService(
                orderRepo.Object,
                cartService.Object,
                productService.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                service.CreateOrderFromCart(1));
        }

        [Fact]
        public async Task CreateOrderFromCart_CreatesOrder_WhenDataIsValid()
        {
            // Arrange
            var cartService = new Mock<ICartService>();
            cartService.Setup(c => c.Getcart(1))
                .ReturnsAsync(new CartDto
                {
                    Items = new List<CartItemDto>
                    {
                        new CartItemDto
                        {
                            ProductId = 10,
                            UnitPrice = 50,
                            Quantity = 2
                        }
                    }
                });

            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetById(10))
                .ReturnsAsync(new ProductDto
                {
                    Id = 10,
                    Name = "Test Product",
                    Price = 50,
                    Stock = 5 // Sufficient stock
                });

            productService.Setup(p => p.Update(It.IsAny<ProductDto>()))
                .Returns(Task.CompletedTask);

            var orderRepo = new Mock<IOrderRepository>();
            orderRepo.Setup(r => r.Create(It.IsAny<Order>())
                ).ReturnsAsync((Order o) =>
                {
                    o.Id = 1;
                    return o;
                });

            var service = new OrderService(
                orderRepo.Object,
                cartService.Object,
                productService.Object
            );

            // Act
            var order = await service.CreateOrderFromCart(1);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(1, order.Id);
            Assert.Equal(100, order.TotalPrice); // 2 * 50

            orderRepo.Verify(r => r.Create(It.IsAny<Order>()), Times.Once);
            cartService.Verify(c => c.ClearCart(1), Times.Once);
        }
    }
}
