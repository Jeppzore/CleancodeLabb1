using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Create(Product product);
        Task<Product?> Update(Product product);
        Task<bool> Delete(int id);
        Task<IEnumerable<Product>> Search(string? query);
    }
}
