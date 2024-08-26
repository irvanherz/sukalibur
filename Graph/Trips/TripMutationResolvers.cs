using AppAny.HotChocolate.FluentValidation;
using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Mutation))]
    public class TripMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Trip> CreateTrip([UseFluentValidation] CreateTripInput input, [Service] TripService tripService)
        {
            var trip = await tripService.CreateTripAsync(input);
            return trip;
        }

        [UseMutationConvention]
        public async Task<Trip> UpdateTrip([UseFluentValidation] UpdateTripInput input, [Service] TripService tripService)
        {
            var trip = await tripService.UpdateTripAsync(input);
            return trip;
        }
    }
}
