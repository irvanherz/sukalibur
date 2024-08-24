using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Auth
{
    public class RegisterUserInput
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
