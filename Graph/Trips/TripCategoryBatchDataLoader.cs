using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripCategoryBatchDataLoader : BatchDataLoader<int, TripCategory>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TripCategoryBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, TripCategory>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            // instead of fetching one person, we fetch multiple persons
            var categories = await context.TripCategories.Where(c => ids.Contains(c.Id)).ToDictionaryAsync(b => b.Id, cancellationToken);
            return categories;
        }
    }
}
