using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<Order?> GetById(int id);
        Task<IEnumerable<Order>> GetAll();
        Task<Order> Create(Order order);
        Task<IEnumerable<Order>> GetByUserId(int userId);
        Task<Order> Update(Order order);
        Task<bool> Delete(int id);
    }
}
