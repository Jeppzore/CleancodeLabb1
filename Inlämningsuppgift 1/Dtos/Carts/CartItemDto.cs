namespace Inlämningsuppgift_1.Dtos.Carts
{
    public class CartItemDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
