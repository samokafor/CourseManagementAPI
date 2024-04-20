using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> AddUserAsync(UserDto userDto);
        public UserDto GetUserByEmail(string email);
        Task<UserDto> GetUserById(int id);
    }
}
