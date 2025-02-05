using FluentValidation;

namespace Sukalibur.Graph.Orders
{
    public class UpdateOrderInput
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public OrderStatus? Status { get; set; }
    }

    public class UpdateOrderInputValidator : AbstractValidator<UpdateOrderInput>
    {
        public UpdateOrderInputValidator()
        {
            RuleFor(x => x.Username).Length(3, 255).Matches("[a-bA-B0-9_]");
            RuleFor(x => x.Name).Length(3, 255);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Length(5, 15);
        }
    }
}
