namespace Inlämningsuppgift_1.Dtos.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
