using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserSkillService
    {
        Task<List<UserSkillEntity>> GetUserSkillsByUserIdAsync(int userId);

        Task<string> GetSkillNameBySkillIdAsync(int skillId);

        Task<List<UserSkillEntity>> GetUserSkillsWithSkillTypeByUserIdAsync(int userId);

    }
}
