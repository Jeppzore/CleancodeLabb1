using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> Products = new()
        {
            new Product { Id = 1, Name = "Pen", Price = 1.5m, Stock = 100 },
            new Product { Id = 2, Name = "Notebook", Price = 3.0m, Stock = 50 },
            new Product { Id = 3, Name = "Mug", Price = 6.0m, Stock = 20 }
        };

        public Task<Product> Create(Product product)
        {
            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);
            return Task.FromResult(product);
        }

        public Task<bool> Delete(int id)
        {
            var existing = Products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult(false);

            Products.Remove(existing);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Product>> GetAll()
            => Task.FromResult(Products.AsEnumerable());

        public Task<Product?> GetById(int id)
            => Task.FromResult(Products.FirstOrDefault(p => p.Id == id));

        public Task<IEnumerable<Product>> Search(string? query)
        {
            query = query?.ToLower() ?? "";
            var results = Products
                .Where(p => p.Name.ToLower().Contains(query))
                .ToList();

            return Task.FromResult(results.AsEnumerable());
        }

        public Task<Product?> Update(Product updated)
        {
            var existing = Products.FirstOrDefault(p => p.Id == updated.Id);
            if (existing == null) return Task.FromResult<Product?>(null);

            existing.Name = updated.Name;
            existing.Price = updated.Price;
            existing.Stock = updated.Stock;

            return Task.FromResult<Product?>(existing);
        }
    }
}
