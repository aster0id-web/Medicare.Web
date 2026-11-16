using Microsoft.AspNetCore.Identity;

namespace MediCare.Web;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // 1. Ensure roles exist
        string[] roles = { "Admin", "Doctor", "Patient" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // 2. Promote your existing working account
        // Note: If you prefer your kamel@gmail.com account, just change the email below!
        // 2. Promote your existing working account and reset password
        var myAccount = await userManager.FindByEmailAsync("elma3allemfacw@gmail.com");
        if (myAccount != null)
        {
            // Grant Admin access
            if (!await userManager.IsInRoleAsync(myAccount, "Admin"))
            {
                await userManager.AddToRoleAsync(myAccount, "Admin");
            }

            // Force a password reset bypassing email verification
            await userManager.RemovePasswordAsync(myAccount);
            await userManager.AddPasswordAsync(myAccount, "Admin@123!");
        }
    }
}