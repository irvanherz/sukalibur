using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Payments
{
    public class PaymentBatchDataLoader : BatchDataLoader<int, Payment>
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public PaymentBatchDataLoader(IDbContextFactory<AppDbContext> contextFactory, IBatchScheduler batchScheduler, DataLoaderOptions? options = null) : base(batchScheduler, options)
        {
            _contextFactory = contextFactory;
        }

        protected override async Task<IReadOnlyDictionary<int, Payment>> LoadBatchAsync(IReadOnlyList<int> ids, CancellationToken cancellationToken)
        {
            using var context = _contextFactory.CreateDbContext();

            // instead of fetching one person, we fetch multiple persons
            var payments = await context.Payments.Where(c => ids.Contains(c.Id)).ToListAsync(cancellationToken);
            return payments.ToDictionary(x => x.Id);
        }
    }
}
