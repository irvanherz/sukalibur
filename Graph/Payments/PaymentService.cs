
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Payments
{
    public class PaymentService : IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PaymentService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _context = contextFactory.CreateDbContext();
            _mapper = mapper;
        }

        public async Task<Payment> CreateOrderPaymentAsync(CreateOrderPaymentInput input)
        {
            var order = await _context.Orders.FindAsync(input.OrderId);
            if (order == null)
                throw new Exception("Order not found");
            var paymentAmount = 0;
            var method = await _context.PaymentChannels.FindAsync(input.ChannelId);
            var payment = new Payment
            {
                ChannelId = input.ChannelId,
                Amount = paymentAmount,
                Status = PaymentStatus.Unpaid,
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
