using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly List<Order> Orders = new()
    {
        new Order
        {
            Id = 1,
            UserId = 1,
            CreatedAt = DateTime.UtcNow,
            Total = 100.00m,
            Items = new List<OrderItem>()
        },
        new Order
        {
            Id = 2,
            UserId = 2,
            CreatedAt = DateTime.UtcNow,
            Total = 150.50m,
            Items = new List<OrderItem>()
        },
    };

        public Task<IEnumerable<Order>> GetAll()
            => Task.FromResult(Orders.AsEnumerable());

        public Task<Order?> GetById(int id)
            => Task.FromResult(Orders.FirstOrDefault(o => o.Id == id));

        public Task<IEnumerable<Order>> GetByUserId(int userId)
            => Task.FromResult(Orders.Where(o => o.UserId == userId).AsEnumerable());

        public Task<Order> Create(Order order)
        {
            order.Id = Orders.Max(o => o.Id) + 1;
            Orders.Add(order);

            return Task.FromResult(order);
        }

        public Task<Order?> Update(Order updated)
        {
            var existing = Orders.FirstOrDefault(o => o.Id == updated.Id);
            if (existing == null)
                return Task.FromResult<Order?>(null);

            existing.UserId = updated.UserId;
            existing.CreatedAt = updated.CreatedAt;
            existing.Total = updated.Total;
            existing.Items = updated.Items;

            return Task.FromResult<Order?>(existing);
        }

        public Task<bool> Delete(int id)
        {
            var existing = Orders.FirstOrDefault(o => o.Id == id);
            if (existing == null)
                return Task.FromResult(false);

            Orders.Remove(existing);

            return Task.FromResult(true);
        }
    }
}
