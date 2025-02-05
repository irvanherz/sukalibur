using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Medias
{
    [ExtendObjectType(typeof(Mutation))]
    public class MediaMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Media> CreateMedia([UseFluentValidation] CreateMediaInput input, [Service] MediaService mediaService)
        {
            var media = await mediaService.CreateMediaAsync(input);
            return media;
        }

        [UseMutationConvention]
        public async Task<Media> UploadUserPhotoMedia([UseFluentValidation] CreateMediaInput input, [Service] MediaService mediaService)
        {
            var media = await mediaService.CreateMediaAsync(input);
            return media;
        }

        [UseMutationConvention]
        public async Task<Media> UploadUserPhotoMedia([UseFluentValidation] UploadUserPhotoMediaInput input, [Service] MediaService mediaService)
        {
            var media = await mediaService.CreateUserPhotoMediaAsync(input);
            return media;
        }

        [UseMutationConvention]
        public async Task<Media> UploadOrganizerPhotoMedia([UseFluentValidation] UploadOrganizerPhotoMediaInput input, [Service] MediaService mediaService)
        {
            var media = await mediaService.CreateOrganizerPhotoMediaAsync(input);
            return media;
        }

        [UseMutationConvention]
        public async Task<Media> UploadOrganizerTripImageMedia(UploadOrganizerTripImageMediaInput input, [Service] MediaService mediaService)
        {
            var media = await mediaService.CreateOrganizerTripImageMediaAsync(input);
            return media;
        }

        //[UseMutationConvention]
        //public async Task<Media> UpdateMedia([UseFluentValidation] UpdateMediaInput input, [Service] MediaService mediaService)
        //{
        //    var media = await mediaService.UpdateMediaAsync(input);
        //    return media;
        //}
    }
}
