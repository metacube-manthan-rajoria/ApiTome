using ApiTome.DatabaseContext;
using Serilog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers()

//Json Formatter
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true; // Enable indented formatting
});

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);

//CorsHeader
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Added Logger Configuration
Log.Logger = 
            new LoggerConfiguration().MinimumLevel.Debug()
            .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "logs/log - .txt", rollingInterval: RollingInterval.Day).CreateLogger();

// Added logger to services
builder.Logging.Services.AddSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAllOrigins");
app.UseHsts();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
