using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
