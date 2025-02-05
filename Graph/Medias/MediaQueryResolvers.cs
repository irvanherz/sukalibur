using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Medias;

namespace Sukalibur.Graph.Medias
{
    [ExtendObjectType(typeof(Query))]
    public class MediaQueryResolvers
    {
        public async Task<Media> GetMedia(int id, MediaBatchDataLoader dataLoader)
        {
            var media = await dataLoader.LoadAsync(id);
            return media!;
        }

        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Media> GetMedias(AppDbContext context)
        {
            return context.Medias;
        }
    }
}
