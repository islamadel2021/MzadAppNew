using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (userMgr.Users.Any())
        {
            return;
        }

        var muhammad = userMgr.FindByNameAsync("muhammad").Result;
        if (muhammad == null)
        {
            muhammad = new ApplicationUser
            {
                UserName = "muhammad",
                Email = "muhammad@email.com",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(muhammad, "Password1!").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(muhammad, new Claim[]{
                            new(JwtClaimTypes.Name, "Muhammad Awdallah")}).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("muhammad created");
        }
        else
        {
            Log.Debug("muhammad already exists");
        }

        var amir = userMgr.FindByNameAsync("amir").Result;
        if (amir == null)
        {
            amir = new ApplicationUser
            {
                UserName = "amir",
                Email = "amir@email.com",
                EmailConfirmed = true
            };
            var result = userMgr.CreateAsync(amir, "Password1!").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = userMgr.AddClaimsAsync(amir, new Claim[]{
                            new(JwtClaimTypes.Name, "Amir Muhammad")}).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("amir created");
        }
        else
        {
            Log.Debug("amir already exists");
        }
    }
}
