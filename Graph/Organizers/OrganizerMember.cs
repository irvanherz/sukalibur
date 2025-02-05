using NodaTime;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Organizers
{
    public enum OrganizerMemberStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum OrganizerMemberRole
    {
        Super,
        Admin,
        CustomerService
    }

    [Table("organizer_members")]
    public class OrganizerMember
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("organizer_id")]
        public int OrganizerId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role")]
        public OrganizerMemberRole Role { get; set; } = OrganizerMemberRole.Admin;

        [Column("status")]
        public OrganizerMemberStatus Status { get; set; } = OrganizerMemberStatus.Pending;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public Organizer Organizer { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
