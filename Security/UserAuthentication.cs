using AutoMapper;
using CourseManagementAPI.Database;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace CourseManagementAPI.Security
{
    public class UserAuthentication : IUserAuthentication
    {

        private readonly IConfiguration _config;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserAuthentication(IConfiguration config, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _config = config;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public string GenerateToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.Gender, user.Gender),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("DateCreated", user.DateCreated.ToString()),
                new Claim("DateModified", user?.DateModified.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserDto Authenticate(UserLoginDto userLogin)
        {
            var currentUser = _userRepository.GetUserByEmail(userLogin.Email);
            
            if (currentUser != null)
            {
                if (!_passwordHasher.VerifyPassword(userLogin.Password, currentUser.Password))
                {
                    return null;
                }
            }

            
            return currentUser;
        }

        public bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }

        public string PasswordRequiremnts()
        {
            var requirementsString = "";

            List<string> requirements = new List<string>
                {
                    "Length greater than or equal to 8",
                    "Includes uppercase letters",
                    "Includes lowercase letters",
                    "Includes numbers",
                    "Includes special characters"
                };

            foreach( var requirement in requirements)
            {
                requirementsString += requirement +" ";
            }

            return requirementsString;
        }
    }
}
