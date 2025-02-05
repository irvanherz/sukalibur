using NodaTime;
using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Payments
{
    [ExtendObjectType(typeof(Query))]
    public class PaymentQueryResolvers
    {
        public async Task<Payment> GetPayment(int id, PaymentBatchDataLoader dataLoader)
        {
            var payment = await dataLoader.LoadAsync(id);
            return payment;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Payment> GetPayments(AppDbContext context)
        {
            return context.Payments;
        }
    }
}
