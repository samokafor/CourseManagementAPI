using AutoMapper;
using CourseManagementAPI.Database;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public UserRepository(CourseDbContext context, IPasswordHasher passwordHasher, IMapper mapper) 
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            if (userDto == null) return null;

            var newUser = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                Role = userDto.Role,
                Password = _passwordHasher.EncryptPassword(userDto.Password)
            };
            var newUserLogin = new UserLogin()
            {
                Email = newUser.Email,
                Password = newUser.Password
            };
            var newUserDto = _mapper.Map<UserDto>(newUser);

            await _context.Users.AddAsync(newUser);
            await _context.UserLogins.AddAsync(newUserLogin);
            await _context.SaveChangesAsync();
            return newUserDto;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null) return null;
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        
        public async Task<UserDto> GetUserById(int id)
        {
            var user =await _context.Users.Include(u => u.UserLogin).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
