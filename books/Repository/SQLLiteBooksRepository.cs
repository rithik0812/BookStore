namespace books.Repository
{
    using books.Data;
    using books.Models;
    using books.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SQLLiteBooksRepository : IBooksRepository
    {
        private readonly BooksContext _booksContext;
        public SQLLiteBooksRepository(BooksContext booksContext) 
        {
            this._booksContext = booksContext;
        }
        public async Task<Book> AddAsync(Book book)
        {
            // Assign New Guid for Id before adding to DB
            book.Id = Guid.NewGuid();
            await _booksContext.Books.AddAsync(book);
            await _booksContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book> DeleteAsync(Guid Id)
        {
            var existingBook = await _booksContext.Books.FindAsync(Id);

            if (existingBook == null)
            {
                return null;
            }

            _booksContext.Books.Remove(existingBook);
            await _booksContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _booksContext.Books.ToListAsync();
        }

        public async Task<Book> GetAsync(Guid Id)
        {
            var existingBook = await _booksContext.Books.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingBook == null) 
            {
                return null;
            }

            return existingBook;
        }

        public async Task<Book> UpdateAsync(Guid Id, Book book)
        {
            var existingBook = await _booksContext.Books.FindAsync(Id);

            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublicationDate = book.PublicationDate;
                existingBook.Description = book.Description;
                await _booksContext.SaveChangesAsync();
                return existingBook;
            }

            return null;
        }
    }
}
