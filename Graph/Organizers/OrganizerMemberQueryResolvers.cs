using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Query))]
    public class OrganizerMemberQueryResolvers
    {
        public async Task<OrganizerMember> GetOrganizerMember(int id, OrganizerMemberBatchDataLoader dataLoader)
        {
            var member = await dataLoader.LoadAsync(id);
            return member;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<OrganizerMember> GetOrganizerMembers(AppDbContext context)
        {
            return context.OrganizerMembers;
        }
    }
}
