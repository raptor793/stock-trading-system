using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioService.Models;

namespace PortfolioService.Data.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Ticker)
                   .IsRequired();

            builder.Property(s => s.Quantity)
                   .IsRequired();

            builder.Property(s => s.CurrentPrice)
                   .IsRequired()
                   .HasColumnType("numeric");

            builder.HasOne(s => s.Portfolio)
                   .WithMany(p => p.Stocks)
                   .HasForeignKey(s => s.PortfolioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
