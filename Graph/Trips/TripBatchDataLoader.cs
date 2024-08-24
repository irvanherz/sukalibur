using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripBatchDataLoader : BatchDataLoader<int, Trip>
    {
        private readonly AppDbContext _context;

        public TripBatchDataLoader(AppDbContext context, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _context = context;
        }

        protected override async Task<IReadOnlyDictionary<int, Trip>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            // instead of fetching one person, we fetch multiple persons
            var trips = await _context.Trips.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return trips.ToDictionary(x => x.Id);
        }
    }
}
