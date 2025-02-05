using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;
using System.Security.Claims;

namespace Sukalibur.Graph.Trips
{
    public class TripItineraryService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public TripItineraryService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        //public async Task<TripItinerary> CreateTripItineraryAsync(CreateTripItineraryInput input, ClaimsPrincipal claimsPrincipal)
        //{
        //    using var context = _contextFactory.CreateDbContext();

        //    var trip = await context.Trips.FindAsync(input.TripId);
        //    if (trip == null)
        //    {
        //        throw new Exception("Trip not found");
        //    }

        //    var nameIdentifier = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";
        //    var userId = Convert.ToInt32(nameIdentifier);

        //    var organizerMember = await context.OrganizerMembers.FirstOrDefaultAsync(m => m.OrganizerId == trip.OrganizerId && m.UserId == userId);
        //    if (organizerMember == null)
        //    {
        //        throw new Exception("You have no permission");
        //    }
        //    var allowedRoles = new List<OrganizerMemberRole> { OrganizerMemberRole.Super, OrganizerMemberRole.Admin };
        //    if (!allowedRoles.Contains(organizerMember.Role))
        //    {
        //        throw new Exception("You have no permission");
        //    }

        //    var itinerary = _mapper.Map<TripItinerary>(input);
        //    await context.TripItineraries.AddAsync(itinerary);
        //    await context.SaveChangesAsync();
        //    return itinerary;
        //}

        //public async Task<TripItinerary> UpdateTripItineraryAsync(UpdateTripItineraryInput input, ClaimsPrincipal claimsPrincipal)
        //{
        //    using var context = _contextFactory.CreateDbContext();

        //    var itinerary = await context.TripItineraries.FindAsync(input.Id);
        //    if (itinerary == null)
        //    {
        //        throw new Exception("Trip itinerary not found");
        //    }

        //    var trip = await context.Trips.FindAsync(itinerary.TripId);
        //    if (trip == null)
        //    {
        //        throw new Exception("Trip not found");
        //    }

        //    var nameIdentifier = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0";
        //    var userId = Convert.ToInt32(nameIdentifier);

        //    var organizerMember = await context.OrganizerMembers.FirstOrDefaultAsync(m => m.OrganizerId == trip.OrganizerId && m.UserId == userId);
        //    if (organizerMember == null)
        //    {
        //        throw new Exception("You have no permission");
        //    }
        //    var allowedRoles = new List<OrganizerMemberRole> { OrganizerMemberRole.Super, OrganizerMemberRole.Admin };
        //    if (!allowedRoles.Contains(organizerMember.Role))
        //    {
        //        throw new Exception("You have no permission");
        //    }

        //    _mapper.Map(input, itinerary);
        //    await context.SaveChangesAsync();
        //    return itinerary;
        //}
    }
}
