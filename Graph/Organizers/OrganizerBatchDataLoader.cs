using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Organizers
{
    public class OrganizerBatchDataLoader : BatchDataLoader<int, Organizer>
    {
        private readonly AppDbContext _context;

        public OrganizerBatchDataLoader(AppDbContext context, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _context = context;
        }

        protected override async Task<IReadOnlyDictionary<int, Organizer>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            // instead of fetching one person, we fetch multiple persons
            var organizers = await _context.Organizers.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return organizers.ToDictionary(x => x.Id);
        }
    }
}
