using System.ComponentModel.DataAnnotations.Schema;

namespace Sukalibur.Graph.Organizers
{
    public class CreateOrganizerInput
    {
        public string Username { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public OrganizerStatus Status { get; set; } = OrganizerStatus.Active;
    }
}