using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderService.Data;
using OrderService.Extensions;
using OrderService.Repositories;
using OrderService.Services;

namespace OrderService
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
                    Url = "http://localhost:5000/orderservice"
                });
            });

            // PostgreSQL connection
            builder.Services.AddDbContext<OrderDbContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

            // Dependency injection
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddSingleton<IStockPriceService, StockPriceService>();

            // MassTransit/RabbitMQ
            builder.Services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
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
