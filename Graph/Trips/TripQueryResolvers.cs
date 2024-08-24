using Sukalibur.Graph.Users;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Query))]
    public class TripQueryResolvers
    {
        public async Task<Trip> GetTrip(int id, TripBatchDataLoader dataLoader)
        {
            var trip = await dataLoader.LoadAsync(id);
            return trip;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Trip> GetTrips([Service] AppDbContext context)
        {
            return context.Trips;
        }
    }
}
