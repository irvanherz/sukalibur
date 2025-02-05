using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    public enum TripAddonStatus
    {
        Active,
        Inactive,
    }

    [Table("trip_addons")]
    public class TripAddon
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("trip_id")]
        public int TripId { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public required string Name { get; set; }

        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; } = string.Empty;

        [Column("price")]
        public required double Price { get; set; }

        [Column("status")]
        public TripAddonStatus Status { get; set; } = TripAddonStatus.Inactive;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
