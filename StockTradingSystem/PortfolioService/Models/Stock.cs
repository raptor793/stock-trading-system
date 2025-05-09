namespace PortfolioService.Models
{
    public class Stock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Ticker { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Value => Quantity * CurrentPrice;
        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;
    }
}
