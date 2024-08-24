using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Auth
{
    public class LoginUserInput
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
