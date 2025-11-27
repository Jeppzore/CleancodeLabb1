namespace Inlämningsuppgift_1.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
