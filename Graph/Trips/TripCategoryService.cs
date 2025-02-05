using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Trips
{
    public class TripCategoryService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public TripCategoryService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<TripCategory> CreateTripCategoryAsync(CreateTripCategoryInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = _mapper.Map<TripCategory>(input);
            await context.TripCategories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<TripCategory> UpdateTripCategoryAsync(UpdateTripCategoryInput input)
        {
            using var context = _contextFactory.CreateDbContext();

            var category = await context.TripCategories.FindAsync(input.Id);
            if (category == null)
            {
                throw new Exception("Trip not found");
            }
            _mapper.Map(input, category);
            await context.SaveChangesAsync();
            return category;
        }
    }
}
