using FluentValidation;

namespace Sukalibur.Graph.Organizers
{
    public class UpdateOrganizerMemberInput
    {
        public int Id { get; set; }
        public OrganizerMemberRole? Role { get; set; }
        public OrganizerMemberStatus? Status { get; set; }
    }

    public class UpdateOrganizerMemberInputValidator : AbstractValidator<UpdateOrganizerMemberInput>
    {
        public UpdateOrganizerMemberInputValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Role).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
