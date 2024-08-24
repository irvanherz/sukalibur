using Sukalibur.Graph.Trips;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sukalibur.Graph.Organizers
{
    [ExtendObjectType(typeof(Organizer))]
    public class OrganizerExtendedFieldResolvers
    {
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Trip> GetTrips([Parent] Organizer organizer, [Service] AppDbContext context)
        {
            return context.Trips.Where(t => t.OrganizerId == organizer.Id);
        }

    }
}
