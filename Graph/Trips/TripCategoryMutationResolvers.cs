namespace Sukalibur.Graph.Trips
{
    [ExtendObjectType(typeof(Mutation))]
    public class TripCategoryMutationResolvers
    {
        [UseMutationConvention]
        public async Task<TripCategory> CreateTripCategory(CreateTripCategoryInput input, [Service] TripCategoryService categoryService)
        {
            var category = await categoryService.CreateTripCategoryAsync(input);
            return category;
        }

        [UseMutationConvention]
        public async Task<TripCategory> UpdateTripCategory(UpdateTripCategoryInput input, [Service] TripCategoryService categoryService)
        {
            var category = await categoryService.UpdateTripCategoryAsync(input);
            return category;
        }
    }
}
