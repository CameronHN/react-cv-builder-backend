using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PositionRepository : Repository<PositionEntity>, IPositionRepository
    {
        private readonly DataContext _dataContext;

        public PositionRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PositionEntity>> GetPositionsByUserIdAsync(int userId)
        {
            try
            {
                return await _dataContext.Positions
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PositionEntity>> SearchPositionsByRoleAsync(string searchString)
        {
            try
            {
                return await _dataContext.Positions
                    .Where(p => p.Role.Contains(searchString))
                    .Include(p => p.Responsibilities)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
