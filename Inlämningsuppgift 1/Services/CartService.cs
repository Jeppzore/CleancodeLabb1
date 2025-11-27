using Inlämningsuppgift_1.Dtos.Carts;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories;

namespace Inlämningsuppgift_1.Services
{
    public class CartService : ICartService
    {

        private readonly ICartRepository _repository;
        private readonly IProductService _productService;

        public CartService(ICartRepository repository, IProductService productService)
        {
            _repository = repository;
            _productService = productService;
        }

        public Task AddItem(int userId, int productId, int quantity)
            => _repository.AddToCart(userId, productId, quantity);

        public async Task<CartDto> Getcart(int userId)
        {
            var items = await _repository.GetCart(userId);
            var dto = new CartDto();

            foreach (var item in items)
            {
                var product = await _productService.GetById(item.ProductId);
                if (product == null) continue; // Skip if product not found

                dto.Items.Add(new CartItemDto
                {
                    ProductID = item.ProductId,
                    ProductName = product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                });
            }
            return dto;
        }

        public Task ClearCart(int userId)
            => _repository.ClearCart(userId);

        public Task RemoveItem(int userId, int productId)
            => _repository.RemoveFromCart(userId, productId);
    }
}
