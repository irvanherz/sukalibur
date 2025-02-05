namespace Sukalibur.Graph.Medias
{
    public class UploadOrganizerPhotoMediaInput
    {
        [GraphQLType(typeof(NonNullType<UploadType>))]
        public required IFile File { get; set; }
        public required int OrganizerId { get; set; }
    }
}
