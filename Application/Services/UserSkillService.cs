using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserSkillService : IUserSkillService
    {
        private IUserSkillRepository _userSkillRepository;

        public UserSkillService(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }

        public async Task<List<UserSkillEntity>> GetUserSkillsByUserIdAsync(int userId)
        {

            return await _userSkillRepository.GetUserSkillsByUserIdAsync(userId);
        }

        public async Task<string> GetSkillNameBySkillIdAsync(int skillId)
        {
            return await _userSkillRepository.GetSkillNameBySkillIdAsync(skillId);
        }

        public async Task<List<UserSkillEntity>> GetUserSkillsWithSkillTypeByUserIdAsync(int userId)
        {
            return await _userSkillRepository.GetUserSkillsWithSkillTypeByUserIdAsync(userId);
        }
    }
}
