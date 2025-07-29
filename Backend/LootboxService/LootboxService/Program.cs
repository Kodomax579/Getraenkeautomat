using LootboxService.Services;
using Serilog;

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

builder.Services.AddSingleton<LootBoxService>();


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
builder.WebHost.UseUrls("http://0.0.0.0:9010");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
