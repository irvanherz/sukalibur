using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Query))]
    public class OrganizerQueryResolvers
    {
        public async Task<Organizer> GetOrganizer(int id, OrganizerBatchDataLoader dataLoader)
        {
            var trip = await dataLoader.LoadAsync(id);
            return trip;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Organizer> GetOrganizers([Service] AppDbContext context)
        {
            return context.Organizers;
        }
    }
}
