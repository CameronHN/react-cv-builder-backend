using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PositionRepository : Repository<PositionEntity>, IPositionRepository
    {
        private readonly DataContext _dataContext;

        public PositionRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<List<PositionEntity>> GetPositionsByUserIdAsync(int userId)
        {
            try
            {
                return _dataContext.Positions
                    .Where(p => p.UserId == userId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
