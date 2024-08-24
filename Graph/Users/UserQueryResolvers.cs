namespace Sukalibur.Graph.Users
{
    [ExtendObjectType(typeof(Query))]
    public class UserQueryResolvers
    {
        public async Task<User> GetUser(int id, UserBatchDataLoader dataLoader)
        {
            var user = await dataLoader.LoadAsync(id);
            return user;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> GetUsers([Service] AppDbContext context)
        {
            return context.Users;
        }
    }
}
