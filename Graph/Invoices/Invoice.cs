using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Invoices
{
    public enum InvoiceStatus
    {
        Unpaid,
        Pending,
        Paid,
        Cancelled,
    }

    [Table("invoices")]
    public class Invoice
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("amount")]
        public double Amount { get; set; } = 0;

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("status")]
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Unpaid;

        [Column("created_at", TypeName = "timestamptz")]

        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
