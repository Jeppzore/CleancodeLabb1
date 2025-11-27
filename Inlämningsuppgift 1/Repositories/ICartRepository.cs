using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetCart(int userId);
        Task AddToCart(int userId, int productId, int quantity);
        Task RemoveFromCart(int userId, int productId);
        Task ClearCart(int userId);
    }
}
