using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PositionService : IPositionService
    {

        private readonly IRepository<PositionEntity> _repository;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IPositionRepository _positionRepository;

        public PositionService(IRepository<PositionEntity> positionRepository, IUserRepository userRepository, IPositionRepository positionRepository1)
        {
            _repository = positionRepository;
            _userRepository = userRepository;
            _positionRepository = positionRepository1;
        }

        public async Task AddPositionAsync(PositionCreateDto positionDto)
        {
            UserEntity userExists = await _userRepository.GetRecordByIdAsync(positionDto.UserId) ?? throw new KeyNotFoundException("User not found");

            PositionEntity position = new PositionEntity
            {
                Role = positionDto.Role,
                StartDate = positionDto.StartDate,
                EndDate = positionDto.EndDate,
                UserId = positionDto.UserId
            };

            await _repository.AddRecordAsync(position);
        }

        public async Task<IEnumerable<PositionEntity>> GetAllAsync()
        {
            return await _repository.GetAllRecordsAsync();
        }

        public async Task<PositionEntity?> GetPositionByIdAsync(int id)
        {
            PositionEntity? position = await _repository.GetRecordByIdAsync(id);
            return position == null ? throw new KeyNotFoundException() : position;
        }

        public async Task UpdatePositionAsync(PositionUpdateDto positionDto)
        {
            PositionEntity existingPosition = await _repository.GetRecordByIdAsync(positionDto.Id) ?? throw new KeyNotFoundException();

            if (positionDto.UserId.HasValue)
            {
                UserEntity userExists = await _userRepository.GetRecordByIdAsync(positionDto.UserId.Value) ?? throw new KeyNotFoundException("Invalid UserId: User does not exist");
                existingPosition.UserId = positionDto.UserId.Value;
            }

            // Update only modified properties
            if (!string.IsNullOrEmpty(positionDto.Role))
            {
                existingPosition.Role = positionDto.Role;
            }

            if (!string.IsNullOrEmpty(positionDto.StartDate))
            {
                existingPosition.StartDate = positionDto.StartDate;
            }

            if (!string.IsNullOrEmpty(positionDto.EndDate))
            {
                existingPosition.EndDate = positionDto.EndDate;
            }

            try
            {
                await _repository.UpdateRecordAsync(existingPosition);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePositionAsync(int id)
        {
            bool deletedPosition = await _repository.DeleteRecordAsync(id);
            if (!deletedPosition)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<List<PositionDto>> GetPositionsByUserIdAsync(int userId)
        {
            UserEntity existingUser = await _userRepository.GetRecordByIdAsync(userId) ?? throw new KeyNotFoundException("Invalid UserId: User does not exist");
            try
            {
                var positions = await _positionRepository.GetPositionsByUserIdAsync(userId);

                // Convert Position to PositionDto
                return positions.Select(p => new PositionDto
                {
                    Role = p.Role,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
