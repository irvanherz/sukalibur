using Sukalibur.Graph.Organizers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Trips
{
    public class CreateTripInput
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? CategoryId { get; set; } = null;

        public int OrganizerId { get; set; }
    }
}
