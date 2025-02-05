using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Payments;
using Sukalibur.Shared;

namespace Sukalibur.Graph.Orders
{
    public class OrderService : IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly AuthContext _authContext;

        public OrderService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper, AuthContext authContext)
        {
            _context = contextFactory.CreateDbContext();
            _mapper = mapper;
            _authContext = authContext;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderInput input)
        {
            var order = _mapper.Map<Order>(input);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public async Task<Order> UpdateOrderAsync(UpdateOrderInput input)
        {
            var order = await _context.Orders.FindAsync(input.Id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            _mapper.Map(input, order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Payment> CheckoutCartAsync(CheckoutCartInput input)
        {
            var cart = await _context.Carts.FindAsync(input.CartId);
            if (cart == null)
                throw new Exception("Cart not found");
            if (cart.UserId != _authContext.CurrentUser!.Id)
                throw new Exception("Unauthorized");
            var cartItems = await _context.CartItems.Include(ci => ci.Trip).Where(ci => input.CartItemIds.Contains(ci.Id)).ToListAsync();
            foreach (var cartItem in cartItems)
            {
                if (cartItem.CartId != cart.Id) throw new Exception("Cart item not found");
            }
            var cartItemsByOrganizerId = cartItems.GroupBy(ci => ci.Trip!.OrganizerId);
            var orders = cartItemsByOrganizerId.Select(group =>
            {
                var orderItems = group.Select(ci => _mapper.Map<OrderItem>(ci)).ToList();
                var orderAmount = orderItems.Sum(oi => oi.PackageUnitPrice * oi.Qty);
                var order = new Order
                {
                    UserId = cart.UserId,
                    OrganizerId = group.Key,
                    Status = OrderStatus.Unpaid,
                    Items = orderItems,
                    Amount = orderAmount
                };
                return order;
            }).ToList();
            var paymentAmount = orders.Sum(o => o.Amount);
            var payment = new Payment
            {
                Amount = paymentAmount,
                Status = PaymentStatus.Unpaid,
                Orders = orders
            };
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
