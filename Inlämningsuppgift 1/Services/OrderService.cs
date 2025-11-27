using Inlämningsuppgift_1.Dtos.Orders;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories;

namespace Inlämningsuppgift_1.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
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
                Total = total,
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

        // Helper method to map Order to OrderDto
        private static OrderDto MapToDto(Order order) =>
            new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Total = order.Total,
                Items = order.Items.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
    }
}
