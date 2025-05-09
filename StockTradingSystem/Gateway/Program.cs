namespace Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // YARP
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("YarpReverseProxy"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.MapControllers();
            app.MapReverseProxy();

            app.Run();
        }
    }
}
