using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUserAuthentication _userAuth;
        private readonly IUserRepository _userRepository;

        public HomeController(IUserAuthentication userAuth, IUserRepository userRepository)
        {
            _userAuth = userAuth;
            
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            if(_userRepository.CheckIfLoggedIn() == true)
            {
                return BadRequest("You have to Sign Out first.");
            }
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
        public async Task<ActionResult<User>> SignUp([FromBody] UserDto userDto)
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
                if (!_userAuth.IsValidPassword(userDto.Password))
                {
                    ModelState.AddModelError("Password", $"Password does not meet reqirements; {_userAuth.PasswordRequiremnts()}");
                    return BadRequest(ModelState);
                }

                var newUser = await _userRepository.AddRegularUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! an error occured! {ex.Message}");
            }
        }
        
        
        [AllowAnonymous]
        [HttpPost("Admin/SignUp")]
        public async Task<ActionResult<User>> SignUpAsAdmin([FromBody] UserDto userDto)
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
                if (!_userAuth.IsValidPassword(userDto.Password))
                {
                    ModelState.AddModelError("Password", $"Password does not meet reqirements; {_userAuth.PasswordRequiremnts()}");
                    return BadRequest(ModelState);
                }

                var newUser = await _userRepository.AddAdminUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Oops! an error occured! {ex.Message}");
            }
        }

        [HttpPost("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                if (_userRepository.CheckIfLoggedIn() == false)
                {
                    return BadRequest("You are not signed in.");
                }
                var expiredToken = _userAuth.GenerateExpiredToken(_userRepository.GetCurentUser());

                return Ok(new { token = expiredToken });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error signing out. {ex.Message}");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("User/{Id:int}")]
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
    }
}
