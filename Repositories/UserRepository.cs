using CourseManagementAPI.Database;
using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;
using CourseManagementAPI.Repositories.Interfaces;
using CourseManagementAPI.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace CourseManagementAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private TextInfo convert = CultureInfo.CurrentCulture.TextInfo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(
            CourseDbContext context, 
            IPasswordHasher passwordHasher,
            IHttpContextAccessor httpContextAccessor
            ) 
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> AddUserAsync(UserDto userDto)
        {
            if (userDto == null) return null;
            

            var newUser = new User()
            {
                FirstName = convert.ToTitleCase(userDto.FirstName.ToLower()),
                LastName = convert.ToTitleCase(userDto.LastName.ToLower()),
                Gender = convert.ToTitleCase(userDto.Gender.ToLower()),
                Email = userDto.Email.ToLower(),
                Username = userDto.Username.ToLower(),
                Role = convert.ToTitleCase(userDto.Role.ToLower()),
                Password = _passwordHasher.EncryptPassword(userDto.Password)
            };
            var newUserLogin = new UserLogin()
            {
                Email = newUser.Email,
                Password = newUser.Password
            };
            
            await _context.Users.AddAsync(newUser);
            await _context.UserLogins.AddAsync(newUserLogin);
            await _context.SaveChangesAsync();
            var addedUser = GetUserByEmail(userDto.Email);

            return addedUser;
        }

        public async Task DeleteUser(int Id)
        {
            var user = await GetUserById(Id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var user = await _context.Users.Include(u => u.UserLogin).ToListAsync();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null) return null;
            return user;
        }
        
        public async Task<User> GetUserById(int id)
        {
            var user =await _context.Users.Include(u => u.UserLogin).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return null;
            return user;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _context.Users.Include(u => u.UserLogin).FirstOrDefaultAsync(u => u.Username == userName);

            if (user == null) return null;
            
            return user;
        }

        public async Task<IEnumerable<User>> SearchUsers(string searchTerm)
        {
            IQueryable<User> query = _context.Users;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(u => u.FirstName.Contains(searchTerm) || u.LastName.Contains(searchTerm) || u.Username.Contains(searchTerm) || u.Email.Contains(searchTerm));
                return await query.ToListAsync();
            }
            throw new Exception("Search term cannot be empty.");
        }

       

        public async Task<User> UpdateUserRole(int Id, UpdateUserRoleDto roleDto)
        {
            if (roleDto != null)
            {
                var user = await GetUserById(Id);
                user.Role = convert.ToTitleCase(roleDto.Role.ToLower());
                user.DateModified = DateTime.Now;

                await _context.SaveChangesAsync();
                return await GetUserById(Id);
            }
            return null;
        }

        public User GetCurentUser()
        {
            ClaimsPrincipal user = _httpContextAccessor.HttpContext.User;
            if (user.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return new User()
                {
                    Id = int.Parse(userClaims.FirstOrDefault(u => u.Type == "Id")?.Value),
                    FirstName = userClaims.FirstOrDefault(u => u.Type == "FirstName")?.Value,
                    LastName = userClaims.FirstOrDefault(u => u.Type == "LastName")?.Value,
                    Gender = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Gender)?.Value,
                    Username = userClaims.FirstOrDefault(u => u.Type == "Username")?.Value,
                    Email = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value

                };
            }
            return null;
        }
    }
}
