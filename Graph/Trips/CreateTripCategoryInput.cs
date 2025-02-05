using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    public class CreateTripCategoryInput
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class CreateTripCategoryInputValidator : AbstractValidator<CreateTripCategoryInput>
    {
        public CreateTripCategoryInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
