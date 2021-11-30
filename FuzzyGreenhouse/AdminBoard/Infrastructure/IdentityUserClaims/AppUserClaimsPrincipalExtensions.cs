using System.Linq;
using System.Security.Claims;

namespace AdminBoard.Infrastructure.IdentityUserClaims
{
    public static class AppUserClaimsPrincipalExtensions
    {
        public static string GetFullNameOrEmail(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == "FirstName");
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == "LastName");
            string fullName = firstName?.Value + " " + lastName?.Value;
            return fullName ?? principal.Identity?.Name;
        }

        public static string GetFullName(this ClaimsPrincipal principal)
        {
            var firstName = principal.Claims.FirstOrDefault(c => c.Type == "FirstName");
            var lastName = principal.Claims.FirstOrDefault(c => c.Type == "LastName");
            return firstName?.Value + " " + lastName?.Value;
        }

        // You can add other extension methods here to access user properties exposed
        // via the ApplicationUserClaimsPrincipalFactory class
    }
}
