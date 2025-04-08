using AlpaStock.Core.Context;
using Austistic.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Austistic.Api.Seed
{
    public class Seeder
    {
        public static async Task SeedData(IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<AustisticContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
            }

            if (!dbContext.Roles.Any())
            {
                await dbContext.Database.EnsureCreatedAsync();
                var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                List<string> roles = new() { "SuperAdmin", "Admin", "User" };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }
            await dbContext.SaveChangesAsync();
            if (!dbContext.Users.Any())
            {
                var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var user = new ApplicationUser()
                {
                    Email = "Bsaheed79@gmail.com",
                    EmailConfirmed = true,
                    Country = "NIgeria",
                    PhoneNumber = "08160250471",
                    FirstName = "Saheed",
                    LastName = "Babatunde",
                    UserName = "Psyche"
                };
                await userManager.CreateAsync(user, "String11@");
                await userManager.AddToRoleAsync(user, "SuperAdmin");
            }

        }
    }
}
