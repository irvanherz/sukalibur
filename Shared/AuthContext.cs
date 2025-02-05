using Sukalibur.Graph.Users;
using System.Security.Claims;

namespace Sukalibur.Shared
{
    public class AuthContext
    {
        public bool IsAuthenticated => CurrentUser != null;
        public CurrentUser? CurrentUser { get; }

        public AuthContext(IHttpContextAccessor httpContextAccessor)
        {
            var claimsPrincipal = httpContextAccessor.HttpContext?.User;
            if (claimsPrincipal == null)
                return;
            if (claimsPrincipal.Identity?.IsAuthenticated == true)
            {
                var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(sub))
                {
                    return;
                }
                var role = claimsPrincipal.FindFirstValue(ClaimTypes.Role);
                CurrentUser = new CurrentUser
                {
                    Id = int.Parse(sub),
                    Role = Enum.Parse<UserRole>(role!)
                };
            }
        }

    }

    public class CurrentUser
    {
        public int Id { get; set; }
        public UserRole Role { get; set; }
    }
}
