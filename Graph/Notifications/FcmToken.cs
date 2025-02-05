using NodaTime;
using Sukalibur.Graph.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Notifications
{
    public enum FcmTokenDeviceType
    {
        Web,
        Android,
        Ios,
    }
    [Table("fcm_tokens")]
    public class FcmToken
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("fcm_token", TypeName = "text")]
        public string Token { get; set; } = null!;

        [Column("device_type", TypeName = "varchar(50)")]
        public FcmTokenDeviceType DeviceType { get; set; } = FcmTokenDeviceType.Web;

        [Column("device_info")]
        public string DeviceInfo { get; set; } = null!;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public User User { get; set; } = null!;
    }
}
