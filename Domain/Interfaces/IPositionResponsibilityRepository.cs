using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPositionResponsibilityRepository : IRepository<PositionResponsibilityEntity>
    {
        Task<List<PositionResponsibilityEntity>> GetPositionResponsibilitiesByPositionIdAsync(int positionId);
    }
}
