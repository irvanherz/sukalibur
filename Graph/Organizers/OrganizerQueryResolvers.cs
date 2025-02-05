using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sukalibur.Graph.Organizers;
using Sukalibur.Shared;
using Sukalibur.Shared.Options;
using System.Security.Claims;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Query))]
    public class OrganizerQueryResolvers
    {
        private readonly AuthContext _authContext;

        public OrganizerQueryResolvers(AuthContext authContext, IOptions<JwtAuthOptions> jwtAuth)
        {
            _authContext = authContext;
        }
        public async Task<Organizer> GetOrganizer(int id, OrganizerBatchDataLoader dataLoader)
        {
            var trip = await dataLoader.LoadAsync(id);
            return trip;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Organizer> GetOrganizers(AppDbContext context)
        {
            return context.Organizers;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Organizer> GetMyOrganizers(AppDbContext context)
        {
            if (!_authContext.IsAuthenticated)
                throw new Exception("User not authenticated");

            var userId = _authContext.CurrentUser!.Id;
            return context.Organizers.Where(o => o.Members.Any(m => m.UserId == userId));
        }
    }
}
