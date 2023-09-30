using GraphQL.API.Backend.DTOs;
using GraphQL.API.Backend.Interfaces;
using GraphQL.API.Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.API.Backend.Repositories
{
    public class InstructorsRepository : IInstructorsRepository
    {
        private readonly IDbContextFactory<SchoolDbContext> _context;

        public InstructorsRepository(IDbContextFactory<SchoolDbContext> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstructorDTO>> GetAllInstructorsByIDAsync(IReadOnlyList<Guid> keys)
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                return await context.Instructors.Where(i=>keys.Contains(i.Id)).ToListAsync();
            }
        }

        public async Task<InstructorDTO> GetInstructorByIdAsync(Guid id)
        {
            using (SchoolDbContext context = _context.CreateDbContext())
            {
                return await context.Instructors.FirstOrDefaultAsync(c => c.Id == id);
            }
        }
    }
}
