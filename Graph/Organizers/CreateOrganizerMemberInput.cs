using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Organizers
{
    public class CreateOrganizerMemberInput
    {
        public int OrganizerId { get; set; }
        public int UserId { get; set; }
        public OrganizerMemberRole Role { get; set; } = OrganizerMemberRole.Admin;
        public OrganizerMemberStatus Status { get; set; } = OrganizerMemberStatus.Pending;
    }

    public class CreateOrganizerMemberInputValidator : AbstractValidator<CreateOrganizerMemberInput>
    {
        public CreateOrganizerMemberInputValidator()
        {
            RuleFor(x => x.OrganizerId).NotNull();
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.Role).IsInEnum();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}