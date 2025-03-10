using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEducationService
    {
        Task<List<EducationEntity>> GetEducationByUserIdAsync(int userId);
    }
}
