using FluentValidation;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Auth
{
    public class SignupInput
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class SignupInputValidator : AbstractValidator<SignupInput>
    {
        public SignupInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.Password).NotEmpty().Length(3, 255);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
