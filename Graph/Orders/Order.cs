using NodaTime;
using Sukalibur.Graph.Payments;
using Sukalibur.Graph.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Orders
{
    public enum OrderStatus
    {
        Unpaid,
        Pending,
        Paid,
        Cancelled,
    }

    [Table("orders")]
    public class Order
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("amount")]
        public double Amount { get; set; } = 0;

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("organizer_id")]
        public int OrganizerId { get; set; }

        [Column("payment_id")]
        public int? PaymentId { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; } = OrderStatus.Unpaid;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public User User { get; set; } = null!;
        public Payment? Payment { get; set; }
        public ICollection<OrderItem> Items { get; set; } = [];
    }
}
