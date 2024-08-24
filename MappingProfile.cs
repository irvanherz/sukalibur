using AutoMapper;
using Sukalibur.Graph.Auth;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;

namespace Sukalibur
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<RegisterUserInput, User>();

            CreateMap<CreateOrganizerInput, Organizer>();
            CreateMap<UpdateOrganizerInput, Organizer>();

            CreateMap<CreateUserInput, User>();
            CreateMap<UpdateUserInput, User>();

            CreateMap<CreateTripInput, Trip>();
            CreateMap<UpdateTripInput, Trip>();

            CreateMap<CreateTripCategoryInput, TripCategory>();
            CreateMap<UpdateTripCategoryInput, TripCategory>();
        }
    }
}
