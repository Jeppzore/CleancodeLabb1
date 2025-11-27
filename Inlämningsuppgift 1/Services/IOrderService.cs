using Inlämningsuppgift_1.Dtos.Orders;

namespace Inlämningsuppgift_1.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAll();
        Task<OrderDto?> GetById(int id);
        Task<OrderDto> Create(CreateOrderRequest request);
        Task<OrderDto> CreateOrderFromCart(int userId);
    }
}
