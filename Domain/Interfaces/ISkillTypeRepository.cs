using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISkillTypeRepository
    {
        Task<SkillTypeEntity> GetSkillTypeWithSkillsAsync(int skillTypeId);
    }
}
