using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Orders
{
    [Table("order_item_addons")]
    public class OrderItemAddons
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        [Column("addon_id")]
        public int AddonId { get; set; }

        [Column("qty")]
        public double Qty { get; set; } = 0;

        [Column("unit_price")]
        public double UnitPrice { get; set; } = 0;

        [Column("total_price")]
        public double TotalPrice { get; set; } = 0;

        [Column("addon_name")]
        public string AddonName { get; set; } = "";

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
