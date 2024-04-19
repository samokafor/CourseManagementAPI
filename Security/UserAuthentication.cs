using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseManagementAPI.Security
{
    public class UserAuthentication : IUserAuthentication
    {

        private readonly IConfiguration _config;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthentication(IConfiguration config, IPasswordHasher passwordHasher)
        {
            _config = config;
            _passwordHasher = passwordHasher;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Username", user.Username),
                new Claim(ClaimTypes.Role, user.Role)
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

        public User Authenticate(UserLoginDto userLogin)
        {
            var currentUser = DummyUsers.Users.FirstOrDefault(u => u.Email.ToLower() == userLogin.Email && _passwordHasher.DecryptPassword(u.Password) == userLogin.Password);
            if (currentUser == null)
            {
                return null;
            }

            return currentUser;
        }

        
    }
}
