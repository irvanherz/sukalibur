using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Mutation))]
    public class TripMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Trip> CreateTrip(CreateTripInput input, [Service] TripService tripService)
        {
            var trip = await tripService.CreateTripAsync(input);
            return trip;
        }

        [UseMutationConvention]
        public async Task<Trip> UpdateTrip(UpdateTripInput input, [Service] TripService tripService)
        {
            var trip = await tripService.UpdateTripAsync(input);
            return trip;
        }
    }
}
