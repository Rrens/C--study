using LibraryApi.Models.Domain;

namespace LibraryApi.Repositories
{
  public interface IUserRepository
  {
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(User user);
    Task<User?> UpdateAsync(Guid id, User user);
    Task<User?> DeleteAsync(Guid id);
  }
}