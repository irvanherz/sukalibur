using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Users
{
    public class CreateUserInput
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateOnly? Dob { get; set; } = null;
        public UserGender Gender { get; set; } = UserGender.Other;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }

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
