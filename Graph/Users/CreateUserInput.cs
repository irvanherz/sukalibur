using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Users
{
    public class CreateUserInput
    {
        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public DateOnly? Dob { get; set; } = null;

        public UserGender Gender { get; set; } = UserGender.Other;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;
    }
}
