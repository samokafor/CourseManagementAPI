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

        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetAllUsersAsync()
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

        [HttpGet("Search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<Instructor>>> SearchUsersAsync(string searchTerm)
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

        
        [HttpPut("UpdateRole/{Id:int}")]
        public async Task<ActionResult<Instructor>> EditUserRoleAsync(int Id, UpdateUserRoleDto roleDto)
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
                var updatedinstructor = await _userRepository.GetUserById(Id);
                return Ok(updatedinstructor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating role. {ex.Message}");

            }
        }
        
    }
}
