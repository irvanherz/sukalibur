using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Organizers
{
    public class OrganizerMemberBatchDataLoader : BatchDataLoader<int, OrganizerMember>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public OrganizerMemberBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, OrganizerMember>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var members = await context.OrganizerMembers.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return members.ToDictionary(x => x.Id);
        }
    }
}
