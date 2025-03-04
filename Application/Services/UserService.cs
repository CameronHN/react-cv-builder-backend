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

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _userRepository.GetAllRecordsAsync();
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            UserEntity? user = await _userRepository.GetRecordByIdAsync(id);
            return user == null ? throw new KeyNotFoundException() : user;
        }

        public async Task UpdateUserAsync(UserUpdateDto userDto)
        {
            UserEntity existingUser = await _userRepository.GetRecordByIdAsync(userDto.Id) ?? throw new KeyNotFoundException();

            // Update only modified properties
            if (!string.IsNullOrEmpty(userDto.FirstName))
            {
                existingUser.FirstName = userDto.FirstName;
            }

            if (!string.IsNullOrEmpty(userDto.MiddleName))
            {
                existingUser.MiddleName = userDto.MiddleName;
            }

            if (!string.IsNullOrEmpty(userDto.LastName))
            {
                existingUser.LastName = userDto.LastName;
            }

            if (!string.IsNullOrEmpty(userDto.Email))
            {
                existingUser.Email = userDto.Email;
            }

            if (!string.IsNullOrEmpty(userDto.PhoneNumber))
            {
                existingUser.PhoneNumber = userDto.PhoneNumber;
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
