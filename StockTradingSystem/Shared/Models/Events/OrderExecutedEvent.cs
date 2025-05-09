namespace Shared.Models.Events
{
    public class OrderExecutedEvent
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; } = null!;
        public string Ticker { get; set; } = null!;
        public int Quantity { get; set; }
        public string Side { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
