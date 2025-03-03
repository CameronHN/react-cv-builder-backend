using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition([FromBody] PositionCreateDto positionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _positionService.AddPositionAsync(positionDto);
                return CreatedAtAction(nameof(GetPositionById), new { id = positionDto.UserId }, positionDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpGet("GetPositions")]
        public async Task<ActionResult<IEnumerable<PositionEntity>>> GetAll()
        {
            try
            {
                var positions = await _positionService.GetAllAsync();
                return Ok(positions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"There was a server-side error. Error: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PositionEntity>> GetPositionById(int id)
        {
            try
            {
                var position = await _positionService.GetPositionByIdAsync(id);
                return Ok(position);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Position with the Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error with the server has occured. Error message: {ex.Message}" });
            }
        }


        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPositionsByUserId(int userId)
        {
            try
            {
                var positions = await _positionService.GetPositionsByUserIdAsync(userId);
                return Ok(positions);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, PositionUpdateDto positionDto)
        {
            // The provided Id does not match the PositionId
            if (id != positionDto.Id)
                return BadRequest(new { message = "The provided Id does not match the Position Id" });

            // Update position
            try
            {
                await _positionService.UpdatePositionAsync(positionDto);
                return Ok(new { message = $"Successfully updated position with Id:{id}" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Position with Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"A server-side error occured while updating the position. Error message: {ex.Message}" });
            }
        }

        [HttpDelete("DeletePosition/{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            try
            {
                await _positionService.DeletePositionAsync(id);
                return Ok(new { message = $"Successfully deleted position with Id:{id}" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Position with Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"A server-side error occured while updating the position. Error message: {ex.Message}" });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPositionsByRole([FromQuery] string searchString)
        {
            try
            {
                var positions = await _positionService.SearchPositionsByRoleAsync(searchString);
                return Ok(positions);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while searching for positions.");
            }
        }
    }
}
