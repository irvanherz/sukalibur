using FluentValidation;

namespace Sukalibur.Graph.Users
{
    public class UpdateUserInputValidator : AbstractValidator<UpdateUserInput>
    {
        public UpdateUserInputValidator()
        {
            RuleFor(x => x.Username).Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.FullName).Length(3, 255);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Length(10, 15);
            RuleFor(x => x.Password).Length(3, 255);
        }
    }
}
