using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Orders;

namespace Sukalibur.Graph.Carts
{
    [ExtendObjectType(typeof(Query))]
    public class CartQueryResolvers
    {
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<CartItem> GetCartItems(AppDbContext context)
        {
            return context.CartItems;
        }
    }
}
