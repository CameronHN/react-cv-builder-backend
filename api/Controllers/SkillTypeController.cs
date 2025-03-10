using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillTypeController : ControllerBase
    {
        private readonly ISkillTypeService _skillTypeService;

        public SkillTypeController(ISkillTypeService skillTypeService)
        {
            _skillTypeService = skillTypeService;
        }

        [HttpGet("{skillTypeId}/skills")]
        public async Task<ActionResult<SkillTypeEntity>> GetSkillTypeWithSkills(int skillTypeId)
        {
            var skillType = await _skillTypeService.GetSkillTypeWithSkillsAsync(skillTypeId);
            if (skillType == null)
            {
                return NotFound();
            }
            return Ok(skillType);
        }
    }
}
