namespace Sukalibur.Graph.Organizers
{
    public class UpdateOrganizerInput
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public OrganizerStatus? Status { get; set; }
    }
}
