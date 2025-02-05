using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Auth
{
    [Table("refresh_tokens")]
    public class RefreshToken
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public required int UserId { get; set; }

        [Column("token", TypeName = "varchar(255)")]
        public required string Token { get; set; }

        [Column("expired_at")]
        public Instant ExpiredAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("created_at")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
