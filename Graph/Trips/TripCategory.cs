using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Trips
{
    [Table("trip_categories")]
    public class TripCategory
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(50)")]
        public required string Name { get; set; }

        [Column("description", TypeName = "varchar(255)")]
        public string? Description { get; set; }

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
