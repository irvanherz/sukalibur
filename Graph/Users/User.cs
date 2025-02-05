using NodaTime;
using Sukalibur.Graph.Carts;
using Sukalibur.Graph.Organizers;
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
        Other,
        Unspecified
    }

    [Table("users")]
    public class User
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("username")]
        public required string Username { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public required string Email { get; set; }

        [Column("full_name", TypeName = "varchar(255)")]
        public string FullName { get; set; } = string.Empty;

        [Column("phone", TypeName = "varchar(255)")]
        public string Phone { get; set; } = string.Empty;

        [Column("dob", TypeName = "date")]
        public DateOnly? Dob { get; set; }

        [Column("gender")]
        public UserGender Gender { get; set; } = UserGender.Unspecified;

        [GraphQLIgnore]
        [Column("password", TypeName = "varchar(255)")]
        public string Password { get; set; } = string.Empty;

        [Column("role")]
        public UserRole Role { get; set; } = UserRole.User;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public ICollection<Cart> Carts { get; set; } = [];
        public ICollection<OrganizerMember> OrganizerMembers { get; set; } = [];
    }
}
