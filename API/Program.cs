using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.Urls.Clear();
app.Urls.Add("http://localhost:9999");

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    message = "Book API dang chay tai http://localhost:9999",
    endpoints = new[]
    {
        "GET /api/categories",
        "GET /api/books",
        "GET /api/books/search?keyword=abc",
        "POST /api/books",
        "PUT /api/books/{id}",
        "DELETE /api/books/{id}"
    }
}));

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await DbSeeder.SeedAsync(dbContext);
}

app.Run();
