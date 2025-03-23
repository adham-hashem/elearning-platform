using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DB
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                }
            }

            // Create initial admin user
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                FullName = "Admin User"
            };
            string adminPassword = "Admin@123";
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("Initial admin created: admin/Admin@123");
                }
                else
                {
                    Console.WriteLine("Failed to create initial admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
