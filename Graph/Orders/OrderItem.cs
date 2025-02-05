using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Orders
{
    [Table("order_items")]
    public class OrderItem
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("qty")]
        public double Qty { get; set; } = 0;

        [Column("trip_id")]
        public int TripId { get; set; }

        [Column("schedule_id")]
        public int ScheduleId { get; set; }

        [Column("package_id")]
        public int PackageId { get; set; }

        [Column("package_price")]
        public double PackageUnitPrice { get; set; } = 0;

        [Column("package_total_price")]
        public double PackageTotalPrice { get; set; } = 0;

        [Column("addons_unit_price")]
        public double AddonsUnitPrice { get; set; } = 0;

        [Column("addons_total_price")]
        public double AddonsTotalPrice { get; set; } = 0;

        [Column("total_price")]
        public double TotalPrice { get; set; } = 0;

        [Column("trip_name")]
        public string TripName { get; set; } = "";

        [Column("package_name")]
        public string PackageName { get; set; } = "";

        [Column("scheduled_at")]
        public Instant ScheduledAt { get; set; }

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
