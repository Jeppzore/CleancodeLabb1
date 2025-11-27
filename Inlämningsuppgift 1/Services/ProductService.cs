using Inlämningsuppgift_1.Dtos.Products;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories.Products;

namespace Inlämningsuppgift_1.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _repository.GetAll();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto?> GetById(int id)
        {
            var p = await _repository.GetById(id);
            if (p == null) return null;

            return MapToDto(p);
        }

        public async Task<IEnumerable<ProductDto>> Search(string? query, decimal? maxPrice)
        {
            var results = await _repository.Search(query);
            
            if (maxPrice.HasValue)
                results = results.Where(x => x.Price <= maxPrice.Value);

            return results.Select(MapToDto);
        }

        public async Task<ProductDto> Create(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };

            var created = await _repository.Create(product);
            return MapToDto(created);
        }

        public async Task<bool> IncreaseStock(int id, int amount)
        {
            var p = await _repository.GetById(id);
            if (p == null) return false;

            p.Stock += amount;
            await _repository.Update(p);

            return true;
        }

        private static ProductDto MapToDto(Product p) =>
            new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock
            };

        public Task Update(ProductDto product)
        {
            var p = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
            return _repository.Update(p);
        }
        
    }
}
