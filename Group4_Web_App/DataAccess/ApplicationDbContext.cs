using Microsoft.EntityFrameworkCore;
using Group4_Web_App.Models;

namespace Group4_Web_App.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Quote> Quotes { get; set; }
  }
}