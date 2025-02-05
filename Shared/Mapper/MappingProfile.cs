using AutoMapper;
using NodaTime;
using Sukalibur.Graph.Auth;
using Sukalibur.Graph.Carts;
using Sukalibur.Graph.Orders;
using Sukalibur.Graph.Organizers;
using Sukalibur.Graph.Trips;
using Sukalibur.Graph.Users;

namespace Sukalibur.Shared.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<string, Period>().ConvertUsing<PeriodTypeConverter>();

            CreateMap<SignupInput, User>();

            CreateMap<CreateOrganizerInput, Organizer>();
            CreateMap<UpdateOrganizerInput, Organizer>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateUserInput, User>();
            CreateMap<UpdateUserInput, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateTripInput, Trip>();

            CreateMap<CreateTripItineraryEntry, TripItinerary>();
            CreateMap<CreateTripScheduleEntry, TripSchedule>();
            CreateMap<CreateTripPackageEntry, TripPackage>();
            CreateMap<CreateTripAddonEntry, TripAddon>();

            CreateMap<UpdateTripInput, Trip>()
                .ForMember(m => m.Schedules, opts => opts.Ignore())
                .ForMember(m => m.Packages, opts => opts.Ignore())
                .ForMember(m => m.Addons, opts => opts.Ignore())
                .ForMember(m => m.Itineraries, opts => opts.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateTripItineraryEntry, TripItinerary>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateTripScheduleEntry, TripSchedule>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateTripPackageEntry, TripPackage>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateTripAddonEntry, TripAddon>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateTripCategoryInput, TripCategory>();
            CreateMap<UpdateTripCategoryInput, TripCategory>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CartItem, OrderItem>();
            CreateMap<AddOrUpdateCartItemInput, CartItem>();
            CreateMap<UpdateCartItemInput, CartItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
