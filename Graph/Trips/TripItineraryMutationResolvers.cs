using AppAny.HotChocolate.FluentValidation;
using System.Security.Claims;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Mutation))]
    public class TripItineraryMutationResolvers
    {
        //[UseMutationConvention]
        //public async Task<TripItinerary> CreateTripItinerary([UseFluentValidation] CreateTripItineraryInput input, [Service] TripItineraryService itineraryService, ClaimsPrincipal claimsPrincipal)
        //{
        //    var itinerary = await itineraryService.CreateTripItineraryAsync(input, claimsPrincipal);
        //    return itinerary;
        //}

        //[UseMutationConvention]
        //public async Task<TripItinerary> UpdateTripItinerary([UseFluentValidation] UpdateTripItineraryInput input, [Service] TripItineraryService itineraryService, ClaimsPrincipal claimsPrincipal)
        //{
        //    var itinerary = await itineraryService.UpdateTripItineraryAsync(input, claimsPrincipal);
        //    return itinerary;
        //}
    }
}
