using NetTopologySuite.Geometries;
using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    [Table("trip_itineraries")]
    public class TripItinerary
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("trip_id")]
        public required int TripId { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public required string Name { get; set; } = string.Empty;

        [Column("description", TypeName = "text")]
        public string Description { get; set; } = string.Empty;

        [Column("is_meeting_point")]
        public bool IsMeetingPoint { get; set; } = false;

        [Column("location")]
        public Point? Location { get; set; }

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("duration")]
        public int Duration { get; set; } = 0;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public Trip Trip { get; set; } = null!;
    }
}
