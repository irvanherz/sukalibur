using FluentValidation;

namespace Sukalibur.Graph.Trips
{
    public class CreateTripInputValidator : AbstractValidator<CreateTripInput>
    {
        public CreateTripInputValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
