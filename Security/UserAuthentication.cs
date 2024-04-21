﻿using CourseManagementAPI.Database.Models;
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
        private readonly IHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserAuthentication(IConfiguration config, IHasher passwordHasher, IUserRepository userRepository)
        {
            _config = config;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public string GenerateToken(User user)
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
            var currentUserLogin = _userRepository.GetUserLogin(userLogin.Email);
            
            if (currentUserLogin != null)
            {
                if (!_passwordHasher.VerifyPassword(userLogin.Password, currentUserLogin.Password))
                {
                    return null;
                }
            }

            
            return _userRepository.GetUserByEmail(currentUserLogin.Email);
        }

        public bool IsValidPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$";

            return Regex.IsMatch(password, pattern);
        }

        public string PasswordRequiremnts()
        {
            string requirements = @"Length greater than or equal to 8, Includes uppercase letters, Includes lowercase letters, Includes numbers, Includes special characters";
            return requirements;
        }
    }
}
