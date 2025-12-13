using Inlämningsuppgift_1.Repositories.Carts;
using Inlämningsuppgift_1.Services;
using Moq;

namespace Inlämningsuppgift_1.Tests.Services
{
    public class CartServiceTests
    {
        [Fact]
        public async Task AddItem_AddsQuantity_WhenProductAlreadyExists()
        {
            // Arrange (FAKE)
            var cartRepo = new CartRepository();
            var productService = new Mock<IProductService>().Object;

            var service = new CartService(cartRepo, productService);

            // Act

            await service.AddItem(1, productId: 10, quantity: 2);
            await service.AddItem(1, productId: 10, quantity: 3);

            var cart = await service.Getcart(1);

            // Assert
            Assert.Single(cart.Items);
            Assert.Equal(5, cart.Items[0].Quantity);
        }

        [Fact]
        public async Task ClearCart_RemovesAllItems()
        {
            // Arrange
            var cartRepo = new CartRepository();
            var productService = new Mock<IProductService>().Object;
            var service = new CartService(cartRepo, productService);

            await service.AddItem(1, 10, 2);

            // Act
            await service.ClearCart(1);
            var cart = await service.Getcart(1);

            // Assert
            Assert.Empty(cart.Items);
        }
    }
}
