using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Users;
using Sukalibur.Shared;

namespace Sukalibur.Graph.Carts
{
    public class CartService : IAsyncDisposable
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly AuthContext _authContext;
        public CartService(IMapper mapper, IDbContextFactory<AppDbContext> contextFactory, AuthContext authContext)
        {
            _mapper = mapper;
            _context = contextFactory.CreateDbContext();
            _authContext = authContext;
        }
        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public async Task<CartItem> AddOrUpdateCartItemAsync(AddOrUpdateCartItemInput input)
        {
            if (!_authContext.IsAuthenticated)
                throw new GraphQLException(ErrorBuilder.New().SetCode("UNAUTHORIZED").SetMessage("You are not authorized to perform this action").Build());
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == input.CartId);
            if (cart == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified cart was not found").Build());
            if (cart.UserId != _authContext.CurrentUser!.Id && _authContext.CurrentUser.Role != UserRole.Super)
                throw new Exception("Unauthorized");

            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == input.TripId);
            if (trip == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified trip was not found").Build());
            var schedule = await _context.TripSchedules.FirstOrDefaultAsync(s => s.Id == input.ScheduleId);
            if (schedule == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified schedule not found").Build());
            var package = await _context.TripPackages.FirstOrDefaultAsync(p => p.Id == input.PackageId);
            if (package == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified package was not found").Build());

            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartId == input.CartId && i.TripId == input.TripId && i.ScheduleId == input.ScheduleId && i.PackageId == i.PackageId);
            if (item == null)
            {
                item = _mapper.Map(input, item)!;
                _context.CartItems.Add(item);
            }
            else
            {
                item.Qty += input.Qty;
                item.Qty = item.Qty < 0 ? 0 : item.Qty;
            }
            item.TripName = trip.Name;
            item.PackageName = package.Name;
            item.PackageUnitPrice = package.Price;
            item.PackageTotalPrice = package.Price * item.Qty;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CartItem> DeleteCartItemAsync(int id)
        {
            if (!_authContext.IsAuthenticated)
                throw new GraphQLException(ErrorBuilder.New().SetCode("UNAUTHORIZED").SetMessage("You are not authorized to perform this action").Build());
            var item = await _context.CartItems.Include(c => c.Cart).FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified cart item was not found").Build());
            if (item.Cart.UserId != _authContext.CurrentUser!.Id && _authContext.CurrentUser.Role != UserRole.Super)
                throw new GraphQLException(ErrorBuilder.New().SetCode("UNAUTHORIZED").SetMessage("You are not authorized to perform this action").Build());
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CartItem> UpdateCartItemAsync(UpdateCartItemInput input)
        {
            if (!_authContext.IsAuthenticated)
                throw new GraphQLException(ErrorBuilder.New().SetCode("UNAUTHORIZED").SetMessage("You are not authorized to perform this action").Build());
            var item = await _context.CartItems.Include(c => c.Cart).FirstOrDefaultAsync(i => i.Id == input.Id);
            if (item == null)
                throw new GraphQLException(ErrorBuilder.New().SetCode("NOT_FOUND").SetMessage("The specified cart item was not found").Build());
            if (item.Cart.UserId != _authContext.CurrentUser!.Id && _authContext.CurrentUser.Role != UserRole.Super)
                throw new GraphQLException(ErrorBuilder.New().SetCode("UNAUTHORIZED").SetMessage("You are not authorized to perform this action").Build());
            item = _mapper.Map(input, item)!;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
