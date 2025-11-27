namespace Inlämningsuppgift_1.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
