using System.Text.Json;
using MediCare.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediCare.Tests;

public class LoginTests
{
    [Fact]
    public async Task MasterAccount_CanAuthenticateAgainstConfiguredDatabase()
    {
        var appSettingsPath = Path.GetFullPath(Path.Combine(
            AppContext.BaseDirectory,
            "../../../../MediCare.Web/appsettings.json"));
        using var appSettings = JsonDocument.Parse(await File.ReadAllTextAsync(appSettingsPath));
        var connectionString = appSettings.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("DefaultConnection")
            .GetString();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<MediCareDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MediCareDbContext>();

        await using var serviceProvider = services.BuildServiceProvider();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var user = await userManager.FindByEmailAsync("master@medicare.com");

        Assert.NotNull(user);
        Assert.True(await userManager.CheckPasswordAsync(user, "Admin@123!"));
    }
}
