using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Notifications;

namespace Sukalibur.Graph.Notifications
{
    public class NotificationBatchDataLoader : BatchDataLoader<int, Notification>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public NotificationBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Notification>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var notifications = await context.Notifications.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return notifications.ToDictionary(x => x.Id);
        }
    }
}
