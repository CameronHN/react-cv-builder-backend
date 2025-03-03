using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPositionService
    {
        Task AddPositionAsync(PositionCreateDto positionCreateDto);

        Task<IEnumerable<PositionEntity>> GetAllAsync();

        Task<PositionEntity?> GetPositionByIdAsync(int id);

        Task UpdatePositionAsync(PositionUpdateDto positionUpdateDto);

        Task DeletePositionAsync(int id);

        Task<List<PositionDto>> GetPositionsByUserIdAsync(int userId);

        Task<List<PositionAndResponsibilitiesDto>> SearchPositionsByRoleAsync(string searchString);

    }
}
