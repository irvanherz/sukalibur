using FluentValidation;

namespace Sukalibur.Graph.Notifications
{
    public class RemoveFcmTokenInput
    {
        public required string Token { get; set; }
    }

    public class RemoveFcmTokenInputValidator : AbstractValidator<RemoveFcmTokenInput>
    {
        public RemoveFcmTokenInputValidator()
        {
            RuleFor(input => input.Token).NotNull();
        }
    }
}