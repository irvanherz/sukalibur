using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Users
{
    public enum UserRole
    {
        Super,
        Admin,
        User
    }

    public enum UserGender
    {
        Male,
        Female,
        Other
    }

    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("full_name", TypeName = "varchar(255)")]
        public string FullName { get; set; } = string.Empty;

        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Column("phone", TypeName = "varchar(255)")]
        public string Phone { get; set; } = string.Empty;

        [Column("dob", TypeName = "date")]
        public DateOnly? Dob { get; set; } = null;

        [Column("gender")]
        public UserGender Gender { get; set; } = UserGender.Other;

        [GraphQLIgnore]
        [Column("password", TypeName = "varchar(255)")]
        public string Password { get; set; } = string.Empty;

        [Column("role")]
        public UserRole Role { get; set; } = UserRole.User;

        [Column("created_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
