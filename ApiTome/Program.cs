var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
