namespace BookManagerAPI.Models;

public class BookModel
{
    public int Id { get; set; }
    public string Title {  get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public double Price { get; set; }
    public int StockQuantity { get; set; }
}
