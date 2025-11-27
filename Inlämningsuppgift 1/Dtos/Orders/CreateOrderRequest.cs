namespace Inlämningsuppgift_1.Dtos.Orders
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
