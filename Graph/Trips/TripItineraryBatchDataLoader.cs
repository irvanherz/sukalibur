using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripItineraryBatchDataLoader : BatchDataLoader<int, TripItinerary>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TripItineraryBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, TripItinerary>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();
            // instead of fetching one person, we fetch multiple persons
            var itineraries = await context.TripItineraries.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return itineraries.ToDictionary(x => x.Id);
        }
    }
}
