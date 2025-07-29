using GetraenkeautomatVorrat.Data;
using GetraenkeautomatVorrat.Services;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSqlite<VorratContext>("Data Source = Vorrat.db");
builder.Services.AddScoped<VorratService>();
//builder.Services.AddHttpClient<RequestProductsService>();
builder.Services.AddControllers();
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

Directory.SetCurrentDirectory(AppContext.BaseDirectory);
builder.WebHost.UseUrls("http://0.0.0.0:9000");

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
