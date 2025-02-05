using AppAny.HotChocolate.FluentValidation;
using Sukalibur.Graph.Orders;

namespace Sukalibur.Graph.Carts
{
    [ExtendObjectType(typeof(Mutation))]
    public class CartMutationResolvers
    {
        [UseMutationConvention]
        public async Task<CartItem> AddOrUpdateCartItem([UseFluentValidation] AddOrUpdateCartItemInput input, [Service] CartService cartService)
        {
            var item = await cartService.AddOrUpdateCartItemAsync(input);
            return item;
        }

        public async Task<CartItem> UpdateCartItem([UseFluentValidation] UpdateCartItemInput input, [Service] CartService cartService)
        {
            var item = await cartService.UpdateCartItemAsync(input);
            return item;
        }

        [UseMutationConvention]
        public async Task<CartItem> DeleteCartItem(int id, [Service] CartService cartService)
        {
            var item = await cartService.DeleteCartItemAsync(id);
            return item;
        }
    }
}
