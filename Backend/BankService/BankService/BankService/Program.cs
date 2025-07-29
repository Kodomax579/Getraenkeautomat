
using BankService.Data;
using BankService.Service;
using Serilog;

namespace BankService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs\\service-log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            builder.Host.UseSerilog();
            builder.Services.AddSqlite<BankContext>("Data Source=BankContext.db");
            builder.Services.AddScoped<BankAccountService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Host.UseWindowsService();

            builder.WebHost.UseUrls("http://0.0.0.0:9005");

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SupportedSubmitMethods();
            });

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
