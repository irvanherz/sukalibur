using NodaTime;
using Sukalibur.Graph.Trips;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [UseFiltering]
        [UseSorting]
        public ICollection<OrganizerMember> Members { get; set; } = [];
        public ICollection<Trip> Trips { get; set; } = [];
    }
}
