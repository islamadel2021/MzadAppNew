using System.Security.Claims;

namespace MzadService.UnitTests;

public class Helper
{
    public static ClaimsPrincipal GetClaimsPrincipal()
    {
        var claims = new List<Claim>{
            new (ClaimTypes.Name, "test")
        };
        var identity = new ClaimsIdentity(claims, "Testing");
        return new ClaimsPrincipal(identity);
    }
}
