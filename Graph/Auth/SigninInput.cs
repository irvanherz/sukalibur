using FluentValidation;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Auth
{
    public class SigninInput
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class SigninInputValidator : AbstractValidator<SigninInput>
    {
        public SigninInputValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
