using Domain.Entities;

namespace Application.Interfaces
{
    public interface ISkillTypeService
    {
        Task<SkillTypeEntity> GetSkillTypeWithSkillsAsync(int skillTypeId);
    }
}
