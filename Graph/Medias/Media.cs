using NodaTime;
using Sukalibur.Graph.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Medias
{
    public enum MediaType
    {
        Image,
        Video,
        Document,
    }

    [Table("medias")]
    public class Media
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("type")]
        public MediaType Type { get; set; } = MediaType.Image;

        [Column("subtype")]
        public string? Subtype { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";

        [Column("description")]
        public string? Description { get; set; } = "";

        [Column("data", TypeName = "jsonb")]
        public required MediaData Data { get; set; }

        [Column("user_id")]
        public int UserId { get; set; } = 0;

        [Column("organizer_id")]
        public int OrganizerId { get; set; } = 0;

        [Column("created_at", TypeName = "timestamptz")]
        public Instant CreatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();

        [Column("updated_at", TypeName = "timestamptz")]
        public Instant UpdatedAt { get; set; } = SystemClock.Instance.GetCurrentInstant();
    }

    [UnionType("MediaData")]
    [JsonPolymorphic]
    [JsonDerivedType(typeof(ImageMediaData), "image")]
    [JsonDerivedType(typeof(VideoMediaData), "video")]
    [JsonDerivedType(typeof(DocumentMediaData), "document")]
    public class MediaData
    {
        public required string OriginalName { get; set; }
        public string? OriginalMime { get; set; }
    }

    [JsonDerivedType(typeof(ImageMediaData), "image")]
    public class ImageMediaData : MediaData
    {
        public List<Item> Sizes { get; set; } = new();

        public class Item
        {
            public required string Id { get; set; }
            public required string Url { get; set; }
            public required string FileName { get; set; }
            public required long FileSize { get; set; }
            public required int Width { get; set; }
            public required int Height { get; set; }
        }

    }

    [JsonDerivedType(typeof(ImageMediaData), "video")]
    public class VideoMediaData : MediaData
    {
        public required string Url { get; set; }
        public required string FileName { get; set; }
        public required long FileSize { get; set; }
    }

    [JsonDerivedType(typeof(ImageMediaData), "document")]
    public class DocumentMediaData : MediaData
    {
        public required string Url { get; set; }
        public required string FileName { get; set; }
        public required long FileSize { get; set; }
    }
}