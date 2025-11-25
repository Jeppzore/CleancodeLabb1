using Inlämningsuppgift_1.Dtos.Products;

namespace Inlämningsuppgift_1.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto?> GetById(int id);
        Task<IEnumerable<ProductDto>> Search(string? query, decimal? maxPrice);
        Task<ProductDto> Create(CreateProductRequest request);
        Task<bool> IncreaseStock(int id, int amount);
    }
}
