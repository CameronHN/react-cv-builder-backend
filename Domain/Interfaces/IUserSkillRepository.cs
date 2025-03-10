using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserSkillRepository
    {
        Task<List<UserSkillEntity>> GetUserSkillsByUserIdAsync(int userId);

        Task<string> GetSkillNameBySkillIdAsync(int skillId);

        Task<List<UserSkillEntity>> GetUserSkillsWithSkillTypeByUserIdAsync(int userId);

    }
}
