namespace Inlämningsuppgift_1.Dtos.Orders
{
    public class UpdateOrderRequest
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<object> Items { get; set; } = new List<object>();
    }
}
