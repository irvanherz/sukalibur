using FluentValidation;

namespace Sukalibur.Graph.Auth
{
    public class LoginUserInputValidator : AbstractValidator<LoginUserInput>
    {
        public LoginUserInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
