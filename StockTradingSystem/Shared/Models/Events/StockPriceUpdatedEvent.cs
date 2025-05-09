namespace Shared.Models.Events
{
    public class StockPriceUpdatedEvent
    {
        public string Ticker { get; set; } = null!;
        public decimal NewPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
