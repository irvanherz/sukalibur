using FluentValidation;
using Sukalibur.Graph.Auth;

namespace Sukalibur.Graph.Carts
{
    public class UpdateCartItemInput
    {
        public int Id { get; set; }
        public int Qty { get; set; }
    }

    public class UpdateCartItemInputValidator : AbstractValidator<UpdateCartItemInput>
    {
        public UpdateCartItemInputValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Qty).NotNull().GreaterThan(0);
        }
    }
}
