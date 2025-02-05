using AppAny.HotChocolate.FluentValidation;
using NodaTime;
using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Payments
{
    [ExtendObjectType(typeof(Mutation))]
    public class PaymentMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Payment> CreateOrderPayment([UseFluentValidation] CreateOrderPaymentInput input, [Service] PaymentService paymentService)
        {
            var payment = await paymentService.CreateOrderPaymentAsync(input);
            return payment;
        }
    }
}
