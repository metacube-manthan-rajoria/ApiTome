using ApiTome.DatabaseContext;
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
