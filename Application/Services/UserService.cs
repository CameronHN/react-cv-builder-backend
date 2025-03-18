using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public UserService(IRepository<UserEntity> repository)
        {
            _userRepository = repository;
        }

        public async Task AddUserAsync(UserEntity user)
        {
            await _userRepository.AddRecordAsync(user);
        }

        private UserDto mapToUserDto(UserEntity user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = string.IsNullOrWhiteSpace(user.PhoneNumber.Trim()) ? "None" : user.PhoneNumber
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllRecordsAsync();
            return users.Select(mapToUserDto);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetRecordByIdAsync(id) ?? throw new KeyNotFoundException();
            return mapToUserDto(user);
        }

        public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            UserEntity existingUser = await _userRepository.GetRecordByIdAsync(userUpdateDto.Id) ?? throw new KeyNotFoundException();

            // Update only modified properties
            if (!string.IsNullOrEmpty(userUpdateDto.FirstName))
            {
                existingUser.FirstName = userUpdateDto.FirstName;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.MiddleName))
            {
                existingUser.MiddleName = userUpdateDto.MiddleName;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.LastName))
            {
                existingUser.LastName = userUpdateDto.LastName;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.Email))
            {
                existingUser.Email = userUpdateDto.Email;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.PhoneNumber))
            {
                existingUser.PhoneNumber = userUpdateDto.PhoneNumber;
            }

            try
            {
                await _userRepository.UpdateRecordAsync(existingUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            bool deletedUser = await _userRepository.DeleteRecordAsync(id);
            if (!deletedUser)
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
