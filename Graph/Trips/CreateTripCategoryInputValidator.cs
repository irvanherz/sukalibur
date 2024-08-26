using FluentValidation;

namespace Sukalibur.Graph.Trips
{
    public class CreateTripCategoryInputValidator : AbstractValidator<CreateTripCategoryInput>
    {
        public CreateTripCategoryInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
