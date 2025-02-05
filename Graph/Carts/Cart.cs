using NodaTime;
using Sukalibur.Graph.Medias;
using Sukalibur.Graph.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Carts
{
    [Table("carts")]
    public class Cart
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; } = 0;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public User User { get; set; } = null!;
    }
}
