using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PortfolioService.Data;
using PortfolioService.Extensions;
using PortfolioService.Repositories;
using PortfolioService.Services.Consumers;

namespace PortfolioService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                // Force Swagger to generate URL through the proxy
                config.AddServer(new OpenApiServer
                {
                    Url = "http://localhost:5000/portfolioservice"
                });
            });

            // PostgreSQL connection
            builder.Services.AddDbContext<PortfolioDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            // Dependency injection
            builder.Services.AddScoped<IStocksRepository, StocksRepository>();
            builder.Services.AddScoped<IPortfolioStocksRepository, PortfolioStocksRepository>();

            // MassTransit/RabbitMQ
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderExecutedConsumer>();
                config.AddConsumer<StockPriceUpdatedConsumer>();

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("portfolio-order", e =>
                    {
                        e.ConfigureConsumer<OrderExecutedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("portfolio-price", e =>
                    {
                        e.ConfigureConsumer<StockPriceUpdatedConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyMigrations(); //Apply migrrations to database
            }
            
            app.MapControllers();

            app.Run();
        }
    }
}
