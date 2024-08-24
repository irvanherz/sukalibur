using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Trips
{
    public class TripCategoryBatchDataLoader : BatchDataLoader<int, TripCategory>
    {
        private readonly AppDbContext _context;

        public TripCategoryBatchDataLoader(AppDbContext context, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _context = context;
        }

        protected override async Task<IReadOnlyDictionary<int, TripCategory>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            // instead of fetching one person, we fetch multiple persons
            var categories = await _context.TripCategories.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return categories.ToDictionary(x => x.Id);
        }
    }
}
