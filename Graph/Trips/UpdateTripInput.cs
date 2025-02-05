using FluentValidation;
using NetTopologySuite.Geometries;
using NodaTime;

namespace Sukalibur.Graph.Trips
{
    public enum UpdateTripEntryAction
    {
        Create,
        Update,
        Delete
    }
    public class UpdateTripInput
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        [DefaultValueSyntax("[]")]
        public List<UpdateTripItineraryEntry> Itineraries { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<UpdateTripScheduleEntry> Schedules { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<UpdateTripPackageEntry> Packages { get; set; } = [];
        [DefaultValueSyntax("[]")]
        public List<UpdateTripAddonEntry> Addons { get; set; } = [];
    }

    public class UpdateTripInputValidator : AbstractValidator<UpdateTripInput>
    {
        public UpdateTripInputValidator()
        {
            RuleFor(x => x.Name).Length(3, 255);
            RuleFor(x => x.Description).Length(0, 255);
        }
    }

    public class UpdateTripItineraryEntry
    {
        public UpdateTripEntryAction Action { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsMeetingPoint { get; set; }
        public Point? Location { get; set; }
        public string? Address { get; set; }
        public int? Duration { get; set; }
    }

    public class UpdateTripScheduleEntry
    {
        public UpdateTripEntryAction Action { get; set; }
        public int? Id { get; set; }
        public Instant? ScheduledAt { get; set; }
    }

    public class UpdateTripPackageEntry
    {
        public UpdateTripEntryAction Action { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
    }

    public class UpdateTripAddonEntry
    {
        public UpdateTripEntryAction Action { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
    }
}
