using BookManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagerAPI.Context;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Books");
    }

    public DbSet<BookModel> Books { get; set; }
}
