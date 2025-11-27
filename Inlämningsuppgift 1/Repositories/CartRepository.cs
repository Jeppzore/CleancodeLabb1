using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories
{
    public class CartRepository : ICartRepository
    {
        private static readonly Dictionary<int, List<CartItem>> Carts = new();

        public Task AddToCart(int userId, CartItem item)
        {
            if (!Carts.ContainsKey(userId))
                Carts[userId] = new List<CartItem>();

            var list = Carts[userId];
            var existing = list.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (existing == null)
                list.Add(item);
            else
                existing.Quantity += item.Quantity;

            return Task.CompletedTask;
        }

        public Task ClearCart(int userId)
        {
            if (Carts.ContainsKey(userId))
                Carts[userId].Clear();

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CartItem>> GetCart(int userId)
        {
            if (!Carts.ContainsKey(userId))
                return Task.FromResult(Enumerable.Empty<CartItem>());

            return Task.FromResult<IEnumerable<CartItem>>(Carts[userId]);
        }

        public Task RemoveFromCart(int userId, int productId)
        {
            if (!Carts.ContainsKey(userId))
                return Task.CompletedTask;

            Carts[userId].RemoveAll(i => i.ProductId == productId);
            return Task.CompletedTask;
        }
    }
}
