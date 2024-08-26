using FluentValidation;

namespace Sukalibur.Graph.Users
{
    public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
    {
        public CreateUserInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.FullName).Length(3, 255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).Length(10, 15);
            RuleFor(x => x.Password).NotEmpty().Length(3, 255);
        }
    }
}
