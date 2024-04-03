
namespace Persistence
{
  using Domain.Account;
  using Domain.Entities;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;

  public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Message> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<ApplicationUser>(e =>
      {
        e.ToTable("Users");
      });
      builder.Entity<IdentityUserClaim<string>>(e =>
      {
        e.ToTable("UserClaims");
      });
      builder.Entity<IdentityUserLogin<string>>(e =>
      {
        e.ToTable("UserLogins");
      });
      builder.Entity<IdentityUserToken<string>>(e =>
      {
        e.ToTable("UserToken");
      });

      builder.Entity<IdentityRole<string>>(e =>
      {
        e.ToTable("Roles");
      });

      builder.Entity<IdentityRoleClaim<string>>(e =>
      {
        e.ToTable("RoleClaims");
      });
    }
  }
}