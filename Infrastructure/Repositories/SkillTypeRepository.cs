using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SkillTypeRepository : ISkillTypeRepository
    {
        private readonly DataContext _dataContext;

        public SkillTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<SkillTypeEntity> GetSkillTypeWithSkillsAsync(int skillTypeId)
        {
            return await _dataContext.SkillTypes
                .Include(st => st.Skills)
                .FirstOrDefaultAsync(st => st.Id == skillTypeId);
        }
    }
}
