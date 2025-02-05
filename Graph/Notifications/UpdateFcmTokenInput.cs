using FluentValidation;

namespace Sukalibur.Graph.Notifications
{
    public class UpdateFcmTokenInput
    {
        public required string Token { get; set; }
    }

    public class AddFcmTokenInputValidator : AbstractValidator<UpdateFcmTokenInput>
    {
        public AddFcmTokenInputValidator()
        {
            RuleFor(input => input.Token).NotNull();
        }
    }
}