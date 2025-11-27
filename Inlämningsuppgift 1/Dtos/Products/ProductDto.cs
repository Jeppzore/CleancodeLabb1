namespace Inlämningsuppgift_1.Dtos.Products
{
    // expose DTOs, not domain models, to the controller.
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
