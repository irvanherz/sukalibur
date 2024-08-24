using Sukalibur.Graph.Users;
using System.Security.Claims;

namespace Sukalibur.Graph.Auth
{
    [ExtendObjectType(typeof(Query))]
    public class AuthQueryResolvers
    {
        public async Task<User> Me(UserBatchDataLoader dataLoader, ClaimsPrincipal claimsPrincipal)
        {
            var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (sub == null)
            {
                throw new Exception("User not authenticated");
            }
            var userId = int.Parse(sub);
            var user = await dataLoader.LoadAsync(userId);
            return user;
        }
    }
}
