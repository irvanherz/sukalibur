using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Mutation))]
    public class OrganizerMutationResolvers
    {
        [UseMutationConvention]
        public async Task<Organizer> CreateOrganizer([UseFluentValidation] CreateOrganizerInput input, [Service] OrganizerService organizerService)
        {
            var organizer = await organizerService.CreateOrganizerAsync(input);
            return organizer;
        }

        [UseMutationConvention]
        public async Task<Organizer> UpdateOrganizer([UseFluentValidation] UpdateOrganizerInput input, [Service] OrganizerService organizerService)
        {
            var organizer = await organizerService.UpdateOrganizerAsync(input);
            return organizer;
        }
    }
}
