using NodaTime;
using Sukalibur.Graph.Notifications;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Places
{
    public class Country
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; } = null!;

        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; } = null!;
    }
}
