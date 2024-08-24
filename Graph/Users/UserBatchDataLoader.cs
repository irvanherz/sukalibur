using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;

namespace Sukalibur.Graph.Users
{
    public class UserBatchDataLoader : BatchDataLoader<int, User>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public UserBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            await using var _context = _contextFactory.CreateDbContext();
            // instead of fetching one person, we fetch multiple persons
            var users = await _context.Users.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return users.ToDictionary(x => x.Id);
        }
    }
}
