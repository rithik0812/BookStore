namespace books.Repository.Interfaces
{
    using books.Models;
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetAsync(Guid Id);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Guid Id, Book book);
        Task<Book> DeleteAsync(Guid Id);
    }
}
