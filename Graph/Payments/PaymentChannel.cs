using NodaTime;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Payments
{
    public enum PaymentChannelStatus
    {
        Enabled,
        Disabled,
    }

    public enum PaymentChannelType
    {
        BankTransfer,
        CreditCard,
        VirtualAccount,
        EWallet
    }

    public class PaymentChannel
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; } = "";

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("status")]
        public PaymentChannelStatus Status { get; set; } = PaymentChannelStatus.Disabled;

        [Column("type")]
        public PaymentChannelType Type { get; set; }

        [Column("status_message")]
        public string StatusMessage { get; set; } = "";

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }
}
