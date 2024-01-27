using Domain.Account;
using Domain.Enties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
  public static class SeedManager
  {
    public static async Task Seed(IServiceProvider services)
    {
      await SeedRoles(services);

      await SeedAdminUser(services);
    }

    private static async Task SeedRoles(IServiceProvider services)
    {
      var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

      await roleManager.CreateAsync(new IdentityRole(Role.Admin));
      await roleManager.CreateAsync(new IdentityRole(Role.User));
    }

    private static async Task SeedAdminUser(IServiceProvider services)
    {
      var context = services.GetRequiredService<DataContext>();
      var userManager = services.GetRequiredService<UserManager<AppUser>>();
      var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

      var adminUser = await context.Users.FirstOrDefaultAsync(user => user.UserName == "MduduziAdmin");

      if (adminUser is null)
      {
        adminUser = new AppUser { UserName = "MduduziAdmin", Email = "MduduziAdmin@gmail.com" };
        await userManager.CreateAsync(adminUser, "#Systems97");
        await userManager.AddToRoleAsync(adminUser, Role.Admin);
      }
    }
  }
}