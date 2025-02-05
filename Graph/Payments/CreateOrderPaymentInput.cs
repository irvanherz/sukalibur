using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Payments
{
    public class CreateOrderPaymentInput
    {
        public required int UserId { get; set; }
        public required int OrderId { get; set; }
        public required int ChannelId { get; set; }
    }
}
