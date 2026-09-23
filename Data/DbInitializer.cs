using GiftOfTheGiversPrototype.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversPrototype.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await context.Database.EnsureCreatedAsync();

        string[] roles = { "Employee", "Donor" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        var employee = await userManager.FindByEmailAsync("employee@giftgivers.local");
        if (employee == null)
        {
            employee = new ApplicationUser
            {
                UserName = "employee@giftgivers.local",
                Email = "employee@giftgivers.local",
                EmailConfirmed = true,
                FullName = "Relief Employee"
            };

            var result = await userManager.CreateAsync(employee, "Employee123");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(employee, "Employee");
        }

        var donor = await userManager.FindByEmailAsync("donor@giftgivers.local");
        if (donor == null)
        {
            donor = new ApplicationUser
            {
                UserName = "donor@giftgivers.local",
                Email = "donor@giftgivers.local",
                EmailConfirmed = true,
                FullName = "Demo Donor"
            };

            var result = await userManager.CreateAsync(donor, "Donor123");
            if (result.Succeeded)
                await userManager.AddToRoleAsync(donor, "Donor");
        }

        if (!await context.ReliefProjects.AnyAsync())
        {
            context.ReliefProjects.AddRange(
                new ReliefProject
                {
                    Name = "Emergency Food Relief",
                    Location = "Gauteng",
                    Status = "Ongoing",
                    Description = "Distribution of food parcels to families affected by emergencies."
                },
                new ReliefProject
                {
                    Name = "Water & Hygiene Support",
                    Location = "Limpopo",
                    Status = "Ongoing",
                    Description = "Providing clean water and hygiene supplies to communities."
                },
                new ReliefProject
                {
                    Name = "Community Medical Support",
                    Location = "KwaZulu-Natal",
                    Status = "Planning",
                    Description = "Supporting communities with essential medical supplies."
                }
            );

            await context.SaveChangesAsync();
        }
    }
}
