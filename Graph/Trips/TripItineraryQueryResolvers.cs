using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Users;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Query))]
    public class TripItineraryQueryResolvers
    {
        public async Task<TripItinerary> GetTripItinerary(int id, TripItineraryBatchDataLoader dataLoader)
        {
            var itinerary = await dataLoader.LoadAsync(id);
            return itinerary;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TripItinerary> GetTripItineraries(AppDbContext context)
        {
            return context.TripItineraries;
        }
    }
}
