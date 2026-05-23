using LibraryApi.Data;
using LibraryApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories
{
  public class SQLBookRepository : IBookRepository
  {
    private readonly LibraryDbContext _dbContext;

    public SQLBookRepository(LibraryDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
      return await _dbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
      return await _dbContext.Books.FindAsync(id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
      await _dbContext.Books.AddAsync(book);
      await _dbContext.SaveChangesAsync();
      return book;
    }

    public async Task<Book?> UpdateAsync(Guid id, Book book)
    {
      var existingBook = await _dbContext.Books.FindAsync(id);
      if (existingBook == null) return null;

      existingBook.Title = book.Title;
      existingBook.Author = book.Author;
      existingBook.ISBN = book.ISBN;
      existingBook.PublishedYear = book.PublishedYear;
      existingBook.Stock = book.Stock;

      await _dbContext.SaveChangesAsync();
      return existingBook;
    }

    public async Task<Book?> DeleteAsync(Guid id)
    {
      var existingBook = await _dbContext.Books.FindAsync(id);
      if (existingBook == null) return null;

      await _dbContext.Books
        .Where(b => b.Id == id)
        .ExecuteDeleteAsync();

      return existingBook;
    }
  }
}