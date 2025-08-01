using GetraenkeautomatVorrat.Data;
using GetraenkeautomatVorrat.Services;
using Serilog;


Directory.SetCurrentDirectory(AppContext.BaseDirectory);

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Logs\\service-log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSqlite<VorratContext>("Data Source = Vorrat.db");
builder.Services.AddScoped<VorratService>();
builder.Services.AddHttpClient<Request>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();
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

builder.WebHost.UseUrls("http://0.0.0.0:9000");

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/Vorrat/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "api/Vorrat";
    c.SwaggerEndpoint("/api/Vorrat/v1/swagger.json", "User API V1");
});


app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
