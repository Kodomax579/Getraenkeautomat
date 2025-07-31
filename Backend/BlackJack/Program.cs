
using BlackJack.Services;
using Serilog;

namespace BlackJack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("Logs\\service-log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Host.UseSerilog();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<BjService>();
            builder.Services.AddSingleton<DealerService>();
            builder.Services.AddSingleton<PlayerService>();
            builder.WebHost.UseUrls("http://0.0.0.0:9011");
            builder.Host.UseWindowsService();

            var app = builder.Build();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/BlackJack/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "api/BlackJack";
                c.SwaggerEndpoint("/api/BlackJack/v1/swagger.json", "User API V1");
                c.SupportedSubmitMethods();
            });


            app.UseAuthorization();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.MapControllers();

            app.Run();
        }
    }
}
