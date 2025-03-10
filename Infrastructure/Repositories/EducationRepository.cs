using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        private readonly DataContext _context;

        public EducationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<EducationEntity>> GetEducationByUserIdAsync(int userId)
        {
            return await _context.Educations
                                 .Where(e => e.UserId == userId)
                                 .ToListAsync();
        }
    }
}
