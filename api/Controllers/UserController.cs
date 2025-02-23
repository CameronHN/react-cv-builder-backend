using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // =================================
        // ========= CREATE USER ===========
        // =================================

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserEntity user)
        {
            if (user == null)
                return BadRequest("Invalid user data.");

            try
            {
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // =================================
        // ======== GET ALL USERS ==========
        // =================================

        // GET: api/users
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"There was a server-side error. Error: {ex.Message}" });
            }
        }


        // =================================
        // ========== GET USER =============
        // =================================

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"User with the Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error with the server has occured. Error message: {ex.Message}" });
            }
        }


        // =================================
        // ========= UPDATE USER ===========
        // =================================

        // PUT: api/users/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            // The provided Id does not match the UserId
            if (id != userDto.Id)
                return BadRequest(new { message = "The provided Id does not match the User Id" });

            // Update user
            try
            {
                await _userService.UpdateUserAsync(userDto);
                return Ok(new { message = $"Successfully updated user with Id:{id}" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"User with Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"A server-side error occured while updating the user. Error message: {ex.Message}" });
            }
        }


        // =================================
        // ========= DELETE USER ===========
        // =================================

        // DELETE: api/users/5
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new { message = $"Successfully deleted user with Id:{id}" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"User with Id:{id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"A server-side error occured while updating the user. Error message: {ex.Message}" });
            }
        }
    }
}
