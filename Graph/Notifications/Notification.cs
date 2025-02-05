using NodaTime;
using Sukalibur.Graph.Orders;
using Sukalibur.Graph.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Notifications
{
    [Table("notifications")]
    public class Notification
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("code", TypeName = "varchar(255)")]
        public required string Code { get; set; }

        [Column("data")]
        public required string Data { get; set; }

        [Column("has_read")]
        public bool HasRead { get; set; } = false;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public User User { get; set; } = null!;
    }
}
