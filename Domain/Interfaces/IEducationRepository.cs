using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEducationRepository
    {
        Task<List<EducationEntity>> GetEducationByUserIdAsync(int userId);
    }
}
