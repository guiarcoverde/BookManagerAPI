using BookManagerAPI.Context;
using BookManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController(DataContext context) : ControllerBase
{

    private readonly DataContext _context = context;

    [HttpPost]
    public async Task<ActionResult<BookModel>> Create(BookModel request)
    {

        double generateRandomDouble = (25.0 + new Random().NextDouble() * (100.0 - 25.0) - 25.0);
        request.Price += generateRandomDouble;
        
        Random random = new();
        
        
        request.StockQuantity += random.Next(0, 50);

        _context.Books.Add(request);
        
        await _context.SaveChangesAsync();

        BookModel bookInserted = new()
        {
            Id = request.Id,
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            StockQuantity = request.StockQuantity,

        };

        return CreatedAtAction(nameof(Create), bookInserted);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BookModel>), 200)]
    public IActionResult GetAllBooks()
    {
        var books = _context.Books.ToList();
        
        return Ok(books);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BookModel), 200)]
    public async Task<ActionResult<BookModel>> UpdateBook(int id, UpdateBookModel request)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        book.Price = request.Price;
        book.StockQuantity = request.StockQuantity;

        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BookModel), 200)]
    public async Task<ActionResult<BookModel>> GetBookById(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }
}
