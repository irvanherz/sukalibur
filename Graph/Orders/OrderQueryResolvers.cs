using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Orders;

namespace Sukalibur.Graph.Orders
{
    [ExtendObjectType(typeof(Query))]
    public class OrderQueryResolvers
    {
        public async Task<Order> GetOrder(int id, OrderBatchDataLoader dataLoader)
        {
            var order = await dataLoader.LoadAsync(id);
            return order;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Order> GetOrders(AppDbContext context)
        {
            return context.Orders;
        }
    }
}
