using Inlämningsuppgift_1.Dtos.Carts;
using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Services
{
    public interface ICartService
    {
        Task AddItem(int userId, int productId, int quantity);
        Task<CartDto> Getcart(int userId);
        Task RemoveItem(int userId, int productId);
        Task ClearCart(int userId);
    }
}
