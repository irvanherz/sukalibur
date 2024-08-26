using FluentValidation;

namespace Sukalibur.Graph.Organizers
{
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
