using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillController : ControllerBase
    {
        private readonly IUserSkillService _userSkillService;

        public UserSkillController(IUserSkillService userSkillService)
        {
            _userSkillService = userSkillService;
        }

        [HttpGet("{userId}/skills")]
        public async Task<ActionResult<List<string>>> GetSkillsByUserId(int userId)
        {
            try
            {
                // Get UserSkill records for the user
                var userSkills = await _userSkillService.GetUserSkillsByUserIdAsync(userId);
                if (userSkills == null || userSkills.Count == 0)
                {
                    return NotFound($"No skills found for user with ID: {userId}.");
                }

                // Extract skill names from UserSkill records
                var skillNames = userSkills.Select(us => us.Skill.SkillName).ToList();

                return Ok(skillNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("skill/{skillId}/name")]
        public async Task<ActionResult<string>> GetSkillNameBySkillId(int skillId)
        {
            var skillName = await _userSkillService.GetSkillNameBySkillIdAsync(skillId);
            if (string.IsNullOrEmpty(skillName))
            {
                return NotFound();
            }
            return Ok(skillName);
        }

        [HttpGet("{userId}/skills-with-type")]
        public async Task<ActionResult<object>> GetUserSkillsWithSkillTypeByUserId(int userId)
        {
            var userSkills = await _userSkillService.GetUserSkillsWithSkillTypeByUserIdAsync(userId);
            if (userSkills == null || userSkills.Count == 0)
            {
                return NotFound($"No skills found for user with ID: {userId}.");
            }

            var result = new
            {
                userId,
                skills = userSkills.Select(us => new
                {
                    skillId = us.Skill.Id,
                    skillName = us.Skill.SkillName,
                    skillType = new
                    {
                        skillTypeId = us.Skill.SkillType.Id,
                        skillTypeName = us.Skill.SkillType.SkillTypeName
                    }
                }).ToList()
            };

            return Ok(result);
        }
    }
}
