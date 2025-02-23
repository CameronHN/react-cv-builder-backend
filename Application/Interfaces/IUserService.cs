using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {       
        Task AddUserAsync(UserEntity user);

        Task<IEnumerable<UserEntity>> GetAllAsync();

        Task<UserEntity?> GetUserByIdAsync(int id);

        Task UpdateUserAsync(UserUpdateDto userDto);

        Task DeleteUserAsync(int id);

    }
}
