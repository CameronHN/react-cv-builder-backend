using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PositionResponsibilityService : IPositionResponsibilityService
    {

        private readonly IRepository<PositionResponsibilityEntity> _repository;
        private readonly IPositionResponsibilityRepository _positionResponsibilityRepository;
        private readonly IPositionRepository _positionRepository;

        public PositionResponsibilityService(IRepository<PositionResponsibilityEntity> positionResponsibilityRepository, IPositionResponsibilityRepository positionResponsibilityRepository1, IPositionRepository positionRepository)
        {
            _repository = positionResponsibilityRepository;
            _positionResponsibilityRepository = positionResponsibilityRepository1;
            _positionRepository = positionRepository;
        }

        public async Task AddPositionResponsibilitynAsync(PositionResponsibilityUpdateDto positionResponsibilityUpdateDto)
        {
            if (!positionResponsibilityUpdateDto.PositionId.HasValue)
            {
                throw new ArgumentException("PositionId is required");
            }

            PositionEntity positionExists = await _positionRepository.GetRecordByIdAsync(positionResponsibilityUpdateDto.PositionId.Value) ?? throw new KeyNotFoundException("Position not found");

            PositionResponsibilityEntity positionResponsibility = new PositionResponsibilityEntity
            {
                PositionId = positionResponsibilityUpdateDto.PositionId.Value,
                Responsibility = positionResponsibilityUpdateDto.Responsibility
            };

            await _repository.AddRecordAsync(positionResponsibility);
        }
        public async Task<IEnumerable<PositionResponsibilityEntity>> GetAllAsync()
        {
            return await _repository.GetAllRecordsAsync();
        }

        public async Task<PositionResponsibilityEntity?> GetPositionResponsibilityByResponsibilityIdAsync(int id)
        {
            PositionResponsibilityEntity? positionResponsibility = await _repository.GetRecordByIdAsync(id);
            return positionResponsibility == null ? throw new KeyNotFoundException() : positionResponsibility;
        }

        public async Task UpdatePositionResponsibilityAsync(PositionResponsibilityUpdateDto positionResponsibilityUpdateDto)
        {
            PositionResponsibilityEntity existingReponsibility = await _repository.GetRecordByIdAsync(positionResponsibilityUpdateDto.Id) ?? throw new KeyNotFoundException();

            if (positionResponsibilityUpdateDto.PositionId.HasValue)
            {
                PositionEntity positionExists = await _positionRepository.GetRecordByIdAsync(positionResponsibilityUpdateDto.PositionId.Value) ?? throw new KeyNotFoundException("Invalid PositionId: Position does not exist");
                existingReponsibility.PositionId = positionResponsibilityUpdateDto.PositionId.Value;
            }

            // Update only modified properties
            if (!string.IsNullOrEmpty(positionResponsibilityUpdateDto.Responsibility))
            {
                existingReponsibility.Responsibility = positionResponsibilityUpdateDto.Responsibility;
            }

            try
            {
                await _repository.UpdateRecordAsync(existingReponsibility);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePositionResponsibilityAsync(int id)
        {
            bool deletedPositionResponsibility = await _repository.DeleteRecordAsync(id);
            if (!deletedPositionResponsibility)
            {
                throw new KeyNotFoundException("Position Responsibility not found");
            }
        }

        // Custom methods

        public Task DeleteResponsibilityByResponsibilityIdAndPositionIdAsync(int responsibilityId, int positionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PositionResponsibilityDto>> GetPositionResponsibilitiesByPositionIdAsync(int positionId)
        {
            if (positionId <= 0)
            {
                throw new ArgumentException("Invalid PositionId", nameof(positionId));
            }

            var responsibilities = await _positionResponsibilityRepository.GetPositionResponsibilitiesByPositionIdAsync(positionId);

            return responsibilities.Select(r => new PositionResponsibilityDto
            {
                Responsibility = r.Responsibility
            }).ToList();

        }
    }
}
