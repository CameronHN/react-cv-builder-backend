using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPositionResponsibilityService
    {
        Task AddPositionResponsibilitynAsync(PositionResponsibilityUpdateDto positionResponsibilityUpdateDto);

        Task<IEnumerable<PositionResponsibilityEntity>> GetAllAsync();

        Task<PositionResponsibilityEntity?> GetPositionResponsibilityByResponsibilityIdAsync(int id);

        Task UpdatePositionResponsibilityAsync(PositionResponsibilityUpdateDto positionResponsibilityUpdateDto);

        Task DeletePositionResponsibilityAsync(int id);

        // Custom methods

        Task DeleteResponsibilityByResponsibilityIdAndPositionIdAsync(int responsibilityId, int positionId);

        Task<List<PositionResponsibilityDto>> GetPositionResponsibilitiesByPositionIdAsync(int positionId);
    }
}
