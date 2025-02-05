using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Trips
{
    public class TripScheduleService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public TripScheduleService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        //public async Task<TripSchedule> CreateTripScheduleAsync(CreateTripScheduleInput input)
        //{
        //    using var context = _contextFactory.CreateDbContext();
        //    var schedule = _mapper.Map<TripSchedule>(input);
        //    await context.TripSchedules.AddAsync(schedule);
        //    await context.SaveChangesAsync();
        //    return schedule;
        //}

        //public async Task<TripSchedule> UpdateTripScheduleAsync(UpdateTripScheduleInput input)
        //{
        //    using var context = _contextFactory.CreateDbContext();

        //    var schedule = await context.TripSchedules.FindAsync(input.Id);
        //    if (schedule == null)
        //    {
        //        throw new Exception("Trip schedule not found");
        //    }
        //    _mapper.Map(input, schedule);
        //    await context.SaveChangesAsync();
        //    return schedule;
        //}
    }
}
