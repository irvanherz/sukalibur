using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    public enum TripScheduleStatus
    {
        Upcoming,
        Ongoing,
        Finished,
        Cancelled
    }

    [Table("trip_schedules")]
    public class TripSchedule
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("trip_id")]
        public int TripId { get; set; }

        [Column("scheduled_at", TypeName = "timestamptz")]
        public Instant ScheduledAt { get; set; }

        [Column("status")]
        public TripScheduleStatus Status { get; set; } = TripScheduleStatus.Upcoming;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public Trip Trip { get; set; } = null!;
    }
}
