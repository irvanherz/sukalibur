using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Organizers
{
    public class CreateOrganizerInput
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public OrganizerStatus Status { get; set; } = OrganizerStatus.Active;
    }

    public class CreateOrganizerInputValidator : AbstractValidator<CreateOrganizerInput>
    {
        public CreateOrganizerInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Length(5, 15);
        }
    }
}