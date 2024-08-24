using Sukalibur.Graph.Users;

namespace Sukalibur.Graph.Auth
{
    public class AuthResult
    {
        public User User { get; set; } = null!;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
