using CourseManagementAPI.Controllers;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserAuthentication _userAuth;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public HomeController(IUserAuthentication userAuth, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _userAuth = userAuth;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            var user = _userAuth.Authenticate(userLogin);

            if (user == null)
            {
                return NotFound("Invalid email or password!");
            }

            var token = _userAuth.GenerateToken(user);
            return Ok(token);
        }


        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<ActionResult<UserDto>> SignUp([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest();
                }

                var existingUser =  _userRepository.GetUserByEmail(userDto.Email);
                if(existingUser != null)
                {
                    ModelState.AddModelError("Email", $"User with email - {userDto.Email} already exists!");
                    return BadRequest(ModelState);
                }

                var newUser = await _userRepository.AddUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { Id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! an error occured! {ex.Message}");
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<UserDto>> GetUserById(int Id)
        {
            try
            {
                var user = await _userRepository.GetUserById(Id);
                if (user == null)
                {
                    return BadRequest("User not found");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error getting user. {ex.Message}");

            }
        }
    }
}
