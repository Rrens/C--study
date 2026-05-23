using LibraryApi.Models.Domain;

namespace LibraryApi.Repositories
{
  public interface IBookRepository
  {
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task<Book> CreateAsync(Book book);
    Task<Book?> UpdateAsync(Guid id, Book book);
    Task<Book?> DeleteAsync(Guid id);
  }
}