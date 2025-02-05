using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    public enum TripReservationStatus
    {
        Pending,
        Approved,
        Cancelled,
        Rejected,
    }

    [Table("trip_reservations")]
    public class TripReservation
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("trip_id")]
        public int TripId { get; set; }

        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("status")]
        public TripReservationStatus Status { get; set; } = TripReservationStatus.Pending;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public Trip Trip { get; set; } = null!;
    }
}
