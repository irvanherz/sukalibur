using FluentValidation;

namespace Sukalibur.Graph.Organizers
{
    public class UpdateOrganizerInputValidator : AbstractValidator<UpdateOrganizerInput>
    {
        public UpdateOrganizerInputValidator()
        {
            RuleFor(x => x.Username).Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.Name).Length(3, 255);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Length(5, 15);
        }
    }
}
