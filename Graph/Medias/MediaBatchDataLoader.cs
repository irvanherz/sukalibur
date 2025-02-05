using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Medias
{
    public class MediaBatchDataLoader : BatchDataLoader<int, Media>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public MediaBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Media>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var medias = await context.Medias.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return medias.ToDictionary(x => x.Id);
        }
    }
}
