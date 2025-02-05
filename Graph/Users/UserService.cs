using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Users
{
    public class UserService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public UserService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(CreateUserInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var user = _mapper.Map<User>(input);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(UpdateUserInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var user = await context.Users.FindAsync(input.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            _mapper.Map(input, user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
