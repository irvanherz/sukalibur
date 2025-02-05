using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Users;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Query))]
    public class TripScheduleQueryResolvers
    {
        public async Task<TripSchedule> GetTripSchedule(int id, TripScheduleBatchDataLoader dataLoader)
        {
            var schedule = await dataLoader.LoadAsync(id);
            return schedule;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TripSchedule> GetTripSchedules(AppDbContext context)
        {
            return context.TripSchedules;
        }
    }
}
