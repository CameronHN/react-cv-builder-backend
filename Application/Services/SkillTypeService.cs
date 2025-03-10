using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class SkillTypeService : ISkillTypeService
    {
        private readonly ISkillTypeRepository _skillTypeRepository;

        public SkillTypeService(ISkillTypeRepository skillTypeRepository)
        {
            _skillTypeRepository = skillTypeRepository;
        }

        public async Task<SkillTypeEntity> GetSkillTypeWithSkillsAsync(int skillTypeId)
        {
            return await _skillTypeRepository.GetSkillTypeWithSkillsAsync(skillTypeId);
        }
    }
}
