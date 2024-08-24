using Microsoft.EntityFrameworkCore;
using System;

namespace Sukalibur.Graph.Users
{
    public class UserBatchDataLoader : BatchDataLoader<int, User>
    {
        private readonly AppDbContext _context;

        public UserBatchDataLoader(AppDbContext context, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _context = context;
        }

        protected override async Task<IReadOnlyDictionary<int, User>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            // instead of fetching one person, we fetch multiple persons
            var users = await _context.Users.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return users.ToDictionary(x => x.Id);
        }
    }
}
