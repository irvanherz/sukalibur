using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Users
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutationResolvers
    {
        [UseMutationConvention]
        public async Task<User> CreateUser([UseFluentValidation] CreateUserInput input, [Service] UserService userService)
        {
            var user = await userService.CreateUserAsync(input);
            return user;
        }

        [UseMutationConvention]
        public async Task<User> UpdateUser([UseFluentValidation] UpdateUserInput input, [Service] UserService userService)
        {
            var user = await userService.UpdateUserAsync(input);
            return user;
        }
    }
}
