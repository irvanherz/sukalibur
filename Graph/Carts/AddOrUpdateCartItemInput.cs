using FluentValidation;
using Sukalibur.Graph.Auth;

namespace Sukalibur.Graph.Carts
{
    public class AddOrUpdateCartItemInput
    {
        public int CartId { get; set; }
        public int TripId { get; set; }
        public int ScheduleId { get; set; }
        public int PackageId { get; set; }
        [DefaultValue(1)]
        public int Qty { get; set; } = 1;
    }

    public class AddOrUpdateCartItemInputValidator : AbstractValidator<AddOrUpdateCartItemInput>
    {
        public AddOrUpdateCartItemInputValidator()
        {
            RuleFor(x => x.CartId).NotNull().GreaterThan(0);
            RuleFor(x => x.TripId).NotNull().GreaterThan(0);
            RuleFor(x => x.ScheduleId).NotNull().GreaterThan(0);
            RuleFor(x => x.PackageId).NotNull().GreaterThan(0);
            RuleFor(x => x.Qty).NotNull();
        }
    }
}
