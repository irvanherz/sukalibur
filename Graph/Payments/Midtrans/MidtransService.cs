using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sukalibur.Shared.Options;

namespace Sukalibur.Graph.Payments.Midtrans
{
    public class MidtransService
    {
        private readonly MidtransOptions _options;
        private readonly AppDbContext _context;
        public MidtransService(IOptions<MidtransOptions> options, IDbContextFactory<AppDbContext> contextFactory)
        {
            _options = options.Value;
            _context = contextFactory.CreateDbContext();
        }

    }
}
