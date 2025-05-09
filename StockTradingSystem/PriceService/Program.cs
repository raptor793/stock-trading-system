using MassTransit;
using PriceService.Services;

namespace PriceService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // MassTransit setup
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

            // Background service
            builder.Services.AddHostedService<PriceBackgroundService>();

            var app = builder.Build();

            app.Run();
        }
    }
}
