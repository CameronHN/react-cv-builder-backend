using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<List<EducationEntity>> GetEducationByUserIdAsync(int userId)
        {
            return await _educationRepository.GetEducationByUserIdAsync(userId);
        }
    }
}
