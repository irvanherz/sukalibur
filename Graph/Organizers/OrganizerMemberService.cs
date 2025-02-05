using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Organizers;

namespace Sukalibur.Graph.Organizers
{
    public class OrganizerMemberService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public OrganizerMemberService(IDbContextFactory<AppDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<OrganizerMember> CreateOrganizerMemberAsync(CreateOrganizerMemberInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var organizerMember = _mapper.Map<OrganizerMember>(input);
            await context.OrganizerMembers.AddAsync(organizerMember);
            await context.SaveChangesAsync();
            return organizerMember;
        }

        public async Task<OrganizerMember> UpdateOrganizerMemberAsync(UpdateOrganizerMemberInput input)
        {
            using var context = _contextFactory.CreateDbContext();
            var organizerMember = await context.OrganizerMembers.FindAsync(input.Id);
            if (organizerMember == null)
            {
                throw new Exception("OrganizerMember not found");
            }

            _mapper.Map(input, organizerMember);
            await context.SaveChangesAsync();
            return organizerMember;
        }
    }
}
