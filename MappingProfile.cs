using AutoMapper;
using Sukalibur.Graph.Auth;
using Sukalibur.Graph.Users;

namespace Sukalibur
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<RegisterUserInput, User>();

        }
    }
}
