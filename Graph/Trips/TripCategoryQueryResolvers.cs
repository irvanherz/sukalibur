using Sukalibur.Graph.Users;

namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Query))]
    public class TripCategoryQueryResolvers
    {
        public async Task<TripCategory> GetTripCategory(int id, TripCategoryBatchDataLoader dataLoader)
        {
            var category = await dataLoader.LoadAsync(id);
            return category;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TripCategory> GetTripCategories([Service] AppDbContext context)
        {
            return context.TripCategories;
        }
    }
}
