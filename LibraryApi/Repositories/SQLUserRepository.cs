using LibraryApi.Data;
using LibraryApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories
{
  public class SQLUserRepository : IUserRepository
  {
    private readonly LibraryDbContext _dbContext;

    public SQLUserRepository(LibraryDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
      return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
      await _dbContext.Users.AddAsync(user);
      await _dbContext.SaveChangesAsync();
      return user;
    }

    public async Task<User?> UpdateAsync(Guid id, User user)
    {
      var existingUser = await _dbContext.Users.FindAsync(id);
      if (existingUser == null) return null;

      var emailOtherPeople = await _dbContext.Users
        .AnyAsync(u => u.Email == user.Email && u.Id != id);

      if (emailOtherPeople)
      {
        throw new Exception("Email already exists!");
      }

      existingUser.Name = user.Name;
      existingUser.Email = user.Email;

      await _dbContext.SaveChangesAsync();
      return existingUser;
    }

    public async Task<User?> DeleteAsync(Guid id)
    {
      var existingUser = await _dbContext.Users.FindAsync(id);
      if (existingUser == null) return null;

      await _dbContext.Users
        .Where(u => u.Id == id)
        .ExecuteDeleteAsync();

      return existingUser;
    }
  }
}