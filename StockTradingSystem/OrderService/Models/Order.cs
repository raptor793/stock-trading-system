namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = null!;
        public string Ticker { get; set; } = null!;
        public int Quantity { get; set; }
        public string Side { get; set; } = null!; // "buy" or "sell"
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
