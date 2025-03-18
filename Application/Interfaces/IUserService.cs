using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserEntity user);

        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<UserDto> GetUserByIdAsync(int id);

        Task UpdateUserAsync(UserUpdateDto userDto);

        Task DeleteUserAsync(int id);

    }
}
