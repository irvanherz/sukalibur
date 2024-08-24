using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Trips
{
    public enum TripStatus
    {
        Active,
        Inactive,
        Blocked
    }

    [Table("trips")]
    public class Trip
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title", TypeName = "varchar(255)")]
        public string Title { get; set; } = string.Empty;

        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; } = string.Empty;

        [Column("category_id")]
        public int? CategoryId { get; set; } = null;

        [Column("organizer_id")]
        public int OrganizerId { get; set; }

        [Column("created_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public TripCategory? Category { get; set; } = null;
        public Organizer Organizer { get; set; } = null!;
    }
}
