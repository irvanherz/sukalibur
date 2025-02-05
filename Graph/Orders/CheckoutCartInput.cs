using FluentValidation;

namespace Sukalibur.Graph.Orders
{
    public class CheckoutCartInput
    {
        public int CartId { get; set; } = 0;
        public List<int> CartItemIds { get; set; } = [];
    }

    public class CheckoutCartInputValidator : AbstractValidator<CheckoutCartInput>
    {
        public CheckoutCartInputValidator()
        {
            RuleFor(x => x.CartId).NotNull().GreaterThan(0);
            RuleFor(x => x.CartItemIds).NotEmpty();
        }
    }
}
