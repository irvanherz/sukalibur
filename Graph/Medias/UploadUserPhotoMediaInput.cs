namespace Sukalibur.Graph.Medias
{
    public class UploadUserPhotoMediaInput
    {
        [GraphQLType(typeof(NonNullType<UploadType>))]
        public required IFile File { get; set; }
        public int? UserId { get; set; }
    }
}
