using System.Security.Claims;

namespace GoBite.API.Model
{
    public static class ClaimsIdentity
    {
        public static string? GetUserid(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
