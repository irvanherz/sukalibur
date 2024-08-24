using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Organizers
{
    public enum OrganizerStatus
    {
        Active,
        Inactive,
        Blocked
    }

    [Table("organizers")]
    public class Organizer
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; } = string.Empty;

        [Column("email", TypeName = "varchar(255)")]
        public string Email { get; set; } = string.Empty;

        [Column("phone", TypeName = "varchar(255)")]
        public string Phone { get; set; } = string.Empty;

        [Column("role")]
        public OrganizerStatus Status { get; set; } = OrganizerStatus.Inactive;

        [Column("created_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
