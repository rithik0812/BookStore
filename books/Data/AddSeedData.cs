using books.Models;
using books.Repository;
using System.Diagnostics;

namespace books.Data
{
    public class AddSeedData
    {
        public static async Task SeedData(BooksContext context)
        {
            if (context.Books.Any()) return;

            var booksSeedData = new List<Book>
            {
                new Book
                {
                    Title = "Book 1",
                    Author = "Author 1",
                    PublicationDate = DateTime.UtcNow.AddMonths(-2),
                    Description = "Book 2 months ago"
                },
                new Book
                {
                    Title = "Book 2",
                    Author = "Author 2",
                    PublicationDate = DateTime.UtcNow.AddMonths(-1),
                    Description = "Book 1 month ago"
                },
                new Book
                {
                    Title = "Book 3",
                    Author = "Author 3",
                    PublicationDate = DateTime.UtcNow.AddMonths(1),
                    Description = "Book 1 month in future"
                },
                new Book
                {
                    Title = "Book 4",
                    Author = "Author 4",
                    PublicationDate = DateTime.UtcNow.AddMonths(2),
                    Description = "Book 2 months in future"
                },
                new Book
                {
                    Title = "Book 5",
                    Author = "Author 5",
                    PublicationDate = DateTime.UtcNow.AddMonths(3),
                    Description = "Book 3 months in future"
                }
            };

            // note: the methods below come from EF core directly not from the Repository folder
            await context.AddRangeAsync(booksSeedData);
            await context.SaveChangesAsync();
        }
    }
}

