using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public BooksController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
            .Include(x => x.Category)
            .OrderByDescending(x => x.Id)
            .Select(x => new BookResponse
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category != null ? x.Category.Name : string.Empty
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _context.Books
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (book is null)
        {
            return NotFound(new { message = "Khong tim thay sach." });
        }

        return Ok(ToResponse(book));
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? keyword)
    {
        var normalizedKeyword = keyword?.Trim().ToLower() ?? string.Empty;

        var query = _context.Books
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(normalizedKeyword))
        {
            query = query.Where(x =>
                x.Title.ToLower().Contains(normalizedKeyword) ||
                x.Author.ToLower().Contains(normalizedKeyword) ||
                (x.Category != null && x.Category.Name.ToLower().Contains(normalizedKeyword)));
        }

        var books = await query
            .OrderByDescending(x => x.Id)
            .Select(x => new BookResponse
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CategoryId = x.CategoryId,
                CategoryName = x.Category != null ? x.Category.Name : string.Empty
            })
            .ToListAsync();

        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] BookRequest request)
    {
        if (!await _context.Categories.AnyAsync(x => x.Id == request.CategoryId))
        {
            return BadRequest(new { message = "Category khong ton tai." });
        }

        var book = new Book
        {
            Title = request.Title.Trim(),
            Author = request.Author.Trim(),
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryId = request.CategoryId,
            ImageUrl = await SaveImageAsync(request.ImageFile)
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var createdBook = await _context.Books
            .Include(x => x.Category)
            .FirstAsync(x => x.Id == book.Id);

        return Ok(new
        {
            message = "Them sach thanh cong.",
            data = ToResponse(createdBook)
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] BookRequest request)
    {
        var book = await _context.Books.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        if (book is null)
        {
            return NotFound(new { message = "Khong tim thay sach." });
        }

        if (!await _context.Categories.AnyAsync(x => x.Id == request.CategoryId))
        {
            return BadRequest(new { message = "Category khong ton tai." });
        }

        book.Title = request.Title.Trim();
        book.Author = request.Author.Trim();
        book.Price = request.Price;
        book.Quantity = request.Quantity;
        book.CategoryId = request.CategoryId;

        if (request.ImageFile is not null)
        {
            DeleteImage(book.ImageUrl);
            book.ImageUrl = await SaveImageAsync(request.ImageFile);
        }

        await _context.SaveChangesAsync();

        book = await _context.Books.Include(x => x.Category).FirstAsync(x => x.Id == id);

        return Ok(new
        {
            message = "Sua sach thanh cong.",
            data = ToResponse(book)
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book is null)
        {
            return NotFound(new { message = "Khong tim thay sach." });
        }

        DeleteImage(book.ImageUrl);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xoa sach thanh cong." });
    }

    private async Task<string?> SaveImageAsync(IFormFile? imageFile)
    {
        if (imageFile is null || imageFile.Length == 0)
        {
            return null;
        }

        var imageFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot"), "ImageBooks");
        Directory.CreateDirectory(imageFolder);

        var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(imageFile.FileName)}";
        var filePath = Path.Combine(imageFolder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        return $"/ImageBooks/{fileName}";
    }

    private void DeleteImage(string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(imageUrl))
        {
            return;
        }

        var relativePath = imageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
        var fullPath = Path.Combine(_environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot"), relativePath);

        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
        }
    }

    private static BookResponse ToResponse(Book book)
    {
        return new BookResponse
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Price = book.Price,
            Quantity = book.Quantity,
            ImageUrl = book.ImageUrl,
            CategoryId = book.CategoryId,
            CategoryName = book.Category?.Name ?? string.Empty
        };
    }
}
