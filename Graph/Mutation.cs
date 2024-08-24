using Sukalibur.Graph.Users;

namespace Sukalibur.Graph
{
    public class Mutation
    {
        [UseMutationConvention]
        public User? UpdateUsername([ID] Guid userId, string username)
        {
            return new User { Username = username };
        }
    }
}
