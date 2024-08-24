using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Trip))]
    public class TripExtendedFieldResolvers
    {
        public async Task<TripCategory?> GetCategory([Parent] Trip trip, TripCategoryBatchDataLoader dataLoader)
        {
            if (trip.CategoryId == null) return null; // This is a nullable foreign key (optional relationship)
            var category = await dataLoader.LoadAsync(trip.CategoryId.Value);
            return category;
        }

        public async Task<Organizer> GetOrganizer([Parent] Trip trip, OrganizerBatchDataLoader dataLoader)
        {
            var organizer = await dataLoader.LoadAsync(trip.OrganizerId);
            return organizer;
        }
    }
}
