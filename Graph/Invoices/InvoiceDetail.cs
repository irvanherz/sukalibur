using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Invoices
{
    [Table("invoice_details")]
    public class InvoiceDetail
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("qty")]
        public double Qty { get; set; } = 0;

        [Column("unit_price")]
        public double UnitPrice { get; set; } = 0;

        [Column("amount")]
        public double Amount { get; set; } = 0;

        [Column("tripId")]
        public int TripId { get; set; }

        [Column("scheduleId")]
        public int ScheduleId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
