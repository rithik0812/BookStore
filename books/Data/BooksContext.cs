namespace books.Data
{
    using books.Models;
    using Microsoft.EntityFrameworkCore;

    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
