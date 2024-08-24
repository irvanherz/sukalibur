using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Organizers
{
    public class OrganizerBatchDataLoader : BatchDataLoader<int, Organizer>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public OrganizerBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Organizer>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            await using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var organizers = await context.Organizers.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return organizers.ToDictionary(x => x.Id);
        }
    }
}
