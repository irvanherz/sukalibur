using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripScheduleBatchDataLoader : BatchDataLoader<int, TripSchedule>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TripScheduleBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, TripSchedule>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            // instead of fetching one person, we fetch multiple persons
            var categories = await context.TripSchedules.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return categories.ToDictionary(x => x.Id);
        }
    }
}
