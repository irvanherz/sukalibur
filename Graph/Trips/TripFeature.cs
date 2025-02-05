using NodaTime;
using Pgvector;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    [Table("trip_features")]
    public class TripFeature
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("trip_id")]
        public required int TripId { get; set; }

        [Column("embedding", TypeName = "vector(384)")]
        public Vector? Embedding { get; set; }

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public Trip Trip { get; set; } = null!;
    }
}
