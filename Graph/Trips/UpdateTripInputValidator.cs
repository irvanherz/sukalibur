using FluentValidation;

namespace Sukalibur.Graph.Trips
{
    public class UpdateTripInputValidator : AbstractValidator<UpdateTripInput>
    {
        public UpdateTripInputValidator()
        {
            RuleFor(x => x.Title).Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
