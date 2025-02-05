using Newtonsoft.Json;
using NodaTime;
using NpgsqlTypes;
using Sukalibur.Graph.Medias;
using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Trips
{
    public enum TripStatus
    {
        Active,
        Inactive,
        Blocked
    }


    [Table("trips")]
    public class Trip
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("organizer_id")]
        public required int OrganizerId { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public required string Name { get; set; }

        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; } = string.Empty;

        [Column("includes", TypeName = "jsonb")]
        public List<string> Includes { get; set; } = [];

        [Column("excludes", TypeName = "jsonb")]
        public List<string> Excludes { get; set; } = [];

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("status")]
        public TripStatus Status { get; set; } = TripStatus.Inactive;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("search_vector")]
        [GraphQLIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public NpgsqlTsVector SearchVector { get; set; } = null!;

        public TripCategory? Category { get; set; } = null;
        public Organizer Organizer { get; set; } = null!;
        [GraphQLIgnore]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public TripFeature Feature { get; set; } = null!;
        public ICollection<TripItinerary> Itineraries { get; set; } = [];
        public ICollection<TripSchedule> Schedules { get; set; } = [];
        public ICollection<TripPackage> Packages { get; set; } = [];
        public ICollection<TripAddon> Addons { get; set; } = [];
        public ICollection<TripReservation> Reservations { get; set; } = [];
    }
}
