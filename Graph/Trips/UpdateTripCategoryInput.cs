using FluentValidation;

namespace Sukalibur.Graph.Trips
{
    public class UpdateTripCategoryInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateTripCategoryInputValidator : AbstractValidator<UpdateTripCategoryInput>
    {
        public UpdateTripCategoryInputValidator()
        {
            RuleFor(x => x.Name).Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }
}
