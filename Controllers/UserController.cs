using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<User>> GetUserById(int Id)
        {
            try
            {
                var user = await _userRepository.GetUserById(Id);
                if (user == null)
                {
                    return BadRequest($"User with ID - {Id} does not exist!");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error getting user. {ex.Message}");

            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                var instructor = await _userRepository.GetAllUsers();
                if (instructor == null)
                {
                    return StatusCode(StatusCodes.Status302Found, "No users have been created yet");
                }
                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error fetching data from database. {ex.Message}");
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpGet("Search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<User>>> SearchUsersAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    return BadRequest();
                }

                var result = await _userRepository.SearchUsers(searchTerm);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! {ex.Message}");

            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("UpdateRole/{Id:int}")]
        public async Task<ActionResult<User>> EditUserRoleAsync(int Id, UpdateUserRoleDto roleDto)
        {
            try
            {
                if (roleDto == null) return BadRequest();
                var userToUpdate = await _userRepository.GetUserById(Id);
                if (userToUpdate == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"user with ID - {Id} does not exist");
                }
                await _userRepository.UpdateUserRole(Id, roleDto);
                var updatedUser = await _userRepository.GetUserById(Id);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating role. {ex.Message}");

            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("Delete/{Id:int}")]
        public async Task<ActionResult> DeleteUserAsync(int Id)
        {
            try
            {
                var userToDelete = await _userRepository.GetUserById(Id);
                if (userToDelete == null)
                {
                    return StatusCode(StatusCodes.Status302Found, $"Instructor with ID - {Id} does not exist");

                }
                await _userRepository.DeleteUser(Id);
                return Ok($"{userToDelete.FirstName + " " + userToDelete.LastName} has been deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting user from database. {ex.Message}");

            }
        }

    }
}
