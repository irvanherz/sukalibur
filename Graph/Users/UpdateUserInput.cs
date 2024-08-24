namespace Sukalibur.Graph.Users
{
    public class UpdateUserInput
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly? Dob { get; set; }

        public UserGender? Gender { get; set; }

        public string? Password { get; set; }

        public UserRole? Role { get; set; }
    }
}