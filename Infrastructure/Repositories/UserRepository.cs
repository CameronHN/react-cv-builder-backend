using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

    }
}
