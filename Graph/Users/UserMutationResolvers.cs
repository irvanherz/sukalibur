namespace Sukalibur.Graph.Users
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutationResolvers
    {
        [UseMutationConvention]
        public async Task<User> CreateUser(CreateUserInput input, [Service] UserService userService)
        {
            var user = await userService.CreateUserAsync(input);
            return user;
        }

        [UseMutationConvention]
        public async Task<User> UpdateUser(UpdateUserInput input, [Service] UserService userService)
        {
            var user = await userService.UpdateUserAsync(input);
            return user;
        }
    }
}
