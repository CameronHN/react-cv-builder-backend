using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<EducationEntity>>> GetEducationByUserId(int userId)
        {
            var educationList = await _educationService.GetEducationByUserIdAsync(userId);
            if (educationList == null || educationList.Count == 0)
            {
                return NotFound($"No education records found for user with ID: {userId}.");
            }
            return Ok(educationList);
        }
    }
}
