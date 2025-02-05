using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Sukalibur.Graph.Organizers
{
    public class OrganizerService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public OrganizerService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Organizer> CreateOrganizerAsync(CreateOrganizerInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var organizer = _mapper.Map<Organizer>(input);
            await context.Organizers.AddAsync(organizer);
            await context.SaveChangesAsync();
            return organizer;
        }

        public async Task<Organizer> UpdateOrganizerAsync(UpdateOrganizerInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var organizer = await context.Organizers.FindAsync(input.Id);
            if (organizer == null)
            {
                throw new Exception("Organizer not found");
            }

            _mapper.Map(input, organizer);
            await context.SaveChangesAsync();
            return organizer;
        }
    }
}
