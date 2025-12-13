using Inlämningsuppgift_1.Dtos.Products;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories.Products;
using Inlämningsuppgift_1.Services;
using Moq;

namespace Inlämningsuppgift_1.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetById_ReturnsProduct_WhenProductExists()
        {
            // Arrange (STUB)
            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.GetById(1))
                .ReturnsAsync(new Product { Id = 1, Name = "Pen", Price = 1.5m });

            var service = new ProductService(repo.Object);

            // Act
            var result = await service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pen", result.Name);
        }

        [Fact]
        public async Task Create_CallsRepositoryCreate()
        {
            // Arrange (MOCK)
            var repo = new Mock<IProductRepository>();
            repo.Setup(r => r.Create(It.IsAny<Product>()))
                .ReturnsAsync(new Product { Id = 1 });

            var service = new ProductService(repo.Object);

            // Act
            await service.Create(new CreateProductRequest
            {
                Name = "Notebook",
                Price = 3.0m,
                Stock = 10
            });

            // Assert
            repo.Verify(r => r.Create(It.IsAny<Product>()), Times.Once);
        }
    }
}
