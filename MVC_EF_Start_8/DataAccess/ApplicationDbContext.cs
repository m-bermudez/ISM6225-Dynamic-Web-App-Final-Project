using Microsoft.EntityFrameworkCore;
using MVC_EF_Start_8.Models;

namespace MVC_EF_Start_8.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Quote> Quotes { get; set; }
  }
}