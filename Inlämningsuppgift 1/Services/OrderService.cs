using Inlämningsuppgift_1.Dtos.Orders;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories.Orders;

namespace Inlämningsuppgift_1.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public OrderService(
            IOrderRepository repository,
            ICartService cartService,
            IProductService productService)
        {
            _repository = repository;
            _cartService = cartService;
            _productService = productService;
        }

        public async Task<OrderDto> Create(CreateOrderRequest request)
        {
            var items = request.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            var total = items.Sum(i => i.Price * i.Quantity);

            var order = new Order
            {
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = total,
                Items = items
            };

            var created = await _repository.Create(order);
            return MapToDto(created);
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var orders = await _repository.GetAll();
            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> GetById(int id)
        {
            var order = await _repository.GetById(id);
            if (order == null)
                return null;

            return MapToDto(order);
        }

        public async Task<OrderDto> CreateOrderFromCart(int userId)
        {
            var cart = await _cartService.Getcart(userId);
            if (!cart.Items.Any())
                throw new InvalidOperationException("Cart is empty");

            var orderItems = new List<OrderItem>();
            decimal total = 0;

            // Validate stock and prepare order items
            foreach (var item in cart.Items)
            {
                var product = await _productService.GetById(item.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");

                if (product.Stock < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for product {product.Name}");

                product.Stock -= item.Quantity;
                await _productService.Update(product);

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Name = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                total += product.Price * item.Quantity;
            }

            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = total,
                Items = orderItems
            };

            var created = await _repository.Create(order);
            await _cartService.ClearCart(userId);

            return MapToDto(created);
        }

        // Helper method to map Order to OrderDto
        private static OrderDto MapToDto(Order order) =>
            new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
    }
}
