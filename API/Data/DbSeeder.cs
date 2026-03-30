using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Categories.AnyAsync())
        {
            context.Categories.AddRange(
                new Category { Name = "Lap trinh" },
                new Category { Name = "Kinh te" },
                new Category { Name = "Van hoc" });

            await context.SaveChangesAsync();
        }

        if (!await context.Books.AnyAsync())
        {
            var categories = await context.Categories.OrderBy(x => x.Id).ToListAsync();

            context.Books.AddRange(
                new Book
                {
                    Title = "ASP.NET Core Can Ban",
                    Author = "Nguyen Van A",
                    Price = 120000,
                    Quantity = 10,
                    CategoryId = categories[0].Id
                },
                new Book
                {
                    Title = "SQL Server Thuc Hanh",
                    Author = "Tran Thi B",
                    Price = 135000,
                    Quantity = 8,
                    CategoryId = categories[0].Id
                },
                new Book
                {
                    Title = "Kinh Te Vi Mo",
                    Author = "Le Van C",
                    Price = 98000,
                    Quantity = 6,
                    CategoryId = categories[1].Id
                });

            await context.SaveChangesAsync();
        }
    }
}
