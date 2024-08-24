using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripBatchDataLoader : BatchDataLoader<int, Trip>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TripBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Trip>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            await using var context = _contextFactory.CreateDbContext();
            // instead of fetching one person, we fetch multiple persons
            var trips = await context.Trips.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return trips.ToDictionary(x => x.Id);
        }
    }
}
