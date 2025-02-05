using NodaTime;
using Sukalibur.Graph.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Payments
{
    public enum PaymentStatus
    {
        Unpaid,
        Pending,
        Success,
        Failed
    }

    [Table("payments")]
    public class Payment
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("amount")]
        public double Amount { get; set; }

        [Column("status")]
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;

        [Column("channel_id")]
        public int ChannelId { get; set; }

        [Column("payment_code")]
        public string? PaymentCode { get; set; }

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        public PaymentChannel? Method { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
    }
}
