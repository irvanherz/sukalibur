using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Trips
{
    public class TripService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public TripService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Trip> CreateTripAsync(CreateTripInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var trip = _mapper.Map<Trip>(input);
            await context.Trips.AddAsync(trip);
            await context.SaveChangesAsync();
            return trip;
        }

        public async Task<Trip> UpdateTripAsync(UpdateTripInput input)
        {
            using var context = _contextFactory.CreateDbContext();

            var trip = await context.Trips.FindAsync(input.Id);
            if (trip == null)
            {
                throw new Exception("Trip not found");
            }
            _mapper.Map(input, trip);
            await context.SaveChangesAsync();
            return trip;
        }
    }
}
