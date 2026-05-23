using System.Security.Cryptography;
using LibraryApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data
{
  public class LibraryDbContext : DbContext
  {
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
  }
}