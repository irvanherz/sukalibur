namespace Sukalibur.Graph.Medias
{
    public class UploadOrganizerTripImageMediaInput
    {
        [GraphQLType(typeof(NonNullType<UploadType>))]
        public required IFile File { get; set; }
        public required int OrganizerId { get; set; }
    }
}
