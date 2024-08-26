using FluentValidation;

namespace Sukalibur.Graph.Trips
{
    public class UpdateTripCategoryInputValidator : AbstractValidator<UpdateTripCategoryInput>
    {
        public UpdateTripCategoryInputValidator()
        {
            RuleFor(x => x.Name).Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
