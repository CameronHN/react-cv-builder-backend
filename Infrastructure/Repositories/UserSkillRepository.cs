using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserSkillRepository : IUserSkillRepository
    {
        private readonly DataContext _dataContext;

        public UserSkillRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UserSkillEntity>> GetUserSkillsByUserIdAsync(int userId)
        {
            return await _dataContext.UserSkills
                                 .Include(us => us.Skill)
                                 .Where(us => us.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<string> GetSkillNameBySkillIdAsync(int skillId)
        {
            var skill = await _dataContext.Skills
                .Where(s => s.Id == skillId)
                .Select(s => s.SkillName)
                .FirstOrDefaultAsync();

            return skill;
        }

        public async Task<List<UserSkillEntity>> GetUserSkillsWithSkillTypeByUserIdAsync(int userId)
        {
            return await _dataContext.UserSkills
                .Where(us => us.UserId == userId)
                .Include(us => us.Skill)
                .ThenInclude(s => s.SkillType)
                .ToListAsync();
        }
    }
}
