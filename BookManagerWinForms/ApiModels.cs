namespace BookManagerWinForms;

public class CategoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class BookItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class CreateBookResponse
{
    public string Message { get; set; } = string.Empty;
    public BookItem? Data { get; set; }
}
