using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;

namespace PortfolioService.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            using (PortfolioDbContext dbContext = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>())
            {
                dbContext.Database.Migrate();
            }
        }
    }
}
