using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Medias
{
    public class CreateMediaInput
    {
        public MediaType Type { get; set; } = MediaType.Image;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public int? OrganizerId { get; set; }
        [GraphQLType(typeof(NonNullType<UploadType>))]
        public IFile File { get; set; } = null!;
    }

    public class CreateMediaInputValidator : AbstractValidator<CreateMediaInput>
    {
        public CreateMediaInputValidator()
        {
            RuleFor(x => x.Name).Length(1, 255).When(x => string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.Description).Length(1, 255).When(x => string.IsNullOrEmpty(x.Name));
            RuleFor(x => x.UserId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.OrganizerId).GreaterThanOrEqualTo(1);
        }
    }
}