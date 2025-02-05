using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Orders
{
    public class OrderBatchDataLoader : BatchDataLoader<int, Order>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public OrderBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Order>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var orders = await context.Orders.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return orders.ToDictionary(x => x.Id);
        }
    }
}
