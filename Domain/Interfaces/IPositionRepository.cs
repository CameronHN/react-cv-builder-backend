using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPositionRepository : IRepository<PositionEntity>
    {
        Task<List<PositionEntity>> GetPositionsByUserIdAsync(int userId);
        Task<List<PositionEntity>> SearchPositionsByRoleAsync(string searchString);
    }
}
