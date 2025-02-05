using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Mutation))]
    public class TripScheduleMutationResolvers
    {
        //[UseMutationConvention]
        //public async Task<TripSchedule> CreateTripSchedule([UseFluentValidation] CreateTripScheduleInput input, [Service] TripScheduleService scheduleService)
        //{
        //    var schedule = await scheduleService.CreateTripScheduleAsync(input);
        //    return schedule;
        //}

        //[UseMutationConvention]
        //public async Task<TripSchedule> UpdateTripSchedule([UseFluentValidation] UpdateTripScheduleInput input, [Service] TripScheduleService scheduleService)
        //{
        //    var schedule = await scheduleService.UpdateTripScheduleAsync(input);
        //    return schedule;
        //}
    }
}
