using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionResponsibilityController : ControllerBase
    {
        private readonly IPositionResponsibilityService _positionResponsibilityService;

        public PositionResponsibilityController(IPositionResponsibilityService positionResponsibilityService)
        {
            _positionResponsibilityService = positionResponsibilityService;
        }

        [HttpGet("position/{positionId}")]
        public async Task<IActionResult> GetPositionResponsibilities(int positionId)
        {
            if (positionId <= 0)
            {
                return BadRequest("Invalid PositionId.");
            }

            try
            {
                var responsibilities = await _positionResponsibilityService.GetPositionResponsibilitiesByPositionIdAsync(positionId);

                if (responsibilities == null || responsibilities.Count == 0)
                {
                    return NotFound($"No responsibilities found for PositionId: {positionId}.");
                }

                return Ok(responsibilities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
