using Serilog;
using User.Data;
using User.Services;
using UserService.Services;

namespace User
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

            // Add services to the container.
            builder.Services.AddSqlite<UserContext>("Data Source = UserContext.db");
            builder.Host.UseSerilog();
            builder.Services.AddScoped<Service>();
            builder.Services.AddHttpClient<RequestService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            builder.Host.UseWindowsService();
            builder.WebHost.UseUrls("http://0.0.0.0:9002");


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SupportedSubmitMethods();
                });
            }

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
