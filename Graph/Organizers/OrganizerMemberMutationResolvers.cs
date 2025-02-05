using AppAny.HotChocolate.FluentValidation;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Mutation))]
    public class OrganizerMemberMutationResolvers
    {
        [UseMutationConvention]
        public async Task<OrganizerMember> CreateOrganizerMember([UseFluentValidation] CreateOrganizerMemberInput input, [Service] OrganizerMemberService memberService)
        {
            var member = await memberService.CreateOrganizerMemberAsync(input);
            return member;
        }

        [UseMutationConvention]
        public async Task<OrganizerMember> UpdateOrganizerMember([UseFluentValidation] UpdateOrganizerMemberInput input, [Service] OrganizerMemberService memberService)
        {
            var member = await memberService.UpdateOrganizerMemberAsync(input);
            return member;
        }
    }
}
