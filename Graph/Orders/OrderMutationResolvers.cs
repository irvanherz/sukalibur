using AppAny.HotChocolate.FluentValidation;
using Sukalibur.Graph.Payments;

namespace Sukalibur.Graph.Orders
{
    [ExtendObjectType(typeof(Mutation))]
    public class OrderMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Order> CreateOrder([UseFluentValidation] CreateOrderInput input, [Service] OrderService orderService)
        {
            var order = await orderService.CreateOrderAsync(input);
            return order;
        }

        [UseMutationConvention]
        public async Task<Payment> CheckoutCart([UseFluentValidation] CheckoutCartInput input, [Service] OrderService orderService)
        {
            var payment = await orderService.CheckoutCartAsync(input);
            return payment;
        }

        [UseMutationConvention]
        public async Task<Order> UpdateOrder([UseFluentValidation] UpdateOrderInput input, [Service] OrderService orderService)
        {
            var order = await orderService.UpdateOrderAsync(input);
            return order;
        }
    }
}
