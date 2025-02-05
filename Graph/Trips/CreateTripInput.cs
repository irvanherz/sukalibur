using FluentValidation;
using NetTopologySuite.Geometries;
using NodaTime;
using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Trips
{
    public enum CreateTripEntryAction
    {
        Create,
        Update,
        Delete
    }

    public class CreateTripInput
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? CategoryId { get; set; } = null;
        public int? OrganizerId { get; set; }
        [DefaultValueSyntax("[]")]
        public List<CreateTripItineraryEntry> Itineraries { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<CreateTripScheduleEntry> Schedules { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<CreateTripPackageEntry> Packages { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<CreateTripAddonEntry> Addons { get; set; } = [];

    }

    public class CreateTripInputValidator : AbstractValidator<CreateTripInput>
    {
        public CreateTripInputValidator()
        {
            ;
            RuleFor(x => x.Name).NotEmpty().Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }

    public class CreateTripItineraryEntry
    {
        public required string Name { get; set; } = "";
        [DefaultValue("")]
        public required string Description { get; set; } = "";
        [DefaultValue(false)]
        public bool IsMeetingPoint { get; set; } = false;
        public Point? Location { get; set; }
        [DefaultValue("")]
        public string Address { get; set; } = "";
        [DefaultValue(0)]
        public int Duration { get; set; } = 0;
    }

    public class CreateTripScheduleEntry
    {
        public Instant ScheduledAt { get; set; }
    }

    public class CreateTripPackageEntry
    {
        public required string Name { get; set; }
        [DefaultValue("")]
        public string Description { get; set; } = string.Empty;
        [DefaultValue(0)]
        public double Price { get; set; } = 0;
    }

    public class CreateTripAddonEntry
    {
        public required string Name { get; set; }
        [DefaultValue("")]
        public string Description { get; set; } = string.Empty;
        [DefaultValue(0)]
        public double Price { get; set; } = 0;
    }

    public class CreateTripItineraryEntryValidator : AbstractValidator<CreateTripItineraryEntry>
    {
        public CreateTripItineraryEntryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 255);
            RuleFor(x => x.Description).Length(0, 255);
            RuleFor(x => x.Address).Length(0, 255);
            RuleFor(x => x.Duration).NotNull();
        }
    }
}
