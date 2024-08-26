using FluentValidation;

namespace Sukalibur.Graph.Auth
{
    public class RegisterUserInputValidator : AbstractValidator<RegisterUserInput>
    {
        public RegisterUserInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.Password).NotEmpty().Length(3, 255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
