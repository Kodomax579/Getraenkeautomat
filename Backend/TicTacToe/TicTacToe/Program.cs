using Serilog;
using TicTacToe.Services;

Directory.SetCurrentDirectory(AppContext.BaseDirectory);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs\\service-log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Service>();


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
builder.WebHost.UseUrls("http://0.0.0.0:9008");


var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/TicTacToe/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "api/TicTacToe";
    c.SwaggerEndpoint("/api/TicTacToe/v1/swagger.json", "User API V1");
    c.SupportedSubmitMethods();
});


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
