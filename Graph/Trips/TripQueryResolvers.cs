using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Users;
using System.Security.Claims;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Query))]
    public class TripQueryResolvers
    {
        //[UseProjection]
        public async Task<Trip> GetTrip(int id, TripBatchDataLoader dataLoader)
        {
            var trip = await dataLoader.LoadAsync(id);
            return trip;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        //[UseProjection]
        public IQueryable<Trip> GetTrips(AppDbContext context)
        {
            return context.Trips;
        }

        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Trip> GetMyTrips(AppDbContext context, ClaimsPrincipal claimsPrincipal)
        {
            var sub = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (sub == null)
            {
                throw new Exception("User not authenticated");
            }
            var userId = int.Parse(sub);
            return context.Trips.Where(t => t.Reservations.Any(m => m.UserId == userId));
        }

        [UsePaging]
        [UseProjection]
        public async Task<IQueryable<Trip>> GetRelevantTrips(int tripId, [Service] TripService tripService)
        {
            return await tripService.GetRelevantTripsAsync(tripId);
        }
    }
}
