using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PositionResponsibilityRepository : Repository<PositionResponsibilityEntity>, IPositionResponsibilityRepository
    {

        private readonly DataContext _dataContext;

        public PositionResponsibilityRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<PositionResponsibilityEntity>> GetPositionResponsibilitiesByPositionIdAsync(int positionId)
        {

            try
            {
                return await _dataContext.PositionResponsibilities
                    .Where(p => p.PositionId == positionId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
